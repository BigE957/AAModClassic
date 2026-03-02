using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class EyeOfJudgement : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Eye of Judgement");
			/* Description.SetDefault(@"Eye of Judgement is protecting you
Damage and speed are increased"); */
			Main.debuff[Type] = false;
			canBeCleared/* tModPorter Note: Removed. Use BuffID.Sets.NurseCannotRemoveDebuff instead, and invert the logic */ = true;
        }
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetDamage(DamageClass.Melee) += 0.2f;
			player.GetDamage(DamageClass.Ranged) += 0.2f;
			player.GetDamage(DamageClass.Magic) += 0.2f;
			player.GetDamage(DamageClass.Summon) += 0.2f;
			player.GetDamage(DamageClass.Throwing) += 0.2f;
			player.moveSpeed += 0.25f;
			if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("EyeOfJudgement").Type] <= 0)
			{
				Projectile.NewProjectile(player.Center.X, player.Center.Y-90, 0f, 0f, Mod.Find<ModProjectile>("EyeOfJudgement").Type, 100, 0, player.whoAmI);
			}
		}
	}
}
