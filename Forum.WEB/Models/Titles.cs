using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.WEB.Models
{
    /// <summary>
    /// Titles of category and it posts
    /// </summary>
    public class Title
    {
        /// <summary>
        /// Category id.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Category title.
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// Post titles.
        /// </summary>
        public IEnumerable<PostTitles> posts { get; set; }
    }

    /// <summary>
    /// Post titles.
    /// </summary>
    public class PostTitles
    {
        /// <summary>
        /// Post id.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Post titles.
        /// </summary>
        public string title { get; set; }
    }
}