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
        public string ImageLink { get; set; }
        public string ArticleLink { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        //public ICollection<User> Users { get; set; }

        public string shortString(string input, int limit)
        {
            if (input.Length < limit)
                return input;
            else
                return input.Substring(0, limit) + "...";
        }
    }
}