# dotVK - C# VK API library

dotVK is a C# library that allows you to communicate with VK API to create different applications (primarily chatbots).

**Status**: Most of VK API features are NOT implemented, however dotVK in its current version is *usable enough* to build simple chatbot applications, and can easily be extended to support more API operations.

(Not a project I am proud of, but one I made open-source anyway)

## Features

- Receiving messages using longpoll
- Sending messages
- Sending pictures, documents/files, reading pictures, documents/files, <...>

# Usage

## Longpoll initialization

To initialize longpoll, you first need to acquire a token for your VK group. Once done, you can initialize your VK bot like this:

```cs
var bot = new VK("<TOKEN>", <GROUP_ID>);
bot.UpdateLongPoll();
bot.EnqueueUpdates(300 * 1000);
```

where token is your token and group_id is the ID of your group (without a minus sign!). `UpdateLongPoll();` will acquire VK's longpoll server, and `EnqueueUpdates();` will make bot update the server once per 5 minutes.

## Longpoll and basic features utilization

Once you've initialized the bot, you can receive longpoll updates by repeatedly calling `ReceiveMessage()`:

```cs
using VKBotExtensions;
<...>

while (true)
{
	var msg = bot.ReceiveMessage();
	if (msg.Updates.Count == 0) continue;
	foreach (Update u in msg.Updates)
	{
		string text = u.Object.Message.Text;
		long sender = u.Object.Message.FromId;
		long peerid = u.Object.Message.PeerId;

		if (text == "/hello")
		{
			bot.SendMessage(peerid, $"Hello, {sender}");
		}
	}
}
```

the example above will repeatedly look for new messages.

## Attachments, replied and forwarded messages

You can access attachments, replies, and forwarded messages using this code:

```cs
Attachment attach = message.Attachments.FirstOrDefault((z) => z.Type == "photo"); // this code will look for pictures
Console.WriteLine(attach.Photo.Sizes.First().Url); // outputs direct URL to a picture
if (message.ReplyMessage is not null) // if there is a message replied to, utilize it instead
{
	attach = message.ReplyMessage.Attachments.FirstOrDefault((x) => x.Type == "sticker");
	Console.WriteLine(attach.Images.First().Url); // direct URL to the sticker
}
message.FwdMessages.ForEach((z) => 
{
	var attach = z.Attachments.FirstOrDefault((c) => c.Type == "wall");
	Console.WriteLine($"https://vk.com/{attachment.Wall.from.screen_name}?w=wall{attachment.Wall.from_id}_{attachment.Wall.id}"); // URL of the VK post
});
```

## Upload flow (uploading a picture, file, etc.)

To upload a picture you will need to do several API requests.

```cs
SKBitmap bmp = SKBitmap.Decode("picture.png"); // this is a picture you want to upload

var uploadURL  = bot.GetMessagesUploadServer().UploadUrl; // first you have to get upload server
var photo = bot.UploadPhoto(uploadURL, bmp); // upload the picture
var msgPhoto = bot.SaveMessagesPhoto(photo.Photo, photo.Server, photo.Hash); // save the photo
string attachString = $"photo{msgPhoto.OwnerId}_{msgPhoto.Id}"; // get attachment string

Console.WriteLine($"Your photo is now at ");

// now to send a message containing this photo, you can just do
bot.SendMessage(peerid, "This is your picture:", attachString);
```

To upload file, use this code instead:

```cs
var file = File.ReadAllBytes("somefile.txt");

var uploadServer = bot.GetDocumentMessagesUploadServer(peer); // get upload server for this dialogue
var vkFile = bot.UploadFile(uploadServer.UploadUrl, file, "txt"); // upload file
var msgFile = bot.SaveMessagesFile(vkFile.File); // save file
string attachString = $"doc{msgFile.doc.owner_id}_{msgFile.doc.id}"; // get attachment string
```

# Any issues?

I cannot promise I will maintain this library, but I can help you if you encounter serious issues using this library. Create a GitHub issue describing your issue.

We do not accept PRs, sorry.