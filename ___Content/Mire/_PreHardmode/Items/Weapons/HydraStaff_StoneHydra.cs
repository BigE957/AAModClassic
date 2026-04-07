using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._PreHardmode.Items.Weapons
{
    public class HydraStaff_StoneHydra : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 74;
            Projectile.timeLeft = Projectile.SentryLifeTime;
            Projectile.ignoreWater = true;
            Projectile.sentry = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Stone Hydra");
			Main.projFrames[Projectile.type] = 10;

        }
		
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			fallThrough = false;
			return true;
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Projectile.width = 80;
			Projectile.velocity.Y = 0f;
			return false;
		}
		
        public override void AI()
        {
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[1] = 1f;
                Projectile.localAI[0] = 1f;
                Projectile.ai[0] = 120f;
                int num501 = 80;
                SoundEngine.PlaySound(SoundID.Item46, Projectile.position);
                for (int num502 = 0; num502 < num501; num502++)
                {
                    int num503 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y + 16f), Projectile.width, Projectile.height - 16, DustID.FrostHydra, 0f, 0f, 0);
                    Main.dust[num503].velocity *= 2f;
                    Main.dust[num503].noGravity = true;
                    Main.dust[num503].scale *= 1.15f;
                }
            }
            Projectile.velocity.X = 0f;
            Projectile.velocity.Y = Projectile.velocity.Y + 0.2f;
            if (Projectile.velocity.Y > 16f)
            {
                Projectile.velocity.Y = 16f;
            }
            bool flag18 = false;
            float num506 = Projectile.Center.X;
            float num507 = Projectile.Center.Y;
            float num508 = 1000f;
            NPC ownerMinionAttackTargetNPC = Projectile.OwnerMinionAttackTargetNPC;
            if (ownerMinionAttackTargetNPC != null && ownerMinionAttackTargetNPC.CanBeChasedBy(this, false))
            {
                float num509 = ownerMinionAttackTargetNPC.position.X + ownerMinionAttackTargetNPC.width / 2;
                float num510 = ownerMinionAttackTargetNPC.position.Y + ownerMinionAttackTargetNPC.height / 2;
                float num511 = Math.Abs(Projectile.position.X + Projectile.width / 2 - num509) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - num510);
                if (num511 < num508 && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, ownerMinionAttackTargetNPC.position, ownerMinionAttackTargetNPC.width, ownerMinionAttackTargetNPC.height))
                {
                    num508 = num511;
                    num506 = num509;
                    num507 = num510;
                    flag18 = true;
                }
            }
            if (!flag18)
            {
                for (int num512 = 0; num512 < 200; num512++)
                {
                    if (Main.npc[num512].CanBeChasedBy(this, false))
                    {
                        float num513 = Main.npc[num512].position.X + Main.npc[num512].width / 2;
                        float num514 = Main.npc[num512].position.Y + Main.npc[num512].height / 2;
                        float num515 = Math.Abs(Projectile.position.X + Projectile.width / 2 - num513) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - num514);
                        if (num515 < num508 && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[num512].position, Main.npc[num512].width, Main.npc[num512].height))
                        {
                            num508 = num515;
                            num506 = num513;
                            num507 = num514;
                            flag18 = true;
                        }
                    }
                }
            }
            if (flag18)
            {
                float num516 = num506;
                float num517 = num507;
                num506 -= Projectile.Center.X;
                num507 -= Projectile.Center.Y;
                int num518 = 0;
                if (Projectile.frameCounter > 0)
                {
                    Projectile.frameCounter--;
                }
                if (Projectile.frameCounter <= 0)
                {
                    int num519 = Projectile.spriteDirection;
                    if (num506 < 0f)
                    {
                        Projectile.spriteDirection = -1;
                    }
                    else
                    {
                        Projectile.spriteDirection = 1;
                    }
                    if (num507 > 0f)
                    {
                        num518 = 0;
                    }
                    else if (Math.Abs(num507) > Math.Abs(num506) * 3f)
                    {
                        num518 = 4;
                    }
                    else if (Math.Abs(num507) > Math.Abs(num506) * 2f)
                    {
                        num518 = 3;
                    }
                    else if (Math.Abs(num506) > Math.Abs(num507) * 3f)
                    {
                        num518 = 0;
                    }
                    else if (Math.Abs(num506) > Math.Abs(num507) * 2f)
                    {
                        num518 = 1;
                    }
                    else
                    {
                        num518 = 2;
                    }
                    int num520 = Projectile.frame;
                    Projectile.frame = num518 * 2;
                    Projectile.frame++;
                    if (num520 != Projectile.frame || num519 != Projectile.spriteDirection)
                    {
                        Projectile.frameCounter = 8;
                        if (Projectile.ai[0] <= 0f)
                        {
                            Projectile.frameCounter = 4;
                        }
                    }
                }
                if (Projectile.ai[0] <= 0f)
                {
                    Projectile.localAI[1] = 0f;
                    Projectile.ai[0] = 60f;
                    if (Main.myPlayer == Projectile.owner)
                    {
                        float num521 = 6f;
                        int num522 = ModContent.ProjectileType<HydraStaff_BogBomb>();
                        Vector2 vector37 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
                        if (num518 == 0)
                        {
                            vector37.Y += 12f;
                            vector37.X += 24 * Projectile.spriteDirection;
                        }
                        else if (num518 == 1)
                        {
                            vector37.Y += 0f;
                            vector37.X += 24 * Projectile.spriteDirection;
                        }
                        else if (num518 == 2)
                        {
                            vector37.Y -= 2f;
                            vector37.X += 24 * Projectile.spriteDirection;
                        }
                        else if (num518 == 3)
                        {
                            vector37.Y -= 6f;
                            vector37.X += 14 * Projectile.spriteDirection;
                        }
                        else if (num518 == 4)
                        {
                            vector37.Y -= 14f;
                            vector37.X += 2 * Projectile.spriteDirection;
                        }
                        if (Projectile.spriteDirection < 0)
                        {
                            vector37.X += 10f;
                        }
                        float num523 = num516 - vector37.X;
                        float num524 = num517 - vector37.Y;
                        float num525 = (float)Math.Sqrt(num523 * num523 + num524 * num524);
                        num525 = num521 / num525;
                        num523 *= num525;
                        num524 *= num525;
                        int num526 = Projectile.damage;
                        Projectile.NewProjectile(Projectile.GetSource_FromThis(), vector37.X, vector37.Y, num523*1.5f, num524*1.5f, num522, num526, Projectile.knockBack, Main.myPlayer, 0f, 0f);
                    }
                }
            }
            else if (Projectile.ai[0] <= 60f && (Projectile.frame == 1 || Projectile.frame == 3 || Projectile.frame == 5 || Projectile.frame == 7 || Projectile.frame == 9))
            {
                Projectile.frame--;
            }
            if (Projectile.ai[0] > 0f)
            {
                Projectile.ai[0] -= 1f;
                return;
            }
        }
    }
}