// See https://aka.ms/new-console-template for more information

using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Collections.Generic;
using System.Text;
using DynamoDBClient;

public class AmazonDynamoDBv2Client
{
    private static void ListTables(AmazonDynamoDBClient client)
    {
        var response = client.ListTablesAsync();
        var result = response.Result;

        foreach (var table in result.TableNames)
        {
            Console.WriteLine(table);
        }
    }

    private static void GetItemsByArtist(DynamoDBContext context, string artist)
    {
        var response = context.FromQueryAsync<SongItem>(new Amazon.DynamoDBv2.DocumentModel.QueryOperationConfig()
        {
            Filter = new Amazon.DynamoDBv2.DocumentModel.QueryFilter("Artist", Amazon.DynamoDBv2.DocumentModel.QueryOperator.Equal, artist)
        });

        var searchResponse = response.GetRemainingAsync();

        foreach (var result in searchResponse.Result)
        {
            Console.WriteLine("Item: {0}", result);
        }

    }

    private static void SaveItem(DynamoDBContext context, SongItem newItem)
    {
        context.SaveAsync(newItem);
    }

    static void Main(string[] args)
    {
        AWSCredentials creds = GetAWSCredentialsByName("cs455Colin");

        AmazonDynamoDBClient client = new AmazonDynamoDBClient(creds, RegionEndpoint.USEast1);
        DynamoDBContext context = new DynamoDBContext(client);


        ListTables(client);
        GetItemsByArtist(context, "No One You Know");

        SongItem newSong = new SongItem();
        newSong.Artist = "Elton";
        newSong.SongTitle = "Hello";
        newSong.Year = "1995";
        newSong.Awards = new List<string>() { "best soundtrack" };

        SaveItem(context, newSong);
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