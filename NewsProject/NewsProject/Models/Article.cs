using System;


public class Article
{
    public int ArticleId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Date { get; set; }
    public int NumOfLikes { get; set; }
    public List<User> Users { get; set; }
    public string Image { get; set; }
    public string ArticleLink { get; set; }
    public User Writer { get; set; }
    
}
