using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using System.Linq;

namespace SharpPaul
{
        public static class Purge
        {
            public static int DeleteAllViews(Document curDoc)
            {
                int counter = 0;

                //get all views
                List<View> viewList = CollectorsAll.getAllViews(curDoc);

                //create transaction
                using (Transaction curTrans = new Transaction(curDoc, "Delete All Views"))
                {
                    if (curTrans.Start() == TransactionStatus.Started)
                    {
                        //loop through views - if view is not current then delete
                        try
                        {
                            foreach (View curView in viewList)
                            {
                                if (curDoc.ActiveView.Id.Compare(curView.Id) != 0)
                                {

                                    try
                                    {
                                        //delete view
                                        curDoc.Delete(curView.Id);

                                        //incremenet counter
                                        counter = counter + 1;
                                    }
                                    catch (Exception)
                                    {
                                        System.Diagnostics.Debug.Print("Could not delete view");
                                    }

                                }
                            }

                        }
                        catch (Exception)
                        {
                            System.Diagnostics.Debug.Print("error");

                        }

                    }

                    //commit changes
                    curTrans.Commit();
                    curTrans.Dispose();
                }

                //return counter 
                return counter;

            }

            public static int DeleteAllSheets(Document curDoc)
            {
                int counter = 0;

                //get all sheets
                List<ViewSheet> viewList = CollectorsAll.getAllSheets(curDoc);

                //create transaction
                using (Transaction curTrans = new Transaction(curDoc, "Delete All Sheets"))
                {
                    if (curTrans.Start() == TransactionStatus.Started)
                    {
                        //loop through sheets - if view is not current then delete
                        foreach (View curView in viewList)
                        {
                            if (curDoc.ActiveView.Id.Compare(curView.Id) != 0)
                            {
                                try
                                {
                                    //delete view
                                    curDoc.Delete(curView.Id);

                                    //incremenet counter
                                    counter = counter + 1;
                                }
                                catch (Exception)
                                {
                                    System.Diagnostics.Debug.Print("Could not delete sheet");

                                }

                            }
                        }

                    }

                    //commit changes
                    curTrans.Commit();
                    curTrans.Dispose();
                }

                //return counter 
                return counter;

            }

            public static int DeleteAllSchedules(Document curDoc)
            {
                int counter = 0;

                //get all schedules
                List<ViewSchedule> viewList = CollectorsAll.getAllSchedules(curDoc);

                //create transaction
                using (Transaction curTrans = new Transaction(curDoc, "Delete All Schedules"))
                {
                    if (curTrans.Start() == TransactionStatus.Started)
                    {
                        //loop through sheets - if view is not current then delete
                        foreach (View curView in viewList)
                        {
                            if (curDoc.ActiveView.Id.Compare(curView.Id) != 0)
                            {
                                try
                                {
                                    //delete view
                                    curDoc.Delete(curView.Id);

                                    //incremenet counter
                                    counter = counter + 1;
                                }
                                catch (Exception)
                                {
                                    System.Diagnostics.Debug.Print("Could not delete schedule");
                                }

                            }
                        }

                    }

                    //commit changes
                    curTrans.Commit();
                    curTrans.Dispose();
                }

                //return counter 
                return counter;

            }

            public static int DeleteAllRevitLinks(Document curDoc)
            {
                int counter = 0;

                //get all Revit Links
                List<RevitLinkInstance> LinkList = CollectorsAll.getRVTLinks(curDoc);

                //create transaction
                using (Transaction curTrans = new Transaction(curDoc, "Delete All Revit Links"))
                {
                    if (curTrans.Start() == TransactionStatus.Started)
                    {
                        //loop through sheets - if view is not current then delete
                        foreach (RevitLinkInstance curLink in LinkList)
                        {
                            if (curDoc.ActiveView.Id.Compare(curLink.Id) != 0)
                            {
                                try
                                {
                                    //delete view
                                    curDoc.Delete(curLink.Id);

                                    //incremenet counter
                                    counter = counter + 1;
                                }
                                catch (Exception)
                                {
                                    System.Diagnostics.Debug.Print("Could not delete Revit link");
                                }

                            }
                        }

                    }

                    //commit changes
                    curTrans.Commit();
                    curTrans.Dispose();
                }

                //return counter 
                return counter;

            }
        }
    }