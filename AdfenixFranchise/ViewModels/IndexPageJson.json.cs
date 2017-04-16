using System;
using System.Collections.Generic;
using AdfenixFranchise.Domain;
using Starcounter;

namespace AdfenixFranchise.ViewModels
{
    partial class IndexPageJson : Json
    {
        protected override void OnData()
        {
            base.OnData();

            var corporations = this.Data as IEnumerable<Corporation>;
            this.Corporations.Clear();

            foreach (var corporation in corporations)
            {
                var c = this.Corporations.Add();
                c.Name = corporation.Name;
                c.Link = "/AdfenixFranchise/franchisehome/" + corporation.GetObjectID();
            }
        }

        private void Handle(Input.CreateCorporotationTrigger action)
        {
            Db.Scope(() => 
            {
                var corp = new Corporation
                {
                    Name = this.CorporationName
                };

                var corpJson = this.Corporations.Add();
                corpJson.Name = this.CorporationName;
                corpJson.Link = "/AdfenixFranchise/franchisehome/" + corp.GetObjectID();
            });
            this.Transaction.Commit();
            this.CorporationName = "";
        }        
    }
}
