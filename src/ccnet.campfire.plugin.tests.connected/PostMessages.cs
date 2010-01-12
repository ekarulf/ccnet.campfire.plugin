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
             Assert.Fail("Requires you to fill in your real campfire account info to work.");
            var room = new CampfireRoom("the first part of the URL",
                                        "your API key",
                                        -1);

            room.Join();

            room.Post("A message from your build server");

            Assert.That(room.Transcript.Last(), Is.EqualTo("A message from your build server"));

            room.Leave();
        }
    }
}