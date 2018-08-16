using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Forum.WEB.Models
{
    /// <summary>
    /// Comment model
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Id parametr which uniq for all comment
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Date and time creating a comment
        /// </summary>
        public DateTime datetime { get; set; }

        /// <summary>
        /// Full name of creator comment
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
        /// Message body comment
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string body { get; set; }
    }
}