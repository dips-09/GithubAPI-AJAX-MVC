using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Homework7.Models
{
    public class Commit
    {
        public string sha { get; set; }
        //public string timestamp { get; set; }
        public committer committer { get; set; }
        public string message { get; set; }
    }

    public class committer
    {
        public string name { get; set; }
        public string email { get; set; }
        public string date { get; set; }
    }
}
