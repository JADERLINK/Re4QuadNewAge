using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Re4QuadExtremeEditor.src;
using Re4QuadExtremeEditor.src.Class;
using Re4QuadExtremeEditor.src.Class.TreeNodeObj;
using Re4QuadExtremeEditor.src.Forms;
using Re4QuadExtremeEditor.src.Class.Enums;
using Re4QuadExtremeEditor.src.Class.MyProperty;
using Re4QuadExtremeEditor.src.Class.MyProperty.CustomAttribute;
using Re4QuadExtremeEditor.src.Class.ObjMethods;
using Re4QuadExtremeEditor.src.Class.Files;
using Re4QuadExtremeEditor.src.Class.Shaders;
using Re4QuadExtremeEditor.src.Controls;
using NsCamera;
using System.IO;

namespace Re4QuadExtremeEditor
{
    public partial class MainForm : Form
    {
        GLControl glControl;
        readonly Timer myTimer = new Timer();

        CameraMoveControl cameraMove;
        ObjectMoveControl objectMove;
        Advertising1Control advertising1Control;
        Advertising2Control advertising2Control;

        #region Camera // variaveis para a camera
        Camera camera = new Camera();
        Matrix4 camMtx = Matrix4.Identity;
        Matrix4 ProjMatrix;
        // movimentação da camera
        bool isShiftDown = false, isControlDown = false, isSpaceDown = false;
        bool isMouseDown = false, isMouseMove = false;
        bool isWDown = false, isSDown = false, isADown = false, isDDown = false;
        //movimentação camera no glControl
        MouseButtons MouseButtonsLeft = MouseButtons.Left; //botão para movimentação camera
        MouseButtons MouseButtonsRight = MouseButtons.Right; // botão para selecionar objeto
        #endregion

        // Property que fica no PropertyGrid quando não tem nada selecionado;
        readonly NoneProperty none = new NoneProperty();

        // define se esta com o PropertyGrid selecionado;
        bool InPropertyGrid = false;

        UpdateMethods updateMethods;

        public MainForm()
        {
            SplashScreen.StartSplashScreen();

            InitializeComponent();
            propertyGridObjs.SelectedItemWithFocusBackColor = Color.FromArgb(0x70, 0xBB, 0xDB);
            propertyGridObjs.SelectedItemWithFocusForeColor = Color.Black;
            treeViewObjs.SelectedNodeBackColor = Color.FromArgb(0x70, 0xBB, 0xDB);
            treeViewObjs.Font = Globals.TreeNodeFontText;

            propertyGridObjs.SelectedObject = none;
            DataBase.SelectedNodes = treeViewObjs.SelectedNodes; // vinculo de referencia entra as listas

            glControl = new OpenTK.GLControl();
            glControl.Dock = DockStyle.Fill;
            glControl.Name = "glControl";
            glControl.TabIndex = 999;
            glControl.TabStop = false;
            glControl.Paint += GlControl_Paint;
            glControl.Load += GlControl_Load;
            glControl.KeyDown += GlControl_KeyDown;
            glControl.KeyUp += GlControl_KeyUp;
            glControl.Leave += GlControl_Leave;
            glControl.MouseWheel += GlControl_MouseWheel;
            glControl.MouseMove += GlControl_MouseMove;
            glControl.MouseDown += GlControl_MouseDown;
            glControl.MouseUp += GlControl_MouseUp;
            glControl.MouseLeave += GlControl_MouseLeave;
            glControl.Resize += GlControl_Resize;
            splitContainerRight.Panel1.Controls.Add(glControl);

            camera.getSelectedObject = getSelectedObject;

            cameraMove = new CameraMoveControl(ref camera, UpdateGL, UpdateCameraMatrix);
            cameraMove.Location = new Point(splitContainerRight.Panel2.Width - cameraMove.Width, 0);
            cameraMove.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            cameraMove.Name = "cameraMove";
            cameraMove.TabIndex = 998;
            cameraMove.TabStop = false;
           
            objectMove = new ObjectMoveControl(ref camera, UpdateGL, UpdateCameraMatrix, UpdatePropertyGrid, UpdateTreeViewObjs);
            objectMove.Location = new Point(0, 0);
            objectMove.Anchor = AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
            objectMove.Name = "objectMove";
            objectMove.TabIndex = 995;
            objectMove.TabStop = false;
           
            advertising1Control = new Advertising1Control();
            advertising1Control.Location = new Point(0, 0);
            advertising1Control.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            advertising1Control.Name = "advertising1Control";
            advertising1Control.TabIndex = 997;
            advertising1Control.TabStop = false;
            advertising1Control.Hide();

            advertising2Control = new Advertising2Control();
            advertising2Control.Location = new Point(0, 0);
            advertising2Control.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            advertising2Control.Name = "advertising2Control";
            advertising2Control.TabIndex = 996;
            advertising2Control.TabStop = false;
            advertising2Control.Hide();

            splitContainerRight.Panel2.Controls.Add(cameraMove);
            splitContainerRight.Panel2.Controls.Add(advertising1Control);
            splitContainerRight.Panel2.Controls.Add(advertising2Control);
            splitContainerRight.Panel2.Controls.Add(objectMove);
            enable_splitContainerRight_Panel2_Resize = true;

            KeyPreview = true;

            myTimer.Tick += updateWASDControls;
            myTimer.Interval = 10;
            myTimer.Enabled = false;

            camMtx = camera.GetViewMatrix();
            ProjMatrix = ReturnNewProjMatrix();

            // todos os metodos listados abaixos, tem que seguir a sequencia abaixo, se não dara erro.

            Lang.StartAttributeTexts();
            Lang.StartTexts();

            src.JSON.Configs.StartLoadConfigs();
            Utils.StartReloadDirectoryDic();
            Utils.StartLoadObjsInfoLists();
            Utils.StartLoadPromptMessageList();
            Utils.StartLoadLangFile();
            Utils.StartEnemyExtraSegmentList();
            Utils.StartSetListBoxsProperty();
            Utils.StartSetListBoxsPropertybjsInfoLists();
            if (Lang.LoadedTranslation) 
            { 
                StartUpdateTranslation();
                cameraMove.StartUpdateTranslation();
                objectMove.StartUpdateTranslation();
            }

            Utils.StartCreateNodes();
            Utils.StartExtraGroup();
            treeViewObjs.Nodes.Add(DataBase.NodeESL);
            treeViewObjs.Nodes.Add(DataBase.NodeETS);
            treeViewObjs.Nodes.Add(DataBase.NodeITA);
            treeViewObjs.Nodes.Add(DataBase.NodeAEV);
            treeViewObjs.Nodes.Add(DataBase.NodeEXTRAS);
            treeViewObjs.Nodes.Add(DataBase.NodeDSE);
            treeViewObjs.Nodes.Add(DataBase.NodeFSE);
            treeViewObjs.Nodes.Add(DataBase.NodeEAR);
            treeViewObjs.Nodes.Add(DataBase.NodeSAR);
            treeViewObjs.Nodes.Add(DataBase.NodeEMI);
            treeViewObjs.Nodes.Add(DataBase.NodeESE);
            treeViewObjs.Nodes.Add(DataBase.NodeQuadCustom);

            updateMethods = new UpdateMethods();
            updateMethods.UpdateGL = UpdateGL;
            updateMethods.UpdatePropertyGrid = UpdatePropertyGrid;
            updateMethods.UpdateTreeViewObjs = UpdateTreeViewObjs;
            updateMethods.UpdateMoveObjSelection = objectMove.UpdateSelection;
            updateMethods.UpdateOrbitCamera = UpdateOrbitCamera;

            if (Globals.BackupConfigs.UseDarkerGrayTheme)
            {
                DarkerGrayTheme();
            }

            if (Globals.BackupConfigs.UseInvertedMouseButtons)
            {
                MouseButtonsLeft = MouseButtons.Right; //botão para movimentação camera
                MouseButtonsRight = MouseButtons.Left; // botão para selecionar objeto
            }

            //apenas para testes, cria um arquivo para tradução
            //src.JSON.LangFile.WriteToLangFile("SourceLang.json");
            //int finish = 0;

        }

        #region GlControl Events

        private Matrix4 ReturnNewProjMatrix() 
        {
            return Matrix4.CreatePerspectiveFieldOfView(Globals.FOV * ((float)Math.PI / 180.0f), (float)glControl.Width / (float)glControl.Height, 0.01f, 1_000_000f);
        }

        private void GlControl_Resize(object sender, EventArgs e)
        {            
            glControl.Context.Update(glControl.WindowInfo);
            GL.Viewport(0, 0, glControl.Width, glControl.Height);
            ProjMatrix = ReturnNewProjMatrix();
            glControl.Invalidate(); 
        }

        private void splitContainerMain_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            glControl.Invalidate();
        }

        private void GlControl_MouseLeave(object sender, EventArgs e)
        {
            camera.resetMouseStuff();
            isMouseDown = false;
            isMouseMove = false;
        }

