using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Re4QuadExtremeEditor.src.Class;
using Re4QuadExtremeEditor.src.Class.Enums;
using NsCamera;

namespace Re4QuadExtremeEditor.src.Controls
{
    public partial class CameraMoveControl : UserControl
    {
        private Camera camera;

        private Class.CustomDelegates.ActivateMethod updateGL;
        private Class.CustomDelegates.ActivateMethod UpdateCameraMatrix;

        private bool CameraModeChangedIsEneable = false;

        public CameraMoveControl(ref Camera camera, Class.CustomDelegates.ActivateMethod updateGL, Class.CustomDelegates.ActivateMethod UpdateCameraMatrix)
        {
            this.camera = camera;
            this.updateGL = updateGL;
            this.UpdateCameraMatrix = UpdateCameraMatrix;
            InitializeComponent();
            comboBoxCameraMode.SelectedIndex = 0;
            CameraModeChangedIsEneable = true;

            comboBoxCameraMode.MouseWheel += ComboBoxCameraMode_MouseWheel;
            trackBarCamSpeed.MouseWheel += TrackBarCamSpeed_MouseWheel;
        }

        public void ResetCamera() 
        {
            CameraModeChangedIsEneable = false;
            comboBoxCameraMode.SelectedIndex = 0;
            camera.ResetCameraToZero();
            CameraModeChangedIsEneable = true;
            UpdateCameraMatrix();
            updateGL();
        }


        private void TrackBarCamSpeed_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
            if (e.X >= 0 && e.Y >= 0 && e.X < trackBarCamSpeed.Width && e.Y < trackBarCamSpeed.Height)
            {
                ((HandledMouseEventArgs)e).Handled = false;
            }
        }

        private void ComboBoxCameraMode_MouseWheel(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = !((ComboBox)sender).DroppedDown;
        }

        private void trackBarCamSpeed_Scroll(object sender, EventArgs e)
        {
            float newValue;
            if (trackBarCamSpeed.Value > 50)
            { newValue = 100.0f + ((trackBarCamSpeed.Value - 50) * 8f); }
            else
            { newValue = (trackBarCamSpeed.Value / 50.0f) * 100f; }
            if (newValue < 1f)
            { newValue = 1f; }
            else if (newValue > 96f && newValue < 114f)
            { newValue = 100f; }

            labelCamSpeedPercentage.Text = Lang.GetText(eLang.labelCamSpeedPercentage) +" "+ ((int)newValue).ToString().PadLeft(3) + "%";
            camera.CamSpeedMultiplier = newValue / 100.0f;
        }

