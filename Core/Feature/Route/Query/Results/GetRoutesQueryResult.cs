using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Feature.Route.Query.Results
{
    public  class GetRoutesQueryResult
    {
        public string Name  { get; set; }
        public double Distance { get; set; }
        public string StartStation {  get; set; }
        public string EndStation {  get; set; }
    }
}
