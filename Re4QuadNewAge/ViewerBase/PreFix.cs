using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;

namespace ViewerBase
{
    public struct PreFix
    {
        public Vector3 Position { get; set; }
        public Vector3 Scale { get; set; }
        public Vector3 Angle { get; set; }

        public Matrix4 GetRotation() 
        {
            return Matrix4.CreateRotationX(Angle.X) * Matrix4.CreateRotationY(Angle.Y) * Matrix4.CreateRotationZ(Angle.Z);
        }
    }
}