        private void GlControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtonsLeft)
            {
                camera.resetMouseStuff();
                isMouseDown = false;
                isMouseMove = false;
                camera.SaveCameraPosition();
                if (!isWDown && !isSDown && !isADown && !isDDown && !isMouseMove && !isShiftDown && !isSpaceDown)
                {
                    myTimer.Enabled = false;
                }
            }    
        }

        private void GlControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtonsLeft)
            {
                camera.resetMouseStuff();
                isMouseDown = true;
                isMouseMove = true;
                camera.SaveCameraPosition();
                myTimer.Enabled = true;
            }       
            if (e.Button == MouseButtonsRight)
            {
                selectObject(e.X, e.Y);
                glControl.Invalidate();
            }
        }

        /// <summary>
        /// metodo destinado para a seleção dos objetos no ambiente GL
        /// </summary>
        private void selectObject(int mx, int my)
        {
            NewAgeTheRender.TheRender.AllRender(ref camMtx, ref ProjMatrix, camera.Position, camera.SelectedObjPosY(), true); // renderiza o ambiente GL no modo seleção.

            int h = glControl.Height;
            byte[] pixel = new byte[4];
            GL.ReadPixels(mx, h - my, 1, 1, PixelFormat.Rgba, PixelType.UnsignedByte, pixel);

            //Console.WriteLine("pixel[0]: " + pixel[0]); // lineID
            //Console.WriteLine("pixel[1]: " + pixel[1]); // lineID
            //Console.WriteLine("pixel[2]: " + pixel[2]); // id da lista
            //Console.WriteLine("pixel[3]: " + pixel[3]);

            // listas
            // aviso: proibido usar os valores 0 e 255, pois fazem parte das cores preta (renderização do cenario) e da cor branca (fundo);
            if (pixel[2] > 0 && pixel[2] < 255)
            {
                ushort LineID = BitConverter.ToUInt16(pixel, 0);

                TreeNode selected = null;
                switch (pixel[2])
                {
                    case (byte)GroupType.ESL:
                        int index1 = DataBase.NodeESL.Nodes.IndexOfKey(LineID.ToString());
                        if (index1 > -1)
                        {
                            selected = DataBase.NodeESL.Nodes[index1];
                        }
                        break;
                    case (byte)GroupType.ETS:
                        int index2 = DataBase.NodeETS.Nodes.IndexOfKey(LineID.ToString());
                        if (index2 > -1)
                        {
                            selected = DataBase.NodeETS.Nodes[index2];
                        }
                        break;
                    case (byte)GroupType.ITA:
                        int index3 = DataBase.NodeITA.Nodes.IndexOfKey(LineID.ToString());
                        if (index3 > -1)
                        {
                            selected = DataBase.NodeITA.Nodes[index3];
                        }
                        break;
                    case (byte)GroupType.AEV:
                        int index4 = DataBase.NodeAEV.Nodes.IndexOfKey(LineID.ToString());
                        if (index4 > -1)
                        {
                            selected = DataBase.NodeAEV.Nodes[index4];
                        }
                        break;
                    case (byte)GroupType.EXTRAS:
                        int index5 = DataBase.NodeEXTRAS.Nodes.IndexOfKey(LineID.ToString());
                        if (index5 > -1)
                        {
                            selected = DataBase.NodeEXTRAS.Nodes[index5];
                        }
                        break;
                    case (byte)GroupType.EAR:
                        int index6 = DataBase.NodeEAR.Nodes.IndexOfKey(LineID.ToString());
                        if (index6 > -1)
                        {
                            selected = DataBase.NodeEAR.Nodes[index6];
                        }
                        break;
                    case (byte)GroupType.SAR:
                        int index7 = DataBase.NodeSAR.Nodes.IndexOfKey(LineID.ToString());
                        if (index7 > -1)
                        {
                            selected = DataBase.NodeSAR.Nodes[index7];
                        }
                        break;
                    case (byte)GroupType.EMI:
                        int index8 = DataBase.NodeEMI.Nodes.IndexOfKey(LineID.ToString());
                        if (index8 > -1)
                        {
                            selected = DataBase.NodeEMI.Nodes[index8];
                        }
                        break;
                    case (byte)GroupType.ESE:
                        int index9 = DataBase.NodeESE.Nodes.IndexOfKey(LineID.ToString());
                        if (index9 > -1)
                        {
                            selected = DataBase.NodeESE.Nodes[index9];
                        }
                        break;
                    case (byte)GroupType.FSE:
                        int index10 = DataBase.NodeFSE.Nodes.IndexOfKey(LineID.ToString());
                        if (index10 > -1)
                        {
                            selected = DataBase.NodeFSE.Nodes[index10];
                        }
                        break;
                    case (byte)GroupType.QUAD_CUSTOM:
                        int index11 = DataBase.NodeQuadCustom.Nodes.IndexOfKey(LineID.ToString());
                        if (index11 > -1)
                        {
                            selected = DataBase.NodeQuadCustom.Nodes[index11];
                        }
                        break;
                }

                if (selected != null)
                {
                    if (isControlDown) // add ou remove da seleção
                    {
                        treeViewObjs.ToSelectMultiNode(selected);
                    }
                    else // seleciona so esse
                    {
                        treeViewObjs.ToSelectSingleNode(selected);
                    }

                }
            }
        }

        private void GlControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown && e.Button == MouseButtonsLeft)
            {
                camera.updateCameraOffsetMatrixWithMouse(isControlDown, e.X, e.Y);
                camMtx = camera.GetViewMatrix();
            }
        }

        private void GlControl_MouseWheel(object sender, MouseEventArgs e)
        {
            camera.resetMouseStuff();
            camera.updateCameraMatrixWithScrollWheel((int)(e.Delta * 0.5f));
            camMtx = camera.GetViewMatrix();
            camera.SaveCameraPosition();
            glControl.Invalidate();
        }

        private void GlControl_Leave(object sender, EventArgs e)
        {
            isWDown = false;
            isSDown = false;
            isADown = false;
            isDDown = false;
            isSpaceDown = false;
            isShiftDown = false;
            isControlDown = false;
            isMouseDown = false;
            isMouseMove = false;
            myTimer.Enabled = false;
        }

        private void GlControl_KeyUp(object sender, KeyEventArgs e)
        {
            isShiftDown = e.Shift;
            isControlDown = e.Control;
            switch (e.KeyCode)
            {
                case Keys.W: isWDown = false; break;
                case Keys.S: isSDown = false; break;
                case Keys.A: isADown = false; break;
                case Keys.D: isDDown = false; break;
                case Keys.Space: isSpaceDown = false; break;
            }
            if (!isWDown && !isSDown && !isADown && !isDDown && !isMouseMove && !isShiftDown && !isSpaceDown)
            {
                myTimer.Enabled = false;
            }
            if (isControlDown)
            {
                camera.SaveCameraPosition();
                camera.resetMouseStuff();
            }
        }

        private void GlControl_KeyDown(object sender, KeyEventArgs e)
        {
            isShiftDown = e.Shift;
            isControlDown = e.Control;
            switch (e.KeyCode)
            {
                case Keys.W:
                    isWDown = true;
                    myTimer.Enabled = true;
                    break;
                case Keys.S:
                    isSDown = true;
                    myTimer.Enabled = true;
                    break;
                case Keys.A:
                    isADown = true;
                    myTimer.Enabled = true;
                    break;
                case Keys.D:
                    isDDown = true;
                    myTimer.Enabled = true;
                    break;
                case Keys.Space:
                    isSpaceDown = true;
                    myTimer.Enabled = true;
                    break;
            }
            if (isShiftDown)
            {
                myTimer.Enabled = true;
            }
            if (isControlDown)
            {
                camera.SaveCameraPosition();
                camera.resetMouseStuff();
            }

        }

        /// <summary>
        /// Atualiza a movimentação de wasd, e cria os "frames" da renderização.
        /// </summary>
        private void updateWASDControls(object sender, EventArgs e)
        {
            if (!isControlDown && camera.CamMode == Camera.CameraMode.FLY)
            {
                if (isWDown)
                {
                    camera.updateCameraToFront();
                }
                if (isSDown)
                {
                    camera.updateCameraToBack();
                }
                if (isDDown)
                {
                    camera.updateCameraToRight();
                }
                if (isADown)
                {
                    camera.updateCameraToLeft();
                }

                if (isShiftDown)
                {
                    camera.updateCameraToDown();
                }

                if (isSpaceDown)
                {
                    camera.updateCameraToUp();
                }

                if (isWDown || isSDown || isDDown || isADown || isShiftDown || isSpaceDown || isMouseMove)
                {
                    camMtx = camera.GetViewMatrix();
                    glControl.Invalidate();
                }

            }
            else 
            {
                glControl.Invalidate();
            }
        }

        private bool theAppLoadedWell = true; //o app carregou corretamente, sem erro na versão do openGL 

        private void GlControl_Load(object sender, EventArgs e)
        {
            try
            {
                Globals.OpenGLVersion = GL.GetString(StringName.Version)?.Trim() ?? "";

                if (Globals.OpenGLVersion.StartsWith("1.")
                    || Globals.OpenGLVersion.StartsWith("2.")
                    || Globals.OpenGLVersion.StartsWith("3.0")
                    || Globals.OpenGLVersion.StartsWith("3.1")
                    || Globals.OpenGLVersion.StartsWith("3.2")
                    )
                {
                    SplashScreen.Conteiner?.Close?.Invoke();
                    this.TopMost = true;
                    MessageBox.Show(
                        "Error: You have an outdated version of OpenGL, which is not supported by this program." +
                        " The program will now exit.\n\n" +
                        "OpenGL version: [" + Globals.OpenGLVersion + "]\n",
                        "OpenGL version error:",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    theAppLoadedWell = false;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                SplashScreen.Conteiner?.Close?.Invoke();
                this.TopMost = true;
                MessageBox.Show(
                      "Error: " +
                      ex.Message,
                      "Error detecting OpenGL version:",
                      MessageBoxButtons.OK,
                     MessageBoxIcon.Error);
                theAppLoadedWell = false;
                this.Close();
            }

            if (theAppLoadedWell)
            {
                GL.Viewport(0, 0, glControl.Width, glControl.Height);
                GL.ClearColor(Globals.SkyColor);

                GL.Enable(EnableCap.DepthTest);
                GL.Disable(EnableCap.Texture2D);
                GL.LineWidth(1.5f);

                DataShader.StartLoad();
                Utils.StartLoadObjsModels();

                glControl.SwapBuffers();
            }

            SplashScreen.Conteiner?.Close?.Invoke();
            // faz a janela ficar no topo
            this.TopMost = true;
            this.TopMost = false;
        }

      

        private void GlControl_Paint(object sender, PaintEventArgs e)
        {
            if (RenderSelectViewer)
            {
                NewAgeTheRender.TheRender.AllRender(ref camMtx, ref ProjMatrix, camera.Position, camera.SelectedObjPosY(), true); // este é da seleção
            }
            else
            {
                NewAgeTheRender.TheRender.AllRender(ref camMtx, ref ProjMatrix, camera.Position, camera.SelectedObjPosY()); // rederiza todos os objetos do GL;
            }
            glControl.SwapBuffers();
        }

        bool RenderSelectViewer = false;
        private void toolStripMenuItemRenderSelectViewer_Click(object sender, EventArgs e)
        {
            RenderSelectViewer = !RenderSelectViewer;
            glControl.Invalidate();
        }

        #endregion


        #region botões do menu edit

        private void toolStripMenuItemAddNewObj_Click(object sender, EventArgs e)
        {
            AddNewObjForm form = new AddNewObjForm();
            form.OnButtonOk_Click += OnButtonOk_Click;
            form.TreeViewDisableDrawNode += TreeViewDisableDrawNode;
            form.TreeViewEnableDrawNode += TreeViewEnableDrawNode;
            form.ShowDialog();
        }

        private void OnButtonOk_Click()
        {
            glControl.Invalidate();
        }

        private void toolStripMenuItemDeleteSelectedObj_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Lang.GetText(eLang.DeleteObjDialog), Lang.GetText(eLang.DeleteObjWarning), MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                foreach (Object3D item in treeViewObjs.SelectedNodes.Values)
                {
                    if (item.Group == GroupType.ETS
                        || item.Group == GroupType.DSE
                        || item.Group == GroupType.FSE
                        || item.Group == GroupType.SAR
                        || item.Group == GroupType.EAR
                        || item.Group == GroupType.ESE
                        || item.Group == GroupType.EMI
                        || item.Group == GroupType.QUAD_CUSTOM
                        )
                    {
                        ((src.Class.Interfaces.INodeChangeAmount)item.Parent).ChangeAmountMethods.RemoveLineID(item.ObjLineRef);
                        item.Remove();
                    }
                    else if (item.Group == GroupType.ITA || item.Group == GroupType.AEV)
                    {
                        DataBase.Extras.RemoveObj(item.ObjLineRef, Utils.GroupTypeToSpecialFileFormat(item.Group));
                        ((SpecialNodeGroup)item.Parent).ChangeAmountMethods.RemoveLineID(item.ObjLineRef);
                        item.Remove();
                    }
                }
                TreeViewUpdateSelectedsClear();
                glControl.Invalidate();
            }
        }

        private void toolStripMenuItemMoveUp_Click(object sender, EventArgs e)
        {
            var ordernedSelectedNodes = treeViewObjs.SelectedNodes.Values.OrderBy(n => n.Index);
            foreach (Object3D item in ordernedSelectedNodes)
            {
                if (item.Group == GroupType.ETS
                    || item.Group == GroupType.ITA
                    || item.Group == GroupType.AEV
                    || item.Group == GroupType.DSE
                    || item.Group == GroupType.FSE
                    || item.Group == GroupType.SAR
                    || item.Group == GroupType.EAR
                    || item.Group == GroupType.ESE
                    || item.Group == GroupType.EMI
                    || item.Group == GroupType.QUAD_CUSTOM
                    )
                {
                    int index = item.Index;
                    if (index > 0)
                    {
                        var Parent = item.Parent;
                        item.Remove();
                        Parent.Nodes.Insert(index -1, item);
                    }
                }
            }
        }

        private void toolStripMenuItemMoveDown_Click(object sender, EventArgs e)
        {
            var invSelectedNodes = treeViewObjs.SelectedNodes.Values.OrderByDescending(n => n.Index);
            foreach (Object3D item in invSelectedNodes)
            {
                if (item.Group == GroupType.ETS
                    || item.Group == GroupType.ITA
                    || item.Group == GroupType.AEV
                    || item.Group == GroupType.DSE
                    || item.Group == GroupType.FSE
                    || item.Group == GroupType.SAR
                    || item.Group == GroupType.EAR
                    || item.Group == GroupType.ESE
                    || item.Group == GroupType.EMI
                    || item.Group == GroupType.QUAD_CUSTOM
                    )
                {
                    int index = item.Index;
                    var Parent = item.Parent;
                    if (index < Parent.GetNodeCount(false) -1)
                    {
                        item.Remove();
                        Parent.Nodes.Insert(index +1, item);
                    }
                }
            }
        }


        private void toolStripMenuItemSearch_Click(object sender, EventArgs e)
        {
            var selectedObj = propertyGridObjs.SelectedObject;
            if (selectedObj is EnemyProperty enemy)
            {
                SearchForm search = new SearchForm(ListBoxProperty.EnemiesList.Values.ToArray(), new UshortObjForListBox(enemy.ReturnUshortFirstSearchSelect(), ""));
                search.Search += enemy.Searched;
                search.ShowDialog();
            }
            else if (selectedObj is EtcModelProperty etcModel)
            {
                SearchForm search = new SearchForm(ListBoxProperty.EtcmodelsList.Values.ToArray(), new UshortObjForListBox(etcModel.ReturnUshortFirstSearchSelect(), ""));
                search.Search += etcModel.Searched;
                search.ShowDialog();
            }
            else if (selectedObj is SpecialProperty special)
            {
                var specialType = special.GetSpecialType();
                if (specialType == SpecialType.T03_Items || specialType == SpecialType.T11_ItemDependentEvents)
                {
                    SearchForm search = new SearchForm(ListBoxProperty.ItemsList.Values.ToArray(), new UshortObjForListBox(special.ReturnUshortFirstSearchSelect(), ""));
                    search.Search += special.Searched;
                    search.ShowDialog();
                }
            }
            else if (selectedObj is QuadCustomProperty quad)
            {
                SearchForm search = new SearchForm(ListBoxProperty.QuadCustomModelIDList.Values.ToArray(), new UintObjForListBox(quad.ReturnUshortFirstSearchSelect(), ""));
                search.Search += quad.Searched;
                search.ShowDialog();
            }

        }


        #endregion


        #region Botoes do menu

        private void SelectRoom_onLoadButtonClick(object sender, EventArgs e)
        {
            if (sender is string == false)
            {
                string text = Lang.GetText(eLang.SelectedRoom) + ": " + sender.ToString();
                if (text.Length > 100)
                {
                    text = text.Substring(0,100);
                    text += "...";
                }
                toolStripMenuItemSelectRoom.Text = text;
            }
            else
            {
                toolStripMenuItemSelectRoom.Text = Lang.GetText(eLang.SelectRoom);
            }

            if (Globals.AutoDefinedRoom)
            {
                if (DataBase.SelectedRoom != null)
                {
                    toolStripTextBoxDefinedRoom.Text = DataBase.SelectedRoom.GetRoomId().ToString("X4");
                }
                else
                {
                    toolStripTextBoxDefinedRoom.Text = "0000";
                }
            }
        }

        private void toolStripMenuItemSelectRoom_Click(object sender, EventArgs e)
        {
            SelectRoomForm selectRoom = new SelectRoomForm();
            selectRoom.onLoadButtonClick += SelectRoom_onLoadButtonClick;
            selectRoom.ShowDialog();
            glControl.Invalidate();
        }

        private void toolStripMenuItemClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItemCredits_Click(object sender, EventArgs e)
        {
            CreditsForm form = new CreditsForm();
            form.ShowDialog();
        }

        private void toolStripMenuItemOptions_Click(object sender, EventArgs e)
        {
            OptionsForm form = new OptionsForm();
            form.OnOKButtonClick += OptionsForm_OnOKButtonClick;
            form.OnOKButtonClick += UpdateTreeViewObjs;
            form.OnOKButtonClick += UpdatePropertyGrid;
            form.ShowDialog();
            glControl.Invalidate();
        }

        private void OptionsForm_OnOKButtonClick()
        {
            TreeViewUpdateSelectedsClear();
        }

        private void toolStripMenuItemCameraMenu_Click(object sender, EventArgs e)
        {
            CameraForm cameraForm = new CameraForm(ref camera, UpdateGL, UpdateCameraMatrix);
            cameraForm.ShowDialog();
        }

        #endregion


        #region botoes do menu view

        private void toolStripMenuItemHideRoomModel_Click(object sender, EventArgs e)
        {
            toolStripMenuItemHideRoomModel.Checked = !toolStripMenuItemHideRoomModel.Checked;
            Globals.RenderRoom = !toolStripMenuItemHideRoomModel.Checked;
            glControl.Invalidate();
        }

        private void toolStripMenuItemHideEnemyESL_Click(object sender, EventArgs e)
        {
            toolStripMenuItemHideEnemyESL.Checked = !toolStripMenuItemHideEnemyESL.Checked;
            Globals.RenderEnemyESL = !toolStripMenuItemHideEnemyESL.Checked;
            treeViewObjs.Refresh();
            glControl.Invalidate();
        }

        private void toolStripMenuItemHideEtcmodelETS_Click(object sender, EventArgs e)
        {
            toolStripMenuItemHideEtcmodelETS.Checked = !toolStripMenuItemHideEtcmodelETS.Checked;
            Globals.RenderEtcmodelETS = !toolStripMenuItemHideEtcmodelETS.Checked;
            treeViewObjs.Refresh();
            glControl.Invalidate();
        }

        private void toolStripMenuItemHideItemsITA_Click(object sender, EventArgs e)
        {
            toolStripMenuItemHideItemsITA.Checked = !toolStripMenuItemHideItemsITA.Checked;
            Globals.RenderItemsITA = !toolStripMenuItemHideItemsITA.Checked;
            treeViewObjs.Refresh();
            glControl.Invalidate();
        }

        private void toolStripMenuItemHideEventsAEV_Click(object sender, EventArgs e)
        {
            toolStripMenuItemHideEventsAEV.Checked = !toolStripMenuItemHideEventsAEV.Checked;
            Globals.RenderEventsAEV = !toolStripMenuItemHideEventsAEV.Checked;
            treeViewObjs.Refresh();
            glControl.Invalidate();
        }


        private void toolStripMenuItemHideFileFSE_Click(object sender, EventArgs e)
        {
            toolStripMenuItemHideFileFSE.Checked = !toolStripMenuItemHideFileFSE.Checked;
            Globals.RenderFileFSE = !toolStripMenuItemHideFileFSE.Checked;
            treeViewObjs.Refresh();
            glControl.Invalidate();
        }

        private void toolStripMenuItemHideFileSAR_Click(object sender, EventArgs e)
        {
            toolStripMenuItemHideFileSAR.Checked = !toolStripMenuItemHideFileSAR.Checked;
            Globals.RenderFileSAR = !toolStripMenuItemHideFileSAR.Checked;
            treeViewObjs.Refresh();
            glControl.Invalidate();
        }

        private void toolStripMenuItemHideFileEAR_Click(object sender, EventArgs e)
        {
            toolStripMenuItemHideFileEAR.Checked = !toolStripMenuItemHideFileEAR.Checked;
            Globals.RenderFileEAR = !toolStripMenuItemHideFileEAR.Checked;
            treeViewObjs.Refresh();
            glControl.Invalidate();
        }

        private void toolStripMenuItemHideFileESE_Click(object sender, EventArgs e)
        {
            toolStripMenuItemHideFileESE.Checked = !toolStripMenuItemHideFileESE.Checked;
            Globals.RenderFileESE = !toolStripMenuItemHideFileESE.Checked;
            treeViewObjs.Refresh();
            glControl.Invalidate();
        }

        private void toolStripMenuItemHideFileEMI_Click(object sender, EventArgs e)
        {
            toolStripMenuItemHideFileEMI.Checked = !toolStripMenuItemHideFileEMI.Checked;
            Globals.RenderFileEMI = !toolStripMenuItemHideFileEMI.Checked;
            treeViewObjs.Refresh();
            glControl.Invalidate();
        }

        private void toolStripMenuItemHideQuadCustom_Click(object sender, EventArgs e)
        {
            toolStripMenuItemHideQuadCustom.Checked = !toolStripMenuItemHideQuadCustom.Checked;
            Globals.RenderFileQuadCustom = !toolStripMenuItemHideQuadCustom.Checked;
            treeViewObjs.Refresh();
            glControl.Invalidate();
        }


        private void toolStripMenuItemHideDesabledEnemy_Click(object sender, EventArgs e)
        {
            toolStripMenuItemHideDesabledEnemy.Checked = !toolStripMenuItemHideDesabledEnemy.Checked;
            Globals.RenderDisabledEnemy = !toolStripMenuItemHideDesabledEnemy.Checked;
            treeViewObjs.Refresh();
            glControl.Invalidate();
        }

        private void toolStripTextBoxDefinedRoom_TextChanged(object sender, EventArgs e)
        {
            Globals.RenderEnemyFromDefinedRoom = ushort.Parse(toolStripTextBoxDefinedRoom.Text, System.Globalization.NumberStyles.HexNumber);
            treeViewObjs.Refresh();
            glControl.Invalidate();
        }

        private void toolStripTextBoxDefinedRoom_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) 
                || e.KeyChar == 'A'
                || e.KeyChar == 'B'
                || e.KeyChar == 'C'
                || e.KeyChar == 'D'
                || e.KeyChar == 'E'
                || e.KeyChar == 'F'
                || e.KeyChar == 'a'
                || e.KeyChar == 'b'
                || e.KeyChar == 'c'
                || e.KeyChar == 'd'
                || e.KeyChar == 'e'
                || e.KeyChar == 'f'
                )
            {
                if (toolStripTextBoxDefinedRoom.SelectionStart < toolStripTextBoxDefinedRoom.TextLength)
                {
                    int CacheSelectionStart = toolStripTextBoxDefinedRoom.SelectionStart;
                    StringBuilder sb = new StringBuilder(toolStripTextBoxDefinedRoom.Text);
                    sb[toolStripTextBoxDefinedRoom.SelectionStart] = e.KeyChar;
                    toolStripTextBoxDefinedRoom.Text = sb.ToString();
                    toolStripTextBoxDefinedRoom.SelectionStart = CacheSelectionStart + 1;
                }
            }
            e.Handled = true;
        }


        private void toolStripMenuItemShowOnlyDefinedRoom_Click(object sender, EventArgs e)
        {
            toolStripMenuItemShowOnlyDefinedRoom.Checked = !toolStripMenuItemShowOnlyDefinedRoom.Checked;
            Globals.RenderDontShowOnlyDefinedRoom = !toolStripMenuItemShowOnlyDefinedRoom.Checked;
            treeViewObjs.Refresh();
            glControl.Invalidate();
        }

        private void toolStripMenuItemAutoDefineRoom_Click(object sender, EventArgs e)
        {
            toolStripMenuItemAutoDefineRoom.Checked = !toolStripMenuItemAutoDefineRoom.Checked;
            Globals.AutoDefinedRoom = toolStripMenuItemAutoDefineRoom.Checked;
        }

        private void toolStripMenuItemHideItemTriggerZone_Click(object sender, EventArgs e)
        {
            toolStripMenuItemHideItemTriggerZone.Checked = !toolStripMenuItemHideItemTriggerZone.Checked;
            Globals.RenderItemTriggerZone = !toolStripMenuItemHideItemTriggerZone.Checked;
            treeViewObjs.Refresh();
            glControl.Invalidate();
        }

        private void toolStripMenuItemHideItemTriggerRadius_Click(object sender, EventArgs e)
        {
            toolStripMenuItemHideItemTriggerRadius.Checked = !toolStripMenuItemHideItemTriggerRadius.Checked;
            Globals.RenderItemTriggerRadius = !toolStripMenuItemHideItemTriggerRadius.Checked;
            treeViewObjs.Refresh();
            glControl.Invalidate();
        }


        private void toolStripMenuItemItemPositionAtAssociatedObjectLocation_Click(object sender, EventArgs e)
        {
            toolStripMenuItemItemPositionAtAssociatedObjectLocation.Checked = !toolStripMenuItemItemPositionAtAssociatedObjectLocation.Checked;
            Globals.RenderItemPositionAtAssociatedObjectLocation = toolStripMenuItemItemPositionAtAssociatedObjectLocation.Checked;
            treeViewObjs.Refresh();
            glControl.Invalidate();
        }

        private void toolStripMenuItemHideExtraObjs_Click(object sender, EventArgs e)
        {
            toolStripMenuItemHideExtraObjs.Checked = !toolStripMenuItemHideExtraObjs.Checked;
            Globals.RenderExtraObjs = !toolStripMenuItemHideExtraObjs.Checked;
            treeViewObjs.Refresh();
            glControl.Invalidate();
        }

        private void toolStripMenuItemHideSpecialTriggerZone_Click(object sender, EventArgs e)
        {
            toolStripMenuItemHideSpecialTriggerZone.Checked = !toolStripMenuItemHideSpecialTriggerZone.Checked;
            Globals.RenderSpecialTriggerZone = !toolStripMenuItemHideSpecialTriggerZone.Checked;
            treeViewObjs.Refresh();
            glControl.Invalidate();
        }

        private void toolStripMenuItemUseMoreSpecialColors_Click(object sender, EventArgs e)
        {
            toolStripMenuItemUseMoreSpecialColors.Checked = !toolStripMenuItemUseMoreSpecialColors.Checked;
            Globals.UseMoreSpecialColors = toolStripMenuItemUseMoreSpecialColors.Checked;
            treeViewObjs.Refresh();
            glControl.Invalidate();
        }

        private void toolStripMenuItemUseCustomColors_Click(object sender, EventArgs e)
        {
            toolStripMenuItemUseCustomColors.Checked = !toolStripMenuItemUseCustomColors.Checked;
            Globals.UseMoreQuadCustomColors = toolStripMenuItemUseCustomColors.Checked;
            treeViewObjs.Refresh();
            glControl.Invalidate();
        }


        private void toolStripMenuItemEtcModelUseScale_Click(object sender, EventArgs e)
        {
            toolStripMenuItemEtcModelUseScale.Checked = !toolStripMenuItemEtcModelUseScale.Checked;
            Globals.RenderEtcmodelUsingScale = toolStripMenuItemEtcModelUseScale.Checked;
            treeViewObjs.Refresh();
            glControl.Invalidate();
        }

        private void toolStripMenuItemHideExtraExceptWarpDoor_Click(object sender, EventArgs e)
        {
            toolStripMenuItemHideExtraExceptWarpDoor.Checked = !toolStripMenuItemHideExtraExceptWarpDoor.Checked;
            Globals.HideExtraExceptWarpDoor = toolStripMenuItemHideExtraExceptWarpDoor.Checked;
            treeViewObjs.Refresh();
            glControl.Invalidate();
        }

        private void toolStripMenuItemHideOnlyWarpDoor_Click(object sender, EventArgs e)
        {
            toolStripMenuItemHideOnlyWarpDoor.Checked = !toolStripMenuItemHideOnlyWarpDoor.Checked;
            Globals.RenderExtraWarpDoor = !toolStripMenuItemHideOnlyWarpDoor.Checked;
            treeViewObjs.Refresh();
            glControl.Invalidate();
        }

        private void toolStripMenuItemNodeDisplayNameInHex_Click(object sender, EventArgs e)
        {
            toolStripMenuItemNodeDisplayNameInHex.Checked = !toolStripMenuItemNodeDisplayNameInHex.Checked;
            Globals.TreeNodeRenderHexValues = toolStripMenuItemNodeDisplayNameInHex.Checked;
            TreeViewDisableDrawNode();
            if (Globals.TreeNodeRenderHexValues)
            {
                treeViewObjs.Font = Globals.TreeNodeFontHex;
            }
            else 
            {
                treeViewObjs.Font = Globals.TreeNodeFontText;
            }
            TreeViewEnableDrawNode();
            treeViewObjs.Refresh();
        }

        private void toolStripMenuItemRefresh_Click(object sender, EventArgs e)
        {
            glControl.Invalidate();
            treeViewObjs.Refresh();
            propertyGridObjs.Refresh();
            glControl.Update(); // Needed after calling propertyGridObjs.Refresh();
        }

        private void toolStripMenuItemResetCamera_Click(object sender, EventArgs e)
        {
            cameraMove.ResetCamera();
        }


        private void toolStripMenuItemHideSideMenu_Click(object sender, EventArgs e)
        {
            if (toolStripMenuItemHideLateralMenu.Checked) // fazer reaparecer
            {
                splitContainerMain.Panel1.Enabled = true;
                splitContainerMain.Panel1Collapsed = false;

                toolStripMenuItemHideLateralMenu.Checked = false;
            }
            else //fazer esconder
            {
                splitContainerMain.Panel1Collapsed = true;
                splitContainerMain.Panel1.Enabled = false;

                toolStripMenuItemHideLateralMenu.Checked = true;
            }
            glControl.Invalidate();
        }

        private void toolStripMenuItemHideBottomMenu_Click(object sender, EventArgs e)
        {
            if (toolStripMenuItemHideBottomMenu.Checked) // fazer reaparecer
            {
                splitContainerRight.Panel2.Enabled = true;
                splitContainerRight.Panel2Collapsed = false;

                toolStripMenuItemHideBottomMenu.Checked = false;
            }
            else //fazer esconder
            {
                splitContainerRight.Panel2Collapsed = true;
                splitContainerRight.Panel2.Enabled = false;

                toolStripMenuItemHideBottomMenu.Checked = true;
            }

            glControl.Invalidate();
        }

        //------------
        private void toolStripMenuItemRoomHideTextures_Click(object sender, EventArgs e)
        {
            toolStripMenuItemRoomHideTextures.Checked = !toolStripMenuItemRoomHideTextures.Checked;
            NewAgeTheRender.RoomSelectedObj.RenderTextures = !NewAgeTheRender.RoomSelectedObj.RenderTextures;
            glControl.Invalidate();
        }

        private void toolStripMenuItemRoomWireframe_Click(object sender, EventArgs e)
        {
            toolStripMenuItemRoomWireframe.Checked = !toolStripMenuItemRoomWireframe.Checked;
            NewAgeTheRender.RoomSelectedObj.RenderWireframe = !NewAgeTheRender.RoomSelectedObj.RenderWireframe;
            glControl.Invalidate();
        }

        private void toolStripMenuItemRoomRenderNormals_Click(object sender, EventArgs e)
        {
            toolStripMenuItemRoomRenderNormals.Checked = !toolStripMenuItemRoomRenderNormals.Checked;
            NewAgeTheRender.RoomSelectedObj.RenderNormals = !NewAgeTheRender.RoomSelectedObj.RenderNormals;
            glControl.Invalidate();
        }

        private void toolStripMenuItemRoomOnlyFrontFace_Click(object sender, EventArgs e)
        {
            toolStripMenuItemRoomOnlyFrontFace.Checked = !toolStripMenuItemRoomOnlyFrontFace.Checked;
            NewAgeTheRender.RoomSelectedObj.RenderOnlyFrontFace = !NewAgeTheRender.RoomSelectedObj.RenderOnlyFrontFace;
            glControl.Invalidate();
        }

        private void toolStripMenuItemRoomVertexColor_Click(object sender, EventArgs e)
        {
            toolStripMenuItemRoomVertexColor.Checked = !toolStripMenuItemRoomVertexColor.Checked;
            NewAgeTheRender.RoomSelectedObj.RenderVertexColor = !NewAgeTheRender.RoomSelectedObj.RenderVertexColor;
            glControl.Invalidate();
        }

        private void toolStripMenuItemRoomAlphaChannel_Click(object sender, EventArgs e)
        {
            toolStripMenuItemRoomAlphaChannel.Checked = !toolStripMenuItemRoomAlphaChannel.Checked;
            NewAgeTheRender.RoomSelectedObj.RenderAlphaChannel = !NewAgeTheRender.RoomSelectedObj.RenderAlphaChannel;
            glControl.Invalidate();
        }

        private void toolStripMenuItemModelsHideTextures_Click(object sender, EventArgs e)
        {
            toolStripMenuItemModelsHideTextures.Checked = !toolStripMenuItemModelsHideTextures.Checked;
            NewAgeTheRender.ObjModel3D.RenderTextures = !NewAgeTheRender.ObjModel3D.RenderTextures;
            glControl.Invalidate();
        }

        private void toolStripMenuItemModelsWireframe_Click(object sender, EventArgs e)
        {
            toolStripMenuItemModelsWireframe.Checked = !toolStripMenuItemModelsWireframe.Checked;
            NewAgeTheRender.ObjModel3D.RenderWireframe = !NewAgeTheRender.ObjModel3D.RenderWireframe;
            glControl.Invalidate();
        }

        private void toolStripMenuItemModelsRenderNormals_Click(object sender, EventArgs e)
        {
            toolStripMenuItemModelsRenderNormals.Checked = !toolStripMenuItemModelsRenderNormals.Checked;
            NewAgeTheRender.ObjModel3D.RenderNormals = !NewAgeTheRender.ObjModel3D.RenderNormals;
            glControl.Invalidate();
        }

        private void toolStripMenuItemModelsOnlyFrontFace_Click(object sender, EventArgs e)
        {
            toolStripMenuItemModelsOnlyFrontFace.Checked = !toolStripMenuItemModelsOnlyFrontFace.Checked;
            NewAgeTheRender.ObjModel3D.RenderOnlyFrontFace = !NewAgeTheRender.ObjModel3D.RenderOnlyFrontFace;
            glControl.Invalidate();
        }

        private void toolStripMenuItemModelsVertexColor_Click(object sender, EventArgs e)
        {
            toolStripMenuItemModelsVertexColor.Checked = !toolStripMenuItemModelsVertexColor.Checked;
            NewAgeTheRender.ObjModel3D.RenderVertexColor = !NewAgeTheRender.ObjModel3D.RenderVertexColor;
            glControl.Invalidate();
        }

        private void toolStripMenuItemModelsAlphaChannel_Click(object sender, EventArgs e)
        {
            toolStripMenuItemModelsAlphaChannel.Checked = !toolStripMenuItemModelsAlphaChannel.Checked;
            NewAgeTheRender.ObjModel3D.RenderAlphaChannel = !NewAgeTheRender.ObjModel3D.RenderAlphaChannel;
            glControl.Invalidate();
        }

        private void toolStripMenuItemRoomTextureNearestLinear_Click(object sender, EventArgs e)
        {
            NewAgeTheRender.RoomSelectedObj.LoadTextureLinear = !NewAgeTheRender.RoomSelectedObj.LoadTextureLinear;
            
            toolStripMenuItemRoomTextureNearestLinear.Text =
                NewAgeTheRender.RoomSelectedObj.LoadTextureLinear ?
                Lang.GetText(eLang.toolStripMenuItemRoomTextureIsLinear) :
                Lang.GetText(eLang.toolStripMenuItemRoomTextureIsNearest) ;

            DataBase.SelectedRoom?.ChangeTextureType();

            glControl.Invalidate();
        }

        private void toolStripMenuItemModelsTextureNearestLinear_Click(object sender, EventArgs e)
        {
            NewAgeTheRender.ObjModel3D.LoadTextureLinear = !NewAgeTheRender.ObjModel3D.LoadTextureLinear;

            toolStripMenuItemModelsTextureNearestLinear.Text =
              NewAgeTheRender.ObjModel3D.LoadTextureLinear ?
              Lang.GetText(eLang.toolStripMenuItemModelsTextureIsLinear) :
              Lang.GetText(eLang.toolStripMenuItemModelsTextureIsNearest);

            Utils.ChangeTextureTypeFromModels();

            glControl.Invalidate();
        }

        #endregion


        #region propertyGridObjs and TreeViewObjs

        private IObject3D getSelectedObject()
        {
            if (DataBase.LastSelectNode is IObject3D node)
            {
                return node;
            }
            return null;
        }

        private void UpdateGL() 
        {
            glControl.Invalidate();
        }

        private void UpdateCameraMatrix() 
        {
            camMtx = camera.GetViewMatrix();
        }

        private void UpdatePropertyGrid() 
        {
            propertyGridObjs.Refresh();
            glControl.Update(); // Needed after calling propertyGridObjs.Refresh();
        }

        private void UpdateTreeViewObjs()
        {
            treeViewObjs.Refresh();
        }

        private void UpdateOrbitCamera() 
        {
            if (camera.isOrbitCamera())
            {
                camera.UpdateCameraOrbitOnChangeValue();
                camMtx = camera.GetViewMatrix();
            }
        }

        private void propertyGridObjs_Enter(object sender, EventArgs e)
        {
            InPropertyGrid = true;
        }

        private void propertyGridObjs_Leave(object sender, EventArgs e)
        {
            InPropertyGrid = false;
        }

        private void propertyGridObjs_PropertySortChanged(object sender, EventArgs e)
        {
            if (propertyGridObjs.PropertySort == PropertySort.CategorizedAlphabetical)
               {propertyGridObjs.PropertySort = PropertySort.Categorized;}
        }


        private void propertyGridObjs_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            propertyGridObjs.Refresh();
            treeViewObjs.Refresh();
        }

        private void propertyGridObjs_SelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs e)
        {
        }

        private void TreeViewUpdateSelectedsClear()
        {
            treeViewObjs.SelectedNodesClearNoRedraw();
            propertyGridObjs.SelectedObject = none;
            objectMove.UpdateSelection();
            treeViewObjs.Refresh();
            propertyGridObjs.Refresh();
        }

        private void TreeViewDisableDrawNode()
        {
            treeViewObjs.Enabled = false;
            //treeViewObjs.Visible = false;
            treeViewObjs.DisableDrawNode();
            //propertyGridObjs.Visible = false;
        }

        private void TreeViewEnableDrawNode()
        {
            treeViewObjs.EnableDrawNode();
            //treeViewObjs.Visible = true;
            treeViewObjs.Enabled = true;
            //propertyGridObjs.Visible = true;
        }

        private void treeViewObjs_AfterSelect(object sender, TreeViewEventArgs e)
        {
            bool OldLastNodeIsNull = !(DataBase.LastSelectNode is Object3D);
            //Console.WriteLine(e.Node);
            //Console.WriteLine(treeViewObjs.SelectedNodes.Count);
            if (e.Node == null || e.Node.Parent == null || treeViewObjs.SelectedNodes.Count == 0)
            {
                propertyGridObjs.SelectedObject = none;
                DataBase.LastSelectNode = null;
            }
            else if (treeViewObjs.SelectedNodes.Count == 1 && e.Node is Object3D node)
            {
                DataBase.LastSelectNode = node;

                if (node.Group == GroupType.ESL)
                {
                    EnemyProperty p = new EnemyProperty(node.ObjLineRef, updateMethods, ((EnemyNodeGroup)node.Parent).PropertyMethods);
                    propertyGridObjs.SelectedObject = p;
                }
                else if (node.Group == GroupType.ETS)
                {
                    EtcModelProperty p = new EtcModelProperty(node.ObjLineRef, updateMethods, ((EtcModelNodeGroup)node.Parent).PropertyMethods);
                    propertyGridObjs.SelectedObject = p;
                }
                else if (node.Group == GroupType.ITA)
                {
                    SpecialProperty p = new SpecialProperty(node.ObjLineRef, updateMethods, ((SpecialNodeGroup)node.Parent).PropertyMethods);
                    propertyGridObjs.SelectedObject = p;
                }
                else if (node.Group == GroupType.AEV)
                {
                    SpecialProperty p = new SpecialProperty(node.ObjLineRef, updateMethods, ((SpecialNodeGroup)node.Parent).PropertyMethods);
                    propertyGridObjs.SelectedObject = p;
                }
                else if (node.Group == GroupType.EXTRAS)
                {
                    var r = DataBase.Extras.AssociationList[node.ObjLineRef];
                    if (r.FileFormat == SpecialFileFormat.AEV)
                    {
                        SpecialProperty p = new SpecialProperty(r.LineID, updateMethods, DataBase.NodeAEV.PropertyMethods, true);
                        propertyGridObjs.SelectedObject = p;
                    }
                    else if (r.FileFormat == SpecialFileFormat.ITA)
                    {
                        SpecialProperty p = new SpecialProperty(r.LineID, updateMethods, DataBase.NodeITA.PropertyMethods, true);
                        propertyGridObjs.SelectedObject = p;
                    }
                    else
                    {
                        propertyGridObjs.SelectedObject = none;
                    }

                }
                else if (node.Group == GroupType.DSE)
                {
                    NewAge_DSE_Property p = new NewAge_DSE_Property(node.ObjLineRef, updateMethods, ((NewAge_DSE_NodeGroup)node.Parent).PropertyMethods);
                    propertyGridObjs.SelectedObject = p;
                }
                else if (node.Group == GroupType.FSE) 
                {
                    NewAge_FSE_Property p = new NewAge_FSE_Property(node.ObjLineRef, updateMethods, ((NewAge_FSE_NodeGroup)node.Parent).PropertyMethods);
                    propertyGridObjs.SelectedObject = p;
                }
                else if (node.Group == GroupType.SAR)
                {
                    NewAge_ESAR_Property p = new NewAge_ESAR_Property(node.ObjLineRef, updateMethods, ((NewAge_ESAR_NodeGroup)node.Parent).PropertyMethods);
                    propertyGridObjs.SelectedObject = p;
                }
                else if (node.Group == GroupType.EAR)
                {
                    NewAge_ESAR_Property p = new NewAge_ESAR_Property(node.ObjLineRef, updateMethods, ((NewAge_ESAR_NodeGroup)node.Parent).PropertyMethods);
                    propertyGridObjs.SelectedObject = p;
                }
                else if (node.Group == GroupType.ESE)
                {
                    NewAge_ESE_Property p = new NewAge_ESE_Property(node.ObjLineRef, updateMethods, ((NewAge_ESE_NodeGroup)node.Parent).PropertyMethods);
                    propertyGridObjs.SelectedObject = p;
                }
                else if (node.Group == GroupType.EMI)
                {
                    NewAge_EMI_Property p = new NewAge_EMI_Property(node.ObjLineRef, updateMethods, ((NewAge_EMI_NodeGroup)node.Parent).PropertyMethods);
                    propertyGridObjs.SelectedObject = p;
                }
                else if (node.Group == GroupType.QUAD_CUSTOM)
                {
                    QuadCustomProperty p = new QuadCustomProperty(node.ObjLineRef, updateMethods, ((QuadCustomNodeGroup)node.Parent).PropertyMethods);
                    propertyGridObjs.SelectedObject = p;
                }
                else
                {
                    propertyGridObjs.SelectedObject = none;
                    DataBase.LastSelectNode = null;
                }
            }
            else if (treeViewObjs.SelectedNodes.Count > 1)
            {
                DataBase.LastSelectNode = treeViewObjs.SelectedNodes.Last().Value;

                MultiSelectProperty p = new MultiSelectProperty(updateMethods);
                int count = p.LoadContent(treeViewObjs.SelectedNodes.Values.ToList());
                if (count != 0)
                {
                    propertyGridObjs.SelectedObject = p;
                }
                else 
                {
                    propertyGridObjs.SelectedObject = none;
                }  
            }
            else 
            {
                propertyGridObjs.SelectedObject = none;
                DataBase.LastSelectNode = null;
            }
            if (camera.isOrbitCamera())
            {
                if (OldLastNodeIsNull)
                {
                    camera.ResetOrbitToSelectedObject();
                }
                camera.UpdateCameraOrbitOnChangeObj();
                camMtx = camera.GetViewMatrix();
            }
            objectMove.UpdateSelection();
            glControl.Invalidate();
        }

        #endregion


        #region Gerenciamento de arquivos //new

        private void toolStripMenuItemNewESL_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            TreeViewDisableDrawNode();
            FileManager.NewFileESL();
            Globals.FilePathESL = null;
            TreeViewEnableDrawNode();
            glControl.Invalidate();
        }

        private void toolStripMenuItemNewETS_2007_PS2_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            FileManager.NewFileETS(Re4Version.V2007PS2);
            Globals.FilePathETS = null;
            glControl.Invalidate();
        }

        private void toolStripMenuItemNewETS_UHD_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            FileManager.NewFileETS(Re4Version.UHD);
            Globals.FilePathETS = null;
            glControl.Invalidate();
        }

        private void toolStripMenuItemNewITA_2007_PS2_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            FileManager.NewFileITA(Re4Version.V2007PS2);
            Globals.FilePathITA = null;
            glControl.Invalidate();
        }

        private void toolStripMenuItemNewITA_UHD_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            FileManager.NewFileITA(Re4Version.UHD);
            Globals.FilePathITA = null;
            glControl.Invalidate();
        }

        private void toolStripMenuItemNewAEV_2007_PS2_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            FileManager.NewFileAEV(Re4Version.V2007PS2);
            Globals.FilePathAEV = null;
            glControl.Invalidate();
        }

        private void toolStripMenuItemNewAEV_UHD_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            FileManager.NewFileAEV(Re4Version.UHD);
            Globals.FilePathAEV = null;
            glControl.Invalidate();
        }

        private void toolStripMenuItemNewDSE_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            FileManager.NewFileDSE();
            Globals.FilePathDSE = null;
            glControl.Invalidate();
        }

        private void toolStripMenuItemNewFSE_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            FileManager.NewFileFSE();
            Globals.FilePathFSE = null;
            glControl.Invalidate();
        }

        private void toolStripMenuItemNewSAR_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            FileManager.NewFileSAR();
            Globals.FilePathSAR = null;
            glControl.Invalidate();
        }

        private void toolStripMenuItemNewEAR_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            FileManager.NewFileEAR();
            Globals.FilePathEAR = null;
            glControl.Invalidate();
        }

        private void toolStripMenuItemNewEMI_2007_PS2_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            FileManager.NewFileEMI(Re4Version.V2007PS2);
            Globals.FilePathEMI = null;
            glControl.Invalidate();
        }

        private void toolStripMenuItemNewESE_2007_PS2_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            FileManager.NewFileESE(Re4Version.V2007PS2);
            Globals.FilePathESE = null;
            glControl.Invalidate();
        }

        private void toolStripMenuItemNewEMI_UHD_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            FileManager.NewFileEMI(Re4Version.UHD);
            Globals.FilePathEMI = null;
            glControl.Invalidate();
        }

        private void toolStripMenuItemNewESE_UHD_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            FileManager.NewFileESE(Re4Version.UHD);
            Globals.FilePathESE = null;
            glControl.Invalidate();
        }

        private void toolStripMenuItemNewQuadCustom_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            FileManager.NewFileQuadCustom();
            Globals.FilePathQuadCustom = null;
            glControl.Invalidate();
        }

        private void toolStripMenuItemNewEFFBLOB_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            FileManager.NewFileEFFBLOB();
            Globals.FilePathEFFBLOB = null;
            glControl.Invalidate();
        }

        private void toolStripMenuItemNewLIT_2007_PS2_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            FileManager.NewFileLIT(Re4Version.V2007PS2);
            Globals.FilePathLIT = null;
            glControl.Invalidate();
        }

        private void toolStripMenuItemNewLIT_UHD_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            FileManager.NewFileLIT(Re4Version.UHD);
            Globals.FilePathLIT = null;
            glControl.Invalidate();
        }

        private void toolStripMenuItemNewITA_PS4_NS_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            FileManager.NewFileITA(Re4Version.UHD, true);
            Globals.FilePathITA = null;
            glControl.Invalidate();
        }

        private void toolStripMenuItemNewAEV_PS4_NS_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            FileManager.NewFileAEV(Re4Version.UHD, true);
            Globals.FilePathAEV = null;
            glControl.Invalidate();
        }

        #endregion

        #region Gerenciamento de arquivos //open

        private bool OpenIsUHD = false;
        private bool OpenIsPs4Ns_Adapted = false;
        private void toolStripMenuItemOpenESL_Click(object sender, EventArgs e)
        {
            openFileDialogESL.ShowDialog();
        }
        private void toolStripMenuItemOpenETS_2007_PS2_Click(object sender, EventArgs e)
        {
            OpenIsUHD = false;
            openFileDialogETS.ShowDialog();
        }
        private void toolStripMenuItemOpenETS_UHD_Click(object sender, EventArgs e)
        {
            OpenIsUHD = true;
            openFileDialogETS.ShowDialog();
        }
        private void toolStripMenuItemOpenITA_2007_PS2_Click(object sender, EventArgs e)
        {
            OpenIsUHD = false;
            OpenIsPs4Ns_Adapted = false;
            openFileDialogITA.ShowDialog();
        }
        private void toolStripMenuItemOpenITA_UHD_Click(object sender, EventArgs e)
        {
            OpenIsUHD = true;
            OpenIsPs4Ns_Adapted = false;
            openFileDialogITA.ShowDialog();
        }
        private void toolStripMenuItemOpenAEV_2007_PS2_Click(object sender, EventArgs e)
        {
            OpenIsUHD = false;
            OpenIsPs4Ns_Adapted = false;
            openFileDialogAEV.ShowDialog();
        }
        private void toolStripMenuItemOpenAEV_UHD_Click(object sender, EventArgs e)
        {
            OpenIsUHD = true;
            OpenIsPs4Ns_Adapted = false;
            openFileDialogAEV.ShowDialog();
        }
        private void toolStripMenuItemOpenITA_PS4_NS_Click(object sender, EventArgs e)
        {
            OpenIsUHD = true;
            OpenIsPs4Ns_Adapted = true;
            openFileDialogITA.ShowDialog();
        }
        private void toolStripMenuItemOpenAEV_PS4_NS_Click(object sender, EventArgs e)
        {
            OpenIsUHD = true;
            OpenIsPs4Ns_Adapted = true;
            openFileDialogAEV.ShowDialog();
        }
        private void toolStripMenuItemOpenDSE_Click(object sender, EventArgs e)
        {
            openFileDialogDSE.ShowDialog();
        }
        private void toolStripMenuItemOpenFSE_Click(object sender, EventArgs e)
        {
            openFileDialogFSE.ShowDialog();
        }
        private void toolStripMenuItemOpenSAR_Click(object sender, EventArgs e)
        {
            openFileDialogSAR.ShowDialog();
        }
        private void toolStripMenuItemOpenEAR_Click(object sender, EventArgs e)
        {
            openFileDialogEAR.ShowDialog();
        }
        private void toolStripMenuItemOpenEMI_2007_PS2_Click(object sender, EventArgs e)
        {
            OpenIsUHD = false;
            openFileDialogEMI.ShowDialog();
        }
        private void toolStripMenuItemOpenESE_2007_PS2_Click(object sender, EventArgs e)
        {
            OpenIsUHD = false;
            openFileDialogESE.ShowDialog();
        }
        private void toolStripMenuItemOpenEMI_UHD_Click(object sender, EventArgs e)
        {
            OpenIsUHD = true;
            openFileDialogEMI.ShowDialog();
        }
        private void toolStripMenuItemOpenESE_UHD_Click(object sender, EventArgs e)
        {
            OpenIsUHD = true;
            openFileDialogESE.ShowDialog();
        }
        private void toolStripMenuItemOpenQuadCustom_Click(object sender, EventArgs e)
        {
            openFileDialogQuadCustom.ShowDialog();
        }

        private void toolStripMenuItemOpenEFFBLOB_Click(object sender, EventArgs e)
        {
            openFileDialogEFFBLOB.ShowDialog();
        }

        private void toolStripMenuItemOpenLIT_2007_PS2_Click(object sender, EventArgs e)
        {
            OpenIsUHD = false;
            openFileDialogLIT.ShowDialog();
        }

        private void toolStripMenuItemOpenLIT_UHD_Click(object sender, EventArgs e)
        {
            OpenIsUHD = true;
            openFileDialogLIT.ShowDialog();
        }

        private void openFileDialogESL_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo fileInfo = null;
            try
            {
                fileInfo = new FileInfo(openFileDialogESL.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }
            if (fileInfo != null)
            {
                if (fileInfo.Length > 0x1000000)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile16MB), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else if (fileInfo.Length == 0)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile0MB), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else
                {
                    FileStream file = null;
                    try
                    {
                        file = fileInfo.OpenRead();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                        e.Cancel = true;
                        return;
                    }
                    if (file != null && fileInfo != null)
                    {
                        TreeViewUpdateSelectedsClear();
                        TreeViewDisableDrawNode();
                        FileManager.LoadFileESL(file, fileInfo);
                        file.Close();
                        Globals.FilePathESL = openFileDialogESL.FileName;
                        openFileDialogESL.FileName = null;
                        glControl.Invalidate();
                        TreeViewEnableDrawNode();
                    }
 
                }
            }

        }
        private void openFileDialogETS_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo fileInfo = null;
            try
            {
                fileInfo = new FileInfo(openFileDialogETS.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }
            if (fileInfo != null)
            {
                if (fileInfo.Length > 0x1000000)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile16MB), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else if (fileInfo.Length == 0)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile0MB), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else if (fileInfo.Length < 16)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile16Bytes), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else
                {
                    FileStream file = null;
                    try
                    {
                        file = fileInfo.OpenRead();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                        e.Cancel = true;
                        return;
                    }
                    if (file != null && fileInfo != null)
                    {
                        TreeViewUpdateSelectedsClear();
                        TreeViewDisableDrawNode();
                        if (OpenIsUHD)
                        {
                            FileManager.LoadFileETS_UHD(file, fileInfo);
                        }
                        else
                        {
                            FileManager.LoadFileETS_2007_PS2(file, fileInfo);
                        }
                        file.Close();
                        Globals.FilePathETS = openFileDialogETS.FileName;
                        openFileDialogETS.FileName = null;
                        glControl.Invalidate();
                        TreeViewEnableDrawNode();
                    }
                }
            }
        }
        private void openFileDialogITA_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo fileInfo = null;
            try
            {
                fileInfo = new FileInfo(openFileDialogITA.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }
            if (fileInfo != null)
            {
                if (fileInfo.Length > 0x1000000)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile16MB), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else if (fileInfo.Length == 0)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile0MB), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else if (fileInfo.Length < 16)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile16Bytes), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else
                {
                    FileStream file = null;
                    try
                    {
                        file = fileInfo.OpenRead();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                        e.Cancel = true;
                        return;
                    }
                    if (file != null && fileInfo != null)
                    {
                        TreeViewUpdateSelectedsClear();
                        TreeViewDisableDrawNode();
                        if (OpenIsPs4Ns_Adapted)
                        {
                            FileManager.LoadFileITA_PS4_NS(file, fileInfo);
                        }
                        else if (OpenIsUHD)
                        {
                            FileManager.LoadFileITA_UHD(file, fileInfo);
                        }
                        else
                        {
                            FileManager.LoadFileITA_2007_PS2(file, fileInfo);
                        }
                        file.Close();
                        Globals.FilePathITA = openFileDialogITA.FileName;
                        openFileDialogITA.FileName = null;
                        glControl.Invalidate();
                        TreeViewEnableDrawNode();
                    }
                }
            }
        }
        private void openFileDialogAEV_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo fileInfo = null;
            try
            {
                fileInfo = new FileInfo(openFileDialogAEV.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }
            if (fileInfo != null)
            {
                if (fileInfo.Length > 0x1000000)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile16MB), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else if (fileInfo.Length == 0)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile0MB), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else if (fileInfo.Length < 16)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile16Bytes), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else
                {
                    FileStream file = null;
                    try
                    {
                        file = fileInfo.OpenRead();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                        e.Cancel = true;
                        return;
                    }
                    if (file != null && fileInfo != null)
                    {
                        TreeViewUpdateSelectedsClear();
                        TreeViewDisableDrawNode();
                        if (OpenIsPs4Ns_Adapted)
                        {
                            FileManager.LoadFileAEV_PS4_NS(file, fileInfo);
                        }
                        else if (OpenIsUHD)
                        {
                            FileManager.LoadFileAEV_UHD(file, fileInfo);
                        }
                        else
                        {
                            FileManager.LoadFileAEV_2007_PS2(file, fileInfo);
                        }
                        file.Close();
                        Globals.FilePathAEV = openFileDialogAEV.FileName;
                        openFileDialogAEV.FileName = null;
                        glControl.Invalidate();
                        TreeViewEnableDrawNode();
                    }
                }
            }
        }
        private void openFileDialogDSE_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo fileInfo = null;
            try
            {
                fileInfo = new FileInfo(openFileDialogDSE.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }
            if (fileInfo != null)
            {
                if (fileInfo.Length > 0x1000000)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile16MB), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else if (fileInfo.Length == 0)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile0MB), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else if (fileInfo.Length < 4)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile4Bytes), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else
                {
                    FileStream file = null;
                    try
                    {
                        file = fileInfo.OpenRead();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                        e.Cancel = true;
                        return;
                    }
                    if (file != null && fileInfo != null)
                    {
                        TreeViewUpdateSelectedsClear();
                        TreeViewDisableDrawNode();
                        FileManager.LoadFileDSE(file, fileInfo);
                        file.Close();
                        Globals.FilePathDSE = openFileDialogDSE.FileName;
                        openFileDialogDSE.FileName = null;
                        glControl.Invalidate();
                        TreeViewEnableDrawNode();
                    }
                }
            }
        }
        private void openFileDialogFSE_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo fileInfo = null;
            try
            {
                fileInfo = new FileInfo(openFileDialogFSE.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }
            if (fileInfo != null)
            {
                if (fileInfo.Length > 0x1000000)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile16MB), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else if (fileInfo.Length == 0)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile0MB), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else if (fileInfo.Length < 16)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile16Bytes), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else
                {
                    FileStream file = null;
                    try
                    {
                        file = fileInfo.OpenRead();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                        e.Cancel = true;
                        return;
                    }
                    if (file != null && fileInfo != null)
                    {
                        TreeViewUpdateSelectedsClear();
                        TreeViewDisableDrawNode();
                        FileManager.LoadFileFSE(file, fileInfo);
                        file.Close();
                        Globals.FilePathFSE = openFileDialogFSE.FileName;
                        openFileDialogFSE.FileName = null;
                        glControl.Invalidate();
                        TreeViewEnableDrawNode();
                    }
                }
            }
        }
        private void openFileDialogSAR_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo fileInfo = null;
            try
            {
                fileInfo = new FileInfo(openFileDialogSAR.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }
            if (fileInfo != null)
            {
                if (fileInfo.Length > 0x1000000)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile16MB), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else if (fileInfo.Length == 0)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile0MB), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else if (fileInfo.Length < 16)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile16Bytes), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else
                {
                    FileStream file = null;
                    try
                    {
                        file = fileInfo.OpenRead();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                        e.Cancel = true;
                        return;
                    }
                    if (file != null && fileInfo != null)
                    {
                        TreeViewUpdateSelectedsClear();
                        TreeViewDisableDrawNode();
                        FileManager.LoadFileSAR(file, fileInfo);
                        file.Close();
                        Globals.FilePathSAR = openFileDialogSAR.FileName;
                        openFileDialogSAR.FileName = null;
                        glControl.Invalidate();
                        TreeViewEnableDrawNode();
                    }
                }
            }
        }
        private void openFileDialogEAR_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo fileInfo = null;
            try
            {
                fileInfo = new FileInfo(openFileDialogEAR.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }
            if (fileInfo != null)
            {
                if (fileInfo.Length > 0x1000000)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile16MB), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else if (fileInfo.Length == 0)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile0MB), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else if (fileInfo.Length < 16)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile16Bytes), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else
                {
                    FileStream file = null;
                    try
                    {
                        file = fileInfo.OpenRead();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                        e.Cancel = true;
                        return;
                    }
                    if (file != null && fileInfo != null)
                    {
                        TreeViewUpdateSelectedsClear();
                        TreeViewDisableDrawNode();
                        FileManager.LoadFileEAR(file, fileInfo);
                        file.Close();
                        Globals.FilePathEAR = openFileDialogEAR.FileName;
                        openFileDialogEAR.FileName = null;
                        glControl.Invalidate();
                        TreeViewEnableDrawNode();
                    }
                }
            }
        }
        private void openFileDialogEMI_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo fileInfo = null;
            try
            {
                fileInfo = new FileInfo(openFileDialogEMI.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }
            if (fileInfo != null)
            {
                if (fileInfo.Length > 0x1000000)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile16MB), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else if (fileInfo.Length == 0)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile0MB), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else if (fileInfo.Length < 4)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile4Bytes), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else
                {
                    FileStream file = null;
                    try
                    {
                        file = fileInfo.OpenRead();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                        e.Cancel = true;
                        return;
                    }
                    if (file != null && fileInfo != null)
                    {
                        TreeViewUpdateSelectedsClear();
                        TreeViewDisableDrawNode();
                        if (OpenIsUHD)
                        {
                            FileManager.LoadFileEMI_UHD(file, fileInfo);
                        }
                        else
                        {
                            FileManager.LoadFileEMI_2007_PS2(file, fileInfo);
                        }
                        file.Close();
                        Globals.FilePathEMI = openFileDialogEMI.FileName;
                        openFileDialogEMI.FileName = null;
                        glControl.Invalidate();
                        TreeViewEnableDrawNode();
                    }
                }
            }
        }
        private void openFileDialogESE_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo fileInfo = null;
            try
            {
                fileInfo = new FileInfo(openFileDialogESE.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }
            if (fileInfo != null)
            {
                if (fileInfo.Length > 0x1000000)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile16MB), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else if (fileInfo.Length == 0)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile0MB), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else if (fileInfo.Length < 16)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile16Bytes), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else
                {
                    FileStream file = null;
                    try
                    {
                        file = fileInfo.OpenRead();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                        e.Cancel = true;
                        return;
                    }
                    if (file != null && fileInfo != null)
                    {
                        TreeViewUpdateSelectedsClear();
                        TreeViewDisableDrawNode();

                        if (OpenIsUHD)
                        {
                            FileManager.LoadFileESE_UHD(file, fileInfo);
                        }
                        else
                        {
                            FileManager.LoadFileESE_2007_PS2(file, fileInfo);
                        }
                        file.Close();
                        Globals.FilePathESE = openFileDialogESE.FileName;
                        openFileDialogESE.FileName = null;
                        glControl.Invalidate();
                        TreeViewEnableDrawNode();
                    }
                }
            }
        }
        private void openFileDialogQuadCustom_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo fileInfo = null;
            try
            {
                fileInfo = new FileInfo(openFileDialogQuadCustom.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }
            if (fileInfo != null)
            {
                if (fileInfo.Length > 0x1000000)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile16MB), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else if (fileInfo.Length == 0)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile0MB), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else
                {
                   
                    FileStream file = null;
                    try
                    {
                        file = fileInfo.OpenRead();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                        e.Cancel = true;
                        return;
                    }
                    if (file != null && fileInfo != null)
                    {
                        TreeViewUpdateSelectedsClear();
                        TreeViewDisableDrawNode();
                        FileManager.LoadFileQuadCustom(file, fileInfo);
                        file.Close();
                        Globals.FilePathQuadCustom = openFileDialogQuadCustom.FileName;
                        openFileDialogQuadCustom.FileName = null;
                        glControl.Invalidate();
                        TreeViewEnableDrawNode();
                    }

                }
            }
        }
        private void openFileDialogLIT_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo fileInfo = null;
            try
            {
                fileInfo = new FileInfo(openFileDialogLIT.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }
            if (fileInfo != null)
            {
                if (fileInfo.Length > 0x1000000)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile16MB), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else if (fileInfo.Length == 0)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile0MB), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else if (fileInfo.Length < 4)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile4Bytes), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else
                {
                    FileStream file = null;
                    try
                    {
                        file = fileInfo.OpenRead();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                        e.Cancel = true;
                        return;
                    }
                    if (file != null && fileInfo != null)
                    {
                        TreeViewUpdateSelectedsClear();
                        TreeViewDisableDrawNode();
                        if (OpenIsUHD)
                        {
                            FileManager.LoadFileLIT_UHD(file, fileInfo);
                        }
                        else
                        {
                            FileManager.LoadFileLIT_2007_PS2(file, fileInfo);
                        }
                        file.Close();
                        Globals.FilePathLIT = openFileDialogLIT.FileName;
                        openFileDialogLIT.FileName = null;
                        glControl.Invalidate();
                        TreeViewEnableDrawNode();
                    }
                }
            }
        }
        private void openFileDialogEFFBLOB_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo fileInfo = null;
            try
            {
                fileInfo = new FileInfo(openFileDialogEFFBLOB.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }
            if (fileInfo != null)
            {
                if (fileInfo.Length > 0x1000000)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile16MB), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else if (fileInfo.Length == 0)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile0MB), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else if (fileInfo.Length < 16)
                {
                    MessageBox.Show(Lang.GetText(eLang.MessageBoxFile16Bytes), Lang.GetText(eLang.MessageBoxWarningTitle), MessageBoxButtons.OK);
                    e.Cancel = true;
                    return;
                }
                else
                {
                    FileStream file = null;
                    try
                    {
                        file = fileInfo.OpenRead();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                        e.Cancel = true;
                        return;
                    }
                    if (file != null && fileInfo != null)
                    {
                        TreeViewUpdateSelectedsClear();
                        TreeViewDisableDrawNode();
                        FileManager.LoadFileEFFBLOB(file, fileInfo);
                        file.Close();
                        Globals.FilePathEFFBLOB = openFileDialogEFFBLOB.FileName;
                        openFileDialogEFFBLOB.FileName = null;
                        glControl.Invalidate();
                        TreeViewEnableDrawNode();
                    }
                }
            }
        }

        #endregion

        #region Gerenciamento de arquivos //Clear

        private void toolStripMenuItemClear_DropDownOpening(object sender, EventArgs e)
        {
            toolStripMenuItemClearESL.Enabled = DataBase.FileESL != null;
            toolStripMenuItemClearETS.Enabled = DataBase.FileETS != null;
            toolStripMenuItemClearITA.Enabled = DataBase.FileITA != null;
            toolStripMenuItemClearAEV.Enabled = DataBase.FileAEV != null;
            toolStripMenuItemClearDSE.Enabled = DataBase.FileDSE != null;
            toolStripMenuItemClearFSE.Enabled = DataBase.FileFSE != null;
            toolStripMenuItemClearSAR.Enabled = DataBase.FileSAR != null;
            toolStripMenuItemClearEAR.Enabled = DataBase.FileEAR != null;
            toolStripMenuItemClearEMI.Enabled = DataBase.FileEMI != null;
            toolStripMenuItemClearESE.Enabled = DataBase.FileESE != null;
            toolStripMenuItemClearQuadCustom.Enabled = DataBase.FileQuadCustom != null;
        }

        private void toolStripMenuItemClearESL_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            TreeViewDisableDrawNode();
            FileManager.ClearESL();
            Globals.FilePathESL = null;
            glControl.Invalidate();
            TreeViewEnableDrawNode();
        }

        private void toolStripMenuItemClearETS_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            TreeViewDisableDrawNode();
            FileManager.ClearETS();
            Globals.FilePathETS = null;
            glControl.Invalidate();
            TreeViewEnableDrawNode();
        }

        private void toolStripMenuItemClearITA_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            TreeViewDisableDrawNode();
            FileManager.ClearITA();
            Globals.FilePathITA = null;
            glControl.Invalidate();
            TreeViewEnableDrawNode();
        }

        private void toolStripMenuItemClearAEV_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            TreeViewDisableDrawNode();
            FileManager.ClearAEV();
            Globals.FilePathAEV = null;
            glControl.Invalidate();
            TreeViewEnableDrawNode();
        }

        private void toolStripMenuItemClearDSE_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            TreeViewDisableDrawNode();
            FileManager.ClearDSE();
            Globals.FilePathDSE = null;
            glControl.Invalidate();
            TreeViewEnableDrawNode();
        }

        private void toolStripMenuItemClearFSE_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            TreeViewDisableDrawNode();
            FileManager.ClearFSE();
            Globals.FilePathFSE = null;
            glControl.Invalidate();
            TreeViewEnableDrawNode();
        }

        private void toolStripMenuItemClearSAR_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            TreeViewDisableDrawNode();
            FileManager.ClearSAR();
            Globals.FilePathSAR = null;
            glControl.Invalidate();
            TreeViewEnableDrawNode();
        }

        private void toolStripMenuItemClearEAR_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            TreeViewDisableDrawNode();
            FileManager.ClearEAR();
            Globals.FilePathEAR = null;
            glControl.Invalidate();
            TreeViewEnableDrawNode();
        }

        private void toolStripMenuItemClearEMI_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            TreeViewDisableDrawNode();
            FileManager.ClearEMI();
            Globals.FilePathEMI = null;
            glControl.Invalidate();
            TreeViewEnableDrawNode();
        }

        private void toolStripMenuItemClearESE_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            TreeViewDisableDrawNode();
            FileManager.ClearESE();
            Globals.FilePathESE = null;
            glControl.Invalidate();
            TreeViewEnableDrawNode();
        }

        private void toolStripMenuItemClearQuadCustom_Click(object sender, EventArgs e)
        {
            TreeViewUpdateSelectedsClear();
            TreeViewDisableDrawNode();
            FileManager.ClearQuadCustom();
            Globals.FilePathQuadCustom = null;
            glControl.Invalidate();
            TreeViewEnableDrawNode();
        }

        #endregion

        #region Gerenciamento de arquivos //Save As..

        private void toolStripMenuItemSaveAs_DropDownOpening(object sender, EventArgs e)
        {
            toolStripMenuItemSaveAsESL.Enabled = DataBase.FileESL != null;
            toolStripMenuItemSaveAsETS.Enabled = DataBase.FileETS != null;
            toolStripMenuItemSaveAsITA.Enabled = DataBase.FileITA != null;
            toolStripMenuItemSaveAsAEV.Enabled = DataBase.FileAEV != null;
            toolStripMenuItemSaveAsDSE.Enabled = DataBase.FileDSE != null;
            toolStripMenuItemSaveAsFSE.Enabled = DataBase.FileFSE != null;
            toolStripMenuItemSaveAsSAR.Enabled = DataBase.FileSAR != null;
            toolStripMenuItemSaveAsEAR.Enabled = DataBase.FileEAR != null;
            toolStripMenuItemSaveAsEMI.Enabled = DataBase.FileEMI != null;
            toolStripMenuItemSaveAsESE.Enabled = DataBase.FileESE != null;
            toolStripMenuItemSaveAsQuadCustom.Enabled = DataBase.FileQuadCustom != null;

            if (DataBase.FileETS != null && DataBase.FileETS.GetRe4Version == Re4Version.V2007PS2)
            {
                toolStripMenuItemSaveAsETS.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsETS_2007_PS2);
            }
            else if (DataBase.FileETS != null && DataBase.FileETS.GetRe4Version == Re4Version.UHD)
            {
                toolStripMenuItemSaveAsETS.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsETS_UHD);
            }
            else 
            {
                toolStripMenuItemSaveAsETS.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsETS);
            }

            if (DataBase.FileITA != null && DataBase.FileITA.IsPs4Ns_Adapted)
            {
                toolStripMenuItemSaveAsITA.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsITA_PS4_NS);
            }
            else if (DataBase.FileITA != null && DataBase.FileITA.GetRe4Version == Re4Version.V2007PS2)
            {
                toolStripMenuItemSaveAsITA.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsITA_2007_PS2);
            }
            else if (DataBase.FileITA != null && DataBase.FileITA.GetRe4Version == Re4Version.UHD)
            {
                toolStripMenuItemSaveAsITA.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsITA_UHD);
            }
            else
            {
                toolStripMenuItemSaveAsITA.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsITA);
            }

            if (DataBase.FileAEV != null && DataBase.FileAEV.IsPs4Ns_Adapted)
            {
                toolStripMenuItemSaveAsAEV.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsAEV_PS4_NS);
            }
            else if (DataBase.FileAEV != null && DataBase.FileAEV.GetRe4Version == Re4Version.V2007PS2)
            {
                toolStripMenuItemSaveAsAEV.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsAEV_2007_PS2);
            }
            else if (DataBase.FileAEV != null && DataBase.FileAEV.GetRe4Version == Re4Version.UHD)
            {
                toolStripMenuItemSaveAsAEV.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsAEV_UHD);
            }
            else
            {
                toolStripMenuItemSaveAsAEV.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsAEV);
            }

            if (DataBase.FileEMI != null && DataBase.FileEMI.GetRe4Version == Re4Version.V2007PS2)
            {
                toolStripMenuItemSaveAsEMI.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsEMI_2007_PS2);
            }
            else if (DataBase.FileEMI != null && DataBase.FileEMI.GetRe4Version == Re4Version.UHD)
            {
                toolStripMenuItemSaveAsEMI.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsEMI_UHD);
            }
            else
            {
                toolStripMenuItemSaveAsEMI.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsEMI);
            }

            if (DataBase.FileESE != null && DataBase.FileESE.GetRe4Version == Re4Version.V2007PS2)
            {
                toolStripMenuItemSaveAsESE.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsESE_2007_PS2);
            }
            else if (DataBase.FileESE != null && DataBase.FileESE.GetRe4Version == Re4Version.UHD)
            {
                toolStripMenuItemSaveAsESE.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsESE_UHD);
            }
            else
            {
                toolStripMenuItemSaveAsESE.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsESE);
            }

        }

        private void toolStripMenuItemSaveAsESL_Click(object sender, EventArgs e)
        {
            saveFileDialogESL.FileName = Globals.FilePathESL;
            saveFileDialogESL.ShowDialog();
        }

        private void toolStripMenuItemSaveAsETS_Click(object sender, EventArgs e)
        {
            saveFileDialogETS.FileName = Globals.FilePathETS;
            saveFileDialogETS.ShowDialog();
        }

        private void toolStripMenuItemSaveAsITA_Click(object sender, EventArgs e)
        {
            saveFileDialogITA.FileName = Globals.FilePathITA;
            saveFileDialogITA.ShowDialog();
        }

        private void toolStripMenuItemSaveAsAEV_Click(object sender, EventArgs e)
        {
            saveFileDialogAEV.FileName = Globals.FilePathAEV;
            saveFileDialogAEV.ShowDialog();
        }

        private void toolStripMenuItemSaveAsDSE_Click(object sender, EventArgs e)
        {
            saveFileDialogDSE.FileName = Globals.FilePathDSE;
            saveFileDialogDSE.ShowDialog();
        }

        private void toolStripMenuItemSaveAsFSE_Click(object sender, EventArgs e)
        {
            saveFileDialogFSE.FileName = Globals.FilePathFSE;
            saveFileDialogFSE.ShowDialog();
        }

        private void toolStripMenuItemSaveAsSAR_Click(object sender, EventArgs e)
        {
            saveFileDialogSAR.FileName = Globals.FilePathSAR;
            saveFileDialogSAR.ShowDialog();
        }

        private void toolStripMenuItemSaveAsEAR_Click(object sender, EventArgs e)
        {
            saveFileDialogEAR.FileName = Globals.FilePathEAR;
            saveFileDialogEAR.ShowDialog();
        }

        private void toolStripMenuItemSaveAsEMI_Click(object sender, EventArgs e)
        {
            saveFileDialogEMI.FileName = Globals.FilePathEMI;
            saveFileDialogEMI.ShowDialog();
        }

        private void toolStripMenuItemSaveAsESE_Click(object sender, EventArgs e)
        {
            saveFileDialogESE.FileName = Globals.FilePathESE;
            saveFileDialogESE.ShowDialog();
        }

        private void toolStripMenuItemSaveAsQuadCustom_Click(object sender, EventArgs e)
        {
            saveFileDialogQuadCustom.FileName = Globals.FilePathQuadCustom;
            saveFileDialogQuadCustom.ShowDialog();
        }

        private void saveFileDialogESL_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(saveFileDialogESL.FileName);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveFileESL(stream);
                stream.Close();
                Globals.FilePathESL = saveFileDialogESL.FileName;
                saveFileDialogESL.FileName = null;
            }
            
        }

        private void saveFileDialogETS_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(saveFileDialogETS.FileName);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveFileETS(stream);
                stream.Close();
                Globals.FilePathETS = saveFileDialogETS.FileName;
                saveFileDialogETS.FileName = null;
            }
        }

        private void saveFileDialogITA_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(saveFileDialogITA.FileName);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveFileITA(stream);
                stream.Close();
                Globals.FilePathITA = saveFileDialogITA.FileName;
                saveFileDialogITA.FileName = null;
            }
        }

        private void saveFileDialogAEV_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(saveFileDialogAEV.FileName);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveFileAEV(stream);
                stream.Close();
                Globals.FilePathAEV = saveFileDialogAEV.FileName;
                saveFileDialogAEV.FileName = null;
            }
        }

        private void saveFileDialogDSE_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(saveFileDialogDSE.FileName);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveFileDSE(stream);
                stream.Close();
                Globals.FilePathDSE = saveFileDialogDSE.FileName;
                saveFileDialogDSE.FileName = null;
            }
        }

        private void saveFileDialogFSE_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(saveFileDialogFSE.FileName);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveFileFSE(stream);
                stream.Close();
                Globals.FilePathFSE = saveFileDialogFSE.FileName;
                saveFileDialogFSE.FileName = null;
            }
        }

        private void saveFileDialogSAR_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(saveFileDialogSAR.FileName);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveFileSAR(stream);
                stream.Close();
                Globals.FilePathSAR = saveFileDialogSAR.FileName;
                saveFileDialogSAR.FileName = null;
            }
        }

        private void saveFileDialogEAR_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(saveFileDialogEAR.FileName);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveFileEAR(stream);
                stream.Close();
                Globals.FilePathEAR = saveFileDialogEAR.FileName;
                saveFileDialogEAR.FileName = null;
            }
        }

        private void saveFileDialogEMI_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(saveFileDialogEMI.FileName);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveFileEMI(stream);
                stream.Close();
                Globals.FilePathEMI = saveFileDialogEMI.FileName;
                saveFileDialogEMI.FileName = null;
            }
        }

        private void saveFileDialogESE_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(saveFileDialogESE.FileName);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveFileESE(stream);
                stream.Close();
                Globals.FilePathESE = saveFileDialogESE.FileName;
                saveFileDialogESE.FileName = null;
            }
        }

        private void saveFileDialogQuadCustom_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(saveFileDialogQuadCustom.FileName);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveFileQuadCustom(stream);
                stream.Close();
                Globals.FilePathQuadCustom = saveFileDialogQuadCustom.FileName;
                saveFileDialogQuadCustom.FileName = null;
            }
        }

        #endregion

        #region Gerenciamento de arquivos //Save

        private void toolStripMenuItemSave_DropDownOpening(object sender, EventArgs e)
        {
            toolStripMenuItemSaveESL.Enabled = DataBase.FileESL != null;
            toolStripMenuItemSaveETS.Enabled = DataBase.FileETS != null;
            toolStripMenuItemSaveITA.Enabled = DataBase.FileITA != null;
            toolStripMenuItemSaveAEV.Enabled = DataBase.FileAEV != null;
            toolStripMenuItemSaveDSE.Enabled = DataBase.FileDSE != null;
            toolStripMenuItemSaveFSE.Enabled = DataBase.FileFSE != null;
            toolStripMenuItemSaveSAR.Enabled = DataBase.FileSAR != null;
            toolStripMenuItemSaveEAR.Enabled = DataBase.FileEAR != null;
            toolStripMenuItemSaveEMI.Enabled = DataBase.FileEMI != null;
            toolStripMenuItemSaveESE.Enabled = DataBase.FileESE != null;
            toolStripMenuItemSaveQuadCustom.Enabled = DataBase.FileQuadCustom != null;

            if (DataBase.FileETS != null && DataBase.FileETS.GetRe4Version == Re4Version.V2007PS2)
            {
                toolStripMenuItemSaveETS.Text = Lang.GetText(eLang.toolStripMenuItemSaveETS_2007_PS2);
            }
            else if (DataBase.FileETS != null && DataBase.FileETS.GetRe4Version == Re4Version.UHD)
            {
                toolStripMenuItemSaveETS.Text = Lang.GetText(eLang.toolStripMenuItemSaveETS_UHD);
            }
            else
            {
                toolStripMenuItemSaveETS.Text = Lang.GetText(eLang.toolStripMenuItemSaveETS);
            }

            if (DataBase.FileITA != null && DataBase.FileITA.IsPs4Ns_Adapted)
            {
                toolStripMenuItemSaveITA.Text = Lang.GetText(eLang.toolStripMenuItemSaveITA_PS4_NS);
            }
            else if (DataBase.FileITA != null && DataBase.FileITA.GetRe4Version == Re4Version.V2007PS2)
            {
                toolStripMenuItemSaveITA.Text = Lang.GetText(eLang.toolStripMenuItemSaveITA_2007_PS2);
            }
            else if (DataBase.FileITA != null && DataBase.FileITA.GetRe4Version == Re4Version.UHD)
            {
                toolStripMenuItemSaveITA.Text = Lang.GetText(eLang.toolStripMenuItemSaveITA_UHD);
            }
            else
            {
                toolStripMenuItemSaveITA.Text = Lang.GetText(eLang.toolStripMenuItemSaveITA);
            }

            if (DataBase.FileAEV != null && DataBase.FileAEV.IsPs4Ns_Adapted)
            {
                toolStripMenuItemSaveAEV.Text = Lang.GetText(eLang.toolStripMenuItemSaveAEV_PS4_NS);
            }
            else if (DataBase.FileAEV != null && DataBase.FileAEV.GetRe4Version == Re4Version.V2007PS2)
            {
                toolStripMenuItemSaveAEV.Text = Lang.GetText(eLang.toolStripMenuItemSaveAEV_2007_PS2);
            }
            else if (DataBase.FileAEV != null && DataBase.FileAEV.GetRe4Version == Re4Version.UHD)
            {
                toolStripMenuItemSaveAEV.Text = Lang.GetText(eLang.toolStripMenuItemSaveAEV_UHD);
            }
            else
            {
                toolStripMenuItemSaveAEV.Text = Lang.GetText(eLang.toolStripMenuItemSaveAEV);
            }


            if (DataBase.FileEMI != null && DataBase.FileEMI.GetRe4Version == Re4Version.V2007PS2)
            {
                toolStripMenuItemSaveEMI.Text = Lang.GetText(eLang.toolStripMenuItemSaveEMI_2007_PS2);
            }
            else if (DataBase.FileEMI != null && DataBase.FileEMI.GetRe4Version == Re4Version.UHD)
            {
                toolStripMenuItemSaveEMI.Text = Lang.GetText(eLang.toolStripMenuItemSaveEMI_UHD);
            }
            else
            {
                toolStripMenuItemSaveEMI.Text = Lang.GetText(eLang.toolStripMenuItemSaveEMI);
            }

            if (DataBase.FileESE != null && DataBase.FileESE.GetRe4Version == Re4Version.V2007PS2)
            {
                toolStripMenuItemSaveESE.Text = Lang.GetText(eLang.toolStripMenuItemSaveESE_2007_PS2);
            }
            else if (DataBase.FileESE != null && DataBase.FileESE.GetRe4Version == Re4Version.UHD)
            {
                toolStripMenuItemSaveESE.Text = Lang.GetText(eLang.toolStripMenuItemSaveESE_UHD);
            }
            else
            {
                toolStripMenuItemSaveESE.Text = Lang.GetText(eLang.toolStripMenuItemSaveESE);
            }

        }

        private void toolStripMenuItemSaveESL_Click(object sender, EventArgs e)
        {
            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(Globals.FilePathESL);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                saveFileDialogESL.FileName = Globals.FilePathESL;
                saveFileDialogESL.ShowDialog();
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveFileESL(stream);
                stream.Close();
            }
        }

        private void toolStripMenuItemSaveETS_Click(object sender, EventArgs e)
        {
            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(Globals.FilePathETS);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                saveFileDialogETS.FileName = Globals.FilePathETS;
                saveFileDialogETS.ShowDialog();
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveFileETS(stream);
                stream.Close();
            }
        }

        private void toolStripMenuItemSaveITA_Click(object sender, EventArgs e)
        {
            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(Globals.FilePathITA);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                saveFileDialogITA.FileName = Globals.FilePathITA;
                saveFileDialogITA.ShowDialog();
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveFileITA(stream);
                stream.Close();
            }
        }

        private void toolStripMenuItemSaveAEV_Click(object sender, EventArgs e)
        {
            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(Globals.FilePathAEV);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                saveFileDialogAEV.FileName = Globals.FilePathAEV;
                saveFileDialogAEV.ShowDialog();
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveFileAEV(stream);
                stream.Close();
            }
        }

        private void toolStripMenuItemSaveDSE_Click(object sender, EventArgs e)
        {
            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(Globals.FilePathDSE);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                saveFileDialogDSE.FileName = Globals.FilePathDSE;
                saveFileDialogDSE.ShowDialog();
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveFileDSE(stream);
                stream.Close();
            }
        }

        private void toolStripMenuItemSaveFSE_Click(object sender, EventArgs e)
        {
            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(Globals.FilePathFSE);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                saveFileDialogFSE.FileName = Globals.FilePathFSE;
                saveFileDialogFSE.ShowDialog();
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveFileFSE(stream);
                stream.Close();
            }
        }

        private void toolStripMenuItemSaveSAR_Click(object sender, EventArgs e)
        {
            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(Globals.FilePathSAR);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                saveFileDialogSAR.FileName = Globals.FilePathSAR;
                saveFileDialogSAR.ShowDialog();
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveFileSAR(stream);
                stream.Close();
            }
        }

        private void toolStripMenuItemSaveEAR_Click(object sender, EventArgs e)
        {
            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(Globals.FilePathEAR);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                saveFileDialogEAR.FileName = Globals.FilePathEAR;
                saveFileDialogEAR.ShowDialog();
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveFileEAR(stream);
                stream.Close();
            }
        }

        private void toolStripMenuItemSaveEMI_Click(object sender, EventArgs e)
        {
            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(Globals.FilePathEMI);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                saveFileDialogEMI.FileName = Globals.FilePathEMI;
                saveFileDialogEMI.ShowDialog();
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveFileEMI(stream);
                stream.Close();
            }
        }

        private void toolStripMenuItemSaveESE_Click(object sender, EventArgs e)
        {
            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(Globals.FilePathESE);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                saveFileDialogESE.FileName = Globals.FilePathESE;
                saveFileDialogESE.ShowDialog();
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveFileESE(stream);
                stream.Close();
            }
        }

        private void toolStripMenuItemSaveQuadCustom_Click(object sender, EventArgs e)
        {
            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(Globals.FilePathQuadCustom);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                saveFileDialogQuadCustom.FileName = Globals.FilePathQuadCustom;
                saveFileDialogQuadCustom.ShowDialog();
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveFileQuadCustom(stream);
                stream.Close();
            }
        }


        private void toolStripMenuItemSaveDirectories_DropDownOpening(object sender, EventArgs e)
        {
            toolStripMenuItemDirectory_ESL.Text = Lang.GetText(eLang.DirectoryESL) + " " + (Globals.FilePathESL ?? "");
            toolStripMenuItemDirectory_ETS.Text = Lang.GetText(eLang.DirectoryETS) + " " + (Globals.FilePathETS ?? "");
            toolStripMenuItemDirectory_ITA.Text = Lang.GetText(eLang.DirectoryITA) + " " + (Globals.FilePathITA ?? "");
            toolStripMenuItemDirectory_AEV.Text = Lang.GetText(eLang.DirectoryAEV) + " " + (Globals.FilePathAEV ?? "");
            toolStripMenuItemDirectory_DSE.Text = Lang.GetText(eLang.DirectoryDSE) + " " + (Globals.FilePathDSE ?? "");
            toolStripMenuItemDirectory_FSE.Text = Lang.GetText(eLang.DirectoryFSE) + " " + (Globals.FilePathFSE ?? "");
            toolStripMenuItemDirectory_SAR.Text = Lang.GetText(eLang.DirectorySAR) + " " + (Globals.FilePathSAR ?? "");
            toolStripMenuItemDirectory_EAR.Text = Lang.GetText(eLang.DirectoryEAR) + " " + (Globals.FilePathEAR ?? "");
            toolStripMenuItemDirectory_EMI.Text = Lang.GetText(eLang.DirectoryEMI) + " " + (Globals.FilePathEMI ?? "");
            toolStripMenuItemDirectory_ESE.Text = Lang.GetText(eLang.DirectoryESE) + " " + (Globals.FilePathESE ?? "");
            toolStripMenuItemDirectory_QuadCustom.Text = Lang.GetText(eLang.DirectoryQuadCustom) + " " + (Globals.FilePathQuadCustom ?? "");
        }

        #endregion

        #region Gerenciamento de arquivos //Save Convert

        private void toolStripMenuItemSaveConverter_DropDownOpening(object sender, EventArgs e)
        {
            toolStripMenuItemSaveConverterETS.Enabled = DataBase.FileETS != null;
            toolStripMenuItemSaveConverterITA.Enabled = DataBase.FileITA != null && DataBase.FileITA.IsPs4Ns_Adapted == false;
            toolStripMenuItemSaveConverterAEV.Enabled = DataBase.FileAEV != null && DataBase.FileAEV.IsPs4Ns_Adapted == false;
            toolStripMenuItemSaveConverterEMI.Enabled = DataBase.FileEMI != null;
            toolStripMenuItemSaveConverterESE.Enabled = DataBase.FileESE != null;

            if (DataBase.FileETS != null && DataBase.FileETS.GetRe4Version == Re4Version.V2007PS2)
            {
                toolStripMenuItemSaveConverterETS.Text = Lang.GetText(eLang.toolStripMenuItemSaveConverterETS_UHD);
            }
            else if (DataBase.FileETS != null && DataBase.FileETS.GetRe4Version == Re4Version.UHD)
            {
                toolStripMenuItemSaveConverterETS.Text = Lang.GetText(eLang.toolStripMenuItemSaveConverterETS_2007_PS2);
            }
            else
            {
                toolStripMenuItemSaveConverterETS.Text = Lang.GetText(eLang.toolStripMenuItemSaveConverterETS);
            }

            if (DataBase.FileITA != null && DataBase.FileITA.GetRe4Version == Re4Version.V2007PS2 && DataBase.FileITA.IsPs4Ns_Adapted == false)
            {
                toolStripMenuItemSaveConverterITA.Text = Lang.GetText(eLang.toolStripMenuItemSaveConverterITA_UHD);
            }
            else if (DataBase.FileITA != null && DataBase.FileITA.GetRe4Version == Re4Version.UHD && DataBase.FileITA.IsPs4Ns_Adapted == false)
            {
                toolStripMenuItemSaveConverterITA.Text = Lang.GetText(eLang.toolStripMenuItemSaveConverterITA_2007_PS2);
            }
            else
            {
                toolStripMenuItemSaveConverterITA.Text = Lang.GetText(eLang.toolStripMenuItemSaveConverterITA);
            }

            if (DataBase.FileAEV != null && DataBase.FileAEV.GetRe4Version == Re4Version.V2007PS2 && DataBase.FileAEV.IsPs4Ns_Adapted == false)
            {
                toolStripMenuItemSaveConverterAEV.Text = Lang.GetText(eLang.toolStripMenuItemSaveConverterAEV_UHD);
            }
            else if (DataBase.FileAEV != null && DataBase.FileAEV.GetRe4Version == Re4Version.UHD && DataBase.FileAEV.IsPs4Ns_Adapted == false)
            {
                toolStripMenuItemSaveConverterAEV.Text = Lang.GetText(eLang.toolStripMenuItemSaveConverterAEV_2007_PS2);
            }
            else
            {
                toolStripMenuItemSaveConverterAEV.Text = Lang.GetText(eLang.toolStripMenuItemSaveConverterAEV);
            }

            if (DataBase.FileEMI != null && DataBase.FileEMI.GetRe4Version == Re4Version.V2007PS2)
            {
                toolStripMenuItemSaveConverterEMI.Text = Lang.GetText(eLang.toolStripMenuItemSaveConverterEMI_UHD);
            }
            else if (DataBase.FileEMI != null && DataBase.FileEMI.GetRe4Version == Re4Version.UHD)
            {
                toolStripMenuItemSaveConverterEMI.Text = Lang.GetText(eLang.toolStripMenuItemSaveConverterEMI_2007_PS2);
            }
            else
            {
                toolStripMenuItemSaveConverterEMI.Text = Lang.GetText(eLang.toolStripMenuItemSaveConverterEMI);
            }

            if (DataBase.FileESE != null && DataBase.FileESE.GetRe4Version == Re4Version.V2007PS2)
            {
                toolStripMenuItemSaveConverterESE.Text = Lang.GetText(eLang.toolStripMenuItemSaveConverterESE_UHD);
            }
            else if (DataBase.FileESE != null && DataBase.FileESE.GetRe4Version == Re4Version.UHD)
            {
                toolStripMenuItemSaveConverterESE.Text = Lang.GetText(eLang.toolStripMenuItemSaveConverterESE_2007_PS2);
            }
            else
            {
                toolStripMenuItemSaveConverterESE.Text = Lang.GetText(eLang.toolStripMenuItemSaveConverterESE);
            }
        }

        private void toolStripMenuItemSaveConverterETS_Click(object sender, EventArgs e)
        {
            saveFileDialogConvertETS.FileName = null;
            saveFileDialogConvertETS.ShowDialog();
        }

        private void toolStripMenuItemSaveConverterITA_Click(object sender, EventArgs e)
        {
            saveFileDialogConvertITA.FileName = null;
            saveFileDialogConvertITA.ShowDialog();
        }

        private void toolStripMenuItemSaveConverterAEV_Click(object sender, EventArgs e)
        {
            saveFileDialogConvertAEV.FileName = null;
            saveFileDialogConvertAEV.ShowDialog();
        }

        private void toolStripMenuItemSaveConverterEMI_Click(object sender, EventArgs e)
        {
            saveFileDialogConvertEMI.FileName = null;
            saveFileDialogConvertEMI.ShowDialog();
        }

        private void toolStripMenuItemSaveConverterESE_Click(object sender, EventArgs e)
        {
            saveFileDialogConvertESE.FileName = null;
            saveFileDialogConvertESE.ShowDialog();
        }

        private void saveFileDialogConvertETS_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(saveFileDialogConvertETS.FileName);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveConvertFileETS(stream);
                stream.Close();
            }
        }

        private void saveFileDialogConvertITA_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(saveFileDialogConvertITA.FileName);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveConvertFileITA(stream);
                stream.Close();
            }
        }

        private void saveFileDialogConvertAEV_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(saveFileDialogConvertAEV.FileName);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveConvertFileAEV(stream);
                stream.Close();
            }
        }

        private void saveFileDialogConvertEMI_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(saveFileDialogConvertEMI.FileName);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveConvertFileEMI(stream);
                stream.Close();
            }
        }

        private void saveFileDialogConvertESE_FileOk(object sender, CancelEventArgs e)
        {

            FileInfo file = null;
            FileStream stream = null;
            try
            {
                file = new FileInfo(saveFileDialogConvertESE.FileName);
                stream = file.Create();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Lang.GetText(eLang.MessageBoxErrorTitle), MessageBoxButtons.OK);
                e.Cancel = true;
                return;
            }

            if (file != null && stream != null)
            {
                FileManager.SaveConvertFileESE(stream);
                stream.Close();
            }
        }

        #endregion




        #region MainForm events/ metodos

        bool enable_splitContainerRight_Panel2_Resize = false;

        private void splitContainerRight_Panel2_Resize(object sender, EventArgs e)
        {
            if (enable_splitContainerRight_Panel2_Resize)
            {
                int painel2Width = splitContainerRight.Panel2.Width;
                int quite = painel2Width / 2;

                int adWidth = advertising1Control.Width;
                int adquite = adWidth / 2;

                int ad2Width = advertising2Control.Width;
                int ad2quite = ad2Width / 2;

                if (painel2Width > 670 + advertising2Control.Width)
                {
                    int posX = quite - ad2quite;
                    if (posX < 426)
                    {
                        posX = 426;
                    }
                    advertising1Control.Hide();
                    advertising1Control.Location = new Point(painel2Width, advertising1Control.Location.Y);
                    advertising2Control.Location = new Point(posX, advertising2Control.Location.Y);
                    advertising2Control.Show();
                }
                else if (painel2Width > 670 + advertising1Control.Width)
                {
                    int posX = painel2Width - cameraMove.Width - advertising1Control.Width;

                    advertising2Control.Hide();
                    advertising2Control.Location = new Point(painel2Width, advertising2Control.Location.Y);
                    advertising1Control.Location = new Point(posX, advertising1Control.Location.Y);
                    advertising1Control.Show();
                }
                else
                {
                    advertising1Control.Hide();
                    advertising2Control.Hide();
                    advertising1Control.Location = new Point(painel2Width, advertising1Control.Location.Y);
                    advertising2Control.Location = new Point(painel2Width, advertising2Control.Location.Y);
                }
            }
        }

        private void DarkerGrayTheme()
        {
            //fundo
            treeViewObjs.BackColor = Color.LightGray;
            propertyGridObjs.HelpBackColor = Color.LightGray;
            propertyGridObjs.HelpBorderColor = Color.LightGray;
            propertyGridObjs.ViewBackColor = Color.LightGray;
            splitContainerRight.Panel2.BackColor = Color.LightGray;
            splitContainerRight.Panel1.BackColor = Color.LightGray;
            cameraMove.BackColor = Color.LightGray;
            objectMove.BackColor = Color.LightGray;

            // bordas
            propertyGridObjs.ViewBorderColor = Color.Silver;
            propertyGridObjs.LineColor = Color.Silver;
            propertyGridObjs.BackColor = Color.Silver;
            splitContainerRight.BackColor = Color.Silver;
            splitContainerLeft.BackColor = Color.Silver;
            splitContainerMain.BackColor = Color.Silver;
            propertyGridObjs.CategorySplitterColor = Color.Silver;

            //meu
            menuStripMenu.BackColor = Color.DarkGray;

            advertising1Control.SetDarkerGrayTheme();
            advertising2Control.SetDarkerGrayTheme();
        }

        private void StartUpdateTranslation()
        {
            // menu principal
            toolStripMenuItemFile.Text = Lang.GetText(eLang.toolStripMenuItemFile);
            toolStripMenuItemEdit.Text = Lang.GetText(eLang.toolStripMenuItemEdit);
            toolStripMenuItemView.Text = Lang.GetText(eLang.toolStripMenuItemView);
            toolStripMenuItemMisc.Text = Lang.GetText(eLang.toolStripMenuItemMisc);
            toolStripMenuItemSelectRoom.Text = Lang.GetText(eLang.SelectRoom);
            //submenu File
            toolStripMenuItemNewFile.Text = Lang.GetText(eLang.toolStripMenuItemNewFile);
            toolStripMenuItemOpen.Text = Lang.GetText(eLang.toolStripMenuItemOpen);
            toolStripMenuItemSave.Text = Lang.GetText(eLang.toolStripMenuItemSave);
            toolStripMenuItemSaveAs.Text = Lang.GetText(eLang.toolStripMenuItemSaveAs);
            toolStripMenuItemSaveConverter.Text = Lang.GetText(eLang.toolStripMenuItemSaveConverter);
            toolStripMenuItemClear.Text = Lang.GetText(eLang.toolStripMenuItemClear);
            toolStripMenuItemClose.Text = Lang.GetText(eLang.toolStripMenuItemClose);
            
            // subsubmenu New
            toolStripMenuItemNewESL.Text = Lang.GetText(eLang.toolStripMenuItemNewESL);
            toolStripMenuItemNewETS_2007_PS2.Text = Lang.GetText(eLang.toolStripMenuItemNewETS_2007_PS2);
            toolStripMenuItemNewITA_2007_PS2.Text = Lang.GetText(eLang.toolStripMenuItemNewITA_2007_PS2);
            toolStripMenuItemNewAEV_2007_PS2.Text = Lang.GetText(eLang.toolStripMenuItemNewAEV_2007_PS2);
            toolStripMenuItemNewETS_UHD.Text = Lang.GetText(eLang.toolStripMenuItemNewETS_UHD);
            toolStripMenuItemNewITA_UHD.Text = Lang.GetText(eLang.toolStripMenuItemNewITA_UHD);
            toolStripMenuItemNewAEV_UHD.Text = Lang.GetText(eLang.toolStripMenuItemNewAEV_UHD);
            toolStripMenuItemNewDSE.Text = Lang.GetText(eLang.toolStripMenuItemNewDSE);
            toolStripMenuItemNewFSE.Text = Lang.GetText(eLang.toolStripMenuItemNewFSE);
            toolStripMenuItemNewSAR.Text = Lang.GetText(eLang.toolStripMenuItemNewSAR);
            toolStripMenuItemNewEAR.Text = Lang.GetText(eLang.toolStripMenuItemNewEAR);
            toolStripMenuItemNewEMI_2007_PS2.Text = Lang.GetText(eLang.toolStripMenuItemNewEMI_2007_PS2);
            toolStripMenuItemNewESE_2007_PS2.Text = Lang.GetText(eLang.toolStripMenuItemNewESE_2007_PS2);
            toolStripMenuItemNewEMI_UHD.Text = Lang.GetText(eLang.toolStripMenuItemNewEMI_UHD);
            toolStripMenuItemNewESE_UHD.Text = Lang.GetText(eLang.toolStripMenuItemNewESE_UHD);
            toolStripMenuItemNewQuadCustom.Text = Lang.GetText(eLang.toolStripMenuItemNewQuadCustom);
            toolStripMenuItemNewITA_PS4_NS.Text = Lang.GetText(eLang.toolStripMenuItemNewITA_PS4_NS);
            toolStripMenuItemNewAEV_PS4_NS.Text = Lang.GetText(eLang.toolStripMenuItemNewAEV_PS4_NS);
            // subsubmenu Open
            toolStripMenuItemOpenESL.Text = Lang.GetText(eLang.toolStripMenuItemOpenESL);
            toolStripMenuItemOpenETS_2007_PS2.Text = Lang.GetText(eLang.toolStripMenuItemOpenETS_2007_PS2);
            toolStripMenuItemOpenITA_2007_PS2.Text = Lang.GetText(eLang.toolStripMenuItemOpenITA_2007_PS2);
            toolStripMenuItemOpenAEV_2007_PS2.Text = Lang.GetText(eLang.toolStripMenuItemOpenAEV_2007_PS2);
            toolStripMenuItemOpenETS_UHD.Text = Lang.GetText(eLang.toolStripMenuItemOpenETS_UHD);
            toolStripMenuItemOpenITA_UHD.Text = Lang.GetText(eLang.toolStripMenuItemOpenITA_UHD);
            toolStripMenuItemOpenAEV_UHD.Text = Lang.GetText(eLang.toolStripMenuItemOpenAEV_UHD);
            toolStripMenuItemOpenDSE.Text = Lang.GetText(eLang.toolStripMenuItemOpenDSE);
            toolStripMenuItemOpenFSE.Text = Lang.GetText(eLang.toolStripMenuItemOpenFSE);
            toolStripMenuItemOpenSAR.Text = Lang.GetText(eLang.toolStripMenuItemOpenSAR);
            toolStripMenuItemOpenEAR.Text = Lang.GetText(eLang.toolStripMenuItemOpenEAR);
            toolStripMenuItemOpenEMI_2007_PS2.Text = Lang.GetText(eLang.toolStripMenuItemOpenEMI_2007_PS2);
            toolStripMenuItemOpenESE_2007_PS2.Text = Lang.GetText(eLang.toolStripMenuItemOpenESE_2007_PS2);
            toolStripMenuItemOpenEMI_UHD.Text = Lang.GetText(eLang.toolStripMenuItemOpenEMI_UHD);
            toolStripMenuItemOpenESE_UHD.Text = Lang.GetText(eLang.toolStripMenuItemOpenESE_UHD);
            toolStripMenuItemOpenQuadCustom.Text = Lang.GetText(eLang.toolStripMenuItemOpenQuadCustom);
            toolStripMenuItemOpenITA_PS4_NS.Text = Lang.GetText(eLang.toolStripMenuItemOpenITA_PS4_NS);
            toolStripMenuItemOpenAEV_PS4_NS.Text = Lang.GetText(eLang.toolStripMenuItemOpenAEV_PS4_NS);
            // subsubmenu Save
            toolStripMenuItemSaveESL.Text = Lang.GetText(eLang.toolStripMenuItemSaveESL);
            toolStripMenuItemSaveETS.Text = Lang.GetText(eLang.toolStripMenuItemSaveETS);
            toolStripMenuItemSaveITA.Text = Lang.GetText(eLang.toolStripMenuItemSaveITA);
            toolStripMenuItemSaveAEV.Text = Lang.GetText(eLang.toolStripMenuItemSaveAEV);
            toolStripMenuItemSaveEMI.Text = Lang.GetText(eLang.toolStripMenuItemSaveEMI);
            toolStripMenuItemSaveESE.Text = Lang.GetText(eLang.toolStripMenuItemSaveESE);
            toolStripMenuItemSaveDSE.Text = Lang.GetText(eLang.toolStripMenuItemSaveDSE);
            toolStripMenuItemSaveFSE.Text = Lang.GetText(eLang.toolStripMenuItemSaveFSE);
            toolStripMenuItemSaveSAR.Text = Lang.GetText(eLang.toolStripMenuItemSaveSAR);
            toolStripMenuItemSaveEAR.Text = Lang.GetText(eLang.toolStripMenuItemSaveEAR);
            toolStripMenuItemSaveQuadCustom.Text = Lang.GetText(eLang.toolStripMenuItemSaveQuadCustom);
            toolStripMenuItemSaveDirectories.Text = Lang.GetText(eLang.toolStripMenuItemSaveDirectories);
            // subsubmenu Save As...
            toolStripMenuItemSaveAsESL.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsESL);
            toolStripMenuItemSaveAsETS.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsETS);
            toolStripMenuItemSaveAsITA.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsITA);
            toolStripMenuItemSaveAsAEV.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsAEV);
            toolStripMenuItemSaveAsEMI.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsEMI);
            toolStripMenuItemSaveAsESE.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsESE);
            toolStripMenuItemSaveAsDSE.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsDSE);
            toolStripMenuItemSaveAsFSE.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsFSE);
            toolStripMenuItemSaveAsSAR.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsSAR);
            toolStripMenuItemSaveAsEAR.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsEAR);
            toolStripMenuItemSaveAsQuadCustom.Text = Lang.GetText(eLang.toolStripMenuItemSaveAsQuadCustom);
            // subsubmenu Save As (Convert)
            toolStripMenuItemSaveConverterETS.Text = Lang.GetText(eLang.toolStripMenuItemSaveConverterETS);
            toolStripMenuItemSaveConverterITA.Text = Lang.GetText(eLang.toolStripMenuItemSaveConverterITA);
            toolStripMenuItemSaveConverterAEV.Text = Lang.GetText(eLang.toolStripMenuItemSaveConverterAEV);
            toolStripMenuItemSaveConverterEMI.Text = Lang.GetText(eLang.toolStripMenuItemSaveConverterEMI);
            toolStripMenuItemSaveConverterESE.Text = Lang.GetText(eLang.toolStripMenuItemSaveConverterESE);
            // subsubmenu Clear
            toolStripMenuItemClearESL.Text = Lang.GetText(eLang.toolStripMenuItemClearESL);
            toolStripMenuItemClearETS.Text = Lang.GetText(eLang.toolStripMenuItemClearETS);
            toolStripMenuItemClearITA.Text = Lang.GetText(eLang.toolStripMenuItemClearITA);
            toolStripMenuItemClearAEV.Text = Lang.GetText(eLang.toolStripMenuItemClearAEV);
            toolStripMenuItemClearDSE.Text = Lang.GetText(eLang.toolStripMenuItemClearDSE);
            toolStripMenuItemClearFSE.Text = Lang.GetText(eLang.toolStripMenuItemClearFSE);
            toolStripMenuItemClearSAR.Text = Lang.GetText(eLang.toolStripMenuItemClearSAR);
            toolStripMenuItemClearEAR.Text = Lang.GetText(eLang.toolStripMenuItemClearEAR);
            toolStripMenuItemClearEMI.Text = Lang.GetText(eLang.toolStripMenuItemClearEMI);
            toolStripMenuItemClearESE.Text = Lang.GetText(eLang.toolStripMenuItemClearESE);
            toolStripMenuItemClearQuadCustom.Text = Lang.GetText(eLang.toolStripMenuItemClearQuadCustom);

            // sub menu edit
            toolStripMenuItemAddNewObj.Text = Lang.GetText(eLang.toolStripMenuItemAddNewObj);
            toolStripMenuItemDeleteSelectedObj.Text = Lang.GetText(eLang.toolStripMenuItemDeleteSelectedObj);
            toolStripMenuItemMoveUp.Text = Lang.GetText(eLang.toolStripMenuItemMoveUp);
            toolStripMenuItemMoveDown.Text = Lang.GetText(eLang.toolStripMenuItemMoveDown);
            toolStripMenuItemSearch.Text = Lang.GetText(eLang.toolStripMenuItemSearch);

            // sub menu Misc
            toolStripMenuItemOptions.Text = Lang.GetText(eLang.toolStripMenuItemOptions);
            toolStripMenuItemCredits.Text = Lang.GetText(eLang.toolStripMenuItemCredits);

            // sub menu View
            toolStripMenuItemSubMenuHide.Text = Lang.GetText(eLang.toolStripMenuItemSubMenuHide);
            toolStripMenuItemSubMenuRoom.Text = Lang.GetText(eLang.toolStripMenuItemSubMenuRoom);
            toolStripMenuItemSubMenuModels.Text = Lang.GetText(eLang.toolStripMenuItemSubMenuModels);
            toolStripMenuItemSubMenuEnemy.Text = Lang.GetText(eLang.toolStripMenuItemSubMenuEnemy);
            toolStripMenuItemSubMenuItem.Text = Lang.GetText(eLang.toolStripMenuItemSubMenuItem);
            toolStripMenuItemSubMenuSpecial.Text = Lang.GetText(eLang.toolStripMenuItemSubMenuSpecial);
            toolStripMenuItemSubMenuEtcModel.Text = Lang.GetText(eLang.toolStripMenuItemSubMenuEtcModel);
            toolStripMenuItemNodeDisplayNameInHex.Text = Lang.GetText(eLang.toolStripMenuItemNodeDisplayNameInHex);
            toolStripMenuItemCameraMenu.Text = Lang.GetText(eLang.toolStripMenuItemCameraMenu);
            toolStripMenuItemResetCamera.Text = Lang.GetText(eLang.toolStripMenuItemResetCamera);
            toolStripMenuItemRefresh.Text = Lang.GetText(eLang.toolStripMenuItemRefresh);

            //sub menu hide
            toolStripMenuItemHideRoomModel.Text = Lang.GetText(eLang.toolStripMenuItemHideRoomModel);
            toolStripMenuItemHideEnemyESL.Text = Lang.GetText(eLang.toolStripMenuItemHideEnemyESL);
            toolStripMenuItemHideEtcmodelETS.Text = Lang.GetText(eLang.toolStripMenuItemHideEtcmodelETS);
            toolStripMenuItemHideItemsITA.Text = Lang.GetText(eLang.toolStripMenuItemHideItemsITA);
            toolStripMenuItemHideEventsAEV.Text = Lang.GetText(eLang.toolStripMenuItemHideEventsAEV);
            toolStripMenuItemHideLateralMenu.Text = Lang.GetText(eLang.toolStripMenuItemHideLateralMenu);
            toolStripMenuItemHideBottomMenu.Text = Lang.GetText(eLang.toolStripMenuItemHideBottomMenu);
            toolStripMenuItemHideFileFSE.Text = Lang.GetText(eLang.toolStripMenuItemHideFileFSE);
            toolStripMenuItemHideFileSAR.Text = Lang.GetText(eLang.toolStripMenuItemHideFileSAR);
            toolStripMenuItemHideFileEAR.Text = Lang.GetText(eLang.toolStripMenuItemHideFileEAR);
            toolStripMenuItemHideFileESE.Text = Lang.GetText(eLang.toolStripMenuItemHideFileESE);
            toolStripMenuItemHideFileEMI.Text = Lang.GetText(eLang.toolStripMenuItemHideFileEMI);
            toolStripMenuItemHideQuadCustom.Text = Lang.GetText(eLang.toolStripMenuItemHideQuadCustom);

            // sub menus de view
            toolStripMenuItemHideDesabledEnemy.Text = Lang.GetText(eLang.toolStripMenuItemHideDesabledEnemy);
            toolStripMenuItemShowOnlyDefinedRoom.Text = Lang.GetText(eLang.toolStripMenuItemShowOnlyDefinedRoom);
            toolStripMenuItemAutoDefineRoom.Text = Lang.GetText(eLang.toolStripMenuItemAutoDefineRoom);
            toolStripMenuItemItemPositionAtAssociatedObjectLocation.Text = Lang.GetText(eLang.toolStripMenuItemItemPositionAtAssociatedObjectLocation);
            toolStripMenuItemHideItemTriggerZone.Text = Lang.GetText(eLang.toolStripMenuItemHideItemTriggerZone);
            toolStripMenuItemHideItemTriggerRadius.Text = Lang.GetText(eLang.toolStripMenuItemHideItemTriggerRadius);
            toolStripMenuItemHideSpecialTriggerZone.Text = Lang.GetText(eLang.toolStripMenuItemHideSpecialTriggerZone);
            toolStripMenuItemHideExtraObjs.Text = Lang.GetText(eLang.toolStripMenuItemHideExtraObjs);
            toolStripMenuItemHideOnlyWarpDoor.Text = Lang.GetText(eLang.toolStripMenuItemHideOnlyWarpDoor);
            toolStripMenuItemHideExtraExceptWarpDoor.Text = Lang.GetText(eLang.toolStripMenuItemHideExtraExceptWarpDoor);
            toolStripMenuItemUseMoreSpecialColors.Text = Lang.GetText(eLang.toolStripMenuItemUseMoreSpecialColors);
            toolStripMenuItemEtcModelUseScale.Text = Lang.GetText(eLang.toolStripMenuItemEtcModelUseScale);
            toolStripMenuItemSubMenuQuadCustom.Text = Lang.GetText(eLang.toolStripMenuItemSubMenuQuadCustom);
            toolStripMenuItemUseCustomColors.Text = Lang.GetText(eLang.toolStripMenuItemUseCustomColors);

            //sub menu de view room and model
            toolStripMenuItemModelsHideTextures.Text = Lang.GetText(eLang.toolStripMenuItemModelsHideTextures);
            toolStripMenuItemModelsWireframe.Text = Lang.GetText(eLang.toolStripMenuItemModelsWireframe);
            toolStripMenuItemModelsRenderNormals.Text = Lang.GetText(eLang.toolStripMenuItemModelsRenderNormals);
            toolStripMenuItemModelsOnlyFrontFace.Text = Lang.GetText(eLang.toolStripMenuItemModelsOnlyFrontFace);
            toolStripMenuItemModelsVertexColor.Text = Lang.GetText(eLang.toolStripMenuItemModelsVertexColor);
            toolStripMenuItemModelsAlphaChannel.Text = Lang.GetText(eLang.toolStripMenuItemModelsAlphaChannel);
            toolStripMenuItemRoomHideTextures.Text = Lang.GetText(eLang.toolStripMenuItemRoomHideTextures);
            toolStripMenuItemRoomWireframe.Text = Lang.GetText(eLang.toolStripMenuItemRoomWireframe);
            toolStripMenuItemRoomRenderNormals.Text = Lang.GetText(eLang.toolStripMenuItemRoomRenderNormals);
            toolStripMenuItemRoomOnlyFrontFace.Text = Lang.GetText(eLang.toolStripMenuItemRoomOnlyFrontFace);
            toolStripMenuItemRoomVertexColor.Text = Lang.GetText(eLang.toolStripMenuItemRoomVertexColor);
            toolStripMenuItemRoomAlphaChannel.Text = Lang.GetText(eLang.toolStripMenuItemRoomAlphaChannel);
            toolStripMenuItemRoomTextureNearestLinear.Text = Lang.GetText(eLang.toolStripMenuItemRoomTextureIsLinear);
            toolStripMenuItemModelsTextureNearestLinear.Text = Lang.GetText(eLang.toolStripMenuItemModelsTextureIsLinear);


            //save and open windows
            openFileDialogAEV.Title = Lang.GetText(eLang.openFileDialogAEV);
            openFileDialogESL.Title = Lang.GetText(eLang.openFileDialogESL);
            openFileDialogETS.Title = Lang.GetText(eLang.openFileDialogETS);
            openFileDialogITA.Title = Lang.GetText(eLang.openFileDialogITA);
            openFileDialogDSE.Title = Lang.GetText(eLang.openFileDialogDSE);
            openFileDialogFSE.Title = Lang.GetText(eLang.openFileDialogFSE);
            openFileDialogSAR.Title = Lang.GetText(eLang.openFileDialogSAR);
            openFileDialogEAR.Title = Lang.GetText(eLang.openFileDialogEAR);
            openFileDialogEMI.Title = Lang.GetText(eLang.openFileDialogEMI);
            openFileDialogESE.Title = Lang.GetText(eLang.openFileDialogESE);
            openFileDialogQuadCustom.Title = Lang.GetText(eLang.openFileDialogQuadCustom);

            saveFileDialogConvertAEV.Title = Lang.GetText(eLang.saveFileDialogConvertAEV);
            saveFileDialogConvertETS.Title = Lang.GetText(eLang.saveFileDialogConvertETS);
            saveFileDialogConvertITA.Title = Lang.GetText(eLang.saveFileDialogConvertITA);
            saveFileDialogConvertEMI.Title = Lang.GetText(eLang.saveFileDialogConvertEMI);
            saveFileDialogConvertESE.Title = Lang.GetText(eLang.saveFileDialogConvertESE);

            saveFileDialogAEV.Title = Lang.GetText(eLang.saveFileDialogAEV);
            saveFileDialogESL.Title = Lang.GetText(eLang.saveFileDialogESL);
            saveFileDialogETS.Title = Lang.GetText(eLang.saveFileDialogETS);
            saveFileDialogITA.Title = Lang.GetText(eLang.saveFileDialogITA);
            saveFileDialogDSE.Title = Lang.GetText(eLang.saveFileDialogDSE);
            saveFileDialogFSE.Title = Lang.GetText(eLang.saveFileDialogFSE);
            saveFileDialogSAR.Title = Lang.GetText(eLang.saveFileDialogSAR);
            saveFileDialogEAR.Title = Lang.GetText(eLang.saveFileDialogEAR);
            saveFileDialogEMI.Title = Lang.GetText(eLang.saveFileDialogEMI);
            saveFileDialogESE.Title = Lang.GetText(eLang.saveFileDialogESE);
            saveFileDialogQuadCustom.Title = Lang.GetText(eLang.saveFileDialogQuadCustom);

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (theAppLoadedWell)
            {
                e.Cancel = true;
                if (MessageBox.Show(Lang.GetText(eLang.MessageBoxFormClosingDialog), Lang.GetText(eLang.MessageBoxFormClosingTitle), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    e.Cancel = false;

                    DataBase.ItemsModels?.ClearGL();
                    DataBase.EtcModels?.ClearGL();
                    DataBase.EnemiesModels?.ClearGL();
                    DataBase.InternalModels?.ClearGL();
                    DataBase.QuadCustomModels?.ClearGL();
                    DataBase.SelectedRoom?.ClearGL();
                    DataShader.EndUnload();
                }
            }
        }


        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            // entrada de teclas para açoes especiais
            cameraMove.isControlDown = e.Control;

            #region usado em propery
            // proibe a estrada de caracteres que não vão nos campos de numeros
            if (InPropertyGrid && propertyGridObjs.SelectedGridItem != null && propertyGridObjs.SelectedGridItem.PropertyDescriptor != null)
            {

                if (propertyGridObjs.SelectedGridItem.PropertyDescriptor.Attributes.Contains(new DecNumberAttribute()))
                {

                    e.SuppressKeyPress = true;
                    if (KeysCheck.KeyIsNum(e.KeyCode))
                    {
                        e.SuppressKeyPress = false;
                    }
                    if (e.Control)
                    {
                        e.SuppressKeyPress = false;
                    }
                    if (e.Alt || e.Shift || e.KeyCode == Keys.Alt)
                    {
                        e.SuppressKeyPress = true;
                    }
                    if (KeysCheck.KeyIsEssential(e.KeyCode))
                    {
                        e.SuppressKeyPress = false;
                    }

                }

                if (propertyGridObjs.SelectedGridItem.PropertyDescriptor.Attributes.Contains(new DecNegativeNumberAttribute()))
                {

                    e.SuppressKeyPress = true;
                    if (KeysCheck.KeyIsNum(e.KeyCode))
                    {
                        e.SuppressKeyPress = false;
                    }
                    if (KeysCheck.KeyIsMinus(e.KeyCode))
                    {
                        e.SuppressKeyPress = false;
                    }
                    if (e.Control)
                    {
                        e.SuppressKeyPress = false;
                    }
                    if (e.Alt || e.Shift || e.KeyCode == Keys.Alt)
                    {
                        e.SuppressKeyPress = true;
                    }
                    if (KeysCheck.KeyIsEssential(e.KeyCode))
                    {
                        e.SuppressKeyPress = false;
                    }

                }

                if (propertyGridObjs.SelectedGridItem.PropertyDescriptor.Attributes.Contains(new HexNumberAttribute()))
                {

                    e.SuppressKeyPress = true;
                    if (KeysCheck.KeyIsNum(e.KeyCode))
                    {
                        e.SuppressKeyPress = false;
                    }
                    if (e.Shift)
                    {
                        e.SuppressKeyPress = true;
                    }
                    if (KeysCheck.KeyIsHex(e.KeyCode))
                    {
                        e.SuppressKeyPress = false;
                    }
                    if (e.Control)
                    {
                        e.SuppressKeyPress = false;
                    }
                    if (e.Alt || e.KeyCode == Keys.Alt)
                    {
                        e.SuppressKeyPress = true;
                    }
                    if (KeysCheck.KeyIsEssential(e.KeyCode))
                    {
                        e.SuppressKeyPress = false;
                    }

                }

                if (propertyGridObjs.SelectedGridItem.PropertyDescriptor.Attributes.Contains(new FloatNumberAttribute()))
                {

                    e.SuppressKeyPress = true;
                    if (KeysCheck.KeyIsNum(e.KeyCode))
                    {
                        e.SuppressKeyPress = false;
                    }
                    if (KeysCheck.KeyIsMinus(e.KeyCode))
                    {
                        e.SuppressKeyPress = false;
                    }
                    if (KeysCheck.KeyIsCommaDot(e.KeyCode))
                    {
                        e.SuppressKeyPress = false;
                    }
                    if (KeysCheck.KeyIsOnlyDot(e.KeyValue))
                    {
                        e.SuppressKeyPress = false;
                    }
                    if (e.Control)
                    {
                        e.SuppressKeyPress = false;
                    }
                    if (e.Alt || e.Shift || e.KeyCode == Keys.Alt)
                    {
                        e.SuppressKeyPress = true;
                    }
                    if (KeysCheck.KeyIsEssential(e.KeyCode))
                    {
                        e.SuppressKeyPress = false;
                    }
                }

                if (propertyGridObjs.SelectedGridItem.PropertyDescriptor.Attributes.Contains(new NoKeyAttribute()))
                {
                    e.SuppressKeyPress = true;
                    if (KeysCheck.KeyIsEssentialNoKey(e.KeyCode))
                    {
                        e.SuppressKeyPress = false;
                    }
                }
            }

            #endregion
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            cameraMove.isControlDown = e.Control;
        }

        #endregion

    }
}
