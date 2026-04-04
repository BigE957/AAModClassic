using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class EyeOfJudgement : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Eye of Judgement");
			/* Description.SetDefault(@"Eye of Judgement is protecting you
Damage and speed are increased"); */
			Main.debuff[Type] = false;
			BuffID.Sets.NurseCannotRemoveDebuff[Type] = false;
        }
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetDamage(DamageClass.Melee) += 0.2f;
			player.GetDamage(DamageClass.Ranged) += 0.2f;
			player.GetDamage(DamageClass.Magic) += 0.2f;
			player.GetDamage(DamageClass.Summon) += 0.2f;
			player.GetDamage(DamageClass.Throwing) += 0.2f;
			player.moveSpeed += 0.25f;
			if (player.ownedProjectileCounts[ModContent.ProjectileType<EyeOfJudgement>()] <= 0)
			{
				Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y-90, 0f, 0f, ModContent.ProjectileType<EyeOfJudgement>(), 100, 0, player.whoAmI);
			}
		}
	}
}
