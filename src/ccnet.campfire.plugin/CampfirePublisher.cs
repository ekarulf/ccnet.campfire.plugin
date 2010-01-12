using System;
using Exortech.NetReflector;
using ThoughtWorks.CruiseControl.Core;
using ThoughtWorks.CruiseControl.Core.Util;

namespace ccnet.campfire.plugin
{
    [ReflectorType("campfire")]
    public class CampfirePublisher : ITask
    {
        public void Run(IIntegrationResult result)
        {
            try
            {
                var room = new CampfireRoom(AccountName, AuthToken, RoomId);
                room.Post(new IntegrationMessage(result).ToString());
            }
            catch (Exception e)
            {
                Log.Warning("[campfirepublisher] Error publishing to Campfire: " + e.Message);
            }
        }

        [ReflectorProperty("account-name")]
        public string AccountName { get; set; }

        [ReflectorProperty("auth-token")]
        public string AuthToken { get; set; }

        [ReflectorProperty("room-id")]
        public int RoomId { get; set; }
    }
}