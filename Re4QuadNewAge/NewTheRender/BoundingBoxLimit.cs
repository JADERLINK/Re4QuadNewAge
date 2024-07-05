using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;

namespace NewAgeTheRender
{
    public struct BoundingBoxLimit
    {
        public Vector3 LowerBoundary { get; private set; }
        public Vector3 UpperBoundary { get; private set; }

        public BoundingBoxLimit(Vector3 min, Vector3 max) 
        {
            LowerBoundary = min;
            UpperBoundary = max;
        }
    }
}
