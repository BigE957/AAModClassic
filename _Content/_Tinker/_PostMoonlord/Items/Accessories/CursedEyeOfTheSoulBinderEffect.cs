using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Desert._PostMoonlord.Items.Materials;
using AAModClassic._Content.Inferno.__Hardmode.Items.Accessories;
using AAModClassic._Content.OldOnesArmy.___PreHardmode.Items.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Tinker._PostMoonlord.Items.Accessories
{
    public class CursedEyeOfTheSoulBinderEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<CursedEyeOfTheSoulBinderPlayer>().effect = true;
        }
    }

    public class CursedEyeOfTheSoulBinderPlayer : EquipmentEffectPlayer
    {

    }

    public class CursedEyeOfTheSoulBinderProjectile : GlobalProjectile
    {
        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (projectile.type != ProjectileID.SpectreWrath && (projectile.minion || projectile.sentry) && Main.player[projectile.owner].GetModPlayer<CursedEyeOfTheSoulBinderPlayer>().effect)
            {
                int num = Main.rand.Next(1, 3);
                for (int i = 0; i < num; i++)
                {
                    ghostHurt(projectile, projectile.damage, new Vector2(target.Center.X, target.Center.Y));
                }
            }
        }

        private static void ghostHurt(Projectile projectile, int dmg, Vector2 Position)
        {
            int num = projectile.damage / 2;
            if (dmg / 2 <= 1)
            {
                return;
            }
            int num2 = 1000;
            if (Main.player[Main.myPlayer].ghostDmg > num2)
            {
                return;
            }
            Main.player[Main.myPlayer].ghostDmg += num;
            int[] array = new int[200];
            int num3 = 0;
            int num4 = 0;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].CanBeChasedBy(projectile, false))
                {
                    float num5 = Math.Abs(Main.npc[i].position.X + Main.npc[i].width / 2 - projectile.position.X + projectile.width / 2) + Math.Abs(Main.npc[i].position.Y + Main.npc[i].height / 2 - projectile.position.Y + projectile.height / 2);
                    if (num5 < 800f)
                    {
                        if (Collision.CanHit(projectile.position, 1, 1, Main.npc[i].position, Main.npc[i].width, Main.npc[i].height) && num5 > 50f)
                        {
                            array[num4] = i;
                            num4++;
                        }
                        else if (num4 == 0)
                        {
                            array[num3] = i;
                            num3++;
                        }
                    }
                }
            }
            if (num3 == 0 && num4 == 0)
            {
                return;
            }
            int num6;
            if (num4 > 0)
            {
                num6 = array[Main.rand.Next(num4)];
            }
            else
            {
                num6 = array[Main.rand.Next(num3)];
            }
            float num7 = 4f;
            float num8 = Main.rand.Next(-100, 101);
            float num9 = Main.rand.Next(-100, 101);
            float num10 = (float)Math.Sqrt(num8 * num8 + num9 * num9);
            num10 = num7 / num10;
            num8 *= num10;
            num9 *= num10;
            int soul = Projectile.NewProjectile(projectile.GetSource_FromThis(), Position.X, Position.Y, num8, num9, ProjectileID.SpectreWrath, num, 0f, projectile.owner, num6, 0f);
            Main.projectile[soul].minion = true;
        }
    }
}