using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.EventBridge.Model;
using System.Text.Json;

namespace EventBridge
{
    public class Messages
    {
        public static PutEventsRequest transactionEvents = new PutEventsRequest
        {
            Entries =
             {
                new PutEventsRequestEntry
                {
                    Source = "custom.myATMapp",
                    EventBusName = "arn:aws:events:us-east-1:584857604010:event-bus/default",
                    DetailType = "transaction",
                    Time = DateTime.Now,
                    Detail = JsonSerializer.Serialize(
                        new
                        {
                            action = "withdrawal",
                            location = "MA-BOS-01",
                            amount = 500,
                            result = "approved",
                            transactionId = "123456",
                            cardPresent = true,
                            partnerBank = "Example Bank",
                            remainingFunds = 723.34
                        }
                    )
                },
            }
        };
    }
}
