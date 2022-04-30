using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessS3Event
{
    public class Movie
    {
        public String Title
        {
            get; set;
        }
        public String Year
        {
            get; set;
        }
        public bool WonAward
        {
            get; set;
        }
        public int Budget
        {
            get; set;
        }
        public String MusicBy
        {
            get; set;
        }

        public string[] Cast
        {
            get; set;
        }

    }
}
