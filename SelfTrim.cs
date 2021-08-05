using Grasshopper;
using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace Mergery_0._0
{
    public class SelfTrim : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public SelfTrim()
          : base("Self Trim", 
                 "STrim",
                 "Clean up the lines that stick out.",
                 "Mergery", 
                 "Line")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Points", "Points", "List of Points to Trim", GH_ParamAccess.list);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddPointParameter("Trimmed Points", "Trimmed Points", "List of Trimmed Points", GH_ParamAccess.list);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<Point3d> iPoints = new List<Point3d>();
            DA.GetDataList("Points", iPoints);

            List<Point3d> p = iPoints;

            int N = p.Count - 2;

            List<Point3d> OutPt = new List<Point3d>();

            Point3d selfTrimmedPtA = new Point3d();
            Point3d selfTrimmedPtB = new Point3d();
            Point3d selfTrimmedPtC = new Point3d();

            for (int i = 0; i < N; i++)
            {
                Point3d aPt = p[i];
                Point3d bPt = p[i + 1];
                Point3d cPt = p[i + 2];


                double aPtX = p[i].X;
                double bPtX = p[i + 1].X;
                double cPtX = p[i + 2].X;

                double aPtY = p[i].Y;
                double bPtY = p[i + 1].Y;
                double cPtY = p[i + 2].Y;

                double aPtZ = p[i].Z;
                double bPtZ = p[i + 1].Z;
                double cPtZ = p[i + 2].Z;

                if (i == N - 2)
                {
                    if ((aPtX == bPtX && bPtX == cPtX && aPtY == bPtY && bPtY == cPtY)
                      || (aPtY == bPtY && bPtY == cPtY && aPtZ == bPtZ && bPtZ == cPtZ)
                      || (aPtZ == bPtZ && bPtZ == cPtZ && aPtX == bPtX && bPtX == cPtX))
                    {
                        Point3d dPt = p[i + 3];

                        selfTrimmedPtA = new Point3d(aPtX, aPtY, aPtZ);
                        selfTrimmedPtC = new Point3d(cPtX, cPtY, cPtZ);

                        OutPt.Add(selfTrimmedPtA);
                        OutPt.Add(selfTrimmedPtC);
                        OutPt.Add(dPt);

                        i++;
                    }
                    else
                    {
                        selfTrimmedPtA = new Point3d(aPtX, aPtY, aPtZ);

                        OutPt.Add(selfTrimmedPtA);
                    }
                }
                else
                {
                    if (i == N - 1)
                    {
                        if ((aPtX == bPtX && bPtX == cPtX && aPtY == bPtY && bPtY == cPtY)
                          || (aPtY == bPtY && bPtY == cPtY && aPtZ == bPtZ && bPtZ == cPtZ)
                          || (aPtZ == bPtZ && bPtZ == cPtZ && aPtX == bPtX && bPtX == cPtX))
                        {
                            selfTrimmedPtA = new Point3d(aPtX, aPtY, aPtZ);
                            selfTrimmedPtC = new Point3d(cPtX, cPtY, cPtZ);

                            OutPt.Add(selfTrimmedPtA);
                            OutPt.Add(selfTrimmedPtC);
                        }
                        else
                        {
                            selfTrimmedPtA = new Point3d(aPtX, aPtY, aPtZ);
                            selfTrimmedPtB = new Point3d(bPtX, bPtY, bPtZ);
                            selfTrimmedPtC = new Point3d(cPtX, cPtY, cPtZ);

                            OutPt.Add(selfTrimmedPtA);
                            OutPt.Add(selfTrimmedPtB);
                            OutPt.Add(selfTrimmedPtC);
                        }

                    }
                    else
                    {
                        if ((aPtX == bPtX && bPtX == cPtX && aPtY == bPtY && bPtY == cPtY)
                          || (aPtY == bPtY && bPtY == cPtY && aPtZ == bPtZ && bPtZ == cPtZ)
                          || (aPtZ == bPtZ && bPtZ == cPtZ && aPtX == bPtX && bPtX == cPtX))
                        {
                            selfTrimmedPtA = new Point3d(aPtX, aPtY, aPtZ);

                            OutPt.Add(selfTrimmedPtA);

                            i++;
                        }
                        else
                        {
                            selfTrimmedPtA = new Point3d(aPtX, aPtY, aPtZ);

                            OutPt.Add(selfTrimmedPtA);
                        }
                    }
                }
            }
            DA.SetDataList("Trimmed Points", OutPt);
        }


        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// You can add image files to your project resources and access them like this:
        /// return Resources.IconForThisComponent;
        /// </summary>
        protected override System.Drawing.Bitmap Icon => null;

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid => new Guid("3F89067C-C771-402D-96A2-69A9665480EC");
    }
}