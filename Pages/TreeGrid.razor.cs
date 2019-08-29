using Microsoft.AspNetCore.Components;
using Syncfusion.EJ2.Blazor.TreeGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace BlazorSyncFusionServerSide.Pages
{
    public class TreeGridBase : ComponentBase
    {
        EjsTreeGrid<TreeData> TheGrid;

        public List<TreeData> TreeGridData { get; set; }

        protected override void OnInitialized()
        {
            TreeGridData = TreeData.GetSelfDataSource().ToList();
        }

        protected void ToolbarClickHandler(Syncfusion.EJ2.Blazor.Navigations.ClickEventArgs Args)
        {
            if (Args.Item.Text == "Excel Export")
            {
                TheGrid.ExcelExport();
            }
        }
    }

    public class TreeData
    {
        public static List<TreeData> tree = new List<TreeData>();

        [Key]
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Progress { get; set; }
        public string Priority { get; set; }
        public int Duration { get; set; }
        public int? ParentID { get; set; }
        public bool? isParent { get; set; }

        public TreeData() { }

        public static List<TreeData> GetSelfDataSource()
        {
            if (tree.Count == 0)
            {
                int root = -1;
                for (var t = 1; t <= 60; t++)
                {
                    Random ran = new Random();
                    string math = (ran.Next() % 3) == 0 ? "High" : (ran.Next() % 2) == 0 ? "Release Breaker" : "Critical";
                    string progr = (ran.Next() % 3) == 0 ? "Started" : (ran.Next() % 2) == 0 ? "Open" : "In Progress";
                    root++;
                    int rootItem = tree.Count + root + 1;
                    tree.Add(new TreeData() { TaskID = rootItem, TaskName = "Parent Task " + rootItem.ToString(), StartDate = new DateTime(1992, 06, 07), EndDate = new DateTime(1994, 08, 25), isParent = true, ParentID = null, Progress = progr, Priority = math, Duration = ran.Next(1, 50) });
                    int parent = tree.Count;
                    for (var c = 0; c < 10; c++)
                    {
                        root++;
                        string val = ((parent + c + 1) % 3 == 0) ? "Low" : "Critical";
                        int parn = parent + c + 1;
                        progr = (ran.Next() % 3) == 0 ? "In Progress" : (ran.Next() % 2) == 0 ? "Open" : "Validated";
                        int iD = tree.Count + root + 1;
                        tree.Add(new TreeData() { TaskID = iD, TaskName = "Child Task " + iD.ToString(), StartDate = new DateTime(1992, 06, 07), EndDate = new DateTime(1994, 08, 25), isParent = (((parent + c + 1) % 3) == 0), ParentID = rootItem, Progress = progr, Priority = val, Duration = ran.Next(1, 50) });
                        if ((((parent + c + 1) % 3) == 0))
                        {
                            int immParent = tree.Count;
                            for (var s = 0; s < 3; s++)
                            {
                                root++;
                                string Prior = (immParent % 2 == 0) ? "Validated" : "Normal";
                                tree.Add(new TreeData() { TaskID = tree.Count + root + 1, TaskName = "Sub Task " + (tree.Count + root + 1).ToString(), StartDate = new DateTime(1992, 06, 07), EndDate = new DateTime(1994, 08, 25), isParent = false, ParentID = iD, Progress = (immParent % 2 == 0) ? "On Progress" : "Closed", Priority = Prior, Duration = ran.Next(1, 50) });
                            }
                        }
                    }
                }
            }
            return tree;
        }
    }
}
