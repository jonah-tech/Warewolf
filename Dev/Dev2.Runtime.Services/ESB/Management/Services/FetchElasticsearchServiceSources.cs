﻿#pragma warning disable
/*
*  Warewolf - Once bitten, there's no going back
*  Copyright 2020 by Warewolf Ltd <alpha@warewolf.io>
*  Licensed under GNU Affero General Public License 3.0 or later.
*  Some rights reserved.
*  Visit our website for more information <http://warewolf.io/>
*  AUTHORS <http://warewolf.io/authors.php> , CONTRIBUTORS <http://warewolf.io/contributors.php>
*  @license GNU Affero General Public License <http://www.gnu.org/licenses/agpl-3.0.html>
*/

using Dev2.Common;
using Dev2.Common.Interfaces;
using Dev2.Common.Interfaces.Core;
using Dev2.Communication;
using Dev2.Data.ServiceModel;
using Dev2.DynamicServices;
using Dev2.Runtime.Hosting;
using Dev2.Runtime.Interfaces;
using Dev2.Workspaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Dev2.Runtime.ESB.Management.Services
{
    public class FetchElasticsearchServiceSources : DefaultEsbManagementEndpoint
    {
        public override StringBuilder Execute(Dictionary<string, StringBuilder> values, IWorkspace theWorkspace)
        {
            var serializer = new Dev2JsonSerializer();
            
            var list = ResourceCatalog.Instance.GetResourceList<ElasticsearchSource>(GlobalConstants.ServerWorkspaceID).Select(a =>
            {
                if (a is ElasticsearchSource res)
                {
                    return new ElasticsearchSourceDefinition
                    {
                        Id = res.ResourceID,
                        Name = res.ResourceName,
                        HostName = res.HostName,
                        Port = res.Port,
                        Path = res.GetSavePath(),
                        Username = res.Username,
                        Password = res.Password,
                        AuthenticationType = res.AuthenticationType,
                        SearchIndex = res.SearchIndex
                    } as IElasticsearchSourceDefinition;
                }
                return null;
            }).ToList();
            return serializer.SerializeToBuilder(new ExecuteMessage { HasError = false, Message = serializer.SerializeToBuilder(list) });
            
        }

        public IResourceCatalog Resources => ResourceCatalog.Instance;

        public override DynamicService CreateServiceEntry() => EsbManagementServiceEntry.CreateESBManagementServiceEntry(HandlesType(), "<DataList><Dev2System.ManagmentServicePayload ColumnIODirection=\"Both\"></Dev2System.ManagmentServicePayload></DataList>");

        public override string HandlesType() => "FetchElasticsearchServiceSources";
    }
}