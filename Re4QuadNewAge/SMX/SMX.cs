using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RE4_SMX_TOOL
{
    public class SMX
    {
        public byte UseSMXID;
        public byte Mode;
        public byte OpacityHierarchy;
        public byte FaceCulling;
        public uint LightSwitch;
        public byte AlphaHierarchy;
        public byte UnknownX09;
        public byte UnknownX0A;
        public byte UnknownX0B;
        public byte[] ColorRGB; // 3 bytes
        public byte ColorAlpha;

        //----------
        //mode 0x00       
        public uint UnknownU10;
        public uint UnknownU14;
        public uint UnknownU18;
        public uint UnknownU1C;
        public uint UnknownU20;
        public uint UnknownU24;
        public uint UnknownU28;
        public uint UnknownU2C;
        public uint UnknownU30;
        public uint UnknownU34;
        public uint UnknownU38;
        public uint UnknownU3C;
        public uint UnknownU40;
        public uint UnknownU44;
        public uint UnknownU48;
        public uint UnknownU4C;
        public uint UnknownU50;
        public uint UnknownU54;
        public uint UnknownU58;
        public uint UnknownU5C;
        public uint UnknownU60;
        public uint UnknownU64;
        public uint UnknownU68;
        public uint UnknownU6C;
        public uint UnknownU70;
        public uint UnknownU74;
        public uint UnknownU78;
        public uint UnknownU7C;
        public uint UnknownU80;
        public uint UnknownU84;
        public float TextureMovement_X;
        public float TextureMovement_Y;

        //----------
        //mode 0x01

        public float RotationSpeed_X;
        public float RotationSpeed_Y;
        public float RotationSpeed_Z;
        public float RotationSpeed_W;
        public uint Unknown_GTU;
        public uint Unknown_GTV;

        //----------
        //mode 0x02

        public float Swing0;
        public float Swing1;
        public float Swing2;
        public float Swing3;
        public float Swing4;
        public float Swing5;
        public float Swing6;
        public float Swing7;
        public float Swing8;
        public float Swing9;
        public float SwingA;
        public float SwingB;
        public float SwingC;

    }
}
