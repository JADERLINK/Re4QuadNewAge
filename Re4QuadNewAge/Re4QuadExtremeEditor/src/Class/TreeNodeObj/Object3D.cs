using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Re4QuadExtremeEditor.src.Class.Enums;
using Re4QuadExtremeEditor.src.Class.ObjMethods;
using OpenTK;

namespace Re4QuadExtremeEditor.src.Class.TreeNodeObj
{
    /// <summary>
    /// <para>classe que representa os objetos, é usado no treeView;</para>
    /// <para>no Quad64 é usado no PropertyGrid, porem aqui dou outra finalidade;</para>
    /// </summary>
    public class Object3D : TreeNode, NsMultiselectTreeView.IAltNode, NsMultiselectTreeView.IFastNode, IEquatable<Object3D>, NsCamera.IObject3D
    {
        public static Object3D CreateNewInstance(GroupType group, ushort objLineRef) 
        {
            Object3D o = new Object3D();
            o.Name = objLineRef.ToString();
            o.Text = "";
            o.Group = group;
            o.ObjLineRef = objLineRef;
            return o;
        }

        protected Object3D() : base() {}
        protected Object3D(string text) : base(text){ }
        protected Object3D(string text, TreeNode[] children) : base(text, children) { }

        public GroupType Group { get; protected set; }

        public ushort ObjLineRef { get; protected set; }

        /// <summary>
        /// Retorna o texto do node;
        /// </summary>
        public string AltText { get {
                if (Parent is TreeNodeGroup parent)
                {
                    return parent.DisplayMethods.GetNodeText(ObjLineRef);
                }
                return "Error Text"; }}

        /// <summary>
        /// atualiza a cor para o node;
        /// </summary>
        public Color AltForeColor { get {
                if (Parent is TreeNodeGroup parent)
                {
                    return parent.DisplayMethods.GetNodeColor(ObjLineRef);
                }
                return ForeColor;} }


        /// <summary>
        /// retorna o posição do objeto para ser usada na camera orbit
        /// </summary>
        public Vector3 GetObjPosition_ToCamera() 
        {
            if (Parent is TreeNodeGroup parent)
            {
                return parent.MoveMethods.GetObjPostion_ToCamera(ObjLineRef);
            }
            return Vector3.Zero;
        }

        /// <summary>
        /// retorna o angulo do objeto para ser usada na camera orbit
        /// </summary>
        public float GetObjAngleY_ToCamera()
        {
            if (Parent is TreeNodeGroup parent)
            {
                return parent.MoveMethods.GetObjAngleY_ToCamera(ObjLineRef);
            }
            return 0;
        }

        /// <summary>
        /// retorna as coordenadas de posição na escala real do jogo pra poder fazer a ação de mover
        /// <para> para enimigos, etcmodel, item, warp, ladder, ashey point, GrappleGun [0]</para>
        /// <para> triggerZone point0 [1]</para>
        /// <para> triggerZone point1 [2]</para>
        /// <para> triggerZone point2 [3]</para>
        /// <para> triggerZone point3 [4]</para>
        /// <para> ReturnTriggerZoneCircleRadius(ID), ReturnTriggerZoneTrueY(ID), ReturnTriggerZoneMoreHeight(ID) [5]</para>
        /// <para> TriggerZone Center [6]</para>
        /// </summary>
        public Vector3[] GetObjPostion_ToMove_General() 
        {
            if (Parent is TreeNodeGroup parent)
            {
                return parent.MoveMethods.GetObjPostion_ToMove_General(ObjLineRef);
            }
            return null;
        }

        /// <summary>
        /// leia GetObjPostion_ToMove_General
        /// <para> Ordem:</para>
        /// <para> para enimigos, etcmodel, item, warp, ladder, ashey point, GrappleGun [0]</para>
        /// <para> triggerZone point0 [1]</para>
        /// <para> triggerZone point1 [2]</para>
        /// <para> triggerZone point2 [3]</para>
        /// <para> triggerZone point3 [4]</para>
        /// <para> ReturnTriggerZoneCircleRadius(ID), ReturnTriggerZoneTrueY(ID), ReturnTriggerZoneMoreHeight(ID) [5]</para>
        /// <para> TriggerZone Center [6]</para>
        /// </summary>
        /// <param name="value"></param>
        public void SetObjPostion_ToMove_General(Vector3[] value) 
        {
            if (Parent is TreeNodeGroup parent)
            {
                parent.MoveMethods.SetObjPostion_ToMove_General(ObjLineRef, value);
            }
        }

        public Vector3[] GetObjRotarionAngles_ToMove() 
        {
            if (Parent is TreeNodeGroup parent)
            {
                return parent.MoveMethods.GetObjRotationAngles_ToMove(ObjLineRef);
            }
            return null;

        }

        public void SetObjRotarionAngles_ToMove(Vector3[] value)
        {
            if (Parent is TreeNodeGroup parent)
            {
                parent.MoveMethods.SetObjRotationAngles_ToMove(ObjLineRef, value);
            }
        }

        public Vector3[] GetObjScale_ToMove()
        {
            if (Parent is TreeNodeGroup parent)
            {
                return parent.MoveMethods.GetObjScale_ToMove(ObjLineRef);
            }
            return null;

        }

        public void SetObjScale_ToMove(Vector3[] value)
        {
            if (Parent is TreeNodeGroup parent)
            {
                parent.MoveMethods.SetObjScale_ToMove(ObjLineRef, value);
            }
        }

        public TriggerZoneCategory GetTriggerZoneCategory() 
        {
            if (Parent is TreeNodeGroup parent)
            {
                return parent.MoveMethods.GetTriggerZoneCategory(ObjLineRef);
            }
            return TriggerZoneCategory.Disable;
        }

        public int HashCodeID { get { return (int)(((uint)Group * 0x10000) + ObjLineRef); } } 

        public override int GetHashCode()
        {
            return HashCodeID;
        }

        public override bool Equals(object obj)
        {
            return (obj is NsMultiselectTreeView.IFastNode fast && fast.HashCodeID == HashCodeID);
        }

        public bool Equals(Object3D other)
        {
            return other.HashCodeID == HashCodeID;
        }
    }
}
