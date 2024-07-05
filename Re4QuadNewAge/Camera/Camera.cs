using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace NsCamera
// basedo em https://github.com/DavidSM64/Quad64/blob/master/src/Viewer/Camera.cs
// e em https://github.com/opentk/LearnOpenTK/blob/master/Common/Camera.cs
{

    public class Camera
    {
        public enum CameraMode : int
        {
            FLY = 0,
            ORBIT = 1,
            LOOK_DIRECTION = 2
        }

        public enum LookDirection : int
        {
            TOP = 0,
            BOTTOM = 1,
            LEFT = 2,
            RIGHT = 3,
            FRONT = 4,
            BACK = 5
        }

        private readonly Vector3[] lookPositions = new Vector3[]
        {
            new Vector3(0, 1640, 0), // top
            new Vector3(0, -1640, 0), // bottom
            new Vector3(-1640, 0, 0), // left
            new Vector3(1640, 0, 0), // right
            new Vector3(0, 0, 1640), // front
            new Vector3(0, 0, -1640)  // back
        };

        private readonly Vector2[] lookEye = new Vector2[] // _pitch, _yaw, in degrees
        {
            new Vector2(0, -180), // top
            new Vector2(0, 180), // bottom
            new Vector2(0, 0), // left
            new Vector2(180, 0), // right
            new Vector2(-90, 0), // front
            new Vector2(90, 0)  // back
        };

        private float camExtraSpeed = 3.0f;
        public float CamExtraSpeed { get { return camExtraSpeed; } set { camExtraSpeed = value; } }

        private float camSpeedMultiplier = 1.0f;
        public float CamSpeedMultiplier { get { return camSpeedMultiplier; } set { camSpeedMultiplier = value; } }

        private readonly Vector3 FixedUp = new Vector3(0, 1, 0);

        private CameraMode camMode = CameraMode.FLY;
        private LookDirection currentLookDirection;

        public CameraMode CamMode { get { return camMode; } }
        public LookDirection CurrentLookDirection { get { return currentLookDirection; } }

        private Vector3 pos = Vector3.Zero;
        private Vector3 lookat = Vector3.Zero;

        private Vector3 savedCamPos = Vector3.Zero;

        public Vector3 Position { get { return pos; } set { pos = value; } } // posição da camera, para ambos os modos
        public Vector3 LookAt { get { return lookat; } set { lookat = value; } } // usado no Quad, modo ORBIT
        public Vector3 SavedCamPos { get { return savedCamPos; } set { savedCamPos = value; } } // posição salva

        // Those vectors are directions pointing outwards from the camera to define how it rotated
        private Vector3 _front = -Vector3.UnitZ; //front back
        private Vector3 _up = Vector3.UnitY; // up down
        private Vector3 _right = Vector3.UnitX; // right left

        public Vector3 Front => _front;
        public Vector3 Up => _up;
        public Vector3 Right => _right;

        /// <summary>
        /// Rotation around the X axis (radians) // CamAngleY
        /// </summary>
        private float _pitch;
        /// <summary>
        /// Rotation around the Y axis (radians) // CamAngleX
        /// </summary>
        private float _yaw = -MathHelper.PiOver2; // Without this you would be started rotated 90 degrees right

        /// <summary>
        /// Rotation around the Y axis (radians) // CamAngleX
        /// </summary>
        public float Yaw { get { return _yaw; } }
        /// <summary>
        /// Rotation around the X axis (radians) // CamAngleY
        /// </summary>
        public float Pitch { get { return _pitch; } }


        // We convert from degrees to radians as soon as the property is set to improve performance
        public float PitchDegrees
        {
            get => MathHelper.RadiansToDegrees(_pitch);
            set
            {
                // We clamp the pitch value between -89 and 89 to prevent the camera from going upside down, and a bunch
                // of weird "bugs" when you are using euler angles for rotation.
                // If you want to read more about this you can try researching a topic called gimbal lock
                var angle = MathHelper.Clamp(value, -89f, 89f);
                _pitch = MathHelper.DegreesToRadians(angle);
                UpdateVectors();
            }
        }

        // We convert from degrees to radians as soon as the property is set to improve performance
        public float YawDegrees
        {
            get => MathHelper.RadiansToDegrees(_yaw);
            set
            {
                _yaw = MathHelper.DegreesToRadians(value);
                UpdateVectors();
            }
        }

        private bool resetMouse = true;
        private int lastMouseX = -1, lastMouseY = -1;

        public void resetMouseStuff()
        {
            resetMouse = true;
        }

        public void SaveCameraPosition()
        {
            savedCamPos = pos;
        }

        private float orbitDistance = 500.0f;
        private float orbitTheta = 0.0f, orbitPhi = 0.0f;

        public bool isOrbitCamera()
        {
            return camMode == CameraMode.ORBIT;
        }


        // // 
        // para ser usado na movimentação do objeto
        private Vector3 moveObj_front = -Vector3.UnitZ; //front back
        private Vector3 moveObj_right = Vector3.UnitX; // right left

        public Vector3 MoveObjFront => moveObj_front;
        public Vector3 MoveObjRight => moveObj_right;

        // //

        public Camera()
        {
            UpdateVectors();
            orientateCam();
        }

        public void ResetCameraToZero()
        {
            camMode = CameraMode.FLY;
            pos = Vector3.Zero;
            _pitch = 0;
            _yaw = -MathHelper.PiOver2;
            UpdateVectors();
            orientateCam();
            resetMouse = true;
        }

        public void SetToFlyMode()
        {
            camMode = CameraMode.FLY;
            UpdateVectors();
            orientateCam();
            resetMouse = true;
        }

        public void SetToOrbitMode()
        {
            camMode = CameraMode.ORBIT;
            ResetOrbitToSelectedObject();
            updateOrbitCamera();
            UpdateVectors();
            resetMouse = true;
        }

        public void setCameraMode_LookDirection(LookDirection dir)
        {
            camMode = CameraMode.LOOK_DIRECTION;
            currentLookDirection = dir;
            pos = lookPositions[(int)dir];
            YawDegrees = lookEye[(int)dir].X;
            PitchDegrees = lookEye[(int)dir].Y;
            orientateCam();
            resetMouse = true;
        }




        public Matrix4 GetViewMatrix()
        {
            if (camMode == CameraMode.ORBIT && getSelectedObject != null && getSelectedObject() != null)
            {
                return Matrix4.LookAt(pos, lookat, FixedUp);
            }
            return Matrix4.LookAt(pos, pos + _front, _up);
        }


        private void UpdateAnglesFromOrbit()
        {
            Vector3 direction = Vector3.Normalize((lookat - pos) / 100f);
            _yaw = (float)Math.Atan2(direction.Z, direction.X);
            _pitch = (float)Math.Asin(direction.Y);
            UpdateVectors();
        }


        // This function is going to update the direction vertices using some of the math learned in the web tutorials
        private void UpdateVectors()
        {
            // First the front matrix is calculated using some basic trigonometry
            _front.X = (float)Math.Cos(_pitch) * (float)Math.Cos(_yaw);
            _front.Y = (float)Math.Sin(_pitch);
            _front.Z = (float)Math.Cos(_pitch) * (float)Math.Sin(_yaw);

            // We need to make sure the vectors are all normalized, as otherwise we would get some funky results
            _front = Vector3.Normalize(_front);

            // Calculate both the right and the up vector using cross product
            // Note that we are calculating the right from the global up, this behaviour might
            // not be what you need for all cameras so keep this in mind if you do not want a FPS camera
            _right = Vector3.Normalize(Vector3.Cross(_front, Vector3.UnitY));
            _up = Vector3.Normalize(Vector3.Cross(_right, _front));

            // para ser usado na movimentação dos objetos
            moveObj_front.X = (float)Math.Cos(_yaw);
            moveObj_front.Y = 0;
            moveObj_front.Z = (float)Math.Sin(_yaw);
            moveObj_front = Vector3.Normalize(moveObj_front);
            moveObj_right = Vector3.Normalize(Vector3.Cross(_front, Vector3.UnitY));
        }

        private void orientateCam() //UpdateVectors
        {
            float CamLX = (float)Math.Cos(_pitch) * (float)Math.Cos(_yaw);
            float CamLY = (float)Math.Sin(_pitch);
            float CamLZ = (float)Math.Cos(_pitch) * (float)Math.Sin(_yaw);
            lookat.X = (pos.X + CamLX) * 100f;
            lookat.Y = (pos.Y + CamLY) * 100f;
            lookat.Z = (pos.Z + CamLZ) * 100f;
        }

        public void updateCameraToUp()
        {
            pos += _up * (camSpeedMultiplier * camExtraSpeed); // Up
        }

        public void updateCameraToDown()
        {
            pos -= _up * (camSpeedMultiplier * camExtraSpeed); // Down
        }

        public void updateCameraToRight()
        {
            pos += _right * (camSpeedMultiplier * camExtraSpeed); // Right
        }

        public void updateCameraToLeft()
        {
            pos -= _right * (camSpeedMultiplier * camExtraSpeed); // Left
        }

        public void updateCameraToFront()
        {
            pos += _front * (camSpeedMultiplier * camExtraSpeed); // Forward 
        }

        public void updateCameraToBack()
        {
            pos -= _front * (camSpeedMultiplier * camExtraSpeed); // Backwards
        }

        public void updateCameraOffsetMatrixWithMouse(bool isControlDown, int mouseX, int mouseY, bool invert = false)
        {
            if (camMode == CameraMode.ORBIT && getSelectedObject != null && getSelectedObject() != null)
            {
                updateCameraOffsetWithMouse_ORBIT(mouseX, mouseY, invert);
            }
            else if (camMode == CameraMode.LOOK_DIRECTION || isControlDown)
            {
                updateCameraOffsetWithMouse_LOOK(mouseX, mouseY, invert, isControlDown);
            }
            else
            {
                updateCameraMatrixWithMouse_FLY(mouseX, mouseY, invert);
            }
        }

        private void updateCameraMatrixWithMouse_FLY(int mouseX, int mouseY, bool invert)
        {
            if (resetMouse)
            {
                lastMouseX = mouseX;
                lastMouseY = mouseY;
                resetMouse = false;
            }
            else
            {
                int MousePosX = mouseX - lastMouseX; //deltaX
                int MousePosY = mouseY - lastMouseY; //deltaY
                lastMouseX = mouseX;
                lastMouseY = mouseY;

                if (invert)
                {
                    MousePosX = -MousePosX;
                    MousePosY = -MousePosY;
                }

                const float sensitivity = 0.2f;
                //Apply the camera pitch and yaw (we clamp the pitch in the camera class)
                YawDegrees += MousePosX * sensitivity;
                PitchDegrees -= MousePosY * sensitivity; // reversed since y-coordinates range from bottom to top
            }

        }

        private void updateCameraOffsetWithMouse_LOOK(int mouseX, int mouseY, bool invert, bool isControlDown)
        {
            if (resetMouse)
            {
                lastMouseX = mouseX;
                lastMouseY = mouseY;
                resetMouse = false;
            }
            int MousePosX = (-mouseX) + lastMouseX;
            int MousePosY = (-mouseY) + lastMouseY;

            float controlDownExtraSpeed = 1f;
            if (camMode == CameraMode.LOOK_DIRECTION && !isControlDown)
            {
                controlDownExtraSpeed = 4f;
            }

            if (invert)
            {
                MousePosX = -MousePosX;
                MousePosY = -MousePosY;
            }

            float sensitivity = 0.2f * controlDownExtraSpeed * (camSpeedMultiplier * camExtraSpeed);
            pos = savedCamPos + (-_right * (MousePosX) * sensitivity) + (_up * (MousePosY) * sensitivity);
        }

        private void updateCameraOffsetWithMouse_ORBIT(int mouseX, int mouseY, bool invert)
        {
            if (resetMouse)
            {
                lastMouseX = mouseX;
                lastMouseY = mouseY;
                resetMouse = false;
            }
            int MousePosX = (-mouseX) + lastMouseX;
            int MousePosY = (-mouseY) + lastMouseY;
            lastMouseX = mouseX;
            lastMouseY = mouseY;
            if (invert)
            {
                MousePosX = -MousePosX;
                MousePosY = -MousePosY;
            }
            orbitTheta += MousePosX * 0.01f * camSpeedMultiplier;
            orbitPhi -= MousePosY * 0.01f * camSpeedMultiplier;
            orbitPhi = MathHelper.Clamp(orbitPhi, -1.57f, 1.57f);
            updateOrbitCamera();
        }

        public void updateCameraMatrixWithScrollWheel(int amt, bool invert = false)
        {
            if (invert)
            {
                amt = -amt;
            }
            if (camMode == CameraMode.ORBIT && getSelectedObject != null && getSelectedObject() != null)
            {
                updateCameraMatrixWithScrollWheel_ORBIT(amt);
            }
            else
            {
                updateCameraMatrixWithScrollWheel_FLY(amt);
            }
        }

        private void updateCameraMatrixWithScrollWheel_FLY(int amt)
        {
            pos += _front * amt * camSpeedMultiplier;
        }

        private void updateCameraMatrixWithScrollWheel_ORBIT(int amt)
        {
            orbitDistance -= amt * 0.2f * camSpeedMultiplier;
            if (orbitDistance < 10.0f)
            { orbitDistance = 10.0f; }
            updateOrbitCamera();
        }

        public Func<IObject3D> getSelectedObject;

        public float SelectedObjPosY()
        {
            if (getSelectedObject == null) { return 0; }
            IObject3D obj = getSelectedObject();
            if (obj == null) { return 0; }
            return obj.GetObjPosition_ToCamera().Y;
        }

        private void updateOrbitCamera()
        {
            if (camMode == CameraMode.ORBIT)
            {
                if (getSelectedObject == null) { return; }
                IObject3D obj = getSelectedObject();
                if (obj == null) { return; }
                Vector3 objPos = obj.GetObjPosition_ToCamera();
                pos.X = objPos.X + (float)(Math.Cos(orbitPhi) * -Math.Sin(orbitTheta) * orbitDistance);
                pos.Y = objPos.Y + (float)(-Math.Sin(orbitPhi) * orbitDistance);
                pos.Z = objPos.Z + (float)(Math.Cos(orbitPhi) * Math.Cos(orbitTheta) * orbitDistance);
                lookat.X = objPos.X;
                lookat.Y = objPos.Y;
                lookat.Z = objPos.Z;
                UpdateAnglesFromOrbit();
            }

        }

        public void ResetOrbitToSelectedObject()
        {
            if (getSelectedObject == null) { return; }
            IObject3D obj = getSelectedObject();
            if (obj != null)
            {
                orbitTheta = -obj.GetObjAngleY_ToCamera();
                orbitPhi = -0.30f;
                orbitDistance = 100.0f;
            }
        }


        public void UpdateCameraOrbitOnChangeObj()
        {
            if (camMode == CameraMode.ORBIT && getSelectedObject != null && getSelectedObject() != null)
            {
                //ResetOrbitToSelectedObject();
                updateOrbitCamera();
                UpdateVectors();
                resetMouse = true;
            }
        }

        public void UpdateCameraOrbitOnChangeValue()
        {
            if (camMode == CameraMode.ORBIT && getSelectedObject != null && getSelectedObject() != null)
            {
                updateOrbitCamera();
                UpdateVectors();
                resetMouse = true;
            }
        }

    }

    public interface IObject3D
    {
        Vector3 GetObjPosition_ToCamera();
        float GetObjAngleY_ToCamera();
    }

}
