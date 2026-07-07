using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terrarium.World.Tiles
{
    public class PermeableTerraWoodWand : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables";
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
