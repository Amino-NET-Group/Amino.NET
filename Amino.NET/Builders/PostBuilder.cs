using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection.Metadata.Ecma335;

namespace Amino.Builders
{
    /// <summary>
    /// An object to easily create Blogs and Wikis
    /// </summary>
    public class PostBuilder
    {
        /// <summary>
        /// The CoverImage of the Post, this is used for <see cref="PostTypes.Wiki"/>
        /// </summary>
        public byte[] CoverImage { get; private set; }
        /// <summary>
        /// The BackgroundImage of the post
        /// </summary>
        public byte[] BackgroundImage { get; private set; }
        /// <summary>
        /// The color of the background if no <see cref="BackgroundImage"/> is set, default: #ffffff
        /// </summary>
        public string BackgroundColor { get; private set; } = "#ffffff";
        /// <summary>
        /// The Content of your post
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// The Title of your post
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// A list of Media attached to the post with Image data, image key and image subtext
        /// </summary>
        public List<(byte[], string?, string?)> MediaList { get; } = new List<(byte[], string?, string?)>();
        /// <summary>
        /// The type of Post you want to create, default is <see cref="PostTypes.Blog"/>
        /// </summary>
        public PostTypes PostType { get; set; } = PostTypes.Blog;
        /// <summary>
        /// The type of background for your post, default is <see cref="BackgroundTypes.Color"/>
        /// </summary>
        public BackgroundTypes BackgroundType { get; set; } = BackgroundTypes.Color;
        /// <summary>
        /// Determines if your post is only availabe for VIP members or not, default is false
        /// </summary>
        public bool FansOnly { get; set; } = false;

        /// <summary>
        /// Allows you to attach a Cover to your Post via Byte[]
        /// </summary>
        /// <param name="cover">The image data for your cover</param>
        public void WithCover(byte[] cover)
        {
            CoverImage = cover;
        }
        /// <summary>
        /// Allows you to attach a Cover to your Post via FilePath
        /// </summary>
        /// <param name="coverPath">The path to your file</param>
        public void WithCover(string coverPath)
        {
            WithCover(File.ReadAllBytes(coverPath));
        }
        /// <summary>
        /// Allows you to attach a Background Image via file bytes
        /// </summary>
        /// <param name="media">The bytes of your image file you want to attach as background</param>
        public void WithBackgroundImage(byte[] media)
        {
            BackgroundImage = media;
        }
        /// <summary>
        /// Allows you to attach a Background Image via file path
        /// </summary>
        /// <param name="mediaPath">The path to your file</param>
        public void WithBackgroundImage(string mediaPath)
        {
            WithBackgroundImage(File.ReadAllBytes(mediaPath));
        }

        /// <summary>
        /// Allows you to add Media to your post
        /// </summary>
        /// <param name="media">The bytes of your media file</param>
        /// <param name="mediaKey">The Key you can attach to the media to embed it into the Post <see cref="Content"/></param>
        /// <param name="caption">The caption of the media</param>
        public void AddMedia(byte[] media, string mediaKey = null, string caption = null)
        {
            MediaList.Add((media, mediaKey, caption));
        }
        /// <summary>
        /// Allows you to add Media to your post
        /// </summary>
        /// <param name="mediaPath">The file path to your media file</param>
        /// <param name="mediaKey">The Key you can use to attach the Media into the Post <see cref="Content"/></param>
        /// <param name="caption">The caption of your media file</param>
        public void AddMedia(string mediaPath, string mediaKey = null, string caption = null)
        {
            AddMedia(File.ReadAllBytes(mediaPath), mediaKey, caption);
        }
        /// <summary>
        /// A string you can use to embed an image into your <see cref="Content"/>
        /// </summary>
        /// <param name="mediaKey">The Key to your attached media file</param>
        /// <returns>[IMG=<paramref name="mediaKey"/>]</returns>
        public string EmbedImage(string mediaKey) => $"[IMG={mediaKey}]";

        /// <summary>
        /// An enum for all availabe <see cref="PostTypes"/>
        /// </summary>
        public enum PostTypes
        {
            /// <summary>
            /// Determines if your target post is a Blog
            /// </summary>
            Blog,
            /// <summary>
            /// Determines if your target post is a Wiki
            /// </summary>
            Wiki
        }
        /// <summary>
        /// An enum for all available <see cref="BackgroundTypes"/>
        /// </summary>
        public enum BackgroundTypes
        {
            /// <summary>
            /// Determines if your Post has a single Color as background
            /// </summary>
            Color,
            /// <summary>
            /// Determines if your Post has an Image as background
            /// </summary>
            Image
        }

    }
}
