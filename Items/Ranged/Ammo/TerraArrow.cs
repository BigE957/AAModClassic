using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged.Ammo
{
    public class TerraArrow : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Terra Arrow");
			/* Tooltip.SetDefault(@"Homes in on enemies
Not Consumable"); */
		}

		public override void SetDefaults()
		{
			Item.damage = 12;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 14;
			Item.height = 32;
			Item.knockBack = 4f;
			Item.value = 30;
			Item.rare = ItemRarityID.Blue;
			Item.shoot = Mod.Find<ModProjectile>("TerraArrow").Type;
            Item.shootSpeed = 1f;
			Item.ammo = AmmoID.Arrow;
		}

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.HolyArrow, 999);
			recipe.AddIngredient(null, "ReaperArrow", 999);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
