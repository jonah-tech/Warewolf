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
using System.Activities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Dev2.Activities.Redis;
using Dev2.Activities.RedisDelete;
using Dev2.Common.Interfaces;
using Dev2.Common.Interfaces.Core;
using Dev2.Common.SaveDialog;
using Dev2.Common.Serializers;
using Dev2.Infrastructure.Tests;
using Dev2.Interfaces;
using Dev2.Runtime.Interfaces;
using Dev2.Runtime.ServiceModel.Data;
using Dev2.Studio.Core;
using Dev2.Studio.Interfaces;
using Dev2.Threading;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TechTalk.SpecFlow;
using Unlimited.Applications.BusinessDesignStudio.Activities;
using Warewolf.Driver.Redis;
using Warewolf.Storage;
using Warewolf.Studio.Core.Infragistics_Prism_Region_Adapter;
using Warewolf.Studio.ViewModels;
using Warewolf.Studio.Views;
using Warewolf.Test.Agent;
using Warewolf.UIBindingTests.Core;

namespace Warewolf.UIBindingTests
{
    [Binding]
    public class RedisSourceSteps
    {
        readonly ScenarioContext _scenarioContext;
        string _illegalCharactersInPath = "Illegal characters in path.";
        public static ContainerLauncher _containerOps;

        public static Stopwatch Stoptime { get; set; }

        public RedisSourceSteps(ScenarioContext scenarioContext)
        {
            if (scenarioContext == null)
            {
                throw new ArgumentNullException("scenarioContext");
            }

            this._scenarioContext = scenarioContext;
        }

        [BeforeFeature(@"RedisSource")]
        public static void SetupForSystem()
        {
            Utils.SetupResourceDictionary();
            var redisSourceControl = new RedisSourceControl();
            var mockStudioUpdateManager = new Mock<IRedisSourceModel>();
            mockStudioUpdateManager.Setup(model => model.ServerName).Returns("localhost");
            var mockRequestServiceNameViewModel = new Mock<IRequestServiceNameViewModel>();
            var mockEventAggregator = new Mock<IEventAggregator>();
            var mockExecutor = new Mock<IExternalProcessExecutor>();
            var task = new Task<IRequestServiceNameViewModel>(() => mockRequestServiceNameViewModel.Object);
            task.Start();
            var redisSourceViewModel = new RedisSourceViewModel(mockStudioUpdateManager.Object, task, mockEventAggregator.Object, new SynchronousAsyncWorker(), mockExecutor.Object);
            redisSourceControl.DataContext = redisSourceViewModel;
            Utils.ShowTheViewForTesting(redisSourceControl);
            FeatureContext.Current.Add(Utils.ViewNameKey, redisSourceControl);
            FeatureContext.Current.Add(Utils.ViewModelNameKey, redisSourceViewModel);
            FeatureContext.Current.Add("updateManager", mockStudioUpdateManager);
            FeatureContext.Current.Add("requestServiceNameViewModel", mockRequestServiceNameViewModel);
            FeatureContext.Current.Add("externalProcessExecutor", mockExecutor);
        }

        [BeforeScenario(@"RedisSource")]
        public void SetupForRedisSource()
        {
            _scenarioContext.Add(Utils.ViewNameKey, FeatureContext.Current.Get<RedisSourceControl>(Utils.ViewNameKey));
            _scenarioContext.Add("updateManager", FeatureContext.Current.Get<Mock<IRedisSourceModel>>("updateManager"));
            _scenarioContext.Add("requestServiceNameViewModel", FeatureContext.Current.Get<Mock<IRequestServiceNameViewModel>>("requestServiceNameViewModel"));
            _scenarioContext.Add("externalProcessExecutor", FeatureContext.Current.Get<Mock<IExternalProcessExecutor>>("externalProcessExecutor"));
            _scenarioContext.Add(Utils.ViewModelNameKey, FeatureContext.Current.Get<RedisSourceViewModel>(Utils.ViewModelNameKey));
        }

        [Then(@"""(.*)"" tab is opened")]
        public void ThenTabIsOpened(string headerText)
        {
            var viewModel = _scenarioContext.Get<IDockAware>("viewModel");
            Assert.AreEqual(headerText, viewModel.Header);
        }

        [Then(@"title is ""(.*)""")]
        public void ThenTitleIs(string title)
        {
            var redisSourceControl = _scenarioContext.Get<RedisSourceControl>(Utils.ViewNameKey);
            var viewModel = _scenarioContext.Get<RedisSourceViewModel>("viewModel");
            Assert.AreEqual(title, viewModel.HeaderText);
            Assert.AreEqual(title, redisSourceControl.GetHeaderText());
        }

        [Given(@"I open New Redis Source")]
        public void GivenIOpenNewRedisSource()
        {
            var redisSourceControl = _scenarioContext.Get<RedisSourceControl>(Utils.ViewNameKey);
            Assert.IsNotNull(redisSourceControl);
        }

