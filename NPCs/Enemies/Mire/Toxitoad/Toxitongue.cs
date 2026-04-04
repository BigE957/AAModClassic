using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Enemies.Mire.Toxitoad
{
    public class Toxitongue : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Toxitoad");
        }
        public override void SetDefaults()
        {
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.friendly = false;
            Projectile.penetrate = -1; 
        }
		
		public override void AI()
        { 
            Vector2 vector54 = Main.npc[Projectile.owner].Center - Projectile.Center;
            Projectile.rotation = vector54.ToRotation() - 1.57f;
            if (!Main.npc[Projectile.owner].active)
            {
                Projectile.Kill();
                return;
            }
            float arg_1C53D_0 = vector54.X;
            if (vector54.X < 0f)
            {
                Main.npc[Projectile.owner].spriteDirection = 1;
                Projectile.direction = 1;
            }
            else
            {
                Main.npc[Projectile.owner].spriteDirection = -1;
                Projectile.direction = -1;
            }
            Main.npc[Projectile.owner].rotation = (vector54 * -1f * Projectile.direction).ToRotation();
            Projectile.spriteDirection = (vector54.X > 0f) ? -1 : 1;
            if (Projectile.ai[0] == 0f && vector54.Length() > 400f)
            {
                Projectile.ai[0] = 1f;
            }
            if (Projectile.ai[0] == 1f || Projectile.ai[0] == 2f)
            {
                float num687 = vector54.Length();
                if (num687 > 1500f)
                {
                    Projectile.Kill();
                    return;
                }
                if (num687 > 600f)
                {
                    Projectile.ai[0] = 2f;
                }
                Projectile.tileCollide = false;
                float num688 = 20f;
                if (Projectile.ai[0] == 2f)
                {
                    num688 = 40f;
                }
                Projectile.velocity = Vector2.Normalize(vector54) * num688;
                if (vector54.Length() < num688)
                {
                    Projectile.Kill();
                    return;
                }
            }
            Projectile.ai[1] += 1f;
            if (Projectile.ai[1] > 5f)
            {
                Projectile.alpha = 0;
            }
            if ((int)Projectile.ai[1] % 4 == 0)
            {
                Vector2 vector55 = vector54 * -1f;
                vector55.Normalize();
                vector55 *= Main.rand.Next(45, 65) * 0.1f;
                vector55 = vector55.RotatedBy((Main.rand.NextDouble() - 0.5) * 1.5707963705062866, default);
                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, vector55.X, vector55.Y, ModContent.ProjectileType<FlairdraCyclone>(), Projectile.damage, Projectile.knockBack, Projectile.owner, -10f, 0f);
                return;
            }
        }
		
		public override void OnHitNPC (NPC target, NPC.HitInfo hit, int damageDone)
		{
            target.AddBuff(BuffID.Venom, 600);
        }
		
		public override bool OnTileCollide (Vector2 oldVelocity)
		{
			Projectile.ai[0] = 1f;
			return false;
		}
        
        // chain voodoo
        public override bool PreDraw(ref Color lightColor)
        {

            Texture2D texture = Mod.GetTexture("NPCs/Enemies/Mire/Toxitoad/Toxitongue_Chain");
            
            Vector2 position = Projectile.Center;
            Vector2 mountedCenter = Main.npc[Projectile.owner].Center;
            Rectangle? sourceRectangle = new Rectangle?();
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            float num1 = texture.Height;
            Vector2 vector24 = mountedCenter - position;
            float rotation = (float)Math.Atan2(vector24.Y, vector24.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector24.X) && float.IsNaN(vector24.Y))
                flag = false;
            while (flag)
            {
                if (vector24.Length() < num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector21 = vector24;
                    vector21.Normalize();
                    position += vector21 * num1;
                    vector24 = mountedCenter - position;
                    Color color2 = Lighting.GetColor((int)position.X / 16, (int)(position.Y / 16.0));
                    color2 = Projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1.35f, SpriteEffects.None, 0.0f);
                }
            }
            return true;
        }
    }
}