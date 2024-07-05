using Re4QuadExtremeEditor.src.Class.Enums;
using Re4QuadExtremeEditor.src.Class.Interfaces;
using Re4QuadExtremeEditor.src.Class.MyProperty.CustomAttribute;
using Re4QuadExtremeEditor.src.Class.MyProperty.CustomCollection;
using Re4QuadExtremeEditor.src.Class.MyProperty.CustomTypeConverter;
using Re4QuadExtremeEditor.src.Class.MyProperty.CustomUITypeEditor;
using Re4QuadExtremeEditor.src.Class.ObjMethods;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;

namespace Re4QuadExtremeEditor.src.Class.MyProperty
{

    [DefaultProperty("Order")]
    [TypeConverter(typeof(GenericConverter))]
    public class EnemyProperty : GenericProperty, IInternalID
    {
        private ushort InternalID = ushort.MaxValue;
        private GroupType groupType = GroupType.ESL;

        private EnemyMethods Methods = null;
        private UpdateMethods updateMethods = null;

        public ushort GetInternalID()
        {
            return InternalID;
        }

        public GroupType GetGroupType()
        {
            return groupType;
        }

        public EnemyProperty(EnemyProperty prop, bool ForMultiSelection = false) 
        {
            EnemyPropertyConstructor(prop.InternalID, prop.updateMethods, prop.Methods, ForMultiSelection);
        }

        public EnemyProperty(ushort InternalID, UpdateMethods updateMethods, EnemyMethods Methods, bool ForMultiSelection = false) : base()
        {
            EnemyPropertyConstructor(InternalID, updateMethods, Methods, ForMultiSelection);
        }

        private void EnemyPropertyConstructor(ushort InternalID, UpdateMethods updateMethods, EnemyMethods Methods, bool ForMultiSelection = false)
        {
            this.InternalID = InternalID;
            this.Methods = Methods;
            this.updateMethods = updateMethods;

            if (!ForMultiSelection)
            {
                SetThis(this);
            }
           
            // ForMultiSelection // a remover
            ChangePropertyIsBrowsable("Category_OrderCategory", false);
            ChangePropertyIsBrowsable("Category_AssociatedSpecialEventCategory", false);
            ChangePropertyIsBrowsable("Category_LineArrayCategory", false);
            ChangePropertyIsBrowsable("Category_EnemyCategory", false);
        }

        #region Category Ids
        private const int CategoryID0_Order = 0;
        private const int CategoryID1_AssociatedSpecialEvent = 1;
        private const int CategoryID2_LineArray = 2;
        private const int CategoryID3_Enemy = 3;
        #endregion


        #region Category property

        [CustomCategory(aLang.Enemy_OrderCategory)]
        [DisplayName("")]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(0, CategoryID0_Order)]
        public string Category_OrderCategory { get => Lang.GetAttributeText(aLang.Enemy_OrderCategory); set { } }

