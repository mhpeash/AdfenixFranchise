using System;
using System.Collections.Generic;
using Starcounter;

namespace AdfenixFranchise.Domain
{
    [Database]
    public class Corporation
    {
        public string Name { get; set; }
        public IEnumerable<FranchiseOffice> FranchiseOffices
        {
            get
            {
                return Db.SQL<FranchiseOffice>($"select f from FranchiseOffice f where f.Corporation = ?", this);
            }
        }
    }
}
