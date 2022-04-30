using Amazon.Lambda.Core;
using Npgsql;
using System.Data;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace PostgreSQLFunction;

public class Function
{
    
    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public string FunctionHandler(string input, ILambdaContext context)
    {
        try
        {
            NpgsqlConnection conn = OpenConnection();
            if (conn.State == ConnectionState.Open)
            {
                Console.WriteLine("Successful connection to database");

                conn.Close();
                conn.Dispose();
            } else
            {
                Console.WriteLine("Failed to connect. Connection State: {0}",
                    Enum.GetName(typeof(ConnectionState), conn.State));
            }
        } catch (NpgsqlException ex)
        {
            Console.WriteLine("Npgsql Error: {0}", ex.Message);
        } catch (Exception ex)
        {
            Console.WriteLine("Error: {0}", ex.Message);
        }

        return String.Empty;
    }

    private NpgsqlConnection OpenConnection()
    {
        string endpoint = "mod12pginstance.c1sghkrjn4qh.us-east-1.rds.amazonaws.com";

        string connString = "Server=" + endpoint + ";" + "port=5432;"
                            + "Database=SalesDB;" + "User ID=postgres;"
                            + "password=cs455pass;" + "Timeout=15";
    
        NpgsqlConnection conn = new NpgsqlConnection(connString);
        conn.Open();
        return conn;
    }
}
