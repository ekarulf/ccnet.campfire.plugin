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
        private readonly bool isHttps;

        public CampfireRoom(string accountName, string authToken, int roomId, bool isHttps)
        {
            this.accountName = accountName;
            this.authToken = authToken;
            this.roomId = roomId;
            this.isHttps = isHttps;
        }

        public void Join()
        {
            PostTo("join");
        }

        public void Post(string message)
        {
            PostTo("speak",
                   req =>
                   {
                       using (var sw = new XmlTextWriter(req.GetRequestStream(), Encoding.UTF8))
                       {
                           sw.WriteStartDocument();
                           sw.WriteStartElement("message");
                           sw.WriteElementString("type", "TextMessage");
                           sw.WriteElementString("body", message);
                           sw.WriteEndElement();
                           sw.WriteEndDocument();
                       }
                   });
        }

        public IEnumerable<string> Transcript
        {
            get
            {
                var webResponse = GetFrom("transcript");
                using (var stream = webResponse.GetResponseStream())
                using (var reader = new StreamReader(stream))
                    return new CampfireTranscriptReader().Process(reader.ReadToEnd());
            }
        }

        public void Leave()
        {
            PostTo("leave");
        }

        private void PostTo(string action)
        {
            PostTo(action, req => { });
        }

        private WebResponse GetFrom(string action)
        {
            var request = RequestTo(action, "GET");
            return request.GetResponse();
        }

        private void PostTo(string action, Action<HttpWebRequest> requestInfo)
        {
            var request = RequestTo(action, "POST");
            request.ContentType = "application/xml";
            requestInfo(request);
            request.GetResponse();
        }

        private HttpWebRequest RequestTo(string action, string method)
        {
            var request = (HttpWebRequest) WebRequest.Create(string.Format("http{3}://{0}.campfirenow.com/room/{1}/{2}.xml", accountName, roomId, action, isHttps ? "s" : ""));
            request.Method = method;
            request.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", EncodedAuthToken);
            request.Accept = "application/xml";
            return request;
        }

        private string EncodedAuthToken
        {
            get
            {
                var bytesToEncode = Encoding.ASCII.GetBytes(authToken + ":X");
                return Convert.ToBase64String(bytesToEncode);
            }
        }
    }
}