        [CustomCategory(aLang.Enemy_AssociatedSpecialEventCategory)]
        [DisplayName("")]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(2, CategoryID1_AssociatedSpecialEvent)]
        public string Category_AssociatedSpecialEventCategory { get => Lang.GetAttributeText(aLang.Enemy_AssociatedSpecialEventCategory); set { } }

        [CustomCategory(aLang.Enemy_LineArrayCategory)]
        [DisplayName("")]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(7, CategoryID2_LineArray)]
        public string Category_LineArrayCategory { get => Lang.GetAttributeText(aLang.Enemy_LineArrayCategory); set { } }


        [CustomCategory(aLang.EnemyCategory)]
        [DisplayName("")]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(9, CategoryID3_Enemy)]
        public string Category_EnemyCategory { get => Lang.GetAttributeText(aLang.EnemyCategory); set { } }

        #endregion

        #region parte1

        [CustomCategory(aLang.Enemy_OrderCategory)]
        [CustomDisplayName(aLang.Enemy_OrderDisplayName)]
        [CustomDescription(aLang.Enemy_OrderDescription)]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(1, CategoryID0_Order)]
        public string Order { get => "0x" + GetInternalID().ToString("X2"); }

        [CustomCategory(aLang.Enemy_AssociatedSpecialEventCategory)]
        [CustomDisplayName(aLang.Enemy_AssociatedSpecialEventTypeDisplayName)]
        [CustomDescription(aLang.Enemy_AssociatedSpecialEventTypeDescription)]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(3, CategoryID1_AssociatedSpecialEvent)]
        public string AssociatedSpecialEventType { get { return DataBase.Extras.AssociatedSpecialEventType(RefInteractionType.Enemy, InternalID); } }

        [CustomCategory(aLang.Enemy_AssociatedSpecialEventCategory)]
        [CustomDisplayName(aLang.Enemy_AssociatedSpecialEventFromSpecialIndexDisplayName)]
        [CustomDescription(aLang.Enemy_AssociatedSpecialEventFromSpecialIndexFromDescription)]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(4, CategoryID1_AssociatedSpecialEvent)]
        public string AssociatedSpecialEventFromSpecialIndex { get { return DataBase.Extras.AssociatedSpecialEventFromSpecialIndex(RefInteractionType.Enemy, InternalID); } }

        [CustomCategory(aLang.Enemy_AssociatedSpecialEventCategory)]
        [CustomDisplayName(aLang.Enemy_AssociatedSpecialEventObjNameDisplayName)]
        [CustomDescription(aLang.Enemy_AssociatedSpecialEventObjNameDescription)]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(5, CategoryID1_AssociatedSpecialEvent)]
        public string AssociatedSpecialEventObjName { get { return DataBase.Extras.AssociatedSpecialEventObjName(RefInteractionType.Enemy, InternalID); } }    

        [CustomCategory(aLang.Enemy_AssociatedSpecialEventCategory)]
        [CustomDisplayName(aLang.Enemy_AssociatedSpecialEventFromFileDisplayName)]
        [CustomDescription(aLang.Enemy_AssociatedSpecialEventFromFileFromDescription)]
        [DefaultValue(null)]
        [ReadOnly(true)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(6, CategoryID1_AssociatedSpecialEvent)]
        public string AssociatedSpecialEventFromFile { get { return DataBase.Extras.AssociatedSpecialEventFromFile(RefInteractionType.Enemy, InternalID); } }


        [CustomCategory(aLang.Enemy_LineArrayCategory)]
        [CustomDisplayName(aLang.Enemy_LineArrayDisplayName)]
        [CustomDescription(aLang.Enemy_LineArrayDescription)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(8, CategoryID2_LineArray)]
        public byte[] Line 
        { 
            get => Methods.ReturnLine(InternalID); 
            set 
            {
                byte[] _set = new byte[32];
                byte[] insert = value.Take(32).ToArray();
                Line.CopyTo(_set, 0);
                insert.CopyTo(_set, 0);
                Methods.SetLine(InternalID, _set);
                updateMethods.UpdateGL();
            } 
        }


        #endregion


        #region  // propriedades do imimigo

        [CustomCategory(aLang.EnemyCategory)]
        [CustomDisplayName(aLang.ESL_ENABLE_Byte_Name)]
        [CustomDescription(aLang.ESL_ENABLE_Byte_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(100, CategoryID3_Enemy)]
        public byte ESL_ENABLE
        {
            get => Methods.ReturnOffset0x00Enable(InternalID);
            set
            {
                Methods.SetOffset0x00Enable(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.EnemyCategory)]
        [CustomDisplayName(aLang.ESL_ENABLE_List_Name)]
        [CustomDescription(aLang.ESL_ENABLE_Byte_Description)]
        [Editor(typeof(EnemyEnableGridComboBox), typeof(UITypeEditor))]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(200, CategoryID3_Enemy)]
        public ByteObjForListBox ESL_ENABLE_ListBox
        {
            get
            {
                byte v = Methods.ReturnOffset0x00Enable(InternalID);
                if (v == 0x00)
                {
                    return ListBoxProperty.EnemyEnableList[0x00];
                }
                else if (v == 0x01)
                {
                    return ListBoxProperty.EnemyEnableList[0x01];
                }
                else
                {
                    return new ByteObjForListBox(0xFF, "XX: " + Lang.GetAttributeText(aLang.ListBoxAnotherValue));
                }
            }
            set
            {
                if (value.ID < 0xFF)
                {
                    Methods.SetOffset0x00Enable(InternalID, value.ID);
                    updateMethods.UpdateGL();
                }
            }
        }


        [CustomCategory(aLang.EnemyCategory)]
        [CustomDisplayName(aLang.ESL_ENEMY_ID_UshotUnflip_Name)]
        [CustomDescription(aLang.ESL_ENEMY_ID_UshotUnflip_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(300, CategoryID3_Enemy)]
        public ushort ESL_ENEMY_ID
        {
            get => Methods.ReturnEnemyID(InternalID);
            set
            {
                Methods.SetEnemyID(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.EnemyCategory)]
        [CustomDisplayName(aLang.ESL_ENEMY_ID_List_Name)]
        [CustomDescription(aLang.ESL_ENEMY_ID_UshotUnflip_Description)]
        [Editor(typeof(EnemyIDGridComboBox), typeof(UITypeEditor))]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(400, CategoryID3_Enemy)]
        public UshortObjForListBox ESL_ENEMY_ID_ListBox
        {
            get
            {
                ushort v = Methods.ReturnEnemyID(InternalID);
                string sv = v.ToString("X4");
                string svff = sv[0].ToString() + sv[1].ToString() + "FF";
                ushort vff = ushort.Parse(svff, System.Globalization.NumberStyles.HexNumber);
                if (ListBoxProperty.EnemiesList.ContainsKey(v))
                {
                    return ListBoxProperty.EnemiesList[v];
                }
                else if (DataBase.EnemiesIDs.List.ContainsKey(vff) && vff != 0xFFFF)
                {
                    return new UshortObjForListBox(vff, sv[0].ToString() + sv[1].ToString() + "XX: " + DataBase.EnemiesIDs.List[vff].Description);
                }
                else
                {
                    return new UshortObjForListBox(0xFFFF, "XXXX: " + Lang.GetAttributeText(aLang.ListBoxUnknownEnemy));
                }
            }
            set
            {
                if (value.ID < 0xFFFF)
                {
                    Methods.SetEnemyID(InternalID, value.ID);
                    updateMethods.UpdateGL();
                }
            }
        }




        [CustomCategory(aLang.EnemyCategory)]
        [CustomDisplayName(aLang.ESL_HX03_Byte_Name)]
        [CustomDescription(aLang.ESL_HX03_Byte_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(500, CategoryID3_Enemy)]
        public byte ESL_HX03
        {
            get => Methods.ReturnByteFromPosition(InternalID, 0x03);
            set
            {
                Methods.SetByteFromPosition(InternalID, 0x03, value);
            }
        }

        [CustomCategory(aLang.EnemyCategory)]
        [CustomDisplayName(aLang.ESL_HX04_Byte_Name)]
        [CustomDescription(aLang.ESL_HX04_Byte_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(600, CategoryID3_Enemy)]
        public byte ESL_HX04
        {
            get => Methods.ReturnByteFromPosition(InternalID, 0x04);
            set
            {
                Methods.SetByteFromPosition(InternalID, 0x04, value);
            }
        }

        [CustomCategory(aLang.EnemyCategory)]
        [CustomDisplayName(aLang.ESL_HX05_Byte_Name)]
        [CustomDescription(aLang.ESL_HX05_Byte_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(700, CategoryID3_Enemy)]
        public byte ESL_HX05
        {
            get => Methods.ReturnByteFromPosition(InternalID, 0x05);
            set
            {
                Methods.SetByteFromPosition(InternalID, 0x05, value);
            }
        }

        [CustomCategory(aLang.EnemyCategory)]
        [CustomDisplayName(aLang.ESL_HX06_Byte_Name)]
        [CustomDescription(aLang.ESL_HX06_Byte_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(800, CategoryID3_Enemy)]
        public byte ESL_HX06
        {
            get => Methods.ReturnByteFromPosition(InternalID, 0x06);
            set
            {
                Methods.SetByteFromPosition(InternalID, 0x06, value);
            }
        }

        [CustomCategory(aLang.EnemyCategory)]
        [CustomDisplayName(aLang.ESL_HX07_Byte_Name)]
        [CustomDescription(aLang.ESL_HX07_Byte_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(900, CategoryID3_Enemy)]
        public byte ESL_HX07
        {
            get => Methods.ReturnByteFromPosition(InternalID, 0x07);
            set
            {
                Methods.SetByteFromPosition(InternalID, 0x07, value);
            }
        }


        [CustomCategory(aLang.EnemyCategory)]
        [CustomDisplayName(aLang.ESL_EnemyLifeAmount_Short_Name)]
        [CustomDescription(aLang.ESL_EnemyLifeAmount_Short_Description)]
        [TypeConverter(typeof(DecNumberTypeConverter))]
        [DecNegativeNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1000, CategoryID3_Enemy)]
        public short ESL_LIFE
        {
            get => Methods.ReturnLife(InternalID);
            set
            {
                Methods.SetLife(InternalID, value);
            }
        }


        [CustomCategory(aLang.EnemyCategory)]
        [CustomDisplayName(aLang.ESL_HX0A_Byte_Name)]
        [CustomDescription(aLang.ESL_HX0A_Byte_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1100, CategoryID3_Enemy)]
        public byte ESL_HX0A
        {
            get => Methods.ReturnByteFromPosition(InternalID, 0x0A);
            set
            {
                Methods.SetByteFromPosition(InternalID, 0x0A, value);
            }
        }

        [CustomCategory(aLang.EnemyCategory)]
        [CustomDisplayName(aLang.ESL_HX0B_Byte_Name)]
        [CustomDescription(aLang.ESL_HX0B_Byte_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1200, CategoryID3_Enemy)]
        public byte ESL_HX0B
        {
            get => Methods.ReturnByteFromPosition(InternalID, 0x0B);
            set
            {
                Methods.SetByteFromPosition(InternalID, 0x0B, value);
            }
        }


        [CustomCategory(aLang.EnemyCategory)]
        [CustomDisplayName(aLang.ESL_PositionX_Short_Name)]
        [CustomDescription(aLang.ESL_PositionX_Short_Description)]
        [TypeConverter(typeof(DecNumberTypeConverter))]
        [DecNegativeNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1300, CategoryID3_Enemy)]
        public short ESL_PositionX
        {
            get => Methods.ReturnPositionX(InternalID);
            set
            {
                Methods.SetPositionX(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.EnemyCategory)]
        [CustomDisplayName(aLang.ESL_PositionY_Short_Name)]
        [CustomDescription(aLang.ESL_PositionY_Short_Description)]
        [TypeConverter(typeof(DecNumberTypeConverter))]
        [DecNegativeNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1400, CategoryID3_Enemy)]
        public short ESL_PositionY
        {
            get => Methods.ReturnPositionY(InternalID);
            set
            {
                Methods.SetPositionY(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.EnemyCategory)]
        [CustomDisplayName(aLang.ESL_PositionZ_Short_Name)]
        [CustomDescription(aLang.ESL_PositionZ_Short_Description)]
        [TypeConverter(typeof(DecNumberTypeConverter))]
        [DecNegativeNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1500, CategoryID3_Enemy)]
        public short ESL_PositionZ
        {
            get => Methods.ReturnPositionZ(InternalID);
            set
            {
                Methods.SetPositionZ(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.EnemyCategory)]
        [CustomDisplayName(aLang.ESL_AngleX_Short_Name)]
        [CustomDescription(aLang.ESL_AngleX_Short_Description)]
        [TypeConverter(typeof(DecNumberTypeConverter))]
        [DecNegativeNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1600, CategoryID3_Enemy)]
        public short ESL_RotationX
        {
            get => Methods.ReturnRotationX(InternalID);
            set
            {
                Methods.SetRotationX(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.EnemyCategory)]
        [CustomDisplayName(aLang.ESL_AngleY_Short_Name)]
        [CustomDescription(aLang.ESL_AngleY_Short_Description)]
        [TypeConverter(typeof(DecNumberTypeConverter))]
        [DecNegativeNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1700, CategoryID3_Enemy)]
        public short ESL_RotationY
        {
            get => Methods.ReturnRotationY(InternalID);
            set
            {
                Methods.SetRotationY(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.EnemyCategory)]
        [CustomDisplayName(aLang.ESL_AngleZ_Short_Name)]
        [CustomDescription(aLang.ESL_AngleZ_Short_Description)]
        [TypeConverter(typeof(DecNumberTypeConverter))]
        [DecNegativeNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1800, CategoryID3_Enemy)]
        public short ESL_RotationZ
        {
            get => Methods.ReturnRotationZ(InternalID);
            set
            {
                Methods.SetRotationZ(InternalID, value);
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.EnemyCategory)]
        [CustomDisplayName(aLang.ESL_ROOM_ID_Ushort_Name)]
        [CustomDescription(aLang.ESL_ROOM_ID_Ushort_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(1900, CategoryID3_Enemy)]
        public ushort ESL_ROOM_ID
        {
            get => Methods.ReturnRoomID(InternalID);
            set
            {
                Methods.SetRoomID(InternalID, value);
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.EnemyCategory)]
        [CustomDisplayName(aLang.ESL_HX1A_Byte_Name)]
        [CustomDescription(aLang.ESL_HX1A_Byte_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(2000, CategoryID3_Enemy)]
        public byte ESL_HX1A
        {
            get => Methods.ReturnByteFromPosition(InternalID, 0x1A);
            set
            {
                Methods.SetByteFromPosition(InternalID, 0x1A, value);
            }
        }

        [CustomCategory(aLang.EnemyCategory)]
        [CustomDisplayName(aLang.ESL_HX1B_Byte_Name)]
        [CustomDescription(aLang.ESL_HX1B_Byte_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(2100, CategoryID3_Enemy)]
        public byte ESL_HX1B
        {
            get => Methods.ReturnByteFromPosition(InternalID, 0x1B);
            set
            {
                Methods.SetByteFromPosition(InternalID, 0x1B, value);
            }
        }

        [CustomCategory(aLang.EnemyCategory)]
        [CustomDisplayName(aLang.ESL_HX1C_Byte_Name)]
        [CustomDescription(aLang.ESL_HX1C_Byte_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(2200, CategoryID3_Enemy)]
        public byte ESL_HX1C
        {
            get => Methods.ReturnByteFromPosition(InternalID, 0x1C);
            set
            {
                Methods.SetByteFromPosition(InternalID, 0x1C, value);
            }
        }

        [CustomCategory(aLang.EnemyCategory)]
        [CustomDisplayName(aLang.ESL_HX1D_Byte_Name)]
        [CustomDescription(aLang.ESL_HX1D_Byte_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(2300, CategoryID3_Enemy)]
        public byte ESL_HX1D
        {
            get => Methods.ReturnByteFromPosition(InternalID, 0x1D);
            set
            {
                Methods.SetByteFromPosition(InternalID, 0x1D, value);
            }
        }

        [CustomCategory(aLang.EnemyCategory)]
        [CustomDisplayName(aLang.ESL_HX1E_Byte_Name)]
        [CustomDescription(aLang.ESL_HX1E_Byte_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(2400, CategoryID3_Enemy)]
        public byte ESL_HX1E
        {
            get => Methods.ReturnByteFromPosition(InternalID, 0x1E);
            set
            {
                Methods.SetByteFromPosition(InternalID, 0x1E, value);
            }
        }

        [CustomCategory(aLang.EnemyCategory)]
        [CustomDisplayName(aLang.ESL_HX1F_Byte_Name)]
        [CustomDescription(aLang.ESL_HX1F_Byte_Description)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(2500, CategoryID3_Enemy)]
        public byte ESL_HX1F
        {
            get => Methods.ReturnByteFromPosition(InternalID, 0x1F);
            set
            {
                Methods.SetByteFromPosition(InternalID, 0x1F, value);
            }
        }

        #endregion


        #region Search Methods


        public ushort ReturnUshortFirstSearchSelect() 
        {
            ushort v = Methods.ReturnEnemyID(InternalID);
            string sv = v.ToString("X4");
            string svff = sv[0].ToString() + sv[1].ToString() + "00";
            ushort vff = ushort.Parse(svff, System.Globalization.NumberStyles.HexNumber);
            if (ListBoxProperty.EnemiesList.ContainsKey(v))
            {
                return v;
            }
            else if (ListBoxProperty.EnemiesList.ContainsKey(vff))
            {
                return vff;
            }
            return v;
        }

        public void Searched(object obj) 
        {
            if (obj is UshortObjForListBox ushortObj)
            {
                Methods.SetEnemyID(InternalID, ushortObj.ID);
                updateMethods.UpdateTreeViewObjs();
                updateMethods.UpdatePropertyGrid();
                updateMethods.UpdateGL();
            }
        }

        #endregion
    }


}
