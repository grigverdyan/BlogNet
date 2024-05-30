using Microsoft.AspNetCore.Components;
using BlogNet_UI.Models;
using BlogNet_UI.Pages;

/*
namespace BlogNet_UI.Pages
{
    public class CreatePost : ComponentBase
    {
        Post post = new Post();

        [Inject]
        HttpClient client { get; set; }

        [Inject]
        IConfiguration configuration { get; set; }

        private string ErrorMessage;

        protected override async Task OnInitializedAsync()
        {
            // Initialize the post object with default values
            post = new Post
            {
                Title = string.Empty,
                Content = string.Empty,
                PostDate = DateTime.Now,
                User = await UserService.GetCurrentUserAsync() // Fetch the current user
            };
        }

        private async Task SubmitPost()
        {
            try
            {
                PostDTO postDto = new PostDTO(post);

                HttpResponseMessage response = await client.PostAsJsonAsync<UserDTO>("https://blognetservice.azurewebsites.net/api/users/", userDto);
                if (!response.IsSuccessStatusCode)
                {
                    ErrorMessage = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    var resultUserDto = await response.Content.ReadFromJsonAsync<UserDTO>();
                    resultUserDto.RegistrationDate = DateTime.Now;
                    var ret = await InstallPhoto(resultUserDto!);
                    if (ret)
                    {
                        ErrorMessage = "Completed Successfully!";
                    }
                }
                await PostService.CreatePostAsync(post);
                // Navigate to the post list or details page after successful submission
                NavigationManager.NavigateTo("/posts");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error creating post: {ex.Message}";
            }
        }
    }
}
*/