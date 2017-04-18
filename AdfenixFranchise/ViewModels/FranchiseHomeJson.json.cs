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

        }

        private void Handle(Input.CreateFranchiseOfficeTrigger action)
        {
            Db.Transact(() =>
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
            });
        }
    }
}
