using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class TrueScalpel : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 50;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 40;
            Item.height = 40;

            Item.useTime = 8;
            Item.useAnimation = 20;
            Item.pick = 205;
            Item.useStyle = 1;
            Item.knockBack = 1;
            Item.value = 10000;
            Item.rare = 7;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("True Scalpel");
        }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "Scalpel");
            recipe.AddIngredient(Mod, "HeroShards");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
