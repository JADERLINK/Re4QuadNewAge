using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;

namespace Re4QuadExtremeEditor.src.Class.CustomDelegates
{

    public delegate string ReturnString(ushort ID);
    public delegate byte ReturnByte(ushort ID);
    public delegate sbyte ReturnSbyte(ushort ID);
    public delegate short ReturnShort(ushort ID);
    public delegate ushort ReturnUshort(ushort ID);
    public delegate int ReturnInt(ushort ID);
    public delegate uint ReturnUint(ushort ID);
    public delegate float ReturnFloat(ushort ID);

    public delegate string[] ReturnStringArray(ushort ID);
    public delegate byte[] ReturnByteArray(ushort ID);
    public delegate sbyte[] ReturnSbyteArray(ushort ID);
    public delegate short[] ReturnShortArray(ushort ID);
    public delegate ushort[] ReturnUshortArray(ushort ID);
    public delegate int[] ReturnIntArray(ushort ID);
    public delegate uint[] ReturnUintArray(ushort ID);
    public delegate float[] ReturnFloatArray(ushort ID);

    public delegate void SetString(ushort ID, string value);
    public delegate void SetByte(ushort ID, byte value);
    public delegate void SetSbyte(ushort ID, sbyte value);
    public delegate void SetShort(ushort ID, short value);
    public delegate void SetUshort(ushort ID, ushort value);
    public delegate void SetInt(ushort ID, int value);
    public delegate void SetUint(ushort ID, uint value);
    public delegate void SetFloat(ushort ID, float value);

    public delegate void SetStringArray(ushort ID, string[] value);
    public delegate void SetByteArray(ushort ID, byte[] value);
    public delegate void SetSbyteArray(ushort ID, sbyte[] value);
    public delegate void SetShortArray(ushort ID, short[] value);
    public delegate void SetUshortArray(ushort ID, ushort[] value);
    public delegate void SetIntArray(ushort ID, int[] value);
    public delegate void SetUintArray(ushort ID, uint[] value);
    public delegate void SetFloatArray(ushort ID, float[] value);


    public delegate byte ReturnByteFromPosition(ushort ID, byte FromPosition);
    public delegate void SetByteFromPosition(ushort ID, byte FromPosition, byte value);

    //OpenTK
    public delegate Vector3 ReturnVector3(ushort ID);
    public delegate void SetVector3(ushort ID, Vector3 value);

    public delegate Vector4 ReturnVector4(ushort ID);
    public delegate void SetVector4(ushort ID, Vector4 value);

    public delegate Matrix4 ReturnMatrix4(ushort ID);
    public delegate void SetMatrix4(ushort ID, Matrix4 value);

    public delegate Vector2[] ReturnVector2Array(ushort ID);
    public delegate void SetVector2Array(ushort ID, Vector2[] value);

    public delegate Vector3[] ReturnVector3Array(ushort ID);
    public delegate void SetVector3Array(ushort ID, Vector3[] value);


    // nodes

    // add novo objeto3D
    public delegate ushort AddNewLineID();
    // exclui objeto3D selecionado
    public delegate void RemoveLineID(ushort ID);

    // color
    public delegate Color ReturnColor(ushort ID);
    public delegate void SetColor(ushort ID, Color value);

    //search
    public delegate void ReturnSearch(object obj);
    public delegate object[] ReturnListToSearch();

    // metodo void
    public delegate void ActivateMethod();

    // return enum
    public delegate Enums.Re4Version ReturnRe4Version();
    public delegate Enums.SpecialFileFormat ReturnSpecialFileFormat();
    public delegate Enums.SpecialType ReturnSpecialType(ushort ID);
    public delegate Enums.SpecialZoneCategory ReturnSpecialZoneCategory(ushort ID);
    public delegate Enums.RefInteractionType ReturnRefInteractionType(ushort ID);
}
