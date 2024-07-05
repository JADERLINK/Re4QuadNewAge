using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Design;
using Re4QuadExtremeEditor.src.Class.Enums;
using Re4QuadExtremeEditor.src.Class.Interfaces;
using Re4QuadExtremeEditor.src.Class.ObjMethods;
using Re4QuadExtremeEditor.src.Class.MyProperty.CustomAttribute;
using Re4QuadExtremeEditor.src.Class.MyProperty.CustomTypeConverter;
using Re4QuadExtremeEditor.src.Class.MyProperty.CustomUITypeEditor;
using Re4QuadExtremeEditor.src.Class.TreeNodeObj;
using Re4QuadExtremeEditor.src.Class.MyProperty.CustomCollection;

namespace Re4QuadExtremeEditor.src.Class.MyProperty
{
    public class MultiSelectProperty : GenericProperty
    {
        private UpdateMethods updateMethods = null;

        public MultiSelectProperty(UpdateMethods updateMethods) : base()
        {
            SetThis(this);

            this.updateMethods = updateMethods;

            EnemyESL = 0;
            EtcModelETS = 0;
            SpecialITA = 0;
            SpecialAEV = 0;
            MultiSelectInfo = new MultiSelectObjInfoToProperty(Lang.GetAttributeText(aLang.MultiSelectInfoValueText), updateMethods);

            ChangePropertyIsBrowsable("MultiSelectInfo", false);
            ChangePropertyIsBrowsable("EnemyESL", false);
            ChangePropertyIsBrowsable("EtcModelETS", false);
            ChangePropertyIsBrowsable("SpecialITA", false);
            ChangePropertyIsBrowsable("SpecialAEV", false);
        }

        public void LoadContent(List<TreeNode> Selecteds) 
        {
            var Object3DEnemy = Selecteds.FindAll(o => o.Parent != null && o.Parent is EnemyNodeGroup).Cast<Object3D>().ToArray();
            var EnemyPropertyList = (from obj in Object3DEnemy select new EnemyProperty(obj.ObjLineRef, updateMethods, ((EnemyNodeGroup)obj.Parent).PropertyMethods, true));

            var Object3DEtcModel = Selecteds.FindAll(o => o.Parent != null && o.Parent is EtcModelNodeGroup).Cast<Object3D>().ToArray();
            var EtcModelPropertyList = (from obj in Object3DEtcModel select new EtcModelProperty(obj.ObjLineRef, updateMethods, ((EtcModelNodeGroup)obj.Parent).PropertyMethods, true));

            var Object3DSpecialITA = Selecteds.FindAll(o => o.Parent != null && o.Parent is SpecialNodeGroup g && g.Group == GroupType.ITA).Cast<Object3D>().ToArray();
            var SpecialITAPropertyList = (from obj in Object3DSpecialITA select new SpecialProperty(obj.ObjLineRef, updateMethods, ((SpecialNodeGroup)obj.Parent).PropertyMethods, false, true));

            var Object3DSpecialAEV = Selecteds.FindAll(o => o.Parent != null && o.Parent is SpecialNodeGroup g && g.Group == GroupType.AEV).Cast<Object3D>().ToArray();
            var SpecialAEVPropertyList = (from obj in Object3DSpecialAEV select new SpecialProperty(obj.ObjLineRef, updateMethods, ((SpecialNodeGroup)obj.Parent).PropertyMethods, false, true));

            MultiSelectInfo.AddRange(EnemyPropertyList);
            MultiSelectInfo.AddRange(EtcModelPropertyList);
            MultiSelectInfo.AddRange(SpecialITAPropertyList);
            MultiSelectInfo.AddRange(SpecialAEVPropertyList);

            EnemyESL = Object3DEnemy.LongLength;
            EtcModelETS = Object3DEtcModel.LongLength;
            SpecialITA = Object3DSpecialITA.LongLength;
            SpecialAEV = Object3DSpecialAEV.LongLength;

            ChangePropertyIsBrowsable("MultiSelectInfo", true);
            ChangePropertyIsBrowsable("EnemyESL", EnemyESL != 0);
            ChangePropertyIsBrowsable("EtcModelETS", EtcModelETS != 0);
            ChangePropertyIsBrowsable("SpecialITA", SpecialITA != 0);
            ChangePropertyIsBrowsable("SpecialAEV", SpecialAEV != 0);
        }

        [CustomCategory(aLang.MultiSelectCategory)]
        [CustomDisplayName(aLang.MultiSelectInfoDisplayName)]
        [CustomDescription(aLang.MultiSelectInfoDescription)]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(true)]
        [Editor(typeof(MultiSelectUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(0, 0)]
        public MultiSelectObjInfoToProperty MultiSelectInfo { get; }


        [CustomCategory(aLang.MultiSelectCategory)]
        [CustomDisplayName(aLang.MultiSelectEnemyDisplayName)]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        //[TypeConverter(typeof(GenericCollectionConverter))]
        //[Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(1, 0)]
        public long EnemyESL { get; private set; }



        [CustomCategory(aLang.MultiSelectCategory)]
        [CustomDisplayName(aLang.MultiSelectEtcmodelDisplayName)]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        //[TypeConverter(typeof(GenericCollectionConverter))]
        //[Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(2, 0)]
        public long EtcModelETS { get; private set; }



        [CustomCategory(aLang.MultiSelectCategory)]
        [CustomDisplayName(aLang.MultiSelectSpecialItaDisplayName)]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        //[TypeConverter(typeof(GenericCollectionConverter))]
        //[Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(3, 0)]
        public long SpecialITA { get; private set; }



        [CustomCategory(aLang.MultiSelectCategory)]
        [CustomDisplayName(aLang.MultiSelectSpecialAevDisplayName)]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        //[TypeConverter(typeof(GenericCollectionConverter))]
        //[Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(4, 0)]
        public long SpecialAEV { get; private set; }

    }

}
