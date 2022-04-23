using Silk.NET.OpenGL;

namespace SE3.Graphics.OpenGL
{
    public class BufferObject<TDataType> : IDisposable
        where TDataType : unmanaged
    {
        private uint handle;
        private BufferTargetARB bufferType;

        public unsafe BufferObject(Span<TDataType> data, BufferTargetARB bufferType)
        {
            this.bufferType = bufferType;

            handle = GLBackend.gl.GenBuffer();
            Bind();
            fixed (void* dataPtr = data)
                GLBackend.gl.BufferData(bufferType, (nuint)(data.Length * sizeof(TDataType)), dataPtr, BufferUsageARB.StaticDraw);
        }

        public void Bind() => GLBackend.gl.BindBuffer(bufferType, handle);
        public void Dispose() => GLBackend.gl.DeleteBuffer(handle);
    }
}
