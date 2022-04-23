using Silk.NET.OpenGL;

namespace SE3.Graphics.OpenGL
{
    public class GLBackend
    {
        public static GL gl;
        internal static Window window;

        public static unsafe void OnLoad()
        {
            gl = GL.GetApi(window.internalWindow);
            Constants.GL_VERSION = gl.GetStringS(GLEnum.Version);
            Constants.GLSL_VERSION = gl.GetStringS(GLEnum.ShadingLanguageVersion);
            Constants.VENDOR_NAME = gl.GetStringS(GLEnum.Vendor);
            Constants.RENDER_NAME = gl.GetStringS(GLEnum.Renderer);
        }

        public static unsafe void OnRender()
        {
            gl.Enable(EnableCap.DepthTest);
            gl.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
            gl.Clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));
        }

        public static unsafe void OnClose() => gl.Dispose();

        public static unsafe void DrawElements(PrimitiveType primitiveType, uint count, DrawElementsType drawElementsType) => gl.DrawElements(primitiveType, count, drawElementsType, null);
        public static void DrawArrays(PrimitiveType primitiveType, int first, uint count) => gl.DrawArrays(primitiveType, first, count);
    }
}
