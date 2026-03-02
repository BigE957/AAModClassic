using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class Stonebreaker : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 40;
            Item.height = 40;
            Item.useAnimation = 30;
            Item.useTime = 10;
            Item.pick = 110;
            Item.useStyle = 1;
            Item.knockBack = 1;
            Item.value = Terraria.Item.sellPrice(0, 1, 8, 0);
            Item.rare = 2;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Stonebreaker");
        }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "DragonDigger");
            recipe.AddIngredient(Mod, "OceanPick");
            recipe.AddIngredient(Mod, "Excavator");
            recipe.AddIngredient(Mod, "DoomiteMiningLaser");
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
