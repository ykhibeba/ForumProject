using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.BLL.DTO
{
    public class CommentDTO
    {
        public int ID { get; set; }

        public int PostID { get; set; }

        public DateTime DateTime { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Body { get; set; }
    }
}
