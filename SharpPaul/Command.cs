#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
#endregion

namespace SharpPaul
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,ref string message,ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            void PurgeViews()
            {
                Document curDoc = uidoc.Document;
                int viewCount = 0;
                int sheetCount = 0;
                int schedCount = 0;
                //int RevitCount = 0;
                //int FamilyCount = 0;

                TaskDialog purgeViewsDialog = new TaskDialog("Purge Views");
                purgeViewsDialog.MainInstruction = "Purge Views";
                purgeViewsDialog.MainContent = "This macro will delete all schedules, sheets, and views except the current view from this model. Do you want to continue?";
                purgeViewsDialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink1, "Yes");
                purgeViewsDialog.AddCommandLink(TaskDialogCommandLinkId.CommandLink2, "No");

                TaskDialogResult tResult = purgeViewsDialog.Show();

                if (TaskDialogResult.CommandLink1 == tResult)
                {
                    //delete views
                    viewCount = Purge.DeleteAllViews(curDoc);

                    //delete sheets
                    sheetCount = Purge.DeleteAllSheets(curDoc);

                    //delete schedules
                    schedCount = Purge.DeleteAllSchedules(curDoc);

                    //delete revitlinks
                    //RevitCount = Purge.DeleteAllRevitLinks(curDoc);

                    //delete family
                    //FamilyCount = Purge.DeleteAllFamilies(curDoc);

                    //alert user
                    TaskDialog.Show("Complete", "Deleted " + viewCount + " views, "/* + RevitCount + " revit links, "*/ + schedCount + " schedules, and " + sheetCount + " sheets from the current model.");

                }
                else
                {
                    return;
                }
            }

            // Modify document within a transaction
            PurgeViews();

            using (Transaction tx = new Transaction(doc))
            {
                tx.Start("Transaction Name");
                tx.Commit();
            }

            return Result.Succeeded;
        }
    }
}