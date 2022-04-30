using System;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;

public class S3ClientApp
{
    static void Main(string[] args)
    {
        AWSCredentials creds = GetAWSCredentialsByName("cs455Colin");
        AmazonS3Client s3Client = new AmazonS3Client(creds, RegionEndpoint.USEast1);
        
        Task<PutBucketResponse> putBucket = s3Client.PutBucketAsync("bucket1aaa"); //create new bucket

        Task<ListBucketsResponse> listBucketsTask = s3Client.ListBucketsAsync();

        ListObjectsV2Request request = new ListObjectsV2Request();

        GetObjectTaggingRequest tagRequest = new GetObjectTaggingRequest();

        if (listBucketsTask != null)
        {
            foreach (var bucket in listBucketsTask.Result.Buckets)
            {
                Console.WriteLine("Bucket: {0}", bucket.BucketName);
                request.BucketName = bucket.BucketName;

                Task<ListObjectsV2Response> numObjInBucket = s3Client.ListObjectsV2Async(request);

                if (numObjInBucket != null)
                {
                    foreach (var ob in numObjInBucket.Result.S3Objects)
                    {
                        Console.WriteLine("Object: {0}", ob.Key);

                        tagRequest.BucketName = bucket.BucketName;
                        tagRequest.Key = ob.Key;
                        //Task<GetObjectTaggingResponse> objTags = s3Client.GetObjectTaggingAsync(tagRequest);

                        //if (objTags != null)
                        //{
                        //    foreach (var tag in objTags.Result.Tagging)
                        //    {
                        //        Console.WriteLine(tag.Key);
                        //        Console.WriteLine(tag.Value);
                        //    }
                        //}
                    }
                }

                //Console.WriteLine("tags: ", objTags.Result);

            }
        }

        //if (numBuckets != null)
        //{
        //    foreach (var bucket in numBuckets.Result.S3Objects)
        //    {
        //        Console.WriteLine("Bucket: {0}", bucket.BucketName);
        //    }
        //}

        //if (listBucketsTask != null)
        //{
        //    foreach (S3Bucket bucket in listBucketsTask.Result.Buckets)
        //    {
        //        Console.WriteLine("Bucket: {0}", bucket.BucketName);
        //    }
        //}


        s3Client.Dispose();
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
