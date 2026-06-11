using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Dungeon.___PreHardmode.Items.Tools
{
    public class Toothpick : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 8;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 38;
            Item.height = 38;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.pick = 90;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1;
            Item.value = 10;
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Toothpick");
        }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Bone, 12);
            recipe.AddRecipeGroup("AAModClassic:Gold", 8);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
