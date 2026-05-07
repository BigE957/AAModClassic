using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.NPCs._BossSagittarius
{
    public class SagShot : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.damage = 10;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.penetrate = 1;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 600;
            Projectile.tileCollide = true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.ai[0] += 0.1f;
            Projectile.velocity *= 0.75f;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
            {
                targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
            }
            return projHitbox.Intersects(targetHitbox);
        }

		public bool spawnSound = false;
        public override void AI()
        {
			if(Main.netMode != NetmodeID.Server && !spawnSound)
			{
				SoundEngine.PlaySound(SoundID.Item33, Projectile.position);				
				spawnSound = true;
			}
            Projectile.rotation = Projectile.velocity.ToRotation() + 1.57f;

            if (Main.rand.NextBool(1))
            {
                int dustnumber = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.VoidDust>(), 0f, 0f, 200, default, 0.8f);
                Main.dust[dustnumber].velocity *= 0.3f;
            }
            for (int m = Projectile.oldPos.Length - 1; m > 0; m--)
            {
                Projectile.oldPos[m] = Projectile.oldPos[m - 1];
            }
            Projectile.oldPos[0] = Projectile.position;
        }

        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
        }
        
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.Violet;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 5) 
            {
                Projectile.frame++; 
                Projectile.frameCounter = 0;
                if (Projectile.frame > 4)
                {
                    Projectile.frame = 0;
                }
            }
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / 5, 0, 0);
            SagiStar.DrawAfterimage(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.oldPos, Projectile.scale, Projectile.rotation, Projectile.direction, 5, frame, 1.2f, 1f, 4, true, 0, 0, true, Color.White);
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, Projectile.direction, 5, frame, Color.White, true);
            return false;
        }
    }
}