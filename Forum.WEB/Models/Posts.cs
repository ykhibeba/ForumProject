using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Forum.WEB.Models
{
    /// <summary>
    /// All posts
    /// </summary>
    public class Posts
    {
        /// <summary>
        /// ID parametr which uniq for all post
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Title of post
        /// </summary>
        [Column(TypeName = "nvarchar")]
        [StringLength(256)]
        public string title { get; set; }

        /// <summary>
        /// Name creator post
        /// </summary>
        [Column(TypeName = "nvarchar")]
        [StringLength(128)]
        public string name { get; set; }
        
        /// <summary>
        /// User name.
        /// </summary>
        [Column(TypeName = "nvarchar")]
        [StringLength(128)]
        public string username { get; set; }

        /// <summary>
        /// Date and time creating post
        /// </summary>
        public DateTime datetime { get; set; }

    }

    /// <summary>
    /// Post of specific category for POST StatusCode
    /// </summary>
    public class Post
    {
        /// <summary>
        /// ID parametr which uniq for all post
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Date and time creating a comment
        /// </summary>
        public DateTime datetime { get; set; }

        /// <summary>
        /// Title of post
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar")]
        [StringLength(256)]
        public string title { get; set; }

        /// <summary>
        /// Name of creator post
        /// </summary>
        [Column(TypeName = "nvarchar")]
        [StringLength(128)]
        public string name { get; set; }

        /// <summary>
        /// User name.
        /// </summary>
        [Column(TypeName = "nvarchar")]
        [StringLength(128)]
        public string username { get; set; }

        /// <summary>
        /// Message body post
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string body { get; set; }

        /// <summary>
        /// List of comment for this post
        /// </summary>
        public IEnumerable<Comment> comments { get; set; }
    }

    /// <summary>
    /// Model for Update Post PUT StatusCode
    /// </summary>
    public class PutPost
    {
        /// <summary>
        /// Title of post
        /// </summary>
        [Column(TypeName = "nvarchar")]
        [StringLength(256)]
        public string title { get; set; }

        /// <summary>
        /// Message body post
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string body { get; set; }
    }
}