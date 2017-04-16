using Starcounter;
using AdfenixFranchise.Handlers;

namespace AdfenixFranchise
{
    class Program
    {
        static void Main()
        {
            Db.Transact(()=> 
            {
                Db.SlowSQL("Delete from Corporation");
                Db.SlowSQL("Delete from Sales");
                Db.SlowSQL("Delete from FranchiseOffice");
            });

            Application.Current.Use(new HtmlFromJsonProvider());
            Application.Current.Use(new PartialToStandaloneHtmlProvider());

            AdfenixFranchiseHandlers.RegisterAdfenixFranchiseHandlers();
        }
    }
}