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
        
        // nome padrão dos arquivos de listagem
        public const string DefaultItemsListFileDirectory = @"itemslist.json"; // o nome do arquivo tem que ser em letra minuscula
        public const string DefaultEtcModelsListFileDirectory = @"etcmodelslist.json";
        public const string DefaultEnemiesListFileDirectory = @"enemieslist.json";

  
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

        // nomes dos nodes principais
        public const string NodeESL = "NodeESL";
        public const string NodeETS = "NodeETS";
        public const string NodeITA = "NodeITA";
        public const string NodeAEV = "NodeAEV";
        public const string NodeEXTRAS = "NodeEXTRAS";


        // nomes dos modelos internos usados nos objetos extras
        public const string ModelKeyWarpPoint = "warparrow.json";
        public const string ModelKeyLadderPoint = "ladderx.json";
        public const string ModelKeyLadderObj = "ladder.json";
        public const string ModelKeyLadderError = "laddererror.json";
        public const string ModelKeyAshleyPoint = "texturex.json";
        public const string ModelKeyGrappleGunPoint = "grapplegunarrow.json";
        public const string ModelKeyLocalTeleportationPoint = "localteleportationarrow.json";


        //o meu maior float
        public const float MyFloatMax = 1_000_000_000_000_000_000_000_000_000f;
    }
}
