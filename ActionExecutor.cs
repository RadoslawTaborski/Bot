using Clicker.Enums;
using Clicker.Models.Actions;
using System.Runtime.InteropServices;

namespace Clicker;

public class ActionExecutor
{
    private const int LEFT_DOWN = 0x0002;
    private const int LEFT_UP = 0x0004;
    private const int RIGHT_DOWN = 0x0008;
    private const int RIGHT_UP = 0x0010;
    private const int MIDDLE_DOWN = 0x0020;
    private const int MIDDLE_UP = 0x0040;
    private const int MOVE = 0x0001;
    private const int ABSOLUTE = 0x8000;
    private const int KEYUP = 0x0002;
    private const int VK_SHIFT = 0x10;

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int x, int y);

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    public static extern void keybd_event(uint bVk, uint bScan, uint dwFlags, uint dwExtraInfo);

    [DllImport("user32.dll")]
    static extern short VkKeyScan(char ch);

    public int Execute(Models.Actions.Action action, Models.Actions.Action next = null)
    {
        var result = 0;
        switch (action)
        {
            case KeyboardAction keyboard:
                foreach (char c in keyboard.Text)
                {
                    var vk = VkKeyScan(c);
                    bool shift = (vk & 0x0100) != 0;

                    if (shift)
                        keybd_event(VK_SHIFT, 0, 0, 0);

                    keybd_event((byte)(vk & 0xFF), 0, 0, 0);
                    keybd_event((byte)(vk & 0xFF), 0, KEYUP, 0);

                    if (shift)
                        keybd_event(VK_SHIFT, 0, KEYUP, 0);
                }
                result++;
                break;

            case MouseAction mouse:
                SetCursorPos(mouse.Point.X, mouse.Point.Y);

                if (mouse.Button.Equals(MouseActions.Left))
                    mouse_event(LEFT_DOWN | LEFT_UP, 0, 0, 0, 0);
                if (mouse.Button.Equals(MouseActions.Right))
                    mouse_event(RIGHT_DOWN | RIGHT_UP, 0, 0, 0, 0);
                if (mouse.Button.Equals(MouseActions.Middle))
                    mouse_event(MIDDLE_DOWN | MIDDLE_UP, 0, 0, 0, 0);
                if (mouse.Button.Equals(MouseActions.Left_Down))
                {
                    mouse_event(LEFT_DOWN, 0, 0, 0, 0);
                    if (next != null && next is MouseAction nextMouse && nextMouse.Button.Equals(MouseActions.Left_Up))
                    {
                        int absX = nextMouse.Point.X * 65535 / Screen.PrimaryScreen.Bounds.Width;
                        int absY = nextMouse.Point.Y * 65535 / Screen.PrimaryScreen.Bounds.Height;
                        Thread.Sleep(mouse.Period);
                        mouse_event(MOVE | ABSOLUTE, (uint)absX, (uint)absY, 0, 0);
                        Thread.Sleep(nextMouse.Period);
                        mouse_event(LEFT_UP, 0, 0, 0, 0);
                    }
                    result++;
                }
                result++;
                break;
            case PauseAction _:
                result++;
                break;

            default:
                MessageBox.Show("Nieznany typ akcji", "BLAD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                break;
        }

        return result;
    }
}
