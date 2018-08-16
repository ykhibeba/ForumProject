using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Forum.WEB.Models
{
    /// <summary>
    /// Category of forum
    /// </summary>
    public class Categories
    {
        /// <summary>
        /// ID parametr which uniq for all post
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Title of category
        /// </summary>
        [Column(TypeName = "nvarchar")]
        [StringLength(256)]
        public string title { get; set; }

        /// <summary>
        /// Description of this category
        /// </summary>
        [Column(TypeName = "nvarchar")]
        [StringLength(256)]
        public string description { get; set; }

    }
}