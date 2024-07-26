using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Re4QuadExtremeEditor.src.Class.CustomDelegates;

namespace Re4QuadExtremeEditor.src.Class.ObjMethods
{
    public class NodeMoveMethods
    {
        /// <summary>
        /// retorna o posição do objeto para ser usada na camera orbit
        /// </summary>
        public ReturnVector3 GetObjPostion_ToCamera;

        /// <summary>
        /// retorna o angulo do objeto para ser usada na camera orbit
        /// </summary>
        public ReturnFloat GetObjAngleY_ToCamera;

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
        public ReturnVector3Array GetObjPostion_ToMove_General;

        /// <summary>
        /// leia GetObjPostion_ToMove_General
        /// </summary>
        public SetVector3Array SetObjPostion_ToMove_General;

        /// <summary>
        /// angulos de rotação
        /// </summary>
        public ReturnVector3Array GetObjRotationAngles_ToMove;

        /// <summary>
        /// angulos de rotação
        /// </summary>
        public SetVector3Array SetObjRotationAngles_ToMove;


        /// <summary>
        /// escala do etcmodel
        /// </summary>
        public ReturnVector3Array GetObjScale_ToMove;

        /// <summary>
        /// escala do etcmodel
        /// </summary>
        public SetVector3Array SetObjScale_ToMove;

        /// <summary>
        /// caso for uma triggerZone retorna o tipo, caso não for, retorna Disable
        /// </summary>
        public ReturnTriggerZoneCategory GetTriggerZoneCategory;
    }
}
