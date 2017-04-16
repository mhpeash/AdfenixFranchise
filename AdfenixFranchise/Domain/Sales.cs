using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starcounter;

namespace AdfenixFranchise.Domain
{
    [Database]
    public class Sales
    {
        public FranchiseOffice FranchiseOffice { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public int ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public DateTime TransactionDate { get; set; }
        public decimal Price { get; set; }
        public decimal Commission { get; set; }
    }
}
