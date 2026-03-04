using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles.Zero
{
    // to investigate: Projectile.Damage, (8843)
    class RiftSlash : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.aiStyle = ProjAIStyleID.Beam;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 240;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 10;
            Projectile.alpha = 50;
        }

        public override void AI()
        {
            Lighting.AddLight((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16, 0.3f * 1, 0.4f * 0, 1f * .2f);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D Tex = TextureAssets.Projectile[Projectile.type].Value;
            Rectangle frame = new Rectangle(0, 0, Tex.Width, Tex.Height);
            BaseDrawing.DrawAfterimage(spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, 
                Projectile.position, Projectile.width, Projectile.height, Projectile.oldPos, 
                Projectile.scale, Projectile.rotation, Projectile.spriteDirection, 
                1, frame, 1.5f, 1, 5, false, 0, 0, Projectile.GetAlpha(AAColor.ZeroShield));
            return true;
        }
    }
}
