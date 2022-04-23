using System.Drawing;
using System.Numerics;

namespace SE3.Utils
{
    public static class SE3Utils
    {
        public static Vector4 GetVector4FromColor(Color color) => new Vector4(color.R / 255f, color.G / 255f, color.B / 255f, color.A / 255f);
    }
}
