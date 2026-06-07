using AAModClassic._Content.Inferno.___PreHardmode.Items.Tools;
using AAModClassic._Content.Terra.__Hardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.__Hardmode.Items.Tools
{
    public class PerfectStonebreaker : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 90;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 8;
            Item.useAnimation = 20;
            Item.pick = 205;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1;
            Item.value = Item.sellPrice(0, 10);
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Perfect Stonebreaker");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Stonebreaker>());
            recipe.AddIngredient(ItemID.ChlorophyteBar, 24);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
