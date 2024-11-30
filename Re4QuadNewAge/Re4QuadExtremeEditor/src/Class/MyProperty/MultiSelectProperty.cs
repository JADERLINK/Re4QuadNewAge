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

            MultiSelectInfo = new MultiSelectObjInfoToProperty(Lang.GetAttributeText(aLang.MultiSelectInfoValueText), updateMethods);
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
            List_EFF_Table0 = new GenericCollection();
            List_EFF_Table1 = new GenericCollection();
            List_EFF_Table2 = new GenericCollection();
            List_EFF_Table3 = new GenericCollection();
            List_EFF_Table4 = new GenericCollection();
            List_EFF_Table6 = new GenericCollection();
            List_EFF_Table7 = new GenericCollection();
            List_EFF_Table8 = new GenericCollection();
            List_EFF_Table9 = new GenericCollection();
            List_EFF_EffectEntry = new GenericCollection();


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
            ChangePropertyIsBrowsable(nameof(List_EFF_Table0), false);
            ChangePropertyIsBrowsable(nameof(List_EFF_Table1), false);
            ChangePropertyIsBrowsable(nameof(List_EFF_Table2), false);
            ChangePropertyIsBrowsable(nameof(List_EFF_Table3), false);
            ChangePropertyIsBrowsable(nameof(List_EFF_Table4), false);
            ChangePropertyIsBrowsable(nameof(List_EFF_Table6), false);
            ChangePropertyIsBrowsable(nameof(List_EFF_Table7), false);
            ChangePropertyIsBrowsable(nameof(List_EFF_Table8), false);
            ChangePropertyIsBrowsable(nameof(List_EFF_Table9), false);
            ChangePropertyIsBrowsable(nameof(List_EFF_EffectEntry), false);


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
            ChangePropertyName(nameof(List_EFF_Table0), Lang.GetText(eLang.NodeEFF_Table0));
            ChangePropertyName(nameof(List_EFF_Table1), Lang.GetText(eLang.NodeEFF_Table1));
            ChangePropertyName(nameof(List_EFF_Table2), Lang.GetText(eLang.NodeEFF_Table2));
            ChangePropertyName(nameof(List_EFF_Table3), Lang.GetText(eLang.NodeEFF_Table3));
            ChangePropertyName(nameof(List_EFF_Table4), Lang.GetText(eLang.NodeEFF_Table4));
            ChangePropertyName(nameof(List_EFF_Table6), Lang.GetText(eLang.NodeEFF_Table6));
            ChangePropertyName(nameof(List_EFF_Table9), Lang.GetText(eLang.NodeEFF_Table9));
            ChangePropertyName(nameof(List_EFF_Table7), Lang.GetText(eLang.NodeEFF_Table7_Effect_0));
            ChangePropertyName(nameof(List_EFF_Table8), Lang.GetText(eLang.NodeEFF_Table8_Effect_1));
            ChangePropertyName(nameof(List_EFF_EffectEntry), Lang.GetText(eLang.NodeEFF_EffectEntry));
        }

        private IEnumerable<Object3D> FindAll(List<TreeNode> Selecteds, GroupType groupType) 
        {
            return Selecteds.FindAll(o => o.Parent != null && o.Parent is TreeNodeGroup g && g.Group == groupType).Cast<Object3D>();
        }

        private IEnumerable<MultiSelectEntryObj> GetMultiSelectEntryList(IEnumerable<Object3D> Object3D) 
        {
            return (from obj in Object3D select new MultiSelectEntryObj(obj));
        }

        public int LoadContent(List<TreeNode> Selecteds) 
        {
            var EntryInfo_ESL = GetMultiSelectEntryList(FindAll(Selecteds, GroupType.ESL));
            var EntryInfo_ETS = GetMultiSelectEntryList(FindAll(Selecteds, GroupType.ETS));
            var EntryInfo_ITA = GetMultiSelectEntryList(FindAll(Selecteds, GroupType.ITA));
            var EntryInfo_AEV = GetMultiSelectEntryList(FindAll(Selecteds, GroupType.AEV));
            var EntryInfo_DSE = GetMultiSelectEntryList(FindAll(Selecteds, GroupType.DSE));
            var EntryInfo_FSE = GetMultiSelectEntryList(FindAll(Selecteds, GroupType.FSE));
            var EntryInfo_SAR = GetMultiSelectEntryList(FindAll(Selecteds, GroupType.SAR));
            var EntryInfo_EAR = GetMultiSelectEntryList(FindAll(Selecteds, GroupType.EAR));
            var EntryInfo_ESE = GetMultiSelectEntryList(FindAll(Selecteds, GroupType.ESE));
            var EntryInfo_EMI = GetMultiSelectEntryList(FindAll(Selecteds, GroupType.EMI));
            var EntryInfo_LIT_ENTRYS = GetMultiSelectEntryList(FindAll(Selecteds, GroupType.LIT_ENTRYS));
            var EntryInfo_LIT_GROUPS = GetMultiSelectEntryList(FindAll(Selecteds, GroupType.LIT_GROUPS));
            var EntryInfo_QuadCustom = GetMultiSelectEntryList(FindAll(Selecteds, GroupType.QUAD_CUSTOM));
            var EntryInfo_EFF_Table0 = GetMultiSelectEntryList(FindAll(Selecteds, GroupType.EFF_Table0));
            var EntryInfo_EFF_Table1 = GetMultiSelectEntryList(FindAll(Selecteds, GroupType.EFF_Table1));
            var EntryInfo_EFF_Table2 = GetMultiSelectEntryList(FindAll(Selecteds, GroupType.EFF_Table2));
            var EntryInfo_EFF_Table3 = GetMultiSelectEntryList(FindAll(Selecteds, GroupType.EFF_Table3));
            var EntryInfo_EFF_Table4 = GetMultiSelectEntryList(FindAll(Selecteds, GroupType.EFF_Table4));
            var EntryInfo_EFF_Table6 = GetMultiSelectEntryList(FindAll(Selecteds, GroupType.EFF_Table6));
            var EntryInfo_EFF_Table7 = GetMultiSelectEntryList(FindAll(Selecteds, GroupType.EFF_Table7_Effect_0));
            var EntryInfo_EFF_Table8 = GetMultiSelectEntryList(FindAll(Selecteds, GroupType.EFF_Table8_Effect_1));
            var EntryInfo_EFF_Table9 = GetMultiSelectEntryList(FindAll(Selecteds, GroupType.EFF_Table9));
            var EntryInfo_EFF_EffectEntry = GetMultiSelectEntryList(FindAll(Selecteds, GroupType.EFF_EffectEntry));


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
            MultiSelectInfo.AddRange(EntryInfo_EFF_Table0);
            MultiSelectInfo.AddRange(EntryInfo_EFF_Table1);
            MultiSelectInfo.AddRange(EntryInfo_EFF_Table2);
            MultiSelectInfo.AddRange(EntryInfo_EFF_Table3);
            MultiSelectInfo.AddRange(EntryInfo_EFF_Table4);
            MultiSelectInfo.AddRange(EntryInfo_EFF_Table6);
            MultiSelectInfo.AddRange(EntryInfo_EFF_Table7);
            MultiSelectInfo.AddRange(EntryInfo_EFF_Table8);
            MultiSelectInfo.AddRange(EntryInfo_EFF_Table9);
            MultiSelectInfo.AddRange(EntryInfo_EFF_EffectEntry);


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
            List_EFF_EffectEntry.AddRange(EntryInfo_EFF_EffectEntry);
            List_EFF_Table0.AddRange(EntryInfo_EFF_Table0);
            List_EFF_Table1.AddRange(EntryInfo_EFF_Table1);
            List_EFF_Table2.AddRange(EntryInfo_EFF_Table2);
            List_EFF_Table3.AddRange(EntryInfo_EFF_Table3);
            List_EFF_Table4.AddRange(EntryInfo_EFF_Table4);
            List_EFF_Table6.AddRange(EntryInfo_EFF_Table6);
            List_EFF_Table7.AddRange(EntryInfo_EFF_Table7);
            List_EFF_Table8.AddRange(EntryInfo_EFF_Table8);
            List_EFF_Table9.AddRange(EntryInfo_EFF_Table9);

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
            ChangePropertyIsBrowsable(nameof(List_EFF_EffectEntry), List_EFF_EffectEntry.Count != 0);
            ChangePropertyIsBrowsable(nameof(List_EFF_Table0), List_EFF_Table0.Count != 0);
            ChangePropertyIsBrowsable(nameof(List_EFF_Table1), List_EFF_Table1.Count != 0);
            ChangePropertyIsBrowsable(nameof(List_EFF_Table2), List_EFF_Table2.Count != 0);
            ChangePropertyIsBrowsable(nameof(List_EFF_Table3), List_EFF_Table3.Count != 0);
            ChangePropertyIsBrowsable(nameof(List_EFF_Table4), List_EFF_Table4.Count != 0);
            ChangePropertyIsBrowsable(nameof(List_EFF_Table6), List_EFF_Table6.Count != 0);
            ChangePropertyIsBrowsable(nameof(List_EFF_Table7), List_EFF_Table7.Count != 0);
            ChangePropertyIsBrowsable(nameof(List_EFF_Table8), List_EFF_Table8.Count != 0);
            ChangePropertyIsBrowsable(nameof(List_EFF_Table9), List_EFF_Table9.Count != 0);

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


        [CustomCategory(aLang.MultiSelectCategory)]
        [DisplayName("ERROR")]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [TypeConverter(typeof(GenericCollectionConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(14, 0)]
        public GenericCollection List_EFF_Table0 { get; private set; }

        [CustomCategory(aLang.MultiSelectCategory)]
        [DisplayName("ERROR")]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [TypeConverter(typeof(GenericCollectionConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(15, 0)]
        public GenericCollection List_EFF_Table1 { get; private set; }

        [CustomCategory(aLang.MultiSelectCategory)]
        [DisplayName("ERROR")]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [TypeConverter(typeof(GenericCollectionConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(16, 0)]
        public GenericCollection List_EFF_Table2 { get; private set; }

        [CustomCategory(aLang.MultiSelectCategory)]
        [DisplayName("ERROR")]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [TypeConverter(typeof(GenericCollectionConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(17, 0)]
        public GenericCollection List_EFF_Table3 { get; private set; }

        [CustomCategory(aLang.MultiSelectCategory)]
        [DisplayName("ERROR")]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [TypeConverter(typeof(GenericCollectionConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(18, 0)]
        public GenericCollection List_EFF_Table4 { get; private set; }


        [CustomCategory(aLang.MultiSelectCategory)]
        [DisplayName("ERROR")]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [TypeConverter(typeof(GenericCollectionConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(19, 0)]
        public GenericCollection List_EFF_Table6 { get; private set; }

        [CustomCategory(aLang.MultiSelectCategory)]
        [DisplayName("ERROR")]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [TypeConverter(typeof(GenericCollectionConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(20, 0)]
        public GenericCollection List_EFF_Table7 { get; private set; }

        [CustomCategory(aLang.MultiSelectCategory)]
        [DisplayName("ERROR")]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [TypeConverter(typeof(GenericCollectionConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(21, 0)]
        public GenericCollection List_EFF_Table8 { get; private set; }

        [CustomCategory(aLang.MultiSelectCategory)]
        [DisplayName("ERROR")]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [TypeConverter(typeof(GenericCollectionConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(22, 0)]
        public GenericCollection List_EFF_EffectEntry { get; private set; }

        [CustomCategory(aLang.MultiSelectCategory)]
        [DisplayName("ERROR")]
        [Description("")]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [TypeConverter(typeof(GenericCollectionConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [DynamicTypeDescriptor.Id(23, 0)]
        public GenericCollection List_EFF_Table9 { get; private set; }
    }

}
