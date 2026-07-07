using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items._BossEmperorFishron.Weapons
{
    public class FishnadoStaff_Fishnado : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fishnado");
			Main.projFrames[Projectile.type] = 6;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }
		
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(407);
        }

        public override void AI()
        {

            if (++Projectile.frameCounter >= 60)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame > 5)
                {
                    Projectile.frame = 0;
                }
            }
            bool flag64 = Projectile.type == ModContent.ProjectileType<FishnadoStaff_Fishnado>();
            Player player = Main.player[Projectile.owner];
            ZAAPlayer modPlayer = player.GetModPlayer<ZAAPlayer>();
            player.AddBuff(ModContent.BuffType<FishnadoStaff_Buff>(), 3600);
            if (flag64)
            {
                if (player.dead)
                {
                    modPlayer.Fishnado = false;
                }
                if (modPlayer.Fishnado)
                {
                    Projectile.timeLeft = 2;
                }
            }
            float num8 = 0.1f;
            float num9 = Projectile.width * 2f;
            for (int j = 0; j < 1000; j++)
            {
                if (j != Projectile.whoAmI && Main.projectile[j].active && Main.projectile[j].owner == Projectile.owner && Main.projectile[j].type == Projectile.type && Math.Abs(Projectile.position.X - Main.projectile[j].position.X) + Math.Abs(Projectile.position.Y - Main.projectile[j].position.Y) < num9)
                {
                    if (Projectile.position.X < Main.projectile[j].position.X)
                    {
                        Projectile.velocity.X = Projectile.velocity.X - num8;
                    }
                    else
                    {
                        Projectile.velocity.X = Projectile.velocity.X + num8;
                    }
                    if (Projectile.position.Y < Main.projectile[j].position.Y)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y - num8;
                    }
                    else
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y + num8;
                    }
                }
            }
            Vector2 vector = Projectile.position;
            float num10 = 400f;

            bool flag = false;
            Projectile.tileCollide = false;
            if (Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
            {
                Projectile.alpha += 20;
                if (Projectile.alpha > 150)
                {
                    Projectile.alpha = 150;
                }
            }
            else
            {
                Projectile.alpha -= 50;
                if (Projectile.alpha < 60)
                {
                    Projectile.alpha = 60;
                }
            }
            Vector2 center = Main.player[Projectile.owner].Center;
            Vector2 value = new Vector2(0.5f);
            if (Projectile.type == ProjectileID.UFOMinion)
            {
                value.Y = 0f;
            }
            NPC ownerMinionAttackTargetNPC = Projectile.OwnerMinionAttackTargetNPC;
            if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy(this, false))
            {
                Vector2 vector2 = ownerMinionAttackTargetNPC.position + ownerMinionAttackTargetNPC.Size * value;
                float num12 = Vector2.Distance(vector2, center);
                if ((Vector2.Distance(center, vector) > num12 && num12 < num10 || !flag) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, ownerMinionAttackTargetNPC.position, ownerMinionAttackTargetNPC.width, ownerMinionAttackTargetNPC.height))
                {
                    num10 = num12;
                    vector = vector2;
                    flag = true;
                }
            }
            if (!flag)
            {
                for (int k = 0; k < 200; k++)
                {
                    NPC nPC = Main.npc[k];
                    if (nPC.CanBeChasedBy(this, false))
                    {
                        Vector2 vector3 = nPC.position + nPC.Size * value;
                        float num13 = Vector2.Distance(vector3, center);
                        if ((Vector2.Distance(center, vector) > num13 && num13 < num10 || !flag) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, nPC.position, nPC.width, nPC.height))
                        {
                            num10 = num13;
                            vector = vector3;
                            flag = true;
                        }
                    }
                }
            }
            int num16 = 500;
            if (flag)
            {
                num16 = 1200;
            }
            float num17 = Vector2.Distance(player.Center, Projectile.Center);
            if (num17 > num16)
            {
                Projectile.ai[0] = 1f;
                Projectile.netUpdate = true;
            }
            if (Projectile.ai[0] == 1f)
            {
                Projectile.tileCollide = false;
            }
            if (flag && Projectile.ai[0] == 0f)
            {
                Vector2 vector4 = vector - Projectile.Center;
                float num18 = vector4.Length();
                vector4.Normalize();
                if (num18 > 400f)
                {
                    float scaleFactor = 2f;
                    vector4 *= scaleFactor;
                    Projectile.velocity = (Projectile.velocity * 20f + vector4) / 21f;
                }
                else
                {
                    Projectile.velocity *= 0.96f;
                }
                if (num18 > 200f)
                {
                    float scaleFactor2 = 6f;
                    vector4 *= scaleFactor2;
                    Projectile.velocity.X = (Projectile.velocity.X * 40f + vector4.X) / 41f;
                    Projectile.velocity.Y = (Projectile.velocity.Y * 40f + vector4.Y) / 41f;
                }
                else if (Projectile.velocity.Y > -1f)
                {
                    Projectile.velocity.Y = Projectile.velocity.Y - 0.1f;
                }
            }
            else
            {
                if (!Collision.CanHitLine(Projectile.Center, 1, 1, Main.player[Projectile.owner].Center, 1, 1))
                {
                    Projectile.ai[0] = 1f;
                }
                float num22 = 9f;
                Vector2 center2 = Projectile.Center;
                Vector2 vector6 = player.Center - center2 + new Vector2(0f, -60f);
                vector6 += new Vector2(0f, 40f);
                float num24 = vector6.Length();
                if (num24 > 200f && num22 < 9f)
                {
                    num22 = 9f;
                }
                if (num24 < 100f && Projectile.ai[0] == 1f && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                {
                    Projectile.ai[0] = 0f;
                    Projectile.netUpdate = true;
                }
                if (num24 > 2000f)
                {
                    Projectile.position.X = Main.player[Projectile.owner].Center.X - Projectile.width / 2;
                    Projectile.position.Y = Main.player[Projectile.owner].Center.Y - Projectile.width / 2;
                }
                if (Math.Abs(vector6.X) > 40f || Math.Abs(vector6.Y) > 10f)
                {
                    vector6.Normalize();
                    vector6 *= num22;
                    vector6 *= new Vector2(1.25f, 0.65f);
                    Projectile.velocity = (Projectile.velocity * 20f + vector6) / 21f;
                }
                else
                {
                    if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
                    {
                        Projectile.velocity.X = -0.15f;
                        Projectile.velocity.Y = -0.05f;
                    }
                    Projectile.velocity *= 1.01f;
                }
            }
            Projectile.rotation = Projectile.velocity.X * 0.05f;
            Projectile.frameCounter++;
            if (Projectile.velocity.X > 0f)
            {
                Projectile.spriteDirection = Projectile.direction = -1;
            }
            else if (Projectile.velocity.X < 0f)
            {
                Projectile.spriteDirection = Projectile.direction = 1;
            }
            if (Projectile.ai[1] > 0f)
            {
                Projectile.ai[1] += 1f;
                if (Main.rand.NextBool(3))
                {
                    Projectile.ai[1] += 1f;
                }
            }
            if (Projectile.ai[1] > 60f)
            {
                Projectile.ai[1] = 0f;
                Projectile.netUpdate = true;
            }
            if (Projectile.ai[0] == 0f)
            {
                float scaleFactor4 = 5f;
                int num29 = 408;

                if (flag)
                {
                    if (Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                    {
                        return;
                    }
                    else if (Projectile.ai[1] == 0f)
                    {
                        Projectile.ai[1] += 1f;
                        if (Main.myPlayer == Projectile.owner)
                        {
                            Vector2 value4 = vector - Projectile.Center;
                            value4.Normalize();
                            value4 *= scaleFactor4;
                            int num33 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, value4.X*4, value4.Y*4, num29, Projectile.damage, 0f, Main.myPlayer, 0f, 0f);
                            Main.projectile[num33].extraUpdates = 1;
                            Main.projectile[num33].minion = true;
                            Main.projectile[num33].timeLeft = 300;
                            Main.projectile[num33].netUpdate = true;
                            Projectile.netUpdate = true;
                        }
                    }
                }
            }
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 7)
            {
                Projectile.frame += 1;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame >= 6)
            {
                Projectile.frame = 0;
            }
        }
    }
}