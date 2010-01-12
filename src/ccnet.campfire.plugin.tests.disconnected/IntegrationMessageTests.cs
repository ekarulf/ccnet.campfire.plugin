using System;
using System.Collections;
using NUnit.Framework;
using ThoughtWorks.CruiseControl.Core;
using ThoughtWorks.CruiseControl.Core.Util;
using ThoughtWorks.CruiseControl.Remote;

namespace ccnet.campfire.plugin.tests.disconnected
{
    [TestFixture]
    public class IntegrationMessageTests
    {
        [Test]
        public void should_format_message_nicely()
        {
            var buildResult = new FakeIntegrationResult {ProjectName = "some name", Label = "ccnet-dev-1235", Status = IntegrationStatus.Success};
            var message = new IntegrationMessage(buildResult);
            Assert.That(message.ToString(), Is.EqualTo("[ccnet] Project 'some name' build complete. Result: Success. Label: ccnet-dev-1235."));
        }
    }

    public class FakeIntegrationResult : IIntegrationResult
    {
        public string BaseFromArtifactsDirectory(string pathToBase)
        {
            throw new NotImplementedException();
        }

        public string BaseFromWorkingDirectory(string pathToBase)
        {
            throw new NotImplementedException();
        }

        public void MarkStartTime()
        {
            throw new NotImplementedException();
        }

        public void MarkEndTime()
        {
            throw new NotImplementedException();
        }

        public bool IsInitial()
        {
            throw new NotImplementedException();
        }

        public void AddTaskResult(string result)
        {
            throw new NotImplementedException();
        }

        public void AddTaskResult(ITaskResult result)
        {
            throw new NotImplementedException();
        }

        public bool HasModifications()
        {
            throw new NotImplementedException();
        }

        public bool ShouldRunBuild()
        {
            throw new NotImplementedException();
        }

        public string ProjectName { get; set; }
        public string ProjectUrl { get; set; }
        public string WorkingDirectory { get; set; }
        public string ArtifactDirectory { get; set; }
        public string BuildLogDirectory { get; set; }
        public BuildCondition BuildCondition { get; set; }
        public string Label { get; set; }
        public IntegrationStatus Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan TotalIntegrationTime { get; set; }
        public bool Failed { get; set; }
        public bool Fixed { get; set; }
        public bool Succeeded { get; set; }
        public IntegrationRequest IntegrationRequest { get; set; }
        public IntegrationStatus LastIntegrationStatus { get; set; }
        public ArrayList FailureUsers { get; set; }
        public DateTime LastModificationDate { get; set; }
        public int LastChangeNumber { get; set; }
        public IntegrationSummary LastIntegration { get; set; }
        public string LastSuccessfulIntegrationLabel { get; set; }
        public IList TaskResults { get; set; }
        public Modification[] Modifications { get; set; }
        public Exception ExceptionResult { get; set; }
        public string TaskOutput { get; set; }
        public Exception SourceControlError { get; set; }
        public IDictionary IntegrationProperties { get; set; }
        public BuildProgressInformation BuildProgressInformation { get; set; }
    }
}