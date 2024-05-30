namespace BlogNet_DAL.Models
{
    public class PostBase
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; }
        public int UserID { get; set; }
    }

    public class Post : PostBase
    {
        public int PostID { get; set; }
    }
}
