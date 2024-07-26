using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Re4QuadExtremeEditor.src.Class.CustomDelegates;

namespace Re4QuadExtremeEditor.src.Class.ObjMethods
{
    /// <summary>
    /// Classe que contem referencia aos metodos de update do GL, PropertyGrid, TreeViewObjs, MoveObjSelection e UpdateOrbitCamera;
    /// </summary>
    public class UpdateMethods
    {
        public ActivateMethod UpdateGL; // atualiza a exibição do GL
        public ActivateMethod UpdatePropertyGrid; // atualiza a exibição do propertyGrid
        public ActivateMethod UpdateTreeViewObjs; // atualiza a exibição do TreeViewObjs
        public ActivateMethod UpdateMoveObjSelection; // atualiza a lista de opções do moveObj
        public ActivateMethod UpdateOrbitCamera; // atualiza a posição da camera em relação a de orbit camera
    }
}
