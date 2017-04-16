using AdfenixFranchise.Domain;
using Starcounter;
using System;

namespace AdfenixFranchise.ViewModels
{
    partial class UpdateFranchiseOfficePageJson : Json
    {
        protected override void OnData()
        {
            base.OnData();
            var office = this.Data as FranchiseOffice;

            this.LoadOfficeDataIntoView(office);

            this.LoadTransactionsIntoView(office);
        }

        private void LoadTransactionsIntoView(FranchiseOffice office)
        {
            foreach (var tansaction in office.Transactions)
            {
                var transaction = this.Transactions.Add();
                transaction.Commission = tansaction.Commission;
                transaction.Price = tansaction.Price;
                transaction.SaleDate = tansaction.TransactionDate.ToString();
            }
        }

        private void LoadOfficeDataIntoView(FranchiseOffice office)
        {
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
            Db.Scope(() =>
            {
                var homeSale = new Sales();
                var franchiseOffice = this.Data as FranchiseOffice;
                homeSale.FranchiseOffice = franchiseOffice;
                homeSale.ZipCode = (int)this.HomeForSale.ZipCode;
                homeSale.Street = this.HomeForSale.Street;
                homeSale.Number = this.HomeForSale.Number;
                homeSale.City = this.HomeForSale.City;
                homeSale.Commission = this.HomeForSale.Commission;
                homeSale.Country = this.HomeForSale.Country;
                homeSale.TransactionDate = DateTime.Parse(this.HomeForSale.SaleDate);
                homeSale.Price = this.HomeForSale.Price;

                var t = this.Transactions.Add();
                t.Price = this.HomeForSale.Price;
                t.SaleDate = this.HomeForSale.SaleDate;
                t.Commission = this.HomeForSale.Commission;
            });
            this.Transaction.Commit();
        }
       
    }
}
