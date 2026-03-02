using AAModClassic;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Armor.Doomsday
{
    public class ZeroMini : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Zero Construct");
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.width = 62;
            Projectile.height = 62;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 300;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            AAPlayer modPlayer = (AAPlayer)player.GetModPlayer(Mod, "AAPlayer");
            if (player.dead)
            {
                modPlayer.MiniZero = false;
            }
            if (modPlayer.MiniZero)
            {
                Projectile.timeLeft = 2;
            }
            float num619 = 0f;
            float num620 = 0f;
            float num621 = 0f;
            float num622 = 0f;
            float num623 = 0.05f;
            for (int num624 = 0; num624 < 1000; num624++)
            {
                if (num624 != Projectile.whoAmI && Main.projectile[num624].active && Main.projectile[num624].owner == Projectile.owner && Math.Abs(Projectile.position.X - Main.projectile[num624].position.X) + Math.Abs(Projectile.position.Y - Main.projectile[num624].position.Y) < Projectile.width)
                {
                    if (Projectile.position.X < Main.projectile[num624].position.X)
                    {
                        Projectile.velocity.X = Projectile.velocity.X - num623;
                    }
                    else
                    {
                        Projectile.velocity.X = Projectile.velocity.X + num623;
                    }
                    if (Projectile.position.Y < Main.projectile[num624].position.Y)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y - num623;
                    }
                    else
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y + num623;
                    }
                }
            }
            Lighting.AddLight(Projectile.Center, 0.8f, 0.3f, 0.1f);
            bool flag23 = false;
            if (Projectile.ai[0] >= 3f && Projectile.ai[0] <= 5f)
            {
                int num625 = 2;
                flag23 = true;
                Projectile.velocity *= 0.9f;
                Projectile.ai[1] += 1f;
                int num626 = (int)Projectile.ai[1] / num625 + (int)(Projectile.ai[0] - 3f) * 8;
                if (num626 < 4)
                {
                    Projectile.frame = 17 + num626;
                }
                else if (num626 < 5)
                {
                    Projectile.frame = 0;
                }
                else if (num626 < 8)
                {
                    Projectile.frame = 1 + num626 - 5;
                }
                else if (num626 < 11)
                {
                    Projectile.frame = 11 - num626;
                }
                else if (num626 < 12)
                {
                    Projectile.frame = 0;
                }
                else if (num626 < 16)
                {
                    Projectile.frame = num626 - 2;
                }
                else if (num626 < 20)
                {
                    Projectile.frame = 29 - num626;
                }
                else if (num626 < 21)
                {
                    Projectile.frame = 0;
                }
                else
                {
                    Projectile.frame = num626 - 4;
                }
                if (Projectile.ai[1] > num625 * 8)
                {
                    Projectile.ai[0] -= 3f;
                    Projectile.ai[1] = 0f;
                }
            }
            if (Projectile.ai[0] >= 6f && Projectile.ai[0] <= 8f)
            {
                Projectile.ai[1] += 1f;
                Projectile.MaxUpdates = 2;
                if (Projectile.ai[0] == 7f)
                {
                    Projectile.rotation = Projectile.velocity.ToRotation() + 3.14159274f;
                }
                else
                {
                    Projectile.rotation += 0.5235988f;
                }
                int num627 = 0;
                switch ((int)Projectile.ai[0])
                {
                    case 6:
                        Projectile.frame = 5;
                        num627 = 40;
                        break;
                    case 7:
                        Projectile.frame = 13;
                        num627 = 30;
                        break;
                    case 8:
                        Projectile.frame = 17;
                        num627 = 30;
                        break;
                }
                if (Projectile.ai[1] > num627)
                {
                    Projectile.ai[1] = 1f;
                    Projectile.ai[0] -= 6f;
                    Projectile.localAI[0] += 1f;
                    Projectile.extraUpdates = 0;
                    Projectile.numUpdates = 0;
                    Projectile.netUpdate = true;
                }
                else
                {
                    flag23 = true;
                }
                if (Projectile.ai[0] == 8f)
                {
                    for (int num628 = 0; num628 < 4; num628++)
                    {
                        int num629 = Utils.SelectRandom(Main.rand, new int[]
                        {
                                                                    226,
                                                                    228,
                                                                    75
                        });
                        int num630 = Dust.NewDust(Projectile.Center, 0, 0, num629, 0f, 0f, 0);
                        Dust dust2 = Main.dust[num630];
                        Vector2 value17 = Vector2.One.RotatedBy(num628 * 1.57079637f, default).RotatedBy(Projectile.rotation, default);
                        dust2.position = Projectile.Center + value17 * 10f;
                        dust2.velocity = value17 * 1f;
                        dust2.scale = 0.6f + Main.rand.NextFloat() * 0.5f;
                        dust2.noGravity = true;
                    }
                }
            }
            if (flag23)
            {
                return;
            }
            Vector2 vector44 = Projectile.position;
            bool flag24 = false;
            if (Projectile.ai[0] < 9f)
            {
                Projectile.tileCollide = true;
            }
            if (Projectile.tileCollide && WorldGen.SolidTile(Framing.GetTileSafely((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16)))
            {
                Projectile.tileCollide = false;
            }
            NPC ownerMinionAttackTargetNPC3 = Projectile.OwnerMinionAttackTargetNPC;
            if (ownerMinionAttackTargetNPC3 != null && ownerMinionAttackTargetNPC3.CanBeChasedBy(this, false))
            {
                float num631 = Vector2.Distance(ownerMinionAttackTargetNPC3.Center, Projectile.Center);
                if (((Vector2.Distance(Projectile.Center, vector44) > num631 && num631 < num619) || !flag24) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, ownerMinionAttackTargetNPC3.position, ownerMinionAttackTargetNPC3.width, ownerMinionAttackTargetNPC3.height))
                {
                    num619 = num631;
                    vector44 = ownerMinionAttackTargetNPC3.Center;
                    flag24 = true;
                }
            }
            if (!flag24)
            {
                for (int num632 = 0; num632 < 200; num632++)
                {
                    NPC nPC2 = Main.npc[num632];
                    if (nPC2.CanBeChasedBy(this, false))
                    {
                        float num633 = Vector2.Distance(nPC2.Center, Projectile.Center);
                        if (((Vector2.Distance(Projectile.Center, vector44) > num633 && num633 < num619) || !flag24) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, nPC2.position, nPC2.width, nPC2.height))
                        {
                            num619 = num633;
                            vector44 = nPC2.Center;
                            flag24 = true;
                        }
                    }
                }
            }
            float num634 = num620;
            if (flag24)
            {
                num634 = num621;
            }
            if (Vector2.Distance(player.Center, Projectile.Center) > num634)
            {
                Projectile.ai[0] += 3 * (3 - (int)(Projectile.ai[0] / 3f));
                Projectile.tileCollide = false;
                Projectile.netUpdate = true;
            }
            bool flag25 = Projectile.ai[0] >= 9f;
            float num637 = 12f;
            if (flag25)
            {
                num637 = 15f;
            }
            Vector2 center2 = Projectile.Center;
            Vector2 vector46 = player.Center - center2 + new Vector2(0f, -60f);
            float num638 = vector46.Length();
            if (num638 > 200f && num637 < 8f)
            {
                num637 = 8f;
            }
            if (num638 < num622 && flag25 && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
            {
                Projectile.ai[0] -= 9f;
                Projectile.netUpdate = true;
            }
            if (num638 > 2000f)
            {
                Projectile.position.X = Main.player[Projectile.owner].Center.X - Projectile.width / 2;
                Projectile.position.Y = Main.player[Projectile.owner].Center.Y - Projectile.height / 2;
                Projectile.netUpdate = true;
            }
            if (num638 > 70f)
            {
                vector46.Normalize();
                vector46 *= num637;
                Projectile.velocity = (Projectile.velocity * 40f + vector46) / 41f;
            }
            else if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
            {
                Projectile.velocity.X = -0.15f;
                Projectile.velocity.Y = -0.05f;
            }
            if (Projectile.ai[0] < 3f || Projectile.ai[0] >= 9f)
            {
                Projectile.rotation += Projectile.velocity.X * 0.04f;
            }
            if (Projectile.ai[1] > 0f)
            {
                Projectile.ai[1] += 1f;
                int num644 = 10;
                if (Projectile.ai[1] > num644)
                {
                    Projectile.ai[1] = 0f;
                    Projectile.netUpdate = true;
                }
            }
            else if (Projectile.ai[0] < 3f)
            {
                int num647 = 0;
                switch ((int)Projectile.ai[0])
                {
                    case 0:
                    case 3:
                    case 6:
                        num647 = 400;
                        break;
                    case 1:
                    case 4:
                    case 7:
                        num647 = 400;
                        break;
                    case 2:
                    case 5:
                    case 8:
                        num647 = 600;
                        break;
                }
                if (Projectile.ai[1] == 0f && flag24 && num619 < num647)
                {
                    Projectile.ai[1] += 1f;
                    if (Main.myPlayer == Projectile.owner)
                    {
                        if (Projectile.localAI[0] >= 3f)
                        {
                            Projectile.ai[0] += 4f;
                            if (Projectile.ai[0] == 6f)
                            {
                                Projectile.ai[0] = 3f;
                            }
                            Projectile.localAI[0] = 0f;
                            return;
                        }
                        Projectile.ai[0] += 6f;
                        Vector2 value21 = vector44 - Projectile.Center;
                        value21.Normalize();
                        float scaleFactor4 = (Projectile.ai[0] == 8f) ? 12f : 10f;
                        Projectile.velocity = value21 * scaleFactor4;
                        Projectile.netUpdate = true;
                        return;
                    }
                }
            }


            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 8)
            {
                Projectile.frameCounter = 0;
                Projectile.frame += 1;
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }
        }
    }
}