        [Given(@"I type HostName as ""(.*)""")]
        [Then(@"I type HostName as ""(.*)""")]
        [When(@"I change HostName to ""(.*)""")]
        public void ThenITypeHostNameAs(string hostName)
        {
            var redisSourceControl = _scenarioContext.Get<RedisSourceControl>(Utils.ViewNameKey);
            redisSourceControl.EnterHostName(hostName);
            var viewModel = _scenarioContext.Get<RedisSourceViewModel>("viewModel");
            Assert.AreEqual(hostName, viewModel.HostName);
        }

        [Then(@"server port is ""(.*)""")]
        public void ThenServerPortIs(int port)
        {
            var viewModel = _scenarioContext.Get<RedisSourceViewModel>("viewModel");
            var redisSourceControl = _scenarioContext.Get<RedisSourceControl>(Utils.ViewNameKey);
            Assert.AreEqual(port.ToString(), viewModel.Port);
            Assert.AreEqual(port.ToString(), redisSourceControl.GetPort());
        }

        [Given(@"I type port number as ""(.*)""")]
        [Then(@"I type port number as ""(.*)""")]
        [When(@"I change port number to ""(.*)""")]
        public void ThenITypePortNumberAs(string portNumber)
        {
            var redisSourceControl = _scenarioContext.Get<RedisSourceControl>(Utils.ViewNameKey);
            redisSourceControl.EnterPortNumber(portNumber);
            var viewModel = _scenarioContext.Get<RedisSourceViewModel>("viewModel");
            Assert.AreEqual(portNumber, viewModel.Port);
        }

        [Given(@"I open ""(.*)"" redis source")]
        public void GivenIOpenRedisSource(string resourceName)
        {
            var redisSourceControl = _scenarioContext.Get<RedisSourceControl>(Utils.ViewNameKey);
            var mockStudioUpdateManager = new Mock<IRedisSourceModel>();
            mockStudioUpdateManager.Setup(model => model.ServerName).Returns("localhost");
            var mockEventAggregator = new Mock<IEventAggregator>();
            var mockExecutor = new Mock<IExternalProcessExecutor>();

            var username = @"dev2\IntegrationTester";
            var password = TestEnvironmentVariables.GetVar(username);
            var redisSourceDefinition = new RedisSourceDefinition
            {
                Name = "Test-Redis",
                HostName = "http://RSAKLFSVRTFSBLD/IntegrationTestSite",
                Password = password,
                Port = "6379"
            };
            mockStudioUpdateManager.Setup(model => model.FetchSource(It.IsAny<Guid>()))
                .Returns(redisSourceDefinition);
            var redisSourceViewModel = new RedisSourceViewModel(mockStudioUpdateManager.Object, mockEventAggregator.Object, redisSourceDefinition, new SynchronousAsyncWorker(), mockExecutor.Object);
            redisSourceControl.DataContext = redisSourceViewModel;
            _scenarioContext.Remove("viewModel");
            _scenarioContext.Add("viewModel", redisSourceViewModel);
        }

        [Given(@"HostName is ""(.*)""")]
        [When(@"HostName is ""(.*)""")]
        [Then(@"HostName is ""(.*)""")]
        public void GivenHostNameIs(string hostName)
        {
            var redisSourceControl = _scenarioContext.Get<RedisSourceControl>(Utils.ViewNameKey);
            var viewModel = _scenarioContext.Get<RedisSourceViewModel>("viewModel");
            Assert.AreEqual(hostName, viewModel.HostName);
            Assert.AreEqual(hostName, redisSourceControl.GetHostName());
        }

        [Given(@"Password is ""(.*)""")]
        public void GivenPasswordIs(string password)
        {
            var redisSourceControl = _scenarioContext.Get<RedisSourceControl>(Utils.ViewNameKey);
            var viewModel = _scenarioContext.Get<RedisSourceViewModel>("viewModel");
            Assert.AreEqual(password, redisSourceControl.GetPassword());
        }

        [When(@"I Cancel the Test")]
        public void WhenICancelTheTest()
        {
            var redisSourceControl = _scenarioContext.Get<RedisSourceControl>(Utils.ViewNameKey);
            redisSourceControl.CancelTest();
        }

        [Given(@"Validation message is thrown")]
        [When(@"Validation message is thrown")]
        [Then(@"Validation message is thrown")]
        public void WhenValidationMessageIsThrown()
        {
            var redisSourceControl = _scenarioContext.Get<RedisSourceControl>(Utils.ViewNameKey);
            var viewModel = _scenarioContext.Get<RedisSourceViewModel>("viewModel");
            var errorMessageFromControl = redisSourceControl.GetErrorMessage();
            var errorMessageOnViewModel = viewModel.TestMessage;
            Assert.AreNotEqual(string.IsNullOrEmpty(errorMessageFromControl), errorMessageOnViewModel);
            var isErrorMessage = !errorMessageOnViewModel.Contains("Passed");
            Assert.IsTrue(isErrorMessage);
        }

