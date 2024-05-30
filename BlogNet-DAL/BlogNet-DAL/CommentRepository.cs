using BlogNet_DAL.Models;
using System.Data.SqlClient;

namespace BlogNet_DAL
{
    public class CommentRepository
    {
        private static readonly string sqlCommentID = "CommentID";
        private static readonly string sqlContent = "Content";
        private static readonly string sqlCommentDate = "CommentDate";
        private static readonly string sqlUserID = "UserID";
        private static readonly string sqlPostID = "PostID";
        private readonly ConnectionManager connection;

        public CommentRepository(ConnectionManager connection)
        {
            this.connection = connection;
        }
        public Comment Get(int id)
        {
            const string getComment = "GetComment";
            SqlCommand getCommand = connection.CreateStoredProcedure(getComment);
            getCommand.Parameters.Add(sqlCommentID, System.Data.SqlDbType.Int).Value = id;

            using SqlDataReader reader = getCommand.ExecuteReader();
            Comment comment = null;
            if (reader.Read())
            {
                comment = new Comment
                {
                    CommentID = id,
                    Content = reader.GetString(reader.GetOrdinal(sqlContent)),
                    CommentDate = reader.GetDateTime(reader.GetOrdinal(sqlCommentDate)),
                    UserID = reader.GetInt32(reader.GetOrdinal(sqlUserID)),
                    PostID = reader.GetInt32(reader.GetOrdinal(sqlPostID))
                };
            }
            return comment;
        }
        public IEnumerable<Comment> Get()
        {

            const string getComments = "GetComments";
            SqlCommand getCommand = connection.CreateStoredProcedure(getComments);

            using SqlDataReader reader = getCommand.ExecuteReader();
            List<Comment> comments = new();
            if (reader.Read())
            {
                Comment comment = new Comment
                {
                    CommentID = reader.GetInt32(reader.GetOrdinal(sqlCommentID)),
                    Content = reader.GetString(reader.GetOrdinal(sqlContent)),
                    CommentDate = reader.GetDateTime(reader.GetOrdinal(sqlCommentDate)),
                    UserID = reader.GetInt32(reader.GetOrdinal(sqlUserID)),
                    PostID = reader.GetInt32(reader.GetOrdinal(sqlPostID))
                };
                comments.Add(comment);
            }
            return comments;
        }
        private bool Upsert(Comment comment)
        {
            const string upsertComment = "UpsertComment";
            const string id = "commentID";
            const string content = "content";
            const string commentDate = "commentDate";
            const string userID = "userID";
            const string postID = "postID";

            SqlCommand cmd = connection.CreateStoredProcedure(upsertComment);
            SqlParameter idParam = cmd.Parameters.Add(id, System.Data.SqlDbType.Int);
            idParam.Direction = System.Data.ParameterDirection.InputOutput;
            idParam.Value = comment.CommentID;
            cmd.Parameters.Add(content, System.Data.SqlDbType.NVarChar).Value = comment.Content;
            cmd.Parameters.Add(commentDate, System.Data.SqlDbType.DateTime).Value = comment.CommentDate;
            
            var userId = cmd.Parameters.Add(userID, System.Data.SqlDbType.Int);
            userId.Value = comment.UserID == 0 ? null : comment.UserID;
            
            var postId = cmd.Parameters.Add(postID, System.Data.SqlDbType.Int);
            postId.Value = comment.PostID == 0 ? null : comment.PostID;

            int ret = cmd.ExecuteNonQuery();
            if (ret > 0)
            {
                comment.CommentID = Convert.ToInt32(idParam.Value);
            }
            return ret > 0;
        }
        public Comment Insert(CommentBase commentToInsert)
        {
            Comment comment = new Comment
            {
                Content = commentToInsert.Content,
                CommentDate = commentToInsert.CommentDate,
                UserID = commentToInsert.UserID,
                PostID = commentToInsert.PostID,
                CommentID = 0
            };
            if (!Upsert(comment))
            {
                return null;
            }
            return comment;
        }
        public bool Update(Comment comment)
        {
            return Upsert(comment);
        }
        public bool Delete(int id)
        {
            const string idParam = "id";
            const string deleteComment = "deleteComment";

            SqlCommand cmd = connection.CreateStoredProcedure(deleteComment);
            cmd.Parameters.Add(idParam, System.Data.SqlDbType.Int).Value = id;
            int ret = cmd.ExecuteNonQuery();
            return ret > 0;
        }
    }
}
