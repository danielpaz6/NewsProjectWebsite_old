using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsProject.Models
{
    public class Article
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public int NumOfLikes { get; set; }
        public List<User> LikesList { get; set; }
        public string ImageLink { get; set; }
        public string ArticleLink { get; set; }
        public User Writer { get; set; }
        public int UserId { get; set; }

    }
}