using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoFPS
{
    public static class CubeConstructor
    {
        public static void InitializeCube(float width, float height, float length, GraphicsDevice gd, out VertexBuffer vbo, out IndexBuffer ibo)
        {
            InitializeVertices(width, height, length, gd, out vbo);
            InitializeIBO(gd, out ibo);
        }

        private static void InitializeVertices(float width, float height, float length, GraphicsDevice gd, out VertexBuffer vbo)
        {
            var vertexData = new VertexPositionNormalTexture[] { 
            // Front Face
            new VertexPositionNormalTexture() { Position = new Vector3(0.0f, 0.0f, 0.0f), TextureCoordinate = new Vector2(0.0f, height), Normal = Vector3.Forward },
            new VertexPositionNormalTexture() { Position = new Vector3(0.0f,  1.0f, 0.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Forward },
            new VertexPositionNormalTexture() { Position = new Vector3( 1.0f,  1.0f, 0.0f), TextureCoordinate = new Vector2(width, 0.0f), Normal = Vector3.Forward },
            new VertexPositionNormalTexture() { Position = new Vector3( 1.0f, 0.0f, 0.0f), TextureCoordinate = new Vector2(width, height), Normal = Vector3.Forward },

            // Back Face
            new VertexPositionNormalTexture() { Position = new Vector3(0.0f, 0.0f, 1.0f), TextureCoordinate = new Vector2(width, height), Normal = Vector3.Backward },
            new VertexPositionNormalTexture() { Position = new Vector3( 1.0f, 0.0f, 1.0f), TextureCoordinate = new Vector2(0.0f, height), Normal = Vector3.Forward },
            new VertexPositionNormalTexture() { Position = new Vector3( 1.0f,  1.0f, 1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Forward },
            new VertexPositionNormalTexture() { Position = new Vector3(0.0f,  1.0f, 1.0f), TextureCoordinate = new Vector2(width, 0.0f), Normal = Vector3.Forward },

            // Top Face
            new VertexPositionNormalTexture() { Position = new Vector3(0.0f, 1.0f, 0.0f), TextureCoordinate = new Vector2(0.0f, length), Normal = Vector3.Up },
            new VertexPositionNormalTexture() { Position = new Vector3(0.0f, 1.0f,  1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Up },
            new VertexPositionNormalTexture() { Position = new Vector3( 1.0f, 1.0f,  1.0f), TextureCoordinate = new Vector2(width, 0.0f), Normal = Vector3.Up },
            new VertexPositionNormalTexture() { Position = new Vector3( 1.0f, 1.0f, 0.0f), TextureCoordinate = new Vector2(width, length), Normal = Vector3.Up },

            // Bottom Face
            new VertexPositionNormalTexture() { Position = new Vector3(0.0f, 0.0f, 0.0f), TextureCoordinate = new Vector2(width, length), Normal = Vector3.Down },
            new VertexPositionNormalTexture() { Position = new Vector3( 1.0f, 0.0f, 0.0f), TextureCoordinate = new Vector2(0.0f, length), Normal = Vector3.Down },
            new VertexPositionNormalTexture() { Position = new Vector3( 1.0f, 0.0f,  1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Down },
            new VertexPositionNormalTexture() { Position = new Vector3(0.0f, 0.0f,  1.0f), TextureCoordinate = new Vector2(width, 0.0f), Normal = Vector3.Down },

            // Left Face
            new VertexPositionNormalTexture() { Position = new Vector3(0.0f, 0.0f,  1.0f), TextureCoordinate = new Vector2(0.0f, height), Normal = Vector3.Left },
            new VertexPositionNormalTexture() { Position = new Vector3(0.0f,  1.0f,  1.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Left },
            new VertexPositionNormalTexture() { Position = new Vector3(0.0f,  1.0f, 0.0f), TextureCoordinate = new Vector2(length, 0.0f), Normal = Vector3.Left },
            new VertexPositionNormalTexture() { Position = new Vector3(0.0f, 0.0f, 0.0f), TextureCoordinate = new Vector2(length, height), Normal = Vector3.Left },

            // Right Face
            new VertexPositionNormalTexture() { Position = new Vector3( 1.0f, 0.0f, 0.0f), TextureCoordinate = new Vector2(0.0f, height), Normal = Vector3.Right },
            new VertexPositionNormalTexture() { Position = new Vector3( 1.0f,  1.0f, 0.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Right },
            new VertexPositionNormalTexture() { Position = new Vector3( 1.0f,  1.0f,  1.0f), TextureCoordinate = new Vector2(length, 0.0f), Normal = Vector3.Right },
            new VertexPositionNormalTexture() { Position = new Vector3( 1.0f, 0.0f,  1.0f), TextureCoordinate = new Vector2(length, height), Normal = Vector3.Right },
        };
            vbo = new VertexBuffer(gd, typeof(VertexPositionNormalTexture), vertexData.Length, BufferUsage.None);
            vbo.SetData<VertexPositionNormalTexture>(vertexData);
        }

        private static void InitializeIBO(GraphicsDevice gd, out IndexBuffer ibo)
        {
            var indexData = new short[]
        {
            // Front face
            0, 2, 1,
            0, 3, 2,

            // Back face 
            4, 6, 5,
            4, 7, 6,

            // Top face
            8, 10, 9,
            8, 11, 10,

            // Bottom face 
            12, 14, 13,
            12, 15, 14,

            // Left face 
            16, 18, 17,
            16, 19, 18,

            // Right face 
            20, 22, 21,
            20, 23, 22
        };
            ibo = new IndexBuffer(gd, IndexElementSize.SixteenBits, indexData.Length, BufferUsage.None);
            ibo.SetData<short>(indexData);
        }
    }
}
