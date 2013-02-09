using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Runtime.Serialization;
using Cascade.Web.ApplicationIntegration;
namespace Cascade.Web.Controllers
{
 
    public class FileUploaderController : ApiController
    {
        public Task<IEnumerable<FileDesc>> Post()
        {
            var folderName = "UploadedFiles";
            var PATH = HttpContext.Current.Server.MapPath("~/App_data/" + folderName);
            var rootUrl = Request.RequestUri.AbsoluteUri.Replace(Request.RequestUri.AbsolutePath, String.Empty);
            if (Request.Content.IsMimeMultipartContent())
            {
                var streamProvider = new CustomMultipartFormDataStreamProvider(PATH);
                var task = Request.Content.ReadAsMultipartAsync(streamProvider).ContinueWith<IEnumerable<FileDesc>>(t =>
                {

                    if (t.IsFaulted || t.IsCanceled)
                    {
                        throw new HttpResponseException(HttpStatusCode.InternalServerError);
                    }

                    var fileInfo = streamProvider.FileData.Select(i =>
                    {
                        var info = new FileInfo(i.LocalFileName);
                        return new FileDesc(info.Name, rootUrl + "/" + folderName + "/" + info.Name, info.Length / 1024);
                    });
                    return fileInfo;
                });

                return task;
            }
            else
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotAcceptable, "This request is not properly formatted"));
            }
        }

    }
}
#region comments
/*
public async Task<List<string>> PostMultipartStream()
   4:     {
   5:         // Verify that this is an HTML Form file upload request
   6:         if (!Request.Content.IsMimeMultipartContent("form-data"))
   7:         {
   8:             throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
   9:         }
  10:  
  11:         // Create a stream provider for setting up output streams that saves the output under c:\tmp\uploads
  12:         // If you want full control over how the stream is saved then derive from MultipartFormDataStreamProvider
  13:         // and override what you need.
  14:         MultipartFormDataStreamProvider streamProvider = new MultipartFormDataStreamProvider("c:\\tmp\\uploads");
  15:  
  16:         // Read the MIME multipart content using the stream provider we just created.
  17:         IEnumerable<HttpContent> bodyparts = await Request.Content.ReadAsMultipartAsync(streamProvider);
  18:  
  19:         // The submitter field is the entity with a Content-Disposition header field with a "name" parameter with value "submitter"
  20:         string submitter;
  21:         if (!bodyparts.TryGetFormFieldValue("submitter", out submitter))
  22:         {
  23:             submitter = "unknown";
  24:         }
  25:  
  26:         // Get a dictionary of local file names from stream provider.
  27:         // The filename parameters provided in Content-Disposition header fields are the keys.
  28:         // The local file names where the files are stored are the values.
  29:         IDictionary<string, string> bodyPartFileNames = streamProvider.BodyPartFileNames;
  30:  
  31:         // Create response containing information about the stored files.
  32:         List<string> result = new List<string>();
  33:         result.Add(submitter);
  34:  
  35:         IEnumerable<string> localFiles = bodyPartFileNames.Select(kv => kv.Value);
  36:         result.AddRange(localFiles);
  37:  
  38:         return result;
  39:     }
 */
#endregion