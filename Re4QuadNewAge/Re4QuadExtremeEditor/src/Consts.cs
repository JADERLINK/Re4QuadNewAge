using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re4QuadExtremeEditor.src
{
    /// <summary>
    /// todas as constantes do programa
    /// </summary>
    public static class Consts
    {
        #region Program Directories
        // lista de de diretorios do programa
        // diretorio principal
        public const string dataDirectory = @"data";
        // diretorio do arquivo de configurações
        public const string ConfigsFileDirectory = @"Configs.json";


        // diretorios com as listagem
        public const string RoomsDirectory = @"data\Rooms";
        public const string ItemsDirectory = @"data\Items";
        public const string EtcModelsDirectory = @"data\EtcModels";
        public const string EnemiesDirectory = @"data\Enemies";
        public const string InternalModelsDirectory = @"data\Internal";
        public const string QuadCustomDirectory = @"data\QuadCustom";
        
        // nome padrão dos arquivos de listagem
        public const string DefaultItemsListFileDirectory = @"itemslist.json"; // o nome do arquivo tem que ser em letra minuscula
        public const string DefaultEtcModelsListFileDirectory = @"etcmodelslist.json";
        public const string DefaultEnemiesListFileDirectory = @"enemieslist.json";
        public const string DefaultQuadCustomModelsListFileDirectory = @"quadcustomlist.json";

  
        // diretorio da lista de PromptMessages
        public const string PromptMessageListFileDirectory = @"data\Others\PromptMessageList.json";

        // diretorio da pasta de linguagens
        public const string LangDirectory = @"lang";

        #endregion

        //nome do grupo dentro do arquivo json
        public const string NameItemsList = @"ItemsList";
        public const string NameEtcModelsList = @"EtcModelsList";
        public const string NameEnemiesList = @"EnemiesList";
        public const string NameNull = @"null";

        // nome para os grupos
        public const string ItemsModelGroupName = "ItemsModelGroupName";
        public const string EtcModelGroupName = "EtcModelGroupName";
        public const string EnemiesModelGroupName = "EnemiesModelGroupName";
        public const string InternalModelGroupName = "InternalModelGroupName";
        public const string QuadCustomModelGroupName = "QuadCustomModelGroupName";

        // nomes dos nodes principais
        public const string NodeESL = "NodeESL";
        public const string NodeETS = "NodeETS";
        public const string NodeITA = "NodeITA";
        public const string NodeAEV = "NodeAEV";
        public const string NodeEXTRAS = "NodeEXTRAS";
        public const string NodeDSE = "NodeDSE";
        public const string NodeEMI = "NodeEMI";
        public const string NodeSAR = "NodeSAR";
        public const string NodeEAR = "NodeEAR";
        public const string NodeESE = "NodeESE";
        public const string NodeFSE = "NodeFSE";
        public const string NodeLIT_GROUPS = "NodeLIT_GROUPS";
        public const string NodeLIT_ENTRYS = "NodeLIT_ENTRYS";
        public const string NodeQuadCustom = "NodeQuadCustom";
        public const string NodeEFF_Table0 = "NodeEFF_Table0";
        public const string NodeEFF_Table1 = "NodeEFF_Table1";
        public const string NodeEFF_Table2 = "NodeEFF_Table2";
        public const string NodeEFF_Table3 = "NodeEFF_Table3";
        public const string NodeEFF_Table4 = "NodeEFF_Table4";
        public const string NodeEFF_Table6 = "NodeEFF_Table6";
        public const string NodeEFF_Table7_Effect_0 = "NodeEFF_Table7_Effect_0";
        public const string NodeEFF_Table8_Effect_1 = "NodeEFF_Table8_Effect_1";
        public const string NodeEFF_EffectEntry = "NodeEFF_EffectEntry";
        public const string NodeEFF_Table9 = "NodeEFF_Table9";

        // nomes dos modelos internos usados nos objetos extras
        public const string ModelKeyWarpPoint = "warparrow.json";
        public const string ModelKeyLadderPoint = "ladderx.json";
        public const string ModelKeyLadderObj = "ladder.json";
        public const string ModelKeyLadderError = "laddererror.json";
        public const string ModelKeyAshleyPoint = "ashleypoint.json";
        public const string ModelKeyGrappleGunPoint = "grapplegunarrow.json";
        public const string ModelKeyLocalTeleportationPoint = "localteleportationarrow.json";
        public const string ModelKeyQuadCustomPoint = "quadcustomarrow.json";
        public const string ModelKey_ESE_Point = "ese_point.json";
        public const string ModelKey_EMI_Point = "emi_point.json";
        public const string ModelKey_LIT_Point = "lit_point.json";
        public const string ModelKey_EFF_EntryPoint = "effentrypoint.json";
        public const string ModelKey_EFF_GroupPoint = "effgrouppoint.json";
        public const string ModelKey_EFF_Table9 = "efftable9.json";

        //quantidade limite de objetos de cada arquivo
        public const ushort AmountLimitETS = 5000;
        public const ushort AmountLimitAEV = 5000;
        public const ushort AmountLimitITA = 5000;
        public const ushort AmountLimitDSE = 5000;
        public const ushort AmountLimitFSE = 5000;
        public const ushort AmountLimitSAR = 5000;
        public const ushort AmountLimitEAR = 5000;
        public const ushort AmountLimitEMI = 5000;
        public const ushort AmountLimitESE = 5000;
        public const ushort AmountLimitQuadCustom = 5000;

        public const ushort AmountLimitLIT_Groups = 500;
        public const ushort AmountLimitLIT_Entrys = 5000;

        public const ushort AmountLimitEFF_Table0 = 1000;
        public const ushort AmountLimitEFF_Table1 = 1000;
        public const ushort AmountLimitEFF_Table2 = 1000;
        public const ushort AmountLimitEFF_Table3 = 1000;
        public const ushort AmountLimitEFF_Table4 = 1000;
        public const ushort AmountLimitEFF_Table6 = 1000;
        public const ushort AmountLimitEFF_Table7and8 = 1000;
        public const ushort AmountLimitEFF_Table9_Group = 1000;
        public const ushort AmountLimitEFF_Table9_entry = 5000;
        public const ushort AmountLimitEFF_EffectEntry = 10000;

        public const ushort QuadCustomLineArrayLength = 92;
        public const ushort QuadCustomTextLength = 500;


        //o meu maior float
        public const float MyFloatMax = 1_000_000_000_000_000_000_000_000_000f;
    }
}
