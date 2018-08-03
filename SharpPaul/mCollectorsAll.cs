using System;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using System.Linq;

namespace SharpPaul
{
    public static class CollectorsAll
    {

        //----------------Sheets and views  --------------------------

        //returns a list of all the sheets in the current model file	
        public static List<ViewSheet> getAllSheets(Document curDoc)
        {
            //get all views
            FilteredElementCollector curCollector = new FilteredElementCollector(curDoc);
            curCollector.OfCategory(BuiltInCategory.OST_Sheets);

            List<ViewSheet> curSheets = new List<ViewSheet>();
            foreach (ViewSheet x in curCollector)
            {
                curSheets.Add(x);
            }

            return curSheets;
        }

        //returns a list of all the titleblocks in the current model file
        public static List<Element> getAllTitleblocks(Document curDoc)
        {
            //get all titleblocks
            FilteredElementCollector curCollector = new FilteredElementCollector(curDoc);
            curCollector.WhereElementIsElementType();
            curCollector.OfCategory(BuiltInCategory.OST_TitleBlocks);

            List<Element> curTblocks = new List<Element>();
            foreach (Element curTB in curTblocks)
            {
                curTblocks.Add(curTB);
            }

            return curTblocks;

        }

        //returns a list of all the views in the current model file
        public static List<View> getAllViews(Document curDoc)
        {
            //get all views
            FilteredElementCollector colViews = new FilteredElementCollector(curDoc);
            colViews.OfCategory(BuiltInCategory.OST_Views);

            List<View> curViews = new List<View>();
            foreach (View x in colViews)
            {
                if (x.IsTemplate == false)
                {
                    curViews.Add(x);
                }
            }

            return curViews;
        }

        //returns a list of all the schedules in the current model file
        public static List<ViewSchedule> getAllSchedules(Document curDoc)
        {
            List<ViewSchedule> schedList = new List<ViewSchedule>();

            FilteredElementCollector curCollector = new FilteredElementCollector(curDoc);
            curCollector.OfClass(typeof(ViewSchedule));

            //loop through views and check if schedule - if so then put into schedule list
            foreach (ViewSchedule curView in curCollector)
            {
                if (curView.ViewType == ViewType.Schedule)
                {
                    schedList.Add((ViewSchedule)curView);
                }
            }

            return schedList;
        }

        //return a list of view templates in the current model file
        public static List<string> getAllViewTemplates(Document curDoc)
        {
            //returns list of view templates 
            List<string> viewTempList = new List<string>();
            List<View> viewList = null;
            viewList = getAllViews(curDoc);

            //loop through views and check if is view template
            foreach (View v in viewList)
            {
                if (v.IsTemplate == true)
                {
                    //add view template to list
                    viewTempList.Add(v.Name);
                }
            }

            return viewTempList;
        }

        //return a list of all the levels in the current model file
        public static List<Level> getAllLevels(Document curDoc)
        {
            FilteredElementCollector colLevels = new FilteredElementCollector(curDoc);
            colLevels.OfCategory(BuiltInCategory.OST_Levels);

            List<Level> curLevels = new List<Level>();
            foreach (Level x in colLevels)
            {
                if (x is Level)
                {
                    curLevels.Add(x);
                }
            }

            return curLevels;

            //order list by elevation
            //curLevels = (from l in curLevelsorderby l.Elevation).tolist();

        }

        //returns a list of all the view types in the current model
        public static List<ViewFamilyType> getAllViewTypes(Document curDoc)
        {
            //get list of view types
            FilteredElementCollector colVT = new FilteredElementCollector(curDoc);
            colVT.OfClass(typeof(ViewFamilyType));

            List<ViewFamilyType> curVTList = new List<ViewFamilyType>();
            foreach (ViewFamilyType x in colVT)
            {
                curVTList.Add(x);
            }

            return curVTList;
        }

        //return list of all view plans (includes AreaPlans, CeilingPlans, and FloorPlans)
        public static List<ViewPlan> getAllViewPlans(Document curDoc)
        {
            //get all views
            List<View> viewList = getAllViews(curDoc);

            //create list for views
            List<ViewPlan> curViews = new List<ViewPlan>();

            //loop through views and check for floor plan views
            foreach (View x in viewList)
            {
                if (x.GetType() == typeof(ViewPlan))
                {
                    if (x.IsTemplate == false)
                    {
                        //add view to list
                        curViews.Add((ViewPlan)x);
                    }
                }
            }

            return curViews;
        }

