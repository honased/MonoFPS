using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoFPS
{
    public static class QuadConstructor
    {
        public static void InitializeQuad(float width, float height, GraphicsDevice gd, out VertexBuffer vbo, out IndexBuffer ibo)
        {
            InitializeVertices(width, height, gd, out vbo);
            InitializeIBO(gd, out ibo);
        }

        private static void InitializeVertices(float width, float height, GraphicsDevice gd, out VertexBuffer vbo)
        {
            var vertexData = new VertexPositionNormalTexture[] { 
            // Front Face
            new VertexPositionNormalTexture() { Position = new Vector3(-0.5f, 0.0f, 0.0f), TextureCoordinate = new Vector2(0.0f, 1.0f), Normal = Vector3.Forward },
            new VertexPositionNormalTexture() { Position = new Vector3(-0.5f,  1.0f, 0.0f), TextureCoordinate = new Vector2(0.0f, 0.0f), Normal = Vector3.Forward },
            new VertexPositionNormalTexture() { Position = new Vector3( 0.5f,  1.0f, 0.0f), TextureCoordinate = new Vector2(1.0f, 0.0f), Normal = Vector3.Forward },
            new VertexPositionNormalTexture() { Position = new Vector3( 0.5f, 0.0f, 0.0f), TextureCoordinate = new Vector2(1.0f, 1.0f), Normal = Vector3.Forward },
        };
            vbo = new VertexBuffer(gd, typeof(VertexPositionNormalTexture), vertexData.Length, BufferUsage.None);
            vbo.SetData<VertexPositionNormalTexture>(vertexData);
        }

        private static void InitializeIBO(GraphicsDevice gd, out IndexBuffer ibo)
        {
            var indexData = new short[]
        {
            // Face
            0, 2, 1,
            0, 3, 2,
        };
            ibo = new IndexBuffer(gd, IndexElementSize.SixteenBits, indexData.Length, BufferUsage.None);
            ibo.SetData<short>(indexData);
        }
    }
}
