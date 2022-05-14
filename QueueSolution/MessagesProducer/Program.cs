using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.SQS;
using Amazon.SQS.Model;

public class Producer
{
    static void Main(string[] args)
    {
        AWSCredentials credentials = GetAWSCredentialsByName("cs455Colin");
        string message = "this is colin, who likes planes";
        string queueUrl = "https://sqs.us-east-1.amazonaws.com/584857604010/InputQueue";
        //Console.WriteLine(queueUrl);

        SendMessageRequest request = new SendMessageRequest
        {
            QueueUrl = queueUrl,
            MessageBody = message
        };

        AmazonSQSClient sqsClient = new AmazonSQSClient(credentials, RegionEndpoint.USEast1);
        sqsClient.SendMessageAsync(request).Wait(); 

        //SendMessageResponse response = sqsClient.SendMessageAsync(request).Result;

        Console.WriteLine("Message successfully sent to queue.");
    }

    private static AWSCredentials GetAWSCredentialsByName(string profileName) //save this one
    {
        if (String.IsNullOrEmpty(profileName))
        {
            throw new ArgumentNullException("profileName cannot be null or empty");
        }

        SharedCredentialsFile credential = new SharedCredentialsFile();
        CredentialProfile profile = credential.ListProfiles().Find(p => p.Name.Equals(profileName));

        if (profileName == null)
        {
            throw new Exception(String.Format("Profile named {0} not found", profileName));
        }

        return AWSCredentialsFactory.GetAWSCredentials(profile, new SharedCredentialsFile());
    }
}