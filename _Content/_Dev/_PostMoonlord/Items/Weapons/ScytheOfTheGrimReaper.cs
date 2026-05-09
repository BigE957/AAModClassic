using AAModClassic.Buffs;
using AAModClassic.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev._PostMoonlord.Items.Weapons
{
    public class ScytheOfTheGrimReaper : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Scythe of the Grim Reaper");
            /* Tooltip.SetDefault(@"Left click to swing and release homing scythe
Right click to do dashing hit
You are immune during the dash and deal 10x damage in true melee
Dashing ability has 10 seconds CD
'Well, how many Grim Reapers have you met before, mate?'
-Gregg"); */
        }

		public override void SetDefaults()
		{
			Item.autoReuse = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useAnimation = 30;
			Item.useTime = 30;
			Item.knockBack = 5f;
			Item.width = 24;
			Item.height = 28;
			Item.damage = 150;
			Item.crit = 14;
			Item.scale = 1.15f;
			Item.UseSound = SoundID.Item71;
			Item.rare = ItemRarityID.Lime;
			Item.shoot = ModContent.ProjectileType<ScytheOfTheGrimReaper_Proj>();
			Item.shootSpeed = 14f;
			Item.value = 500000;
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
				Item.shoot = ModContent.ProjectileType<ScytheOfTheGrimReaper_Proj>();
				return true;
			}
			if (player.altFunctionUse == 2 && !player.HasBuff(ModContent.BuffType<ReaperCD_Buff>()))
			{
				player.AddBuff(ModContent.BuffType<ReaperImmune_Buff>(), 60);
				player.AddBuff(ModContent.BuffType<ReaperCD_Buff>(), 600);
				Item.shoot = ModContent.ProjectileType<ReaperHitbox>();
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
			if (type == ModContent.ProjectileType<ScytheOfTheGrimReaper_Proj>() && player.HasBuff(ModContent.BuffType<ReaperImmune_Buff>()))
			{
				damage /= 10;
			}
			return true;
		}
	}
}
