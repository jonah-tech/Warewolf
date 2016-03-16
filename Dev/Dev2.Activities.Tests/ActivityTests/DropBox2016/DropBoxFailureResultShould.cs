﻿using System;
using System.Diagnostics.CodeAnalysis;
using Dev2.Activities.DropBox2016;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev2.Tests.Activities.ActivityTests.DropBox2016
{
    [TestClass]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class DropBoxFailureResultShould
    {
        [TestMethod]
        [Owner("Nkosinathi Sangweni")]
        public void ConstructDropBoxFailureResult_GivenException_ShouldRetunNewFailureResult()
        {
            //---------------Set up test pack-------------------
            var failureResult = new DropboxFailureResult(new Exception("Message"));
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            Assert.IsNotNull(failureResult);
        }

        [TestMethod]
        [Owner("Nkosinathi Sangweni")]
        public void failureResult_GivenException_ShouldRetunNewFailureResult()
        {
            //---------------Set up test pack-------------------
            var dpExc = new Exception("Message");
            var failureResult = new DropboxFailureResult(dpExc);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var exception = failureResult.GetException();
            //---------------Test Result -----------------------
            Assert.AreEqual(exception, dpExc);
        }
    }
}
