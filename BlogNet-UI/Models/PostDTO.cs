namespace BlogNet_UI.Models
{
    public class PostDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; }
        public int UserID { get; set; }
        public int PostID { get; set; }

        public PostDTO(Post post)
        {
            Title = post.Title;
            Content = post.Content;
            PostDate = post.PostDate;
            UserID = post.User.UserID;
       //     PostID = post.PostID;
        }

        public PostDTO(string title, string content, DateTime postDate, int userID, int postID)
        {
            Title = title;
            Content = content;
            PostDate = postDate;
            UserID = userID;
            PostID = postID;
        }
    }
}
