﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.Win32.TaskScheduler;
using TechTalk.SpecFlow;
using Warewolf.UI.Tests;
using Microsoft.VisualStudio.TestTools.UITesting;
using System.Diagnostics;
using System.IO;
using System.Net;
using Warewolf.UI.Tests.Settings.SettingsUIMapClasses;
using Warewolf.UI.Tests.Explorer.ExplorerUIMapClasses;

namespace Warewolf.UI.Load.Specs
{
    [Binding]
    class LoadSteps
    {
        [Given(@"there are 30 duplicates of All Tools workflow in the explorer")]
        public void CallDuplicateService_GivenValidComsController_ShouldDuplicate()
        {
            UIMap.AssertStudioIsRunning();
            if (!File.Exists(Environment.ExpandEnvironmentVariables("%programdata%\\Warewolf\\Resources\\All Tools 29.xml")))
            {
                UIMap.Click_Settings_RibbonButton();
                SettingsUIMap.Check_Public_Contribute();
                if (UIMap.MainStudioWindow.SideMenuBar.SaveButton.Enabled)
                {
                    UIMap.Click_Save_Ribbon_Button_With_No_Save_Dialog();
                }
                for (var i = 0; i < 30; i++)
                {
                    using (var webClient = new WebClient())
                    {
                        webClient.Credentials = CredentialCache.DefaultNetworkCredentials;
                        webClient.DownloadData("http://localhost:3142/services/DuplicateResourceService?NewResourceName=All%20Tools%20" + i.ToString() + "&ResourceID=8c1f16c0-b753-41a1-bd5b-6c65326d188d&sourcePath=All%20Tools&destinationPath=");
                    }
                }
                ExplorerUIMap.Click_Explorer_Refresh_Button();
            }
        }

        [AfterFeature]
        public static void RemoveScheduledTasks()
        {
            var localTaskService = ScenarioContext.Current.Get<TaskService>("localTaskService");
            var numberOfTasks = ScenarioContext.Current.Get<String>("numberOfTasks");
            for (var i = int.Parse(numberOfTasks); i > 0; i--)
            {
                localTaskService.GetFolder("Warewolf").DeleteTask("UILoadTest" + i.ToString());
            }
            localTaskService.Dispose();
        }

        [Given(@"I start the timer")]
        [When(@"I start the timer")]
        public void StartTimer()
        {
            if (!ScenarioContext.Current.ContainsKey("StartTime"))
            {
                ScenarioContext.Current.Add("StartTime", System.DateTime.Now);
            }
            else
            {
                ScenarioContext.Current.Set(System.DateTime.Now, "StartTime");
            }
        }
        
        [Then(@"the timer duration is between ""(.*)"" and ""(.*)"" seconds")]
        public void StopTimer(string durationGreaterThan, string durationLessThan)
        {
            var startTime = ScenarioContext.Current.Get<System.DateTime>("StartTime");
            var totalSeconds = (System.DateTime.Now - startTime).TotalSeconds;
            Assert.IsTrue(totalSeconds < int.Parse(durationLessThan) && totalSeconds > int.Parse(durationGreaterThan), "Load test failed. Duration of " + totalSeconds.ToString() + " seconds is greater than " + durationLessThan + " seconds or less than " + durationGreaterThan + ".");
            Console.WriteLine("timer stopped after " + totalSeconds + " seconds.");
        }

        [Given(@"I have ""(.*)"" new workflow tabs open")]
        [When(@"I open ""(.*)"" new workflow tabs")]
        public void OpenManyNewWorkflowTabs(string numberOfTabs)
        {
            for(var i = int.Parse(numberOfTabs); i > 0; i--)
            {
                UIMap.Click_NewWorkflow_RibbonButton();
            }
        }

        [Given(@"I have ""(.*)"" scheduled tasks")]
        public void ManyTasks(string numberOfTasks)
        {
            TaskService localTaskService = new TaskService();
            try
            {
                for (var i = int.Parse(numberOfTasks); i > 0; i--)
                {
                    TaskDefinition td = localTaskService.NewTask();
                    td.RegistrationInfo.Description = "Does something";
                    td.Triggers.Add(new DailyTrigger { DaysInterval = 2 });
                    td.Actions.Add(new ExecAction("cmd.exe", "/c echo WarewolfAgent.exe", null));
                    TaskFolder localWarewolfFolder = localTaskService.GetFolder("Warewolf");
                    if (localWarewolfFolder != null)
                    {
                        localWarewolfFolder.RegisterTaskDefinition(@"UILoadTest" + i.ToString(), td);
                    }
                    else
                    {
                        Assert.Fail("Task scheduler has no Warewolf folder.");
                    }
                }
            }
            finally
            {
                ScenarioContext.Current.Add("localTaskService", localTaskService);
                ScenarioContext.Current.Add("numberOfTasks", numberOfTasks);
            }
        }

        [When(@"I close the Studio")]
        public void CloseStudio()
        {
            var studioProcess = Process.GetProcessesByName("Warewolf Studio");
            if (studioProcess != null && studioProcess.Length > 0)
            {
                ScenarioContext.Current.Add("studioProcess", studioProcess[0].MainModule.FileName);
                Mouse.Click(UIMap.MainStudioWindow.CloseStudioButton);
                studioProcess[0].WaitForExit();
            }
        }

        [When("I start the Studio")]
        public void StartStudio()
        {
            var studioProcess = ScenarioContext.Current.Get<String>("studioProcess");
            var startInfo = new ProcessStartInfo() {
                FileName = studioProcess,
                CreateNoWindow = false,
                UseShellExecute = true,
                WindowStyle = ProcessWindowStyle.Normal
            };
            Process.Start(startInfo);
        }

        #region Additional test attributes

        UIMap UIMap
        {
            get
            {
                if (_UIMap == null)
                {
                    _UIMap = new UIMap();
                }

                return _UIMap;
            }
        }

        private UIMap _UIMap;

        SettingsUIMap SettingsUIMap
        {
            get
            {
                if (_SettingsUIMap == null)
                {
                    _SettingsUIMap = new SettingsUIMap();
                }

                return _SettingsUIMap;
            }
        }

        private SettingsUIMap _SettingsUIMap;

        ExplorerUIMap ExplorerUIMap
        {
            get
            {
                if (_ExplorerUIMap == null)
                {
                    _ExplorerUIMap = new ExplorerUIMap();
                }

                return _ExplorerUIMap;
            }
        }

        private ExplorerUIMap _ExplorerUIMap;

        #endregion
    }
}
