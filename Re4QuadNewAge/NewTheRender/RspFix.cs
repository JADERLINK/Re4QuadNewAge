using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;

namespace NewAgeTheRender
{
    public struct RspFix
    {
        public RspFix(Vector3 Scale, Vector3 Position, Matrix4 Rotation) 
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Rotation = Rotation;
        }
        public Vector3 Position { get; set; }
        public Vector3 Scale { get; set; }
        public Matrix4 Rotation { get; set; }
    }
}
