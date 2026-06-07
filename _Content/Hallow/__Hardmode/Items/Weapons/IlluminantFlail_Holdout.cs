using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic._Content.Hallow.__Hardmode.Items.Weapons
{

    public class IlluminantFlail_Holdout : FlailHoldout
    {
        public float rot = 0;

        public override string ChainTexturePath => Texture + "_Chain";

        public override float DrawRotationOffset => base.DrawRotationOffset;

        public override float LaunchSpeed => 15;

        public override int LaunchTimeLimit => 27;

        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.knockBack = 3;

            base.SetDefaults();
        }

        public override void AI()
        {
            //BaseAI.AIFlail(Projectile, ref Projectile.ai, false, 230);
            //Projectile.direction = Projectile.spriteDirection = Main.player[Projectile.owner].direction;

            base.AI();
            /*
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
            */
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return base.PreDraw(ref lightColor);
        }
    }
}
