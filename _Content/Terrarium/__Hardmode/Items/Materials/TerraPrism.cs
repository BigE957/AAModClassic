using AAModClassic._Content._Misc.__Hardmode.Items.Materials;
using AAModClassic._Content.Sky.__Hardmode.Items.Materials;
using AAModClassic._Content.Terra._PostMoonLord.Items.Tiles.Functional;
using AAModClassic._Content.Terrarium.___PreHardmode.Items.Materials;
using AAModClassic._Content.Underground.___PreHardmode.Items.Materials;
using AAModClassic.Assets;
using AAModClassic.Globals;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terrarium.__Hardmode.Items.Materials
{
    public class TerraPrism : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        public override string Texture => AssetDirectory.Items.BiomePrism;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Prism");
            // Tooltip.SetDefault("Imbued with the unified harmony of the land of Terraria");
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
            return AAColor.TerraGlow;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, AAColor.TerraGlow.ToVector3() * 0.55f * Main.essScale);
        }
    }
}