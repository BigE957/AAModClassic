using System;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{
    public class ChaosChain : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chaos Chain");
		}
        public override void SetDefaults()
        {
            Projectile.width = 58;
            Projectile.height = 58;
            Projectile.friendly = true;
            Projectile.penetrate = -1; 
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = false;
        }
		
		public override void AI()
		{
            if (Main.rand.NextFloat() < 1f)
            {
                Dust dust1;
                Dust dust2;
                Vector2 position = Projectile.position;
                dust1 = Main.dust[Dust.NewDust(position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.AkumaDust>(), 0, 0, 0)];
                dust2 = Main.dust[Dust.NewDust(position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.YamataAuraDust>(), 0, 0, 0)];
                dust1.noGravity = true;
                dust2.noGravity = true;
            }
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
            Vector2 vector14 = new Vector2(Projectile.position.X + (Projectile.width * 0.5f), Projectile.position.Y + (Projectile.height * 0.5f));
            float num166 = Main.player[Projectile.owner].position.X + (Main.player[Projectile.owner].width / 2) - vector14.X;
            float num167 = Main.player[Projectile.owner].position.Y + (Main.player[Projectile.owner].height / 2) - vector14.Y;
            float num168 = (float)Math.Sqrt((num166 * num166) + (num167 * num167));
            if (Projectile.ai[0] == 0f)
            {
                if (num168 > 700f)
                {
                    Projectile.ai[0] = 1f;
                }
                else if (num168 > 500f)
                {
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

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 30;
            height = 30;
            return true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.ai[0] = 1f;
            return false;
        }

        public override void OnHitNPC (NPC target, NPC.HitInfo hit, int damageDone)
		{
            target.AddBuff(ModContent.BuffType<Buffs.HydraToxin>(), 150);
            target.AddBuff(ModContent.BuffType<Buffs.DragonFire>(), 150);
        }
		
 
        // chain voodoo
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = Mod.GetTexture("Chains/ChaosChain_Chain");

            BaseDrawing.DrawChain(spriteBatch, texture, 0, Projectile.Center, Main.player[Projectile.owner].Center, 0f, lightColor, 1f, true);
            return true;
        }
    }
}