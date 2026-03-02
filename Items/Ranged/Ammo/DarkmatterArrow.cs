using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged.Ammo
{
    public class DarkmatterArrow : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Darkmatter Arrow");
		}

		public override void SetDefaults()
		{
			Item.damage = 14;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 14;
			Item.height = 32;
			Item.maxStack = 999;
			Item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			Item.knockBack = 4f;
			Item.value = 30;
			Item.shoot = Mod.Find<ModProjectile>("DarkmatterArrow").Type;   //The projectile shoot when your weapon using this ammo
			Item.shootSpeed = 1f;                  //The speed of the projectile
			Item.ammo = AmmoID.Arrow;
			Item.rare = 9;
			AARarity = 12;
		}

		public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
		{
			foreach (TooltipLine line2 in list)
			{
				if (line2.Mod == "Terraria" && line2.Name == "ItemName")
				{
					line2.OverrideColor = AAColor.Rarity12;
				}
			}
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Electrified, 300);
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(400);
            recipe.AddIngredient(null, "DarkEnergy", 1);
            recipe.AddIngredient(null, "DarkMatter", 3);
			recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.Register();
		}
	}
}
