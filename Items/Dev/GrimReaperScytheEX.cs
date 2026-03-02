using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Dev
{
    public class GrimReaperScytheEX : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Soul Shredder");
            /* Tooltip.SetDefault(@"Left click to swing and release homing scythe
Right click to do dashing hit
You are immune during the dash and deal 15x damage in true melee
Dashing ability has 5 seconds CD
'Well, how many Grim Reapers have you met before, mate?'
-Gregg
Scythe of the Grim Reaper EX"); */
		}

		public override void SetDefaults()
		{
			Item.autoReuse = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useAnimation = 30;
			Item.useTime = 30;
			Item.knockBack = 6f;
			Item.width = 24;
			Item.height = 28;
			Item.damage = 225;
			Item.crit = 14;
			Item.scale = 1.15f;
			Item.UseSound = SoundID.Item71;
			Item.rare = ItemRarityID.Purple;
			Item.shoot = Mod.Find<ModProjectile>("GrimReaperScytheEX").Type;
			Item.shootSpeed = 16f;
			Item.value = 1000000;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		}
		
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override bool CanUseItem(Player player)
		{
			int side = player.direction;
			if (player.altFunctionUse != 2)
			{
				Item.shoot = Mod.Find<ModProjectile>("GrimReaperScytheEX").Type;
				return true;
			}
			if (player.altFunctionUse == 2 && !player.HasBuff(Mod.Find<ModBuff>("ReaperCD").Type))
			{
				player.AddBuff(Mod.Find<ModBuff>("ReaperImmune2").Type, 60);
				player.AddBuff(Mod.Find<ModBuff>("ReaperCD").Type, 300);
				Item.shoot = Mod.Find<ModProjectile>("ReaperHitbox").Type;
				player.velocity.X = 26f * side;
				return true;
			}
			else
			{
				return false;
			}
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (type == Mod.Find<ModProjectile>("GrimReaperScytheEX").Type)
			{
				float num121 = 0.99f;
				int num122 = 3;
				Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
				float num82 = Main.mouseX + Main.screenPosition.X - vector2.X;
				float num83 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
				Vector2 vector14 = new Vector2(speedX, speedY);
				vector14.Normalize();
				vector14 *= 40f;
				bool flag11 = Collision.CanHit(vector2, 0, 0, vector2 + vector14, 0, 0);
				for (int num123 = 0; num123 < num122; num123++)
				{
					float num124 = num123 - (num122 - 1f) / 2f;
					Vector2 vector15 = vector14.RotatedBy(num121 * num124, default);
					if (!flag11)
					{
						vector15 -= vector14;
					}
					if (type == Mod.Find<ModProjectile>("GrimReaperScytheEX").Type && player.HasBuff(Mod.Find<ModBuff>("ReaperImmune2").Type))
					{
						Projectile.NewProjectile(vector2.X + vector15.X, vector2.Y + vector15.Y, num82, num83, type, damage/15, knockBack, player.whoAmI);
					}
					else
					{
						Projectile.NewProjectile(vector2.X + vector15.X, vector2.Y + vector15.Y, num82, num83, type, damage, knockBack, player.whoAmI);
					}
				}
			}
			if (type == Mod.Find<ModProjectile>("ReaperHitbox").Type)
			{
				return true;
			}
			return false;
		}
		
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "GrimReaperScythe", 1);
            recipe.AddIngredient(null, "EXSoul", 1);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }
	}
}
