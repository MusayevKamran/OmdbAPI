using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OmdbApi.Configurations
{
    public class EmailConfiguration
    {
        public string From { get; set; }
        public string Password { get; set; }
        public string SenderName { get; set; }
    }
}
