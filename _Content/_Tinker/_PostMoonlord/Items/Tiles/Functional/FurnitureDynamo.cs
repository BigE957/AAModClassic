using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content._Tinker._PostMoonlord.Items.Tiles.Functional
{
    public class FurnitureDynamo : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables.Functional";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Furniture Dynamo");
            /* Tooltip.SetDefault(@"Combines all funiture-crafting stations into one block
Now you don't have to clutter your base with 12 crafting stations!"); */
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.rare = ItemRarityID.Cyan;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.value = 1000000;
            Item.createTile = ModContent.TileType<FurnitureDynamo_Tile>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Sawmill, 1);
            recipe.AddIngredient(ItemID.BoneWelder, 1);
            recipe.AddIngredient(ItemID.BlendOMatic, 1);
            recipe.AddIngredient(ItemID.GlassKiln, 1);
            recipe.AddIngredient(ItemID.HeavyWorkBench, 1);
            recipe.AddIngredient(ItemID.HoneyDispenser, 1);
            recipe.AddIngredient(ItemID.IceMachine, 1);
            recipe.AddIngredient(ItemID.LivingLoom, 1);
            recipe.AddIngredient(ItemID.MeatGrinder, 1);
            recipe.AddIngredient(ItemID.SkyMill, 1);
            recipe.AddIngredient(ItemID.Solidifier, 1);
            recipe.AddIngredient(ItemID.SteampunkBoiler, 1);
            recipe.AddIngredient(ItemID.FleshCloningVaat, 1);
            recipe.AddIngredient(ItemID.LihzahrdFurnace, 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}
