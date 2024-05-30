namespace BlogNet_UI.Models
{
    public class Post
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; }
        public User User { get; set; }
        public int PostID { get; set; }

        public Post(PostDTO pdto, User udto)
        {
            Title = pdto.Title;
            Content = pdto.Content;
            PostDate = pdto.PostDate;
            User = udto;
            PostID = pdto.PostID;
        }

        public Post(string title, string content, DateTime postDate, User user, int postID)
        {
            Title = title;
            Content = content;
            PostDate = postDate;
            User = user;
            PostID = postID;
        }
    }
}
