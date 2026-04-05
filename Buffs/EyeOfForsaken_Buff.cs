using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class EyeOfForsaken_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Eye of the Forsaken");
			/* Description.SetDefault(@"Eye of the Forsaken is protecting you
Damage and speed are increased"); */
			Main.debuff[Type] = false;
			BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetDamage(DamageClass.Melee) += 0.25f;
			player.GetDamage(DamageClass.Ranged) += 0.25f;
			player.GetDamage(DamageClass.Magic) += 0.25f;
			player.GetDamage(DamageClass.Summon) += 0.25f;
			player.GetDamage(DamageClass.Throwing) += 0.25f;
			player.moveSpeed += 0.35f;
			if (player.ownedProjectileCounts[ModContent.ProjectileType<EyeOfForsaken>()] <= 0)
			{
				Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y-90, 0f, 0f, ModContent.ProjectileType<EyeOfForsaken>(), 150, 0, player.whoAmI);
			}
		}
	}
}
