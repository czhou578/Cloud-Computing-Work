using System;
using System.IO;
using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;

public class DetectObjects
{
    static void Main(String[] args)
    {
        const float MIN_CONFIDENCE = 90F;
        AWSCredentials creds = GetAWSCredentialsByName("cs455Colin");
        AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient(creds, RegionEndpoint.USEast1);
        string exeDir = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0])!;

        //string photo1 = "C:\\Users\\mycol\\WEB PROJECTS\\CS-455-Work\\AWSRekognitionSolution\\DetectObjectsInImage\\Images\\CoupleWithDog.jpg";
        string photo1 = $"{exeDir}\\Image\\CoupleWithDog.jpg";

        Image image = new Image();

        using (FileStream fs = new FileStream(photo1, FileMode.Open, FileAccess.Read))
        {
            byte[] data = new byte[fs.Length];
            fs.Read(data, 0, (int)fs.Length);
            image.Bytes = new MemoryStream(data);
            fs.Close();
        }

        DetectLabelsRequest detectLabelsRequest = new DetectLabelsRequest();
        detectLabelsRequest.Image = image;

        var result = rekognitionClient.DetectLabelsAsync(detectLabelsRequest);
        DetectLabelsResponse response = result.Result;

        foreach(var label in response.Labels)
        {
            Console.WriteLine("{0}: ({1})", label.Name, label.Confidence);

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