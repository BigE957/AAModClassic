using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;

namespace AAModClassic._Content.Terrarium.World.Tiles
{
    public class PermeableTerraWoodWand : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Permeable Terra Wood Wand");
            /* Tooltip.SetDefault(@"Right click to swap modes"); */
        }
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.LivingWoodWand);
            Item.createTile = ModContent.TileType<PermeableTerraWood_Tile>();
        }

        public override bool CanRightClick() => true;

        public override void RightClick(Player player)
        {
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Research);
            bool favorited = Item.favorited;
            Item.SetDefaults(ModContent.ItemType<TerraWoodWand>());
            Item.stack++;
            Item.favorited = favorited;
        }
    }
}
