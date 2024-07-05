using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Re4QuadExtremeEditor.src.Class.Enums;

namespace Re4QuadExtremeEditor.src
{
   public static class Lang
    {
        /// <summary>
        /// indica se uma tradução foi carregado, se as janelas devem trocar de texto
        /// </summary>
        public static bool LoadedTranslation = false;

        /// <summary>
        /// textos do programa e da tradução carragada, parte dos atributos das Propriedades
        /// </summary>
        public static Dictionary<aLang, string> AttributeText = new Dictionary<aLang, string>();
        /// <summary>
        /// textos do programa e da tradução carragada
        /// </summary>
        public static Dictionary<eLang, string> Text = new Dictionary<eLang, string>();


        public static string GetAttributeText(aLang Id) 
        {
            if (AttributeText.ContainsKey(Id))
            {
                return AttributeText[Id];
            }
            return "";
        }
        public static void GetAttributeText(aLang Id, ref string text)
        {
            if (AttributeText.ContainsKey(Id))
            {
                text = AttributeText[Id];
            }
        }
        public static string GetText(eLang Id)
        {
            if (Text.ContainsKey(Id))
            {
                return Text[Id];
            }
            return "";
        }
        public static void GetText(eLang Id, ref string text)
        {
            if (Text.ContainsKey(Id))
            {
                text = Text[Id];
            }
        }
        public static void SetAttributeText(aLang Id, string text) 
        {
            if (AttributeText.ContainsKey(Id))
            {
                AttributeText[Id] = text;
            }
            else 
            {
                AttributeText.Add(Id, text);
            }
        }
        public static void SetText(eLang Id, string text)
        {
            if (Text.ContainsKey(Id))
            {
                Text[Id] = text;
            }
            else
            {
                Text.Add(Id, text);
            }
        }



