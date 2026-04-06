using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._PreHardmode.Items.Weapons
{
    public class Depthwalker : BaseAAItem
    {
        public override void SetDefaults()
        {
			Item.useTime = 25;
            Item.CloneDefaults(ItemID.CorruptYoyo);

            Item.damage = 14;                            
            Item.value = 1000000;
            Item.rare = ItemRarityID.Green;
            Item.knockBack = 1;
            Item.channel = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.shoot = ModContent.ProjectileType<Depthwalker_Holdout>();  
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
