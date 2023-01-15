using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using VKBotExtensions;

namespace dotVK
{
    public class VK
    {
        internal string ACCESS = "";
        internal long GROUP_ID = 0;
        internal string Server_URL = "";
        internal string Key = "temp";
        public long ts;

        /// <summary>
        /// Creates a VK bot class
        /// </summary>
        /// <param name="accessToken">Access token to send messages</param>
        /// <param name="GroupID">Group ID to host the bot in</param>
        public VK(string accessToken, long GroupID)
        {
            this.ACCESS = accessToken;
            this.GROUP_ID = GroupID;
        }

        public void EnqueueUpdates(int interval)
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;

                while (true)
                {
                    Thread.Sleep(interval);
                    UpdateLongPoll();
                }
            }).Start();
        }

        public void UpdateLongPoll()
        {
            try
            {
                using (var w = new WebClient())
                {
                    var a = new NameValueCollection();
                    a.Add("access_token", ACCESS);
                    a.Add("v", "5.125");
                    a.Add("group_id", GROUP_ID.ToString());

                    string query = Utils.ToQueryString(a);

                    string answ = w.DownloadString("https://api.vk.com/method/groups.getLongPollServer" + query);
                    UpdateServer u = JsonConvert.DeserializeObject<UpdateServer>(answ);
                    Key = u.Response.Key;
                    Server_URL = u.Response.Server;
                    ts = long.Parse(u.Response.Ts);
                }
            }
            catch
            {

            }
        }

        public Root ReceiveMessage()
        {
            using (HttpClient http = new HttpClient())
            {
                var jsonTask = http.GetStringAsync(Server_URL + "?act=a_check&key=" + Key + "&ts=" + ts + "&wait=10&mode=2&version=2");
                jsonTask.Wait();
                var result = jsonTask.Result;
                Console.WriteLine(result);
                if (result.Contains("failed"))
                {
                    try
                    {
                        FailedRoot r = JsonConvert.DeserializeObject<FailedRoot>(result);
                        if (r.failed == 1)
                        {
                            ts = r.ts;
                        }
                    }
                    catch
                    {

                    }
                }
                try
                {
                    Root root = JsonConvert.DeserializeObject<Root>(result);
                    ts = long.Parse(root.Ts);
                    return root;
                }
                catch
                {

                }
                var a = new Root() { Ts = "NO" };
                return a;
            }
        }
    }
}
