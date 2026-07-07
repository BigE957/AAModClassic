using AAModClassic.Buffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    public class StarHelmetMagePlayer_DarkLeech : StarHelmetMagePlayer_ArmorBonusLeechAbstract
    {

        public override void SetDefaults()
        {
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.width = Projectile.height = 4;
            Projectile.usesLocalNPCImmunity = true;
            for (int i = 0; i < Projectile.localNPCImmunity.Length; i++)
            {
                Projectile.localNPCImmunity[i] = -1;
            }
            Projectile.timeLeft = 3;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.DamageType = DamageClass.Magic;
            dust = ModContent.DustType<Dusts.DarkmatterDust>();
            potencyFactor = .02f;
        }
        public override void PlayerBenefit(int potency, Player player)
        {
            player.statLife += potency;
            player.HealEffect(potency, true);
        }

    }
}
