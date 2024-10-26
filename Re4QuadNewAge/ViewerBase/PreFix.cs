using System;
using System.Collections.Generic;
using System.Text;
using OpenTK;

namespace ViewerBase
{
    public class PreFix
    {
        public Vector3 Position { get; set; }
        public Vector3 Scale { get; set; }
        public Vector3 Angle { get => _angle; set 
            {
                _angle = value;
                _rotation = Matrix4.CreateRotationX(value.X) * Matrix4.CreateRotationY(value.Y) * Matrix4.CreateRotationZ(value.Z);
            } }

        private Vector3 _angle = Vector3.Zero;
        private Matrix4 _rotation = Matrix4.Identity;

        public Matrix4 GetRotation()
        {
            return _rotation;
        }
    }
}