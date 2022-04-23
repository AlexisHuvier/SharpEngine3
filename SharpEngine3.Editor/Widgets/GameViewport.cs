using SE3.Graphics.OpenGL;
using ImGuiNET;
using System.Numerics;
using Silk.NET.OpenGL;

namespace SE3Editor.Widgets
{
    internal class GameViewport
    {
        private static uint tex;
        private static uint drb;
        private static uint fb;

        public unsafe static void OnLoad()
        {
            Utils.GameTesting.OnLoad();

            fb = GLBackend.gl.GenFramebuffer();
            GLBackend.gl.BindFramebuffer(FramebufferTarget.Framebuffer, fb);

            tex = GLBackend.gl.GenTexture();
            GLBackend.gl.BindTexture(TextureTarget.Texture2D, tex);
            GLBackend.gl.TexImage2D(GLEnum.Texture2D, 0, InternalFormat.Rgba, 800, 600, 0, GLEnum.Rgba, GLEnum.UnsignedByte, null);
            GLBackend.gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)GLEnum.Linear);
            GLBackend.gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)GLEnum.Linear);
            GLBackend.gl.FramebufferTexture2D(GLEnum.Framebuffer, GLEnum.ColorAttachment0, GLEnum.Texture2D, tex, 0);

            drb = GLBackend.gl.GenRenderbuffer();
            GLBackend.gl.BindRenderbuffer(RenderbufferTarget.Renderbuffer, drb);
            GLBackend.gl.RenderbufferStorage(RenderbufferTarget.Renderbuffer, GLEnum.DepthComponent, 800, 600);
            GLBackend.gl.FramebufferRenderbuffer(GLEnum.Framebuffer, GLEnum.DepthAttachment, GLEnum.Renderbuffer, drb);

            GLEnum[] drawBuffers = new GLEnum[1] { GLEnum.ColorAttachment0 };
            fixed (GLEnum* ptr = drawBuffers)
            GLBackend.gl.DrawBuffers(1, ptr);
        }

        public static void OnRender(Editor editor)
        {
            var lastSize = editor.internalWindow.FramebufferSize;
            GLBackend.gl.BindFramebuffer(FramebufferTarget.Framebuffer, fb);

            GLBackend.gl.Enable(EnableCap.DepthTest);
            GLBackend.gl.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GLBackend.gl.Clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));
            GLBackend.gl.Viewport(0, 0, 800, 600);

            Utils.GameTesting.OnRender(editor.internalWindow.Time, editor.internalWindow.Size);

            GLBackend.gl.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
            GLBackend.gl.Viewport(lastSize);
        }

        public static void CreateImGuiWindow(Editor editor)
        {
            ImGui.SetNextWindowPos(new Vector2((editor.internalWindow.FramebufferSize.X - editor.internalWindow.FramebufferSize.X * 0.5f) / 2f, 20));
            ImGui.SetNextWindowSize(new Vector2(editor.internalWindow.FramebufferSize.X * 0.5f, editor.internalWindow.FramebufferSize.Y * 0.65f));

            ImGui.Begin(Resources.strings.Viewport, ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse);
            ImGui.BeginChild("GameRender");
            Vector2 wsize = ImGui.GetWindowSize();
            ImGui.Image(new IntPtr(tex), wsize);
            ImGui.EndChild();
            ImGui.End();
        }
    }
}
