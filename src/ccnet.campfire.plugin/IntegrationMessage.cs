using ThoughtWorks.CruiseControl.Core;

namespace ccnet.campfire.plugin
{
    public class IntegrationMessage
    {
        private readonly IIntegrationResult result;

        public IntegrationMessage(IIntegrationResult result)
        {
            this.result = result;
        }

        public override string ToString()
        {
            return string.Format("[ccnet] Project '{0}' build complete. Result: {1}. Label: {2}.", result.ProjectName, result.Status, result.Label);
        }
    }
}