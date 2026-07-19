using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic._Content.Stars._PostMoonlord.Items.Quest;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Hoard._PostMoonlord.Items._BossGreedA.BossStandard
{
    public class GreedAwakenedBox : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables.MusicBoxes";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Music Box (Worm King Greed)");
            // Tooltip.SetDefault("Plays 'Ira De Riquezas Perdidas' by Tyeski");

            ItemID.Sets.CanGetPrefixes[Type] = false;
            ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.MusicBox;
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<GreedAwakenedBox_Tile>();
            Item.width = 24;
            Item.height = 24;
            Item.rare = ItemRarityID.LightRed;
            Item.value = 10000;
            Item.accessory = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(ModContent.ItemType<GravitySphere>(), 1);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}

