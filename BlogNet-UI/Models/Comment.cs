namespace BlogNet_UI.Models
{
    public class Comment
    {
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
        public int CommentID { get; set; }

        public Comment(CommentDTO commentDTO, Post post, User user)
        {
            CommentID = commentDTO.CommentID;
            Post = post;
            User = user;
            CommentDate = commentDTO.CommentDate;
            Content = commentDTO.Content;
        }

        public Comment(string content, DateTime commentDate, Post post, User user, int commentID)
        {
            Content = content;
            CommentDate = commentDate;
            Post = post;
            User = user;
            CommentID = commentID;
        }
    }
}
