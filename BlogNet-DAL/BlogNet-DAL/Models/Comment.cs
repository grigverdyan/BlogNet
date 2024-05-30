namespace BlogNet_DAL.Models
{
    public class CommentBase
    {
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }
        public int PostID { get; set; }
        public int UserID { get; set; }
    }

    public class Comment : CommentBase
    {
        public int CommentID { get; set; }
    }
}