        //return list of all floor plan views
        public static List<ViewPlan> getAllFloorPlans(Document curDoc)
        {
            //get all views
            List<ViewPlan> viewList = getAllViewPlans(curDoc);

            //create list for views
            List<ViewPlan> curViews = new List<ViewPlan>();

            //loop through views and check for floor plan views
            foreach (View x in viewList)
            {
                if (x.ViewType == ViewType.FloorPlan)
                {
                    //add view to list
                    curViews.Add((ViewPlan)x);
                }
            }

            return curViews;
        }

        //return list of all area plan views
        public static List<ViewPlan> getAllAreaPlans(Document curDoc)
        {
            //get all views
            List<ViewPlan> viewList = getAllViewPlans(curDoc);

            //create list for views
            List<ViewPlan> curViews = new List<ViewPlan>();

            //loop through views and check for area plan views
            foreach (View x in viewList)
            {
                if (x.ViewType == ViewType.AreaPlan)
                {
                    //add view to list
                    curViews.Add((ViewPlan)x);
                }
            }

            return curViews;
        }

        //return a list of all the 3D views in the current model
        public static List<View3D> getAll3DViews(Document curDoc)
        {
            //get all views
            List<View> viewList = getAllViews(curDoc);

            //loop through views and check if 3D
            List<View3D> view3DList = new List<View3D>();

            foreach (View curView in viewList)
            {
                if (curView.GetType() == typeof(View3D))
                {
                    //add to list
                    view3DList.Add((View3D)curView);
                }
            }

            return view3DList;
        }

        //returns list of all section and elevation views in the current model
        public static List<View> getAllSectionViews(Document curDoc)
        {
            //get all views
            FilteredElementCollector m_colViews = new FilteredElementCollector(curDoc);
            m_colViews.OfCategory(BuiltInCategory.OST_Views);
            m_colViews.OfClass(typeof(ViewSection));

            List<View> curViews = new List<View>();
            foreach (View x in m_colViews)
            {
                if (x.IsTemplate == false)
                {
                    curViews.Add(x);
                }
            }

            return curViews;
        }

        //returns a list of all the viewports in the current model
        public static List<Viewport> getAllViewports(Document curDoc)
        {
            //get all viewports
            FilteredElementCollector vpCollector = new FilteredElementCollector(curDoc);
            vpCollector.OfCategory(BuiltInCategory.OST_Viewports);

            //output viewports to list
            List<Viewport> vpList = new List<Viewport>();
            foreach (Viewport curVP in vpCollector)
            {
                //add to list
                vpList.Add(curVP);
            }

            return vpList;
        }

        //----------------Walls and rooms  --------------------------

        //returns a list of all the walls in the current model
        public static List<Wall> getAllWalls(Document curDoc)
        {
            //returns list of all wall types in current model
            List<Wall> wallList = new List<Wall>();

            FilteredElementCollector curCollector = new FilteredElementCollector(curDoc);
            curCollector.OfClass(typeof(Wall));

            foreach (Wall curWall in curCollector)
            {
                //add wall to list
                wallList.Add(curWall);
            }

            return wallList;
        }

        //returns a list of all wall types in the current model
        public static List<WallType> getAllWallTypes(Document curDoc)
        {
            //returns list of all wall types in current model
            List<WallType> wallList = new List<WallType>();

            FilteredElementCollector curCollector = new FilteredElementCollector(curDoc);
            curCollector.OfClass(typeof(WallType));

            foreach (WallType curWallType in curCollector)
            {
                //add wall type to list
                wallList.Add(curWallType);
            }

            return wallList;
        }

        //return a list of all the rooms in the current model file
        public static List<Room> getAllRooms(Document curDoc)
        {
            //get all rooms
            FilteredElementCollector curCollector = new FilteredElementCollector(curDoc);
            curCollector.OfCategory(BuiltInCategory.OST_Rooms);

            List<Room> roomList = new List<Room>();
            foreach (Room curRoom in curCollector.ToElements())
            {
                roomList.Add(curRoom);
            }

            return roomList;
        }

        //----------------Annotation and Lines --------------------------

