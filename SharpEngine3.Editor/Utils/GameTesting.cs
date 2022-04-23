using SE3.Graphics.OpenGL;
using SE3.Components;
using System.Numerics;

namespace SE3Editor.Utils
{
    internal class GameTesting
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

        public static void OnLoad()
        {
            EBO = new BufferObject<uint>(Indices, Silk.NET.OpenGL.BufferTargetARB.ElementArrayBuffer);
            VBO = new BufferObject<float>(Vertices, Silk.NET.OpenGL.BufferTargetARB.ArrayBuffer);
            VAO = new VAO<float, uint>(VBO, EBO);

            VAO.VertexAttributePointer(0, 3, Silk.NET.OpenGL.VertexAttribPointerType.Float, 5, 0);
            VAO.VertexAttributePointer(1, 2, Silk.NET.OpenGL.VertexAttribPointerType.Float, 5, 3);

            Shader = Shader.FromFiles("D:\\Programmation\\c#\\projets\\SharpEngine3\\SharpEngine3.Tests\\bin\\Debug\\net6.0\\shader.vert", "D:\\Programmation\\c#\\projets\\SharpEngine3\\SharpEngine3.Tests\\bin\\Debug\\net6.0\\shader.frag");
            Texture = new Texture("D:\\Programmation\\c#\\projets\\SharpEngine3\\SharpEngine3.Tests\\bin\\Debug\\net6.0\\container.png");
            Transform = new Transform();
            Camera = new Camera();
        }

        public static void OnRender(double time, Silk.NET.Maths.Vector2D<int> Size)
        {
            VAO.Bind();
            Shader.Use();
            Texture.Bind(Silk.NET.OpenGL.TextureUnit.Texture0);
            Shader.SetUniform("uTexture0", 0);

            float difference = (float)(time);
            Transform.Rotation = Quaternion.CreateFromYawPitchRoll(difference, difference, 0);

            Matrix4x4 model = Transform.ViewMatrix;
            Matrix4x4 view = Camera.GetViewMatrix();
            Matrix4x4 projection = Camera.GetProjectionMatrix(Size.X, Size.Y);

            Shader.SetUniform("uModel", model);
            Shader.SetUniform("uView", view);
            Shader.SetUniform("uProjection", projection);

            GLBackend.DrawArrays(Silk.NET.OpenGL.PrimitiveType.Triangles, 0, 36);
        }
    }
}
