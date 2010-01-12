using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace ccnet.campfire.plugin
{
    public class CampfireTranscriptReader
    {
        public IEnumerable<string> Process(string transcriptXml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(transcriptXml);
            foreach (var node in doc.SelectNodes("messages/message/body").Cast<XmlElement>())
                yield return node.InnerText;
        }
    }
}