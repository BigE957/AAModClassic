using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Snow.___PreHardmode.Items._BossSubzeroSerpent.Weapons
{
    public class SnowflakeShuriken_Proj : ModProjectile
	{
        public override string Texture => ModContent.GetInstance<SnowflakeShuriken>().Texture;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("SS");
		}

		public override void SetDefaults()
		{
			Projectile.CloneDefaults(ProjectileID.Shuriken);
			Projectile.width = 15;
			Projectile.height = 15;
            Projectile.DamageType = DamageClass.Ranged;
        }
        

		public override void OnKill(int timeLeft)
		{
			if (Main.rand.Next(0, 4) == 0)
				Item.NewItem(Projectile.GetSource_DropAsItem(), (int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height, ModContent.ItemType<SnowflakeShuriken>(), 1, false, 0, false, false);

			for (int i = 0; i < 5; i++)
			{
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, ModContent.DustType<Dusts.IceDust>());
			}
			SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
		}

	}
}