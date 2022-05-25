using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;

public class VideoAnalysis
{
    private static S3Object GetVideo(string fileName)
    {
        S3Object video = new S3Object()
        {
            Bucket = "module24videos",
            Name = fileName
        };

        return video;
    }

    static void Main(String[] args)
    {
        const float MIN_CONFIDENCE = 90F;

        AWSCredentials creds = GetAWSCredentialsByName("cs455Colin");
        AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient(creds, RegionEndpoint.USEast1);

        string videoFileName = "StudentsInCoffeeShop.mp4";
        Video video = new Video()
        {
            S3Object = GetVideo(videoFileName),
        };

        StartLabelDetectionRequest startLabelDetectionRequest = new StartLabelDetectionRequest();
        startLabelDetectionRequest.Video = video;
        startLabelDetectionRequest.MinConfidence = MIN_CONFIDENCE;

        var startlabelAsync = rekognitionClient.StartLabelDetectionAsync(startLabelDetectionRequest);
        
        StartLabelDetectionResponse labelResponse = new StartLabelDetectionResponse();
        labelResponse.JobId = startlabelAsync.Result.JobId;

        GetLabelDetectionRequest getLabelDetectionRequest = new GetLabelDetectionRequest();
        getLabelDetectionRequest.JobId = labelResponse.JobId;

        while (true)
        {
            var task = rekognitionClient.GetLabelDetectionAsync(getLabelDetectionRequest);
            task.Wait();

            GetLabelDetectionResponse getLabelDetectionResponse = task.Result;

            if (getLabelDetectionResponse.JobStatus == VideoJobStatus.SUCCEEDED)
            {
                
                foreach(var labels in getLabelDetectionResponse.Labels)
                {
                    Console.WriteLine("{0}: {1}", labels.Label.Name, labels.Label.Confidence);
                }

                break;

            } else if (getLabelDetectionResponse.JobStatus == VideoJobStatus.FAILED)
            {
                break;

            } else if (getLabelDetectionResponse.JobStatus == VideoJobStatus.IN_PROGRESS)
            {
                Console.WriteLine("Analysis in progress. Will check job status in 5 seconds...");
                System.Threading.Thread.Sleep(5000);
            }
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