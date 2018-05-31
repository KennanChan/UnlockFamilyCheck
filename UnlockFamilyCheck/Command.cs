using System;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace UnlockFamilyCheck
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                var app = commandData.Application.Application;
                app.FamilyLoadingIntoDocument -=
                    HWFamilyLibMaster.EventHandler.FamilyMasterEventHandler
                        .ControlledApplication_FamilyLoadingIntoDocument;
                app.DocumentSaving -=
                    HWFamilyLibMaster.EventHandler.FamilyMasterEventHandler.ControlledApplication_DocumentSaving;
                app.DocumentSavingAs -=
                    HWFamilyLibMaster.EventHandler.FamilyMasterEventHandler.ControlledApplication_DocumentSavingAs;
            }
            catch (Exception e)
            {
                TaskDialog.Show("Error", e.ToString());
                return Result.Failed;
            }
            return Result.Succeeded;
        }
    }
}
