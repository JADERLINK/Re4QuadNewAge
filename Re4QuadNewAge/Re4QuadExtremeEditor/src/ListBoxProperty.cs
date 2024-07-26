using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Re4QuadExtremeEditor.src.Class;
using Re4QuadExtremeEditor.src.Class.Enums;

namespace Re4QuadExtremeEditor.src
{
    /// <summary>
    /// Conteudo usado nas Listbox/ComboBox
    /// </summary>
    public static class ListBoxProperty
    {
        // listas de objetos a serem selecionados nos list box

        public static Dictionary<ushort, UshortObjForListBox> EnemiesList;
        public static Dictionary<ushort, UshortObjForListBox> ItemsList;
        public static Dictionary<ushort, UshortObjForListBox> EtcmodelsList;
        public static Dictionary<byte, ByteObjForListBox> EnemyEnableList;
        public static Dictionary<bool, BoolObjForListBox> FloatTypeList;
        public static Dictionary<SpecialType, ByteObjForListBox> SpecialTypeList;
        public static Dictionary<byte, ByteObjForListBox> SpecialZoneCategoryList;
        public static Dictionary<ushort, UshortObjForListBox> ItemAuraTypeList;
        public static Dictionary<byte, ByteObjForListBox> RefInteractionTypeList;
        public static Dictionary<byte, ByteObjForListBox> PromptMessageList;
        public static Dictionary<byte, ByteObjForListBox> QuadCustomPointStatusList;
        public static Dictionary<uint, UintObjForListBox> QuadCustomModelIDList;
    }
}
