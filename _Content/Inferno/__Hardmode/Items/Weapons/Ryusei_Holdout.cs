using AAModClassic._Content.Inferno.Buffs;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.__Hardmode.Items.Weapons
{
    public class Ryusei_Holdout : FlailHoldout
    {
        public float rot = 0;

        public override string ChainTexturePath => Texture + "_Chain";

        public override float DrawRotationOffset => base.DrawRotationOffset;

        public override float LaunchSpeed => 22;

        public override int LaunchTimeLimit => 19;

        public override float RetractAcceleration => base.RetractAcceleration;

        public override float MaxRetractSpeed => base.MaxRetractSpeed;

        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.knockBack = 3;

            base.SetDefaults();
        }

        public override void AI()
        {
            //BaseAI.AIFlail(Projectile, ref Projectile.ai, false, 250);
            //Projectile.direction = Projectile.spriteDirection = Main.player[Projectile.owner].direction;

            base.AI();

            if (CurrentAIState != AIState.Ricochet && CurrentAIState != AIState.Dropping)
            {
                if ((Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y)) / 2f > 0.52f)
                {
                    rot += (float)Math.PI / 16f;
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

        public override bool PreDraw(ref Color lightColor)
        {
            return base.PreDraw(ref lightColor);
            /*
            Texture2D chainTex = ModContent.Request<Texture2D>("AAModClassic/Chains/Ryusei_Chain").Value;
            if (Main.instance.IsActive)
                for (int m = 0; m < 2; m++)
                    BaseDrawing.DrawChain(Main.spriteBatch, chainTex, Projectile.Center, Main.player[Projectile.owner].Center);

            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Width(), TextureAssets.Projectile[Projectile.type].Height(), 0, 2);
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 1, frame, lightColor, true);
            return false;
            */
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, 300);
            target.AddBuff(ModContent.BuffType<DragonFire_Buff>(), 300);
		}
    }
}
