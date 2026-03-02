using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class Depthwalker : BaseAAItem
    {
        public override void SetDefaults()
        {
			Item.useTime = 25;
            Item.CloneDefaults(ItemID.CorruptYoyo);

            Item.damage = 14;                            
            Item.value = 1000000;
            Item.rare = 2;
            Item.knockBack = 1;
            Item.channel = true;
            Item.useStyle = 5;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.shoot = Mod.Find<ModProjectile>("Depthwalker").Type;  
		}

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Poisoned, 200);
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Depthwalker");
            // Tooltip.SetDefault("Walk the Hydra");
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "AbyssiumBar", 12);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

    }
}
