using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoFPS
{
    public static class Camera3D
    {
        private const float FOV = 60.0f;
        private const float ASPECT_RATIO = 1920.0f / 1080.0f;

        public static Vector3 Position;
        public static Vector3 Direction;

        public static Matrix View => GetView();
        public static Matrix Projection => GetProjection();

        private static Matrix GetView()
        {
            
            return Matrix.CreateLookAt(Position, Position + Direction, Vector3.Up);
        }

        private static Matrix GetProjection()
        {
            return Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(FOV), ASPECT_RATIO, 0.01f, 10000.0f);
        }
    }
}
