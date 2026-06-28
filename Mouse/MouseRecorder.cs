using Clicker.Enums;
using Gma.System.MouseKeyHook;

namespace Clicker.Mouse;

public delegate void MouseDown(MouseRecorderItem item);

public class MouseRecorder(MouseDown mouseDown)
{
    private event MouseDown MouseDown = mouseDown;

    private IKeyboardMouseEvents? _globalHook;

    public void Start()
    {
        _globalHook = Hook.GlobalEvents();
        _globalHook.MouseDownExt += GlobalHook_MouseDownExt;
    }

    public void Stop()
    {
        _globalHook?.MouseDownExt -= GlobalHook_MouseDownExt;
        _globalHook?.Dispose();
    }

    private void GlobalHook_MouseDownExt(object? sender, MouseEventExtArgs e)
    {
        Point point = new(Cursor.Position.X, Cursor.Position.Y);
        MouseActions button = e.Button == MouseButtons.Middle
                    ? MouseActions.Middle
                    : e.Button == MouseButtons.Right
                        ? MouseActions.Right
                        : MouseActions.Left;
        MouseRecorderItem recordItem = new(point, button);

        MouseDown(recordItem);
    }
}
