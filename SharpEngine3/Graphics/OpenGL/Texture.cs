using Silk.NET.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Runtime.InteropServices;

namespace SE3.Graphics.OpenGL
{
    public class Texture: IDisposable
    {
        private uint handle;

        public unsafe Texture(string path)
        {
            Image<Rgba32> img = (Image<Rgba32>)Image.Load(path);

            fixed (void* data = &MemoryMarshal.GetReference(img.GetPixelRowSpan(0)))
                Load(data, (uint)img.Width, (uint)img.Height);

            img.Dispose();
        }

        public unsafe Texture(Span<byte> data, uint width, uint height)
        {
            fixed(void* d = &data[0])
                Load(d, width, height);
        }

        private unsafe void Load(void* data, uint width, uint height)
        {
            handle = GLBackend.gl.GenTexture();
            Bind();

            GLBackend.gl.TexImage2D(TextureTarget.Texture2D, 0, (int)InternalFormat.Rgba, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, data);

            GLBackend.gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)GLEnum.Repeat);
            GLBackend.gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)GLEnum.Repeat);
            GLBackend.gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)GLEnum.Linear);
            GLBackend.gl.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)GLEnum.Linear);
        }

        public void Bind(TextureUnit textureSlot = TextureUnit.Texture0)
        {
            GLBackend.gl.ActiveTexture(textureSlot);
            GLBackend.gl.BindTexture(TextureTarget.Texture2D, handle);
        }

        public void Dispose() => GLBackend.gl.DeleteTexture(handle);
    }
}
