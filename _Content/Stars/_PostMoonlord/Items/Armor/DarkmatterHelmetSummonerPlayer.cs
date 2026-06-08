using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    public class DarkmatterHelmetSummonerPlayer : ModPlayer
    {
        public bool setBonus = false;
        public override void ResetEffects()
        {
            setBonus = false;

        }
    }
    public class DarkmatterHelmetSummonerPlayer_DarkMinions : GlobalProjectile
    {
        //power settings
        const int cooldownRate = 120;
        const float radius = 300;
        const int damageReductionPerBlast = 30;
        //

        int cooldown = 0;
        public int reduceDamage = 0;
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
            if (projectile.minion && projectile.minionSlots > 0 && projectile.active && Main.player[projectile.owner].GetModPlayer<DarkmatterHelmetSummonerPlayer>().setBonus && cooldown == 0)
            {

                for (int p = 0; p < Main.projectile.Length; p++)
                {
                    if ((Main.projectile[p].Center - projectile.Center).Length() < radius - 100 && Main.projectile[p].active && Main.projectile[p].hostile && Main.projectile[p].damage > 0)
                    {
                        DarkBlast(projectile);
                        break;
                    }
                }
            }
            if (projectile.damage > 0 && projectile.hostile && reduceDamage > EstimatedDamage(projectile))
            {
                projectile.Kill();
            }
            return base.PreAI(projectile);
        }
        void DarkBlast(Projectile projectile)
        {
            for (int i = 0; i < 100; i++)
            {
                float theta = Main.rand.NextFloat(-(float)Math.PI, (float)Math.PI);
                Dust dust = Dust.NewDustPerfect(projectile.Center, ModContent.DustType<Dusts.DarkmatterDust>(), PolarVector(radius / 30, theta));
                dust.noGravity = true;
            }
            cooldown = (int)(cooldownRate / projectile.minionSlots);
            for (int p = 0; p < Main.projectile.Length; p++)
            {
                if ((Main.projectile[p].Center - projectile.Center).Length() < radius && Main.projectile[p].active && Main.projectile[p].hostile && Main.projectile[p].damage > 0)
                {
                    Main.projectile[p].GetGlobalProjectile<DarkmatterHelmetSummonerPlayer_DarkMinions>().reduceDamage += damageReductionPerBlast;
                }
            }
        }
        static int EstimatedDamage(Projectile projectile)
        {
            return projectile.damage * (Main.expertMode ? 4 : 2);
        }
        public override Color? GetAlpha(Projectile projectile, Color lightColor)
        {
            if (projectile.GetGlobalProjectile<DarkmatterHelmetSummonerPlayer_DarkMinions>().reduceDamage > 0)
            {
                float v = projectile.GetGlobalProjectile<DarkmatterHelmetSummonerPlayer_DarkMinions>().reduceDamage / (float)EstimatedDamage(projectile);

                lightColor.R = (byte)(lightColor.R * (1f - lightColor.R * v * .8f));
                lightColor.G = (byte)(lightColor.G * (1f - lightColor.R * v * .8f));
                lightColor.B = (byte)(lightColor.B * (1f - lightColor.R * v * .8f));

                return lightColor;
            }
            return null;
        }
        public override void ModifyHitPlayer(Projectile projectile, Player target, ref Player.HurtModifiers modifiers)
        {
            modifiers.FinalDamage.Flat -= (int)(reduceDamage * (Main.expertMode ? .25f : .5f));
        }
        public static Vector2 PolarVector(float radius, float theta)
        {
            return new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta)) * radius;
        }


    }
}