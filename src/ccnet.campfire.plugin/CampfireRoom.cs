using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

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
            var request = (HttpWebRequest)WebRequest.Create(string.Format("http://{0}.campfirenow.com/room/{1}/{2}.xml" , accountName, roomId, action));
            request.Method = "POST";
            request.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", EncodedAuthToken);
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
            using (var sw = new StreamWriter(request.GetRequestStream()))
            {
                sw.Write("<message><type>TextMessage</type><body>" + message + "</body></message>");
            }
            request.GetResponse();
        }

        public IEnumerable<string> Transcript
        {
            get { yield break; }
        }

        public void Leave()
        {
            throw new NotImplementedException();
        }
    }
}