        [Then(@"Validation message is ""(.*)""")]
        public void ThenValidationMessageIs(string msg)
        {
            var redisSourceControl = _scenarioContext.Get<RedisSourceControl>(Utils.ViewNameKey);
            var viewModel = _scenarioContext.Get<RedisSourceViewModel>("viewModel");
            var errorMessageFromControl = redisSourceControl.GetErrorMessage();
            var errorMessageOnViewModel = viewModel.TestMessage;
            var isErrorMessageOnControl = errorMessageFromControl.Equals(msg, StringComparison.OrdinalIgnoreCase);
            Assert.IsTrue(isErrorMessageOnControl);
            var isErrorMessage = errorMessageOnViewModel.Equals(msg, StringComparison.OrdinalIgnoreCase);
            Assert.IsTrue(isErrorMessage);
        }

        [When(@"Validation message is Not thrown")]
        public void WhenValidationMessageIsNotThrown()
        {
            var redisSourceControl = _scenarioContext.Get<RedisSourceControl>(Utils.ViewNameKey);
            var viewModel = _scenarioContext.Get<RedisSourceViewModel>("viewModel");
            var errorMessageFromControl = redisSourceControl.GetErrorMessage();
            var errorMessageOnViewModel = viewModel.TestMessage;
            var isErrorMessageOnViewModel = !errorMessageOnViewModel.Contains("Passed");
            var isErrorMessageOnControl = !errorMessageFromControl.Contains("Passed");
            Assert.IsFalse(isErrorMessageOnViewModel);
            Assert.IsFalse(isErrorMessageOnControl);
        }

        [Given(@"Password field is ""(.*)""")]
        [When(@"Password field is ""(.*)""")]
        [Then(@"Password field is ""(.*)""")]
        public void WhenPasswordFieldIs(string visibility)
        {
            var expectedVisibility = String.Equals(visibility, "Collapsed", StringComparison.InvariantCultureIgnoreCase) ? Visibility.Collapsed : Visibility.Visible;
            var redisSourceControl = _scenarioContext.Get<RedisSourceControl>(Utils.ViewNameKey);
            var databaseDropDownVisibility = redisSourceControl.GetPasswordVisibility();
            Assert.AreEqual(expectedVisibility, databaseDropDownVisibility);
        }

        [When(@"Password field")]
        public void WhenPasswordField()
        {
            var redisSourceControl = _scenarioContext.Get<RedisSourceControl>(Utils.ViewNameKey);
            var viewModel = _scenarioContext.Get<RedisSourceViewModel>("viewModel");
            var username = @"dev2\IntegrationTester";
            var password = TestEnvironmentVariables.GetVar(username);
            Assert.AreEqual(password, viewModel.Password);
            Assert.AreEqual(password, redisSourceControl.GetPassword());
        }

        [Given(@"I type Password")]
        [When(@"I type Password")]
        [Then(@"I type Password")]
        public void WhenITypePassword()
        {
            var redisSourceControl = _scenarioContext.Get<RedisSourceControl>(Utils.ViewNameKey);
            var username = @"dev2\IntegrationTester";
            var password = TestEnvironmentVariables.GetVar(username);
            redisSourceControl.EnterPassword(password);
            var viewModel = _scenarioContext.Get<RedisSourceViewModel>("viewModel");
            Assert.AreEqual(password, viewModel.Password);
        }

        [When(@"the error message is ""(.*)""")]
        public void WhenTheErrorMessageIs(string errorMessage)
        {
            errorMessage = "Exception: " + _illegalCharactersInPath + Environment.NewLine + Environment.NewLine +
                           "Inner Exception: " + _illegalCharactersInPath;

            var viewModel = ScenarioContext.Current.Get<RedisSourceViewModel>("viewModel");
            Assert.AreEqual(errorMessage, viewModel.TestMessage);
        }

        [Given(@"""(.*)"" is ""(.*)""")]
        [When(@"""(.*)"" is ""(.*)""")]
        [Then(@"""(.*)"" is ""(.*)""")]
        public void GivenIs(string controlName, string enabledString)
        {
            Utils.CheckControlEnabled(controlName, enabledString, _scenarioContext.Get<ICheckControlEnabledView>(Utils.ViewNameKey), Utils.ViewNameKey);
        }

