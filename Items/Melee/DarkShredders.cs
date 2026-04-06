using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee
{
    public class DarkShredders : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 350;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 80;
            Item.height = 80;
            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.channel = true;
            Item.useStyle = 100;
            Item.knockBack = 3f;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = ItemRarityID.Purple;      
            Item.shoot = ModContent.ProjectileType<Projectiles.DarkShredders>();
            Item.noUseGraphic = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DarkEnergy", 5);
            recipe.AddIngredient(null, "DarkMatter", 12);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Reality Shredders");
            // Tooltip.SetDefault("Blades made out of Dark matter. Inflicts the Electified debuff");
        }

 
        public override void UseItemFrame(Player player)
        {
            player.bodyFrame.Y = 3 * player.bodyFrame.Height;
        }
    }
}
