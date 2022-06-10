using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using System;
using System.IO;

public class Secrets
{
    static void Main(string[] args)
    {
        string secretName = "TravelApp/Production";
        string region = "us-east-1";
        string secret = "";

        AWSCredentials credentials = GetAWSCredentialsByName("cs455Colin");
        MemoryStream memoryStream = new MemoryStream();

        AmazonSecretsManagerClient secretsManagerClient = new AmazonSecretsManagerClient(credentials, Amazon.RegionEndpoint.USEast1);

        GetSecretValueRequest request = new GetSecretValueRequest();
        request.SecretId = secretName;
        request.VersionStage = "AWSCURRENT"; // VersionStage defaults to AWSCURRENT if unspecified.

        GetSecretValueResponse response = null;

        try
        {
            response = secretsManagerClient.GetSecretValueAsync(request).Result;

        } catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

        if (response.SecretString != null)
        {
            secret = response.SecretString;
            Console.WriteLine("Secret value: {0}", secret);
        } else
        {
            memoryStream = response.SecretBinary;
            StreamReader reader = new StreamReader(memoryStream);
            string decodedBinarySecret = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd()));
            Console.WriteLine(decodedBinarySecret);
        }

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