        [Then(@"Test Connecton is ""(.*)""")]
        [When(@"Test Connecton is ""(.*)""")]
        public void ThenTestConnectonIs(string successString)
        {
            var mockUpdateManager = _scenarioContext.Get<Mock<IRedisSourceModel>>("updateManager");
            var isSuccess = String.Equals(successString, "Successful", StringComparison.InvariantCultureIgnoreCase);
            var isLongRunning = String.Equals(successString, "Long Running", StringComparison.InvariantCultureIgnoreCase);
            if (isSuccess)
            {
                mockUpdateManager.Setup(manager => manager.TestConnection(It.IsAny<IRedisServiceSource>()));
            }
            else if (isLongRunning)
            {
                var viewModel = _scenarioContext.Get<RedisSourceViewModel>("viewModel");
                mockUpdateManager.Setup(manager => manager.TestConnection(It.IsAny<IRedisServiceSource>()));
                viewModel.AsyncWorker = new AsyncWorker();
            }
            else
            {
                mockUpdateManager.Setup(manager => manager.TestConnection(It.IsAny<IRedisServiceSource>()))
                    .Throws(new WarewolfTestException(_illegalCharactersInPath, new Exception(_illegalCharactersInPath)));
            }
            var redisSourceControl = _scenarioContext.Get<RedisSourceControl>(Utils.ViewNameKey);
            redisSourceControl.PerformTestConnection();
        }

        [When(@"I save the source")]
        public void WhenISaveTheSource()
        {
            var mockRequestServiceNameViewModel = _scenarioContext.Get<Mock<IRequestServiceNameViewModel>>("requestServiceNameViewModel");
            mockRequestServiceNameViewModel.Setup(model => model.ShowSaveDialog()).Verifiable();
            var redisSourceControl = _scenarioContext.Get<RedisSourceControl>(Utils.ViewNameKey);
            redisSourceControl.PerformSave();
        }

        [Then(@"the save dialog is opened")]
        public void ThenTheSaveDialogIsOpened()
        {
            var mockRequestServiceNameViewModel = _scenarioContext.Get<Mock<IRequestServiceNameViewModel>>("requestServiceNameViewModel");
            mockRequestServiceNameViewModel.Verify();
        }

        [When(@"I save as ""(.*)""")]
        public void WhenISaveAs(string resourceName)
        {
            var mockRequestServiceNameViewModel = _scenarioContext.Get<Mock<IRequestServiceNameViewModel>>("requestServiceNameViewModel");
            mockRequestServiceNameViewModel.Setup(model => model.ResourceName).Returns(new ResourceName("", resourceName));
            mockRequestServiceNameViewModel.Setup(model => model.ShowSaveDialog()).Returns(MessageBoxResult.OK);
            var redisSourceControl = _scenarioContext.Get<RedisSourceControl>(Utils.ViewNameKey);
            redisSourceControl.PerformSave();
        }

        [Given(@"I Select Authentication Type as ""(.*)""")]
        [When(@"I Select Authentication Type as ""(.*)""")]
        [Then(@"I Select Authentication Type as ""(.*)""")]
        [Then(@"Select Authentication Type as ""(.*)""")]
        [When(@"I edit Authentication Type as ""(.*)""")]
        [Given(@"Select Authentication Type as ""(.*)""")]
        public void ThenSelectAuthenticationTypeAs(string authenticationTypeString)
        {
            var authenticationType = String.Equals(authenticationTypeString, "Password",
                StringComparison.OrdinalIgnoreCase)
                ? AuthenticationType.Password
                : AuthenticationType.Anonymous;

            var redisSourceControl = _scenarioContext.Get<RedisSourceControl>(Utils.ViewNameKey);
            redisSourceControl.SetAuthenticationType(authenticationType);
            var viewModel = _scenarioContext.Get<RedisSourceViewModel>("viewModel");
            Assert.AreEqual(authenticationType, viewModel.AuthenticationType);
        }

        [Given(@"Redis source ""(.*)""")]
        public void GivenRedisSource(string hostName)
        {
            SetUpRedisSourceViewModel(hostName);
        }

        [Then(@"I have a key ""(.*)""")]
        [Given(@"I have a key ""(.*)""")]
        public void GivenIHaveAKey(string key)
        {
            var hostName = _scenarioContext.Get<string>("hostName");
            var redisImpl = GetRedisCacheImpl(hostName);
            GenResourceAndDataobject(key, hostName, out Mock<IResourceCatalog> mockResourceCatalog, out Mock<IDSFDataObject> mockDataobject, out ExecutionEnvironment environment);

            var assignActivity = new DsfMultiAssignActivity();
            var ttl = 3000;
            var redisActivityNew = GetRedisActivity(mockResourceCatalog.Object, key, ttl, hostName, redisImpl, assignActivity);

            _scenarioContext.Add(nameof(RedisActivity), redisActivityNew);
            _scenarioContext.Add(nameof(RedisCacheImpl), redisImpl);
            _scenarioContext.Add(nameof(ttl), ttl);

            Assert.IsNotNull(redisActivityNew.Key);
        }
        [Then(@"I add another key ""(.*)""")]
        [Given(@"I add another key ""(.*)""")]
        public void GivenIAddAnotherKey(string key)
        {
            var hostName = _scenarioContext.Get<string>("hostName");
            var redisImpl = GetRedisCacheImpl(hostName);
            GenResourceAndDataobject(key, hostName, out Mock<IResourceCatalog> mockResourceCatalog, out Mock<IDSFDataObject> mockDataobject, out ExecutionEnvironment environment);

            var assignActivity = new DsfMultiAssignActivity();
            var ttl = 3000;
            var redisActivityNew = GetRedisActivity(mockResourceCatalog.Object, key, ttl, hostName, redisImpl, assignActivity);

            _scenarioContext.Remove(nameof(RedisActivity));
            _scenarioContext.Remove(nameof(RedisCacheImpl));
            _scenarioContext.Remove(nameof(ttl));

            _scenarioContext.Add(nameof(RedisActivity), redisActivityNew);
            _scenarioContext.Add(nameof(RedisCacheImpl), redisImpl);
            _scenarioContext.Add(nameof(ttl), ttl);

            Assert.IsNotNull(redisActivityNew.Key);
        }

