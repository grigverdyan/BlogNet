namespace BlogNet_UI.Models
{
    public class CommentDTO
    {
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }
        public int PostID { get; set; }
        public int UserID { get; set; }
        public int CommentID { get; set; }

        public CommentDTO(Comment comment)
        {
            Content = comment.Content;
            CommentDate = comment.CommentDate;
            PostID = comment.Post.PostID;
            UserID = comment.User.UserID;
        }

        public CommentDTO(string content, DateTime commentDate, int postID, int userID, int commentID)
        {
            Content = content;
            CommentDate = commentDate;
            PostID = postID;
            UserID = userID;
            CommentID = commentID;
        }
    }
}
