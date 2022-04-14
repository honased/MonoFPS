using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoFPS
{
    public static class FloorConstructor
    {
        public static void InitializeFloor(float width, float length, GraphicsDevice gd, out VertexBuffer vbo, out IndexBuffer ibo)
        {
            InitializeVertices(width, length, gd, out vbo);
            InitializeIBO(gd, false, out ibo);
        }

        public static void InitializeCeiling(float width, float length, GraphicsDevice gd, out VertexBuffer vbo, out IndexBuffer ibo)
        {
            InitializeVertices(width, length, gd, out vbo);
            InitializeIBO(gd, true, out ibo);
        }

        private static void InitializeVertices(float width, float length, GraphicsDevice gd, out VertexBuffer vbo)
        {
            var vertexData = new VertexPositionNormalTexture[] { 
            // Bottom Face
            new VertexPositionNormalTexture() { Position = new Vector3(0.0f, 0.0f, 0.0f), TextureCoordinate = new Vector2(width, length), Normal = Vector3.Down },
            new VertexPositionNormalTexture() { Position = new Vector3( 1.0f, 0.0f, 0.0f), TextureCoordinate = new Vector2(0.0f, length), Normal = Vector3.Down },
            new VertexPositionNormalTexture() { Position = new Vector3( 1.0f, 0.0f,  1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Down },
            new VertexPositionNormalTexture() { Position = new Vector3(0.0f, 0.0f,  1.0f), TextureCoordinate = new Vector2(width, 0.0f), Normal = Vector3.Down },
        };
            vbo = new VertexBuffer(gd, typeof(VertexPositionNormalTexture), vertexData.Length, BufferUsage.None);
            vbo.SetData<VertexPositionNormalTexture>(vertexData);
        }

        private static void InitializeIBO(GraphicsDevice gd, bool isCeiling, out IndexBuffer ibo)
        {
            var indexData = new short[]
            {
                // Face
                0, 1, 2,
                0, 2, 3,
            };

            if(isCeiling)
            {
                indexData = new short[]
                {
                    // Face
                    0, 2, 1,
                    0, 3, 2,
                };
            }

            ibo = new IndexBuffer(gd, IndexElementSize.SixteenBits, indexData.Length, BufferUsage.None);
            ibo.SetData<short>(indexData);
        }
    }
}
