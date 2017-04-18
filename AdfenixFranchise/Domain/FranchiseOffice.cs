using System;
using System.Collections.Generic;
using System.Linq;
using Starcounter;

namespace AdfenixFranchise.Domain
{
    [Database]
    public class FranchiseOffice
    {
        public Corporation Corporation { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string Contact { get; set; }
        public long ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public string Key { get {
                return this.GetObjectID();
            } }

        public IEnumerable<Sales> Transactions
        {
            get
            {
                return Db.SQL<Sales>("select e from Sales e where e.FranchiseOffice = ?", this);
            }
        }

        public int Trend { get; set; }

        public long NumberOfHomeSold
        {
            get
            {
                return Db.SQL<long>($"select COUNT(e) from Sales e where e.FranchiseOffice = ?", this).First;
            }
        }

        public decimal AverageCommission
        {
            get
            {
                try
                {
                    return this.Transactions.Average(e => e.Commission);
                }
                catch (Exception exception)
                {
                    return 0;
                }
            }
        }

        public decimal TotalCommission
        {
            get
            {
                return Db.SQL<decimal>($"select sum(e.Commission) from Sales e where e.FranchiseOffice = ?", this).First;
            }
        }
    }
}
