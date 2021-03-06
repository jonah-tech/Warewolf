#pragma warning disable
/*
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
using System.Globalization;
using System.Linq;
using Dev2.Activities;
using Dev2.Activities.Debug;
using Dev2.Common;
using Dev2.Common.DateAndTime;
using Dev2.Common.Interfaces.Core.Convertors.DateAndTime;
using Dev2.Common.Interfaces.Diagnostics.Debug;
using Dev2.Common.State;
using Dev2.Data;
using Dev2.Data.Interfaces.Enums;
using Dev2.Data.TO;
using Dev2.Data.Util;
using Dev2.Diagnostics;
using Dev2.Interfaces;
using Dev2.Util;
using Dev2.Validation;
using Unlimited.Applications.BusinessDesignStudio.Activities.Utilities;
using Warewolf.Storage;
using Warewolf.Storage.Interfaces;


namespace Unlimited.Applications.BusinessDesignStudio.Activities

{
    public class DsfDateTimeDifferenceActivity : DsfActivityAbstract<string>, IDateTimeDiffTO
    {
#pragma warning disable S3776,S1541,S134,CC0075,S1066,S1067

        #region Properties

        /// <summary>
        /// The property that holds the date time string the user enters into the "Input1" box
        /// </summary>
        [Inputs("Input1")]
        [FindMissing]
        public string Input1 { get; set; }

        /// <summary>
        /// The property that holds the input format string the user enters into the "Input2" box
        /// </summary>
        [Inputs("Input2")]
        [FindMissing]
        public string Input2 { get; set; }

        /// <summary>
        /// The property that holds the output format string the user enters into the "Input Format" box
        /// </summary>
        [Inputs("InputFormat")]
        [FindMissing]
        public string InputFormat { get; set; }

        /// <summary>
        /// The property that holds the time modifier string the user selects in the "Output In" combobox
        /// </summary>
        [Inputs("OutputType")]
        public string OutputType { get; set; }

        /// <summary>
        /// The property that holds the result string the user enters into the "Result" box
        /// </summary>
        [Outputs("Result")]
        [FindMissing]
        public new string Result { get; set; }

        #endregion Properties

        #region Ctor

        /// <summary>
        /// The consructor for the activity 
        /// </summary>
        public DsfDateTimeDifferenceActivity()
            : base("Date and Time Difference")
        {
            Input1 = string.Empty;
            Input2 = string.Empty;
            InputFormat = string.Empty;
            OutputType = "Years";
            Result = string.Empty;
        }

        #endregion Ctor



        public override List<string> GetOutputs() => new List<string> { Result };

        /// <summary>
        /// The execute method that is called when the activity is executed at run time and will hold all the logic of the activity
        /// </summary>       
        protected override void OnExecute(NativeActivityContext context)
        {
            var dataObject = context.GetExtension<IDSFDataObject>();
            ExecuteTool(dataObject, 0);
        }

        protected override void ExecuteTool(IDSFDataObject dataObject, int update)
        {
            var allErrors = new ErrorResultTO();
            var errors = new ErrorResultTO();
            allErrors.MergeErrors(errors);
            InitializeDebug(dataObject);
            // Process if no errors
            try
            {
                TryExecute(dataObject, update, allErrors, errors);
            }
            catch (Exception e)
            {
                Dev2Logger.Error("DSFDateTime", e, GlobalConstants.WarewolfError);
                allErrors.AddError(e.Message);
            }
            finally
            {
                // Handle Errors
                if (allErrors.HasErrors())
                {
                    DisplayAndWriteError("DsfDateTimeDifferenceActivity", allErrors);
                    var errorString = allErrors.MakeDisplayReady();
                    dataObject.Environment.AddError(errorString);
                    dataObject.Environment.Assign(Result, null, update);
                }
                if (dataObject.IsDebugMode())
                {
                    DispatchDebugState(dataObject, StateType.Before, update);
                    DispatchDebugState(dataObject, StateType.After, update);
                }
            }
        }

        private void TryExecute(IDSFDataObject dataObject, int update, ErrorResultTO allErrors, ErrorResultTO errors)
        {
            if (dataObject.IsDebugMode())
            {
                if (string.IsNullOrEmpty(Input1))
                {
                    AddDebugInputItem(new DebugItemStaticDataParams(DateTime.Now.ToString(GlobalConstants.PreviousDev2DotNetDefaultDateTimeFormat), "now()", "Input 1", "="));
                }
                else
                {
                    AddDebugInputItem(Input1, "Input 1", dataObject.Environment, update);
                }

                if (string.IsNullOrEmpty(Input2))
                {
                    AddDebugInputItem(new DebugItemStaticDataParams(DateTime.Now.ToString(GlobalConstants.PreviousDev2DotNetDefaultDateTimeFormat), "now()", "Input 2", "="));
                }
                else
                {
                    AddDebugInputItem(Input2, "Input 2", dataObject.Environment, update);
                }

                AddDebugInputItem(InputFormat, "Input Format", dataObject.Environment, update);
                if (!String.IsNullOrEmpty(OutputType))
                {
                    AddDebugInputItem(new DebugItemStaticDataParams(OutputType, "Output In"));
                }
            }
            var colItr = new WarewolfListIterator();

            var input1Itr = new WarewolfIterator(dataObject.Environment.EvalStrict(string.IsNullOrEmpty(Input1) ? GlobalConstants.CalcExpressionNow : Input1, update));
            colItr.AddVariableToIterateOn(input1Itr);

            var evalInp2 = dataObject.Environment.EvalStrict(string.IsNullOrEmpty(Input2) ? GlobalConstants.CalcExpressionNow : Input2, update);

            var input2Itr = new WarewolfIterator(evalInp2);
            colItr.AddVariableToIterateOn(input2Itr);

            var ifItr = new WarewolfIterator(dataObject.Environment.Eval(InputFormat ?? string.Empty, update));
            colItr.AddVariableToIterateOn(ifItr);
            var indexToUpsertTo = 1;
            while (colItr.HasMoreData())
            {
                var transObj = ConvertToDateTimeDiffTo(colItr.FetchNextValue(input1Itr),
                    colItr.FetchNextValue(input2Itr),
                    colItr.FetchNextValue(ifItr),
                    OutputType);
                //Create a DateTimeComparer using the DateTimeConverterFactory
                var comparer = DateTimeConverterFactory.CreateComparer();
                var expression = Result;

                if (comparer.TryCompare(transObj, out string result, out string error))
                {
                    expression = GetExpression(update, indexToUpsertTo, expression);

                    var rule = new IsSingleValueRule(() => Result);
                    var single = rule.Check();
                    if (single != null)
                    {
                        allErrors.AddError(single.Message);
                    }
                    else
                    {
                        dataObject.Environment.Assign(expression, result, update);
                    }
                }
                else
                {
                    DoDebugOutput(dataObject, expression, update);
                    allErrors.AddError(error);
                }
                indexToUpsertTo++;
            }

            allErrors.MergeErrors(errors);
            if (dataObject.IsDebugMode() && !allErrors.HasErrors())
            {
                AddDebugOutputItem(new DebugEvalResult(Result, null, dataObject.Environment, update));
            }
        }

        private string GetExpression(int update, int indexToUpsertTo, string expression)
        {
            if (DataListUtil.IsValueRecordset(Result) &&
                                   DataListUtil.GetRecordsetIndexType(Result) == enRecordsetIndexType.Star)
            {
                if (update == 0)
                {
                    expression = Result.Replace(GlobalConstants.StarExpression, indexToUpsertTo.ToString(CultureInfo.InvariantCulture));
                }
            }
            else
            {
                expression = Result;
            }

            return expression;
        }

        void DoDebugOutput(IDSFDataObject dataObject, string region, int update)
        {
            if (dataObject.IsDebugMode())
            {
                AddDebugOutputItem(new DebugEvalResult(region, "", dataObject.Environment, update));
            }
        }

        #region Private Methods

        void AddDebugInputItem(string expression, string labelText, IExecutionEnvironment environment, int update)
        {
            AddDebugInputItem(new DebugEvalResult(expression, labelText, environment, update));
        }

        /// <summary>
        /// Used for converting the properties of this activity to a DateTimeTO object
        /// </summary>
        /// <param name="input1">The input1.</param>
        /// <param name="input2">The input2.</param>
        /// <param name="evaledInputFormat">The evaled input format.</param>
        /// <param name="outputType">Type of the output.</param>
        /// <returns></returns>
        static IDateTimeDiffTO ConvertToDateTimeDiffTo(string input1, string input2, string evaledInputFormat, string outputType) => DateTimeConverterFactory.CreateDateTimeDiffTO(input1, input2, evaledInputFormat, outputType);

        #endregion Private Methods

        #region Get Debug Inputs/Outputs

        public override List<DebugItem> GetDebugInputs(IExecutionEnvironment env, int update)
        {
            foreach (IDebugItem debugInput in _debugInputs)
            {
                debugInput.FlushStringBuilder();
            }
            return _debugInputs;
        }

        public override List<DebugItem> GetDebugOutputs(IExecutionEnvironment env, int update)
        {
            foreach (IDebugItem debugOutput in _debugOutputs)
            {
                debugOutput.FlushStringBuilder();
            }
            return _debugOutputs;
        }

        #endregion Get Inputs/Outputs

        #region Get ForEach Inputs/Outputs

        public override void UpdateForEachInputs(IList<Tuple<string, string>> updates)
        {
            foreach (Tuple<string, string> t in updates)
            {

                if (t.Item1 == Input1)
                {
                    Input1 = t.Item2;
                }

                if (t.Item1 == Input2)
                {
                    Input2 = t.Item2;
                }

                if (t.Item1 == InputFormat)
                {
                    InputFormat = t.Item2;
                }

            }
        }

        public override void UpdateForEachOutputs(IList<Tuple<string, string>> updates)
        {
            var itemUpdate = updates?.FirstOrDefault(tuple => tuple.Item1 == Result);
            if (itemUpdate != null)
            {
                Result = itemUpdate.Item2;
            }
        }

        #endregion

        public override IList<DsfForEachItem> GetForEachInputs() => GetForEachItems(Input1, Input2, InputFormat);

        public override IList<DsfForEachItem> GetForEachOutputs() => GetForEachItems(Result);

        public bool Equals(DsfDateTimeDifferenceActivity other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return base.Equals(other)
                && string.Equals(Input1, other.Input1)
                && string.Equals(Input2, other.Input2)
                && string.Equals(InputFormat, other.InputFormat)
                && string.Equals(OutputType, other.OutputType)
                && string.Equals(Result, other.Result);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Equals((DsfDateTimeDifferenceActivity)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ (Input1 != null ? Input1.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Input2 != null ? Input2.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (InputFormat != null ? InputFormat.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (OutputType != null ? OutputType.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Result != null ? Result.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override IEnumerable<StateVariable> GetState()
        {
            return new[]
            {
                new StateVariable
                {
                    Name = "Input1",
                    Type = StateVariable.StateType.Input,
                    Value = Input1
                },
                new StateVariable
                {
                    Name = "Input2",
                    Type = StateVariable.StateType.Input,
                    Value = Input2
                },
                new StateVariable
                {
                    Name="InputFormat",
                    Type = StateVariable.StateType.Input,
                    Value = InputFormat
                },
                new StateVariable
                {
                    Name="OutputType",
                    Type = StateVariable.StateType.Input,
                    Value = OutputType
                },
                new StateVariable
                {
                    Name="Result",
                    Type = StateVariable.StateType.Output,
                    Value = Result
                }
            };
        }
#pragma warning restore S3776, S1541, S134, CC0075, S1066, S1067
    }
}
