using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastracture.Specifications.BusSpecification
{
    public class BusSpecification
    {
        public int? OwnerId { get; set; }
        public int? DriverId { get; set; }

        private string? _Search;
        public string? Search
        {
            get => _Search;
            set => _Search = value?.Trim().ToLower();
        }
    }
}
