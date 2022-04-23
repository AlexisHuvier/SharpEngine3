using System.Numerics;

namespace SE3.Components
{
    public class Camera
    { 
        private Vector3 position = new Vector3(0f, 0f, 3f);
        private Vector3 front = new Vector3(0f, 0f, -1f);
        private Vector3 up = Vector3.UnitY;
        private Vector3 direction = Vector3.Zero;
        private float yaw = -90f;
        private float pitch = 0f;
        private float zoom = 45f;

        public Matrix4x4 GetViewMatrix() => Matrix4x4.CreateLookAt(position, position + front, up);
        public Matrix4x4 GetProjectionMatrix(int width, int height) => Matrix4x4.CreatePerspectiveFieldOfView(Utils.MathHelper.DegreesToRadiants(zoom), width / height, 0.1f, 100f);

        public float GetYaw() => yaw;
        public float GetPitch() => pitch;
        public float GetZoom() => zoom;
        public Vector3 GetDirection() => direction;
        public Vector3 GetPosition() => position;
        public Vector3 GetFront() => front;
        public Vector3 GetUp() => up;
        public Vector3 GetRight() => Vector3.Normalize(Vector3.Cross(front, up));

        public void SetYaw(float yaw)
        {
            this.yaw = yaw;
            UpdateDirection();
        }

        public void SetPitch(float pitch)
        {
            this.pitch = pitch;
            UpdateDirection();
        }

        public void SetZoom(float zoom) => this.zoom = Math.Clamp(zoom, 1f, 45f);
        public void SetPosition(Vector3 position) => this.position = position;

        public void UpdateDirection()
        {
            direction.X = MathF.Cos(Utils.MathHelper.DegreesToRadiants(yaw)) * MathF.Cos(Utils.MathHelper.DegreesToRadiants(pitch));
            direction.Y = MathF.Sin(Utils.MathHelper.DegreesToRadiants(pitch));
            direction.Z = MathF.Sin(Utils.MathHelper.DegreesToRadiants(yaw)) * MathF.Cos(Utils.MathHelper.DegreesToRadiants(pitch));
            front = Vector3.Normalize(direction);
        }
    }
}
