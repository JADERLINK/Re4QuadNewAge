using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Re4QuadExtremeEditor.src.JSON;
using Re4QuadExtremeEditor.src.Class;
using Re4QuadExtremeEditor.src.Class.TreeNodeObj;
using Re4QuadExtremeEditor.src.Class.Files;
using Re4QuadExtremeEditor.src.Class.Enums;
using Re4QuadExtremeEditor.src.Class.Shaders;
using NewAgeTheRender;

namespace Re4QuadExtremeEditor.src
{
    /// <summary>
    /// Contem todo o conteudo da modelagem(3d), os conteudos dos arquivos AEV, ESL, ETS e ITA, e as definições dos objetos;
    /// </summary>
    public static class DataBase
    { 
        //representa a Room (cenario) selecionada, e a ser renderizada
        public static RoomSelectedObj SelectedRoom = null;
    
        // os grupos de objetos presentes no programa
        public static ModelGroupConteiner EnemiesModels; // modelos dos inimigos
        public static ModelGroupConteiner ItemsModels; // modelos dos itens
        public static ModelGroupConteiner EtcModels; // modelos da pasta "etcmodel"
        public static ModelGroupConteiner InternalModels; // modelos proprios para o programa
        public static ModelGroupConteiner QuadCustomModels; // QuadCustom File


        // Dicionarios com os ids dos objetos no jogo
        public static ObjectInfoList EnemiesIDs;
        public static ObjectInfoList ItemsIDs;
        public static ObjectInfoList EtcModelIDs;
        public static QuadCustomInfoList QuadCustomIDs;

        // aqui são os objetos que representa os arquivos no programa
        public static FileEnemyEslGroup FileESL;
        public static FileEtcModelEtsGroup FileETS;
        public static FileSpecialGroup FileITA;
        public static FileSpecialGroup FileAEV;
        public static ExtraGroup Extras;
        public static File_DSE_Group FileDSE;
        public static File_EMI_Group FileEMI;
        public static File_ESAR_Group FileSAR;
        public static File_ESAR_Group FileEAR;
        public static File_ESE_Group FileESE;
        public static File_FSE_Group FileFSE;
        public static File_LIT_Group FileLIT;
        public static FileQuadCustomGroup FileQuadCustom;

        //conteudo do treeview
        public static EnemyNodeGroup NodeESL;
        public static EtcModelNodeGroup NodeETS;
        public static SpecialNodeGroup NodeITA;
        public static SpecialNodeGroup NodeAEV;
        public static ExtraNodeGroup NodeEXTRAS;
        public static NewAge_DSE_NodeGroup NodeDSE;
        public static NewAge_EMI_NodeGroup NodeEMI;
        public static NewAge_ESAR_NodeGroup NodeSAR;
        public static NewAge_ESAR_NodeGroup NodeEAR;
        public static NewAge_ESE_NodeGroup NodeESE;
        public static NewAge_FSE_NodeGroup NodeFSE;
        public static QuadCustomNodeGroup NodeQuadCustom;
        public static NewAge_LIT_Groups_NodeGroup NodeLIT_Groups;
        public static NewAge_LIT_Entrys_NodeGroup NodeLIT_Entrys;

        // lista de objetos selecionados na treeview
        public static Dictionary<int, TreeNode> SelectedNodes;
        // o ultimo node/objeto selecionado
        public static TreeNode LastSelectNode = null;

        //dicionario com a lista de diretorio para os models usar
        //PathKey, diretorio
        public static Dictionary<string, string> DirectoryDic;

    }
}
