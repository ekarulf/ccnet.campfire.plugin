using System.Linq;
using NUnit.Framework;

namespace ccnet.campfire.plugin.tests.connected
{
    [TestFixture]
    public class PostMessages
    {
        [Test]
        public void can_post_a_message_then_retrieve_it()
        {
            var room = new CampfireRoom("ccnetcampfireplugin",
                                        "4c7838c235627478c86d2c6b6ed60b512cdb8303",
                                        265935);

            room.Join();

            room.Post("A message from your build server");

            Assert.That(room.Transcript.Last(), Is.EqualTo("A message from your build server"));

            room.Leave();
        }
    }
}