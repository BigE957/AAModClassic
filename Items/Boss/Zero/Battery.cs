using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class Battery : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Unstable Power Cell");
            /* Tooltip.SetDefault(@"Acts as a bullet
Non-consumable"); */

            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(10, 4));
        }

        public override void SetDefaults()
		{
			Item.damage = 40;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 20;
			Item.height = 32;
			Item.consumable = false;
			Item.knockBack = 7f;
			Item.value = Item.sellPrice(0, 30, 0, 0);
			Item.rare = ItemRarityID.LightPurple;
			Item.shoot = Mod.Find<ModProjectile>("RealityLaser").Type;
			Item.shootSpeed = 0f;
			Item.ammo = AmmoID.Bullet;
            Item.rare = ItemRarityID.Cyan; AARarity = 13;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity13;
                }
            }
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(1);
			recipe.AddIngredient(ItemID.MoonlordBullet, 999);
            recipe.AddIngredient(null, "ApocalyptitePlate", 1);
            recipe.AddIngredient(null, "UnstableSingularity", 1);
            recipe.AddTile(null, "ACS");
			recipe.Register();
		}
	}
}
