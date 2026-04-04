using System;
using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityLeviathan
{
    class DeityLeviathan_RazorbladeRift : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.hostile = true;
            Projectile.penetrate = -1;
            Projectile.aiStyle = -1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 300;
        }

        public override void AI()
        {
            if (Projectile.ai[1] > 0f)
            {
                int num611 = (int)Projectile.ai[1] - 1;
                if (num611 < 255)
                {
                    Projectile.localAI[0] += 1f;
                    if (Projectile.localAI[0] > 10f)
                    {
                        int num612 = 6;
                        for (int num613 = 0; num613 < num612; num613++)
                        {
                            Vector2 vector43 = Vector2.Normalize(Projectile.velocity) * new Vector2(Projectile.width / 2f, Projectile.height) * 0.75f;
                            vector43 = vector43.RotatedBy((num613 - (num612 / 2 - 1)) * 3.1415926535897931 / (double)(float)num612, default) + Projectile.Center;
                            Vector2 value15 = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - 1.57079637f).ToRotationVector2() * Main.rand.Next(3, 8);
                            int num614 = Dust.NewDust(vector43 + value15, 0, 0, ModContent.DustType<CthulhuDust>(), value15.X * 2f, value15.Y * 2f, 100, default, 1.4f);
                            Main.dust[num614].noGravity = true;
                            Main.dust[num614].noLight = true;
                            Main.dust[num614].velocity /= 4f;
                            Main.dust[num614].velocity -= Projectile.velocity;
                        }
                        Projectile.alpha -= 5;
                        if (Projectile.alpha < 100)
                        {
                            Projectile.alpha = 100;
                        }
                        Projectile.rotation += Projectile.velocity.X * 0.1f;
                        Projectile.frame = (int)(Projectile.localAI[0] / 3f) % 3;
                    }
                    Vector2 value16 = Main.player[num611].Center - Projectile.Center;
                    float num615 = 4f;
                    num615 += Projectile.localAI[0] / 20f;
                    Projectile.velocity = Vector2.Normalize(value16) * num615;
                    if (value16.Length() < 50f)
                    {
                        Projectile.Kill();
                    }
                }
            }
            else
            {
                float num616 = 0.209439516f;
                float num617 = 4f;
                float num618 = (float)(Math.Cos((double)(num616 * Projectile.ai[0])) - 0.5) * num617;
                Projectile.velocity.Y = Projectile.velocity.Y - num618;
                Projectile.ai[0] += 1f;
                num618 = (float)(Math.Cos((double)(num616 * Projectile.ai[0])) - 0.5) * num617;
                Projectile.velocity.Y = Projectile.velocity.Y + num618;
                Projectile.localAI[0] += 1f;
                if (Projectile.localAI[0] > 10f)
                {
                    Projectile.alpha -= 5;
                    if (Projectile.alpha < 100)
                    {
                        Projectile.alpha = 100;
                    }
                    Projectile.rotation += Projectile.velocity.X * 0.1f;
                    Projectile.frame = (int)(Projectile.localAI[0] / 3f) % 3;
                }
            }
            if (Projectile.wet)
            {
                Projectile.position.Y = Projectile.position.Y - 16f;
                Projectile.Kill();
                return;
            }
        }

        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 400;

            int selectedTarget = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (n.CanBeChasedBy(Projectile) && (!n.wet || homingCanAimAtWetEnemies))
                {
                    float distance = Projectile.Distance(n.Center);
                    if (distance <= homingMaximumRangeInPixels &&
                        (
                            selectedTarget == -1 || //there is no selected target
                            Projectile.Distance(Main.npc[selectedTarget].Center) > distance) //or we are closer to this target than the already selected target
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Wet, 1000);
        }

        public override void OnKill(int timeleft)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath19, Projectile.Center);
            int num325 = 36;
            for (int num326 = 0; num326 < num325; num326++)
            {
                Vector2 vector8 = Vector2.Normalize(Projectile.velocity) * new Vector2(Projectile.width / 2f, Projectile.height) * 0.75f;
                vector8 = vector8.RotatedBy((double)((num326 - (num325 / 2 - 1)) * 6.28318548f / num325), default) + Projectile.Center;
                Vector2 vector9 = vector8 - Projectile.Center;
                int num327 = Dust.NewDust(vector8 + vector9, 0, 0, ModContent.DustType<CthulhuDust>(), vector9.X * 2f, vector9.Y * 2f, 100, default, 1.4f);
                Main.dust[num327].noGravity = true;
                Main.dust[num327].noLight = true;
                Main.dust[num327].velocity = vector9;
            }
            if (Projectile.owner == Main.myPlayer)
            {
                if (Projectile.ai[1] < 1f)
                {
                    int num328 = Main.expertMode ? 25 : 40;
                    int num329 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X - Projectile.direction * 30, Projectile.Center.Y - 4f, (float)-(float)Projectile.direction * 0.01f, 0f, ModContent.ProjectileType<Leviacane>(), num328, 4f, Projectile.owner, 16f, 15f);
                    Main.projectile[num329].netUpdate = true;
                }
                else
                {
                    int num330 = (int)(Projectile.Center.Y / 16f);
                    int num331 = (int)(Projectile.Center.X / 16f);
                    int num332 = 100;
                    if (num331 < 10)
                    {
                        num331 = 10;
                    }
                    if (num331 > Main.maxTilesX - 10)
                    {
                        num331 = Main.maxTilesX - 10;
                    }
                    if (num330 < 10)
                    {
                        num330 = 10;
                    }
                    if (num330 > Main.maxTilesY - num332 - 10)
                    {
                        num330 = Main.maxTilesY - num332 - 10;
                    }
                    for (int num333 = num330; num333 < num330 + num332; num333++)
                    {
                        Tile tile = Main.tile[num331, num333];
                        if (tile.HasTile && (Main.tileSolid[tile.TileType] || tile.LiquidAmount != 0))
                        {
                            num330 = num333;
                            break;
                        }
                    }
                    int num334 = Main.expertMode ? 100 : 140;
                    int num335 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), num331 * 16 + 8, num330 * 16 - 24, 0f, 0f, ModContent.ProjectileType<Leviacane2>(), num334, 4f, Main.myPlayer, 16f, 24f);
                    Main.projectile[num335].netUpdate = true;
                }
            }
        }
    }
}