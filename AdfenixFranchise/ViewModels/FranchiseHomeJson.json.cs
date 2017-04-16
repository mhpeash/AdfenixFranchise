using AdfenixFranchise.Domain;
using Starcounter;
using System.Collections.Generic;

namespace AdfenixFranchise.ViewModels
{
    partial class FranchiseHomeJson : Json, IBound<Corporation>
    {
        protected override void OnData()
        {
            base.OnData();
            var data = this.Data;
            this.CompanyName = data.Name;
            var offices = this.Data.FranchiseOffices;
            this.ShowFranchiseOfficces(offices);
        }

        private void ShowFranchiseOfficces(IEnumerable<FranchiseOffice> offices)
        {
            foreach (var franchiseOffice in offices)
            {
                var offObj = franchiseOffice;
                var office = this.FranchiseOffices.Add();
                var objectId = franchiseOffice.GetObjectID();
                office.Link = $"/AdfenixFranchise/franchiseoffice/{objectId}";
                office.Key = objectId;
                office.Name = offObj.Name;
                office.HomesSold = offObj.NumberOfHomeSold;
                office.TotalCommision = offObj.TotalCommission;
                office.AverageCommision = offObj.AverageCommission;
                office.Trend = offObj.Trend;
            }
        }

        private void Handle(Input.CreateFranchiseOfficeTrigger action)
        {
            Db.Scope(() =>
            {
                var newOfficeData = new FranchiseOffice
                {
                    Corporation = this.Data,
                    Name = this.FranchiseName,
                    City = "",
                    Contact = "",
                    Country = "",
                    Street = "",
                    ZipCode = 0
                };
                var newOffice = this.FranchiseOffices.Add();
                newOffice.Name = this.FranchiseName;
                newOffice.Link = "/AdfenixFranchise/franchiseoffice/" + newOfficeData.GetObjectID();
                newOffice.Key = newOfficeData.GetObjectID();
            });
            this.Transaction.Commit();
            this.FranchiseName = "";
        }
    }
}
