using SE3.Graphics;
using SE3.Graphics.OpenGL;
using SE3.Components;
using System.Numerics;
using Silk.NET.Input;

namespace SE3Examples
{
    internal class WindowTest : Window
    {
        private static BufferObject<float> VBO;
        private static BufferObject<uint> EBO;
        private static VAO<float, uint> VAO;
        private static Shader Shader;
        private static Texture Texture;
        private static Transform Transform;
        private static Camera Camera;

        private static readonly float[] Vertices =
        {
            //X    Y      Z     U   V
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,
             0.5f, -0.5f, -0.5f,  1.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 1.0f,

            -0.5f, -0.5f,  0.5f,  0.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 1.0f,

            -0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  1.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  1.0f, 1.0f,

             0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  0.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 1.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,
             0.5f, -0.5f, -0.5f,  1.0f, 0.0f,
             0.5f, -0.5f,  0.5f,  1.0f, 1.0f,
             0.5f, -0.5f,  0.5f,  1.0f, 1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, 1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, 0.0f,

            -0.5f,  0.5f, -0.5f,  0.0f, 0.0f,
             0.5f,  0.5f, -0.5f,  1.0f, 0.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
             0.5f,  0.5f,  0.5f,  1.0f, 1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f, 1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f, 0.0f
        };

        private static readonly uint[] Indices =
        {
            0, 1, 3,
            1, 2, 3
        };

        public WindowTest(string title, int width, int height) : base(title, width, height)
        {}

        public override void OnLoad()
        {
            base.OnLoad();

            EBO = new BufferObject<uint>(Indices, Silk.NET.OpenGL.BufferTargetARB.ElementArrayBuffer);
            VBO = new BufferObject<float>(Vertices, Silk.NET.OpenGL.BufferTargetARB.ArrayBuffer);
            VAO = new VAO<float, uint>(VBO, EBO);

            VAO.VertexAttributePointer(0, 3, Silk.NET.OpenGL.VertexAttribPointerType.Float, 5, 0);
            VAO.VertexAttributePointer(1, 2, Silk.NET.OpenGL.VertexAttribPointerType.Float, 5, 3);

            Shader = Shader.FromFiles("shader.vert", "shader.frag");
            Texture = new Texture("container.png");
            Transform = new Transform();
            Camera = new Camera();
        }

        public override void OnRender(double delta)
        {
            base.OnRender(delta);

            VAO.Bind();
            Shader.Use();
            Texture.Bind(Silk.NET.OpenGL.TextureUnit.Texture0);
            Shader.SetUniform("uTexture0", 0);

            float difference = (float)(internalWindow.Time);
            Transform.Rotation = Quaternion.CreateFromYawPitchRoll(difference, difference, 0);

            Matrix4x4 model = Transform.ViewMatrix;
            Matrix4x4 view = Camera.GetViewMatrix();
            Matrix4x4 projection = Camera.GetProjectionMatrix(internalWindow.Size.X, internalWindow.Size.Y);

            Shader.SetUniform("uModel", model);
            Shader.SetUniform("uView", view);
            Shader.SetUniform("uProjection", projection);

            GLBackend.DrawArrays(Silk.NET.OpenGL.PrimitiveType.Triangles, 0, 36);
        }

        public override void OnClose()
        {
            base.OnClose();

            VBO.Dispose();
            EBO.Dispose();
            VAO.Dispose();
            Shader.Dispose();
            Texture.Dispose();
        }

        public override void OnKeyDown(IKeyboard keybord, Key key, int arg3)
        {
            base.OnKeyDown(keybord, key, arg3);

            if (key == Key.Escape)
                Close();

            if (key == Key.Up)
                Camera.SetPosition(Camera.GetPosition() + Camera.GetFront() * 2f);
            if (key == Key.Down)
                Camera.SetPosition(Camera.GetPosition() - Camera.GetFront() * 2f);
            if (key == Key.Right)
                Camera.SetPosition(Camera.GetPosition() + Camera.GetRight() * 2f);
            if (key == Key.Left)
                Camera.SetPosition(Camera.GetPosition() - Camera.GetRight() * 2f);
        }

        public override void OnMouseScroll(IMouse mouse, ScrollWheel scrollwheel)
        {
            base.OnMouseScroll(mouse, scrollwheel);

            Camera.SetZoom(Math.Clamp(Camera.GetZoom() - scrollwheel.Y, 1f, 45f));
        }
    }
}
