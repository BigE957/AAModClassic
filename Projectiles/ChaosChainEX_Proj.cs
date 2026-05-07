using System;
using System.IO;
using AAModClassic._Content.Chaos.Buffs;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class ChaosChainEX_Proj : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Perfect Chaos Chain");
		}

        public override void SetDefaults()
        {
            Projectile.width = 58;
            Projectile.height = 58;
            Projectile.friendly = true;
            Projectile.penetrate = -1; 
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;
            Projectile.extraUpdates = 1;
        }

        public float[] InternalAI = new float[2];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(InternalAI[0]);
                writer.Write(InternalAI[1]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                InternalAI[0] = reader.ReadSingle();
                InternalAI[1] = reader.ReadSingle();
            }
        }

        float Rot = 0;
        int Dir = 1;
		
		public override void AI()
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == ModContent.ProjectileType<ChaosChainEXSaw>() && Projectile.ai[0] == 1f)
                {
                    InternalAI[1] = 1;
                }
            }
            if (Projectile.velocity.X < 0)
            {
                Dir = -1;
            }
            Rot += 0.03f * Projectile.direction;

            if (Projectile.timeLeft == 120)
            {
                Projectile.ai[0] = 1f;
            }

            if (Main.player[Projectile.owner].dead)
            {
                Projectile.Kill();
                return;
            }

            Main.player[Projectile.owner].itemAnimation = 5;
            Main.player[Projectile.owner].itemTime = 5;

            if (Projectile.alpha == 0)
            {
                if (Projectile.position.X + (Projectile.width / 2) > Main.player[Projectile.owner].position.X + (Main.player[Projectile.owner].width / 2))
                {
                    Main.player[Projectile.owner].ChangeDir(1);
                }
                else
                {
                    Main.player[Projectile.owner].ChangeDir(-1);
                }
            }
            Projectile.rotation += .4f;
            Vector2 vector14 = new Vector2(Projectile.position.X + (Projectile.width * 0.5f), Projectile.position.Y + (Projectile.height * 0.5f));
            float num166 = Main.player[Projectile.owner].position.X + (Main.player[Projectile.owner].width / 2) - vector14.X;
            float num167 = Main.player[Projectile.owner].position.Y + (Main.player[Projectile.owner].height / 2) - vector14.Y;
            float num168 = (float)Math.Sqrt((num166 * num166) + (num167 * num167));
            if (Projectile.ai[0] == 0f)
            {
                if (num168 > 1000)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity, ModContent.ProjectileType<ChaosChainEXSaw>(), Projectile.damage, 0, Main.myPlayer);
                    Projectile.ai[0] = 1f;
                }
                else if (num168 > 500f)
                {
                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position, Projectile.velocity, ModContent.ProjectileType<ChaosChainEXSaw>(), Projectile.damage, 0, Main.myPlayer);
                    Projectile.ai[0] = 1f;
                }
                Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
                Projectile.ai[1] += 1f;
                if (Projectile.ai[1] > 5f)
                {
                    Projectile.alpha = 0;
                }
                if (Projectile.ai[1] > 8f)
                {
                    Projectile.ai[1] = 8f;
                }
                if (Projectile.ai[1] >= 10f)
                {
                    Projectile.ai[1] = 15f;
                    Projectile.velocity.Y = Projectile.velocity.Y + 0.3f;
                }
                if (Projectile.velocity.X < 0f)
                {
                    Projectile.spriteDirection = -1;
                }
                else
                {
                    Projectile.spriteDirection = 1;
                }
            }
            else if (Projectile.ai[0] == 1f)
            {
                Projectile.tileCollide = false;
                Projectile.rotation = (float)Math.Atan2(num167, num166) - 1.57f;
                float num169 = 30f;

                if (num168 < 50f)
                {
                    Projectile.Kill();
                }
                num168 = num169 / num168;
                num166 *= num168;
                num167 *= num168;
                Projectile.velocity.X = num166;
                Projectile.velocity.Y = num167;
                if (Projectile.velocity.X < 0f)
                {
                    Projectile.spriteDirection = 1;
                }
                else
                {
                    Projectile.spriteDirection = -1;
                }

            }
        }
		
		public override void OnHitNPC (NPC target, NPC.HitInfo hit, int damageDone)
		{
            if (Projectile.ai[0] == 0f)
            {
                Projectile.NewProjectile(Projectile.GetSource_OnHit(target), Projectile.position, Projectile.velocity, ModContent.ProjectileType<ChaosChainEXSaw>(), Projectile.damage, 0, Main.myPlayer);
            }
            target.AddBuff(ModContent.BuffType<DiscordianInferno_Buff>(), 240);
        }
		
        // chain voodoo
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = ModContent.Request<Texture2D>("AAModClassic/Chains/ChaosChainEX_Chain").Value;
            BaseDrawing.DrawChain(Main.spriteBatch, texture, 0, Projectile.Center, Main.player[Projectile.owner].Center, 0f, lightColor, 1f, true);
            Texture2D Tex = ModContent.Request<Texture2D>("AAModClassic/Projectiles/ChaosChainEXSaw").Value;
            Texture2D Tex2 = ModContent.Request<Texture2D>("AAModClassic/Projectiles/ChaosChainEXSphere").Value;
            Rectangle frame = new Rectangle(0, 0, Tex.Width, Tex.Height);
            for (int i = 0; i < 1000; ++i)
            {
                if (Projectile.ai[0] == 1f)
                {
                    BaseDrawing.DrawTexture(Main.spriteBatch, Tex2, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Rot, Dir, 1, frame, lightColor, true);
                }
                else
                {
                    BaseDrawing.DrawTexture(Main.spriteBatch, Tex, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Rot, Dir, 1, frame, lightColor, true);
                }
            }
            return true;
        }
    }
}