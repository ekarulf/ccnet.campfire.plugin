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
                var room = new CampfireRoom("ccnetcampfireplugin",
                                            "4c7838c235627478c86d2c6b6ed60b512cdb8303",
                                            265935);
                room.Post(new IntegrationMessage(result).ToString());
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}