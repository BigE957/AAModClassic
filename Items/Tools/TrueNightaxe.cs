using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class TrueNightaxe : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 90;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 40;
            Item.height = 40;

            Item.useTime = 8;
            Item.useAnimation = 17;
            Item.pick = 205;
            Item.useStyle = 1;
            Item.knockBack = 1;
            Item.value = 10;
            Item.rare = 7;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("True Nightaxe");
        }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "Nightaxe");
            recipe.AddIngredient(Mod, "HeroShards");
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}
