using AAModClassic.Assets;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace AAModClassic._Content.Chaos.__Hardmode.Items.Materials
{
    public class ChaosPrism : BaseAAItem
    {
        public override string Texture => AssetDirectory.Items.BiomePrism;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Prism");
            // Tooltip.SetDefault("Imbued with the discordian flames of chaos");
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = 10000;
            Item.rare = ItemRarityID.Yellow;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return AAColor.Shen3;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, AAColor.Shen3.ToVector3() * 0.55f * Main.essScale);
        }
    }
}