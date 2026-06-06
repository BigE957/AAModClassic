using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Weapons
{
    public class Pyrosphere_Holdout : FlailHoldout
	{
        private float rot = 0;

        public override string ChainTexturePath => Texture + "_Chain";

        public override float DrawRotationOffset => base.DrawRotationOffset;

        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.knockBack = 3;

            base.SetDefaults();
        }

        public override void AI()
        {
            //BaseAI.AIFlail(Projectile, ref Projectile.ai, false, 160);
            //Projectile.direction = Projectile.spriteDirection = Main.player[Projectile.owner].direction;

            base.AI();

            if (CurrentAIState != AIState.Ricochet && CurrentAIState != AIState.Dropping)
            {
                if ((Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) / 2f > 0.52f)
                {
                    rot += (float)Math.PI / 16f * Math.Sign(Projectile.velocity.X);
                }
                else
                {
                    rot *= 0.9f;
                    if (rot < (float)Math.PI / 20f)
                        rot = 0f;
                }
                Projectile.rotation += rot;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return base.PreDraw(ref lightColor);
            /*
            Texture2D chainTex = ModContent.Request<Texture2D>(Texture + "_Chain").Value;
            if (Main.instance.IsActive)
                for (int m = 0; m < 2; m++)
                    BaseDrawing.DrawChain(Main.spriteBatch, chainTex, Projectile.Center, Main.player[Projectile.owner].Center);
            return true;
            */
        }
    }
}