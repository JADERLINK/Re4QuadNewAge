using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing.Design;
using Re4QuadExtremeEditor.src.Class.Enums;
using Re4QuadExtremeEditor.src.Class.Interfaces;
using Re4QuadExtremeEditor.src.Class.ObjMethods;
using Re4QuadExtremeEditor.src.Class.MyProperty.CustomAttribute;
using Re4QuadExtremeEditor.src.Class.MyProperty.CustomTypeConverter;
using Re4QuadExtremeEditor.src.Class.MyProperty.CustomUITypeEditor;
using Re4QuadExtremeEditor.src.Class.MyProperty.CustomCollection;

namespace Re4QuadExtremeEditor.src.Class.MyProperty
{
    public abstract class BaseTriggerZoneProperty : BaseProperty, IInternalID
    {
        public abstract ushort GetInternalID();

        public abstract GroupType GetGroupType();

        protected abstract BaseTriggerZoneMethods GetTriggerZoneMethods();

        protected const int CategoryID_TriggerZone = 10;

        protected UpdateMethods updateMethods = null;

        protected void SetTriggerZoneDescription(int startOffset) 
        {
            ChangePropertyDescription(nameof(Unknown_GH), Lang.GetAttributeText(aLang.Unknown_GH_Byte_Description)
                .Replace("<<Offset1>>", ReturnOffset(startOffset, 0x00)));
            ChangePropertyDescription(nameof(Category), Lang.GetAttributeText(aLang.TriggerZoneCategory_Byte_Description)
                .Replace("<<Offset1>>", ReturnOffset(startOffset, 0x01)));
            ChangePropertyDescription(nameof(Category_ListBox), Lang.GetAttributeText(aLang.TriggerZoneCategory_Byte_Description)
                .Replace("<<Offset1>>", ReturnOffset(startOffset, 0x01)));
            ChangePropertyDescription(nameof(Unknown_GK), Lang.GetAttributeText(aLang.Unknown_GK_ByteArray2_Description)
                .Replace("<<Offset1>>", ReturnOffset(startOffset, 0x02))
                .Replace("<<Offset2>>", ReturnOffset(startOffset, 0x03)));

            ChangePropertyDescription(nameof(TriggerZoneTrueY), Lang.GetAttributeText(aLang.TriggerZoneTrueY_Description)
                .Replace("<<Offset1>>", ReturnOffset(startOffset, 0x07))
                .Replace("<<Offset2>>", ReturnOffset(startOffset, 0x06))
                .Replace("<<Offset3>>", ReturnOffset(startOffset, 0x05))
                .Replace("<<Offset4>>", ReturnOffset(startOffset, 0x04)));
            ChangePropertyDescription(nameof(TriggerZoneTrueY_Hex), Lang.GetAttributeText(aLang.TriggerZoneTrueY_Description)
                .Replace("<<Offset1>>", ReturnOffset(startOffset, 0x07))
                .Replace("<<Offset2>>", ReturnOffset(startOffset, 0x06))
                .Replace("<<Offset3>>", ReturnOffset(startOffset, 0x05))
                .Replace("<<Offset4>>", ReturnOffset(startOffset, 0x04)));
            ChangePropertyDescription(nameof(TriggerZoneMoreHeight), Lang.GetAttributeText(aLang.TriggerZoneMoreHeight_Description)
                .Replace("<<Offset1>>", ReturnOffset(startOffset, 0x0B))
                .Replace("<<Offset2>>", ReturnOffset(startOffset, 0x0A))
                .Replace("<<Offset3>>", ReturnOffset(startOffset, 0x09))
                .Replace("<<Offset4>>", ReturnOffset(startOffset, 0x08)));
            ChangePropertyDescription(nameof(TriggerZoneMoreHeight_Hex), Lang.GetAttributeText(aLang.TriggerZoneMoreHeight_Description)
                .Replace("<<Offset1>>", ReturnOffset(startOffset, 0x0B))
                .Replace("<<Offset2>>", ReturnOffset(startOffset, 0x0A))
                .Replace("<<Offset3>>", ReturnOffset(startOffset, 0x09))
                .Replace("<<Offset4>>", ReturnOffset(startOffset, 0x08)));
            ChangePropertyDescription(nameof(TriggerZoneCircleRadius), Lang.GetAttributeText(aLang.TriggerZoneCircleRadius_Description)
                .Replace("<<Offset1>>", ReturnOffset(startOffset, 0x0F))
                .Replace("<<Offset2>>", ReturnOffset(startOffset, 0x0E))
                .Replace("<<Offset3>>", ReturnOffset(startOffset, 0x0D))
                .Replace("<<Offset4>>", ReturnOffset(startOffset, 0x0C)));
            ChangePropertyDescription(nameof(TriggerZoneCircleRadius_Hex), Lang.GetAttributeText(aLang.TriggerZoneCircleRadius_Description)
                .Replace("<<Offset1>>", ReturnOffset(startOffset, 0x0F))
                .Replace("<<Offset2>>", ReturnOffset(startOffset, 0x0E))
                .Replace("<<Offset3>>", ReturnOffset(startOffset, 0x0D))
                .Replace("<<Offset4>>", ReturnOffset(startOffset, 0x0C)));
            ChangePropertyDescription(nameof(TriggerZoneCorner0_X), Lang.GetAttributeText(aLang.TriggerZoneCorner0_X_Description)
                .Replace("<<Offset1>>", ReturnOffset(startOffset, 0x13))
                .Replace("<<Offset2>>", ReturnOffset(startOffset, 0x12))
                .Replace("<<Offset3>>", ReturnOffset(startOffset, 0x11))
                .Replace("<<Offset4>>", ReturnOffset(startOffset, 0x10))); 
            ChangePropertyDescription(nameof(TriggerZoneCorner0_X_Hex), Lang.GetAttributeText(aLang.TriggerZoneCorner0_X_Description)
                .Replace("<<Offset1>>", ReturnOffset(startOffset, 0x13))
                .Replace("<<Offset2>>", ReturnOffset(startOffset, 0x12))
                .Replace("<<Offset3>>", ReturnOffset(startOffset, 0x11))
                .Replace("<<Offset4>>", ReturnOffset(startOffset, 0x10)));
            ChangePropertyDescription(nameof(TriggerZoneCorner0_Z), Lang.GetAttributeText(aLang.TriggerZoneCorner0_Z_Description)
                .Replace("<<Offset1>>", ReturnOffset(startOffset, 0x17))
                .Replace("<<Offset2>>", ReturnOffset(startOffset, 0x16))
                .Replace("<<Offset3>>", ReturnOffset(startOffset, 0x15))
                .Replace("<<Offset4>>", ReturnOffset(startOffset, 0x14)));
            ChangePropertyDescription(nameof(TriggerZoneCorner0_Z_Hex), Lang.GetAttributeText(aLang.TriggerZoneCorner0_Z_Description)
                .Replace("<<Offset1>>", ReturnOffset(startOffset, 0x17))
                .Replace("<<Offset2>>", ReturnOffset(startOffset, 0x16))
                .Replace("<<Offset3>>", ReturnOffset(startOffset, 0x15))
                .Replace("<<Offset4>>", ReturnOffset(startOffset, 0x14)));
            ChangePropertyDescription(nameof(TriggerZoneCorner1_X), Lang.GetAttributeText(aLang.TriggerZoneCorner1_X_Description)
                .Replace("<<Offset1>>", ReturnOffset(startOffset, 0x1B))
                .Replace("<<Offset2>>", ReturnOffset(startOffset, 0x1A))
                .Replace("<<Offset3>>", ReturnOffset(startOffset, 0x19))
                .Replace("<<Offset4>>", ReturnOffset(startOffset, 0x18)));
            ChangePropertyDescription(nameof(TriggerZoneCorner1_X_Hex), Lang.GetAttributeText(aLang.TriggerZoneCorner1_X_Description)
                .Replace("<<Offset1>>", ReturnOffset(startOffset, 0x1B))
                .Replace("<<Offset2>>", ReturnOffset(startOffset, 0x1A))
                .Replace("<<Offset3>>", ReturnOffset(startOffset, 0x19))
                .Replace("<<Offset4>>", ReturnOffset(startOffset, 0x18)));
            ChangePropertyDescription(nameof(TriggerZoneCorner1_Z), Lang.GetAttributeText(aLang.TriggerZoneCorner1_Z_Description)
                .Replace("<<Offset1>>", ReturnOffset(startOffset, 0x1F))
                .Replace("<<Offset2>>", ReturnOffset(startOffset, 0x1E))
                .Replace("<<Offset3>>", ReturnOffset(startOffset, 0x1D))
                .Replace("<<Offset4>>", ReturnOffset(startOffset, 0x1C))); 
            ChangePropertyDescription(nameof(TriggerZoneCorner1_Z_Hex), Lang.GetAttributeText(aLang.TriggerZoneCorner1_Z_Description)
                .Replace("<<Offset1>>", ReturnOffset(startOffset, 0x1F))
                .Replace("<<Offset2>>", ReturnOffset(startOffset, 0x1E))
                .Replace("<<Offset3>>", ReturnOffset(startOffset, 0x1D))
                .Replace("<<Offset4>>", ReturnOffset(startOffset, 0x1C)));
            ChangePropertyDescription(nameof(TriggerZoneCorner2_X), Lang.GetAttributeText(aLang.TriggerZoneCorner2_X_Description)
                .Replace("<<Offset1>>", ReturnOffset(startOffset, 0x23))
                .Replace("<<Offset2>>", ReturnOffset(startOffset, 0x22))
                .Replace("<<Offset3>>", ReturnOffset(startOffset, 0x21))
                .Replace("<<Offset4>>", ReturnOffset(startOffset, 0x20))); 
            ChangePropertyDescription(nameof(TriggerZoneCorner2_X_Hex), Lang.GetAttributeText(aLang.TriggerZoneCorner2_X_Description)
                .Replace("<<Offset1>>", ReturnOffset(startOffset, 0x23))
                .Replace("<<Offset2>>", ReturnOffset(startOffset, 0x22))
                .Replace("<<Offset3>>", ReturnOffset(startOffset, 0x21))
                .Replace("<<Offset4>>", ReturnOffset(startOffset, 0x20)));
            ChangePropertyDescription(nameof(TriggerZoneCorner2_Z), Lang.GetAttributeText(aLang.TriggerZoneCorner2_Z_Description)
                .Replace("<<Offset1>>", ReturnOffset(startOffset, 0x27))
                .Replace("<<Offset2>>", ReturnOffset(startOffset, 0x26))
                .Replace("<<Offset3>>", ReturnOffset(startOffset, 0x25))
                .Replace("<<Offset4>>", ReturnOffset(startOffset, 0x24))); 
            ChangePropertyDescription(nameof(TriggerZoneCorner2_Z_Hex), Lang.GetAttributeText(aLang.TriggerZoneCorner2_Z_Description)
                .Replace("<<Offset1>>", ReturnOffset(startOffset, 0x27))
                .Replace("<<Offset2>>", ReturnOffset(startOffset, 0x26))
                .Replace("<<Offset3>>", ReturnOffset(startOffset, 0x25))
                .Replace("<<Offset4>>", ReturnOffset(startOffset, 0x24)));
            ChangePropertyDescription(nameof(TriggerZoneCorner3_X), Lang.GetAttributeText(aLang.TriggerZoneCorner3_X_Description)
                .Replace("<<Offset1>>", ReturnOffset(startOffset, 0x2B))
                .Replace("<<Offset2>>", ReturnOffset(startOffset, 0x2A))
                .Replace("<<Offset3>>", ReturnOffset(startOffset, 0x29))
                .Replace("<<Offset4>>", ReturnOffset(startOffset, 0x28))); 
            ChangePropertyDescription(nameof(TriggerZoneCorner3_X_Hex), Lang.GetAttributeText(aLang.TriggerZoneCorner3_X_Description)
                .Replace("<<Offset1>>", ReturnOffset(startOffset, 0x2B))
                .Replace("<<Offset2>>", ReturnOffset(startOffset, 0x2A))
                .Replace("<<Offset3>>", ReturnOffset(startOffset, 0x29))
                .Replace("<<Offset4>>", ReturnOffset(startOffset, 0x28)));
            ChangePropertyDescription(nameof(TriggerZoneCorner3_Z), Lang.GetAttributeText(aLang.TriggerZoneCorner3_Z_Description)
                .Replace("<<Offset1>>", ReturnOffset(startOffset, 0x2F))
                .Replace("<<Offset2>>", ReturnOffset(startOffset, 0x2E))
                .Replace("<<Offset3>>", ReturnOffset(startOffset, 0x2D))
                .Replace("<<Offset4>>", ReturnOffset(startOffset, 0x2C))); 
            ChangePropertyDescription(nameof(TriggerZoneCorner3_Z_Hex), Lang.GetAttributeText(aLang.TriggerZoneCorner3_Z_Description)
                .Replace("<<Offset1>>", ReturnOffset(startOffset, 0x2F))
                .Replace("<<Offset2>>", ReturnOffset(startOffset, 0x2E))
                .Replace("<<Offset3>>", ReturnOffset(startOffset, 0x2D))
                .Replace("<<Offset4>>", ReturnOffset(startOffset, 0x2C)));

        }

