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
            return result.Status.ToString();
        }
    }
}