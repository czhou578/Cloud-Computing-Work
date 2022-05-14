using System;
using System.Collections.Generic;
using System.Text;
using Amazon.DynamoDBv2.DataModel;

namespace DynamoDBClient
{
    [DynamoDBTable("Music")]
    public class SongItem
    {
        [DynamoDBHashKey]
        public string Artist { get; set; }
        public string SongTitle { get; set; }
        public string Year { get; set; }
        public List<string> Awards { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Artist: " + Artist);
            sb.AppendLine("SongTitle: " + SongTitle);

            if (!String.IsNullOrEmpty(Year))
            {
                sb.AppendLine("Year: " + Year);
            }

            if (Awards != null && Awards.Count > 0)
            {
                sb.AppendLine("Awards: " + String.Join(",", Awards));
            }
            return sb.ToString();
        }
    }
}
