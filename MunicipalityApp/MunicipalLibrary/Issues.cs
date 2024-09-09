using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MunicipalLibrary
{
    public class Issues
    {
        public int IssueId { get; set; }
        public string location { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public byte[] ImageData { get; set; }
    }
}
