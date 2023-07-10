using dotVK;
using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

namespace VKBotExtensions
{
    public static class Actions
    {
        public static Random rng = new Random();
        public static List<CachedNames> cache = new List<CachedNames>();

        public static string SendMessage(this VK bot, long peer, string body, string attachment = null, Forward fwd = null, ContentSource cs = null)
        {
            using (var w = new WebClient())
            {
                var a = new NameValueCollection();
                a.Add("access_token", bot.ACCESS);
                a.Add("v", "5.126");
                a.Add("random_id", rng.Next(0, Int32.MaxValue).ToString());
                a.Add("peer_ids", peer.ToString());
                a.Add("message", body);
                if (attachment != null) a.Add("attachment", attachment);
                if (fwd != null) a.Add("forward", JsonConvert.SerializeObject(fwd));
                if (cs != null) a.Add("content_source", JsonConvert.SerializeObject(cs));
                string query = VKUtils.ToQueryString(a);
                string answ = w.DownloadString("https://api.vk.com/method/messages.send" + query);
                return answ;
            }
        }

        public static void DeleteMessages(this VK bot, long msgID)
        {
            using (var w = new WebClient())
            {
                var a = new NameValueCollection();
                a.Add("access_token", bot.ACCESS);
                a.Add("v", "5.126");
                a.Add("message_ids", msgID.ToString());
                a.Add("delete_for_all", "1");
                string query = VKUtils.ToQueryString(a);

                string answ = w.DownloadString("https://api.vk.com/method/messages.delete" + query);
            }
        }

        public static void DeleteMessages(this VK bot, long[] msgID)
        {
            using (var w = new WebClient())
            {
                string ids = "";
                foreach (long l in msgID) { ids += l + ","; } ids = ids.Substring(0, ids.Length - 1);
                var a = new NameValueCollection();
                a.Add("access_token", bot.ACCESS);
                a.Add("v", "5.126");
                a.Add("message_ids", ids);
                a.Add("delete_for_all", "1");
                string query = VKUtils.ToQueryString(a);

                string answ = w.DownloadString("https://api.vk.com/method/messages.delete" + query);
            }
        }

        public static UploadServer GetMessagesUploadServer(this VK bot)
        {
            using (var w = new WebClient())
            {
                var a = new NameValueCollection();
                a.Add("access_token", bot.ACCESS);
                a.Add("v", "5.130");
                a.Add("peer_id", "0");
                string query = VKUtils.ToQueryString(a);

                string answ = w.DownloadString("https://api.vk.com/method/photos.getMessagesUploadServer" + query);
                var b = JsonConvert.DeserializeObject<UploadServerRoot>(answ);
                return b.response;
            }
        }

        public static UploadServer GetDocumentMessagesUploadServer(this VK bot, long peerID)
        {
            using (var w = new WebClient())
            {
                var a = new NameValueCollection();
                a.Add("access_token", bot.ACCESS);
                a.Add("v", "5.131");
                a.Add("peer_id", peerID.ToString());
                a.Add("type", "doc");
                string query = VKUtils.ToQueryString(a);

                string answ = w.DownloadString("https://api.vk.com/method/docs.getMessagesUploadServer" + query);
                var b = JsonConvert.DeserializeObject<UploadServerRoot>(answ);
                return b.response;
            }
        }

        public static UploadServer GetVideoUploadServer(this VK bot, string name)
        {
            using (var w = new WebClient())
            {
                var a = new NameValueCollection();
                a.Add("access_token", bot.ACCESS);
                a.Add("v", "5.130");
                a.Add("peer_id", "0");
                a.Add("name", name);
                string query = VKUtils.ToQueryString(a);

                string answ = w.DownloadString("https://api.vk.com/method/video.save" + query);
                var b = JsonConvert.DeserializeObject<UploadServerRoot>(answ);
                return b.response;
            }
        }

        public static UploadedFile UploadVideo(this VK bot, string url, byte[] video)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                MultipartFormDataContent form = new MultipartFormDataContent();

                form.Add(new ByteArrayContent(video), "video_file", $"upload.mp4");

                var tsk = httpClient.PostAsync(url, form);
                tsk.Wait();
                var rslt = tsk.Result;
                var strtsk = rslt.Content.ReadAsByteArrayAsync();
                strtsk.Wait();
                byte[] result = strtsk.Result;
                string lol = Encoding.UTF8.GetString(result);

