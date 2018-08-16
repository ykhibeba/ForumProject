using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.BLL.DTO
{
    public class PostDTO
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string CreatorName { get; set; }

        public string UserName { get; set; }

        public DateTime DateTime { get; set; }

        public string Body { get; set; }

        public int CategoryID { get; set; }
    }
}
