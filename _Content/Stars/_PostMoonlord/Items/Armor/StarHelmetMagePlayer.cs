using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    public class StarHelmetMagePlayer : ModPlayer
    {
        public bool setBonus = false;
        public int[] npcCooldown = new int[Main.npc.Length];
        public bool sunSiphon = false;
        public override void ResetEffects()
        {
            setBonus = false;

        }
        public override void PreUpdate()
        {
            if (setBonus)
            {
                for (int n = 0; n < Main.npc.Length; n++)
                {
                    if (npcCooldown[n] > 0)
                    {
                        npcCooldown[n]--;
                    }
                    if (Main.npc[n].CanBeChasedBy() && npcCooldown[n] == 0 && (Main.npc[n].Center - Player.Center).Length() < 300)
                    {

                        npcCooldown[n] = 30;
                        int type = ModContent.ProjectileType<StarHelmetMagePlayer_DarkLeech>();
                        if (sunSiphon)
                        {
                            type = ModContent.ProjectileType<StarHelmetMagePlayer_SunSiphon>();
                        }

                        Projectile.NewProjectile(Main.npc[n].GetSource_FromThis(), Main.npc[n].Center, Vector2.Zero, type, (int)Player.GetDamage(DamageClass.Magic).ApplyTo(100f), 0f, Player.whoAmI, n);
                    }
                }

                int dustID = sunSiphon ? ModContent.DustType<RadiumDust>() : ModContent.DustType<DarkmatterDust>();
                for (int i = 0; i < 15; i++)
                {
                    Vector2 offset = new Vector2();
                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                    offset.X += (float)(Math.Sin(angle) * 300);
                    offset.Y += (float)(Math.Cos(angle) * 300);
                    Dust dust = Main.dust[Dust.NewDust(Player.Center + offset - new Vector2(4, 4), 0, 0, dustID, 0, 0, 100, default, 1f)];
                    dust.velocity = Player.velocity;
                    dust.noGravity = true;
                }
            }

        }

    }
}