        [Then(@"I have an existing key to delete ""(.*)""")]
        public void GivenIHaveAKeyToDelete(string key)
        {
            var hostName = _scenarioContext.Get<string>("hostName");
            var redisImpl = GetRedisCacheImpl(hostName);
            GenResourceAndDataobject(key, hostName, out Mock<IResourceCatalog> mockResourceCatalog, out Mock<IDSFDataObject> mockDataobject, out ExecutionEnvironment environment);

            var redisDeleteActivityNew = GetRedisDeleteActivity(mockResourceCatalog.Object, key, hostName, redisImpl);

            _scenarioContext.Add(nameof(RedisDeleteActivity), redisDeleteActivityNew);

            Assert.IsNotNull(redisDeleteActivityNew.Key);
        }

        [Given(@"No data in the cache")]
        public void GivenNoDataInTheCache()
        {
            var redisActivityOld = _scenarioContext.Get<SpecRedisActivity>(nameof(RedisActivity));
            var impl = _scenarioContext.Get<RedisCacheImpl>(nameof(RedisCacheImpl));
            var ttl = _scenarioContext.Get<int>("ttl");

            var environment = new ExecutionEnvironment();

            var key = environment.EvalToExpression(redisActivityOld.Key, 0);

            do
            {
                Thread.Sleep(1000);
            } while (Stoptime.ElapsedMilliseconds < ttl);

            var actualCachedData = GetCachedData(impl, key);
            Assert.IsNull(actualCachedData);
        }

        [Then(@"The ""(.*)"" Cache has been deleted")]
        public void CacheHasBeenDeleted(string key)
        {
            var hostName = _scenarioContext.Get<string>("hostName");
            var redisImpl = GetRedisCacheImpl(hostName);

            var actualCachedData = GetCachedData(redisImpl, key);
            Assert.IsNull(actualCachedData, key + ": Cache still exists");
        }

        [Then(@"The ""(.*)"" Cache exists")]
        public void CacheExists(string key)
        {
            var hostName = _scenarioContext.Get<string>("hostName");
            var redisImpl = GetRedisCacheImpl(hostName);

            var actualCachedData = GetCachedData(redisImpl, key);
            Assert.IsNotNull(actualCachedData, key + ": Cache does not exists");
        }
        [Given(@"an assign ""(.*)"" as")]
        [Then(@"an assign ""(.*)"" as")]
        public void GivenAnAssignAs(string data, Table table)
        {
            var redisActivity = _scenarioContext.Get<SpecRedisActivity>(nameof(RedisActivity));

            var assignActivity = GetDsfMultiAssignActivity("[[Var1]]", "Test1");

            redisActivity.ActivityFunc = new ActivityFunc<string, bool> { Handler = assignActivity };

            var assignOutputs = assignActivity.GetForEachOutputs();

            GetExpectedTableData(table, 0, out string expectedKey, out string expectedValue);

            Assert.AreEqual(expectedKey, assignOutputs[0].Value);
            Assert.IsTrue(expectedValue.Contains(assignOutputs[0].Name));

            var dic = new Dictionary<string, string> { { assignOutputs[0].Value, assignOutputs[0].Name } };

            _scenarioContext.Remove(nameof(RedisActivity));
            _scenarioContext.Add(nameof(RedisActivity), redisActivity);
            _scenarioContext.Add(data, dic);
            _scenarioContext.Add(nameof(DsfMultiAssignActivity), assignActivity);

        }

