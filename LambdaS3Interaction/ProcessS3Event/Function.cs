using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.S3;
using System.Xml;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Amazon.S3.Model;
using Amazon.S3.Util;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ProcessS3Event;

public class Function
{
    IAmazonS3 S3Client { get; set; }

    /// <summary>
    /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
    /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
    /// region the Lambda function is executed in.
    /// </summary>
    public Function()
    {
        S3Client = new AmazonS3Client();
    }

    /// <summary>
    /// Constructs an instance with a preconfigured S3 client. This can be used for testing the outside of the Lambda environment.
    /// </summary>
    /// <param name="s3Client"></param>
    public Function(IAmazonS3 s3Client)
    {
        this.S3Client = s3Client;
    }
    
    /// <summary>
    /// This method is called for every Lambda invocation. This method takes in an S3 event object and can be used 
    /// to respond to S3 notifications.
    /// </summary>
    /// <param name="evnt"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<string?> FunctionHandler(S3Event evnt, ILambdaContext context)
    {
        var s3Event = evnt.Records?[0].S3;

        if(s3Event == null)
        {
            return null;
        }

        try
        {

            string bucketName = s3Event.Bucket.Name;
            string objectKey = s3Event.Object.Key;

            Stream stream = await S3Client.GetObjectStreamAsync(bucketName, objectKey, null);
            using (StreamReader reader = new StreamReader(stream))
            {
                string content = reader.ReadToEnd();
                Movie movie = JsonSerializer.Deserialize<Movie>(content);

                if (movie != null)
                {
                    Console.WriteLine("Item: {0}", movie.Title);
                    Console.WriteLine("Item: {0}", movie.Cast);
                    Console.WriteLine("Item: {0}", movie.Year);
                    Console.WriteLine("Item: {0}", movie.Budget);
                    Console.WriteLine("Item: {0}", movie.MusicBy);
                }


                //XmlDocument doc = new XmlDocument();
                //doc.LoadXml(content);

                //XmlElement root = doc.DocumentElement;
                //foreach (XmlElement element in doc.SelectNodes("//*")) {
                //    Console.WriteLine("{0}: {1}", element.Name, element.InnerText);
                //}

                reader.Close();
            }

            //XmlDocument document = new XmlDocument();
            //document.LoadXml(@"Data/Good.xml");
            //Console.WriteLine("hello Colin");
            //Console.WriteLine("Bucket: {0}", s3Event.Bucket.Name);
            //Console.WriteLine("File: {0}", s3Event.Object.Key);
            //return stream;
            return "";
        }
        catch(XmlException e)
        {
            //context.Logger.LogInformation($"Error getting object {s3Event.Object.Key} from bucket {s3Event.Bucket.Name}. Make sure they exist and your bucket is in the same region as this function.");
            Console.WriteLine("failed cosun zhou");
            context.Logger.LogInformation(e.Message);
            context.Logger.LogInformation(e.StackTrace);
            throw;
        }
    }
}