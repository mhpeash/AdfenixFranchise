using Starcounter;
using AdfenixFranchise.Handlers;

namespace AdfenixFranchise
{
    class Program
    {
        static void Main()
        {
            Application.Current.Use(new HtmlFromJsonProvider());
            Application.Current.Use(new PartialToStandaloneHtmlProvider());

            AdfenixFranchiseHandlers.RegisterAdfenixFranchiseHandlers();
        }
    }
}