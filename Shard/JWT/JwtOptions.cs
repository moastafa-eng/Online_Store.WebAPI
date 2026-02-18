using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.JWT
{
    public class JwtOptions
    {
        public string Issure { get; set; }
        public string Audience { get; set; }
        public string SecurityKey { get; set; }
        public string DurationInDayes { get; set; }
    }
}
