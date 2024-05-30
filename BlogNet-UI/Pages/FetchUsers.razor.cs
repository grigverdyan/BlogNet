using BlogNet_UI.Models;
using Microsoft.AspNetCore.Components;

namespace Blognet_UI.Pages;

public partial class FetchUsers : ComponentBase
{
    [Inject]
    HttpClient client { get; set; }
    List<User> users { get; set; }
    string ErrorMessage { get; set; }
    protected override async Task OnInitializedAsync()
    {
        var userDTOs = await client.GetFromJsonAsync<List<UserDTO>>("https://blognetservice.azurewebsites.net/api/users");
        if (userDTOs is null)
        {
            ErrorMessage = "There are no users!";
        }
        else
        {
            users = new List<User>();
            foreach (var us in userDTOs)
            {
                User user = new(us);
                users.Add(user);
            }
        }

    }
}
