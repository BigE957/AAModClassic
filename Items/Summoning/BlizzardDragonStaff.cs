using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Summoning
{
    public class BlizzardDragonStaff : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Blizzard Dragon Staff");
            /* Tooltip.SetDefault(@"Summons a blizzard dragon to fight for you
Hydra Staff EX"); */
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<BlizzardDragon>();
            Item.damage = 222;
            Item.width = 50;
            Item.height = 50;
            Item.UseSound = SoundID.Item44;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 15, 0, 0);
            Item.knockBack = 8f;
            Item.rare = ItemRarityID.Purple;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 50;
            Item.sentry = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int i = Main.myPlayer;
            int num74 = Item.shoot;
            int num76 = Item.damage;
            float num77 = Item.knockBack;
            int num154 = (int)(Main.mouseX + Main.screenPosition.X) / 16;
            int num155 = (int)(Main.mouseY + Main.screenPosition.Y) / 16;
            if (player.gravDir == -1f)
            {
                num155 = (int)(Main.screenPosition.Y + Main.screenHeight - Main.mouseY) / 16;
            }
			if (player.ownedProjectileCounts[num74] < player.maxTurrets)
			{
				Projectile.NewProjectile(source, Main.mouseX + Main.screenPosition.X, num155 * 16 - 24, 0f, 15f, num74, num76, num77, i, 0f, 0f);
			}
			if (player.ownedProjectileCounts[num74] == player.maxTurrets)
			{
				for(int g = 0; g < 1000; ++g)
				{
					if(Main.projectile[g].active && Main.projectile[g].type == num74)
					{
						Main.projectile[g].Kill();
						break;
					}
				}
				 Projectile.NewProjectile(source, Main.mouseX + Main.screenPosition.X, num155 * 16 - 24, 0f, 15f, num74, num76, num77, i, 0f, 0f);
			}

            return false;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.StaffoftheFrostHydra);
			recipe.AddIngredient(null, "EXSoul");
			recipe.AddTile(null, "QuantumFusionAccelerator");
			recipe.Register();
		}
    }
}