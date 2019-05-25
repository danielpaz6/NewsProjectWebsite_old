using System;

/// <summary>
/// Summary description for Class1
/// </summary>
/// 
namespace NewsProject.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int UserPassword { get; set; }
        public string Email { get; set; }
        public int Permissions { get; set; }
    }
}