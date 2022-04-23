using Silk.NET.Windowing;
using Silk.NET.Maths;
using Silk.NET.Input;
using Silk.NET.OpenGL.Extensions.ImGui;
using SE3.Graphics.OpenGL;
using System.Numerics;

namespace SE3.Graphics
{
    public class Window
    {
        public readonly IWindow internalWindow;
        protected internal ImGuiController imguiController;
        protected internal IInputContext inputContext;

        public Window(string title, int width, int height, WindowState startState = WindowState.Normal, bool VSync = true)
        {
            GLBackend.window = this;
            WindowOptions options = WindowOptions.Default;
            options.Title = title;
            options.Size = new Vector2D<int>(width, height);
            options.WindowState = startState;
            options.VSync = VSync;
            internalWindow = Silk.NET.Windowing.Window.Create(options);

            internalWindow.Load += OnLoad;
            internalWindow.Update += OnUpdate;
            internalWindow.Render += OnRender;
            internalWindow.Render += OnEndRender;
            internalWindow.Resize += OnResize;
            internalWindow.FramebufferResize += OnFramebufferResize;
            internalWindow.Closing += OnClose;
        }

        public void Run() => internalWindow.Run();
        public void Close() => internalWindow.Close();

        public virtual void OnLoad()
        {
            inputContext = internalWindow.CreateInput();

            foreach(IKeyboard keyboard in inputContext.Keyboards)
                keyboard.KeyDown += OnKeyDown;

            foreach(IMouse mouse in inputContext.Mice)
            {
                mouse.Scroll += OnMouseScroll;
                mouse.MouseMove += OnMouseMove;
            }

            GLBackend.OnLoad();

            imguiController = new ImGuiController(GLBackend.gl, internalWindow, inputContext);
        }

        public virtual void OnRender(double delta)
        {
            imguiController.Update((float)delta);

            GLBackend.OnRender();
        }

        public virtual void OnEndRender(double delta)
        {
            CreateImGuiWindow();
            imguiController.Render();
        }

        public virtual void CreateImGuiWindow() { }

        public virtual void OnResize(Vector2D<int> size)
        {
            GLBackend.gl.Viewport(size);
        }

        public virtual void OnFramebufferResize(Vector2D<int> size) 
        {
            GLBackend.gl.Viewport(size);
        }

        public virtual void OnUpdate(double delta) { }

        public virtual void OnClose() 
        {
            imguiController.Dispose();
            inputContext.Dispose();
            GLBackend.OnClose();
        }

        public virtual void OnKeyDown(IKeyboard keybord, Key key, int arg3) { }

        public virtual void OnMouseMove(IMouse mouse, Vector2 position) { }

        public virtual void OnMouseScroll(IMouse mouse, ScrollWheel scrollwheel) { }
    }
}
