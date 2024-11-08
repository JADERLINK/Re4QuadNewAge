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
        public override Type GetClassType()
        {
            return typeof(MultiSelectProperty);
        }

        public MultiSelectProperty(UpdateMethods updateMethods) : base()
        {
            SetThis(this);

            List_ESL = new GenericCollection();
            List_ETS = new GenericCollection();
            List_ITA = new GenericCollection();
            List_AEV = new GenericCollection();
            List_DSE = new GenericCollection();
            List_FSE = new GenericCollection();
            List_EAR = new GenericCollection();
            List_SAR = new GenericCollection();
            List_EMI = new GenericCollection();
            List_ESE = new GenericCollection();
            List_LIT_GROUPS = new GenericCollection();
            List_LIT_ENTRYS = new GenericCollection();
            List_QuadCustom = new GenericCollection();
            MultiSelectInfo = new MultiSelectObjInfoToProperty(Lang.GetAttributeText(aLang.MultiSelectInfoValueText), updateMethods);

            ChangePropertyIsBrowsable(nameof(MultiSelectInfo), false);
            ChangePropertyIsBrowsable(nameof(List_ESL), false);
            ChangePropertyIsBrowsable(nameof(List_ETS), false);
            ChangePropertyIsBrowsable(nameof(List_ITA), false);
            ChangePropertyIsBrowsable(nameof(List_AEV), false);
            ChangePropertyIsBrowsable(nameof(List_DSE), false);
            ChangePropertyIsBrowsable(nameof(List_FSE), false);
            ChangePropertyIsBrowsable(nameof(List_EAR), false);
            ChangePropertyIsBrowsable(nameof(List_SAR), false);
            ChangePropertyIsBrowsable(nameof(List_EMI), false);
            ChangePropertyIsBrowsable(nameof(List_ESE), false);
            ChangePropertyIsBrowsable(nameof(List_LIT_GROUPS), false);
            ChangePropertyIsBrowsable(nameof(List_LIT_ENTRYS), false);
            ChangePropertyIsBrowsable(nameof(List_QuadCustom), false);

            ChangePropertyName(nameof(List_ESL), Lang.GetText(eLang.NodeESL));
            ChangePropertyName(nameof(List_ETS), Lang.GetText(eLang.NodeETS));
            ChangePropertyName(nameof(List_ITA), Lang.GetText(eLang.NodeITA));
            ChangePropertyName(nameof(List_AEV), Lang.GetText(eLang.NodeAEV));
            ChangePropertyName(nameof(List_DSE), Lang.GetText(eLang.NodeDSE));
            ChangePropertyName(nameof(List_FSE), Lang.GetText(eLang.NodeFSE));
            ChangePropertyName(nameof(List_EAR), Lang.GetText(eLang.NodeEAR));
            ChangePropertyName(nameof(List_SAR), Lang.GetText(eLang.NodeSAR));
            ChangePropertyName(nameof(List_EMI), Lang.GetText(eLang.NodeEMI));
            ChangePropertyName(nameof(List_ESE), Lang.GetText(eLang.NodeESE));
            ChangePropertyName(nameof(List_LIT_GROUPS), Lang.GetText(eLang.NodeLIT_GROUPS));
            ChangePropertyName(nameof(List_LIT_ENTRYS), Lang.GetText(eLang.NodeLIT_ENTRYS));
            ChangePropertyName(nameof(List_QuadCustom), Lang.GetText(eLang.NodeQuadCustom));
        }

        public int LoadContent(List<TreeNode> Selecteds) 
        {
            var Object3D_ESL = Selecteds.FindAll(o => o.Parent != null && o.Parent is TreeNodeGroup g && g.Group == GroupType.ESL).Cast<Object3D>();
            var Object3D_ETS = Selecteds.FindAll(o => o.Parent != null && o.Parent is TreeNodeGroup g && g.Group == GroupType.ETS).Cast<Object3D>();
            var Object3D_ITA = Selecteds.FindAll(o => o.Parent != null && o.Parent is TreeNodeGroup g && g.Group == GroupType.ITA).Cast<Object3D>();
            var Object3D_AEV = Selecteds.FindAll(o => o.Parent != null && o.Parent is TreeNodeGroup g && g.Group == GroupType.AEV).Cast<Object3D>();
            var Object3D_DSE = Selecteds.FindAll(o => o.Parent != null && o.Parent is TreeNodeGroup g && g.Group == GroupType.DSE).Cast<Object3D>();
            var Object3D_FSE = Selecteds.FindAll(o => o.Parent != null && o.Parent is TreeNodeGroup g && g.Group == GroupType.FSE).Cast<Object3D>();
            var Object3D_SAR = Selecteds.FindAll(o => o.Parent != null && o.Parent is TreeNodeGroup g && g.Group == GroupType.SAR).Cast<Object3D>();
            var Object3D_EAR = Selecteds.FindAll(o => o.Parent != null && o.Parent is TreeNodeGroup g && g.Group == GroupType.EAR).Cast<Object3D>();
            var Object3D_ESE = Selecteds.FindAll(o => o.Parent != null && o.Parent is TreeNodeGroup g && g.Group == GroupType.ESE).Cast<Object3D>();
            var Object3D_EMI = Selecteds.FindAll(o => o.Parent != null && o.Parent is TreeNodeGroup g && g.Group == GroupType.EMI).Cast<Object3D>();
            var Object3D_LIT_ENTRYS = Selecteds.FindAll(o => o.Parent != null && o.Parent is TreeNodeGroup g && g.Group == GroupType.LIT_ENTRYS).Cast<Object3D>();
            var Object3D_LIT_GROUPS = Selecteds.FindAll(o => o.Parent != null && o.Parent is TreeNodeGroup g && g.Group == GroupType.LIT_GROUPS).Cast<Object3D>();
            var Object3D_QuadCustom = Selecteds.FindAll(o => o.Parent != null && o.Parent is TreeNodeGroup g && g.Group == GroupType.QUAD_CUSTOM).Cast<Object3D>();

            var EntryInfo_ESL = (from obj in Object3D_ESL select new MultiSelectEntryObj(obj));
            var EntryInfo_ETS = (from obj in Object3D_ETS select new MultiSelectEntryObj(obj));
            var EntryInfo_ITA = (from obj in Object3D_ITA select new MultiSelectEntryObj(obj));
            var EntryInfo_AEV = (from obj in Object3D_AEV select new MultiSelectEntryObj(obj));
            var EntryInfo_DSE = (from obj in Object3D_DSE select new MultiSelectEntryObj(obj));
            var EntryInfo_FSE = (from obj in Object3D_FSE select new MultiSelectEntryObj(obj));
            var EntryInfo_SAR = (from obj in Object3D_SAR select new MultiSelectEntryObj(obj));
            var EntryInfo_EAR = (from obj in Object3D_EAR select new MultiSelectEntryObj(obj));
            var EntryInfo_ESE = (from obj in Object3D_ESE select new MultiSelectEntryObj(obj));
            var EntryInfo_EMI = (from obj in Object3D_EMI select new MultiSelectEntryObj(obj));
            var EntryInfo_LIT_ENTRYS = (from obj in Object3D_LIT_ENTRYS select new MultiSelectEntryObj(obj));
            var EntryInfo_LIT_GROUPS = (from obj in Object3D_LIT_GROUPS select new MultiSelectEntryObj(obj));
            var EntryInfo_QuadCustom = (from obj in Object3D_QuadCustom select new MultiSelectEntryObj(obj));

            MultiSelectInfo.AddRange(EntryInfo_ESL);
            MultiSelectInfo.AddRange(EntryInfo_ETS);
            MultiSelectInfo.AddRange(EntryInfo_ITA);
            MultiSelectInfo.AddRange(EntryInfo_AEV);
            MultiSelectInfo.AddRange(EntryInfo_DSE);
            MultiSelectInfo.AddRange(EntryInfo_FSE);
            MultiSelectInfo.AddRange(EntryInfo_EAR);
            MultiSelectInfo.AddRange(EntryInfo_SAR);
            MultiSelectInfo.AddRange(EntryInfo_ESE);
            MultiSelectInfo.AddRange(EntryInfo_EMI);
            MultiSelectInfo.AddRange(EntryInfo_LIT_ENTRYS);
            MultiSelectInfo.AddRange(EntryInfo_LIT_GROUPS);
            MultiSelectInfo.AddRange(EntryInfo_QuadCustom);

            List_ESL.AddRange(EntryInfo_ESL);
            List_ETS.AddRange(EntryInfo_ETS);
            List_ITA.AddRange(EntryInfo_ITA);
            List_AEV.AddRange(EntryInfo_AEV);
            List_DSE.AddRange(EntryInfo_DSE);
            List_FSE.AddRange(EntryInfo_FSE);
            List_EAR.AddRange(EntryInfo_EAR);
            List_SAR.AddRange(EntryInfo_SAR);
            List_ESE.AddRange(EntryInfo_ESE);
            List_EMI.AddRange(EntryInfo_EMI);
            List_LIT_GROUPS.AddRange(EntryInfo_LIT_GROUPS);
            List_LIT_ENTRYS.AddRange(EntryInfo_LIT_ENTRYS);
            List_QuadCustom.AddRange(EntryInfo_QuadCustom);

            ChangePropertyIsBrowsable(nameof(MultiSelectInfo), MultiSelectInfo.GetCount() != 0);
            ChangePropertyIsBrowsable(nameof(List_ESL), List_ESL.Count != 0);
            ChangePropertyIsBrowsable(nameof(List_ETS), List_ETS.Count != 0);
            ChangePropertyIsBrowsable(nameof(List_ITA), List_ITA.Count != 0);
            ChangePropertyIsBrowsable(nameof(List_AEV), List_AEV.Count != 0);
            ChangePropertyIsBrowsable(nameof(List_DSE), List_DSE.Count != 0);
            ChangePropertyIsBrowsable(nameof(List_FSE), List_FSE.Count != 0);
            ChangePropertyIsBrowsable(nameof(List_EAR), List_EAR.Count != 0);
            ChangePropertyIsBrowsable(nameof(List_SAR), List_SAR.Count != 0);
            ChangePropertyIsBrowsable(nameof(List_EMI), List_EMI.Count != 0);
            ChangePropertyIsBrowsable(nameof(List_ESE), List_ESE.Count != 0);
            ChangePropertyIsBrowsable(nameof(List_LIT_GROUPS), List_LIT_GROUPS.Count != 0);
            ChangePropertyIsBrowsable(nameof(List_LIT_ENTRYS), List_LIT_ENTRYS.Count != 0);
            ChangePropertyIsBrowsable(nameof(List_QuadCustom), List_QuadCustom.Count != 0);

            return MultiSelectInfo.GetCount();
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
        [DisplayName("ERROR")]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [TypeConverter(typeof(GenericCollectionConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(1, 0)]
        public GenericCollection List_ESL { get; private set; }



        [CustomCategory(aLang.MultiSelectCategory)]
        [DisplayName("ERROR")]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [TypeConverter(typeof(GenericCollectionConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(2, 0)]
        public GenericCollection List_ETS { get; private set; }



        [CustomCategory(aLang.MultiSelectCategory)]
        [DisplayName("ERROR")]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [TypeConverter(typeof(GenericCollectionConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(3, 0)]
        public GenericCollection List_ITA { get; private set; }



        [CustomCategory(aLang.MultiSelectCategory)]
        [DisplayName("ERROR")]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [TypeConverter(typeof(GenericCollectionConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(4, 0)]
        public GenericCollection List_AEV { get; private set; }



        [CustomCategory(aLang.MultiSelectCategory)]
        [DisplayName("ERROR")]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [TypeConverter(typeof(GenericCollectionConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(5, 0)]
        public GenericCollection List_DSE { get; private set; }



        [CustomCategory(aLang.MultiSelectCategory)]
        [DisplayName("ERROR")]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [TypeConverter(typeof(GenericCollectionConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(6, 0)]
        public GenericCollection List_FSE { get; private set; }



        [CustomCategory(aLang.MultiSelectCategory)]
        [DisplayName("ERROR")]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [TypeConverter(typeof(GenericCollectionConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(7, 0)]
        public GenericCollection List_SAR { get; private set; }



        [CustomCategory(aLang.MultiSelectCategory)]
        [DisplayName("ERROR")]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [TypeConverter(typeof(GenericCollectionConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(8, 0)]
        public GenericCollection List_EAR { get; private set; }



        [CustomCategory(aLang.MultiSelectCategory)]
        [DisplayName("ERROR")]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [TypeConverter(typeof(GenericCollectionConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(9, 0)]
        public GenericCollection List_EMI { get; private set; }



        [CustomCategory(aLang.MultiSelectCategory)]
        [DisplayName("ERROR")]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [TypeConverter(typeof(GenericCollectionConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(10, 0)]
        public GenericCollection List_ESE { get; private set; }


        [CustomCategory(aLang.MultiSelectCategory)]
        [DisplayName("ERROR")]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [TypeConverter(typeof(GenericCollectionConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(11, 0)]
        public GenericCollection List_LIT_GROUPS { get; private set; }


        [CustomCategory(aLang.MultiSelectCategory)]
        [DisplayName("ERROR")]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [TypeConverter(typeof(GenericCollectionConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(12, 0)]
        public GenericCollection List_LIT_ENTRYS { get; private set; }


        [CustomCategory(aLang.MultiSelectCategory)]
        [DisplayName("ERROR")]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [TypeConverter(typeof(GenericCollectionConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(13, 0)]
        public GenericCollection List_QuadCustom { get; private set; }
    }

}
