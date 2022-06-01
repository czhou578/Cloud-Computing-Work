using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.Textract;
using Amazon.Textract.Model;

public class Textract
{
    static void Main(string[] args)
    {
        AWSCredentials credentials = GetAWSCredentialsByName("cs455Colin");
        AmazonTextractClient client = new AmazonTextractClient(credentials, Amazon.RegionEndpoint.USEast1);
        string filePath1 = "C:\\Users\\mycol\\WEB PROJECTS\\CS-455-Work\\Module25\\Documents\\HandWrittenDocument.png";
        string filePath2 = "C:\\Users\\mycol\\WEB PROJECTS\\CS-455-Work\\Module25\\Documents\\LicensePlate1.jpg";
        byte[] docbytes = FileToByteArray(filePath2);
        Document document1 = new Document();
        document1.Bytes = new MemoryStream(docbytes);

        DetectDocumentTextRequest detectDocumentTextRequest = new DetectDocumentTextRequest();
        detectDocumentTextRequest.Document = document1;

        Task<DetectDocumentTextResponse> response = client.DetectDocumentTextAsync(detectDocumentTextRequest, new System.Threading.CancellationToken());

        while (true)
        {
            if (response.Status == TaskStatus.RanToCompletion)
            {
                foreach (Block b in response.Result.Blocks)
                {
                    Console.WriteLine(b.Text);
                }

                break;
            }

            Thread.Sleep(1000);
        }
        
    }

    private static byte[] FileToByteArray(string filePath)
    {
        byte[] byteArray = null;

        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            using (BinaryReader br = new BinaryReader(fs))
            {
                byteArray = br.ReadBytes((int)(new FileInfo(filePath).Length));
            }
        }

        return byteArray;
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

