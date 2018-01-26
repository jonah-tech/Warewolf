﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.2.0.0
//      SpecFlow Generator Version:2.2.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Dev2.Activities.Specs.Merge
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.2.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClassAttribute()]
    public partial class MergeExecutionFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "MergeExecution.feature"
#line hidden
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassInitializeAttribute()]
        public static void FeatureSetup(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner(null, 0);
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "MergeExecution", "\tIn order to avoid silly mistakes\r\n\tAs a math idiot\r\n\tI want to be told the sum o" +
                    "f two numbers", ProgrammingLanguage.CSharp, new string[] {
                        "WorkflowMerging"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.ClassCleanupAttribute()]
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestInitializeAttribute()]
        public virtual void TestInitialize()
        {
            if (((testRunner.FeatureContext != null) 
                        && (testRunner.FeatureContext.FeatureInfo.Title != "MergeExecution")))
            {
                global::Dev2.Activities.Specs.Merge.MergeExecutionFeature.FeatureSetup(null);
            }
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCleanupAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Merge AssignOnlyWithNoOutput Workflow with Same Version")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "MergeExecution")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("WorkflowMerging")]
        public virtual void MergeAssignOnlyWithNoOutputWorkflowWithSameVersion()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Merge AssignOnlyWithNoOutput Workflow with Same Version", ((string[])(null)));
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
  testRunner.Given("I Load workflow \"AssignOnlyWithNoOutput\" from \"localhost\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 9
  testRunner.And("I Load workflow \"AssignOnlyWithNoOutput\" from \"Remote Connection Integration\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
  testRunner.When("Merge Window is opened with remote \"AssignOnlyWithNoOutput\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 11
  testRunner.Then("Current workflow contains \"1\" tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 12
  testRunner.And("Different workflow contains \"1\" tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
  testRunner.And("Merge conflicts count is \"1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
  testRunner.And("Merge variable conflicts is false", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 15
  testRunner.And("Merge window has no Conflicting tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Merge VersionHelloWorld Workflow")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "MergeExecution")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("WorkflowMerging")]
        public virtual void MergeVersionHelloWorldWorkflow()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Merge VersionHelloWorld Workflow", ((string[])(null)));
#line 18
this.ScenarioSetup(scenarioInfo);
#line 19
  testRunner.Given("I Load workflow \"MergeHelloWorld\" from \"localhost\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 20
  testRunner.And("I Load workflow \"VersionHelloWorld\" from \"Remote Connection Integration\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 21
  testRunner.When("Merge Window is opened with remote \"VersionHelloWorld\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 22
  testRunner.Then("Current workflow contains \"8\" tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 23
  testRunner.And("Different workflow contains \"8\" tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 24
  testRunner.And("Merge conflicts count is \"8\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 25
  testRunner.And("Merge variable conflicts is false", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 26
  testRunner.And("Merge window has \"2\" Conflicting tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Merge WorkFlowWithOneScalar different input mapping")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "MergeExecution")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("WorkflowMerging")]
        public virtual void MergeWorkFlowWithOneScalarDifferentInputMapping()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Merge WorkFlowWithOneScalar different input mapping", ((string[])(null)));
#line 28
this.ScenarioSetup(scenarioInfo);
#line 29
  testRunner.Given("I Load workflow \"WorkFlowWithOneScalar\" from \"localhost\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 30
  testRunner.And("I Load workflow version \"1\" of \"WorkFlowWithOneScalar\" from \"localhost\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 31
  testRunner.When("Merge Window is opened with local \"WorkFlowWithOneScalar\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 32
  testRunner.Then("Current workflow contains \"1\" tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 33
  testRunner.And("Different workflow contains \"1\" tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
  testRunner.And("Merge conflicts count is \"1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 35
  testRunner.And("Merge variable conflicts is true", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 36
  testRunner.And("Merge window has \"1\" Conflicting tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Merge WorkFlowWithOneRecordSet different input mapping")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "MergeExecution")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("WorkflowMerging")]
        public virtual void MergeWorkFlowWithOneRecordSetDifferentInputMapping()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Merge WorkFlowWithOneRecordSet different input mapping", ((string[])(null)));
#line 38
this.ScenarioSetup(scenarioInfo);
#line 39
  testRunner.Given("I Load workflow \"WorkFlowWithOneRecordSet\" from \"localhost\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 40
  testRunner.And("I Load workflow version \"1\" of \"WorkFlowWithOneRecordSet\" from \"localhost\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 41
  testRunner.When("Merge Window is opened with local \"WorkFlowWithOneRecordSet\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 42
  testRunner.Then("Current workflow contains \"1\" tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 43
  testRunner.And("Different workflow contains \"1\" tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 44
  testRunner.And("Merge conflicts count is \"1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 45
  testRunner.And("Merge variable conflicts is true", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 46
  testRunner.And("Merge window has \"1\" Conflicting tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Merge WorkFlowWithOneObject different input mapping")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "MergeExecution")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("WorkflowMerging")]
        public virtual void MergeWorkFlowWithOneObjectDifferentInputMapping()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Merge WorkFlowWithOneObject different input mapping", ((string[])(null)));
#line 48
this.ScenarioSetup(scenarioInfo);
#line 49
  testRunner.Given("I Load workflow \"WorkFlowWithOneObject\" from \"localhost\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 50
  testRunner.And("I Load workflow version \"1\" of \"WorkFlowWithOneObject\" from \"localhost\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 51
  testRunner.When("Merge Window is opened with local \"WorkFlowWithOneObject\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 52
  testRunner.Then("Current workflow contains \"1\" tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 53
  testRunner.And("Different workflow contains \"1\" tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 54
  testRunner.And("Merge conflicts count is \"1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 55
  testRunner.And("Merge variable conflicts is true", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 56
  testRunner.And("Merge window has \"1\" Conflicting tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Merge Workflow with Assign tool As First Tool And Split tool as Second tool count" +
            "")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "MergeExecution")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("WorkflowMerging")]
        public virtual void MergeWorkflowWithAssignToolAsFirstToolAndSplitToolAsSecondToolCount()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Merge Workflow with Assign tool As First Tool And Split tool as Second tool count" +
                    "", ((string[])(null)));
#line 58
this.ScenarioSetup(scenarioInfo);
#line 59
  testRunner.Given("I Load workflow \"WorkflowWithDifferentToolSequence\" from \"localhost\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 60
  testRunner.And("I Load workflow \"WorkflowWithDifferentToolSequence\" from \"Remote Connection Integ" +
                    "ration\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 61
  testRunner.When("Merge Window is opened with remote \"WorkflowWithDifferentToolSequence\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 62
  testRunner.Then("Current workflow contains \"3\" tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 63
  testRunner.And("Different workflow contains \"3\" tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 64
  testRunner.And("Merge conflicts count is \"3\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 65
  testRunner.And("Merge variable conflicts is false", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Merge Workflow Containing SequenceTool With Different Children Counts Equals One")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "MergeExecution")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("WorkflowMerging")]
        public virtual void MergeWorkflowContainingSequenceToolWithDifferentChildrenCountsEqualsOne()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Merge Workflow Containing SequenceTool With Different Children Counts Equals One", ((string[])(null)));
#line 67
this.ScenarioSetup(scenarioInfo);
#line 68
  testRunner.Given("I Load workflow \"WorkflowWithSequenceToolWithDifferentChildren\" from \"localhost\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 69
  testRunner.And("I Load workflow version \"1\" of \"WorkflowWithSequenceToolWithDifferentChildren\" fr" +
                    "om \"localhost\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 70
  testRunner.When("Merge Window is opened with local \"WorkflowWithSequenceToolWithDifferentChildren\"" +
                    "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 71
  testRunner.Then("Current workflow contains \"1\" tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 72
  testRunner.And("Different workflow contains \"1\" tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 73
  testRunner.And("Merge conflicts count is \"1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 74
  testRunner.And("Merge variable conflicts is false", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Merge Workflow Containing SequenceTool With Different Children Sequence")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "MergeExecution")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("WorkflowMerging")]
        public virtual void MergeWorkflowContainingSequenceToolWithDifferentChildrenSequence()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Merge Workflow Containing SequenceTool With Different Children Sequence", ((string[])(null)));
#line 76
this.ScenarioSetup(scenarioInfo);
#line 77
  testRunner.Given("I Load workflow \"WorkflowWithSequenceToolWithChildrenInDifferentOrder\" from \"loca" +
                    "lhost\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 78
  testRunner.And("I Load workflow version \"1\" of \"WorkflowWithSequenceToolWithChildrenInDifferentOr" +
                    "der\" from \"localhost\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 79
  testRunner.When("Merge Window is opened with local \"WorkflowWithSequenceToolWithChildrenInDifferen" +
                    "tOrder\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 80
  testRunner.Then("Current workflow contains \"1\" tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 81
  testRunner.And("Different workflow contains \"1\" tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 82
  testRunner.And("Merge conflicts count is \"1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 83
  testRunner.And("Merge variable conflicts is false", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Merge Workflow Containing Same tools But disconnected Arms")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "MergeExecution")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("WorkflowMerging")]
        public virtual void MergeWorkflowContainingSameToolsButDisconnectedArms()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Merge Workflow Containing Same tools But disconnected Arms", ((string[])(null)));
#line 85
this.ScenarioSetup(scenarioInfo);
#line 86
  testRunner.Given("I Load workflow \"WorkflowWithAssignToolsWithDisconnectedArms\" from \"localhost\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 87
  testRunner.And("I Load workflow \"WorkflowWithAssignToolsWithDisconnectedArms\" from \"Remote Connec" +
                    "tion Integration\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 88
  testRunner.When("Merge Window is opened with remote \"WorkflowWithAssignToolsWithDisconnectedArms\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 89
  testRunner.Then("Current workflow contains \"1\" tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 90
  testRunner.And("Different workflow contains \"1\" tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 91
  testRunner.And("Merge conflicts count is \"1\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 92
  testRunner.And("Merge variable conflicts is false", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Merge Workflow Containing Removed tool with same Variable List")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "MergeExecution")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("WorkflowMerging")]
        public virtual void MergeWorkflowContainingRemovedToolWithSameVariableList()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Merge Workflow Containing Removed tool with same Variable List", ((string[])(null)));
#line 94
this.ScenarioSetup(scenarioInfo);
#line 95
  testRunner.Given("I Load workflow \"MergeRemovedTool\" from \"localhost\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 96
  testRunner.And("I Load workflow version \"1\" of \"MergeRemovedTool\" from \"localhost\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 97
  testRunner.When("Merge Window is opened with local \"MergeRemovedTool\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 98
  testRunner.Then("Current workflow contains \"5\" tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 99
  testRunner.And("Different workflow contains \"5\" tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 100
  testRunner.And("Merge conflicts count is \"5\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 101
  testRunner.And("Merge variable conflicts is false", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 102
  testRunner.And("I select Current Tool", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 103
  testRunner.And("I select Current Arm", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 104
  testRunner.And("I select Current Arm", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 105
  testRunner.Then("Save is enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute()]
        [Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute("Merge Workflow Containing Switch tool")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestPropertyAttribute("FeatureTitle", "MergeExecution")]
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryAttribute("WorkflowMerging")]
        public virtual void MergeWorkflowContainingSwitchTool()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Merge Workflow Containing Switch tool", ((string[])(null)));
#line 107
this.ScenarioSetup(scenarioInfo);
#line 108
  testRunner.Given("I Load workflow \"MergeSwitchTool\" from \"localhost\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 109
  testRunner.And("I Load workflow version \"1\" of \"MergeSwitchTool\" from \"localhost\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 110
  testRunner.When("Merge Window is opened with local \"MergeSwitchTool\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 111
  testRunner.Then("Current workflow contains \"5\" tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 112
  testRunner.And("Different workflow contains \"5\" tools", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 113
  testRunner.And("Merge conflicts count is \"5\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 114
  testRunner.And("Merge variable conflicts is false", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 115
  testRunner.And("I select Current Tool", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 116
  testRunner.And("I select Current Arm", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 117
  testRunner.And("I select Current Tool", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 118
  testRunner.And("I select Current Arm", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 119
  testRunner.Then("Save is enabled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
