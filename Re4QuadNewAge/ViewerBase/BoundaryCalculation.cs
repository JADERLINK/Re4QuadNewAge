using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;

namespace ViewerBase
{
    public static class BoundaryCalculation
    {
        public static void TreatedModel(ref TreatedModel treatedModel)
        {
            // calculo Boundary do objeto.
            Dictionary<Boundary, Vector3> oboundary = new Dictionary<Boundary, Vector3>();
            if (treatedModel.Meshes.Count != 0)
            {
                oboundary.Add(Boundary.max, treatedModel.Meshes[0].MaxBoundary);
                oboundary.Add(Boundary.min, treatedModel.Meshes[0].MinBoundary);
            }
            else
            {
                oboundary.Add(Boundary.max, Vector3.Zero);
                oboundary.Add(Boundary.min, Vector3.Zero);
            }

            for (int i = 1; i < treatedModel.Meshes.Count; i++)
            {
                // MAX
                if (oboundary[Boundary.max].X < treatedModel.Meshes[i].MaxBoundary.X)
                {
                    var p = oboundary[Boundary.max];
                    p.X = treatedModel.Meshes[i].MaxBoundary.X;
                    oboundary[Boundary.max] = p;
                }

                if (oboundary[Boundary.max].Y < treatedModel.Meshes[i].MaxBoundary.Y)
                {
                    var p = oboundary[Boundary.max];
                    p.Y = treatedModel.Meshes[i].MaxBoundary.Y;
                    oboundary[Boundary.max] = p;
                }

                if (oboundary[Boundary.max].Z < treatedModel.Meshes[i].MaxBoundary.Z)
                {
                    var p = oboundary[Boundary.max];
                    p.Z = treatedModel.Meshes[i].MaxBoundary.Z;
                    oboundary[Boundary.max] = p;
                }

                //MIN
                if (oboundary[Boundary.min].X > treatedModel.Meshes[i].MinBoundary.X)
                {
                    var p = oboundary[Boundary.min];
                    p.X = treatedModel.Meshes[i].MinBoundary.X;
                    oboundary[Boundary.min] = p;
                }

                if (oboundary[Boundary.min].Y > treatedModel.Meshes[i].MinBoundary.Y)
                {
                    var p = oboundary[Boundary.min];
                    p.Y = treatedModel.Meshes[i].MinBoundary.Y;
                    oboundary[Boundary.min] = p;
                }

                if (oboundary[Boundary.min].Z > treatedModel.Meshes[i].MinBoundary.Z)
                {
                    var p = oboundary[Boundary.min];
                    p.Z = treatedModel.Meshes[i].MinBoundary.Z;
                    oboundary[Boundary.min] = p;
                }

            }

            Vector3 omin = oboundary[Boundary.min];
            Vector3 omax = oboundary[Boundary.max];

            treatedModel.MinBoundary = omin;
            treatedModel.MaxBoundary = omax;
            treatedModel.CenterBoundary = new Vector3((omax.X + omin.X) / 2, (omax.Y + omin.Y) / 2, (omax.Z + omin.Z) / 2);
        }

    }
}
