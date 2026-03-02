using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Projectiles
{

    public class Drop : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = false;
            Projectile.hostile = false; 
            Projectile.DamageType = DamageClass.Magic; 
            Projectile.tileCollide = true;
            Projectile.penetrate = 10;
            Projectile.timeLeft = 600;
            Projectile.light = 0.25f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
            Projectile.damage = 10;
            Projectile.scale = 1f;
            Projectile.usesIDStaticNPCImmunity = false;
            Projectile.usesLocalNPCImmunity = true;
        }

        private const int AI_Timer_Slot = 1;

        public float AI_Timer
        {
            get => Projectile.ai[AI_Timer_Slot];
            set => Projectile.ai[AI_Timer_Slot] = value;
        }

        public override void AI()
        {
            Projectile.rotation = ((float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f) + ((float)Math.PI);
            if (Main.rand.Next(12) == 0)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, Mod.Find<ModDust>("AbyssDust").Type, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f, 150, default, 0.7f);
            }

            Projectile.velocity.Y = Projectile.velocity.Y + 0.08f;
            if (Projectile.velocity.Y >= 0)
            {
                Projectile.friendly = true;
            }
            else
            {
                Projectile.friendly = false;
            }

        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.NPCHit3, Projectile.position);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) // Want some Venom?
        {
        //target.AddBuff(BuffID.Venom, 180);
        }

    }
}
