using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsProject.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Color { get; set; }
        public string Name { get; set; }
        public int count { get; set; }
    }
}