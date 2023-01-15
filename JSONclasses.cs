﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotVK
{

    class FailedRoot
    {
        public int failed { get; set; }
        public int ts { get; set; }
    }

    public class Root
    {
        [JsonProperty("ts")]
        public string Ts { get; set; }

        [JsonProperty("updates")]
        public List<Update> Updates { get; set; }
    }

    public partial class GroupRoot
    {
        [JsonProperty("response")]
        public List<GroupResponse> Response { get; set; }
    }

    public partial class GroupResponse
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("is_closed")]
        public long IsClosed { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("photo_50")]
        public string Photo50 { get; set; }

        [JsonProperty("photo_100")]
        public string Photo100 { get; set; }

        [JsonProperty("photo_200")]
        public string Photo200 { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class VKConvIDRes
    {
        [JsonProperty("response")]
        public VKConvResp Response { get; set; }
    }

    public partial class VKConvResp
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("items")]
        public List<IDItem> Items { get; set; }
    }

    public partial class IDItem
    {
        [JsonProperty("date")]
        public long Date { get; set; }

        [JsonProperty("from_id")]
        public long FromId { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }

    public class VKMessage
    {
        public List<Response> response { get; set; }
    }

    public class Response
    {

        [JsonProperty("peer_id")]
        public long PeerId { get; set; }

        [JsonProperty("message_id")]
        public long MessageId { get; set; }

        [JsonProperty("conversation_message_id")]
        public long ConversationMessageId { get; set; }

    }

    public class Forward
    {
        public long owner_id { get; set; }
        public long peer_id { get; set; }
        public List<long> conversation_message_ids { get; set; }
        public bool is_reply { get; set; }
    }

    public class UploadServerRoot
    {
        public UploadServer response { get; set; }
    }
    public class UploadServer
    {
        [JsonProperty("album_id")]
        public long AlbumId { get; set; }
        [JsonProperty("video_id")]
        public long VideoId { get; set; }
        [JsonProperty("owner_id")]
        public long OwnerId { get; set; }

        [JsonProperty("upload_url")]
        public string UploadUrl { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }
    }

    public class MessagesFile
    {
        public string type { get; set; }
        public Doc doc { get; set; }
    }

    public class Doc
    {
        public int id { get; set; }
        public int owner_id { get; set; }
        public string title { get; set; }
        public int size { get; set; }
        public string ext { get; set; }
        public int date { get; set; }
        public int type { get; set; }
        public string url { get; set; }
    }

    public class MessagesFileRoot
    {
        public MessagesFile response { get; set; }
    }

    public class MessagesPhotoRoot
    {
        public List<MessagesPhoto> response { get; set; }
    }

    public class MessagesPhoto
    {
        [JsonProperty("album_id")]
        public long AlbumId { get; set; }

        [JsonProperty("date")]
        public long Date { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("owner_id")]
        public long OwnerId { get; set; }

        [JsonProperty("has_tags")]
        public bool HasTags { get; set; }

        [JsonProperty("access_key")]
        public string AccessKey { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class UploadedPhoto
    {
        [JsonProperty("server")]
        public long Server { get; set; }

        [JsonProperty("photo")]
        public string Photo { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }
    }

    public class UploadedFile
    {
        [JsonProperty("file")]
        public string File { get; set; }
    }

    public class UpdateServer
    {
        [JsonProperty("response")]
        public rsp Response { get; set; }
    }

    public partial class rsp
    {
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("server")]
        public string Server { get; set; }

        [JsonProperty("ts")]
        public string Ts { get; set; }
    }

    public partial class Update
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("object")]
        public Object Object { get; set; }

        [JsonProperty("group_id")]
        public long GroupId { get; set; }

        [JsonProperty("event_id")]
        public string EventId { get; set; }
    }

    public partial class Object
    {
        [JsonProperty("message")]
        public Message Message { get; set; }

        [JsonProperty("client_info")]
        public ClientInfo ClientInfo { get; set; }
    }

    public partial class ClientInfo
    {
        [JsonProperty("button_actions")]
        public List<string> ButtonActions { get; set; }

        [JsonProperty("keyboard")]
        public bool Keyboard { get; set; }

        [JsonProperty("inline_keyboard")]
        public bool InlineKeyboard { get; set; }

        [JsonProperty("carousel")]
        public bool Carousel { get; set; }

        [JsonProperty("lang_id")]
        public long LangId { get; set; }
    }

    public class ProfilePictureRoot
    {
        public List<ProfilePicture> response;
    }

    public class ProfilePicture {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("can_access_closed")]
        public bool CanAccessClosed { get; set; }

        [JsonProperty("is_closed")]
        public bool IsClosed { get; set; }

        [JsonProperty("photo_400_orig")]
        public string Photo400_Orig { get; set; }
        [JsonProperty("photo_200")]
        public string Photo200 { get; set; }
    }

    public partial class Message
    {
        [JsonProperty("date")]
        public long Date { get; set; }

        [JsonProperty("from_id")]
        public long FromId { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("out")]
        public long Out { get; set; }

        [JsonProperty("peer_id")]
        public long PeerId { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("conversation_message_id")]
        public long ConversationMessageId { get; set; }

        [JsonProperty("fwd_messages")]
        public List<Message> FwdMessages { get; set; }

        [JsonProperty("reply_message")]
        public Message ReplyMessage { get; set; }

        [JsonProperty("important")]
        public bool Important { get; set; }

        [JsonProperty("random_id")]
        public long RandomId { get; set; }

        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; set; }

        [JsonProperty("is_hidden")]
        public bool IsHidden { get; set; }
    }

    public class ContentSource
    {
        public string type { get; set; }
        public long owner_id { get; set; }
        public long peer_id { get; set; }
        public long conversation_message_id { get; set; }
    }
    
    public class Sticker
    {
        [JsonProperty("images")]
        public List<PhotoSize> Images { get; set; }
    }

    public class Attachment
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("photo")]
        public AttachmentPhoto Photo { get; set; }
        [JsonProperty("sticker")]
        public Sticker Sticker { get; set; }
        [JsonProperty("story")]
        public Story Story { get; set; }
    }

    public class Story
    {
        public int id { get; set; }
        public int owner_id { get; set; }
        public string access_key { get; set; }
        public int can_comment { get; set; }
        public int can_reply { get; set; }
        public int can_see { get; set; }
        public bool can_like { get; set; }
        public int can_share { get; set; }
        public int can_hide { get; set; }
        public int date { get; set; }
        public int expires_at { get; set; }
        public int seen { get; set; }
        public bool is_one_time { get; set; }
        public string track_code { get; set; }
        public string type { get; set; }
        public Video video { get; set; }
        public string reaction_set_id { get; set; }
        public bool no_sound { get; set; }
        public int can_ask { get; set; }
        public int can_ask_anonymous { get; set; }
    }

    public class Video
    {
        public Files files { get; set; }
        public string access_key { get; set; }
        public int can_add { get; set; }
        public int can_download { get; set; }
        public int is_private { get; set; }
        public int date { get; set; }
        public string description { get; set; }
        public int duration { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int id { get; set; }
        public int owner_id { get; set; }
        public string title { get; set; }
        public string player { get; set; }
        public string type { get; set; }
        public int views { get; set; }
    }

    public class Files
    {
        public string mp4_720 { get; set; }
    }

    public class AttachmentPhoto
    {
        [JsonProperty("album_id")]
        public long AlbumId { get; set; }

        [JsonProperty("date")]
        public long Date { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("owner_id")]
        public long OwnerId { get; set; }

        [JsonProperty("has_tags")]
        public bool HasTags { get; set; }

        [JsonProperty("access_key")]
        public string AccessKey { get; set; }

        [JsonProperty("sizes")]
        public List<PhotoSize> Sizes { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class PhotoSize
    {
        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }

    public partial class Agent
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }
    }
}
