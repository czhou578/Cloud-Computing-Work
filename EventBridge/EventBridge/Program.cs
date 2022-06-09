using System;
using Amazon;
using Amazon.EventBridge;
using Amazon.EventBridge.Model;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using EventBridge;

public class EventBridgeSend
{
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

    static void Main (string[] args)
    {
        AWSCredentials creds = GetAWSCredentialsByName("cs455Colin");
        AmazonEventBridgeClient client = new AmazonEventBridgeClient(creds, Amazon.RegionEndpoint.USEast1);
        PutEventsRequest putEventsRequest = Messages.transactionEvents;
        Task<PutEventsResponse> response = client.PutEventsAsync(putEventsRequest);

        Console.WriteLine(response.Result.HttpStatusCode);

    }
}