using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace ViewerBase
{
    public class TextureRef // baseado em https://github.com/opentk/LearnOpenTK/blob/master/Common/Texture.cs
    {
        private int Handle;
        //public readonly Bitmap bitmap;

        public TextureRef(Bitmap bitmap)
        {
            //this.bitmap = bitmap;
            Handle = GetTextureIdGL(bitmap);

            IsLoaded = true;
        }

        private static int GetTextureIdGL(Bitmap bitmap)
        {
            GL.BindVertexArray(0);
            int ID = GL.GenTexture();

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, ID);
            //LOAD FILE
            BitmapData imageData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, imageData.Width, imageData.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, imageData.Scan0);
            bitmap.UnlockBits(imageData);
            //TEXTURE PROPERTY
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.BindTexture(TextureTarget.Texture2D, 0);
            return ID;
        }

        public void Use(TextureUnit unit)
        {
            if (IsLoaded)
            {
                GL.ActiveTexture(unit);
                GL.BindTexture(TextureTarget.Texture2D, Handle);
            }
        }

        private bool IsLoaded = false;

        public void Unload() 
        {
            if (IsLoaded)
            {
                IsLoaded = false;
                GL.DeleteBuffer(Handle);
                Handle = -1;
            }
        }

    }
}

