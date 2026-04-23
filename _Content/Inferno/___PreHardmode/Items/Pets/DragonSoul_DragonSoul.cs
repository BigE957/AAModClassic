using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Pets
{
    /// <summary>
    /// ALPHA THIS IS NOT AN ITEMS, ALSO WHY THE ITEM HAVE JUST AN EXTRA S, IT WOULDN'T BE CASE IF IT WAS IN THE PROPER PLACE. ALSO WOULD BE BETTER IN POETHIC FRENCH
    /// </summary>
    /// lol
    public class DragonSoul_DragonSoul : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dragon Soul");
			Main.projFrames[Projectile.type] = 4;
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
            Lighting.AddLight((int)(Projectile.Center.X + Projectile.width / 2) / 16, (int)(Projectile.position.Y + Projectile.height / 2) / 16, .5f, 0.3f, 0f);
            if (Projectile.velocity.X > 0f)
            {
                Projectile.spriteDirection = -1;
            }
            else if (Projectile.velocity.X < 0f)
            {
                Projectile.spriteDirection = 1;
            }
            Projectile.rotation = Projectile.velocity.X * 0.1f;
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 4)
            {
                Projectile.frame++;
                Projectile.frameCounter = 0;
            }
            if (Projectile.frame >= 4)
            {
                Projectile.frame = 0;
            }
            Player player = Main.player[Projectile.owner];
            if (Main.myPlayer == Projectile.owner)
            {
                if (player.GetModPlayer<AAPlayer>().DragonSoul)
                {
                    Projectile.timeLeft = 2;
                }
            }
            if (Main.player[Projectile.owner].dead)
            {
                Projectile.Kill();
                return;
            }
            float num146 = 3.5f;
            Vector2 vector13 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
            float num147 = Main.player[Projectile.owner].position.X + Main.player[Projectile.owner].width / 2 - vector13.X;
            float num148 = Main.player[Projectile.owner].position.Y + Main.player[Projectile.owner].height / 2 - vector13.Y;
            int num149 = 40;
            float num150 = (float)Math.Sqrt(num147 * num147 + num148 * num148);
            num150 = (float)Math.Sqrt(num147 * num147 + num148 * num148);
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