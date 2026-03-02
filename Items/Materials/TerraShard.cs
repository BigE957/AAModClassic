using Microsoft.Xna.Framework;
using Terraria;

namespace AAModClassic.Items.Materials
{
    public class TerraShard : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Shard");
            // ticksperframe, frameCount
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 18;
            Item.maxStack = 999;
            Item.value = 100;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.LimeGreen.ToVector3() * 0.55f * Main.essScale);
        }
    }
}