        private void comboBoxCameraMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CameraModeChangedIsEneable)
            {
                switch (comboBoxCameraMode.SelectedIndex)
                {
                    case 0: // Fly
                        camera.SetToFlyMode();

                        break;
                    case 1: // Orbit
                        camera.SetToOrbitMode();
                        break;
                    case 2: // Top
                        camera.setCameraMode_LookDirection(Camera.LookDirection.TOP);
                        break;
                    case 3: // Bottom
                        camera.setCameraMode_LookDirection(Camera.LookDirection.BOTTOM);
                        break;
                    case 4: // Left
                        camera.setCameraMode_LookDirection(Camera.LookDirection.LEFT);
                        break;
                    case 5: // Right
                        camera.setCameraMode_LookDirection(Camera.LookDirection.RIGHT);
                        break;
                    case 6: // Front
                        camera.setCameraMode_LookDirection(Camera.LookDirection.FRONT);
                        break;
                    case 7: // Back
                        camera.setCameraMode_LookDirection(Camera.LookDirection.BACK);
                        break;
                }
                UpdateCameraMatrix();
                updateGL();
            }
        }

        private void buttonGrid_Click(object sender, EventArgs e)
        {
            Globals.CamGridEnable = !Globals.CamGridEnable;
            updateGL();
        }

        private void textBoxGridSize_TextChanged(object sender, EventArgs e)
        {
            int value = 100;
            if (int.TryParse(textBoxGridSize.Text, out value))
            {
                Globals.CamGridvalue = value;
                updateGL();
            }
            else 
            {
                Globals.CamGridvalue = 100;
                textBoxGridSize.Text = "100";
                updateGL();
            }
        }

        private void textBoxGridSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                if (textBoxGridSize.SelectionStart < textBoxGridSize.TextLength)
                {
                    int CacheSelectionStart = textBoxGridSize.SelectionStart;
                    StringBuilder sb = new StringBuilder(textBoxGridSize.Text);
                    sb[textBoxGridSize.SelectionStart] = e.KeyChar;
                    textBoxGridSize.Text = sb.ToString();
                    textBoxGridSize.SelectionStart = CacheSelectionStart + 1;
                }
            }
            e.Handled = true;
        }

        bool moveCam_Invert = false;
        int moveCam_InOut_lastPosY = 0;
        bool moveCam_InOut_mouseDown = false;
        bool moveCam_strafe_mouseDown = false;

        public bool isControlDown = false;

        private void pictureBoxMoveCamStrafe_MouseMove(object sender, MouseEventArgs e)
        {
            if (moveCam_strafe_mouseDown)
            {
                camera.updateCameraOffsetMatrixWithMouse(isControlDown, e.X, e.Y, moveCam_Invert);
                UpdateCameraMatrix();
                updateGL();
            }
        }

        private void pictureBoxMoveCamStrafe_MouseUp(object sender, MouseEventArgs e)
        {
            camera.resetMouseStuff();
            camera.SaveCameraPosition();
            moveCam_strafe_mouseDown = false;
            moveCam_Invert = false;
        }

        private void pictureBoxMoveCamStrafe_MouseDown(object sender, MouseEventArgs e)
        {
            camera.resetMouseStuff();
            camera.SaveCameraPosition();
            moveCam_strafe_mouseDown = true;
            if (e.Button == MouseButtons.Right)
            {
                moveCam_Invert = true;
            }
            if (e.Button == MouseButtons.Left)
            {
                moveCam_Invert = false;
            }
        }

        private void pictureBoxMoveCamStrafe_MouseLeave(object sender, EventArgs e)
        {
            camera.resetMouseStuff();
            moveCam_strafe_mouseDown = false;
            moveCam_Invert = false;
        }


    
        private void pictureBoxMoveCamInOut_MouseMove(object sender, MouseEventArgs e)
        {
            if (moveCam_InOut_mouseDown)
            {
                camera.resetMouseStuff();
                camera.updateCameraMatrixWithScrollWheel((e.Y - moveCam_InOut_lastPosY) * -10, moveCam_Invert);
                camera.SaveCameraPosition();
                moveCam_InOut_lastPosY = e.Y;
                UpdateCameraMatrix();
                updateGL();
            }
        }

        private void pictureBoxMoveCamInOut_MouseUp(object sender, MouseEventArgs e)
        {
            moveCam_InOut_mouseDown = false;
            moveCam_Invert = false;
        }

        private void pictureBoxMoveCamInOut_MouseDown(object sender, MouseEventArgs e)
        {
            moveCam_InOut_mouseDown = true;
            moveCam_InOut_lastPosY = e.Y;
            if (e.Button == MouseButtons.Right)
            {
                moveCam_Invert = true;    
            }
            if (e.Button == MouseButtons.Left)
            {
                moveCam_Invert = false;
            }
        }

        private void pictureBoxMoveCamInOut_MouseLeave(object sender, EventArgs e)
        {
            moveCam_InOut_mouseDown = false;
        }


        public void StartUpdateTranslation()
        {
            labelCamSpeedPercentage.Text = Lang.GetText(eLang.labelCamSpeedPercentage) + " 100%";
            buttonGrid.Text = Lang.GetText(eLang.buttonGrid);
            labelCamModeText.Text = Lang.GetText(eLang.labelCamModeText);
            labelMoveCamText.Text = Lang.GetText(eLang.labelMoveCamText);
            CameraModeChangedIsEneable = false;
            comboBoxCameraMode.Items[0] = Lang.GetText(eLang.CameraMode_Fly);
            comboBoxCameraMode.Items[1] = Lang.GetText(eLang.CameraMode_Orbit);
            comboBoxCameraMode.Items[2] = Lang.GetText(eLang.CameraMode_Top);
            comboBoxCameraMode.Items[3] = Lang.GetText(eLang.CameraMode_Bottom);
            comboBoxCameraMode.Items[4] = Lang.GetText(eLang.CameraMode_Left);
            comboBoxCameraMode.Items[5] = Lang.GetText(eLang.CameraMode_Right);
            comboBoxCameraMode.Items[6] = Lang.GetText(eLang.CameraMode_Front);
            comboBoxCameraMode.Items[7] = Lang.GetText(eLang.CameraMode_Back);
            comboBoxCameraMode.SelectedIndex = 0;
            CameraModeChangedIsEneable = true;
        }

    }
}
