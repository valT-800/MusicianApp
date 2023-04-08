using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace MusicianAppUI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UploadAzureController : ControllerBase
  {
    private readonly string _azureConnectionString;
    public UploadAzureController(IConfiguration configuration)
    {
      _azureConnectionString = configuration.GetConnectionString("AzureConnectionString");
    }
    
    [HttpPost("[action]")]
    public async Task Upload(IList<IFormFile> UploadFiles)
    {
      try
      {
        foreach (var files in UploadFiles)
        {
          // Azure connection string and container name passed as an argument to get the Blob reference of the container.
          var container = new BlobContainerClient(_azureConnectionString, "upload-container");

          // Method to create our container if it doesn’t exist.
          var createResponse = await container.CreateIfNotExistsAsync();

          // If container successfully created, then set public access type to Blob.
          if (createResponse != null && createResponse.GetRawResponse().Status == 201)
            await container.SetAccessPolicyAsync(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

          // Method to create a new Blob client.
          var blob = container.GetBlobClient(files.FileName);

          // If a blob with the same name exists, then we delete the Blob and its snapshots.
          await blob.DeleteIfExistsAsync(Azure.Storage.Blobs.Models.DeleteSnapshotsOption.IncludeSnapshots);

          // Create a file stream and use the UploadSync method to upload the Blob.
          using (var fileStream = files.OpenReadStream())
          {
            await blob.UploadAsync(fileStream, new Azure.Storage.Blobs.Models.BlobHttpHeaders { ContentType = files.ContentType });
          }
        }
      }
      catch (Exception e)
      {
        Response.Clear();
        Response.StatusCode = 204;
        Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "File failed to upload";
        Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = e.Message;
      }
    }
  }
}