        [Given(@"another assign ""(.*)"" as")]
        [Then(@"another assign ""(.*)"" as")]
        public void GivenAnotherAssignAs(string data, Table table)
        {
            var redisActivity = _scenarioContext.Get<SpecRedisActivity>(nameof(RedisActivity));

            var assignActivity = GetDsfMultiAssignActivity("[[Var3]]", "Test4");

            redisActivity.ActivityFunc = new ActivityFunc<string, bool> { Handler = assignActivity };

            var assignOutputs = assignActivity.GetForEachOutputs();

            GetExpectedTableData(table, 0, out string expectedKey, out string expectedValue);

            Assert.AreEqual(expectedKey, assignOutputs[0].Value);
            Assert.IsTrue(expectedValue.Contains(assignOutputs[0].Name));

            var dic = new Dictionary<string, string> { { assignOutputs[0].Value, assignOutputs[0].Name } };

            _scenarioContext.Remove(nameof(RedisActivity));
            _scenarioContext.Remove(nameof(DsfMultiAssignActivity));

            _scenarioContext.Add(nameof(RedisActivity), redisActivity);
            _scenarioContext.Add(data, dic);
            _scenarioContext.Add(nameof(DsfMultiAssignActivity), assignActivity);

        }


        [Then(@"I execute the get/set tool")]
        [When(@"I execute the get/set tool")]
        public void WhenIExecuteTheTool()
        {
            var redisActivityOld = _scenarioContext.Get<SpecRedisActivity>(nameof(RedisActivity));
            var dataToStore = _scenarioContext.Get<Dictionary<string, string>>("dataToStore");
            var assignActivity = _scenarioContext.Get<DsfMultiAssignActivity>(nameof(DsfMultiAssignActivity));
            var hostName = _scenarioContext.Get<string>("hostName");
            var impl = _scenarioContext.Get<RedisCacheImpl>(nameof(RedisCacheImpl));

            GenResourceAndDataobject(redisActivityOld.Key, hostName, out Mock<IResourceCatalog> mockResourceCatalog, out Mock<IDSFDataObject> mockDataobject, out ExecutionEnvironment environment);

            ExecuteGetSetTool(redisActivityOld, mockDataobject);
        }
        [When(@"I execute the delete tool")]
        [Then(@"I execute the delete tool")]
        public void WhenIExecuteTheDeleteTool()
        {
            var redisActivityOld = _scenarioContext.Get<SpecRedisDeleteActivity>(nameof(RedisDeleteActivity));
            var dataToStore = _scenarioContext.Get<Dictionary<string, string>>("dataToStore");
            var hostName = _scenarioContext.Get<string>("hostName");
            var impl = _scenarioContext.Get<RedisCacheImpl>(nameof(RedisCacheImpl));

            GenResourceAndDataobject(redisActivityOld.Key, hostName, out Mock<IResourceCatalog> mockResourceCatalog, out Mock<IDSFDataObject> mockDataobject, out ExecutionEnvironment environment);

            ExecuteDeleteTool(redisActivityOld, mockDataobject);
        }

        [Then(@"the cache will contain")]
        public void ThenTheCacheWillContain(Table table)
        {
            var redisActivity = _scenarioContext.Get<SpecRedisActivity>(nameof(RedisActivity));
            var impl = _scenarioContext.Get<RedisCacheImpl>(nameof(RedisCacheImpl));

            var actualCacheData = GetCachedData(impl, redisActivity.Key);

            GetExpectedTableData(table, 0, out string expectedKey, out string expectedValue);

            Assert.IsTrue(expectedValue.Contains(actualCacheData.Keys.ToList()[0]));
            Assert.IsTrue(expectedValue.Contains(actualCacheData.Values.ToList()[0]));

            _scenarioContext.Add(redisActivity.Key, actualCacheData);
        }

        [Then(@"output variables have the following values")]
        public void ThenOutputVariablesHaveTheFollowingValues(Table table)
        {
            var redisActivity = _scenarioContext.Get<RedisActivity>(nameof(RedisActivity));
            var impl = _scenarioContext.Get<RedisCacheImpl>(nameof(RedisCacheImpl));
            var actualCacheData = GetCachedData(impl, redisActivity.Key);

            GetExpectedTableData(table, 0, out string expectedKey, out string expectedValue);

            Assert.AreEqual(expected: expectedKey, actual: actualCacheData.Keys.ToArray()[0]);
            Assert.IsTrue(expectedValue.Contains(actualCacheData.Values.ToArray()[0]));

        }

