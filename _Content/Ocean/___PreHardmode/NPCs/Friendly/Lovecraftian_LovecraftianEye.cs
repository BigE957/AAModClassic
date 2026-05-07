using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Ocean.___PreHardmode.NPCs.Friendly

{
    public class Lovecraftian_LovecraftianEye : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lovecraftian Eye");
		}

		public override void SetDefaults()
		{
            Projectile.penetrate = 1;
            Projectile.width = 10;
            Projectile.height = 10;
            Projectile.tileCollide = true;
            Projectile.hostile = false;
            Projectile.friendly = true;
            
		}

        public override void OnKill(int timeleft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.CthulhuAuraDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, new Color(191, 86, 188), 2f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(Projectile.Center, Projectile.width, Projectile.height, ModContent.DustType<Dusts.CthulhuAuraDust>(), -Projectile.velocity.X * 0.2f,
                    -Projectile.velocity.Y * 0.2f, 100, new Color(191, 86, 188));
                Main.dust[num469].velocity *= 2f;
            }
        }

        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2(Projectile.velocity.Y, Projectile.velocity.X) + 1.57f;
            if (NPC.downedMoonlord)
            {
                Projectile.damage = 200;
                return;
            }
            if (Main.hardMode)
            {
                Projectile.damage = 90;
                return;
            }
            if (!Main.hardMode)
            {
                Projectile.damage = 20;
                return;
            }
        }
    }
}
