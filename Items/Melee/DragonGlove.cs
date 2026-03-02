using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class DragonGlove : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragon Glove");
        }

        public override void SetDefaults()
        {
            Item.useStyle = 1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 9;
            Item.useTime = 9;
            Item.width = 28;
            Item.height = 24;
            Item.damage = 21;
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item1;
            Item.scale = 1.35f;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.rare = 3;
            Item.value = 50000;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "IncineriteBar", 10);
            recipe.AddIngredient(Mod, "DragonClaw", 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}