        [Given(@"data exists \(TTL not hit\) for key ""(.*)"" as")]
        public void GivenDataExistsTTLNotHitForKeyAs(string key, Table table)
        {
            var hostName = _scenarioContext.Get<string>("hostName");
            var redisImpl = GetRedisCacheImpl(hostName);

            GenResourceAndDataobject(key, hostName, out Mock<IResourceCatalog> mockResourceCatalog, out Mock<IDSFDataObject> mockDataobject, out ExecutionEnvironment environment);

            var dataStored = new Dictionary<string, string> { { "[[Var1]]", "Data in cache" } };

            var assignActivity = GetDsfMultiAssignActivity(dataStored.Keys.ToArray()[0], dataStored.Values.ToArray()[0]);

            var ttl = 3000;
            var redisActivityNew = GetRedisActivity(mockResourceCatalog.Object, key, ttl, hostName, redisImpl, assignActivity);

            ExecuteGetSetTool(redisActivityNew, mockDataobject);

            var sdfsf = redisActivityNew.SpecPerformExecution(dataStored);

            var actualDataStored = GetCachedData(redisImpl, key);

            GetExpectedTableData(table, 0, out string expectedKey, out string expectedValue);

            Assert.AreEqual(expectedKey, key);
            Assert.IsTrue(expectedValue.Contains(actualDataStored.Keys.ToArray()[0]));
            Assert.IsTrue(expectedValue.Contains(actualDataStored.Values.ToArray()[0]));

            _scenarioContext.Add(redisActivityNew.Key, actualDataStored);
            _scenarioContext.Remove(nameof(RedisActivity));
            _scenarioContext.Add(nameof(RedisActivity), redisActivityNew);
        }

        [Then(@"the assign ""(.*)"" is not executed")]
        public void ThenTheAssignIsNotExecuted(string key)
        {
            var dataToStore = _scenarioContext.Get<IDictionary<string, string>>(key);

            Assert.IsNotNull(dataToStore);
        }

        [Given(@"data does not exist \(TTL exceeded\) for key ""(.*)"" as")]
        public void GivenDataDoesNotExistTTLExceededForKeyAs(string p0, Table table)
        {
            var ttl = _scenarioContext.Get<int>("ttl");
            var redisActivity = _scenarioContext.Get<RedisActivity>(nameof(RedisActivity));
            var impl = _scenarioContext.Get<RedisCacheImpl>(nameof(RedisCacheImpl));

            do
            {
                Thread.Sleep(1000);
            } while (Stoptime.ElapsedMilliseconds < ttl);

            var actualCachedData = GetCachedData(impl, redisActivity.Key);

            Assert.IsNull(actualCachedData);
        }

        [Then(@"the assign ""(.*)"" is executed")]
        public void ThenTheAssignIsExecuted(string p0)
        {
            var dataToStore = _scenarioContext.Get<Dictionary<string, string>>(p0);
            var redisActivity = _scenarioContext.Get<SpecRedisActivity>(nameof(RedisActivity));

            var executioResult = redisActivity.SpecPerformExecution(dataToStore);

            Assert.AreEqual("Success", executioResult[0]);
        }


        private static void GenResourceAndDataobject(string key, string hostName, out Mock<IResourceCatalog> mockResourceCatalog, out Mock<IDSFDataObject> mockDataobject, out ExecutionEnvironment environment)
        {
            mockResourceCatalog = new Mock<IResourceCatalog>();
            mockDataobject = new Mock<IDSFDataObject>();
            var redisSource = new Dev2.Data.ServiceModel.RedisSource { HostName = hostName };
            mockResourceCatalog.Setup(o => o.GetResource<Dev2.Data.ServiceModel.RedisSource>(It.IsAny<Guid>(), It.IsAny<Guid>())).Returns(redisSource);

            environment = new ExecutionEnvironment();
            environment.Assign(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>());
            environment.EvalToExpression(key, 0);

            mockDataobject.Setup(o => o.IsDebugMode()).Returns(true);
            mockDataobject.Setup(o => o.Environment).Returns(environment);
        }

        private static void GetExpectedTableData(Table table, int rowNumber, out string expectedKey, out string expectedValue)
        {
            var expectedRow = table.Rows[rowNumber].Values.ToList();

            expectedKey = expectedRow[0];
            expectedValue = expectedRow[1];
        }

        private IDictionary<string, string> GetCachedData(RedisCacheImpl impl, string key)
        {
            var actualCacheData = impl.Get(key);

            if (actualCacheData != null)
            {
                var serializer = new Dev2JsonSerializer();
                return serializer.Deserialize<IDictionary<string, string>>(actualCacheData);
            }
            return null;
        }

        private DsfMultiAssignActivity GetDsfMultiAssignActivity(string fieldName, string fieldValue)
        {
            return new DsfMultiAssignActivity() { FieldsCollection = new List<ActivityDTO> { new ActivityDTO(fieldName, fieldValue, 1) } };
        }

        private RedisCacheImpl GetRedisCacheImpl(string hostName)
        {
            return new RedisCacheImpl(hostName);
        }

