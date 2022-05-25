using System;
using System.IO;
using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;

public class Recognize
{

    static void Main(string[] args)
    {
        try
        {
            AWSCredentials creds = GetAWSCredentialsByName("cs455Colin");
            AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient(creds, RegionEndpoint.USEast1);
            DetectTextRequest detectTextRequest = new DetectTextRequest();
            
            string photo2 = "C:\\Users\\mycol\\WEB PROJECTS\\CS-455-Work\\Module22\\BioConvention.png";
            Image image = new Image();
            string photo1 = "C:\\Users\\mycol\\WEB PROJECTS\\CS-455-Work\\Module22\\PetroleumConference.png";

            using (FileStream fs = new FileStream(photo1, FileMode.Open, FileAccess.Read))
            {
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, (int)fs.Length);
                image.Bytes = new MemoryStream(data);
                fs.Close();
            }

            detectTextRequest.Image = image;

            var response = rekognitionClient.DetectTextAsync(detectTextRequest);
            DetectTextResponse responses = response.Result;
            foreach(var data in responses.TextDetections)
            {
                Console.WriteLine(data.DetectedText);
            }

        } catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    private static AWSCredentials GetAWSCredentialsByName(string profileName)
    {
        if (String.IsNullOrEmpty(profileName))
        {
            throw new ArgumentNullException("profileName cannot be null or empty");
        }

        SharedCredentialsFile credential = new SharedCredentialsFile();
        CredentialProfile? profile = credential.ListProfiles().Find(p => p.Name.Equals(profileName));
        if (profile == null)
        {
            throw new Exception("profile not found");
        }

        if (profileName == null)
        {
            throw new Exception(String.Format("Profile named {0} not found", profileName));
        }

        return AWSCredentialsFactory.GetAWSCredentials(profile, new SharedCredentialsFile());
    }

}