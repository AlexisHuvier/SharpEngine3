using Silk.NET.OpenGL;

namespace SE3.Graphics.OpenGL
{
    public class VAO<TVertexType, TIndexType>: IDisposable 
        where TVertexType : unmanaged
        where TIndexType : unmanaged
    {
        private uint handle;

        public VAO(BufferObject<TVertexType> vbo, BufferObject<TIndexType> ebo)
        {
            handle = GLBackend.gl.GenVertexArray();
            Bind();
            vbo.Bind();
            ebo.Bind();
        }

        public unsafe void VertexAttributePointer(uint index, int count, VertexAttribPointerType type, uint vertexSize, int offset)
        {
            GLBackend.gl.VertexAttribPointer(index, count, type, false, vertexSize * (uint)sizeof(TVertexType), (void*)(offset * sizeof(TVertexType)));
            GLBackend.gl.EnableVertexAttribArray(index);
        }

        public void Bind() => GLBackend.gl.BindVertexArray(handle);
        public void Dispose() => GLBackend.gl.DeleteVertexArray(handle);
    }
}