        private static SpecRedisActivity GetRedisActivity(IResourceCatalog resourceCatalog, string key, int ttl, string hostName, RedisCacheImpl impl, DsfMultiAssignActivity assignActivity)
        {
            Stoptime = Stopwatch.StartNew();
            return new SpecRedisActivity(resourceCatalog, impl)
            {
                Key = key,
                ActivityFunc = new ActivityFunc<string, bool> { Handler = assignActivity },
                TTL = ttl,
                RedisSource = new Dev2.Data.ServiceModel.RedisSource { HostName = hostName },
            };
        }
        private static SpecRedisDeleteActivity GetRedisDeleteActivity(IResourceCatalog resourceCatalog, string key, string hostName, RedisCacheImpl impl)
        {
            return new SpecRedisDeleteActivity(resourceCatalog, impl)
            {
                Key = key,
                RedisSource = new Dev2.Data.ServiceModel.RedisSource { HostName = hostName },
            };
        }
        private void SetUpRedisSourceViewModel(string hostName)
        {
            var redisSourceControl = _scenarioContext.Get<RedisSourceControl>(Utils.ViewNameKey);
            var mockStudioUpdateManager = new Mock<IRedisSourceModel>();
            mockStudioUpdateManager.Setup(model => model.ServerName).Returns(hostName);
            var mockEventAggregator = new Mock<IEventAggregator>();
            var mockExecutor = new Mock<IExternalProcessExecutor>();

            var username = @"dev2\IntegrationTester";
            var password = TestEnvironmentVariables.GetVar(username);
            var redisSourceDefinition = new RedisSourceDefinition
            {
                Name = "Test-Redis",
                HostName = "http://RSAKLFSVRTFSBLD/IntegrationTestSite",
                Password = password,
                Port = "6379"
            };
            //Test Locally
            //var redisSourceDefinition = new RedisSourceDefinition
            //{
            //    Name = "localhost",
            //    HostName = "127.0.0.1",
            //    Password = "",
            //    Port = "6379"
            //};
            mockStudioUpdateManager.Setup(model => model.FetchSource(It.IsAny<Guid>()))
                .Returns(redisSourceDefinition);
            var redisSourceViewModel = new RedisSourceViewModel(mockStudioUpdateManager.Object, mockEventAggregator.Object, redisSourceDefinition, new SynchronousAsyncWorker(), mockExecutor.Object);
            redisSourceControl.DataContext = redisSourceViewModel;

            _scenarioContext.Remove("viewModel");
            _scenarioContext.Add("viewModel", redisSourceViewModel);

            _scenarioContext.Add("hostName", hostName);
        }

        private static void ExecuteGetSetTool(SpecRedisActivity redisActivity, Mock<IDSFDataObject> mockDataobject)
        {
            redisActivity.SpecExecuteTool(mockDataobject.Object);
        }
        private static void ExecuteDeleteTool(SpecRedisDeleteActivity redisDeleteActivity, Mock<IDSFDataObject> mockDataobject)
        {
            redisDeleteActivity.SpecExecuteTool(mockDataobject.Object);
        }
        class SpecRedisActivity : RedisActivity
        {
            public SpecRedisActivity()
            {
            }

            public SpecRedisActivity(IResourceCatalog resourceCatalog, RedisCacheBase redisCache) : base(resourceCatalog, redisCache)
            {
            }

            public List<string> SpecPerformExecution(Dictionary<string, string> evaluatedValues)
            {
                return base.PerformExecution(evaluatedValues);
            }

            public void SpecExecuteTool(IDSFDataObject dataObject)
            {
                base.ExecuteTool(dataObject, 0);
            }

        }

        class SpecRedisDeleteActivity : RedisDeleteActivity
        {
            public SpecRedisDeleteActivity()
            {
            }

            public SpecRedisDeleteActivity(IResourceCatalog resourceCatalog, RedisCacheBase redisCache) : base(resourceCatalog, redisCache)
            {
            }

            public List<string> SpecPerformExecution(Dictionary<string, string> evaluatedValues)
            {
                return base.PerformExecution(evaluatedValues);
            }

            public void SpecExecuteTool(IDSFDataObject dataObject)
            {
                base.ExecuteTool(dataObject, 0);
            }

        }
        [AfterScenario(@"RedisSource")]
        public void Cleanup()
        {
            var mockExecutor = new Mock<IExternalProcessExecutor>();
            var mockUpdateManager = _scenarioContext.Get<Mock<IRedisSourceModel>>("updateManager");
            var mockRequestServiceNameViewModel = _scenarioContext.Get<Mock<IRequestServiceNameViewModel>>("requestServiceNameViewModel");
            var mockEventAggregator = new Mock<IEventAggregator>();
            var task = new Task<IRequestServiceNameViewModel>(() => mockRequestServiceNameViewModel.Object);
            task.Start();
            var viewModel = new RedisSourceViewModel(mockUpdateManager.Object, task, mockEventAggregator.Object, new SynchronousAsyncWorker(), mockExecutor.Object);
            var redisSourceControl = _scenarioContext.Get<RedisSourceControl>(Utils.ViewNameKey);
            redisSourceControl.DataContext = viewModel;
            FeatureContext.Current.Remove("viewModel");
            FeatureContext.Current.Add("viewModel", viewModel);
            FeatureContext.Current.Remove("externalProcessExecutor");
            FeatureContext.Current.Add("externalProcessExecutor", mockExecutor);
        }
    }
}