using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace ccnet.campfire.plugin.tests.disconnected
{
    [TestFixture]
    public class CampfireTranscriptReaderTests
    {
        [Test]
        public void can_read_empty_transcript()
        {
            const string transcript = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                                      "<messages type=\"array\"/>";


            var result = new CampfireTranscriptReader().Process(transcript);
            Assert.That(result.ToArray(), Is.EqualTo(new string[] { }));
        }

        [Test]
        public void can_read_transcript_with_one_message()
        {
            const string transcript = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"+
                                      "<messages type=\"array\">"+
                                      "  <message>" +
                                      "    <id type=\"integer\">1</id>" +
                                      "    <body>Hello</body>" +
                                      "    <room-id type=\"integer\">1</room-id>" +
                                      "    <user-id type=\"integer\">2</user-id>" +
                                      "    <created-at type=\"datetime\">2009-11-22T19:11:41Z</created-at>" +
                                      "    <type>TextMessage</type>" +
                                      "  </message>"+
                                      "</messages>";
            var result = new CampfireTranscriptReader().Process(transcript);
            Assert.That(result.ToArray(), Is.EqualTo(new[] { "Hello" }));
        }

        [Test]
        public void can_read_transcript_with_two_messages()
        {
            const string transcript = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                                      "<messages type=\"array\">" +
                                      "  <message>" +
                                      "    <id type=\"integer\">1</id>" +
                                      "    <body>Hello</body>" +
                                      "    <room-id type=\"integer\">1</room-id>" +
                                      "    <user-id type=\"integer\">2</user-id>" +
                                      "    <created-at type=\"datetime\">2009-11-22T19:11:41Z</created-at>" +
                                      "    <type>TextMessage</type>" +
                                      "  </message>" +
                                      "  <message>" +
                                      "    <id type=\"integer\">1</id>" +
                                      "    <body>goodbye</body>" +
                                      "    <room-id type=\"integer\">1</room-id>" +
                                      "    <user-id type=\"integer\">2</user-id>" +
                                      "    <created-at type=\"datetime\">2009-11-22T19:11:41Z</created-at>" +
                                      "    <type>TextMessage</type>" +
                                      "  </message>"+
                                      "</messages>";
            var result = new CampfireTranscriptReader().Process(transcript);
            Assert.That(result.ToArray(), Is.EqualTo(new[]{"Hello","goodbye"}));
        }

        private static void StreamFor(string theString, Action<Stream> action)
        {
            using(var ms = new MemoryStream())
            using (var sw = new StreamWriter(ms))
            {
                sw.Write(theString);
                sw.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                action(ms);
            }
        }
    }
}

//  <message>
//    <id type="integer">1</id>
//    <body>Hello</body>
//    <room-id type="integer">1</room-id>
//    <user-id type="integer">2</user-id>
//    <created-at type="datetime">2009-11-22T19:11:41Z</created-at>
//    <type>TextMessage</type>
//  </message>
//  ...
//</messages>