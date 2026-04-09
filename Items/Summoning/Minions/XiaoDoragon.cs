using System;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Buffs;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Summoning.Minions
{
    public class XiaoDoragon : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Xiao Doragon");
			Main.projFrames[Projectile.type] = 5;
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 96;
            Projectile.height = 70;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.minionSlots = 1f;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
        }

        public int FrameTimer = 0;
        bool hasTarget = false;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            player.AddBuff(ModContent.BuffType<XiaoDoragon_Buff>(), 3600);

            if (player.dead)
            {
                modPlayer.Xiao = false;
            }
            if (modPlayer.Xiao)
            {
                Projectile.timeLeft = 2;
            }

            float minRange = 700f;
            float Range = 800f;
            float MaxRange = 1200f;
            float MaxOwnerDist = 150f;
            float IdleSpeed = 0.05f;
            for (int num638 = 0; num638 < 1000; num638++)
            {
                bool flag23 = Main.projectile[num638].type == ModContent.ProjectileType<XiaoDoragon>();
                if (num638 != Projectile.whoAmI && Main.projectile[num638].active && Main.projectile[num638].owner == Projectile.owner && flag23 && Math.Abs(Projectile.position.X - Main.projectile[num638].position.X) + Math.Abs(Projectile.position.Y - Main.projectile[num638].position.Y) < Projectile.width)
                {
                    if (Projectile.position.X < Main.projectile[num638].position.X)
                    {
                        Projectile.velocity.X = Projectile.velocity.X - IdleSpeed;
                    }
                    else
                    {
                        Projectile.velocity.X = Projectile.velocity.X + IdleSpeed;
                    }
                    if (Projectile.position.Y < Main.projectile[num638].position.Y)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y - IdleSpeed;
                    }
                    else
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y + IdleSpeed;
                    }
                }
            }
            Vector2 TargetCenter = Projectile.position;
            hasTarget = false;
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
                    float Distance = Vector2.Distance(target.Center, Projectile.Center);
                    if (((Vector2.Distance(Projectile.Center, TargetCenter) > Distance && Distance < minRange) || !hasTarget) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, target.position, target.width, target.height))
                    {
                        minRange = Distance;
                        TargetCenter = target.Center;
                        hasTarget = true;
                    }
                }
			}
			else
			{
				for (int targetID = 0; targetID < 200; targetID++)
                {
                    NPC target = Main.npc[targetID];
                    if (target.CanBeChasedBy(Projectile, false))
                    {
                        float Distance = Vector2.Distance(target.Center, Projectile.Center);
                        if (((Vector2.Distance(Projectile.Center, TargetCenter) > Distance && Distance < minRange) || !hasTarget) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, target.position, target.width, target.height))
                        {
                            minRange = Distance;
                            TargetCenter = target.Center;
                            hasTarget = true;
                        }
                    }
                }
			}
            float inRange = Range;
            if (hasTarget)
            {
                inRange = MaxRange;
            }
            if (Vector2.Distance(player.Center, Projectile.Center) > inRange)
            {
                Projectile.ai[0] = 1f;
                Projectile.tileCollide = false;
                Projectile.netUpdate = true;
            }
            if (hasTarget && Projectile.ai[0] == 0f)
            {
                Vector2 TargetPos = TargetCenter - Projectile.Center;
                float TargetDistance = TargetPos.Length();
                TargetPos.Normalize();
                if (TargetDistance > 200f)
                {
                    float FastSpeed = 10f;
                    TargetPos *= FastSpeed;
                    Projectile.velocity = (Projectile.velocity * 40f + TargetPos) / 41f;
                }
                else
                {
                    float Speed = 6f;
                    TargetPos *= -Speed;
                    Projectile.velocity = (Projectile.velocity * 40f + TargetPos) / 41f;
                }
            }
            else
            {
                bool isIdle = false;
                if (!isIdle)
                {
                    isIdle = Projectile.ai[0] == 1f;
                }
                float Speed = 6f;
                if (isIdle)
                {
                    Speed = 15f;
                }
                Vector2 Center = Projectile.Center;
                Vector2 IdlePos = player.Center - Center + new Vector2(0f, -60f);
                float OwnerDistance = IdlePos.Length();
                if (OwnerDistance > 200f && Speed < 8f)
                {
                    Speed = 8f;
                }
                if (OwnerDistance < MaxOwnerDist && isIdle && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                {
                    Projectile.ai[0] = 0f;
                    Projectile.netUpdate = true;
                }
                if (OwnerDistance > 2000f)
                {
                    Projectile.position.X = Main.player[Projectile.owner].Center.X - Projectile.width / 2;
                    Projectile.position.Y = Main.player[Projectile.owner].Center.Y - Projectile.height / 2;
                    Projectile.netUpdate = true;
                }
                if (OwnerDistance > 70f)
                {
                    IdlePos.Normalize();
                    IdlePos *= Speed;
                    Projectile.velocity = (Projectile.velocity * 40f + IdlePos) / 41f;
                }
                else if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
                {
                    Projectile.velocity.X = -0.15f;
                    Projectile.velocity.Y = -0.05f;
                }
            }
            
            if(hasTarget)
            {
                Projectile.spriteDirection = ((TargetCenter - Projectile.Center).X > 0? -1: 1);
            }
            else
            {
                Projectile.spriteDirection =(Projectile.velocity.X > 0? -1: 1);
            }
            

            if (Projectile.ai[1] > 0f)
            {
                Projectile.ai[1] += Main.rand.Next(1, 4);
            }
            if (Projectile.ai[1] > 45f)
            {
                Projectile.ai[1] = 0f;
                Projectile.netUpdate = true;
            }
            if (Projectile.ai[0] == 0f)
            {
                float ShootSpeed = 8f;
                int proj = ModContent.ProjectileType<XiaoFireball>();
                if (hasTarget && Projectile.ai[1] == 0f)
                {
                    Projectile.ai[1] += 1f;
                    if (Main.myPlayer == Projectile.owner && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, TargetCenter, 0, 0))
                    {
                        Vector2 value19 = TargetCenter - Projectile.Center;
                        value19.Normalize();
                        value19 *= ShootSpeed;
                        int num659 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, value19.X, value19.Y, proj, Projectile.damage, 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[num659].penetrate = 2;
                        Main.projectile[num659].timeLeft = 300;
						Main.projectile[num659].usesLocalNPCImmunity = true;
						Main.projectile[num659].localNPCHitCooldown = -1;
                        Projectile.netUpdate = true;
                    }
                }
            }
        }

        public override void PostAI()
        {
            for (int m = Projectile.oldPos.Length - 1; m > 0; m--)
            {
                Projectile.oldPos[m] = Projectile.oldPos[m - 1];
            }
            Projectile.oldPos[0] = Projectile.position;

            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
            }
            if (Projectile.frame > 4)
            {
                Projectile.frame = 0;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;

            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / 5, 0, 0);


            if (Projectile.spriteDirection == 1)
            {
                tex = Mod.GetTexture("Items/Summoning/Minions/XiaoDoragonBlue");
            }

            if (hasTarget)
            {
                tex = Mod.GetTexture("Items/Summoning/Minions/XiaoDoragonA");
                BaseDrawing.DrawAfterimage(Main.spriteBatch, tex, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.oldPos, 1f, Projectile.rotation, Projectile.spriteDirection, 5, frame, 1, 1, 5, true);
            }


            BaseDrawing.DrawTexture(Main.spriteBatch, tex, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, Projectile.spriteDirection, 5, frame, lightColor, true);

            if (hasTarget)
            {
                Texture2D g = Mod.GetTexture("Glowmasks/XiaoDoragon_Glow");
                BaseDrawing.DrawTexture(Main.spriteBatch, g, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, Projectile.spriteDirection, 5, frame, AAColor.Shen2, true);
                BaseDrawing.DrawAfterimage(Main.spriteBatch, g, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.oldPos, 1f, Projectile.rotation, Projectile.spriteDirection, 5, frame, 1, 1, 5, true, 0, 0, AAColor.Shen2);
            }
            return false;
        }
    }
}