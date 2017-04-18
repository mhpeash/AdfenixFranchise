using AdfenixFranchise.Domain;
using Starcounter;
using System;

namespace AdfenixFranchise.ViewModels
{
    partial class UpdateFranchiseOfficePageJson : Json, IBound<FranchiseOffice>
    {
        protected override void OnData()
        {
            base.OnData();
            var office = this.Data as FranchiseOffice;
            this.BackUrl = "/AdfenixFranchise/franchisehome/" + office.Corporation.GetObjectID();

            this.Office = new OfficeJson();
            this.Office.City = office.City;
            this.Office.Name = office.Name;
            this.Office.Street = office.Street;
            this.Office.ZipCode = office.ZipCode;
            this.Office.Country = office.Country;
            this.Office.Contact = office.Contact;
        }

        private void Handle(Input.UpdateOfficeTrigger acton)
        {
            Db.Scope(() =>
            {
                var office = this.Data as FranchiseOffice;
                office.Name = this.Office.Name;
                office.Contact = this.Office.Contact;
                office.Country = this.Office.Country;
                office.City = this.Office.City;
                office.ZipCode = this.Office.ZipCode;
                office.Street = this.Office.Street;
            });
            this.Transaction.Commit();
        }

        private void Handle(Input.AddHomeTrigger action)
        {
            Db.Transact(()=> 
            {
                var newHome = new Sales
                {
                    FranchiseOffice = this.Data,
                    ZipCode = (int)this.HomeForSale.ZipCode,
                    Street = this.HomeForSale.Street,
                    Number = this.HomeForSale.Number,
                    City = this.HomeForSale.City,
                    Commission = this.HomeForSale.Commission,
                    Country = this.HomeForSale.Country,
                    SaleDate = DateTime.Parse(this.HomeForSale.SaleDate),
                    Price = this.HomeForSale.Price

                };
            });            
        }
       
    }
}
