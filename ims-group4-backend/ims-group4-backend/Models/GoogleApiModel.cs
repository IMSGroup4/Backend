using Google.Apis.Auth.OAuth2;
using Google.Cloud.Vision.V1;
using Grpc.Core;
using System.Collections.Generic;
using System.IO;

namespace ims_group4_backend.Models{

    public class GoogleApiModel {

		public GoogleApiModel() {
			System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "./ims-group4-383411-c583819d8d71.json");
		}
		public async Task<List<EntityAnnotation>> DetectImage(string base64Image)
		{
			var imageBytes = Convert.FromBase64String(base64Image);
			var client = await ImageAnnotatorClient.CreateAsync();
			var response = await client.DetectLabelsAsync(Image.FromBytes(imageBytes));
			return response.ToList();
		}
	}
}