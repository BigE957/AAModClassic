using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class EyeOfForsaken : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Eye of the Forsaken");
			/* Description.SetDefault(@"Eye of the Forsaken is protecting you
Damage and speed are increased"); */
			Main.debuff[Type] = false;
			canBeCleared/* tModPorter Note: Removed. Use BuffID.Sets.NurseCannotRemoveDebuff instead, and invert the logic */ = true;
        }
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetDamage(DamageClass.Melee) += 0.25f;
			player.GetDamage(DamageClass.Ranged) += 0.25f;
			player.GetDamage(DamageClass.Magic) += 0.25f;
			player.GetDamage(DamageClass.Summon) += 0.25f;
			player.GetDamage(DamageClass.Throwing) += 0.25f;
			player.moveSpeed += 0.35f;
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("EyeOfForsaken").Type] <= 0)
			{
				Projectile.NewProjectile(player.Center.X, player.Center.Y-90, 0f, 0f, Mod.Find<ModProjectile>("EyeOfForsaken").Type, 150, 0, player.whoAmI);
			}
		}
	}
}
