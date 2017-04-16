using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starcounter;
using AdfenixFranchise.ViewModels;
using AdfenixFranchise.Domain;

namespace AdfenixFranchise.Handlers
{
    public static class AdfenixFranchiseHandlers
    {
        public static void RegisterAdfenixFranchiseHandlers()
        {
            Handle.GET("/AdfenixFranchise/master", () =>
            {
                return Db.Scope(() => 
                {
                    Session session = Session.Current;
                    if (session != null && session.Data != null)
                        return session.Data;

                    if (session == null)
                    {
                        session = new Session(SessionOptions.PatchVersioning);
                    }

                    var indexPage = new IndexPageJson();
                    var corporations = Db.SQL<Corporation>("Select c from Corporation c");

                    if (corporations != null)
                    {
                        indexPage.Data = corporations;
                    }

                    var masterPage = new MasterPageJson();
                    masterPage.CurrentPage = indexPage;
                    masterPage.Session = session;
                    return masterPage;
                });                
            });

            Handle.GET("/AdfenixFranchise/partial/index", () => new IndexPageJson());
            Handle.GET("/AdfenixFranchise", () => 
            {
                return Db.Scope(() => 
                {
                    var corporations = Db.SQL<Corporation>($"select c from Corporation c");
                    return WrapPage<IndexPageJson>("/AdfenixFranchise/partial/index" , corporations);
                });
            });

            Handle.GET("/AdfenixFranchise/partial/franchisehome", () => new FranchiseHomeJson());
            Handle.GET("/AdfenixFranchise/franchisehome/{?}", (string id) =>
            {
                return Db.Scope(() =>
                {
                    var data = DbHelper.FromID(DbHelper.Base64DecodeObjectID(id));
                    return WrapPage<FranchiseHomeJson>("/AdfenixFranchise/partial/franchisehome", data);
                });
            });

            Handle.GET("/AdfenixFranchise/partial/franchiseoffice", () => new UpdateFranchiseOfficePageJson());
            Handle.GET("/AdfenixFranchise/franchiseoffice/{?}", (string id) =>
            {
                return Db.Scope(() =>
                {
                    var data = DbHelper.FromID(DbHelper.Base64DecodeObjectID(id));
                    return WrapPage<UpdateFranchiseOfficePageJson>("/AdfenixFranchise/partial/franchiseoffice", data);
                });
            });

        }

        private static Json WrapPage<T>(string partialPath, object data = null) where T : Json
        {
            var masterPage = (MasterPageJson)Self.GET("/AdfenixFranchise/master");

            if (masterPage.CurrentPage == null || masterPage.CurrentPage.GetType() != typeof(T))
            {
                masterPage.CurrentPage = Self.GET(partialPath);
            }

            if (data != null)
            {
                masterPage.CurrentPage.Data = data;
            }
            return masterPage;
        }
    }
}
