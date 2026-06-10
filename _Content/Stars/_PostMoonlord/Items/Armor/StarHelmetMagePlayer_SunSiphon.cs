using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    public class StarHelmetMagePlayer_SunSiphon : StarHelmetMagePlayer_ArmorBonusLeechAbstract
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
            dust = ModContent.DustType<Dusts.RadiumDust>();
            potencyFactor = .25f;
        }
        public override void PlayerBenefit(int potency, Player player)
        {
            int manaCount = 0;
            int overloadCount = 0;
            for(; potency > 0; potency--)
            {
                if(player.statManaMax2 > player.statMana )
                {
                    player.statMana++;
                    manaCount++;
                }
                else
                {
                    overloadCount++;
                    
                }
            }
            if(manaCount>0)
            {
                player.ManaEffect(manaCount);
            }
            if (overloadCount >0)
            {
                if (player.HasBuff(ModContent.BuffType<ManaOverload_Buff>()))
                {

                    player.buffTime[player.FindBuffIndex(ModContent.BuffType<ManaOverload_Buff>())] += overloadCount * 2;
                    if (player.buffTime[player.FindBuffIndex(ModContent.BuffType<ManaOverload_Buff>())] > 600)
                    {
                        player.buffTime[player.FindBuffIndex(ModContent.BuffType<ManaOverload_Buff>())] = 600;
                    }
                }
                else
                {
                    player.AddBuff(ModContent.BuffType<ManaOverload_Buff>(), overloadCount * 2);
                }
                CombatText.NewText(player.Hitbox, Color.Purple, overloadCount * 2);
            }
            
        }

    }
}
