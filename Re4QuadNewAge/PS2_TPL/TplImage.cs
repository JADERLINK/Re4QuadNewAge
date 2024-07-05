using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace TPL_PS2_EXTRACT
{

    public class TplImage
    {
        private BinaryReader br;
        private bool flipY = false;
        private bool rotateInterlace1and3 = false;
        private long mainOffset = 0;

        public TplImage(ref BinaryReader br, bool flipY, bool rotateInterlace1and3, long mainOffset = 0)
        {
            this.br = br;
            this.flipY = flipY;
            this.rotateInterlace1and3 = rotateInterlace1and3;
            this.mainOffset = mainOffset;
        }

        public bool GetImage(int width, int height, ushort bitDepth, ushort interlace, uint indexesOffset, uint paletteOffset, out Bitmap bitmap)
        {

            if (bitDepth == 0x8 && (interlace == 0x0 || interlace == 0x1))
            {
                return bitDepth8interlace0and1(width, height, bitDepth, interlace, indexesOffset, paletteOffset, out bitmap);
            }
            else if (bitDepth == 0x9 && (interlace == 0x0 || interlace == 0x1))
            {
                return bitDepth9interlace0and1(width, height, bitDepth, interlace, indexesOffset, paletteOffset, out bitmap);
            }
            else if (bitDepth == 0x6 && (interlace == 0x0 || interlace == 0x1))
            {
                return bitDepth6interlace0and1(width, height, bitDepth, interlace, indexesOffset, paletteOffset, out bitmap);
            }
            else if (bitDepth == 0x8 && (interlace == 0x2 || interlace == 0x3))
            {
                return bitDepth8interlace2and3(width, height, bitDepth, interlace, indexesOffset, paletteOffset, out bitmap);
            }
            else if (bitDepth == 0x9 && (interlace == 0x2 || interlace == 0x3))
            {
                return bitDepth9interlace2and3(width, height, bitDepth, interlace, indexesOffset, paletteOffset, out bitmap);
            }
            else if (bitDepth == 0x6 && (interlace == 0x2 || interlace == 0x3))
            {
                //não suportado atualmente
            }
            else
            {
                //outros casos não existem
            }

            bitmap = null;
            return false;
        }

        private bool bitDepth8interlace0and1(int width, int height, ushort bitDepth, ushort interlace, uint indexesOffset, uint paletteOffset, out Bitmap bitmap)
        {
            int indexesbytesCount = (height * width) / 2;

            br.BaseStream.Position = indexesOffset + mainOffset;

            byte[] indexes = br.ReadBytes(indexesbytesCount);

            br.BaseStream.Position = paletteOffset + mainOffset;

            byte[] palette = br.ReadBytes(0x80);

            Color[] colors = new Color[16];

            int cont = 0;
            for (int ic = 0; ic < 16; ic++)
            {
                Color c = Color.FromArgb(
                    (byte)(palette[cont + 3] * 0xFF / 0x80),
                    palette[cont + 0],
                    palette[cont + 1],
                    palette[cont + 2]
                    );
                colors[ic] = c;
                cont += 4;
                if (ic == 7)
                {
                    cont = 0x40;
                }
            }

            SimpleBitmap sbitmap = new SimpleBitmap(width, height);

            int Xcont = 0;
            int Ycont = 0;
            for (int IN = 0; IN < indexes.Length; IN++)
            {
                int nibbleLow = indexes[IN] >> 4;
                int nibbleHigh = indexes[IN] & 0x0F;

                sbitmap.SetPixel(Xcont + 1, Ycont, colors[nibbleLow]);
                sbitmap.SetPixel(Xcont, Ycont, colors[nibbleHigh]);


                Xcont += 2;
                if (Xcont >= width)
                {
                    Xcont = 0;
                    Ycont += 1;
                }
            }

            bitmap = new Bitmap(width, height, width * 4,
            System.Drawing.Imaging.PixelFormat.Format32bppArgb,
            System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(sbitmap.BitmapData, 0));

            if (rotateInterlace1and3 && interlace == 0x1)
            {
                bitmap.RotateFlip(RotateFlipType.Rotate90FlipX);
            }

            if (flipY)
            {
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }

            return true;

        }

        private bool bitDepth9interlace0and1(int width, int height, ushort bitDepth, ushort interlace, uint indexesOffset, uint paletteOffset, out Bitmap bitmap)
        {
            int indexesbytesCount = (height * width);

            br.BaseStream.Position = indexesOffset + mainOffset;

            byte[] indexes = br.ReadBytes(indexesbytesCount);

            br.BaseStream.Position = paletteOffset + mainOffset;

            byte[] palette = br.ReadBytes(256 * 4);

            Color[] colors = new Color[256];

            int index = 0;
            int swap = 1;
            for (int ic = 0; ic < 256; ic++)
            {

                if (ic % 8 == 0 && ic != 0)
                {
                    if (swap == 1)
                    {
                        index += 8;
                    }
                    else if (swap == 2)
                    {
                        index -= 16;
                    }
                    else if (swap == 3)
                    {
                        index += 8;
                    }
                    else
                    {
                        swap = 0;
                    }
                    swap++;

                }

                int cont = index * 4;

                Color c = Color.FromArgb(
                  (byte)(palette[cont + 3] * 0xFF / 0x80),
                  palette[cont + 0],
                  palette[cont + 1],
                  palette[cont + 2]
                  );

                colors[ic] = c;

                index++;

            }

            
            SimpleBitmap sbitmap = new SimpleBitmap(width, height);

            int Xcont = 0;
            int Ycont = 0;
            for (int IN = 0; IN < indexes.Length; IN++)
            {
                sbitmap.SetPixel(Xcont, Ycont, colors[indexes[IN]]);

                Xcont += 1;
                if (Xcont >= width)
                {
                    Xcont = 0;
                    Ycont += 1;
                }
            }

            bitmap = new Bitmap(width, height, width * 4,
            System.Drawing.Imaging.PixelFormat.Format32bppArgb,
            System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(sbitmap.BitmapData, 0));

            if (rotateInterlace1and3 && interlace == 0x1)
            {
                bitmap.RotateFlip(RotateFlipType.Rotate90FlipX);
            }

            if (flipY)
            {
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }

            return true;
        }

        private bool bitDepth6interlace0and1(int width, int height, ushort bitDepth, ushort interlace, uint indexesOffset, uint paletteOffset, out Bitmap bitmap)
        {
            int bytesCount = (height * width) * 4;

            br.BaseStream.Position = indexesOffset + mainOffset;

            byte[] ColorBytes = br.ReadBytes(bytesCount);

            SimpleBitmap sbitmap = new SimpleBitmap(width, height);

            int Xcont = 0;
            int Ycont = 0;
            int lenght = height * width;
            int cont = 0;
            for (int IN = 0; IN < lenght; IN++)
            {
                Color c = Color.FromArgb(
                   ColorBytes[cont + 3],
                   ColorBytes[cont + 0],
                   ColorBytes[cont + 1],
                   ColorBytes[cont + 2]
                   );
                cont += 4;

                sbitmap.SetPixel(Xcont, Ycont, c);

                Xcont++;
                if (Xcont >= width)
                {
                    Xcont = 0;
                    Ycont += 1;
                }
            }

            bitmap = new Bitmap(width, height, width * 4,
            System.Drawing.Imaging.PixelFormat.Format32bppArgb,
            System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(sbitmap.BitmapData, 0));

            if (rotateInterlace1and3 && interlace == 0x1)
            {
                bitmap.RotateFlip(RotateFlipType.Rotate90FlipX);
            }

            if (flipY)
            {
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }

            return true;
        }


        private bool bitDepth8interlace2and3(int width, int height, ushort bitDepth, ushort interlace, uint indexesOffset, uint paletteOffset, out Bitmap bitmap)
        {
            int indexesbytesCount = (height * width) / 2;

            br.BaseStream.Position = indexesOffset + mainOffset;

            byte[] indexes = br.ReadBytes(indexesbytesCount);

            br.BaseStream.Position = paletteOffset + mainOffset;

            byte[] palette = br.ReadBytes(0x80);

            Color[] colors = new Color[16];

            int cont = 0;
            for (int ic = 0; ic < 16; ic++)
            {
                Color c = Color.FromArgb(
                    (byte)(palette[cont + 3] * 0xFF / 0x80),
                    palette[cont + 0],
                    palette[cont + 1],
                    palette[cont + 2]
                    );
                colors[ic] = c;
                cont += 4;
                if (ic == 7)
                {
                    cont = 0x40;
                }
            }


            // no comentario esta a imagem na posição correta
            //SimpleBitmap sbitmap = new SimpleBitmap(width, height);
            SimpleBitmap sbitmap = new SimpleBitmap(height, width);

            //Notas
            // a cada 32 bytes é uma nova sequencia
            // parede de 16 bytes, sendo 32 pixel
            // sendo 2 linhas


            int Xcont = 0;
            int Ycont = 0;

            int altCont = 0;
            int niveis = 0;

            //int nivelMax = (height / 16); // o correto é ser height
            int nivelMax = (width / 16);

            bool flipInX = false;

            for (int IN = 0; IN < indexes.Length; IN += 32)
            {

                preenche8(ref sbitmap, Xcont, Ycont, ref colors, ref indexes, IN, flipInX);

                Ycont += 16;
                niveis += 1;

                if (niveis == nivelMax)
                {
                    niveis = 0;
                    altCont += 1;
                    if (altCont % 2 == 0)
                    {
                        altCont += 2;

                        if (flipInX)
                        {
                            flipInX = false;
                        }
                        else
                        {
                            flipInX = true;
                        }
                    }
                    Ycont = altCont;

                    if (altCont == 16)
                    {
                        flipInX = false;
                        Ycont = 0;
                        altCont = 0;
                        niveis = 0;
                        Xcont += 32;

                    }
                }

            }

            // correção da imagem baseado em gambiarra

            if (width > 128 || height > 128)
            {
                SimpleBitmap bitmapFix = new SimpleBitmap(width, height);

                int blockAmounts = (width / 128) * (height / 128);

                int copyX = 0;
                int copyY = 0;
                int setX = 0;
                int setY = 0;

                for (int iB = 0; iB < blockAmounts; iB++)
                {
                    for (int y = 0; y < 128; y++)
                    {
                        for (int x = 0; x < 128; x++)
                        {
                            Color colorGet = sbitmap.GetPixel(x + copyX, y + copyY);
                            bitmapFix.SetPixel(x + setX, y + setY, colorGet);

                        }
                    }

                    setX += 128;
                    if (setX >= width)
                    {
                        setX = 0;
                        setY += 128;
                    }

                    copyY += 128;
                    if (copyY >= width)
                    {
                        copyY = 0;
                        copyX += 128;
                    }

                }

                sbitmap = bitmapFix;

            }


            bitmap = new Bitmap(width, height, width * 4,
            System.Drawing.Imaging.PixelFormat.Format32bppArgb,
            System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(sbitmap.BitmapData, 0));

            if (rotateInterlace1and3 && interlace == 0x3)
            {
                bitmap.RotateFlip(RotateFlipType.Rotate90FlipX);
            }

            if (flipY)
            {
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);

            }

            return true;
        }

        private bool bitDepth9interlace2and3(int width, int height, ushort bitDepth, ushort interlace, uint indexesOffset, uint paletteOffset, out Bitmap bitmap)
        {
            int indexesbytesCount = (height * width);

            br.BaseStream.Position = indexesOffset + mainOffset;

            byte[] indexes = br.ReadBytes(indexesbytesCount);

            br.BaseStream.Position = paletteOffset + mainOffset;

            byte[] palette = br.ReadBytes(256 * 4);

            Color[] colors = new Color[256];

            int index = 0;
            int swap = 1;
            for (int ic = 0; ic < 256; ic++)
            {

                if (ic % 8 == 0 && ic != 0)
                {
                    if (swap == 1)
                    {
                        index += 8;
                    }
                    else if (swap == 2)
                    {
                        index -= 16;
                    }
                    else if (swap == 3)
                    {
                        index += 8;
                    }
                    else
                    {
                        swap = 0;
                    }
                    swap++;

                }

                int cont = index * 4;

                Color c = Color.FromArgb(
                  (byte)(palette[cont + 3] * 0xFF / 0x80),
                  palette[cont + 0],
                  palette[cont + 1],
                  palette[cont + 2]
                  );

                colors[ic] = c;

                index++;

            }

            SimpleBitmap sbitmap = new SimpleBitmap(width, height);

            int Xcont = 0;
            int Ycont = 0;
            bool flipInX = false;
            for (int IN = 0; IN < indexes.Length; IN += 32)
            {
                preenche9(ref sbitmap, Xcont, Ycont, ref colors, ref indexes, IN, flipInX);

                Xcont += 16;

                if (Xcont >= width)
                {
                    Xcont = 0;
                    Ycont += 1;
                    if (Ycont % 2 == 0)
                    {
                        Ycont += 2;

                        if (flipInX)
                        {
                            flipInX = false;
                        }
                        else
                        {
                            flipInX = true;
                        }

                    }

                }

            }

            bitmap = new Bitmap(width, height, width * 4,
            System.Drawing.Imaging.PixelFormat.Format32bppArgb,
            System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(sbitmap.BitmapData, 0));

            if (rotateInterlace1and3 && interlace == 0x3)
            {
                bitmap.RotateFlip(RotateFlipType.Rotate90FlipX);
            }

            if (flipY)
            {
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
            }

            return true;
        }



        private static void preenche9(ref SimpleBitmap bitmap, int Xcont, int Ycont, ref Color[] colors, ref byte[] indexes, int IN, bool flipInX)
        {
            if (flipInX == false)
            {
                bitmap.SetPixel(Xcont + 0x0, Ycont + 0, colors[indexes[IN + 0x0]]);
                bitmap.SetPixel(Xcont + 0x4, Ycont + 2, colors[indexes[IN + 0x1]]);
                bitmap.SetPixel(Xcont + 0x8, Ycont + 0, colors[indexes[IN + 0x2]]);
                bitmap.SetPixel(Xcont + 0xC, Ycont + 2, colors[indexes[IN + 0x3]]);

                bitmap.SetPixel(Xcont + 0x1, Ycont + 0, colors[indexes[IN + 0x4]]);
                bitmap.SetPixel(Xcont + 0x5, Ycont + 2, colors[indexes[IN + 0x5]]);
                bitmap.SetPixel(Xcont + 0x9, Ycont + 0, colors[indexes[IN + 0x6]]);
                bitmap.SetPixel(Xcont + 0xD, Ycont + 2, colors[indexes[IN + 0x7]]);

                bitmap.SetPixel(Xcont + 0x2, Ycont + 0, colors[indexes[IN + 0x8]]);
                bitmap.SetPixel(Xcont + 0x6, Ycont + 2, colors[indexes[IN + 0x9]]);
                bitmap.SetPixel(Xcont + 0xA, Ycont + 0, colors[indexes[IN + 0xA]]);
                bitmap.SetPixel(Xcont + 0xE, Ycont + 2, colors[indexes[IN + 0xB]]);

                bitmap.SetPixel(Xcont + 0x3, Ycont + 0, colors[indexes[IN + 0xC]]);
                bitmap.SetPixel(Xcont + 0x7, Ycont + 2, colors[indexes[IN + 0xD]]);
                bitmap.SetPixel(Xcont + 0xB, Ycont + 0, colors[indexes[IN + 0xE]]);
                bitmap.SetPixel(Xcont + 0xF, Ycont + 2, colors[indexes[IN + 0xF]]);

                bitmap.SetPixel(Xcont + 0x4, Ycont + 0, colors[indexes[IN + 0x10]]);
                bitmap.SetPixel(Xcont + 0x0, Ycont + 2, colors[indexes[IN + 0x11]]);
                bitmap.SetPixel(Xcont + 0xC, Ycont + 0, colors[indexes[IN + 0x12]]);
                bitmap.SetPixel(Xcont + 0x8, Ycont + 2, colors[indexes[IN + 0x13]]);

                bitmap.SetPixel(Xcont + 0x5, Ycont + 0, colors[indexes[IN + 0x14]]);
                bitmap.SetPixel(Xcont + 0x1, Ycont + 2, colors[indexes[IN + 0x15]]);
                bitmap.SetPixel(Xcont + 0xD, Ycont + 0, colors[indexes[IN + 0x16]]);
                bitmap.SetPixel(Xcont + 0x9, Ycont + 2, colors[indexes[IN + 0x17]]);

                bitmap.SetPixel(Xcont + 0x6, Ycont + 0, colors[indexes[IN + 0x18]]);
                bitmap.SetPixel(Xcont + 0x2, Ycont + 2, colors[indexes[IN + 0x19]]);
                bitmap.SetPixel(Xcont + 0xE, Ycont + 0, colors[indexes[IN + 0x1A]]);
                bitmap.SetPixel(Xcont + 0xA, Ycont + 2, colors[indexes[IN + 0x1B]]);

                bitmap.SetPixel(Xcont + 0x7, Ycont + 0, colors[indexes[IN + 0x1C]]);
                bitmap.SetPixel(Xcont + 0x3, Ycont + 2, colors[indexes[IN + 0x1D]]);
                bitmap.SetPixel(Xcont + 0xF, Ycont + 0, colors[indexes[IN + 0x1E]]);
                bitmap.SetPixel(Xcont + 0xB, Ycont + 2, colors[indexes[IN + 0x1F]]);
            }
            else
            {
                bitmap.SetPixel(Xcont + 0x4, Ycont + 0, colors[indexes[IN + 0x0]]);
                bitmap.SetPixel(Xcont + 0x0, Ycont + 2, colors[indexes[IN + 0x1]]);
                bitmap.SetPixel(Xcont + 0xC, Ycont + 0, colors[indexes[IN + 0x2]]);
                bitmap.SetPixel(Xcont + 0x8, Ycont + 2, colors[indexes[IN + 0x3]]);

                bitmap.SetPixel(Xcont + 0x5, Ycont + 0, colors[indexes[IN + 0x4]]);
                bitmap.SetPixel(Xcont + 0x1, Ycont + 2, colors[indexes[IN + 0x5]]);
                bitmap.SetPixel(Xcont + 0xD, Ycont + 0, colors[indexes[IN + 0x6]]);
                bitmap.SetPixel(Xcont + 0x9, Ycont + 2, colors[indexes[IN + 0x7]]);

                bitmap.SetPixel(Xcont + 0x6, Ycont + 0, colors[indexes[IN + 0x8]]);
                bitmap.SetPixel(Xcont + 0x2, Ycont + 2, colors[indexes[IN + 0x9]]);
                bitmap.SetPixel(Xcont + 0xE, Ycont + 0, colors[indexes[IN + 0xA]]);
                bitmap.SetPixel(Xcont + 0xA, Ycont + 2, colors[indexes[IN + 0xB]]);

                bitmap.SetPixel(Xcont + 0x7, Ycont + 0, colors[indexes[IN + 0xC]]);
                bitmap.SetPixel(Xcont + 0x3, Ycont + 2, colors[indexes[IN + 0xD]]);
                bitmap.SetPixel(Xcont + 0xF, Ycont + 0, colors[indexes[IN + 0xE]]);
                bitmap.SetPixel(Xcont + 0xB, Ycont + 2, colors[indexes[IN + 0xF]]);

                bitmap.SetPixel(Xcont + 0x0, Ycont + 0, colors[indexes[IN + 0x10]]);
                bitmap.SetPixel(Xcont + 0x4, Ycont + 2, colors[indexes[IN + 0x11]]);
                bitmap.SetPixel(Xcont + 0x8, Ycont + 0, colors[indexes[IN + 0x12]]);
                bitmap.SetPixel(Xcont + 0xC, Ycont + 2, colors[indexes[IN + 0x13]]);

                bitmap.SetPixel(Xcont + 0x1, Ycont + 0, colors[indexes[IN + 0x14]]);
                bitmap.SetPixel(Xcont + 0x5, Ycont + 2, colors[indexes[IN + 0x15]]);
                bitmap.SetPixel(Xcont + 0x9, Ycont + 0, colors[indexes[IN + 0x16]]);
                bitmap.SetPixel(Xcont + 0xD, Ycont + 2, colors[indexes[IN + 0x17]]);

                bitmap.SetPixel(Xcont + 0x2, Ycont + 0, colors[indexes[IN + 0x18]]);
                bitmap.SetPixel(Xcont + 0x6, Ycont + 2, colors[indexes[IN + 0x19]]);
                bitmap.SetPixel(Xcont + 0xA, Ycont + 0, colors[indexes[IN + 0x1A]]);
                bitmap.SetPixel(Xcont + 0xE, Ycont + 2, colors[indexes[IN + 0x1B]]);

                bitmap.SetPixel(Xcont + 0x3, Ycont + 0, colors[indexes[IN + 0x1C]]);
                bitmap.SetPixel(Xcont + 0x7, Ycont + 2, colors[indexes[IN + 0x1D]]);
                bitmap.SetPixel(Xcont + 0xB, Ycont + 0, colors[indexes[IN + 0x1E]]);
                bitmap.SetPixel(Xcont + 0xF, Ycont + 2, colors[indexes[IN + 0x1F]]);

            }


        }


        private static void preenche8(ref SimpleBitmap bitmap, int Xcont, int Ycont, ref Color[] colors, ref byte[] indexes, int IN, bool flipInX)
        {
            int[][] idxs = new int[0x20][]; // indice de cor;
            for (int i = 0; i < 0x20; i++)
            {
                int nibbleLow = indexes[IN + i] >> 4;
                int nibbleHigh = indexes[IN + i] & 0x0F;

                int[] two = new int[2];

                two[0] = nibbleLow;
                two[1] = nibbleHigh;

                idxs[i] = two;
            }


            if (flipInX == false)
            {
                bitmap.SetPixel(Xcont + 0x4, Ycont + 2, colors[idxs[0x0][0]]);
                bitmap.SetPixel(Xcont + 0x0, Ycont + 0, colors[idxs[0x0][1]]);
                bitmap.SetPixel(Xcont + 0xC, Ycont + 2, colors[idxs[0x1][0]]);
                bitmap.SetPixel(Xcont + 0x8, Ycont + 0, colors[idxs[0x1][1]]);
                bitmap.SetPixel(Xcont + 0x14, Ycont + 2, colors[idxs[0x2][0]]);
                bitmap.SetPixel(Xcont + 0x10, Ycont + 0, colors[idxs[0x2][1]]);
                bitmap.SetPixel(Xcont + 0x1C, Ycont + 2, colors[idxs[0x3][0]]);
                bitmap.SetPixel(Xcont + 0x18, Ycont + 0, colors[idxs[0x3][1]]);

                bitmap.SetPixel(Xcont + 0x5, Ycont + 2, colors[idxs[0x4][0]]);
                bitmap.SetPixel(Xcont + 0x1, Ycont + 0, colors[idxs[0x4][1]]);
                bitmap.SetPixel(Xcont + 0xD, Ycont + 2, colors[idxs[0x5][0]]);
                bitmap.SetPixel(Xcont + 0x9, Ycont + 0, colors[idxs[0x5][1]]);
                bitmap.SetPixel(Xcont + 0x15, Ycont + 2, colors[idxs[0x6][0]]);
                bitmap.SetPixel(Xcont + 0x11, Ycont + 0, colors[idxs[0x6][1]]);
                bitmap.SetPixel(Xcont + 0x1D, Ycont + 2, colors[idxs[0x7][0]]);
                bitmap.SetPixel(Xcont + 0x19, Ycont + 0, colors[idxs[0x7][1]]);



                bitmap.SetPixel(Xcont + 0x6, Ycont + 2, colors[idxs[0x8][0]]);
                bitmap.SetPixel(Xcont + 0x2, Ycont + 0, colors[idxs[0x8][1]]);
                bitmap.SetPixel(Xcont + 0xE, Ycont + 2, colors[idxs[0x9][0]]);
                bitmap.SetPixel(Xcont + 0xA, Ycont + 0, colors[idxs[0x9][1]]);
                bitmap.SetPixel(Xcont + 0x16, Ycont + 2, colors[idxs[0xA][0]]);
                bitmap.SetPixel(Xcont + 0x12, Ycont + 0, colors[idxs[0xA][1]]);
                bitmap.SetPixel(Xcont + 0x1E, Ycont + 2, colors[idxs[0xB][0]]);
                bitmap.SetPixel(Xcont + 0x1A, Ycont + 0, colors[idxs[0xB][1]]);

                bitmap.SetPixel(Xcont + 0x7, Ycont + 2, colors[idxs[0xC][0]]);
                bitmap.SetPixel(Xcont + 0x3, Ycont + 0, colors[idxs[0xC][1]]);
                bitmap.SetPixel(Xcont + 0xF, Ycont + 2, colors[idxs[0xD][0]]);
                bitmap.SetPixel(Xcont + 0xB, Ycont + 0, colors[idxs[0xD][1]]);
                bitmap.SetPixel(Xcont + 0x17, Ycont + 2, colors[idxs[0xE][0]]);
                bitmap.SetPixel(Xcont + 0x13, Ycont + 0, colors[idxs[0xE][1]]);
                bitmap.SetPixel(Xcont + 0x1F, Ycont + 2, colors[idxs[0xF][0]]);
                bitmap.SetPixel(Xcont + 0x1B, Ycont + 0, colors[idxs[0xF][1]]);



                bitmap.SetPixel(Xcont + 0x0, Ycont + 2, colors[idxs[0x10][0]]);
                bitmap.SetPixel(Xcont + 0x4, Ycont + 0, colors[idxs[0x10][1]]);
                bitmap.SetPixel(Xcont + 0x8, Ycont + 2, colors[idxs[0x11][0]]);
                bitmap.SetPixel(Xcont + 0xC, Ycont + 0, colors[idxs[0x11][1]]);
                bitmap.SetPixel(Xcont + 0x10, Ycont + 2, colors[idxs[0x12][0]]);
                bitmap.SetPixel(Xcont + 0x14, Ycont + 0, colors[idxs[0x12][1]]);
                bitmap.SetPixel(Xcont + 0x18, Ycont + 2, colors[idxs[0x13][0]]);
                bitmap.SetPixel(Xcont + 0x1C, Ycont + 0, colors[idxs[0x13][1]]);

                bitmap.SetPixel(Xcont + 0x1, Ycont + 2, colors[idxs[0x14][0]]);
                bitmap.SetPixel(Xcont + 0x5, Ycont + 0, colors[idxs[0x14][1]]);
                bitmap.SetPixel(Xcont + 0x9, Ycont + 2, colors[idxs[0x15][0]]);
                bitmap.SetPixel(Xcont + 0xD, Ycont + 0, colors[idxs[0x15][1]]);
                bitmap.SetPixel(Xcont + 0x11, Ycont + 2, colors[idxs[0x16][0]]);
                bitmap.SetPixel(Xcont + 0x15, Ycont + 0, colors[idxs[0x16][1]]);
                bitmap.SetPixel(Xcont + 0x19, Ycont + 2, colors[idxs[0x17][0]]);
                bitmap.SetPixel(Xcont + 0x1D, Ycont + 0, colors[idxs[0x17][1]]);



                bitmap.SetPixel(Xcont + 0x2, Ycont + 2, colors[idxs[0x18][0]]);
                bitmap.SetPixel(Xcont + 0x6, Ycont + 0, colors[idxs[0x18][1]]);
                bitmap.SetPixel(Xcont + 0xA, Ycont + 2, colors[idxs[0x19][0]]);
                bitmap.SetPixel(Xcont + 0xE, Ycont + 0, colors[idxs[0x19][1]]);
                bitmap.SetPixel(Xcont + 0x12, Ycont + 2, colors[idxs[0x1A][0]]);
                bitmap.SetPixel(Xcont + 0x16, Ycont + 0, colors[idxs[0x1A][1]]);
                bitmap.SetPixel(Xcont + 0x1A, Ycont + 2, colors[idxs[0x1B][0]]);
                bitmap.SetPixel(Xcont + 0x1E, Ycont + 0, colors[idxs[0x1B][1]]);

                bitmap.SetPixel(Xcont + 0x3, Ycont + 2, colors[idxs[0x1C][0]]);
                bitmap.SetPixel(Xcont + 0x7, Ycont + 0, colors[idxs[0x1C][1]]);
                bitmap.SetPixel(Xcont + 0xB, Ycont + 2, colors[idxs[0x1D][0]]);
                bitmap.SetPixel(Xcont + 0xF, Ycont + 0, colors[idxs[0x1D][1]]);
                bitmap.SetPixel(Xcont + 0x13, Ycont + 2, colors[idxs[0x1E][0]]);
                bitmap.SetPixel(Xcont + 0x17, Ycont + 0, colors[idxs[0x1E][1]]);
                bitmap.SetPixel(Xcont + 0x1B, Ycont + 2, colors[idxs[0x1F][0]]);
                bitmap.SetPixel(Xcont + 0x1F, Ycont + 0, colors[idxs[0x1F][1]]);



            }
            else
            {
                // versão flip

                bitmap.SetPixel(Xcont + 0x0, Ycont + 2, colors[idxs[0x0][0]]);
                bitmap.SetPixel(Xcont + 0x4, Ycont + 0, colors[idxs[0x0][1]]);
                bitmap.SetPixel(Xcont + 0x8, Ycont + 2, colors[idxs[0x1][0]]);
                bitmap.SetPixel(Xcont + 0xC, Ycont + 0, colors[idxs[0x1][1]]);
                bitmap.SetPixel(Xcont + 0x10, Ycont + 2, colors[idxs[0x2][0]]);
                bitmap.SetPixel(Xcont + 0x14, Ycont + 0, colors[idxs[0x2][1]]);
                bitmap.SetPixel(Xcont + 0x18, Ycont + 2, colors[idxs[0x3][0]]);
                bitmap.SetPixel(Xcont + 0x1C, Ycont + 0, colors[idxs[0x3][1]]);

                bitmap.SetPixel(Xcont + 0x1, Ycont + 2, colors[idxs[0x4][0]]);
                bitmap.SetPixel(Xcont + 0x5, Ycont + 0, colors[idxs[0x4][1]]);
                bitmap.SetPixel(Xcont + 0x9, Ycont + 2, colors[idxs[0x5][0]]);
                bitmap.SetPixel(Xcont + 0xD, Ycont + 0, colors[idxs[0x5][1]]);
                bitmap.SetPixel(Xcont + 0x11, Ycont + 2, colors[idxs[0x6][0]]);
                bitmap.SetPixel(Xcont + 0x15, Ycont + 0, colors[idxs[0x6][1]]);
                bitmap.SetPixel(Xcont + 0x19, Ycont + 2, colors[idxs[0x7][0]]);
                bitmap.SetPixel(Xcont + 0x1D, Ycont + 0, colors[idxs[0x7][1]]);



                bitmap.SetPixel(Xcont + 0x2, Ycont + 2, colors[idxs[0x8][0]]);
                bitmap.SetPixel(Xcont + 0x6, Ycont + 0, colors[idxs[0x8][1]]);
                bitmap.SetPixel(Xcont + 0xA, Ycont + 2, colors[idxs[0x9][0]]);
                bitmap.SetPixel(Xcont + 0xE, Ycont + 0, colors[idxs[0x9][1]]);
                bitmap.SetPixel(Xcont + 0x12, Ycont + 2, colors[idxs[0xA][0]]);
                bitmap.SetPixel(Xcont + 0x16, Ycont + 0, colors[idxs[0xA][1]]);
                bitmap.SetPixel(Xcont + 0x1A, Ycont + 2, colors[idxs[0xB][0]]);
                bitmap.SetPixel(Xcont + 0x1E, Ycont + 0, colors[idxs[0xB][1]]);

                bitmap.SetPixel(Xcont + 0x3, Ycont + 2, colors[idxs[0xC][0]]);
                bitmap.SetPixel(Xcont + 0x7, Ycont + 0, colors[idxs[0xC][1]]);
                bitmap.SetPixel(Xcont + 0xB, Ycont + 2, colors[idxs[0xD][0]]);
                bitmap.SetPixel(Xcont + 0xF, Ycont + 0, colors[idxs[0xD][1]]);
                bitmap.SetPixel(Xcont + 0x13, Ycont + 2, colors[idxs[0xE][0]]);
                bitmap.SetPixel(Xcont + 0x17, Ycont + 0, colors[idxs[0xE][1]]);
                bitmap.SetPixel(Xcont + 0x1B, Ycont + 2, colors[idxs[0xF][0]]);
                bitmap.SetPixel(Xcont + 0x1F, Ycont + 0, colors[idxs[0xF][1]]);



                bitmap.SetPixel(Xcont + 0x4, Ycont + 2, colors[idxs[0x10][0]]);
                bitmap.SetPixel(Xcont + 0x0, Ycont + 0, colors[idxs[0x10][1]]);
                bitmap.SetPixel(Xcont + 0xC, Ycont + 2, colors[idxs[0x11][0]]);
                bitmap.SetPixel(Xcont + 0x8, Ycont + 0, colors[idxs[0x11][1]]);
                bitmap.SetPixel(Xcont + 0x14, Ycont + 2, colors[idxs[0x12][0]]);
                bitmap.SetPixel(Xcont + 0x10, Ycont + 0, colors[idxs[0x12][1]]);
                bitmap.SetPixel(Xcont + 0x1C, Ycont + 2, colors[idxs[0x13][0]]);
                bitmap.SetPixel(Xcont + 0x18, Ycont + 0, colors[idxs[0x13][1]]);

                bitmap.SetPixel(Xcont + 0x5, Ycont + 2, colors[idxs[0x14][0]]);
                bitmap.SetPixel(Xcont + 0x1, Ycont + 0, colors[idxs[0x14][1]]);
                bitmap.SetPixel(Xcont + 0xD, Ycont + 2, colors[idxs[0x15][0]]);
                bitmap.SetPixel(Xcont + 0x9, Ycont + 0, colors[idxs[0x15][1]]);
                bitmap.SetPixel(Xcont + 0x15, Ycont + 2, colors[idxs[0x16][0]]);
                bitmap.SetPixel(Xcont + 0x11, Ycont + 0, colors[idxs[0x16][1]]);
                bitmap.SetPixel(Xcont + 0x1D, Ycont + 2, colors[idxs[0x17][0]]);
                bitmap.SetPixel(Xcont + 0x19, Ycont + 0, colors[idxs[0x17][1]]);



                bitmap.SetPixel(Xcont + 0x6, Ycont + 2, colors[idxs[0x18][0]]);
                bitmap.SetPixel(Xcont + 0x2, Ycont + 0, colors[idxs[0x18][1]]);
                bitmap.SetPixel(Xcont + 0xE, Ycont + 2, colors[idxs[0x19][0]]);
                bitmap.SetPixel(Xcont + 0xA, Ycont + 0, colors[idxs[0x19][1]]);
                bitmap.SetPixel(Xcont + 0x16, Ycont + 2, colors[idxs[0x1A][0]]);
                bitmap.SetPixel(Xcont + 0x12, Ycont + 0, colors[idxs[0x1A][1]]);
                bitmap.SetPixel(Xcont + 0x1E, Ycont + 2, colors[idxs[0x1B][0]]);
                bitmap.SetPixel(Xcont + 0x1A, Ycont + 0, colors[idxs[0x1B][1]]);

                bitmap.SetPixel(Xcont + 0x7, Ycont + 2, colors[idxs[0x1C][0]]);
                bitmap.SetPixel(Xcont + 0x3, Ycont + 0, colors[idxs[0x1C][1]]);
                bitmap.SetPixel(Xcont + 0xF, Ycont + 2, colors[idxs[0x1D][0]]);
                bitmap.SetPixel(Xcont + 0xB, Ycont + 0, colors[idxs[0x1D][1]]);
                bitmap.SetPixel(Xcont + 0x17, Ycont + 2, colors[idxs[0x1E][0]]);
                bitmap.SetPixel(Xcont + 0x13, Ycont + 0, colors[idxs[0x1E][1]]);
                bitmap.SetPixel(Xcont + 0x1F, Ycont + 2, colors[idxs[0x1F][0]]);
                bitmap.SetPixel(Xcont + 0x1B, Ycont + 0, colors[idxs[0x1F][1]]);

            }


        }


    }

}