                return JsonConvert.DeserializeObject<UploadedFile>(lol);
            }
        }

        public static UploadedFile UploadFile(this VK bot, string url, byte[] file, string ext)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                MultipartFormDataContent form = new MultipartFormDataContent();

                form.Add(new ByteArrayContent(file), "file", $"upload.{ext}");

                var tsk = httpClient.PostAsync(url, form);
                tsk.Wait();
                var rslt = tsk.Result;
                var strtsk = rslt.Content.ReadAsByteArrayAsync();
                strtsk.Wait();
                byte[] result = strtsk.Result;
                string lol = Encoding.UTF8.GetString(result);

                return JsonConvert.DeserializeObject<UploadedFile>(lol);
            }
        }

        public static MessagesFile SaveMessagesFile(this VK bot, string file)
        {
            using (var w = new WebClient())
            {
                var a = new NameValueCollection();
                a.Add("access_token", bot.ACCESS);
                a.Add("v", "5.131");
                a.Add("file", file);
                string query = VKUtils.ToQueryString(a);

                string answ = w.DownloadString("https://api.vk.com/method/docs.save" + query);
                var b = JsonConvert.DeserializeObject<MessagesFileRoot>(answ);
                return b.response;
            }
        }

        public static UploadedPhoto UploadPhoto(this VK bot, string url, SKBitmap bmp)
        {
            byte[] image;
            using (var stream = new MemoryStream())
            {
                var data = SKImage.FromBitmap(bmp).Encode(SKEncodedImageFormat.Png, 100);//bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                data.SaveTo(stream);
                image = stream.ToArray();
            }

            using (HttpClient httpClient = new HttpClient())
            {
                MultipartFormDataContent form = new MultipartFormDataContent();

                form.Add(new ByteArrayContent(image), "photo", "upload.png");

                var tsk = httpClient.PostAsync(url, form);
                tsk.Wait();
                var rslt = tsk.Result;
                var strtsk = rslt.Content.ReadAsByteArrayAsync();
                strtsk.Wait();
                byte[] result = strtsk.Result;
                string lol = Encoding.UTF8.GetString(result);

                return JsonConvert.DeserializeObject<UploadedPhoto>(lol);
            }
        }

        public static UploadedPhoto UploadPhoto(this VK bot, string url, byte[] photo)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                MultipartFormDataContent form = new MultipartFormDataContent();

                form.Add(new ByteArrayContent(photo), "photo", "upload.png");

                var tsk = httpClient.PostAsync(url, form);
                tsk.Wait();
                var rslt = tsk.Result;
                var strtsk = rslt.Content.ReadAsByteArrayAsync();
                strtsk.Wait();
                byte[] result = strtsk.Result;
                string lol = Encoding.UTF8.GetString(result);

                return JsonConvert.DeserializeObject<UploadedPhoto>(lol);
            }
        }

        public static MessagesPhoto SaveMessagesPhoto(this VK bot, string photo, long server, string hash)
        {
            using (var w = new WebClient())
            {
                var a = new NameValueCollection();
                a.Add("access_token", bot.ACCESS);
                a.Add("v", "5.130");
                a.Add("photo", photo);
                a.Add("server", server.ToString());
                a.Add("hash", hash);
                string query = VKUtils.ToQueryString(a);

                string answ = w.DownloadString("https://api.vk.com/method/photos.saveMessagesPhoto" + query);
                var b = JsonConvert.DeserializeObject<MessagesPhotoRoot>(answ);
                return b.response[0];
            }
        }

        public static GroupResponse GetGroupInfo(this VK bot, long group_id)
        {
            group_id = Math.Abs(group_id);

            using (var w = new WebClient())
            {
                var a = new NameValueCollection();
                a.Add("access_token", bot.ACCESS);
                a.Add("group_id", group_id.ToString());
                a.Add("v", "5.130");
                a.Add("fields", "description, members_count");
                string query = VKUtils.ToQueryString(a);

                string answ = w.DownloadString("https://api.vk.com/method/groups.getById" + query);
                var b = JsonConvert.DeserializeObject<GroupRoot>(answ);
                return b.Response[0];
            }
        }

        public static ProfilePicture GetProfilePicture(this VK bot, long user_id, string namecase = "Nom")
        {
            using (var w = new WebClient())
            {
                var a = new NameValueCollection();
                a.Add("access_token", bot.ACCESS);
                a.Add("user_ids", user_id.ToString());
                a.Add("v", "5.130");
                a.Add("fields", "photo_400_orig,photo_200");
                a.Add("name_case", namecase);
                string query = VKUtils.ToQueryString(a);

                string answ = w.DownloadString("https://api.vk.com/method/users.get" + query);
                var b = JsonConvert.DeserializeObject<ProfilePictureRoot>(answ);
                return b.response[0];
            }
        }
        
        public static NameHolder GetName(this VK bot, long id, string namecase = "Nom", bool ignoreCache = false)
        {
            if (!ignoreCache)
            {
                var c = cache.FirstOrDefault((z) => z.id == id);
                if (c != null) return c.name;
            }
            if (id < 0)
            {
                var gi = bot.GetGroupInfo(id);
                return new NameHolder() { GroupName = gi.Name, LinkName = gi.ScreenName, FullName = gi.Name };
            }
            else
            {
                var a = bot.GetProfilePicture(id, namecase);
                return new NameHolder() { FirstName = a.FirstName, LastName = a.LastName, FullName = $"{a.FirstName} {a.LastName}" };
            }
        }

        public static ResponseConversationMembers GetConversationMembers(this VK bot, long peerid)
        {
            using (var w = new WebClient())
            {
                var a = new NameValueCollection();
                a.Add("access_token", bot.ACCESS);
                a.Add("v", "5.131");
                a.Add("peer_id", peerid.ToString());
                string query = VKUtils.ToQueryString(a);

                string answ = w.DownloadString("https://api.vk.com/method/messages.getConversationMembers" + query);
                var b = JsonConvert.DeserializeObject<RootConversationMembers>(answ);
                return b.response;
            }
        }
    }

    public class CachedNames
    {
        public long id;
        public NameHolder name;
    }
}
