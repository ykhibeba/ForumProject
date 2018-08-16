using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Forum.BLL.DTO
{
    public class CategoryDTO
    {
        public int ID { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(256)]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(256)]
        public string Description { get; set; }
    }
}