        /// <summary>
        /// adicionas os textos dos atributos, das propriedades
        /// </summary>
        public static void StartAttributeTexts()
        {
            // float number
            AttributeText.Add(aLang.ListBoxFloatTypeEnable, "Use Hex Float");
            AttributeText.Add(aLang.ListBoxFloatTypeDisable, "Use Dec Float");

            AttributeText.Add(aLang.FloatTypeCategory, "Float Type");
            AttributeText.Add(aLang.FloatType_DisplayName, "Float Type");
            AttributeText.Add(aLang.FloatType_Description, "Changes the display mode of float type fields");

            // special types
            AttributeText.Add(aLang.SpecialType00_GeneralPurpose, "00: General Purpose");
            AttributeText.Add(aLang.SpecialType01_WarpDoor, "01: Warp Door");
            AttributeText.Add(aLang.SpecialType02_CutSceneEvents, "02: CutScene Or Events");
            AttributeText.Add(aLang.SpecialType03_Items, "03: Item");
            AttributeText.Add(aLang.SpecialType04_GroupedEnemyTrigger, "04: Grouped Enemy Trigger");
            AttributeText.Add(aLang.SpecialType05_Message, "05: Message");
            //AttributeText.Add(aLang.SpecialType06_Unused, "06: Unused");
            //AttributeText.Add(aLang.SpecialType07_Unused, "07: Unused");
            AttributeText.Add(aLang.SpecialType08_TypeWriter, "08: TypeWriter");
            //AttributeText.Add(aLang.SpecialType09_Unused, "09: Unused");
            AttributeText.Add(aLang.SpecialType0A_DamagesThePlayer, "0A: Damages The Player");
            AttributeText.Add(aLang.SpecialType0B_FalseCollision, "0B: False Collision");
            //AttributeText.Add(aLang.SpecialType0C_Unused, "0C: Unused");
            AttributeText.Add(aLang.SpecialType0D_Unknown, "0D: Unknown");
            AttributeText.Add(aLang.SpecialType0E_Crouch, "0E: Crouch");
            //AttributeText.Add(aLang.SpecialType0F_Unused, "0F: Unused");
            AttributeText.Add(aLang.SpecialType10_FixedLadderClimbUp, "10: Fixed Ladder Climb Up");
            AttributeText.Add(aLang.SpecialType11_ItemDependentEvents, "11: Item Dependent Events");
            AttributeText.Add(aLang.SpecialType12_AshleyHideCommand, "12: Ashley Hide Command");
            AttributeText.Add(aLang.SpecialType13_LocalTeleportation, "13: Local Teleportation");
            AttributeText.Add(aLang.SpecialType14_UsedForElevators, "14: Used For Elevators");
            AttributeText.Add(aLang.SpecialType15_AdaGrappleGun, "15: Ada Grapple Gun");
            AttributeText.Add(aLang.SpecialTypeUnspecifiedType, "Unspecified Type");


            // ListBox values
            AttributeText.Add(aLang.ListBoxEnable, "Enable");
            AttributeText.Add(aLang.ListBoxDisable, "Disable");
            AttributeText.Add(aLang.ListBoxAnotherValue, "Another Value");
            AttributeText.Add(aLang.ListBoxUnknownEnemy, "Unknown Enemy");
            AttributeText.Add(aLang.ListBoxUnknownItem, "Unknown Item");
            AttributeText.Add(aLang.ListBoxUnknownEtcModel, "Unknown EtcModel");

            AttributeText.Add(aLang.ListBoxSpecialZoneCategory00, "Disable TriggerZone");
            AttributeText.Add(aLang.ListBoxSpecialZoneCategory01, "TriggerZone Use 4 Points");
            AttributeText.Add(aLang.ListBoxSpecialZoneCategory02, "TriggerZone Use 1 Point And Circle Radius");
            AttributeText.Add(aLang.ListBoxSpecialZoneCategoryAnotherValue, "Another Value: Disable TriggerZone");


            AttributeText.Add(aLang.ListBoxItemAuraType00, "No sparkle and no light column (Default)");
            AttributeText.Add(aLang.ListBoxItemAuraType01, "Small glint (with sparkle)");
            AttributeText.Add(aLang.ListBoxItemAuraType02, "Small white/gold light column (with sparkle)");
            AttributeText.Add(aLang.ListBoxItemAuraType03, "Small blue/white light column (no sparkle)");
            AttributeText.Add(aLang.ListBoxItemAuraType04, "Small Green light column (herbs)");
            AttributeText.Add(aLang.ListBoxItemAuraType05, "Small Red light column (ammo)");
            AttributeText.Add(aLang.ListBoxItemAuraType06, "Small Red light column (ammo)");
            AttributeText.Add(aLang.ListBoxItemAuraType07, "BIG blue/white light column");
            AttributeText.Add(aLang.ListBoxItemAuraType08, "Small white light column (with sparkle)");
            AttributeText.Add(aLang.ListBoxItemAuraType09, "BIG yellow light column (bonus time)");
            AttributeText.Add(aLang.ListBoxItemAuraTypeAnotherValue, "Another Value: Aka Default");


            AttributeText.Add(aLang.ListBoxRefInteractionType00, "Unassociated Object");
            AttributeText.Add(aLang.ListBoxRefInteractionType01Enemy, "Object associated to Enemy");
            AttributeText.Add(aLang.ListBoxRefInteractionType02EtcModel, "Object associated to EtcModel");
            AttributeText.Add(aLang.ListBoxRefInteractionTypeAnotherValue, "Another Value: Unknown");

            AttributeText.Add(aLang.ListBoxPromptMessageTypeAnotherValue, "\"aaa\"");



            // EnemyProperty
            AttributeText.Add(aLang.Enemy_OrderCategory, "Order");
            AttributeText.Add(aLang.Enemy_OrderDisplayName, "Order ID");
            AttributeText.Add(aLang.Enemy_OrderDescription, "Unique identifier (index) for enemy, stores line position within the file.");
            AttributeText.Add(aLang.Enemy_AssociatedSpecialEventCategory, "Associated Special Event");
            AttributeText.Add(aLang.Enemy_AssociatedSpecialEventObjNameDisplayName, "Obj Name");
            AttributeText.Add(aLang.Enemy_AssociatedSpecialEventObjNameDescription, "Associated object index, if it's item type, display the name.");
            AttributeText.Add(aLang.Enemy_AssociatedSpecialEventTypeDisplayName, "Special Type");
            AttributeText.Add(aLang.Enemy_AssociatedSpecialEventTypeDescription, "Special Event type description.");
            AttributeText.Add(aLang.Enemy_AssociatedSpecialEventFromSpecialIndexDisplayName, "Special Index");
            AttributeText.Add(aLang.Enemy_AssociatedSpecialEventFromSpecialIndexFromDescription, "Special Index description");
            AttributeText.Add(aLang.Enemy_AssociatedSpecialEventFromFileDisplayName, "From File");
            AttributeText.Add(aLang.Enemy_AssociatedSpecialEventFromFileFromDescription, "Source file description (Informs which file the information is coming from).");
            AttributeText.Add(aLang.Enemy_LineArrayCategory, "All bytes of the line");
            AttributeText.Add(aLang.Enemy_LineArrayDisplayName, "Line Array [Hex] (Byte[32])");
            AttributeText.Add(aLang.Enemy_LineArrayDescription, "Contains all line bytes. \r\nWarning: Be careful when editing, as it may shift the byte positions.");
            AttributeText.Add(aLang.EnemyCategory, "Enemy");
            AttributeText.Add(aLang.ESL_ENABLE_List_Name, "Is Enable [List]");
            AttributeText.Add(aLang.ESL_ENABLE_Byte_Name, "Is Enable [Hex] (Byte)");
            AttributeText.Add(aLang.ESL_ENABLE_Byte_Description, "Offset[0x00]  \r\nTAG: ESL_ENABLE  \r\n\r\nEnemy activity status when entering room, \r\n0x00: Deactivated, \r\n0x01: Activated. \r\nNo other values are valid.");
            AttributeText.Add(aLang.ESL_ENEMY_ID_List_Name, "Enemy ID [List]");
            AttributeText.Add(aLang.ESL_ENEMY_ID_UshotUnflip_Name, "Enemy ID [Hex] (Ushort Unflip)");
            AttributeText.Add(aLang.ESL_ENEMY_ID_UshotUnflip_Description, "Offset[0x01][0x02]  \r\nTAG: ESL_ENEMY_ID  \r\n\r\nThis is the ID of the enemy placed in the Room."); //
            AttributeText.Add(aLang.ESL_HX03_Byte_Name, "Unknown 0x03 [Hex] (Byte)");
            AttributeText.Add(aLang.ESL_HX03_Byte_Description, "Offset[0x03]  \r\nTAG: ESL_HX03  \r\n\r\n###############"); //
            AttributeText.Add(aLang.ESL_HX04_Byte_Name, "Unknown 0x04 [Hex] (Byte)");
            AttributeText.Add(aLang.ESL_HX04_Byte_Description, "Offset[0x04]  \r\nTAG: ESL_HX04  \r\n\r\n###############"); //
            AttributeText.Add(aLang.ESL_HX05_Byte_Name, "Unknown 0x05 [Hex] (Byte)");
            AttributeText.Add(aLang.ESL_HX05_Byte_Description, "Offset[0x05]  \r\nTAG: ESL_HX05  \r\n\r\n###############"); //
            AttributeText.Add(aLang.ESL_HX06_Byte_Name, "Unknown 0x06 [Hex] (Byte)");
            AttributeText.Add(aLang.ESL_HX06_Byte_Description, "Offset[0x06]  \r\nTAG: ESL_HX06  \r\n\r\n###############"); //
            AttributeText.Add(aLang.ESL_HX07_Byte_Name, "Unknown 0x07 [Hex] (Byte)");
            AttributeText.Add(aLang.ESL_HX07_Byte_Description, "Offset[0x07]  \r\nTAG: ESL_HX07  \r\n\r\n###############"); //
            AttributeText.Add(aLang.ESL_EnemyLifeAmount_Short_Name, "Life Amount [Dec] (Short)");
            AttributeText.Add(aLang.ESL_EnemyLifeAmount_Short_Description, "Offset[0x09][0x08]  \r\nTAG: ESL_LIFE  \r\n\r\nEnemy life value, \r\nMax value is set to 32767, \r\ne Negative numbers will cause the enemy to be immortal."); //
            AttributeText.Add(aLang.ESL_HX0A_Byte_Name, "Unknown 0x0A [Hex] (Byte)");
            AttributeText.Add(aLang.ESL_HX0A_Byte_Description, "Offset[0x0A]  \r\nTAG: ESL_HX0A  \r\n\r\n###############"); //
            AttributeText.Add(aLang.ESL_HX0B_Byte_Name, "Unknown 0x0B [Hex] (Byte)");
            AttributeText.Add(aLang.ESL_HX0B_Byte_Description, "Offset[0x0B]  \r\nTAG: ESL_HX0B  \r\n\r\n###############"); //
            AttributeText.Add(aLang.ESL_PositionX_Short_Name, "Position.X [Dec] (Short)");
            AttributeText.Add(aLang.ESL_PositionX_Short_Description, "Offset[0x0D][0x0C]  \r\nTAG: ESL_POSITION.X  \r\n\r\n###############"); //
            AttributeText.Add(aLang.ESL_PositionY_Short_Name, "Position.Y [Dec] (Short)");
            AttributeText.Add(aLang.ESL_PositionY_Short_Description, "Offset[0x0F][0x0E]  \r\nTAG: ESL_POSITION.Y  \r\n\r\n###############"); //
            AttributeText.Add(aLang.ESL_PositionZ_Short_Name, "Position.Z [Dec] (Short)");
            AttributeText.Add(aLang.ESL_PositionZ_Short_Description, "Offset[0x11][0x10]  \r\nTAG: ESL_POSITION.Z  \r\n\r\n###############"); //
            AttributeText.Add(aLang.ESL_AngleX_Short_Name, "Rotation.X [Dec] (Short)");
            AttributeText.Add(aLang.ESL_AngleX_Short_Description, "Offset[0x13][0x12]  \r\nTAG: ESL_ALGLE.X  \r\n\r\n###############"); //
            AttributeText.Add(aLang.ESL_AngleY_Short_Name, "Rotation.Y [Dec] (Short)");
            AttributeText.Add(aLang.ESL_AngleY_Short_Description, "Offset[0x15][0x14]  \r\nTAG: ESL_ALGLE.Y  \r\n\r\n###############"); //
            AttributeText.Add(aLang.ESL_AngleZ_Short_Name, "Rotation.Z [Dec] (Short)");
            AttributeText.Add(aLang.ESL_AngleZ_Short_Description, "Offset[0x17][0x16]  \r\nTAG: ESL_ALGLE.Z  \r\n\r\n###############"); //
            AttributeText.Add(aLang.ESL_ROOM_ID_Ushort_Name, "Room ID [Hex] (Ushot)");
            AttributeText.Add(aLang.ESL_ROOM_ID_Ushort_Description, "Offset[0x19][0x18]  \r\nTAG: ESL_ROOM_ID  \r\n\r\nEnemy's Room ID | represents the ID of the room where the enemy is being added."); //
            AttributeText.Add(aLang.ESL_HX1A_Byte_Name, "Unknown 0x1A [Hex] (Byte)");
            AttributeText.Add(aLang.ESL_HX1A_Byte_Description, "Offset[0x1A]  \r\nTAG: ESL_HX1A  \r\n\r\n###############"); //
            AttributeText.Add(aLang.ESL_HX1B_Byte_Name, "Unknown 0x1B [Hex] (Byte)");
            AttributeText.Add(aLang.ESL_HX1B_Byte_Description, "Offset[0x1B]  \r\nTAG: ESL_HX1B  \r\n\r\n###############"); //
            AttributeText.Add(aLang.ESL_HX1C_Byte_Name, "Unknown 0x1C [Hex] (Byte)");
            AttributeText.Add(aLang.ESL_HX1C_Byte_Description, "Offset[0x1C]  \r\nTAG: ESL_HX1C  \r\n\r\n###############"); //
            AttributeText.Add(aLang.ESL_HX1D_Byte_Name, "Unknown 0x1D [Hex] (Byte)");
            AttributeText.Add(aLang.ESL_HX1D_Byte_Description, "Offset[0x1D]  \r\nTAG: ESL_HX1D  \r\n\r\n###############"); //
            AttributeText.Add(aLang.ESL_HX1E_Byte_Name, "Unknown 0x1E [Hex] (Byte)");
            AttributeText.Add(aLang.ESL_HX1E_Byte_Description, "Offset[0x1E]  \r\nTAG: ESL_HX1E  \r\n\r\n###############"); //
            AttributeText.Add(aLang.ESL_HX1F_Byte_Name, "Unknown 0x1F [Hex] (Byte)");
            AttributeText.Add(aLang.ESL_HX1F_Byte_Description, "Offset[0x1F]  \r\nTAG: ESL_HX1F  \r\n\r\n###############"); //



            // EtcModelProperty
            AttributeText.Add(aLang.EtcModel_InternalLineIDCategory, "Internal Line ID");
            AttributeText.Add(aLang.EtcModel_InternalLineIDDisplayName, "Internal Line ID");
            AttributeText.Add(aLang.EtcModel_InternalLineIDDescription, "It is the internal ID of the selected line (Does not correspond to the file save order).");
            AttributeText.Add(aLang.EtcModel_AssociatedSpecialEventCategory, "Associated Special Event");
            AttributeText.Add(aLang.EtcModel_AssociatedSpecialEventObjNameDisplayName, "Obj Name");
            AttributeText.Add(aLang.EtcModel_AssociatedSpecialEventObjNameDescription, "Associated object index, if it's item type, display the name.");
            AttributeText.Add(aLang.EtcModel_AssociatedSpecialEventTypeDisplayName, "Special Type");
            AttributeText.Add(aLang.EtcModel_AssociatedSpecialEventTypeDescription, "Special Event type description.");
            AttributeText.Add(aLang.EtcModel_AssociatedSpecialEventFromSpecialIndexDisplayName, "Special Index");
            AttributeText.Add(aLang.EtcModel_AssociatedSpecialEventFromSpecialIndexFromDescription, "Special Index description");
            AttributeText.Add(aLang.EtcModel_AssociatedSpecialEventFromFileDisplayName, "From File");
            AttributeText.Add(aLang.EtcModel_AssociatedSpecialEventFromFileDescription, "Source file description (Informs which file the information is coming from).");
            AttributeText.Add(aLang.EtcModel_LineArrayCategory, "All bytes of the line");
            AttributeText.Add(aLang.EtcModel_LineArrayDisplayName, "Line Array [Hex] (Byte[<<Lenght>>])");  //classic 64, UHD 40
            AttributeText.Add(aLang.EtcModel_LineArrayDescription, "Contains all line bytes. \r\nWarning: Be careful when editing, as it may shift the byte positions.");
            AttributeText.Add(aLang.EtcModelCategory, "EtcModel");


            AttributeText.Add(aLang.EtcModelID_Ushort_DisplayName, "EtcModel ID [Hex] (Ushort)");
            AttributeText.Add(aLang.EtcModelID_List_DisplayName, "EtcModel ID (List)");
            AttributeText.Add(aLang.EtcModelID_Ushort_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: ETCMODEL_ID  \r\n\r\nETCMODEL model ID, Warning: the ID must exist in .ETM file, otherwise, the object will not appear in game."); //
            
            AttributeText.Add(aLang.ETS_ID_Ushort_DisplayName, "ETS ID [Hex] (Ushort)");
            AttributeText.Add(aLang.ETS_ID_Ushort_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: ETS_ID  \r\n\r\nObject unique identifier (index). \r\nThe valid range for the index lay betwen 0x00 and 0x3F, values above are not loaded, \r\nindex values can be repeated any amount of times, \r\nbut, only 99 valid objects can be loaded to the scenario."); //
            
            //Scale
            AttributeText.Add(aLang.ETCM_Unknown_TTS_X_Float_DisplayName, "Unknown TTS.X [Dec] (Float) {It's not scale}");
            AttributeText.Add(aLang.ETCM_Unknown_TTS_X_Hex_DisplayName, "Unknown TTS.X [Hex] (Float) {It's not scale}");
            AttributeText.Add(aLang.ETCM_Unknown_TTS_X_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: UNKNOWN_TTS.X  \r\n\r\n###############"); //
            
            AttributeText.Add(aLang.ETCM_Unknown_TTS_Y_Float_DisplayName, "Unknown TTS.Y [Dec] (Float) {It's not scale}");
            AttributeText.Add(aLang.ETCM_Unknown_TTS_Y_Hex_DisplayName, "Unknown TTS.Y [Hex] (Float)  {It's not scale}");
            AttributeText.Add(aLang.ETCM_Unknown_TTS_Y_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: UNKNOWN_TTS.Y  \r\n\r\n###############"); //
            
            AttributeText.Add(aLang.ETCM_Unknown_TTS_Z_Float_DisplayName, "Unknown TTS.Z [Dec] (Float) {It's not scale}");
            AttributeText.Add(aLang.ETCM_Unknown_TTS_Z_Hex_DisplayName, "Unknown TTS.Z [Hex] (Float) {It's not scale}");
            AttributeText.Add(aLang.ETCM_Unknown_TTS_Z_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: UNKNOWN_TTS.Z  \r\n\r\n###############"); //

            //Angle
            AttributeText.Add(aLang.ETCM_AngleX_Float_DisplayName, "Angle.X [Dec] (Float)");
            AttributeText.Add(aLang.ETCM_AngleX_Hex_DisplayName, "Angle.X [Hex] (Float)");
            AttributeText.Add(aLang.ETCM_AngleX_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: ETCM_ANGLE.X  \r\n\r\n###############"); //

            AttributeText.Add(aLang.ETCM_AngleY_Float_DisplayName, "Angle.Y [Dec] (Float)");
            AttributeText.Add(aLang.ETCM_AngleY_Hex_DisplayName, "Angle.Y [Hex] (Float)");
            AttributeText.Add(aLang.ETCM_AngleY_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: ETCM_ANGLE.Y  \r\n\r\n###############"); //

            AttributeText.Add(aLang.ETCM_AngleZ_Float_DisplayName, "Angle.Z [Dec] (Float)");
            AttributeText.Add(aLang.ETCM_AngleZ_Hex_DisplayName, "Angle.Z [Hex] (Float)");
            AttributeText.Add(aLang.ETCM_AngleZ_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: ETCM_ANGLE.Z  \r\n\r\n###############"); //

            //Position
            AttributeText.Add(aLang.ETCM_PositionX_Float_DisplayName, "Position.X [Dec] (Float)");
            AttributeText.Add(aLang.ETCM_PositionX_Hex_DisplayName, "Position.X [Hex] (Float)");
            AttributeText.Add(aLang.ETCM_PositionX_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: ETCM_POSITION.X  \r\n\r\n###############"); //

            AttributeText.Add(aLang.ETCM_PositionY_Float_DisplayName, "Position.Y [Dec] (Float)");
            AttributeText.Add(aLang.ETCM_PositionY_Hex_DisplayName, "Position.Y [Hex] (Float)");
            AttributeText.Add(aLang.ETCM_PositionY_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: ETCM_POSITION.Y  \r\n\r\n###############"); //

            AttributeText.Add(aLang.ETCM_PositionZ_Float_DisplayName, "Position.Z [Dec] (Float)");
            AttributeText.Add(aLang.ETCM_PositionZ_Hex_DisplayName, "Position.Z [Hex] (Float)");
            AttributeText.Add(aLang.ETCM_PositionZ_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: ETCM_POSITION.Z  \r\n\r\n###############"); //

            //unused values only classic

            AttributeText.Add(aLang.ETCM_UnusedsInfo_Text, "Read-Me");
            AttributeText.Add(aLang.ETCM_UnusedsInfo_DisplayName, "Unused Info");
            AttributeText.Add(aLang.ETCM_UnusedsInfo_Description, "The fields below are only present in the \"classic\" version of the game, as functionalities are unknown, leave always at 0."); //

            AttributeText.Add(aLang.ETCM_Unknown_TTS_W_Float_DisplayName, "Unknown TTS.W [Dec] (Float) {It's not scale}");
            AttributeText.Add(aLang.ETCM_Unknown_TTS_W_Hex_DisplayName, "Unknown TTS.W [Hex] (Float) {It's not scale}");
            AttributeText.Add(aLang.ETCM_Unknown_TTS_W_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: UNKNOWN_TTS.W  \r\n\r\n###############"); //

            AttributeText.Add(aLang.ETCM_AngleW_Float_DisplayName, "Angle.W [Dec] (Float)");
            AttributeText.Add(aLang.ETCM_AngleW_Hex_DisplayName, "Angle.W [Hex] (Float)");
            AttributeText.Add(aLang.ETCM_AngleW_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: ETCM_ANGLE.W  \r\n\r\n###############"); //

            AttributeText.Add(aLang.ETCM_PositionW_Float_DisplayName, "Position.W [Dec] (Float)");
             AttributeText.Add(aLang.ETCM_PositionW_Hex_DisplayName, "Position.W [Hex] (Float)");
            AttributeText.Add(aLang.ETCM_PositionW_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: ETCM_POSITION.W  \r\n\r\n###############"); //

            AttributeText.Add(aLang.ETCM_Unknown_TTJ_ByteArray4_DisplayName, "Unknown TTJ [Hex] (Byte[4])");
            AttributeText.Add(aLang.ETCM_Unknown_TTJ_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_TTJ, UNKNOWN_TTJ  \r\n\r\n###############"); //


            AttributeText.Add(aLang.ETCM_Unknown_TTH_ByteArray4_DisplayName, "Unknown TTH [Hex] (Byte[4])");
            AttributeText.Add(aLang.ETCM_Unknown_TTH_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_TTH, UNKNOWN_TTH  \r\n\r\n###############"); //


            AttributeText.Add(aLang.ETCM_Unknown_TTG_ByteArray4_DisplayName, "Unknown TTG [Hex] (Byte[4])");
            AttributeText.Add(aLang.ETCM_Unknown_TTG_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_TTG, UNKNOWN_TTG  \r\n\r\n###############"); //


          


            // Spacial Property
            AttributeText.Add(aLang.Special_InternalLineIDCategory, "Internal Line ID");
            AttributeText.Add(aLang.Special_InternalLineIDDisplayName, "Internal Line ID");
            AttributeText.Add(aLang.Special_InternalLineIDDescription, "It is the internal ID of the selected line (Does not correspond to the file save order).");
            AttributeText.Add(aLang.Special_LineArrayCategory, "All bytes of the line");
            // classic AEV: 160
            // Classic ITA: 176
            // UHD: 156
            AttributeText.Add(aLang.Special_LineArrayDisplayName, "Line Array [Hex] (Byte[<<Lenght>>])");
            AttributeText.Add(aLang.Special_LineArrayDescription, "Contains all line bytes. \r\nWarning: Be careful when editing, as it may shift the byte positions.");


            AttributeText.Add(aLang.SpecialCategory, "Special");
            AttributeText.Add(aLang.SpecialTypeCategory, "Special Type");
            AttributeText.Add(aLang.SpecialTriggerZoneCategory, "Trigger Zone");
            AttributeText.Add(aLang.SpecialGeneralCategory, "Special General");


            AttributeText.Add(aLang.SpecialTypeID_Byte_DisplayName, "Special Type ID [Hex] (Byte)");
            AttributeText.Add(aLang.SpecialTypeID_List_DisplayName, "Special Type ID (List)");
            AttributeText.Add(aLang.SpecialTypeID_Byte_Description, "Offset[0x35]  \r\nTAG: SPECIAL_TYPE  \r\n\r\n###############");

            AttributeText.Add(aLang.SpecialIndex_Byte_DisplayName, "Special Index [Hex] (Byte)");
            AttributeText.Add(aLang.SpecialIndex_Byte_Description, "Offset[0x36]  \r\nTAG: SPECIAL_INDEX  \r\n\r\n###############");

            AttributeText.Add(aLang.Special_Category_Byte_DisplayName, "Category [Hex] (Byte)");
            AttributeText.Add(aLang.Special_Category_List_DisplayName, "Category (List)");
            AttributeText.Add(aLang.Special_Category_Byte_Description, "Offset[0x05]  \r\nTAG: Category  \r\n\r\n###############");


            //TriggerZone
            AttributeText.Add(aLang.TriggerZoneTrueY_Float_DisplayName, "TriggerZone True Y [Dec] (Float)");
            AttributeText.Add(aLang.TriggerZoneTrueY_Hex_DisplayName, "TriggerZone True Y [Hex] (Float)");
            AttributeText.Add(aLang.TriggerZoneTrueY_Description, "Offset[0x0B][0x0A][0x09][0x08]  \r\nTAG: TriggerZoneTrueY  \r\n\r\n###############");

            AttributeText.Add(aLang.TriggerZoneMoreHeight_Float_DisplayName, "TriggerZone More Height [Dec] (Float)");
            AttributeText.Add(aLang.TriggerZoneMoreHeight_Hex_DisplayName, "TriggerZone More Height [Hex] (Float)");
            AttributeText.Add(aLang.TriggerZoneMoreHeight_Description, "Offset[0x0F][0x0E][0x0D][0x0C]  \r\nTAG: TriggerZoneMoreHeight  \r\n\r\n###############");

            AttributeText.Add(aLang.TriggerZoneCircleRadius_Float_DisplayName, "TriggerZone Circle Radius [Dec] (Float)");
            AttributeText.Add(aLang.TriggerZoneCircleRadius_Hex_DisplayName, "TriggerZone Circle Radius [Hex] (Float)");
            AttributeText.Add(aLang.TriggerZoneCircleRadius_Description, "Offset[0x13][0x12][0x11][0x10]  \r\nTAG: TriggerZoneCircleRadius  \r\n\r\n###############");

            AttributeText.Add(aLang.TriggerZoneCorner0_X_Float_DisplayName, "TriggerZone Corner0.X [Dec] (Float)");
            AttributeText.Add(aLang.TriggerZoneCorner0_X_Hex_DisplayName, "TriggerZone Corner0.X [Hex] (Float)");
            AttributeText.Add(aLang.TriggerZoneCorner0_X_Description, "Offset[0x17][0x16][0x15][0x14]  \r\nTAG: TZC_0.X  \r\n\r\n###############");

            AttributeText.Add(aLang.TriggerZoneCorner0_Z_Float_DisplayName, "TriggerZone Corner0.Z [Dec] (Float)");
            AttributeText.Add(aLang.TriggerZoneCorner0_Z_Hex_DisplayName, "TriggerZone Corner0.Z [Hex] (Float)");
            AttributeText.Add(aLang.TriggerZoneCorner0_Z_Description, "Offset[0x1B][0x1A][0x19][0x18]  \r\nTAG: TZC_0.Z  \r\n\r\n###############");

            AttributeText.Add(aLang.TriggerZoneCorner1_X_Float_DisplayName, "TriggerZone Corner1.X [Dec] (Float)");
            AttributeText.Add(aLang.TriggerZoneCorner1_X_Hex_DisplayName, "TriggerZone Corner1.X [Hex] (Float)");
            AttributeText.Add(aLang.TriggerZoneCorner1_X_Description, "Offset[0x1F][0x1E][0x1D][0x1C]  \r\nTAG: TZC_1.X  \r\n\r\n###############");

            AttributeText.Add(aLang.TriggerZoneCorner1_Z_Float_DisplayName, "TriggerZone Corner1.Z [Dec] (Float)");
            AttributeText.Add(aLang.TriggerZoneCorner1_Z_Hex_DisplayName, "TriggerZone Corner1.Z [Hex] (Float)");
            AttributeText.Add(aLang.TriggerZoneCorner1_Z_Description, "Offset[0x23][0x22][0x21][0x20]  \r\nTAG: TZC_1.Z  \r\n\r\n###############");

            AttributeText.Add(aLang.TriggerZoneCorner2_X_Float_DisplayName, "TriggerZone Corner2.X [Dec] (Float)");
            AttributeText.Add(aLang.TriggerZoneCorner2_X_Hex_DisplayName, "TriggerZone Corner2.X [Hex] (Float)");
            AttributeText.Add(aLang.TriggerZoneCorner2_X_Description, "Offset[0x27][0x26][0x25][0x24]  \r\nTAG: TZC_2.X  \r\n\r\n###############");

            AttributeText.Add(aLang.TriggerZoneCorner2_Z_Float_DisplayName, "TriggerZone Corner2.Z [Dec] (Float)");
            AttributeText.Add(aLang.TriggerZoneCorner2_Z_Hex_DisplayName, "TriggerZone Corner2.Z [Hex] (Float)");
            AttributeText.Add(aLang.TriggerZoneCorner2_Z_Description, "Offset[0x2B][0x2A][0x29][0x28]  \r\nTAG: TZC_2.Z  \r\n\r\n###############");

            AttributeText.Add(aLang.TriggerZoneCorner3_X_Float_DisplayName, "TriggerZone Corner3.X [Dec] (Float)");
            AttributeText.Add(aLang.TriggerZoneCorner3_X_Hex_DisplayName, "TriggerZone Corner3.X [Hex] (Float)");
            AttributeText.Add(aLang.TriggerZoneCorner3_X_Description, "Offset[0x2F][0x2E][0x2D][0x2C]  \r\nTAG: TZC_3.X  \r\n\r\n###############");

            AttributeText.Add(aLang.TriggerZoneCorner3_Z_Float_DisplayName, "TriggerZone Corner3.Z [Dec] (Float)");
            AttributeText.Add(aLang.TriggerZoneCorner3_Z_Hex_DisplayName, "TriggerZone Corner3.Z [Hex] (Float)");
            AttributeText.Add(aLang.TriggerZoneCorner3_Z_Description, "Offset[0x33][0x32][0x31][0x30]  \r\nTAG: TZC_3.Z  \r\n\r\n###############");

            // GENERAL
            AttributeText.Add(aLang.Unknown_GG_ByteArray4_DisplayName, "Unknown GG [Hex] (Byte[4])");
            AttributeText.Add(aLang.Unknown_GG_ByteArray4_Description, "Offset[0x00][0x01][0x02][0x03]  \r\nTAG: U_GG  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_GH_Byte_DisplayName, "Unknown GH [Hex] (Byte)");
            AttributeText.Add(aLang.Unknown_GH_Byte_Description, "Offset[0x04]  \r\nTAG: U_GH  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_GK_ByteArray2_DisplayName, "Unknown GK [Hex] (Byte[2])");
            AttributeText.Add(aLang.Unknown_GK_ByteArray2_Description, "Offset[0x06][0x07]  \r\nTAG: U_GK  \r\n\r\n###############");


            AttributeText.Add(aLang.Unknown_KG_Byte_DisplayName, "Unknown KG [Hex] (Byte)");
            AttributeText.Add(aLang.Unknown_KG_Byte_Description, "Offset[0x34]  \r\nTAG: U_KG  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_KJ_Byte_DisplayName, "Unknown KJ [Hex] (Byte)");
            AttributeText.Add(aLang.Unknown_KJ_Byte_Description, "Offset[0x37]  \r\nTAG: U_KJ  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_LI_Byte_DisplayName, "Unknown LI [Hex] (Byte)");
            AttributeText.Add(aLang.Unknown_LI_Byte_Description, "Offset[0x38]  \r\nTAG: U_LI  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_LO_Byte_DisplayName, "Unknown LO [Hex] (Byte)");
            AttributeText.Add(aLang.Unknown_LO_Byte_Description, "Offset[0x39]  \r\nTAG: U_LO  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_LU_Byte_DisplayName, "Unknown LU [Hex] (Byte)");
            AttributeText.Add(aLang.Unknown_LU_Byte_Description, "Offset[0x3A]  \r\nTAG: U_LU  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_LH_Byte_DisplayName, "Unknown LH [Hex] (Byte)");
            AttributeText.Add(aLang.Unknown_LH_Byte_Description, "Offset[0x3B]  \r\nTAG: U_LH  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_MI_ByteArray2_DisplayName, "Unknown MI [Hex] (Byte[2])");
            AttributeText.Add(aLang.Unknown_MI_ByteArray2_Description, "Offset[0x3C][0x3D]  \r\nTAG: U_MI  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_MO_ByteArray2_DisplayName, "Unknown MO [Hex] (Byte[2])");
            AttributeText.Add(aLang.Unknown_MO_ByteArray2_Description, "Offset[0x3E][0x3F]  \r\nTAG: U_MO  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_MU_ByteArray2_DisplayName, "Unknown MU [Hex] (Byte[2])");
            AttributeText.Add(aLang.Unknown_MU_ByteArray2_Description, "Offset[0x40][0x41]  \r\nTAG: U_MU  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_NI_ByteArray2_DisplayName, "Unknown NI [Hex] (Byte[2])");
            AttributeText.Add(aLang.Unknown_NI_ByteArray2_Description, "Offset[0x42][0x43]  \r\nTAG: U_NI  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_NO_Byte_DisplayName, "Unknown NO [Hex] (Byte)");
            AttributeText.Add(aLang.Unknown_NO_Byte_Description, "Offset[0x44]  \r\nTAG: U_NO  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_NS_Byte_DisplayName, "Unknown NS [Hex] (Byte)");
            AttributeText.Add(aLang.Unknown_NS_Byte_Description, "Offset[0x45]  \r\nTAG: U_NS  \r\n\r\n###############");

            AttributeText.Add(aLang.RefInteractionType_Byte_DisplayName, "Ref Interaction Type [Hex] (Byte)");
            AttributeText.Add(aLang.RefInteractionType_List_DisplayName, "Ref Interaction Type (List)");
            AttributeText.Add(aLang.RefInteractionType_Byte_Description, "Offset[0x46]  \r\nTAG: REF_INTERACTION_TYPE  \r\n\r\n###############");

            AttributeText.Add(aLang.RefInteractionIndex_Byte_DisplayName, "Ref Interaction Index [Hex] (Byte)");
            AttributeText.Add(aLang.RefInteractionIndex_Byte_Description, "Offset[0x47]  \r\nTAG: REF_INTERACTION_INDEX  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_NT_Byte_DisplayName, "Unknown NT [Hex] (Byte)");
            AttributeText.Add(aLang.Unknown_NT_Byte_Description, "Offset[0x48]  \r\nTAG: U_NT  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_NU_Byte_DisplayName, "Unknown NU [Hex] (Byte)");
            AttributeText.Add(aLang.Unknown_NU_Byte_Description, "Offset[0x49]  \r\nTAG: U_NU  \r\n\r\n###############");

            AttributeText.Add(aLang.PromptMessage_Byte_DisplayName, "Prompt Message [Hex] (Byte)");
            AttributeText.Add(aLang.PromptMessage_List_DisplayName, "Prompt Message (List)");
            AttributeText.Add(aLang.PromptMessage_Byte_Description, "Offset[0x4A]  \r\nTAG: PromptMessage  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_PI_Byte_DisplayName, "Unknown PI [Hex] (Byte)");
            AttributeText.Add(aLang.Unknown_PI_Byte_Description, "Offset[0x4B]  \r\nTAG: U_PI  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_PO_ByteArray4_DisplayName, "Unknown PO [Hex] (Byte[4])");
            AttributeText.Add(aLang.Unknown_PO_ByteArray4_Description, "Offset[0x4C][0x4D][0x4E][0x4F]  \r\nTAG: U_PO  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_PU_ByteArray2_DisplayName, "Unknown PU [Hex] (Byte[2])");
            AttributeText.Add(aLang.Unknown_PU_ByteArray2_Description, "Offset[0x50][0x51]  \r\nTAG: U_PU  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_PK_Byte_DisplayName, "Unknown PK [Hex] (Byte)");
            AttributeText.Add(aLang.Unknown_PK_Byte_Description, "Offset[0x52]  \r\nTAG: U_PK  \r\n\r\n###############");

            AttributeText.Add(aLang.MessageColor_Byte_DisplayName, "Message Color [Hex] (Byte)");
            AttributeText.Add(aLang.MessageColor_Byte_Description, "Offset[0x53]  \r\nTAG: MessageColor  \r\n\r\nColor of the displayed message, being: \r\n00: Default or White \r\n01: Green \r\n02: Red \r\n Another Value: Aka Default");

            AttributeText.Add(aLang.Unknown_QI_ByteArray4_DisplayName, "Unknown QI [Hex] (Byte[4])");
            AttributeText.Add(aLang.Unknown_QI_ByteArray4_Description, "Offset[0x54][0x55][0x56][0x57]  \r\nTAG: U_QI  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_QO_ByteArray4_DisplayName, "Unknown QO [Hex] (Byte[4])");
            AttributeText.Add(aLang.Unknown_QO_ByteArray4_Description, "Offset[0x58][0x59][0x5A][0x5B]  \r\nTAG: U_QO  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_QU_ByteArray4_DisplayName, "Unknown QU [Hex] (Byte[4]) {Only Classic}");
            AttributeText.Add(aLang.Unknown_QU_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_QU  \r\n\r\n###############");


            ////Unknown/geral types

            AttributeText.Add(aLang.Unknown_HH_ByteArray2_DisplayName, "Unknown HH [Hex] (Byte[2])");
            AttributeText.Add(aLang.Unknown_HH_ByteArray2_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: U_HH  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_HK_ByteArray2_DisplayName, "Unknown HK [Hex] (Byte[2])");
            AttributeText.Add(aLang.Unknown_HK_ByteArray2_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: U_HK  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_HL_ByteArray2_DisplayName, "Unknown HL [Hex] (Byte[2])");
            AttributeText.Add(aLang.Unknown_HL_ByteArray2_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: U_HL  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_HM_ByteArray2_DisplayName, "Unknown HM [Hex] (Byte[2])");
            AttributeText.Add(aLang.Unknown_HM_ByteArray2_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: U_HM  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_HN_ByteArray2_DisplayName, "Unknown HN [Hex] (Byte[2])");
            AttributeText.Add(aLang.Unknown_HN_ByteArray2_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: U_HN  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_HR_ByteArray2_DisplayName, "Unknown HR [Hex] (Byte[2])");
            AttributeText.Add(aLang.Unknown_HR_ByteArray2_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: U_HR  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_RH_ByteArray2_DisplayName, "Unknown RH [Hex] (Byte[2])");
            AttributeText.Add(aLang.Unknown_RH_ByteArray2_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: U_RH  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_RJ_ByteArray2_DisplayName, "Unknown RJ [Hex] (Byte[2])");
            AttributeText.Add(aLang.Unknown_RJ_ByteArray2_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: U_RJ  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_RK_ByteArray2_DisplayName, "Unknown RK [Hex] (Byte[2])");
            AttributeText.Add(aLang.Unknown_RK_ByteArray2_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: U_RK  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_RL_ByteArray2_DisplayName, "Unknown RL [Hex] (Byte[2])");
            AttributeText.Add(aLang.Unknown_RL_ByteArray2_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: U_RL  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_RM_ByteArray2_DisplayName, "Unknown RM [Hex] (Byte[2])");
            AttributeText.Add(aLang.Unknown_RM_ByteArray2_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: U_RM  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_RN_ByteArray2_DisplayName, "Unknown RN [Hex] (Byte[2])");
            AttributeText.Add(aLang.Unknown_RN_ByteArray2_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: U_RN  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_RP_ByteArray2_DisplayName, "Unknown RP [Hex] (Byte[2])");
            AttributeText.Add(aLang.Unknown_RP_ByteArray2_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: U_RP  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_RQ_ByteArray2_DisplayName, "Unknown RQ [Hex] (Byte[2])");
            AttributeText.Add(aLang.Unknown_RQ_ByteArray2_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: U_RQ  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_TG_ByteArray4_DisplayName, "Unknown TG [Hex] (Byte[4])");
            AttributeText.Add(aLang.Unknown_TG_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_TG  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_TH_ByteArray4_DisplayName, "Unknown TH [Hex] (Byte[4])");
            AttributeText.Add(aLang.Unknown_TH_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_TH  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_TJ_ByteArray4_DisplayName, "Unknown TJ [Hex] (Byte[4])");
            AttributeText.Add(aLang.Unknown_TJ_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_TJ  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_TK_ByteArray4_DisplayName, "Unknown TK [Hex] (Byte[4])");
            AttributeText.Add(aLang.Unknown_TK_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_TK  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_TL_ByteArray4_DisplayName, "Unknown TL [Hex] (Byte[4])");
            AttributeText.Add(aLang.Unknown_TL_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_TL  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_TM_ByteArray4_DisplayName, "Unknown TM [Hex] (Byte[4])");
            AttributeText.Add(aLang.Unknown_TM_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_TM  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_TN_ByteArray4_DisplayName, "Unknown TN [Hex] (Byte[4])");
            AttributeText.Add(aLang.Unknown_TN_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_TN  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_TP_ByteArray4_DisplayName, "Unknown TP [Hex] (Byte[4])");
            AttributeText.Add(aLang.Unknown_TP_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_TP  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_TQ_ByteArray4_DisplayName, "Unknown TQ [Hex] (Byte[4])");
            AttributeText.Add(aLang.Unknown_TQ_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_TQ  \r\n\r\n###############");


            ////end only ITA Classic

            AttributeText.Add(aLang.Unknown_VS_ByteArray4_DisplayName, "Unknown VS [Hex] (Byte[4]) {Only Classic}");
            AttributeText.Add(aLang.Unknown_VS_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_VS  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_VT_ByteArray4_DisplayName, "Unknown VT [Hex] (Byte[4]) {Only Classic}");
            AttributeText.Add(aLang.Unknown_VT_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_VT  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_VI_ByteArray4_DisplayName, "Unknown VI [Hex] (Byte[4]) {Only Classic}");
            AttributeText.Add(aLang.Unknown_VI_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_VI  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_VO_ByteArray4_DisplayName, "Unknown VO [Hex] (Byte[4]) {Only Classic}");
            AttributeText.Add(aLang.Unknown_VO_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_VO  \r\n\r\n###############");


            //// ObjPoint

            AttributeText.Add(aLang.ObjPointX_Float_DisplayName, "Object Point.X [Dec] (Float)");
            AttributeText.Add(aLang.ObjPointX_Hex_DisplayName, "Object Point.X [Hex] (Float)");
            AttributeText.Add(aLang.ObjPointX_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: ITEM_POSITION.X, DestinationPoint.X, LadderPoint.X, TeleportationPoint.X, StartPoint.X  \r\n\r\n###############");

            AttributeText.Add(aLang.ObjPointY_Float_DisplayName, "Object Point.Y [Dec] (Float)");
            AttributeText.Add(aLang.ObjPointY_Hex_DisplayName, "Object Point.Y [Hex] (Float)");
            AttributeText.Add(aLang.ObjPointY_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: ITEM_POSITION.Y, DestinationPoint.Y, LadderPoint.Y, TeleportationPoint.Y, StartPoint.Y  \r\n\r\n###############");

            AttributeText.Add(aLang.ObjPointZ_Float_DisplayName, "Object Point.Z [Dec] (Float)");
            AttributeText.Add(aLang.ObjPointZ_Hex_DisplayName, "Object Point.Z [Hex] (Float)");
            AttributeText.Add(aLang.ObjPointZ_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: ITEM_POSITION.Z, DestinationPoint.Z, LadderPoint.Z, TeleportationPoint.Z, StartPoint.Z  \r\n\r\n###############");

            AttributeText.Add(aLang.ObjPointW_ByteArray4_DisplayName, "Object Point.W [Hex] (Byte[4])");
            AttributeText.Add(aLang.ObjPointW_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: ITEM_POSITION.W  \r\n\r\n###############");

            AttributeText.Add(aLang.ObjPointW_onlyClassic_ByteArray4_DisplayName, "Object Point.W [Hex] (Byte[4]) {Only Classic}");
            AttributeText.Add(aLang.ObjPointW_onlyClassic_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: LadderPoint.W, TeleportationPoint.W, StartPoint.W, HidingPoint.W  \r\n\r\n###############");


            // outros types
            //TYPE 0x04, 0x05, 0x0A and 0x11

            AttributeText.Add(aLang.NeededItemNumber_Ushort_DisplayName, "Needed Item Number [Hex] (Ushort)");
            AttributeText.Add(aLang.NeededItemNumber_List_DisplayName, "Needed Item Number (List)");
            AttributeText.Add(aLang.NeededItemNumber_Ushort_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: NeededItemNumber  \r\n\r\n###############");


            AttributeText.Add(aLang.EnemyGroup_Ushort_DisplayName, "Enemy Group [Hex] (Ushort)");
            AttributeText.Add(aLang.EnemyGroup_Ushort_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: EnemyGroup  \r\n\r\n###############");


            AttributeText.Add(aLang.RoomMessage_Ushort_DisplayName, "Room Message [Hex] (Ushort)");
            AttributeText.Add(aLang.RoomMessage_Ushort_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: RoomMessage  \r\n\r\n###############");

            AttributeText.Add(aLang.MessageCutSceneID_Ushort_DisplayName, "Message CutScene ID [Hex] (Ushort)");
            AttributeText.Add(aLang.MessageCutSceneID_Ushort_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: MessageCutSceneID  \r\n\r\n###############");

            AttributeText.Add(aLang.MessageID_Ushort_DisplayName, "Message ID [Hex] (Ushort)");
            AttributeText.Add(aLang.MessageID_Ushort_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: MessageID  \r\n\r\n###############");


            AttributeText.Add(aLang.ActivationType_Byte_DisplayName, "Activation Type [Hex] (Byte)");
            AttributeText.Add(aLang.ActivationType_Byte_Description, "Offset[<<Offset1>>]  \r\nTAG: ActivationType  \r\n\r\n###############");

            AttributeText.Add(aLang.DamageType_Byte_DisplayName, "Damage Type [Hex] (Byte)");
            AttributeText.Add(aLang.DamageType_Byte_Description, "Offset[<<Offset1>>]  \r\nTAG: DamageType  \r\n\r\n###############");

            AttributeText.Add(aLang.BlockingType_Byte_DisplayName, "Blocking Type [Hex] (Byte)");
            AttributeText.Add(aLang.BlockingType_Byte_Description, "Offset[<<Offset1>>]  \r\nTAG: BlockingType  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_SJ_Byte_DisplayName, "Unknown SJ [Hex] (Byte)");
            AttributeText.Add(aLang.Unknown_SJ_Byte_Description, "Offset[<<Offset1>>]  \r\nTAG: U_SJ  \r\n\r\n###############");

            AttributeText.Add(aLang.DamageAmount_Ushort_DisplayName, "Damage Amount [Dec] (Ushort)");
            AttributeText.Add(aLang.DamageAmount_Ushort_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: DamageAmount  \r\n\r\n###############");


            // Type 0x01 warp door

            AttributeText.Add(aLang.DestinationFacingAngle_Float_DisplayName, "Destination Facing Angle [Dec] (Float)");
            AttributeText.Add(aLang.DestinationFacingAngle_Hex_DisplayName, "Destination Facing Angle [Hex] (Float)");
            AttributeText.Add(aLang.DestinationFacingAngle_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: DestinationFacingAngle  \r\n\r\n###############");

            AttributeText.Add(aLang.DestinationRoom_UshortUnflip_DisplayName, "Destination Room [Hex] (Ushort Unflip)");
            AttributeText.Add(aLang.DestinationRoom_UshortUnflip_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: DestinationRoom  \r\n\r\n###############");

            AttributeText.Add(aLang.LockedDoorType_Byte_DisplayName, "Locked Door Type [Hex] (Byte)");
            AttributeText.Add(aLang.LockedDoorType_Byte_Description, "Offset[<<Offset1>>]  \r\nTAG: LockedDoorType  \r\n\r\nDoor status. \r\n00: Opened, free access. \r\n01: Locked.\r\n02: Unlock door.\r\nOther values: unknown.");

            AttributeText.Add(aLang.LockedDoorIndex_Byte_DisplayName, "Locked Door Index [Hex] (Byte)");
            AttributeText.Add(aLang.LockedDoorIndex_Byte_Description, "Offset[<<Offset1>>]  \r\nTAG: LockedDoorIndex  \r\n\r\n###############");

            //Type 0x13 Teleportation

            AttributeText.Add(aLang.TeleportationFacingAngle_Float_DisplayName, "Teleportation Facing Angle [Dec] (Float)");
            AttributeText.Add(aLang.TeleportationFacingAngle_Hex_DisplayName, "Teleportation Facing Angle [Hex] (Float)");
            AttributeText.Add(aLang.TeleportationFacingAngle_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: TeleportationFacingAngle  \r\n\r\n###############");


            //Type 0x10 LadderClimbUp


            AttributeText.Add(aLang.LadderFacingAngle_Float_DisplayName, "Ladder Facing Angle [Dec] (Float)");
            AttributeText.Add(aLang.LadderFacingAngle_Hex_DisplayName, "Ladder Facing Angle [Hex] (Float)");
            AttributeText.Add(aLang.LadderFacingAngle_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: LadderFacingAngle  \r\n\r\n###############");

            AttributeText.Add(aLang.LadderStepCount_Sbyte_DisplayName, "Ladder Step Count [Dec] (Sbyte)");
            AttributeText.Add(aLang.LadderStepCount_Sbyte_Description, "Offset[<<Offset1>>]  \r\nTAG: LadderStepCount  \r\n\r\nLadder steps number, considering that: \r\nFor positive values the player will climb. \r\nFor negative values the player will go down the stairs. \r\nDo not use the values -1, 0 and +1, as the player will stay in the ladder animation infinitely.");

            AttributeText.Add(aLang.LadderParameter0_Byte_DisplayName, "Ladder Parameter 0 [Hex] (Byte)");
            AttributeText.Add(aLang.LadderParameter0_Byte_Description, "Offset[<<Offset1>>]  \r\nTAG: LadderParameter[0]  \r\n\r\n###############");

            AttributeText.Add(aLang.LadderParameter1_Byte_DisplayName, "Ladder Parameter 1 [Hex] (Byte)");
            AttributeText.Add(aLang.LadderParameter1_Byte_Description, "Offset[<<Offset1>>]  \r\nTAG: LadderParameter[1]  \r\n\r\n###############");

            AttributeText.Add(aLang.LadderParameter2_Byte_DisplayName, "Ladder Parameter 2 [Hex] (Byte)");
            AttributeText.Add(aLang.LadderParameter2_Byte_Description, "Offset[<<Offset1>>]  \r\nTAG: LadderParameter[2]  \r\n\r\n###############");

            AttributeText.Add(aLang.LadderParameter3_Byte_DisplayName, "Ladder Parameter 3 [Hex] (Byte)");
            AttributeText.Add(aLang.LadderParameter3_Byte_Description, "Offset[<<Offset1>>]  \r\nTAG: LadderParameter[3]  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_SG_Byte_DisplayName, "Unknown SG [Hex] (Byte)");
            AttributeText.Add(aLang.Unknown_SG_Byte_Description, "Offset[<<Offset1>>]  \r\nTAG: U_SG  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_SH_ByteArray2_DisplayName, "Unknown SH [Hex] (Byte[2])");
            AttributeText.Add(aLang.Unknown_SH_ByteArray2_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: U_SH  \r\n\r\n###############");

            //// type 0x03 itens

            AttributeText.Add(aLang.Unknown_RI_X_Float_DisplayName, "Unknown RI.X [Dec] (Float)");
            AttributeText.Add(aLang.Unknown_RI_X_Hex_DisplayName, "Unknown RI.X [Hex] (Float)");
            AttributeText.Add(aLang.Unknown_RI_X_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_RI.X  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_RI_Y_Float_DisplayName, "Unknown RI.Y [Dec] (Float)");
            AttributeText.Add(aLang.Unknown_RI_Y_Hex_DisplayName, "Unknown RI.Y [Hex] (Float)");
            AttributeText.Add(aLang.Unknown_RI_Y_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_RI.Y  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_RI_Z_Float_DisplayName, "Unknown RI.Z [Dec] (Float)");
            AttributeText.Add(aLang.Unknown_RI_Z_Hex_DisplayName, "Unknown RI.Z [Hex] (Float)");
            AttributeText.Add(aLang.Unknown_RI_Z_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_RI.Z  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_RI_W_ByteArray4_DisplayName, "Unknown RI.W [Hex] (Byte[4]) {Only Classic}");
            AttributeText.Add(aLang.Unknown_RI_W_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_RI.W  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_RO_ByteArray4_DisplayName, "Unknown RO [Hex] (Byte[4]) {Only Classic}");
            AttributeText.Add(aLang.Unknown_RO_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_RO  \r\n\r\n###############");

            AttributeText.Add(aLang.ItemNumber_Ushort_DisplayName, "Item Number [Hex] (Ushort)");
            AttributeText.Add(aLang.ItemNumber_List_DisplayName, "Item Number (List)");
            AttributeText.Add(aLang.ItemNumber_Ushort_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: ItemNumber  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_RU_ByteArray2_DisplayName, "Unknown RU [Hex] (Byte[2])");
            AttributeText.Add(aLang.Unknown_RU_ByteArray2_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: U_RU  \r\n\r\n###############");

            AttributeText.Add(aLang.ItemAmount_Ushort_DisplayName, "Item Amount [Dec] (Ushort)");
            AttributeText.Add(aLang.ItemAmount_Ushort_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: ItemAmount  \r\n\r\n###############");

            AttributeText.Add(aLang.SecundIndex_Ushort_DisplayName, "Secund Index [Hex] (Ushort)");
            AttributeText.Add(aLang.SecundIndex_Ushort_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: SecundIndex  \r\n\r\n###############");

            AttributeText.Add(aLang.ItemAuraType_Ushort_DisplayName, "Item Aura Type [Hex] (Ushort)");
            AttributeText.Add(aLang.ItemAuraType_List_DisplayName, "Item Aura Type (List)");
            AttributeText.Add(aLang.ItemAuraType_Ushort_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: ItemAuraType  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_QM_Byte_DisplayName, "Unknown QM [Hex] (Byte)");
            AttributeText.Add(aLang.Unknown_QM_Byte_Description, "Offset[<<Offset1>>]  \r\nTAG: U_QM  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_QL_Byte_DisplayName, "Unknown QL [Hex] (Byte)");
            AttributeText.Add(aLang.Unknown_QL_Byte_Description, "Offset[<<Offset1>>]  \r\nTAG: U_QL  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_QR_Byte_DisplayName, "Unknown QR [Hex] (Byte)");
            AttributeText.Add(aLang.Unknown_QR_Byte_Description, "Offset[<<Offset1>>]  \r\nTAG: U_QR  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_QH_Byte_DisplayName, "Unknown QH [Hex] (Byte)");
            AttributeText.Add(aLang.Unknown_QH_Byte_Description, "Offset[<<Offset1>>]  \r\nTAG: U_QH  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_QG_ByteArray2_DisplayName, "Unknown QG [Hex] (Byte[2])");
            AttributeText.Add(aLang.Unknown_QG_ByteArray2_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: U_QG  \r\n\r\n###############");

            AttributeText.Add(aLang.ItemTriggerRadius_Float_DisplayName, "Item Trigger Radius [Dec] (Float)");
            AttributeText.Add(aLang.ItemTriggerRadius_Hex_DisplayName, "Item Trigger Radius [Hex] (Float)");
            AttributeText.Add(aLang.ItemTriggerRadius_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: ItemTriggerRadius  \r\n\r\n###############");

            AttributeText.Add(aLang.ItemAngleX_Float_DisplayName, "Item Angle.X [Dec] (Float)");
            AttributeText.Add(aLang.ItemAngleX_Hex_DisplayName, "Item Angle.X [Hex] (Float)");
            AttributeText.Add(aLang.ItemAngleX_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: ITEM_ANGLE.X  \r\n\r\n###############");

            AttributeText.Add(aLang.ItemAngleY_Float_DisplayName, "Item Angle.Y [Dec] (Float)");
            AttributeText.Add(aLang.ItemAngleY_Hex_DisplayName, "Item Angle.Y [Hex] (Float)");
            AttributeText.Add(aLang.ItemAngleY_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: ITEM_ANGLE.Y  \r\n\r\n###############");

            AttributeText.Add(aLang.ItemAngleZ_Float_DisplayName, "Item Angle.Z [Dec] (Float)");
            AttributeText.Add(aLang.ItemAngleZ_Hex_DisplayName, "Item Angle.Z [Hex] (Float)");
            AttributeText.Add(aLang.ItemAngleZ_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: ITEM_ANGLE.Z  \r\n\r\n###############");

            AttributeText.Add(aLang.ItemAngleW_ByteArray4_DisplayName, "Item Angle.W [Hex] (Byte[4])");
            AttributeText.Add(aLang.ItemAngleW_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: ITEM_ANGLE.W  \r\n\r\n###############");


            // type 0x14 AshleyHiding

            AttributeText.Add(aLang.AshleyHidingPointX_Float_DisplayName, "Hiding Point.X [Dec] (Float)");
            AttributeText.Add(aLang.AshleyHidingPointX_Hex_DisplayName, "Hiding Point.X [Hex] (Float)");
            AttributeText.Add(aLang.AshleyHidingPointX_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: HidingPoint.X  \r\n\r\n###############");

            AttributeText.Add(aLang.AshleyHidingPointY_Float_DisplayName, "Hiding Point.Y [Dec] (Float)");
            AttributeText.Add(aLang.AshleyHidingPointY_Hex_DisplayName, "Hiding Point.Y [Hex] (Float)");
            AttributeText.Add(aLang.AshleyHidingPointY_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: HidingPoint.Y  \r\n\r\n###############");

            AttributeText.Add(aLang.AshleyHidingPointZ_Float_DisplayName, "Hiding Point.Z [Dec] (Float)");
            AttributeText.Add(aLang.AshleyHidingPointZ_Hex_DisplayName, "Hiding Point.Z [Hex] (Float)");
            AttributeText.Add(aLang.AshleyHidingPointZ_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: HidingPoint.Z  \r\n\r\n###############");

            AttributeText.Add(aLang.AshleyHidingZoneCorner0_X_Float_DisplayName, "HidingZone Corner0.X [Dec] (Float)");
            AttributeText.Add(aLang.AshleyHidingZoneCorner0_X_Hex_DisplayName, "HidingZone Corner0.X [Hex] (Float)");
            AttributeText.Add(aLang.AshleyHidingZoneCorner0_X_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: HidingZoneCorner0.X  \r\n\r\n###############");

            AttributeText.Add(aLang.AshleyHidingZoneCorner0_Z_Float_DisplayName, "HidingZone Corner0.Z [Dec] (Float)");
            AttributeText.Add(aLang.AshleyHidingZoneCorner0_Z_Hex_DisplayName, "HidingZone Corner0.Z [Hex] (Float)");
            AttributeText.Add(aLang.AshleyHidingZoneCorner0_Z_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: HidingZoneCorner0.Z  \r\n\r\n###############");

            AttributeText.Add(aLang.AshleyHidingZoneCorner1_X_Float_DisplayName, "HidingZone Corner1.X [Dec] (Float)");
            AttributeText.Add(aLang.AshleyHidingZoneCorner1_X_Hex_DisplayName, "HidingZone Corner1.X [Hex] (Float)");
            AttributeText.Add(aLang.AshleyHidingZoneCorner1_X_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: HidingZoneCorner1.X  \r\n\r\n###############");

            AttributeText.Add(aLang.AshleyHidingZoneCorner1_Z_Float_DisplayName, "HidingZone Corner1.Z [Dec] (Float)");
            AttributeText.Add(aLang.AshleyHidingZoneCorner1_Z_Hex_DisplayName, "HidingZone Corner1.Z [Hex] (Float)");
            AttributeText.Add(aLang.AshleyHidingZoneCorner1_Z_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: HidingZoneCorner1.Z  \r\n\r\n###############");

            AttributeText.Add(aLang.AshleyHidingZoneCorner2_X_Float_DisplayName, "HidingZone Corner2.X [Dec] (Float)");
            AttributeText.Add(aLang.AshleyHidingZoneCorner2_X_Hex_DisplayName, "HidingZone Corner2.X [Hex] (Float)");
            AttributeText.Add(aLang.AshleyHidingZoneCorner2_X_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: HidingZoneCorner2.X  \r\n\r\n###############");

            AttributeText.Add(aLang.AshleyHidingZoneCorner2_Z_Float_DisplayName, "HidingZone Corner2.Z [Dec] (Float)");
            AttributeText.Add(aLang.AshleyHidingZoneCorner2_Z_Hex_DisplayName, "HidingZone Corner2.Z [Hex] (Float)");
            AttributeText.Add(aLang.AshleyHidingZoneCorner2_Z_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: HidingZoneCorner2.Z  \r\n\r\n###############");

            AttributeText.Add(aLang.AshleyHidingZoneCorner3_X_Float_DisplayName, "HidingZone Corner3.X [Dec] (Float)");
            AttributeText.Add(aLang.AshleyHidingZoneCorner3_X_Hex_DisplayName, "HidingZone Corner3.X [Hex] (Float)");
            AttributeText.Add(aLang.AshleyHidingZoneCorner3_X_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: HidingZoneCorner3.X  \r\n\r\n###############");

            AttributeText.Add(aLang.AshleyHidingZoneCorner3_Z_Float_DisplayName, "HidingZone Corner3.Z [Dec] (Float)");
            AttributeText.Add(aLang.AshleyHidingZoneCorner3_Z_Hex_DisplayName, "HidingZone Corner3.Z [Hex] (Float)");
            AttributeText.Add(aLang.AshleyHidingZoneCorner3_Z_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: HidingZoneCorner3.Z  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_SM_ByteArray4_DisplayName, "Unknown SM [Hex] (Byte[4])");
            AttributeText.Add(aLang.Unknown_SM_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_SM  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_SN_ByteArray4_DisplayName, "Unknown SN [Hex] (Byte[4])");
            AttributeText.Add(aLang.Unknown_SN_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_SN  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_SP_Byte_DisplayName, "Unknown SP [Hex] (Byte)");
            AttributeText.Add(aLang.Unknown_SP_Byte_Description, "Offset[<<Offset1>>]  \r\nTAG: U_SP  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_SQ_Byte_DisplayName, "Unknown SQ [Hex] (Byte)");
            AttributeText.Add(aLang.Unknown_SQ_Byte_Description, "Offset[<<Offset1>>]  \r\nTAG: U_SQ  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_SR_ByteArray2_DisplayName, "Unknown SR [Hex] (Byte[2])");
            AttributeText.Add(aLang.Unknown_SR_ByteArray2_Description, "Offset[<<Offset1>>][<<Offset2>>]  \r\nTAG: U_SR  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_SS_ByteArray4_DisplayName, "Unknown SS [Hex] (Byte[4])");
            AttributeText.Add(aLang.Unknown_SS_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_SS  \r\n\r\n###############");


            // type 0x15 Ada Grapple Gun

            AttributeText.Add(aLang.GrappleGunEndPointX_Float_DisplayName, "End Point.X [Dec] (Float)");
            AttributeText.Add(aLang.GrappleGunEndPointX_Hex_DisplayName, "End Point.X [Hex] (Float)");
            AttributeText.Add(aLang.GrappleGunEndPointX_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: EndPoint.X  \r\n\r\n###############");

            AttributeText.Add(aLang.GrappleGunEndPointY_Float_DisplayName, "End Point.Y [Dec] (Float)");
            AttributeText.Add(aLang.GrappleGunEndPointY_Hex_DisplayName, "End Point.Y [Hex] (Float)");
            AttributeText.Add(aLang.GrappleGunEndPointY_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: EndPoint.Y  \r\n\r\n###############");

            AttributeText.Add(aLang.GrappleGunEndPointZ_Float_DisplayName, "End Point.Z [Dec] (Float)");
            AttributeText.Add(aLang.GrappleGunEndPointZ_Hex_DisplayName, "End Point.Z [Hex] (Float)");
            AttributeText.Add(aLang.GrappleGunEndPointZ_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: EndPoint.Z  \r\n\r\n###############");

            AttributeText.Add(aLang.GrappleGunEndPointW_ByteArray4_DisplayName, "End Point.W [Hex] (Byte[4]) {Only Classic}");
            AttributeText.Add(aLang.GrappleGunEndPointW_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: EndPoint.W  \r\n\r\n###############");

            AttributeText.Add(aLang.GrappleGunThirdPointX_Float_DisplayName, "Third Point.X [Dec] (Float)");
            AttributeText.Add(aLang.GrappleGunThirdPointX_Hex_DisplayName, "Third Point.X [Hex] (Float)");
            AttributeText.Add(aLang.GrappleGunThirdPointX_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: ThirdPoint.X  \r\n\r\n###############");

            AttributeText.Add(aLang.GrappleGunThirdPointY_Float_DisplayName, "Third Point.Y [Dec] (Float)");
            AttributeText.Add(aLang.GrappleGunThirdPointY_Hex_DisplayName, "Third Point.Y [Hex] (Float)");
            AttributeText.Add(aLang.GrappleGunThirdPointY_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: ThirdPoint.Y  \r\n\r\n###############");

            AttributeText.Add(aLang.GrappleGunThirdPointZ_Float_DisplayName, "Third Point.Z [Dec] (Float)");
            AttributeText.Add(aLang.GrappleGunThirdPointZ_Hex_DisplayName, "Third Point.Z [Hex] (Float)");
            AttributeText.Add(aLang.GrappleGunThirdPointZ_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: ThirdPoint.Z  \r\n\r\n###############");

            AttributeText.Add(aLang.GrappleGunThirdPointW_ByteArray4_DisplayName, "Third Point.W [Hex] (Byte[4]) {Only Classic}");
            AttributeText.Add(aLang.GrappleGunThirdPointW_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: ThirdPoint.W  \r\n\r\n###############");

            AttributeText.Add(aLang.GrappleGunFacingAngle_Float_DisplayName, "GrappleGun Facing Angle [Dec] (Float)");
            AttributeText.Add(aLang.GrappleGunFacingAngle_Hex_DisplayName, "GrappleGun Facing Angle [Hex] (Float)");
            AttributeText.Add(aLang.GrappleGunFacingAngle_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: GrappleGunFacingAngle  \r\n\r\n###############");


            AttributeText.Add(aLang.GrappleGunParameter0_Byte_DisplayName, "GrappleGun Parameter 0 [Hex] (Byte)");
            AttributeText.Add(aLang.GrappleGunParameter0_Byte_Description, "Offset[<<Offset1>>]  \r\nTAG: GrappleGunParameter[0]  \r\n\r\n###############");

            AttributeText.Add(aLang.GrappleGunParameter1_Byte_DisplayName, "GrappleGun Parameter 1 [Hex] (Byte)");
            AttributeText.Add(aLang.GrappleGunParameter1_Byte_Description, "Offset[<<Offset1>>]  \r\nTAG: GrappleGunParameter[1]  \r\n\r\n###############");

            AttributeText.Add(aLang.GrappleGunParameter2_Byte_DisplayName, "GrappleGun Parameter 2 [Hex] (Byte)");
            AttributeText.Add(aLang.GrappleGunParameter2_Byte_Description, "Offset[<<Offset1>>]  \r\nTAG: GrappleGunParameter[2]  \r\n\r\n###############");

            AttributeText.Add(aLang.GrappleGunParameter3_Byte_DisplayName, "GrappleGun Parameter 3 [Hex] (Byte)");
            AttributeText.Add(aLang.GrappleGunParameter3_Byte_Description, "Offset[<<Offset1>>]  \r\nTAG: GrappleGunParameter[3]  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_SK_ByteArray4_DisplayName, "Unknown SK [Hex] (Byte[4])");
            AttributeText.Add(aLang.Unknown_SK_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_SK  \r\n\r\n###############");

            AttributeText.Add(aLang.Unknown_SL_ByteArray4_DisplayName, "Unknown SL [Hex] (Byte[4])");
            AttributeText.Add(aLang.Unknown_SL_ByteArray4_Description, "Offset[<<Offset1>>][<<Offset2>>][<<Offset3>>][<<Offset4>>]  \r\nTAG: U_SL  \r\n\r\n###############");


            //MultiSelect

            AttributeText.Add(aLang.MultiSelectCategory, "MultiSelect");
            AttributeText.Add(aLang.MultiSelectInfoDisplayName, "MultiSelect Info");
            AttributeText.Add(aLang.MultiSelectInfoValueText, "Read-me");
            AttributeText.Add(aLang.MultiSelectInfoDescription, "Click on the 3 dots to access the multi-selection editor, use with care as changes cannot be undone.");
            AttributeText.Add(aLang.MultiSelectEnemyDisplayName, "Enemies (ESL)");
            AttributeText.Add(aLang.MultiSelectEtcmodelDisplayName, "EtcModels (ETS)");
            AttributeText.Add(aLang.MultiSelectSpecialItaDisplayName, "Items (ITA)");
            AttributeText.Add(aLang.MultiSelectSpecialAevDisplayName, "Events (AEV)");
            AttributeText.Add(aLang.MultiSelectAmountSelected, "Amount Selected");


            /*

            */

            //AttributeText.Add(aLang.Null, "");
        }

        /// <summary>
        /// todos os outros texto usados pelo programa
        /// </summary>
        public static void StartTexts() 
        {
            // MessageBox texts
            Text.Add(eLang.MessageBoxErrorTitle, "Error:");
            Text.Add(eLang.MessageBoxWarningTitle, "Warning:");
            Text.Add(eLang.MessageBoxFile16MB, "Files bigger than 16Mb cannot be opened!");
            Text.Add(eLang.MessageBoxFile0MB, "Files without content cannot be opened!");
            Text.Add(eLang.MessageBoxFile16Bytes, "Files smaller than 16 bytes cannot be opened!");
            Text.Add(eLang.MessageBoxFileNotOpen, "Could not open file!");

            Text.Add(eLang.MessageBoxFormClosingTitle, "Warning:");
            Text.Add(eLang.MessageBoxFormClosingDialog, "Any unsaved changes will be lost.\r\nWould you really like to close the program?");

            // nodes names
            Text.Add(eLang.NodeESL, "Enemies (ESL)");
            Text.Add(eLang.NodeETS, "EtcModels (ETS)");
            Text.Add(eLang.NodeITA, "Items (ITA)");
            Text.Add(eLang.NodeAEV, "Events (AEV)");
            Text.Add(eLang.NodeEXTRAS, "Extras");

            Text.Add(eLang.AddNewETS, "Add new EtcModel object");
            Text.Add(eLang.AddNewITA, "Add new Item ITA object");
            Text.Add(eLang.AddNewAEV, "Add new Event AEV object");
            Text.Add(eLang.AddNewNull, "None");

            Text.Add(eLang.DeleteObjWarning, "Warning:");
            Text.Add(eLang.DeleteObjDialog, "Selected objects will be deleted, are you sure?\r\nThe action cannot be undone!");

            Text.Add(eLang.MultiSelectEditorFinishMessageBoxTitle, "Done:");
            Text.Add(eLang.MultiSelectEditorFinishMessageBoxDialog, "Property value changes on objects have been made!");

            Text.Add(eLang.OptionsFormWarningLoadModelsMessageBoxTitle, "Warning:");
            Text.Add(eLang.OptionsFormWarningLoadModelsMessageBoxDialog, "Warning: Files stored in HD (hard drive) can have much higher loading time, after hit OK, please wait until the options windows is closed.");

            //OptionsFormSelectDiretory
            Text.Add(eLang.OptionsFormSelectDiretory, "Select folder directory to:");

            Text.Add(eLang.OptionsUseInternalLanguage, "Use Internal Language");

            //labels
            Text.Add(eLang.labelCamSpeedPercentage, "Cam speed:");
            Text.Add(eLang.labelObjSpeed, "Move speed:");

            // diretory Patch
            Text.Add(eLang.DiretoryESL, "ESL File Patch:");
            Text.Add(eLang.DiretoryETS, "ETS File Patch:");
            Text.Add(eLang.DiretoryITA, "ITA File Patch:");
            Text.Add(eLang.DiretoryAEV, "AEV File Patch:");

            //room
            Text.Add(eLang.SelectedRoom, "Selected Room");
            Text.Add(eLang.SelectRoom, "Select Room");
            Text.Add(eLang.NoneRoom, "None");

            // MoveMode
            Text.Add(eLang.MoveMode_Enemy_PositionAndRotationAll, "Enemy: Squad and Vertical = Position; Horizontal [123] = Rotation XYZ;");
            Text.Add(eLang.MoveMode_EtcModel_PositionAndRotationAll, "EtcModel: Squad and Vertical = Position; Horizontal [123] = Rotation XYZ;");
            Text.Add(eLang.MoveMode_EtcModel_Scale, "EtcModel: Vertical = Scale All; Horizontal [123] = Scale XYZ; Scale does not work in game;");
            Text.Add(eLang.MoveMode_Item_PositionAndRotationAll, "Item: Squad and Vertical = Position; Horizontal [123] = Rotation XYZ;");
            Text.Add(eLang.MoveMode_TriggerZone_MoveAll, "TriggerZone: Squad and Vertical = Position All; Horizontal[1] = Height; Horizontal[2] = Rotation; Horizontal[3] = Scale;");
            Text.Add(eLang.MoveMode_TriggerZone_Point0, "TriggerZone: Squad and Vertical = Position Point 0; Horizontal[1] = Height;");
            Text.Add(eLang.MoveMode_TriggerZone_Point1, "TriggerZone: Squad and Vertical = Position Point 1; Horizontal[1] = Height;");
            Text.Add(eLang.MoveMode_TriggerZone_Point2, "TriggerZone: Squad and Vertical = Position Point 2; Horizontal[1] = Height;");
            Text.Add(eLang.MoveMode_TriggerZone_Point3, "TriggerZone: Squad and Vertical = Position Point 3; Horizontal[1] = Height;");
            Text.Add(eLang.MoveMode_TriggerZone_Wall01, "TriggerZone: Squad and Vertical = Position Wall Mode Point 0 and 1; Horizontal[1] = Height;");
            Text.Add(eLang.MoveMode_TriggerZone_Wall12, "TriggerZone: Squad and Vertical = Position Wall Mode Point 1 and 2; Horizontal[1] = Height;");
            Text.Add(eLang.MoveMode_TriggerZone_Wall23, "TriggerZone: Squad and Vertical = Position Wall Mode Point 2 and 3; Horizontal[1] = Height;");
            Text.Add(eLang.MoveMode_TriggerZone_Wall30, "TriggerZone: Squad and Vertical = Position Wall Mode Point 3 and 0; Horizontal[1] = Height;");
            Text.Add(eLang.MoveMode_SpecialObj_Position, "Special Object: Squad and Vertical = Position All;");
            Text.Add(eLang.MoveMode_Obj_PositionAndRotationAll, "Object: Squad and Vertical = Position; Horizontal 123 = Rotation XYZ;");
            Text.Add(eLang.MoveMode_Obj_PositionAndRotationY, "Object: Squad and Vertical = Position; Horizontal[2] = Rotation Y;");
            Text.Add(eLang.MoveMode_Obj_Position, "Object: Squad and Vertical = Position;");
            Text.Add(eLang.MoveMode_Ashley_Position, "Ashley Point: Squad and Vertical = Position;");
            Text.Add(eLang.MoveMode_AshleyZone_MoveAll, "AshleyZone: Squad = Position All; Horizontal[2] = Rotation; Horizontal[3] = Scale;");
            Text.Add(eLang.MoveMode_AshleyZone_Point0, "AshleyZone: Squad = Position Point 0;");
            Text.Add(eLang.MoveMode_AshleyZone_Point1, "AshleyZone: Squad = Position Point 1;");
            Text.Add(eLang.MoveMode_AshleyZone_Point2, "AshleyZone: Squad = Position Point 2;");
            Text.Add(eLang.MoveMode_AshleyZone_Point3, "AshleyZone: Squad = Position Point 3;");



            // item rotation options
            Text.Add(eLang.RotationXYZ, "Rotation XYZ");
            Text.Add(eLang.RotationXZY, "Rotation XZY");
            Text.Add(eLang.RotationYXZ, "Rotation YXZ");
            Text.Add(eLang.RotationYZX, "Rotation YZX");
            Text.Add(eLang.RotationZYX, "Rotation ZYX");
            Text.Add(eLang.RotationZXY, "Rotation ZXY");
            Text.Add(eLang.RotationXY, "Rotation XY");
            Text.Add(eLang.RotationXZ, "Rotation XZ");
            Text.Add(eLang.RotationYX, "Rotation YX");
            Text.Add(eLang.RotationYZ, "Rotation YZ");
            Text.Add(eLang.RotationZX, "Rotation ZX");
            Text.Add(eLang.RotationZY, "Rotation ZY");
            Text.Add(eLang.RotationX, "Rotation X");
            Text.Add(eLang.RotationY, "Rotation Y");
            Text.Add(eLang.RotationZ, "Rotation Z");



            // menus
            //subsubmenu Save
            Text.Add(eLang.toolStripMenuItemSaveETS, "Save EtcModel ETS File");
            Text.Add(eLang.toolStripMenuItemSaveITA, "Save Itens ITA File");
            Text.Add(eLang.toolStripMenuItemSaveAEV, "Save Events AEV File");

            Text.Add(eLang.toolStripMenuItemSaveETS_2007_PS2, "Save EtcModel ETS File (2007/PS2)");
            Text.Add(eLang.toolStripMenuItemSaveITA_2007_PS2, "Save Itens ITA File (2007/PS2)");
            Text.Add(eLang.toolStripMenuItemSaveAEV_2007_PS2, "Save Events AEV File (2007/PS2)");

            Text.Add(eLang.toolStripMenuItemSaveETS_UHD, "Save EtcModel ETS File (UHD)");
            Text.Add(eLang.toolStripMenuItemSaveITA_UHD, "Save Itens ITA File (UHD)");
            Text.Add(eLang.toolStripMenuItemSaveAEV_UHD, "Save Events AEV File (UHD)");

            // subsubmenu Save As...
            Text.Add(eLang.toolStripMenuItemSaveAsETS, "Save As EtcModel ETS File");
            Text.Add(eLang.toolStripMenuItemSaveAsITA, "Save As Itens ITA File");
            Text.Add(eLang.toolStripMenuItemSaveAsAEV, "Save As Events AEV File");

            Text.Add(eLang.toolStripMenuItemSaveAsETS_2007_PS2, "Save As EtcModel ETS File (2007/PS2)");
            Text.Add(eLang.toolStripMenuItemSaveAsITA_2007_PS2, "Save As Itens ITA File (2007/PS2)");
            Text.Add(eLang.toolStripMenuItemSaveAsAEV_2007_PS2, "Save As Events AEV File (2007/PS2)");

            Text.Add(eLang.toolStripMenuItemSaveAsETS_UHD, "Save As EtcModel ETS File (UHD)");
            Text.Add(eLang.toolStripMenuItemSaveAsITA_UHD, "Save As Itens ITA File (UHD)");
            Text.Add(eLang.toolStripMenuItemSaveAsAEV_UHD, "Save As Events AEV File (UHD)");

            // subsubmenu Save As (Convert)
            Text.Add(eLang.toolStripMenuItemSaveConverterETS, "Save As EtcModel ETS File, Convert To ...");
            Text.Add(eLang.toolStripMenuItemSaveConverterITA, "Save As Itens ITA File, Convert To ...");
            Text.Add(eLang.toolStripMenuItemSaveConverterAEV, "Save As Events AEV File, Convert To ...");

            Text.Add(eLang.toolStripMenuItemSaveConverterETS_2007_PS2, "Save As EtcModel ETS File, Convert To 2007/PS2");
            Text.Add(eLang.toolStripMenuItemSaveConverterITA_2007_PS2, "Save As Itens ITA File, Convert To 2007/PS2");
            Text.Add(eLang.toolStripMenuItemSaveConverterAEV_2007_PS2, "Save As Events AEV File, Convert To 2007/PS2");

            Text.Add(eLang.toolStripMenuItemSaveConverterETS_UHD, "Save As EtcModel ETS File, Convert To UHD");
            Text.Add(eLang.toolStripMenuItemSaveConverterITA_UHD, "Save As Itens ITA File, Convert To UHD");
            Text.Add(eLang.toolStripMenuItemSaveConverterAEV_UHD, "Save As Events AEV File, Convert To UHD");


            // enemy groups
            Text.Add(eLang.EnemyExtraSegmentSegund, "{Segund Segment}");
            Text.Add(eLang.EnemyExtraSegmentThird, "{Third Segment}");
            Text.Add(eLang.EnemyExtraSegmentNoSound, "[No Sound]");

            /*

            */

            //Text.Add(eLang.Null, "");
        }

        /// <summary>
        /// os texto que são usado quando é carregado uma tradução no programa
        /// </summary>
        public static void StartOthersTexts() 
        {
            // menu principal
            Text.Add(eLang.toolStripMenuItemFile, "File");
            Text.Add(eLang.toolStripMenuItemEdit, "Edit");
            Text.Add(eLang.toolStripMenuItemView, "View");
            Text.Add(eLang.toolStripMenuItemMisc, "Misc");
            //submenu File
            Text.Add(eLang.toolStripMenuItemNewFile, "New");
            Text.Add(eLang.toolStripMenuItemOpen, "Open");
            Text.Add(eLang.toolStripMenuItemSave, "Save");
            Text.Add(eLang.toolStripMenuItemSaveAs, "Save As ...");
            Text.Add(eLang.toolStripMenuItemSaveConverter, "Save As (Covert)");
            Text.Add(eLang.toolStripMenuItemClear, "Clear");
            Text.Add(eLang.toolStripMenuItemClose, "Close");
            // subsubmenu New
            Text.Add(eLang.toolStripMenuItemNewESL, "New Enemy ESL File");
            Text.Add(eLang.toolStripMenuItemNewETS_2007_PS2, "New EtcModel ETS File (2007/PS2)");
            Text.Add(eLang.toolStripMenuItemNewITA_2007_PS2, "New Itens ITA File (2007/PS2)");
            Text.Add(eLang.toolStripMenuItemNewAEV_2007_PS2, "New Events AEV File (2007/PS2)");
            Text.Add(eLang.toolStripMenuItemNewETS_UHD, "New EtcModel ETS File (UHD)");
            Text.Add(eLang.toolStripMenuItemNewITA_UHD, "New Itens ITA File (UHD)");
            Text.Add(eLang.toolStripMenuItemNewAEV_UHD, "New Events AEV File (UHD)");
            // subsubmenu Open
            Text.Add(eLang.toolStripMenuItemOpenESL, "Open Enemy ESL File");
            Text.Add(eLang.toolStripMenuItemOpenETS_2007_PS2, "Open EtcModel ETS File (2007/PS2)");
            Text.Add(eLang.toolStripMenuItemOpenITA_2007_PS2, "Open Itens ITA File (2007/PS2)");
            Text.Add(eLang.toolStripMenuItemOpenAEV_2007_PS2, "Open Events AEV File (2007/PS2)");
            Text.Add(eLang.toolStripMenuItemOpenETS_UHD, "Open EtcModel ETS File (UHD)");
            Text.Add(eLang.toolStripMenuItemOpenITA_UHD, "Open Itens ITA File (UHD)");
            Text.Add(eLang.toolStripMenuItemOpenAEV_UHD, "Open Events AEV File (UHD)");
            // subsubmenu Save
            Text.Add(eLang.toolStripMenuItemSaveESL, "Save Enemy ESL File");
            Text.Add(eLang.toolStripMenuItemSaveDirectories, "Directories");
            // subsubmenu Save As...
            Text.Add(eLang.toolStripMenuItemSaveAsESL, "Save As Enemy ESL File");
            // subsubmenu Clear
            Text.Add(eLang.toolStripMenuItemClearESL, "Clear Enemy ESL List");
            Text.Add(eLang.toolStripMenuItemClearETS, "Clear EtcModel ETS List");
            Text.Add(eLang.toolStripMenuItemClearITA, "Clear Itens ITA List");
            Text.Add(eLang.toolStripMenuItemClearAEV, "Clear Events AEV List");

            // sub menu edit
            Text.Add(eLang.toolStripMenuItemAddNewObj, "Add New Object(s)");
            Text.Add(eLang.toolStripMenuItemDeleteSelectedObj, "Delete Selected(s) object(s)");
            Text.Add(eLang.toolStripMenuItemMoveUp, "Move Object To Up");
            Text.Add(eLang.toolStripMenuItemMoveDown, "Move Object To Down");
            Text.Add(eLang.toolStripMenuItemSearch, "Search");

            // sub menu Misc
            Text.Add(eLang.toolStripMenuItemOptions, "Options");
            Text.Add(eLang.toolStripMenuItemCredits, "Credits");

            // sub menu View
            Text.Add(eLang.toolStripMenuItemHideRoomModel, "Hide Room Model");
            Text.Add(eLang.toolStripMenuItemHideEnemyESL, "Hide Enemy .ESL");
            Text.Add(eLang.toolStripMenuItemHideEtcmodelETS, "Hide Etcmodel .ETS");
            Text.Add(eLang.toolStripMenuItemHideItemsITA, "Hide Items .ITA");
            Text.Add(eLang.toolStripMenuItemHideEventsAEV, "Hide Events .AEV");
            Text.Add(eLang.toolStripMenuItemHideLateralMenu, "Hide Lateral Menu");
            Text.Add(eLang.toolStripMenuItemHideBottomMenu, "Hide Bottom Menu");
            Text.Add(eLang.toolStripMenuItemSubMenuRoom, "Room Options");
            Text.Add(eLang.toolStripMenuItemSubMenuModels, "Models Options");
            Text.Add(eLang.toolStripMenuItemSubMenuEnemy, "Enemy Options");
            Text.Add(eLang.toolStripMenuItemSubMenuItem, "Item Options");
            Text.Add(eLang.toolStripMenuItemSubMenuSpecial, "Special Options");
            Text.Add(eLang.toolStripMenuItemSubMenuEtcModel, "EtcModel Options");
            Text.Add(eLang.toolStripMenuItemNodeDisplayNameInHex, "Display Obj Name In Hex");
            Text.Add(eLang.toolStripMenuItemCameraMenu, "Camera Menu");
            Text.Add(eLang.toolStripMenuItemResetCamera, "Reset Camera");
            Text.Add(eLang.toolStripMenuItemRefresh, "Refresh Display");

            // sub menus de view
            Text.Add(eLang.toolStripMenuItemHideDesabledEnemy, "Hide Desabled Enemy");
            Text.Add(eLang.toolStripMenuItemShowOnlyDefinedRoom, "Show Only Defined Room:");
            Text.Add(eLang.toolStripMenuItemAutoDefineRoom, "Auto Define Room");
            Text.Add(eLang.toolStripMenuItemItemPositionAtAssociatedObjectLocation, "Item Position At Associated Object Location");
            Text.Add(eLang.toolStripMenuItemHideItemTriggerZone, "Hide Item Trigger Zone");
            Text.Add(eLang.toolStripMenuItemHideItemTriggerRadius, "Hide Item Trigger Radius");
            Text.Add(eLang.toolStripMenuItemHideSpecialTriggerZone, "Hide Special Trigger Zone");
            Text.Add(eLang.toolStripMenuItemHideExtraObjs, "Hide Extra Objects");
            Text.Add(eLang.toolStripMenuItemHideOnlyWarpDoor, "Hide Only Warp Door");
            Text.Add(eLang.toolStripMenuItemHideExtraExceptWarpDoor, "Hide Extra Except Warp Door");
            Text.Add(eLang.toolStripMenuItemUseMoreSpecialColors, "Use More Special Colors");
            Text.Add(eLang.toolStripMenuItemEtcModelUseScale, "EtcModel Use Scale");

            // sub menu de view room and model
            Text.Add(eLang.toolStripMenuItemModelsHideTextures, "Hide Textures");
            Text.Add(eLang.toolStripMenuItemModelsWireframe, "Enable Wireframe");
            Text.Add(eLang.toolStripMenuItemModelsRenderNormals, "Enable Normals");
            Text.Add(eLang.toolStripMenuItemModelsOnlyFrontFace, "Enable Only Front Face");
            Text.Add(eLang.toolStripMenuItemModelsVertexColor, "Disable Vertex Color");
            Text.Add(eLang.toolStripMenuItemModelsAlphaChannel, "Disable Alpha Channel");
            Text.Add(eLang.toolStripMenuItemRoomHideTextures, "Hide Textures");
            Text.Add(eLang.toolStripMenuItemRoomWireframe, "Enable Wireframe");
            Text.Add(eLang.toolStripMenuItemRoomRenderNormals, "Enable Normals");
            Text.Add(eLang.toolStripMenuItemRoomOnlyFrontFace, "Enable Only Front Face");
            Text.Add(eLang.toolStripMenuItemRoomVertexColor, "Disable Vertex Color");
            Text.Add(eLang.toolStripMenuItemRoomAlphaChannel, "Disable Alpha Channel");

            //save and open windows
            Text.Add(eLang.openFileDialogAEV, "Open Events AEV File");
            Text.Add(eLang.openFileDialogESL, "Open Enemy ESL File");
            Text.Add(eLang.openFileDialogETS, "Open EtcModel ETS File");
            Text.Add(eLang.openFileDialogITA, "Open Itens ITA File");
            Text.Add(eLang.saveFileDialogAEV, "Save Events AEV File");
            Text.Add(eLang.saveFileDialogConvertAEV, "Save Convert Events AEV File");
            Text.Add(eLang.saveFileDialogConvertETS, "Save Convert EtcModel ETS File");
            Text.Add(eLang.saveFileDialogConvertITA, "Save Convert Itens ITA File");
            Text.Add(eLang.saveFileDialogESL, "Save Enemy ESL File");
            Text.Add(eLang.saveFileDialogETS, "Save EtcModel ETS File");
            Text.Add(eLang.saveFileDialogITA, "Save Itens ITA File");

            //cameraMove
            Text.Add(eLang.buttonGrid, "Grid");
            Text.Add(eLang.labelCamModeText, "Camera Mode:");
            Text.Add(eLang.labelMoveCamText, "Move Camera");
            Text.Add(eLang.CameraMode_Fly, "Fly");
            Text.Add(eLang.CameraMode_Orbit, "Orbit");
            Text.Add(eLang.CameraMode_Top, "Top");
            Text.Add(eLang.CameraMode_Bottom, "Bottom");
            Text.Add(eLang.CameraMode_Left, "Left");
            Text.Add(eLang.CameraMode_Right, "Right");
            Text.Add(eLang.CameraMode_Front, "Front");
            Text.Add(eLang.CameraMode_Back, "Back");


            // objectMove
            Text.Add(eLang.buttonDropToGround, "Drop to ground");
            Text.Add(eLang.checkBoxKeepOnGround, "Keep on ground");
            Text.Add(eLang.checkBoxLockMoveSquareHorizontal, "Lock move square horizontal");
            Text.Add(eLang.checkBoxLockMoveSquareVertical, "Lock move square vertical");
            Text.Add(eLang.checkBoxMoveRelativeCam, "Move relative to camera");


            //AddNewObjForm
            Text.Add(eLang.AddNewObjForm, "Add New Object");
            Text.Add(eLang.buttonCancel, "CANCEL");
            Text.Add(eLang.buttonOK, "OK");
            Text.Add(eLang.labelAmountInfo, "<- Amount to be added");
            Text.Add(eLang.labelTypeInfo, "Select the type of object to be added:");


            //MultiSelectEditor
            Text.Add(eLang.MultiSelectEditor, "Multi Select Editor");
            Text.Add(eLang.labelValueSumText2, "Value to be added:");
            Text.Add(eLang.labelValueSumText, "Value to be added:");
            Text.Add(eLang.labelPropertyDescriptionText, "Property description:");
            Text.Add(eLang.labelChoiseText, "Choose the property to be edited:");
            Text.Add(eLang.checkBoxProgressiveSum, "Progressively add");
            Text.Add(eLang.checkBoxHexadecimal, "Hexadecimal editor mode:");
            Text.Add(eLang.checkBoxDecimal, "Decimal editor mode:");
            Text.Add(eLang.checkBoxCurrentValuePlusValueToAdd, "Current value plus Value To add");
            Text.Add(eLang.buttonSetValue, "Set value");
            Text.Add(eLang.buttonClose, "Close");


            //SelectRoomForm
            Text.Add(eLang.SelectRoomForm, "Select Room");
            Text.Add(eLang.labelInfo, "After click \"LOAD\", wait for the room to be loaded.");
            Text.Add(eLang.labelSelectAList, "Select a List:");
            Text.Add(eLang.labelSelectARoom, "Select a Room:");
            Text.Add(eLang.buttonLoad, "LOAD");
            Text.Add(eLang.buttonCancel2, "CANCEL");


            //OptionsForm
            Text.Add(eLang.OptionsForm, "Options");
            Text.Add(eLang.Options_buttonCancel, "CANCEL");
            Text.Add(eLang.Options_buttonOK, "OK");
            Text.Add(eLang.checkBoxDisableItemRotations, "Disable Item Rotations");
            Text.Add(eLang.checkBoxForceReloadModels, "Force Reload Models And Json Files");
            Text.Add(eLang.checkBoxIgnoreRotationForZeroXYZ, "Ignore rotation if any of XYZ field is zero");
            Text.Add(eLang.checkBoxIgnoreRotationForZisNotGreaterThanZero, "Ignore rotation if Z field is not greater than zero");
            Text.Add(eLang.groupBoxColors, "Colors");
            Text.Add(eLang.groupBoxDirectory, "Directories");
            Text.Add(eLang.groupBoxFloatStyle, "Float Style");
            Text.Add(eLang.groupBoxFractionalPart, "Fractional Part Amount");
            Text.Add(eLang.groupBoxInputFractionalSymbol, "Input fractional symbol");
            Text.Add(eLang.groupBoxItemRotations, "Item Rotations");
            Text.Add(eLang.groupBoxLanguage, "Language");
            Text.Add(eLang.groupBoxOutputFractionalSymbol, "Output fractional symbol");
            Text.Add(eLang.labelDivider, "Divider:");
            Text.Add(eLang.labelItemExtraCalculation, "Extra Calculation: Radian = (Input * Multiplier) / Divider");
            Text.Add(eLang.labelitemRotationOrderText, "Rotation Order:");
            Text.Add(eLang.labelLanguageWarning, "Language changes only take effect after program restart");
            Text.Add(eLang.labelMultiplier, "Multiplier:");
            Text.Add(eLang.labelSkyColor, "Sky Color");
            Text.Add(eLang.labelOptionsDirectory, "Directory:");
            Text.Add(eLang.radioButtonAcceptsCommaAndPeriod, "Accepts comma and period");
            Text.Add(eLang.radioButtonOnlyAcceptComma, "Only accept comma");
            Text.Add(eLang.radioButtonOnlyAcceptPeriod, "Only accept period");
            Text.Add(eLang.radioButtonOutputComma, "Output Comma");
            Text.Add(eLang.radioButtonOutputPeriod, "Output Period");
            Text.Add(eLang.tabPageDiretory, "Diretory");
            Text.Add(eLang.tabPageOthers, "Other");
            Text.Add(eLang.tabPageLists, "Lists");
            Text.Add(eLang.groupBoxLists, "Lists");
            Text.Add(eLang.labelEnemies, "Enemies");
            Text.Add(eLang.labelEtcModels, "EtcModels");
            Text.Add(eLang.labelItems, "Items");
            Text.Add(eLang.groupBoxTheme, "Theme");
            Text.Add(eLang.labelThemeWarning, "Theme changes only take effect after program restart");
            Text.Add(eLang.checkBoxUseDarkerGrayTheme, "Use Darker Gray Theme");


            //SearchForm
            //checkBoxFilterMode
            Text.Add(eLang.SearchForm, "Search");
            Text.Add(eLang.checkBoxFilterMode, "Filter Mode");

            //CameraForm
            Text.Add(eLang.CameraForm, "Camera");
            Text.Add(eLang.CameraLabelInfo, "The values are on a scale of 1/100");
            Text.Add(eLang.CameraButtonClose, "Close");
            Text.Add(eLang.CameraButtonGetPos, "Get Camera Position");
            Text.Add(eLang.CamaraButtonSetPos, "Set Camera Position");

            /*

            */

            //Text.Add(eLang.Null, "");
        }
    }
}
