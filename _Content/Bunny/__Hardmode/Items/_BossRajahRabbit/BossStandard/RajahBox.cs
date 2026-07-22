using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic._Content._Misc.__Hardmode.Items.Consumables;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Bunny.__Hardmode.Items._BossRajahRabbit.BossStandard
{
    public class RajahBox : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables.MusicBoxes";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Music Box (Rajah Rabbit)");
            // Tooltip.SetDefault(@"Plays 'JUSTICE' by SpectralAves");

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
            Item.createTile = ModContent.TileType<RajahBox_Tile>();
            Item.width = 36;
            Item.height = 36;
            Item.rare = ItemRarityID.LightRed;
            Item.value = 10000;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(ModContent.ItemType<Carrot>(), 20);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