        private string ReturnOffset(int offset, int plus) 
        {
            return "0x" + (offset + plus).ToString("X2");
        }

        protected void SetTriggerZoneFloatType(bool IsHex) 
        {
            ChangePropertyIsBrowsable(nameof(TriggerZoneTrueY), !IsHex);
            ChangePropertyIsBrowsable(nameof(TriggerZoneMoreHeight), !IsHex);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCircleRadius), !IsHex);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner0_X), !IsHex);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner0_Z), !IsHex);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner1_X), !IsHex);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner1_Z), !IsHex);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner2_X), !IsHex);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner2_Z), !IsHex);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner3_X), !IsHex);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner3_Z), !IsHex);

            ChangePropertyIsBrowsable(nameof(TriggerZoneTrueY_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(TriggerZoneMoreHeight_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCircleRadius_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner0_X_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner0_Z_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner1_X_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner1_Z_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner2_X_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner2_Z_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner3_X_Hex), IsHex);
            ChangePropertyIsBrowsable(nameof(TriggerZoneCorner3_Z_Hex), IsHex);
        }


        #region SpecialTriggerZoneCategory part1

        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.Unknown_GH_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(17, CategoryID_TriggerZone)]
        public byte Unknown_GH
        {
            get => GetTriggerZoneMethods().ReturnUnknown_GH(GetInternalID());
            set
            {
                GetTriggerZoneMethods().SetUnknown_GH(GetInternalID(), value);
            }
        }

        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCategory_Byte_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(18, CategoryID_TriggerZone)]
        public byte Category
        {
            get => GetTriggerZoneMethods().ReturnCategory(GetInternalID());
            set
            {
                GetTriggerZoneMethods().SetCategory(GetInternalID(), value);
                updateMethods.UpdateMoveObjSelection();
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCategory_List_DisplayName)]
        [Editor(typeof(SpecialZoneCategoryGridComboBox), typeof(UITypeEditor))]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [DynamicTypeDescriptor.Id(19, CategoryID_TriggerZone)]
        public ByteObjForListBox Category_ListBox
        {
            get
            {
                byte v = GetTriggerZoneMethods().ReturnCategory(GetInternalID());
                if (ListBoxProperty.SpecialZoneCategoryList.ContainsKey(v) && v != 0xFF)
                {
                    return ListBoxProperty.SpecialZoneCategoryList[v];
                }
                else
                {
                    return new ByteObjForListBox(0xFF, "XX: " + Lang.GetAttributeText(aLang.ListBoxSpecialZoneCategoryAnotherValue));
                }
            }
            set
            {
                if (value.ID < 0xFF)
                {
                    GetTriggerZoneMethods().SetCategory(GetInternalID(), value.ID);
                    updateMethods.UpdateMoveObjSelection();
                    updateMethods.UpdateOrbitCamera();
                    updateMethods.UpdateGL();
                }
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.Unknown_GK_ByteArray2_DisplayName)]
        [TypeConverter(typeof(ByteArrayTypeConverter))]
        [Editor(typeof(NoneUITypeEditor), typeof(UITypeEditor))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(20, CategoryID_TriggerZone)]
        public byte[] Unknown_GK
        {
            get => GetTriggerZoneMethods().ReturnUnknown_GK(GetInternalID());
            set
            {
                GetTriggerZoneMethods().SetUnknown_GK(GetInternalID(), GetNewByteArrayValue(value));
            }
        }

        #endregion

        #region SpecialTriggerZoneCategory float

        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneTrueY_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(21, CategoryID_TriggerZone)]
        public float TriggerZoneTrueY
        {
            get => GetTriggerZoneMethods().ReturnTriggerZoneTrueY(GetInternalID());
            set
            {
                GetTriggerZoneMethods().SetTriggerZoneTrueY(GetInternalID(), value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneMoreHeight_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(22, CategoryID_TriggerZone)]
        public float TriggerZoneMoreHeight
        {
            get => GetTriggerZoneMethods().ReturnTriggerZoneMoreHeight(GetInternalID());
            set
            {
                GetTriggerZoneMethods().SetTriggerZoneMoreHeight(GetInternalID(), value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCircleRadius_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(23, CategoryID_TriggerZone)]
        public float TriggerZoneCircleRadius
        {
            get => GetTriggerZoneMethods().ReturnTriggerZoneCircleRadius(GetInternalID());
            set
            {
                GetTriggerZoneMethods().SetTriggerZoneCircleRadius(GetInternalID(), value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner0_X_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(24, CategoryID_TriggerZone)]
        public float TriggerZoneCorner0_X
        {
            get => GetTriggerZoneMethods().ReturnTriggerZoneCorner0_X(GetInternalID());
            set
            {
                GetTriggerZoneMethods().SetTriggerZoneCorner0_X(GetInternalID(), value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner0_Z_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(25, CategoryID_TriggerZone)]
        public float TriggerZoneCorner0_Z
        {
            get => GetTriggerZoneMethods().ReturnTriggerZoneCorner0_Z(GetInternalID());
            set
            {
                GetTriggerZoneMethods().SetTriggerZoneCorner0_Z(GetInternalID(), value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner1_X_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(26, CategoryID_TriggerZone)]
        public float TriggerZoneCorner1_X
        {
            get => GetTriggerZoneMethods().ReturnTriggerZoneCorner1_X(GetInternalID());
            set
            {
                GetTriggerZoneMethods().SetTriggerZoneCorner1_X(GetInternalID(), value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner1_Z_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(27, CategoryID_TriggerZone)]
        public float TriggerZoneCorner1_Z
        {
            get => GetTriggerZoneMethods().ReturnTriggerZoneCorner1_Z(GetInternalID());
            set
            {
                GetTriggerZoneMethods().SetTriggerZoneCorner1_Z(GetInternalID(), value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner2_X_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(28, CategoryID_TriggerZone)]
        public float TriggerZoneCorner2_X
        {
            get => GetTriggerZoneMethods().ReturnTriggerZoneCorner2_X(GetInternalID());
            set
            {
                GetTriggerZoneMethods().SetTriggerZoneCorner2_X(GetInternalID(), value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner2_Z_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(29, CategoryID_TriggerZone)]
        public float TriggerZoneCorner2_Z
        {
            get => GetTriggerZoneMethods().ReturnTriggerZoneCorner2_Z(GetInternalID());
            set
            {
                GetTriggerZoneMethods().SetTriggerZoneCorner2_Z(GetInternalID(), value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner3_X_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(30, CategoryID_TriggerZone)]
        public float TriggerZoneCorner3_X
        {
            get => GetTriggerZoneMethods().ReturnTriggerZoneCorner3_X(GetInternalID());
            set
            {
                GetTriggerZoneMethods().SetTriggerZoneCorner3_X(GetInternalID(), value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner3_Z_Float_DisplayName)]
        [TypeConverter(typeof(FloatNumberTypeConverter))]
        [FloatNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(true)]
        [AllowInMultiSelect()]
        [DynamicTypeDescriptor.Id(31, CategoryID_TriggerZone)]
        public float TriggerZoneCorner3_Z
        {
            get => GetTriggerZoneMethods().ReturnTriggerZoneCorner3_Z(GetInternalID());
            set
            {
                GetTriggerZoneMethods().SetTriggerZoneCorner3_Z(GetInternalID(), value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        #endregion

        #region SpecialTriggerZoneCategory hex

        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneTrueY_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(21, CategoryID_TriggerZone)]
        public uint TriggerZoneTrueY_Hex
        {
            get => GetTriggerZoneMethods().ReturnTriggerZoneTrueY_Hex(GetInternalID());
            set
            {
                GetTriggerZoneMethods().SetTriggerZoneTrueY_Hex(GetInternalID(), value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneMoreHeight_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(22, CategoryID_TriggerZone)]
        public uint TriggerZoneMoreHeight_Hex
        {
            get => GetTriggerZoneMethods().ReturnTriggerZoneMoreHeight_Hex(GetInternalID());
            set
            {
                GetTriggerZoneMethods().SetTriggerZoneMoreHeight_Hex(GetInternalID(), value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCircleRadius_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(23, CategoryID_TriggerZone)]
        public uint TriggerZoneCircleRadius_Hex
        {
            get => GetTriggerZoneMethods().ReturnTriggerZoneCircleRadius_Hex(GetInternalID());
            set
            {
                GetTriggerZoneMethods().SetTriggerZoneCircleRadius_Hex(GetInternalID(), value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner0_X_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(24, CategoryID_TriggerZone)]
        public uint TriggerZoneCorner0_X_Hex
        {
            get => GetTriggerZoneMethods().ReturnTriggerZoneCorner0_X_Hex(GetInternalID());
            set
            {
                GetTriggerZoneMethods().SetTriggerZoneCorner0_X_Hex(GetInternalID(), value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner0_Z_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(25, CategoryID_TriggerZone)]
        public uint TriggerZoneCorner0_Z_Hex
        {
            get => GetTriggerZoneMethods().ReturnTriggerZoneCorner0_Z_Hex(GetInternalID());
            set
            {
                GetTriggerZoneMethods().SetTriggerZoneCorner0_Z_Hex(GetInternalID(), value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner1_X_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(26, CategoryID_TriggerZone)]
        public uint TriggerZoneCorner1_X_Hex
        {
            get => GetTriggerZoneMethods().ReturnTriggerZoneCorner1_X_Hex(GetInternalID());
            set
            {
                GetTriggerZoneMethods().SetTriggerZoneCorner1_X_Hex(GetInternalID(), value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner1_Z_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(27, CategoryID_TriggerZone)]
        public uint TriggerZoneCorner1_Z_Hex
        {
            get => GetTriggerZoneMethods().ReturnTriggerZoneCorner1_Z_Hex(GetInternalID());
            set
            {
                GetTriggerZoneMethods().SetTriggerZoneCorner1_Z_Hex(GetInternalID(), value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner2_X_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(28, CategoryID_TriggerZone)]
        public uint TriggerZoneCorner2_X_Hex
        {
            get => GetTriggerZoneMethods().ReturnTriggerZoneCorner2_X_Hex(GetInternalID());
            set
            {
                GetTriggerZoneMethods().SetTriggerZoneCorner2_X_Hex(GetInternalID(), value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner2_Z_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(29, CategoryID_TriggerZone)]
        public uint TriggerZoneCorner2_Z_Hex
        {
            get => GetTriggerZoneMethods().ReturnTriggerZoneCorner2_Z_Hex(GetInternalID());
            set
            {
                GetTriggerZoneMethods().SetTriggerZoneCorner2_Z_Hex(GetInternalID(), value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner3_X_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(30, CategoryID_TriggerZone)]
        public uint TriggerZoneCorner3_X_Hex
        {
            get => GetTriggerZoneMethods().ReturnTriggerZoneCorner3_X_Hex(GetInternalID());
            set
            {
                GetTriggerZoneMethods().SetTriggerZoneCorner3_X_Hex(GetInternalID(), value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }


        [CustomCategory(aLang.SpecialTriggerZoneCategory)]
        [CustomDisplayName(aLang.TriggerZoneCorner3_Z_Hex_DisplayName)]
        [TypeConverter(typeof(HexNumberTypeConverter))]
        [HexNumber()]
        [DefaultValue(null)]
        [ReadOnly(false)]
        [Browsable(false)]
        [DynamicTypeDescriptor.Id(31, CategoryID_TriggerZone)]
        public uint TriggerZoneCorner3_Z_Hex
        {
            get => GetTriggerZoneMethods().ReturnTriggerZoneCorner3_Z_Hex(GetInternalID());
            set
            {
                GetTriggerZoneMethods().SetTriggerZoneCorner3_Z_Hex(GetInternalID(), value);
                updateMethods.UpdateOrbitCamera();
                updateMethods.UpdateGL();
            }
        }

        #endregion

    }
}
