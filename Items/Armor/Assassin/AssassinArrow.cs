using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Assassin
{
    public class AssassinArrow : ModProjectile
	{
        public override void SetStaticDefaults()
        {

            // DisplayName.SetDefault("Assassin Arrow");    
		}

		public override void SetDefaults()
		{
			Projectile.width = 40;
			Projectile.height = 14;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 600;
			Projectile.light = 0.5f;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = true;
			Projectile.extraUpdates = 1;
            Projectile.knockBack = 0.1f;
            Projectile.arrow = true;
            AIType = ProjectileID.WoodenArrowFriendly;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(Mod.Find<ModBuff>("AssassinHurt").Type, 1000);
        }
        public override void OnKill(int timeleft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int num468 = 0; num468 < 4; num468++)
            {
                num468 = Dust.NewDust(new Microsoft.Xna.Framework.Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width, Projectile.height, ModContent.DustType<Dusts.YamataADust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, default);
            }
        }
    }
}