        /*returns a list of all line styles in the current model
        public static CategoryNameMap GetAllLineStyles(Document curDoc)
        {
            //get all linestyles
            Category curCat = curDoc.Settings.Categories[BuiltInCategory.OST_Lines];
            CategoryNameMap subCats = curCat.SubCategories;

            return subCats;
        }*/

        //returns a list of all model lines styles in use in the current model
        public static List<GraphicsStyle> getAllModelLineStyles(Document curDoc)
        {
            //returns list of all model line styles in current model
            List<GraphicsStyle> lineList = new List<GraphicsStyle>();
            FilteredElementCollector curCollector = new FilteredElementCollector(curDoc);
            CurveElementFilter curFilter = new CurveElementFilter(CurveElementType.ModelCurve);
            GraphicsStyle curLineStyle = null;

            //need to filter our arcs and other curved lines
            foreach (ModelCurve curLine in curCollector.WherePasses(curFilter))
            {
                curLineStyle = (GraphicsStyle)curLine.LineStyle;

                if (lineList.Contains(curLineStyle) == false)
                {
                    //add to list
                    lineList.Add(curLineStyle);
                }
            }

            return lineList;
        }

        //get all filled region types in the curent model
        public static List<FilledRegionType> getAllFilledRegionTypes(Document curDoc)
        {
            //get all filled regions
            FilteredElementCollector curCollector = new FilteredElementCollector(curDoc);
            curCollector.OfClass(typeof(FilledRegionType));

            List<FilledRegionType> filledRegionList = new List<FilledRegionType>();

            foreach (FilledRegionType curType in curCollector)
            {
                //add to list
                filledRegionList.Add(curType);
            }

            return filledRegionList;
        }

        //get all text notes in the current model
        public static List<TextNote> getAllTextNotes(Document curDoc)
        {
            //get all text notes
            FilteredElementCollector curCollector = new FilteredElementCollector(curDoc);
            curCollector.OfClass(typeof(TextNote));

            //create list for text notes
            List<TextNote> noteList = new List<TextNote>();

            //loop through text notes and add to list
            foreach (TextNote curNote in curCollector)
            {
                //add to list
                noteList.Add(curNote);
            }

            return noteList;
        }

        //----------------Linked files --------------------------

        //returns a list of all the RVT links in the current model file
        public static List<RevitLinkInstance> getRVTLinks(Document curDoc)
        {
            List<RevitLinkInstance> RvtLinkList = new List<RevitLinkInstance>();

            //get all RVT links in project
            FilteredElementCollector RvtLinkCollector = new FilteredElementCollector(curDoc);
            RvtLinkCollector.OfCategory(BuiltInCategory.OST_RvtLinks);
            RvtLinkCollector.OfClass(typeof(Instance));

            //loop through collector and put links in list
            foreach (RevitLinkInstance curLink in RvtLinkCollector)
            {
                RvtLinkList.Add(curLink);
            }

            return RvtLinkList;
        }

        //returns list of all linkd CAD files
        public static List<ImportInstance> getCADLinks(Document curDoc)
        {
            List<ImportInstance> linkList = new List<ImportInstance>();

            //get all CAD links in project
            FilteredElementCollector linkCollector = new FilteredElementCollector(curDoc);
            linkCollector.OfClass(typeof(ImportInstance)).ToElements();

            //loop through collector and put links in list
            foreach (ImportInstance curLink in linkCollector)
            {
                if (curLink.IsLinked == true)
                {
                    linkList.Add(curLink);
                }
            }

            return linkList;
        }

        //----------------Mass elements --------------------------

        public static List<Element> getAllMasses(Document curDoc)
        {
            //get all masses in current doc
            FilteredElementCollector massCollector = new FilteredElementCollector(curDoc);
            massCollector.OfCategory(BuiltInCategory.OST_Mass);

            List<Element> massList = new List<Element>();

            //loop through masses and add to list
            foreach (Element curMass in massCollector.ToElements())
            {
                massList.Add(curMass);
            }

            return massList;
        }

        public static List<Element> getAllMassFloors(Document curDoc)
        {
            //get all mass floors in current doc
            FilteredElementCollector massFloorColl = new FilteredElementCollector(curDoc);
            massFloorColl.OfCategory(BuiltInCategory.OST_MassFloor);

            List<Element> massFloorList = new List<Element>();

            //loop through masses and add to list
            foreach (Element curMassFloor in massFloorColl.ToElements())
            {
                massFloorList.Add(curMassFloor);
            }

            return massFloorList;
        }

