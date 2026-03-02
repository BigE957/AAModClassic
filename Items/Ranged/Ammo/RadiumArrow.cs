using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged.Ammo
{
    public class RadiumArrow : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Radium Arrow");
		}

		public override void SetDefaults()
		{
			Item.damage = 14;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 14;
			Item.height = 32;
			Item.maxStack = 999;
			Item.consumable = true;
			Item.knockBack = 4f;
			Item.value = 30;
			Item.rare = 11;
			Item.shoot = Mod.Find<ModProjectile>("RadiumArrow").Type;
			Item.shootSpeed = 6f; 
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

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(400);
            recipe.AddIngredient(null, "Stardust", 1);
            recipe.AddIngredient(null, "RadiumBar", 3);
            recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.Register();
		}
	}
}
