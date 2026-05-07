using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Weapons
{
    public class Yogan_Holdout : ModProjectile
    {
        public float rot = 0;

        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 3600;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.tileCollide = true;
            Projectile.damage = 1;
            Projectile.penetrate = -1;
            Projectile.knockBack = 3;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void AI()
        {
            BaseAI.AIFlail(Projectile, ref Projectile.ai, false, 200);
            Projectile.direction = Projectile.spriteDirection = Main.player[Projectile.owner].direction;
            if ((Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) / 2f > 0.52f)
            {
                rot += (float)Math.PI / 16f;
            }
            else { rot *= 0.9f; if (rot < (float)Math.PI / 20f) { rot = 0f; } }
            Projectile.rotation += rot;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }

        public override bool OnTileCollide(Vector2 value2)
        {
            BaseAI.TileCollideFlail(Projectile, ref value2);
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D chainTex = Mod.GetTexture("Chains/Yogan_Chain");
            if (Main.instance.IsActive)
                for (int m = 0; m < 2; m++)
                    BaseDrawing.DrawChain(Main.spriteBatch, chainTex, 0, Projectile.Center, Main.player[Projectile.owner].Center);
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height, 0, 2);
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 1, frame, lightColor, true);
            return false;
        }
    }
}
