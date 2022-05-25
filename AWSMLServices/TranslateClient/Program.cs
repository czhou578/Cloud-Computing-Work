using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.Translate;
using Amazon.Translate.Model;

public class Translate
{
    static void Main(string[] args)
    {
            if (args.Length == 0)
            {
                Console.WriteLine("Pass in an argument");
                return;
            }
            string enteredInput = "";

            foreach (string arg in args) {
            enteredInput += arg + " ";    
            }


            AWSCredentials creds = GetAWSCredentialsByName("cs455Colin");
            AmazonTranslateClient translateClient = new AmazonTranslateClient(creds, RegionEndpoint.USEast1);
            TranslateTextRequest textRequest = new TranslateTextRequest
            {
                SourceLanguageCode = "en",
                TargetLanguageCode = "da",
                Text = enteredInput
            };

            TranslateTextRequest textRequest2 = new TranslateTextRequest
            {
                SourceLanguageCode = "en",
                TargetLanguageCode = "fr",
                Text = enteredInput
            };

            TranslateTextRequest textRequest3 = new TranslateTextRequest
            {
                SourceLanguageCode = "en",
                TargetLanguageCode = "it",
                Text = enteredInput
            };

            TranslateTextRequest textRequest4 = new TranslateTextRequest
            {
                SourceLanguageCode = "en",
                TargetLanguageCode = "es",
                Text = enteredInput
            };

            var response = translateClient.TranslateTextAsync(textRequest);
            var response2 = translateClient.TranslateTextAsync(textRequest2);
            var response3 = translateClient.TranslateTextAsync(textRequest3);
            var response4 = translateClient.TranslateTextAsync(textRequest4);

            Console.WriteLine(response.Result.TranslatedText.ToString());
            Console.WriteLine(response2.Result.TranslatedText.ToString());
            Console.WriteLine(response3.Result.TranslatedText.ToString());
            Console.WriteLine(response4.Result.TranslatedText.ToString());
        //while (args[0] != "Q" || args[0] != "q")
        //{
        //}


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