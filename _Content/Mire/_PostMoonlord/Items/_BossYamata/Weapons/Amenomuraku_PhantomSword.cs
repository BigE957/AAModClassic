using System;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;


namespace AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Weapons
{

    public class Amenomuraku_PhantomSword : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;       //projectile width
            Projectile.height = 30;  //projectile height
            Projectile.friendly = true;      //make that the projectile will not damage you
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;         // 
            Projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            Projectile.penetrate = 10;      //how many npc will penetrate
            Projectile.timeLeft = 300;   //how many time this projectile has before disepire
            Projectile.light = 0.25f;    // projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            Projectile.damage = 1;
            Projectile.scale = 0.75f;
            Projectile.usesIDStaticNPCImmunity = false;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 5;
        }

        private const int AI_Timer_Slot = 1;

        public float AI_Timer
        {
            get => Projectile.ai[AI_Timer_Slot];
            set => Projectile.ai[AI_Timer_Slot] = value;
        }

        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f + ((float)Math.PI * 0.25f);
            if (Projectile.localAI[0] == 0f)
            {
                AdjustMagnitude(ref Projectile.velocity);
                Projectile.localAI[0] = 1f;
            }

            Vector2 move = Vector2.Zero;
            AI_Timer++;
            if (AI_Timer >= 20)
            {
                Projectile.tileCollide = true;
            }
        }

        public override bool PreDraw(ref Color lightColor)
        {
            BaseDrawing.DrawAfterimage(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile, .5f, 1f, 12, false, 0f, 0f);
            BaseDrawing.DrawTexture(Main.spriteBatch, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile, lightColor, false);
            return false;
        }

        private static void AdjustMagnitude(ref Vector2 vector)
        {
            float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
            if (magnitude > 15f)
            {
                vector *= 15f / magnitude;
            }
        }
    }
}
