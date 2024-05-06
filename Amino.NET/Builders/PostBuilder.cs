using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection.Metadata.Ecma335;

namespace Amino.NET.Builders
{
    public class PostBuilder
    {
        public byte[] CoverImage { get; private set; }
        public byte[] BackgroundImage { get; private set; }
        public string BackgroundColor { get; private set; } = "#ffffff";
        public string Content { get; set; }
        public string Title { get; set; }
        public List<(byte[], string?, string?)> MediaList { get; } = new List<(byte[], string?, string?)>();
        public PostTypes PostType { get; set; } = PostTypes.Blog;
        public BackgroundTypes BackgroundType { get; set; } = BackgroundTypes.Color;
        public bool FansOnly { get; set; }


        public void WithCover(byte[] cover)
        {
            CoverImage = cover;
        }
        public void WithCover(string coverPath)
        {
            WithCover(File.ReadAllBytes(coverPath));
        }

        public void WithBackgroundImage(byte[] media)
        {
            BackgroundImage = media;
        }

        public void WithBackgroundImage(string mediaPath)
        {
            WithBackgroundImage(File.ReadAllBytes(mediaPath));
        }

        public void AddMedia(byte[] media, string mediaKey = null, string caption = null)
        {
            MediaList.Add((media, mediaKey, caption));
        }

        public void AddMedia(string mediaPath, string mediaKey = null, string caption = null)
        {
            AddMedia(File.ReadAllBytes(mediaPath), mediaKey, caption);
        }

        public string EmbedImage(string mediaKey) => $"[IMG={mediaKey}]";

        public enum PostTypes
        {
            Blog,
            Wiki
        }
        public enum BackgroundTypes
        {
            Color,
            Image
        }

    }
}
