using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    public class RadiumHelmetSummonerSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<RadiumHelmetSummonerSetPlayer>().setBonus = true;
        }

        public override string GetDescription() => Language.GetTextValue(Description).FormatWith((int)Main.LocalPlayer.GetDamage(DamageClass.Summon).ApplyTo(RadiumHelmetSummonerSetProjectile.baseBlastDamage));
    }

    public class RadiumHelmetSummonerSetPlayer : ModPlayer
    {
        public bool setBonus = false;
        public override void ResetEffects()
        {
            setBonus = false;

        }
    }
    public class RadiumHelmetSummonerSetProjectile : GlobalProjectile
    {
        //power settings
        const int cooldownRate = 120;
        const float radius = 300;
        public const int baseBlastDamage = 200;
        //

        int cooldown = 0;
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public override bool PreAI(Projectile projectile)
        {

            if (cooldown > 0)
            {
                cooldown--;
            }
            if (projectile.minion && projectile.minionSlots > 0 && projectile.active && Main.player[projectile.owner].GetModPlayer<RadiumHelmetSummonerSetPlayer>().setBonus && cooldown == 0)
            {

                for (int n = 0; n < Main.npc.Length; n++)
                {
                    if ((Main.npc[n].Center - projectile.Center).Length() < radius - 100 && Main.npc[n].CanBeChasedBy())
                    {
                        SunBlast(projectile);
                        break;
                    }
                }
            }

            return base.PreAI(projectile);
        }
        void SunBlast(Projectile projectile)
        {
            for (int i = 0; i < 100; i++)
            {
                float theta = Main.rand.NextFloat(-(float)Math.PI, (float)Math.PI);
                Dust dust = Dust.NewDustPerfect(projectile.Center, ModContent.DustType<Dusts.RadiumDust>(), PolarVector(radius / 30, theta));
                dust.noGravity = true;
            }
            cooldown = (int)(cooldownRate / projectile.minionSlots);
            Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<RadiumHelmetSummonerSetEffect_RadiumBlast>(), (int)Main.player[projectile.owner].GetDamage(DamageClass.Summon).ApplyTo(baseBlastDamage), 0f, projectile.owner, radius);

        }
        public static Vector2 PolarVector(float radius, float theta)
        {
            return new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta)) * radius;
        }


    }
}