using AAModClassic.___Content.Mire.Buffs;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.AH.Haruka
{
    public class HarukaKunai : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 34;
			Projectile.friendly = false;
            Projectile.hostile = true;
			Projectile.timeLeft = 1200;
			Projectile.penetrate = 1;
            Projectile.extraUpdates = 1;
            Projectile.aiStyle = -1;
		}

        public override void AI()
        {
            BaseAI.AIThrownWeapon(Projectile, ref Projectile.ai, Projectile.timeLeft < 1160, 800);
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
		{
			// For going through platforms and such, javelins use a tad smaller size
			width = height = 10; // notice we set the width to the height, the height to 10. so both are 10
			return true;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Abyssal Kunai");
		}

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<HydraToxin_Buff>(), 180);
            Projectile.netUpdate = true;
        }

        public override void OnKill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
			     Dust.NewDust(Projectile.position + Projectile.velocity, Projectile.width, Projectile.height, ModContent.DustType<Dusts.CthulhuAuraDust>(), Projectile.oldVelocity.X * 0.1f, Projectile.oldVelocity.Y * 0.1f);
			}
			SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
			
		}
	}
}