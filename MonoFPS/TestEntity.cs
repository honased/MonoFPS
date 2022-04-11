using HonasGame.Assets;
using HonasGame.ECS;
using HonasGame.ECS.Components;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoFPS
{
    public class TestEntity : Entity
    {
        public TestEntity()
        {
            new ModelRenderer(this) { Model = AssetLibrary.GetAsset<Model>("rifle") };
        }

        protected override void Cleanup()
        {

        }
    }
}
