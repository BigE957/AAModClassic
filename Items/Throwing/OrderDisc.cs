using AAModClassic.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Throwing
{
	public class OrderDisc : BaseAAItem
	{
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.LightDisc);
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.shootSpeed = 16f;
			Item.stack = 1;
			Item.useTime = 12;
			Item.damage = 75;                            
			Item.value = 20;
			Item.rare = ItemRarityID.Pink;
			Item.knockBack = 4;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useAnimation = 12;
			Item.shoot = ModContent.ProjectileType<Projectiles.OrderDiscP>();
			Item.width = 46;
			Item.height = 46;
            Item.noMelee = true;
        }

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Order Disc");
			// Tooltip.SetDefault("Ignores enemy defense");
		}

        public override bool CanUseItem(Player player)       //this make that you can shoot only 1 boomerang at once
        {
            int num16 = 0;
            for (int num17 = 0; num17 < 1000; num17++)
            {
                if (Main.projectile[num17].active && Main.projectile[num17].owner == Main.myPlayer && Main.projectile[num17].type == Item.shoot)
                {
                    num16++;
                }
            }
            if (num16 >= Item.stack)
            {
                return false;
            }
            return true;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<OrderBar>(), 15);
			recipe.AddIngredient(ItemID.Ectoplasm, 10);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}
