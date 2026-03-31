using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.NPCs.Bosses.SoC.Bosses
{
    public class Leviacane : ModProjectile
    {
    	public int spawnCount = 0;
    	
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Leviacane");
			Main.projFrames[Projectile.type] = 6;
		}
    	
        public override void SetDefaults()
        {
            Projectile.aiStyle = -1;
            Projectile.width = 150;
            Projectile.height = 42;
            Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.alpha = 255;
            Projectile.timeLeft = 600;
            CooldownSlot = 1;
        }
        
        public override void AI()
        {
            int num599 = 10;
            int num600 = 15;
            float num601 = 1f;
            int num602 = 150;
            int num603 = 42;
            if (Projectile.velocity.X != 0f)
            {
                Projectile.direction = (Projectile.spriteDirection = -Math.Sign(Projectile.velocity.X));
            }
            Projectile.frameCounter++;
            if (Projectile.frameCounter > 2)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame >= 6)
            {
                Projectile.frame = 0;
            }
            if (Projectile.localAI[0] == 0f && Main.myPlayer == Projectile.owner)
            {
                Projectile.localAI[0] = 1f;
                Projectile.position.X = Projectile.position.X + (Projectile.width / 2);
                Projectile.position.Y = Projectile.position.Y + (Projectile.height / 2);
                Projectile.scale = ((num599 + num600) - Projectile.ai[1]) * num601 / (num600 + num599);
                Projectile.width = (int)(num602 * Projectile.scale);
                Projectile.height = (int)(num603 * Projectile.scale);
                Projectile.position.X = Projectile.position.X - (Projectile.width / 2);
                Projectile.position.Y = Projectile.position.Y - (Projectile.height / 2);
                Projectile.netUpdate = true;
            }
            if (Projectile.ai[1] != -1f)
            {
                Projectile.scale = ((num599 + num600) - Projectile.ai[1]) * num601 / (num600 + num599);
                Projectile.width = (int)(num602 * Projectile.scale);
                Projectile.height = (int)(num603 * Projectile.scale);
            }
            if (!Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
            {
                Projectile.alpha -= 30;
                if (Projectile.alpha < 60)
                {
                    Projectile.alpha = 60;
                }
            }
            else
            {
                Projectile.alpha += 30;
                if (Projectile.alpha > 150)
                {
                    Projectile.alpha = 150;
                }
            }
            if (Projectile.ai[0] > 0f)
            {
                Projectile.ai[0] -= 1f;
            }
            if (Projectile.ai[0] == 1f && Projectile.ai[1] > 0f && Projectile.owner == Main.myPlayer)
            {
                Projectile.netUpdate = true;
                Vector2 center = Projectile.Center;
                center.Y -= (float)num603 * Projectile.scale / 2f;
                float num604 = ((float)(num599 + num600) - Projectile.ai[1] + 1f) * num601 / (float)(num600 + num599);
                center.Y -= (float)num603 * num604 / 2f;
                center.Y += 2f;
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), center.X, center.Y, Projectile.velocity.X, Projectile.velocity.Y, Projectile.type, Projectile.damage, Projectile.knockBack, Projectile.owner, 10f, Projectile.ai[1] - 1f);
                int num605 = 4;
                if ((int)Projectile.ai[1] % num605 == 0 && Projectile.ai[1] != 0f)
                {
                    int num606 = ModContent.NPCType<DeityShark>();
                    int num607 = NPC.NewNPC(Projectile.GetSource_FromThis(), (int)center.X, (int)center.Y, num606, 0, 0f, 0f, 0f, 0f, 255);
                    Main.npc[num607].velocity = Projectile.velocity;
                    Main.npc[num607].netUpdate = true;
                }
            }
            if (Projectile.ai[0] <= 0f)
            {
                float num608 = 0.104719758f;
                float num609 = (float)Projectile.width / 5f;
                float num610 = (float)(Math.Cos((double)(num608 * -(double)Projectile.ai[0])) - 0.5) * num609;
                Projectile.position.X = Projectile.position.X - num610 * (float)(-(float)Projectile.direction);
                Projectile.ai[0] -= 1f;
                num610 = (float)(Math.Cos((double)(num608 * -(double)Projectile.ai[0])) - 0.5) * num609;
                Projectile.position.X = Projectile.position.X + num610 * (float)(-(float)Projectile.direction);
                return;
            }
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
        	return new Color(255, 255, 53, Projectile.alpha);
        }
        
        public override bool PreDraw(ref Color lightColor)
        {
        	Texture2D texture2D13 = TextureAssets.Projectile[Projectile.type].Value;
			int num214 = TextureAssets.Projectile[Projectile.type].Value.Height / Main.projFrames[Projectile.type];
			int y6 = num214 * Projectile.frame;
			Main.spriteBatch.Draw(texture2D13, Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, texture2D13.Width, num214)), Projectile.GetAlpha(lightColor), Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)num214 / 2f), Projectile.scale, SpriteEffects.None, 0f);
			return false;
        }
    }
}