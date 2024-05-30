using BlogNet_UI.Models;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Azure.Storage;

namespace BlogNet_UI.Pages;

public class PhotoURLHelper
{
    public string PhotoUrl { set; get; }

    public PhotoURLHelper() { }
    public PhotoURLHelper(string photoUrl)
    {
        PhotoUrl = photoUrl;
    }
};

public partial class RegisterUser : ComponentBase
{
    User user = new User();

    [Inject]
    HttpClient client { get; set; }

    [Inject]
    IConfiguration configuration { get; set; }

    string ErrorMessage { get; set; }

    IList<IBrowserFile> files = new List<IBrowserFile>();
    private async Task UploadFilesAsync(IBrowserFile file)
    {
        files.Add(file);
    }

    private async Task<bool> UpdatePhotoUrl(int userId, string path)
    {
        PhotoURLHelper photourl = new(path);
        HttpResponseMessage response = await client.PutAsJsonAsync<PhotoURLHelper>($"https://blognetservice.azurewebsites.net/api/users/{userId}", photourl);
        if (!response.IsSuccessStatusCode)
        {
            ErrorMessage = await response.Content.ReadAsStringAsync();
            return false;
        }
        return true;
    }

    private async Task<bool> InstallPhoto(UserDTO userDTO)
    {
        string accountName = "blognetstorage";
        string accountKey = configuration.GetConnectionString("connectionStringBlob");
        string containerName = "userpics";
        string blobUri = "https://" + accountName + ".blob.core.windows.net";

        if (files.Count == 0)
        {
            return true;
        }

        Azure.Storage.StorageSharedKeyCredential sharedKeyCredential =
                                                new StorageSharedKeyCredential(accountName, accountKey);

        var blobServiceClient = new BlobServiceClient
                                                (new Uri(blobUri), sharedKeyCredential);

        // Create a BlobContainerClient
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        // Create the container if it doesn't exist
        if (!containerClient.Exists())
        {
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);
        }

        // Create a unique blob name
        string blobName = "registrationPhoto" + userDTO.UserID;

        // Create a BlobClient
        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        // Upload the file to the blob
        using (Stream fileStream = files.LastOrDefault().OpenReadStream())
        {
            var path = await blobClient.UploadAsync(fileStream, overwrite: true);
            if (path.Value != null)
            {
                return await UpdatePhotoUrl(userDTO.UserID, blobUri + blobClient.Uri.AbsolutePath);
            }
        }
        return false;
    }
    public async Task submit(EditContext context)
    {
        UserDTO userDto = new UserDTO(user);

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
        files.Clear();

    }

}
