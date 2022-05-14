using System;
using Amazon;
using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Amazon.Runtime.CredentialManagement;

public class SMSMessage
{
    private const string departureGateChangedTopicArn = "arn:aws:sns:us-east-1:584857604010:DepartureGateChangedTopic";

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

    static void Main (string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine("Unable to process, try again");
            return;
        }

        var phoneNumber = args[0];

        AWSCredentials creds = GetAWSCredentialsByName ("cs455Colin");
        AmazonSimpleNotificationServiceClient client = new AmazonSimpleNotificationServiceClient(creds, RegionEndpoint.USEast1);
        var subscription = new Subscription
        {
            Protocol = "sms"
        };

        var request = new PublishRequest
        {
            Message = phoneNumber
        };

        try
        {
            var subscribeResponse = client.SubscribeAsync(subscription);
        } catch (Exception ex)
        {
            Console.WriteLine (ex.Message);
        }
        
        //client.SubscribeAsync("hello");
    }   
}
