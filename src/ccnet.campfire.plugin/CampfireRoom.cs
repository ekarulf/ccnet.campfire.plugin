using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace ccnet.campfire.plugin
{
    public class CampfireRoom
    {
        private readonly string accountName;
        private readonly string authToken;
        private readonly int roomId;

        public CampfireRoom(string accountName, string authToken, int roomId)
        {
            this.accountName = accountName;
            this.authToken = authToken;
            this.roomId = roomId;
        }

        public void Join()
        {
            PostTo("join").GetResponse();
        }

        private HttpWebRequest PostTo(string action)
        {
            var request = (HttpWebRequest) WebRequest.Create(string.Format("http://{0}.campfirenow.com/room/{1}/{2}.xml", accountName, roomId, action));
            request.Method = "POST";
            request.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", EncodedAuthToken);
            request.ContentType = "application/xml";
            request.Accept = "application/xml";
            return request;
        }

        private string EncodedAuthToken
        {
            get
            {
                byte[] bytesToEncode = Encoding.ASCII.GetBytes(authToken + ":X");
                return Convert.ToBase64String(bytesToEncode);
            }
        }

        public void Post(string message)
        {
            var request = PostTo("speak");
            using (var sw = new XmlTextWriter(request.GetRequestStream(), Encoding.UTF8))
            {
                sw.WriteStartDocument();
                sw.WriteStartElement("message");
                sw.WriteElementString("type", "TextMessage");
                sw.WriteElementString("body", message);
                sw.WriteEndElement();
                sw.WriteEndDocument();
            }
            request.GetResponse();
        }

        public IEnumerable<string> Transcript
        {
            get
            {
                var webResponse = GetFrom("transcript").GetResponse();
                using (var stream = webResponse.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    return new CampfireTranscriptReader().Process(reader.ReadToEnd());
                }
            }
        }

        private HttpWebRequest GetFrom(string action)
        {
            var request = (HttpWebRequest) WebRequest.Create(string.Format("http://{0}.campfirenow.com/room/{1}/{2}.xml", accountName, roomId, action));
            request.Method = "GET";
            request.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", EncodedAuthToken);
            request.Accept = "application/xml";
            return request;
        }

        public void Leave()
        {
            PostTo("leave").GetResponse();
        }
    }
}