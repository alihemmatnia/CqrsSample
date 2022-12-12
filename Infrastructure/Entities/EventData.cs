using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public  class EventData
    {
        public Guid Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Response { get; set; }
    }
}
