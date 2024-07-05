using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NsCamera;
using Re4QuadExtremeEditor.src.Class.Enums;

namespace Re4QuadExtremeEditor.src.Forms
{
    public partial class CameraForm : Form
    {
        private Camera camera;
        private Class.CustomDelegates.ActivateMethod updateGL;
        private Class.CustomDelegates.ActivateMethod UpdateCameraMatrix;

        public CameraForm(ref Camera camera, Class.CustomDelegates.ActivateMethod updateGL, Class.CustomDelegates.ActivateMethod UpdateCameraMatrix)
        {
            this.camera = camera;
            this.updateGL = updateGL;
            this.UpdateCameraMatrix = UpdateCameraMatrix;
            InitializeComponent();

            GetPos();

            if (camera.CamMode != Camera.CameraMode.FLY)
            {
                floatBoxCamX.ReadOnly = true;
                floatBoxCamY.ReadOnly = true;
                floatBoxCamZ.ReadOnly = true;
                floatBoxPitch.ReadOnly = true;
                floatBoxYaw.ReadOnly = true;
                buttonSetPos.Enabled = false;
                buttonGetPos.Enabled = false;
            }

            if (Lang.LoadedTranslation)
            {
                StartUpdateTranslation();
            }
        }

        private void CameraForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F7)
            {
                Close();
            }
        }


        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GetPos() 
        {
            floatBoxCamX.Value = camera.Position.X;
            floatBoxCamY.Value = camera.Position.Y;
            floatBoxCamZ.Value = camera.Position.Z;
            floatBoxPitch.Value = camera.PitchDegrees;
            floatBoxYaw.Value = camera.YawDegrees;
        }

        private void buttonGetPos_Click(object sender, EventArgs e)
        {
            GetPos();
        }

        private void buttonSetPos_Click(object sender, EventArgs e)
        {
            camera.Position = new OpenTK.Vector3(floatBoxCamX.Value, floatBoxCamY.Value, floatBoxCamZ.Value);
            camera.YawDegrees = floatBoxYaw.Value;
            camera.PitchDegrees = floatBoxPitch.Value;
            UpdateCameraMatrix();
            updateGL();
        }

        private void StartUpdateTranslation()
        {
            Text = Lang.GetText(eLang.CameraForm);
            labelInfo.Text = Lang.GetText(eLang.CameraLabelInfo);
            buttonClose.Text = Lang.GetText(eLang.CameraButtonClose);
            buttonGetPos.Text = Lang.GetText(eLang.CameraButtonGetPos);
            buttonSetPos.Text = Lang.GetText(eLang.CamaraButtonSetPos);
        }
    }
}
