using Silk.NET.OpenGL;

namespace SE3.Graphics.OpenGL
{
    public class Shader : IDisposable
    {
        private readonly uint handle;

        public Shader(string vertSource, string fragSource)
        {
            uint vertexShader = GLBackend.gl.CreateShader(ShaderType.VertexShader);
            GLBackend.gl.ShaderSource(vertexShader, vertSource);
            GLBackend.gl.CompileShader(vertexShader);

            string infoLog = GLBackend.gl.GetShaderInfoLog(vertexShader);
            if (!string.IsNullOrEmpty(infoLog))
                Console.WriteLine($"Error while compiling vertex shader : {infoLog}");

            uint fragmentShader = GLBackend.gl.CreateShader(ShaderType.FragmentShader);
            GLBackend.gl.ShaderSource(fragmentShader, fragSource);
            GLBackend.gl.CompileShader(fragmentShader);

            infoLog = GLBackend.gl.GetShaderInfoLog(fragmentShader);
            if (!string.IsNullOrEmpty(infoLog))
                Console.WriteLine($"Error while compiling fragment shader : {infoLog}");

            handle = GLBackend.gl.CreateProgram();
            GLBackend.gl.AttachShader(handle, vertexShader);
            GLBackend.gl.AttachShader(handle, fragmentShader);
            GLBackend.gl.LinkProgram(handle);

            GLBackend.gl.GetProgram(handle, GLEnum.LinkStatus, out int status);
            if (status == 0)
                Console.WriteLine($"Error linking shader : {GLBackend.gl.GetProgramInfoLog(handle)}");

            GLBackend.gl.DetachShader(handle, vertexShader);
            GLBackend.gl.DetachShader(handle, fragmentShader);
            GLBackend.gl.DeleteShader(vertexShader);
            GLBackend.gl.DeleteShader(fragmentShader);
        }

        public static Shader FromFiles(string vertPath, string fragPath) => new Shader(File.ReadAllText(vertPath), File.ReadAllText(fragPath));

        public void Use() => GLBackend.gl.UseProgram(handle);
        public void Dispose() => GLBackend.gl.DeleteProgram(handle);
        public int GetAttribLocation(string name) => GLBackend.gl.GetAttribLocation(handle, name);

        // Uniform setters - BEGIN

        public void SetUniform(string name, int data)
        {
            int location = GLBackend.gl.GetUniformLocation(handle, name);
            if (location == -1)
                throw new Exception($"Uniform '{name}' doesnt exist in shader {handle}");
            GLBackend.gl.Uniform1(location, data);
        }

        public void SetUniform(string name, float data)
        {
            int location = GLBackend.gl.GetUniformLocation(handle, name);
            if (location == -1)
                throw new Exception($"Uniform '{name}' doesnt exist in shader {handle}");
            GLBackend.gl.Uniform1(location, data);
        }

        public unsafe void SetUniform(string name, System.Numerics.Matrix4x4 data)
        {
            int location = GLBackend.gl.GetUniformLocation(handle, name);
            if(location == -1)
                throw new Exception($"Uniform '{name}' doesnt exist in shader {handle}");
            GLBackend.gl.UniformMatrix4(location, 1, false, (float*)&data);
        }

        // Uniform setters - END
    }
}
