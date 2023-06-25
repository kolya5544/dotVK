using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using VKBotExtensions;
using static System.Net.Mime.MediaTypeNames;

namespace dotVK
{
    public class VKUtils
    {
        public static string ToQueryString(NameValueCollection nvc)
        {
            var array = (
                from key in nvc.AllKeys
                from value in nvc.GetValues(key)
                select string.Format(
            "{0}={1}",
            HttpUtility.UrlEncode(key),
            HttpUtility.UrlEncode(value))
                ).ToArray();
            return "?" + string.Join("&", array);
        }

        public static string ResolveAttachmentURL(VK bot, Attachment att)
        {
            if (att is null) return "";
            if (att.Photo != null)
            {
                PhotoSize bestSize = null; long bestRes = 0;
                PhotoSize originalSize = null;
                foreach (PhotoSize ps in att.Photo.Sizes)
                {
                    if ((ps.Width * ps.Height) > bestRes && ps.Width < 2048 && ps.Height < 2048) { bestSize = ps; bestRes = (ps.Width * ps.Height); }
                    if (ps.Type == "z") { originalSize = ps; }
                }

                var url = bestSize.Url;
                return url;
            }
            if (att.Sticker != null)
            {
                var sticker = att.Sticker;
                PhotoSize bestSize = null; long bestRes = 0;
                PhotoSize originalSize = null;
                foreach (PhotoSize ps in sticker.Images)
                {
                    if ((ps.Width * ps.Height) > bestRes && ps.Width < 2048 && ps.Height < 2048) { bestSize = ps; bestRes = (ps.Width * ps.Height); }
                    if (ps.Type == "z") { originalSize = ps; }
                }

                var url = bestSize.Url;
                return url;
            }
            if (att.Wall != null)
            {
                var url = $"https://vk.com/";
                if (att.Wall.from != null)
                {
                    url += $"{att.Wall.from.screen_name}?w=wall{att.Wall.from_id}_{att.Wall.id}";
                }
                else
                {
                    url += "404";
                }
                return url;
            }
            if (att.Video != null)
            {
                var url = "";
                var shortName = att.Video.OwnerId > 0 ? "" : bot.GetGroupInfo(att.Video.OwnerId).ScreenName;
                if (att.Video.ContentRestricted != 1)
                {
                    url += $"https://vk.com/{(att.Video.OwnerId < 0 ? shortName : $"id{Math.Abs(att.Video.OwnerId)}")}?z=video{att.Video.OwnerId}_{att.Video.Id}";
                }
                else
                {
                    url += $"video is unavailable, or is only available for https://vk.com/{(att.Video.OwnerId < 0 ? shortName : $"id{Math.Abs(att.Video.OwnerId)}")} subscribers";
                }
                return url;
            }
            if (att.Audio != null)
            {
                // bruh
                var duration = TimeSpan.FromSeconds(att.Audio.Duration).ToString(@"hh\:mm\:ss");
                string trackFullName = $"{att.Audio.Artist} - {att.Audio.Title}, {duration}";
                return trackFullName;
            }
            if (att.Doc != null)
            {
                var f = att.Doc;
                return f.url;
            }
            if (att.AudioMessage != null)
            {
                return att.AudioMessage.LinkMp3;
            }
            return "";
        }
    }
}
