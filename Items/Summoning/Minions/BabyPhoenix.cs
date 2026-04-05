using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Summoning.Minions
{
    public class BabyPhoenix : ModProjectile
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Baby Phoenix");
            Main.projFrames[Projectile.type] = 8;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        } 
        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.tileCollide = false;
        }
        

        public override void AI()
        {
            for (int num527 = 0; num527 < 1000; num527++)
            {
                if (num527 != Projectile.whoAmI && Main.projectile[num527].active && Main.projectile[num527].owner == Projectile.owner && Main.projectile[num527].type == Projectile.type && Math.Abs(Projectile.position.X - Main.projectile[num527].position.X) + Math.Abs(Projectile.position.Y - Main.projectile[num527].position.Y) < Projectile.width)
                {
                    if (Projectile.position.X < Main.projectile[num527].position.X)
                    {
                        Projectile.velocity.X = Projectile.velocity.X - 0.05f;
                    }
                    else
                    {
                        Projectile.velocity.X = Projectile.velocity.X + 0.05f;
                    }
                    if (Projectile.position.Y < Main.projectile[num527].position.Y)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y - 0.05f;
                    }
                    else
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y + 0.05f;
                    }
                }
            }

            bool flag64 = Projectile.type == ModContent.ProjectileType<Items.Summoning.Minions.BabyPhoenix>();
            Player player = Main.player[Projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            player.AddBuff(ModContent.BuffType<BabyPhoenix>(), 3600);
            if (flag64)
            {
                if (player.dead)
                {
                    modPlayer.BabyPhoenix = false;
                }
                if (modPlayer.BabyPhoenix)
                {
                    Projectile.timeLeft = 2;
                }
            }
            float num528 = Projectile.position.X;
            float num529 = Projectile.position.Y;
            float num530 = 900f;
            bool flag19 = false;
            int num531 = 500;
            if (Projectile.ai[1] != 0f || Projectile.friendly)
            {
                num531 = 1400;
            }
            if (Math.Abs(Projectile.Center.X - Main.player[Projectile.owner].Center.X) + Math.Abs(Projectile.Center.Y - Main.player[Projectile.owner].Center.Y) > num531)
            {
                Projectile.ai[0] = 1f;
            }
            if (Projectile.ai[0] == 0f)
            {
                Projectile.tileCollide = false;
                NPC ownerMinionAttackTargetNPC2 = Projectile.OwnerMinionAttackTargetNPC;
                if (ownerMinionAttackTargetNPC2 != null && ownerMinionAttackTargetNPC2.CanBeChasedBy(this, false))
                {
                    float num532 = ownerMinionAttackTargetNPC2.position.X + ownerMinionAttackTargetNPC2.width / 2;
                    float num533 = ownerMinionAttackTargetNPC2.position.Y + ownerMinionAttackTargetNPC2.height / 2;
                    float num534 = Math.Abs(Projectile.position.X + Projectile.width / 2 - num532) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - num533);
                    if (num534 < num530 && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, ownerMinionAttackTargetNPC2.position, ownerMinionAttackTargetNPC2.width, ownerMinionAttackTargetNPC2.height))
                    {
                        num530 = num534;
                        num528 = num532;
                        num529 = num533;
                        flag19 = true;
                    }
                }
                if (!flag19)
                {
                    for (int num535 = 0; num535 < 200; num535++)
                    {
                        if (Main.npc[num535].CanBeChasedBy(this, false))
                        {
                            float num536 = Main.npc[num535].position.X + Main.npc[num535].width / 2;
                            float num537 = Main.npc[num535].position.Y + Main.npc[num535].height / 2;
                            float num538 = Math.Abs(Projectile.position.X + Projectile.width / 2 - num536) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - num537);
                            if (num538 < num530 && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[num535].position, Main.npc[num535].width, Main.npc[num535].height))
                            {
                                num530 = num538;
                                num528 = num536;
                                num529 = num537;
                                flag19 = true;
                            }
                        }
                    }
                }
            }
            else
            {
                Projectile.tileCollide = false;
            }
            if (!flag19)
            {
                Projectile.friendly = true;
                float num539 = 8f;
                if (Projectile.ai[0] == 1f)
                {
                    num539 = 12f;
                }
                Vector2 vector38 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
                float num540 = Main.player[Projectile.owner].Center.X - vector38.X;
                float num541 = Main.player[Projectile.owner].Center.Y - vector38.Y - 60f;
                float num542 = (float)Math.Sqrt(num540 * num540 + num541 * num541);
                if (num542 < 100f && Projectile.ai[0] == 1f && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                {
                    Projectile.ai[0] = 0f;
                }
                if (num542 > 2000f)
                {
                    Projectile.position.X = Main.player[Projectile.owner].Center.X - Projectile.width / 2;
                    Projectile.position.Y = Main.player[Projectile.owner].Center.Y - Projectile.width / 2;
                }
                if (num542 > 70f)
                {
                    num542 = num539 / num542;
                    num540 *= num542;
                    num541 *= num542;
                    Projectile.velocity.X = (Projectile.velocity.X * 20f + num540) / 21f;
                    Projectile.velocity.Y = (Projectile.velocity.Y * 20f + num541) / 21f;
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
                Projectile.friendly = false;
                Projectile.rotation = Projectile.velocity.X * 0.05f;
                Projectile.frameCounter++;
                if (Projectile.frameCounter >= 4)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame++;
                }
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
                if (Math.Abs(Projectile.velocity.X) > 0.2)
                {
                    Projectile.spriteDirection = -Projectile.direction;
                    return;
                }
            }
            else
            {
                if (Projectile.ai[1] == -1f)
                {
                    Projectile.ai[1] = 17f;
                }
                if (Projectile.ai[1] > 0f)
                {
                    Projectile.ai[1] -= 1f;
                }
                if (Projectile.ai[1] == 0f)
                {
                    Projectile.friendly = true;
                    float num543 = 8f;
                    Vector2 vector39 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
                    float num544 = num528 - vector39.X;
                    float num545 = num529 - vector39.Y;
                    float num546 = (float)Math.Sqrt(num544 * num544 + num545 * num545);
                    if (num546 < 100f)
                    {
                        num543 = 10f;
                    }
                    num546 = num543 / num546;
                    num544 *= num546;
                    num545 *= num546;
                    Projectile.velocity.X = (Projectile.velocity.X * 14f + num544) / 15f;
                    Projectile.velocity.Y = (Projectile.velocity.Y * 14f + num545) / 15f;
                }
                else
                {
                    Projectile.friendly = false;
                    if (Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y) < 10f)
                    {
                        Projectile.velocity *= 1.05f;
                    }
                }
                Projectile.rotation = Projectile.velocity.X * 0.05f;
                Projectile.frameCounter++;
                if (Projectile.frameCounter >= 4)
                {
                    Projectile.frameCounter = 0;
                    Projectile.frame++;
                }
                if (Projectile.frame < 4)
                {
                    Projectile.frame = 4;
                }
                if (Projectile.frame > 7)
                {
                    Projectile.frame = 4;
                }
                if (Math.Abs(Projectile.velocity.X) > 0.2)
                {
                    Projectile.spriteDirection = -Projectile.direction;
                    return;
                }
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 1000);
        }
    }
}