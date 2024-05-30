using BlogNet_UI.Models;
using Microsoft.AspNetCore.Components;

namespace Blognet_UI.Pages;

/*
public partial class FetchComments : ComponentBase
{
    [Inject]
    HttpClient client { get; set; }
    List<Post> posts { get; set; }
    string ErrorMessage { get; set; }
    protected override async Task OnInitializedAsync()
    {
        //var commentDTOs = await client.GetFromJsonAsync<List<CommentDTO>>("https://blognetservice.azurewebsites.net/api/posts/{PostID}/comments");
        var postDTOs = await client.GetFromJsonAsync<List<PostDTO>>("https://blognetservice.azurewebsites.net/api/posts");
        var userDTOs = await client.GetFromJsonAsync<List<UserDTO>>("https://blognetservice.azurewebsites.net/api/users");
        if (commentDTOs is not null || postDTOs is null || userDTOs is null)
        {
            ErrorMessage = "There are no comments!";
        }
        else
        {
            posts = new List<Post>();
            foreach (var ps in postDTOs)
            {
                var userDTO = userDTOs.Find(us => us.UserID == ps.UserID);
                User user = new(userDTO!);
                Post post = new(ps, user);
                posts.Add(post);
            }
        }

    }
}
*/