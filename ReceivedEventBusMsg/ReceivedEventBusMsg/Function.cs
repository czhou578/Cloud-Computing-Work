using Amazon.Lambda.Core;
using Amazon.Lambda.CloudWatchEvents;
using EventBridge;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ReceivedEventBusMsg;

public class Function
{
    
    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public void FunctionHandler(CloudWatchEvent<Messages> input, ILambdaContext context)
    {
        //var payloadModel = JsonSerializer.Deserialize<Messages>(input);
        Console.WriteLine(input.ToString());
    }
}