        //----------------Miscellaneous --------------------------

        //return list of all generic family instances in the current model
        public static List<FamilyInstance> getAllGenericFamilies(Document curDoc)
        {
            //create filters
            Autodesk.Revit.DB.ElementClassFilter familyfilter = new Autodesk.Revit.DB.ElementClassFilter(typeof(Autodesk.Revit.DB.FamilyInstance));
            Autodesk.Revit.DB.ElementCategoryFilter typeFilter = new Autodesk.Revit.DB.ElementCategoryFilter(BuiltInCategory.OST_GenericModel);
            Autodesk.Revit.DB.LogicalAndFilter andFilter = new Autodesk.Revit.DB.LogicalAndFilter(familyfilter, typeFilter);

            //create collector and filter
            Autodesk.Revit.DB.FilteredElementCollector collector = new Autodesk.Revit.DB.FilteredElementCollector(curDoc);
            collector.WherePasses(andFilter);

            //create list for family instances
            List<FamilyInstance> famList = new List<FamilyInstance>();

            //loop through collector and add to list
            foreach (FamilyInstance curFam in collector)
            {
                famList.Add(curFam);
            }

            return famList;
        }

        //returns a list of all the areas in the current model
        public static List<Area> getAllAreas(Document curDoc)
        {
            //get all areas in the current model
            FilteredElementCollector areaCollector = new FilteredElementCollector(curDoc);
            areaCollector.OfCategory(BuiltInCategory.OST_Areas);

            //create list of areas
            List<Area> areaList = new List<Area>();

            //add areas to list
            foreach (Area curArea in areaCollector)
            {
                areaList.Add(curArea);
            }

            return areaList;
        }

        //return list of user-created worksets
        public static List<Workset> getAllUserWorksets(Document curDoc)
        {
            //get all user created worksets in current file
            FilteredWorksetCollector worksetCollector = new FilteredWorksetCollector(curDoc);
            worksetCollector.OfKind(WorksetKind.UserWorkset);

            //create list of worksets
            List<Workset> worksetList = new List<Workset>();

            foreach (Workset curWorkset in worksetCollector)
            {
                //add to list
                worksetList.Add(curWorkset);
            }

            return worksetList;
        }

        //return list of all family symbols in the current model
        public static List<FamilySymbol> getAllFamilySymbols(Document curDoc)
        {
            //get all family symbols
            FilteredElementCollector famCollector = new FilteredElementCollector(curDoc);
            famCollector.OfClass(typeof(FamilySymbol));

            //create list of family symbols
            List<FamilySymbol> famList = new List<FamilySymbol>();

            foreach (FamilySymbol curFS in famList)
            {
                //add to list
                famList.Add(curFS);
            }

            return famList;
        }

        //return list of all families in the current model
        public static List<Family> getAllFamilies(Document curDoc)
        {
            //get all families
            FilteredElementCollector famCollector = new FilteredElementCollector(curDoc);
            famCollector.OfClass(typeof(Family));

            //create list of family symbols
            List<Family> famList = new List<Family>();

            foreach (Family curFam in famList)
            {
                //add to list
                famList.Add(curFam);
            }

            return famList;
        }


        //get all model curves in the current model
        public static List<ModelCurve> getAllModelCurves(Document curDoc)
        {
            FilteredElementCollector curCollector = new FilteredElementCollector(curDoc);
            CurveElementFilter curFilter = new CurveElementFilter(CurveElementType.ModelCurve);

            //create list
            List<ModelCurve> lineList = new List<ModelCurve>();

            //loop through the elements - if element is a model line then add to list
            foreach (ModelCurve curCurve in curCollector.WherePasses(curFilter))
            {
                //add to list
                lineList.Add(curCurve);
            }

            return lineList;
        }

        /* public static List<string> getAllScopeBoxes(Document curDoc)
        {
            FilteredElementCollector m_colScope = new FilteredElementCollector(curDoc);
            m_colScope.OfCategory(BuiltInCategory.OST_VolumeOfInterest);

            List<string> scopeBoxList = new List<string>();

            //loop through scope boxes and add to list
            foreach (Element sb_loopVariable in m_colScope)
            {
                sb = sb_loopVariable;
                scopeBoxList.Add(sb.Name);
            }

            return scopeBoxList;
        }
        */

    }
}
