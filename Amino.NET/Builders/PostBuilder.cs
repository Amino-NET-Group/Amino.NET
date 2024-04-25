using System.Collections.Generic;
using System.IO;

namespace Amino.NET.Builders
{
    public class PostBuilder
    {
        public byte[] CoverImage { get; private set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public Dictionary<byte[], string> MediaList { get; } = new Dictionary<byte[], string>();
        public PostTypes PostType { get; set; } = PostTypes.Blog;

        public void WithCover(byte[] cover)
        {
            CoverImage = cover;
        }
        public void WithCover(string coverPath)
        {
            WithCover(File.ReadAllBytes(coverPath));
        }
        public void AddMedia(byte[] media, string mediaKey)
        {
            MediaList.Add(media, mediaKey);
        }

        public void AddMedia(string mediaPath, string mediaKey)
        {
            AddMedia(File.ReadAllBytes(mediaPath), mediaKey);
        }

        public enum PostTypes
        {
            Blog,
            Wiki
        }

    }
}
