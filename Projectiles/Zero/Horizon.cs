using Microsoft.Xna.Framework;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic.Base.BaseMod.Base;


namespace AAModClassic.Projectiles.Zero
{
    // to investigate: Projectile.Damage, (8843)
    class Horizon : ModProjectile
	{
        public override void SetDefaults()
		{
            
            Projectile.width = 64;
            Projectile.height = 64;
            Projectile.alpha = 100;
            Projectile.light = 0.2f;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.scale = 0.9f;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 300;
            Projectile.scale = .1f;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }
		
        public override void AI()
        {
            Projectile.rotation += .05f;
            if (Projectile.ai[0] == 0f)
            {
                Projectile.scale += .02f;
                if (Projectile.scale >= 1)
                {
                    Projectile.ai[0] = 1f;
                }
            }
            if (Projectile.ai[0] == 1f)
            {
                Projectile.scale -= .02f;
                if (Projectile.scale <= 0)
                {
                    Projectile.active = false;
                }
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            BaseDrawing.DrawTexture(spriteBatch, tex, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, -Projectile.rotation, Projectile.direction, 1, new Rectangle(0, 0, tex.Width, tex.Height), AAColor.Yamata, true);
            BaseDrawing.DrawTexture(spriteBatch, tex, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, Projectile.direction, 1, new Rectangle(0, 0, tex.Width, tex.Height), AAColor.ZeroShield, true);
            return false;
        }
    }
}
