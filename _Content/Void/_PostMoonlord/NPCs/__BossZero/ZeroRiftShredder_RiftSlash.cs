using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero
{
    class ZeroRiftShredder_RiftSlash : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 50;
            Projectile.height = 50;
            Projectile.aiStyle = ProjAIStyleID.Beam;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 240;
        }

        public override void PostAI()
        {
            Projectile.timeLeft--;
            if (Projectile.timeLeft <= 0)
            {
                Projectile.Kill();
            }
            Lighting.AddLight(Projectile.Center, .5f, 0f, .1f);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            BaseDrawing.DrawAfterimage(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile, 1.5f, 1f, 5, false, 0f, 0f, Projectile.GetAlpha(AAColor.ZeroShield));
            return true;
        }
    }
}
