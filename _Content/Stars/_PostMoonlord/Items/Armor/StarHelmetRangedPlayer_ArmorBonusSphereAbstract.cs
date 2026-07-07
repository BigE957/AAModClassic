using AAModClassic.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    public abstract class StarHelmetRangedPlayer_ArmorBonusSphereAbstract : ModProjectile
    {
        protected int useDust = 0;
        public virtual void InflictBuffs(NPC target)
        {

        }

        bool runOnce = true;
        int shader;
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if(runOnce)
            {
                runOnce = false;
                shader = player.dye[1].dye;
            }
            Projectile.frameCounter++;
            if (Projectile.frameCounter % 10 == 0)
            {
                Projectile.frame++;
                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.frame = 0;
                }
            }
            if (Projectile.timeLeft == 3)
            {
                Explode();
            }
            if (Main.rand.NextBool(4))
            {
                Dust dyeMe = Dust.NewDustPerfect(Projectile.Center, useDust);
                if (shader != 0)
                {
                    dyeMe.shader = GameShaders.Armor.GetSecondaryShader(shader, player);
                }
                    
            }

        }
       
        
        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];

            if (shader != 0)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);

                GameShaders.Armor.GetSecondaryShader(shader, player).Apply(null);
            }
            return Projectile.timeLeft > 2;
        }
        public override void PostDraw(Color lightColor)
        {
            
            Player player = Main.player[Projectile.owner];
            if (shader != 0)
            {
                Main.spriteBatch.End();
                Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.EffectMatrix);
            }
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.localNPCImmunity[target.whoAmI] = -1;
            target.immune[Projectile.owner] = 0;
            InflictBuffs(target);
            Explode();
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Explode();
            return false;
        }
        void Explode()
        {
            if (Projectile.timeLeft > 2)
            {
                Projectile.position.X -= 50;
                Projectile.position.Y -= 50;
                Projectile.width = Projectile.height = 100;
                Projectile.tileCollide = false;
                for (int d = 0; d < 40; d++)
                {
                    Dust dyeMe = Dust.NewDustPerfect(Projectile.Center, useDust, PolarVector(Main.rand.NextFloat(6f), Main.rand.NextFloat((float)Math.PI * 2)));
                    if (shader != 0)
                    {
                        Player player = Main.player[Projectile.owner];
                        dyeMe.shader = GameShaders.Armor.GetSecondaryShader(shader, player);
                    }
                }
                Projectile.timeLeft = 2;
            }
        }
        public static Vector2 PolarVector(float radius, float theta)
        {
            return new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta)) * radius;
        }
    }
}
