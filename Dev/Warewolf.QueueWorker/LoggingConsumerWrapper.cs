﻿/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2019 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later.
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using System;
using System.Threading.Tasks;
using Warewolf.Auditing;
using Warewolf.Common;
using Warewolf.Data;
using Warewolf.Logging;
using Warewolf.Streams;

namespace QueueWorker
{
    internal class LoggingConsumerWrapper : IConsumer
    {
        private readonly IExecutionLogPublisher _logger;

        private readonly IConsumer _consumer;
        private readonly string _userName;
        private readonly Guid _resourceId;

        public LoggingConsumerWrapper(IExecutionLogPublisher logger, IConsumer consumer, Guid resourceId, string userName)
        {
            _logger = logger;
            _consumer = consumer;
            _resourceId = resourceId;
            _userName = userName;
        }

        public Task<ConsumerResult> Consume(byte[] body)
        {
            var executionId = Guid.NewGuid();
            _logger.StartExecution("processing {body}");
            var startDate = DateTime.UtcNow;

            return _consumer.Consume(body)
                .ContinueWith((requestForwarderResult) =>
                {
                    if (requestForwarderResult.Status == TaskStatus.RanToCompletion)
                    {
                        var endDate = DateTime.UtcNow;
                        var duration = endDate - startDate;

                        if (requestForwarderResult.Result == ConsumerResult.Success)
                        {
                            _logger.Info("success");
                            var executionInfo = new ExecutionInfo(startDate, duration, endDate, Warewolf.Triggers.QueueRunStatus.Success, executionId);
                            var executionEntry = new ExecutionHistory(_resourceId, "", executionInfo, _userName);

                            _logger.ExecutionSucceeded(executionEntry);
                        }
                        else
                        {
                            _logger.Warn("failure");
                            var executionInfo = new ExecutionInfo(startDate, duration, endDate, Warewolf.Triggers.QueueRunStatus.Success, executionId);
                            var executionEntry = new ExecutionHistory(_resourceId, "", executionInfo, _userName);

                            _logger.ExecutionFailed(executionEntry);
                        }
                    }
                    return requestForwarderResult.Result;
                });
        }
    }
}
