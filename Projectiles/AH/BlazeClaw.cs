using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.AH
{
    public class BlazeClaw : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Blaze Claw");
			Main.projFrames[Projectile.type] = 5;
			ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		}


        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 20;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.minionSlots = 0.5f;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
        }

        public override void AI()
        {
            Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;
            if (Projectile.frameCounter++ > 6)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
                if (Projectile.frame > 3)
                {
                    Projectile.frame = 0;
                }
            }
            float radius = 700f;
            float num14 = 800f;
            float num15 = 1200f;
            float num16 = 150f;
            Player player = Main.player[Projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            player.AddBuff(Mod.Find<ModBuff>("ChaosClaw").Type, 3600);
            if (player.dead)
            {
                modPlayer.ChaosClaw = false;
            }
            if (modPlayer.ChaosClaw)
            {
                Projectile.timeLeft = 2;
            }
            for (int whoAmI = 0; whoAmI < 1000; whoAmI++)
            {
                if (whoAmI != Projectile.whoAmI && Main.projectile[whoAmI].active && Main.projectile[whoAmI].owner == Projectile.owner && Math.Abs(Projectile.position.X - Main.projectile[whoAmI].position.X) + Math.Abs(Projectile.position.Y - Main.projectile[whoAmI].position.Y) < Projectile.width)
                {
                    if (Projectile.position.X < Main.projectile[whoAmI].position.X)
                    {
                        Projectile.velocity.X = Projectile.velocity.X - 0.05f;
                    }
                    else
                    {
                        Projectile.velocity.X = Projectile.velocity.X + 0.05f;
                    }
                    if (Projectile.position.Y < Main.projectile[whoAmI].position.Y)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y - 0.05f;
                    }
                    else
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y + 0.05f;
                    }
                }
            }
            Vector2 position = Projectile.position;
            bool foundTarget = false;
            if (Projectile.ai[0] != 1f)
            {
                Projectile.tileCollide = false;
            }
            if (Projectile.tileCollide && WorldGen.SolidTile(Framing.GetTileSafely((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16)))
            {
                Projectile.tileCollide = false;
            }
            if (player.HasMinionAttackTargetNPC)
			{
				NPC target = Main.npc[player.MinionAttackTargetNPC];
                if (target.CanBeChasedBy(Projectile, false))
                {
                    float distance = Vector2.Distance(target.Center, Projectile.Center);
                    if (((Vector2.Distance(Projectile.Center, position) > distance && distance < radius) || !foundTarget) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, target.position, target.width, target.height))
                    {
                        radius = distance;
                        position = target.Center;
                        foundTarget = true;
                    }
                }
			}
			else
			{
                for (int num645 = 0; num645 < 200; num645++)
                {
                    NPC target = Main.npc[num645];
                    if (target.CanBeChasedBy(Projectile, false))
                    {
                        float distance = Vector2.Distance(target.Center, Projectile.Center);
                        if (((Vector2.Distance(Projectile.Center, position) > distance && distance < radius) || !foundTarget) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, target.position, target.width, target.height))
                        {
                            radius = distance;
                            position = target.Center;
                            foundTarget = true;
                        }
                    }
                }
            }
            float num18 = num14;
            if (foundTarget)
            {
                num18 = num15;
            }
            if (Vector2.Distance(player.Center, Projectile.Center) > num18)
            {
                Projectile.ai[0] = 1f;
                Projectile.tileCollide = false;
                Projectile.netUpdate = true;
            }
            if (foundTarget && Projectile.ai[0] == 0f)
            {
                Vector2 difference = position - Projectile.Center;
                float num648 = difference.Length();
                difference.Normalize();
                if (num648 > 200f)
                {
                    float scaleFactor2 = 6f;
                    difference *= scaleFactor2;
                    Projectile.velocity = (Projectile.velocity * 40f + difference) / 41f;
                }
                else
                {
                    float num649 = 4f;
                    difference *= -num649;
                    Projectile.velocity = (Projectile.velocity * 40f + difference) / 41f;
                }
            }
            else
            {
                bool flag26 = false;
                if (!flag26)
                {
                    flag26 = Projectile.ai[0] == 1f;
                }
                float num650 = 6f;
                if (flag26)
                {
                    num650 = 15f;
                }
                Vector2 center2 = Projectile.Center;
                Vector2 vector48 = player.Center - center2 + new Vector2(0f, -60f);
                float num651 = vector48.Length();
                if (num651 > 200f && num650 < 8f)
                {
                    num650 = 8f;
                }
                if (num651 < num16 && flag26 && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                {
                    Projectile.ai[0] = 0f;
                    Projectile.netUpdate = true;
                }
                if (num651 > 2000f)
                {
                    Projectile.position.X = Main.player[Projectile.owner].Center.X - (Projectile.width / 2);
                    Projectile.position.Y = Main.player[Projectile.owner].Center.Y - (Projectile.height / 2);
                    Projectile.netUpdate = true;
                }
                if (num651 > 70f)
                {
                    vector48.Normalize();
                    vector48 *= num650;
                    Projectile.velocity = (Projectile.velocity * 40f + vector48) / 41f;
                }
                else if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
                {
                    Projectile.velocity.X = -0.15f;
                    Projectile.velocity.Y = -0.05f;
                }
            }
            if (foundTarget)
            {
				Projectile.spriteDirection = (position - Projectile.Center).X > 0 ? 1 : -1;
                Projectile.rotation = (position - Projectile.Center).ToRotation() + 1.57f;
			}
			else
			{
				Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;
				Projectile.rotation = Projectile.velocity.ToRotation() + 1.57f;
			}
            if (Projectile.ai[1] > 0f)
            {
                Projectile.ai[1] += Main.rand.Next(1, 4);
            }
            if (Projectile.ai[1] > 60f)
            {
                Projectile.ai[1] = 0f;
                Projectile.netUpdate = true;
            }
            if (Projectile.ai[0] == 0f)
            {
                float speedScale = 7f;
                int shoot = Mod.Find<ModProjectile>("BlazeBall").Type;
                if (foundTarget && Projectile.ai[1] == 0f)
                {
                    Projectile.ai[1] += 1f;
                    if (Main.myPlayer == Projectile.owner && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, position, 0, 0))
                    {
                        Vector2 speed = position - Projectile.Center;
                        speed.Normalize();
                        speed *= speedScale;
                        int num659 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, speed.X, speed.Y, shoot, Projectile.damage, 0f, Main.myPlayer, 0f, 0f);
                        Main.projectile[num659].timeLeft = 300;
                        Projectile.netUpdate = true;
                    }
                }
            }
        }
    }
}