using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class Incapacitator : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Incapacitator");
            // Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            Item.shoot = Mod.Find<ModProjectile>("Incapacitator").Type;
            Item.shootSpeed = 11f;
            Item.damage = 21;
            Item.knockBack = 5f;
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.noUseGraphic = true;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.value = 60;
            Item.rare = ItemRarityID.Orange;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(50);
            recipe.AddIngredient(null, "Doomite");
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
