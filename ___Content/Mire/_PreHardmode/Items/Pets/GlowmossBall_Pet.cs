using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._PreHardmode.Items.Pets
{
    /// <summary>
    /// ALPHA THIS IS NOT AN ITEM
    /// </summary>
	public class Glowmoss : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Glowmoss");
			Main.projFrames[Projectile.type] = 1;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.LightPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.width = 30;
			Projectile.height = 30;
			Projectile.penetrate = -1;
			Projectile.netImportant = true;
			Projectile.timeLeft *= 5;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
		}

		public override void AI()
        {
            Lighting.AddLight((int)(Projectile.Center.X + Projectile.width / 2) / 16, (int)(Projectile.position.Y + Projectile.height / 2) / 16, 0f, 0.5f, 0.2f);
            Player player = Main.player[Projectile.owner];
            Projectile.rotation += 0.02f;
            if (Main.myPlayer == Projectile.owner)
            {
                if (player.GetModPlayer<AAPlayer>().Glowmoss)
                {
                    Projectile.timeLeft = 2;
                }
            }
            if (Main.player[Projectile.owner].dead)
            {
                Projectile.Kill();
                return;
            }
            float num146 = 3.3f;
            Vector2 vector13 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
            float num147 = Main.player[Projectile.owner].position.X + Main.player[Projectile.owner].width / 2 - vector13.X;
            float num148 = Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].height / 2 - vector13.Y;
            int num149 = 70;
            if (Main.player[Projectile.owner].controlUp)
            {
                num148 = Main.player[Projectile.owner].position.Y - 40f - vector13.Y;
                num147 -= 6f;
                num149 = 4;
            }
            else if (Main.player[Projectile.owner].controlDown)
            {
                num148 = Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].height + 40f - vector13.Y;
                num147 -= 6f;
                num149 = 4;
            }
            float num150 = (float)Math.Sqrt(num147 * num147 + num148 * num148);
            if (num150 > 800f)
            {
                Projectile.position.X = Main.player[Projectile.owner].position.X + Main.player[Projectile.owner].width / 2 - Projectile.width / 2;
                Projectile.position.Y = Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].height / 2 - Projectile.height / 2;
                return;
            }
            if (num150 > num149)
            {
                num150 = num146 / num150;
                num147 *= num150;
                num148 *= num150;
                Projectile.velocity.X = num147;
                Projectile.velocity.Y = num148;
                return;
            }
            Projectile.velocity.X = 0f;
            Projectile.velocity.Y = 0f;
            return;
        }
    }
}