using System.Numerics;
using Avalonia.Automation.Provider;
using Avalonia.Input;
using Avalonia.OpenGL.Controls;

namespace Tutorial14
{
    partial class OpenGlControl
    {
        const float cameraPositionStep = 1.1f;
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.Key)
            {
                case Key.Up:
                    CameraPosition += CameraTarget * cameraPositionStep;
                    e.Handled = true;
                    break;
                case Key.Down:
                    CameraPosition -= CameraTarget * cameraPositionStep;
                    e.Handled = true;
                    break;
                case Key.Left:
                    Vector3 left = Vector3.Cross(CameraTarget, CameraUp);
                    left = Vector3.Normalize(left);
                    left *= cameraPositionStep;
                    CameraPosition += left;
                    e.Handled = true;
                    break;
                case Key.Right:
                    Vector3 right = Vector3.Cross(CameraUp, CameraTarget);
                    right = Vector3.Normalize(right);
                    right *= cameraPositionStep;
                    CameraPosition += right;
                    e.Handled = true;
                    break;
            }
            
            // switch (Key) {
            //
            //     case OGLDEV_KEY_UP:
            //     {
            //         m_pos += (m_target * STEP_SCALE);
            //         Ret = true;
            //     }
            //         break;
            //
            //     case OGLDEV_KEY_DOWN:
            //     {
            //         m_pos -= (m_target * STEP_SCALE);
            //         Ret = true;
            //     }
            //         break;
            //
            //     case OGLDEV_KEY_LEFT:
            //     {
            //         Vector3f Left = m_target.Cross(m_up);
            //         Left.Normalize();
            //         Left *= STEP_SCALE;
            //         m_pos += Left;
            //         Ret = true;
            //     }
            //         break;
            //
            //     case OGLDEV_KEY_RIGHT:
            //     {
            //         Vector3f Right = m_up.Cross(m_target);
            //         Right.Normalize();
            //         Right *= STEP_SCALE;
            //         m_pos += Right;
            //         Ret = true;
            //     }
            //         break;
            //
            //     case OGLDEV_KEY_PAGE_UP:
            //         m_pos.y += STEP_SCALE;
            //         break;
            //
            //     case OGLDEV_KEY_PAGE_DOWN:
            //         m_pos.y -= STEP_SCALE;
            //         break;
            //
            //     default:
            //         break;            
            // }

        }
    }
}