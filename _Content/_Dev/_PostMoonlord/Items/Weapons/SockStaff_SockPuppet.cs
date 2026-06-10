using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev._PostMoonlord.Items.Weapons
{
    public class SockStaff_SockPuppet : ModProjectile
    {

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sock Puppet");
			Main.projFrames[Projectile.type] = 8;
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		}
    	
        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 60;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.minionSlots = 1;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
        }

        public int FrameTimer = 0;

        public override void AI()
        {
            bool flag64 = Projectile.type == ModContent.ProjectileType<SockStaff_SockPuppet>();
            Player player = Main.player[Projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            player.AddBuff(ModContent.BuffType<SockPuppet_Buff>(), 3600);
            if (flag64)
            {
                if (player.dead)
                {
                    modPlayer.Sock = false;
                }
                if (modPlayer.Sock)
                {
                    Projectile.timeLeft = 2;
                }
            }

            float num633 = 700f;
            float num634 = 800f;
            float num635 = 1200f;
            float num636 = 150f;
            float num637 = 0.05f;
            for (int num638 = 0; num638 < 1000; num638++)
            {
                bool flag23 = Main.projectile[num638].type == ModContent.ProjectileType<SockStaff_SockPuppet>();
                if (num638 != Projectile.whoAmI && Main.projectile[num638].active && Main.projectile[num638].owner == Projectile.owner && flag23 && Math.Abs(Projectile.position.X - Main.projectile[num638].position.X) + Math.Abs(Projectile.position.Y - Main.projectile[num638].position.Y) < Projectile.width)
                {
                    if (Projectile.position.X < Main.projectile[num638].position.X)
                    {
                        Projectile.velocity.X = Projectile.velocity.X - num637;
                    }
                    else
                    {
                        Projectile.velocity.X = Projectile.velocity.X + num637;
                    }
                    if (Projectile.position.Y < Main.projectile[num638].position.Y)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y - num637;
                    }
                    else
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y + num637;
                    }
                }
            }
            bool flag24 = false;
            if (flag24)
            {
                return;
            }
            Vector2 vector46 = Projectile.position;
            bool flag25 = false;
            if (Projectile.ai[0] != 1f)
            {
                Projectile.tileCollide = false;
            }
            if (Projectile.tileCollide && WorldGen.SolidTile(Framing.GetTileSafely((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16)))
            {
                Projectile.tileCollide = false;
            }
            bool AttackFrame = false;
            if (player.HasMinionAttackTargetNPC)
            {
                NPC npc = Main.npc[player.MinionAttackTargetNPC];
                AttackFrame = true;
                if (Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
                {
                    num633 = Vector2.Distance(Projectile.Center, vector46);
                    vector46 = npc.Center;
                    flag25 = true;
                }
            }
            else for (int k = 0; k < 200; k++)
            {
                NPC npc = Main.npc[k];
                if (npc.CanBeChasedBy(this, false))
                {
                    AttackFrame = true;
                    float num646 = Vector2.Distance(npc.Center, Projectile.Center);
                    if ((num646 < num633 || !flag25) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
                    {
                        num633 = num646;
                        vector46 = npc.Center;
                        flag25 = true;
                    }
                }
            }
            float num647 = num634;
            if (flag25)
            {
                num647 = num635;
            }
            if (Vector2.Distance(player.Center, Projectile.Center) > num647)
            {
                Projectile.ai[0] = 1f;
                Projectile.tileCollide = false;
                Projectile.netUpdate = true;
            }
            if (flag25 && Projectile.ai[0] == 0f)
            {
                Vector2 vector47 = vector46 - Projectile.Center;
                float num648 = vector47.Length();
                vector47.Normalize();
                if (num648 > 200f)
                {
                    float scaleFactor2 = 6f;
                    vector47 *= scaleFactor2;
                    Projectile.velocity = (Projectile.velocity * 40f + vector47) / 41f;
                }
                else
                {
                    float num649 = 4f;
                    vector47 *= -num649;
                    Projectile.velocity = (Projectile.velocity * 40f + vector47) / 41f;
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
                if (num651 < num636 && flag26 && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                {
                    Projectile.ai[0] = 0f;
                    Projectile.netUpdate = true;
                }
                if (num651 > 2000f)
                {
                    Projectile.position.X = Main.player[Projectile.owner].Center.X - Projectile.width / 2;
                    Projectile.position.Y = Main.player[Projectile.owner].Center.Y - Projectile.height / 2;
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
            Projectile.rotation = Projectile.velocity.X * 0.04f;

            if(flag25)
            {
                Projectile.spriteDirection = (vector46 - Projectile.Center).X > 0? -1: 1;
            }
            else
            {
                Projectile.spriteDirection =Projectile.velocity.X > 0? -1: 1;
            }

            if (++Projectile.frameCounter >= 10)
            {
                Projectile.frameCounter = 0;
                if (!AttackFrame)
                {
                    if (++Projectile.frame > 3)
                    {
                        Projectile.frame = 0;
                    }
                }
                else
                {
                    if (++Projectile.frame < 3 || Projectile.frame > 7)
                    {
                        Projectile.frame = 3;
                    }
                }

            }
            if (Projectile.ai[1] > 0f)
            {
                Projectile.ai[1] += Main.rand.Next(1, 4);
            }
            if (Projectile.ai[1] > 90f)
            {
                Projectile.ai[1] = 0f;
                Projectile.netUpdate = true;
            }
            if (Projectile.ai[0] == 0f)
            {
                float scaleFactor3 = 16f;
                if (flag25 && Projectile.ai[1] == 0f)
                {
                    Projectile.ai[1] += 1f;
                    if (Main.myPlayer == Projectile.owner && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, vector46, 0, 0))
                    {
                        Vector2 value19 = vector46 - Projectile.Center;
                        value19.Normalize();
                        value19 *= scaleFactor3;
                        int proj = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, value19.X, value19.Y, ProjectileID.SaucerLaser, Projectile.damage, Projectile.knockBack, Main.myPlayer, 0f, 0f);
                        Main.projectile[proj].hostile = false;
                        Main.projectile[proj].friendly = true;
                        Main.projectile[proj].minion = true;
                        Main.projectile[proj].netUpdate = true;
                        Projectile.netUpdate = true;
                    }
                }
            }
        }
    }
}