using BlogNet_DAL.Models;
using System.Data.SqlClient;

namespace BlogNet_DAL
{
    public class PostRepository
    {
        private static readonly string sqlPostID = "PostID";
        private static readonly string sqlTitle = "Title";
        private static readonly string sqlContent = "Content";
        private static readonly string sqlPostDate = "PostDate";
        private static readonly string sqlUserID = "UserID";
        private readonly ConnectionManager connection;

        public PostRepository(ConnectionManager connection)
        {
            this.connection = connection;
        }
        public Post Get(int id)
        {
            const string getPost = "GetPost";
            SqlCommand getCommand = connection.CreateStoredProcedure(getPost);
            getCommand.Parameters.Add(sqlPostID, System.Data.SqlDbType.Int).Value = id;

            using SqlDataReader reader = getCommand.ExecuteReader();
            Post post = null;
            if (reader.Read())
            {
                post = new Post
                {
                    PostID = id,
                    Title = reader.GetString(reader.GetOrdinal(sqlTitle)),
                    Content = reader.GetString(reader.GetOrdinal(sqlContent)),
                    PostDate = reader.GetDateTime(reader.GetOrdinal(sqlPostDate)),
                    UserID = reader.GetInt32(reader.GetOrdinal(sqlUserID))
                };
            }
            return post;
        }
        public IEnumerable<Post> Get()
        {

            const string getPosts = "GetPosts";
            SqlCommand getCommand = connection.CreateStoredProcedure(getPosts);

            using SqlDataReader reader = getCommand.ExecuteReader();
            List<Post> posts = new();
            if (reader.Read())
            {
                Post post = new Post
                {
                    PostID = reader.GetInt32(reader.GetOrdinal(sqlPostID)),
                    Title = reader.GetString(reader.GetOrdinal(sqlTitle)),
                    Content = reader.GetString(reader.GetOrdinal(sqlContent)),
                    PostDate = reader.GetDateTime(reader.GetOrdinal(sqlPostDate)),
                    UserID = reader.GetInt32(reader.GetOrdinal(sqlUserID))
                };
                posts.Add(post);
            }
            return posts;
        }
        private bool Upsert(Post post)
        {
            const string upsertPost = "UpsertPost";
            const string id = "postID";
            const string title = "title";
            const string content = "content";
            const string postDate = "postDate";
            const string userID = "userID";

            SqlCommand cmd = connection.CreateStoredProcedure(upsertPost);
            SqlParameter idParam = cmd.Parameters.Add(id, System.Data.SqlDbType.Int);
            idParam.Direction = System.Data.ParameterDirection.InputOutput;
            idParam.Value = post.PostID;
            cmd.Parameters.Add(title, System.Data.SqlDbType.NVarChar).Value = post.Title;
            cmd.Parameters.Add(content, System.Data.SqlDbType.NVarChar).Value = post.Content;
            cmd.Parameters.Add(postDate, System.Data.SqlDbType.DateTime).Value = post.PostDate;
            var userId = cmd.Parameters.Add(userID, System.Data.SqlDbType.Int);
            userId.Value = post.UserID == 0 ? null : post.UserID;
            int ret = cmd.ExecuteNonQuery();
            if (ret > 0)
            {
                post.PostID = Convert.ToInt32(idParam.Value);
            }
            return ret > 0;
        }
        public Post Insert(PostBase postToInsert)
        {
            Post post = new Post
            {
                Title = postToInsert.Title,
                Content = postToInsert.Content,
                PostDate = postToInsert.PostDate,
                UserID = postToInsert.UserID,
                PostID = 0
            };
            if (!Upsert(post))
            {
                return null;
            }
            return post;
        }
        public bool Update(Post post)
        {
            return Upsert(post);
        }
        public bool Delete(int id)
        {
            const string idParam = "id";
            const string deletePost = "deletePost";

            SqlCommand cmd = connection.CreateStoredProcedure(deletePost);
            cmd.Parameters.Add(idParam, System.Data.SqlDbType.Int).Value = id;
            int ret = cmd.ExecuteNonQuery();
            return ret > 0;
        }
    }
}
