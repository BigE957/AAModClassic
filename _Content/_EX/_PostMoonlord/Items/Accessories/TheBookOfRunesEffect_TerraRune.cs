using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Accessories
{
    public class TheBookOfRunesEffect_TerraRune : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Rune");
            ProjectileID.Sets.DontAttachHideToAlpha[Projectile.type] = true;
            Main.projPet[Projectile.type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.width = 12;
            Projectile.height = 22;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            Projectile.minionSlots = 0f;
            Projectile.damage = 1;
            Projectile.tileCollide = false;
            Projectile.timeLeft *= 5;
        }

        public override void AI()
        {
            Lighting.AddLight((int)(Projectile.position.X + Projectile.width / 2) / 16, (int)(Projectile.position.Y + Projectile.height / 2) / 16, 1f, 0.95f, 0.8f);
            Player player = Main.player[Projectile.owner];
            ZAAPlayer modPlayer = player.GetModPlayer<ZAAPlayer>();

            if (player.dead || !player.GetModPlayer<TheBookOfRunesPlayer>().effect || player.maxMinions - player.slotsMinions < 2f)
            {
                Projectile.active = false;
                return;
            }
            else
            {
                Projectile.timeLeft = 2;
            }

            Projectile.timeLeft ++;

            foreach (Projectile p in Main.ActiveProjectiles)
            {
                bool flag23 = p.type == ModContent.ProjectileType<TheBookOfRunesEffect_TerraRune>();
                if (p.whoAmI != Projectile.whoAmI && p.owner == Projectile.owner && flag23 && Math.Abs(Projectile.position.X - p.position.X) + Math.Abs(Projectile.position.Y - p.position.Y) < Projectile.width)
                {
                    if (Projectile.position.X < p.position.X)
                    {
                        Projectile.velocity.X = Projectile.velocity.X - 0.02f;
                    }
                    else
                    {
                        Projectile.velocity.X = Projectile.velocity.X + 0.02f;
                    }
                    if (Projectile.position.Y < p.position.Y)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y - 0.02f;
                    }
                    else
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y + 0.02f;
                    }
                }
            }
            if (Vector2.Distance(player.Center, Projectile.Center) > 400f)
			{
				Projectile.ai[0] = 1f;
				Projectile.tileCollide = false;
				Projectile.netUpdate = true;
			}
			Vector2 vector = player.Center - Projectile.Center - new Vector2(0, 50f);
            float num639 = 7f;
			if (vector.Length() > 200f && num639 < 10f)
			{
				num639 = 10f;
			}
			if (vector.Length() < 100f && Projectile.ai[0] == 1f && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
			{
				Projectile.ai[0] = 0f;
				Projectile.netUpdate = true;
			}
			if (vector.Length() > 2000f)
			{
				Projectile.position.X = Main.player[Projectile.owner].Center.X - Projectile.width / 2;
				Projectile.position.Y = Main.player[Projectile.owner].Center.Y - Projectile.height / 2;
				Projectile.netUpdate = true;
			}
			if (vector.Length() > 150f)
			{
				vector.Normalize();
				vector *= num639;
				Projectile.velocity = (Projectile.velocity * 40f + vector) / 41f;
			}
            else if (vector.Length() > 40f)
			{
				vector.Normalize();
				vector *= num639;
				Projectile.velocity = (Projectile.velocity * 40f + vector) / 41f;
			}
			if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
			{
				Projectile.velocity.X = -0.04f;
				Projectile.velocity.Y = -0.02f;
			}

            if (Projectile.ai[1] > 0f)
			{
				Projectile.ai[1] += Main.rand.Next(1, 4);
			}
			if (Projectile.ai[1] > 220f)
			{
				Projectile.ai[1] = 0f;
				Projectile.netUpdate = true;
			}
            if (Projectile.localAI[0] < 120f)
			{
				Projectile.localAI[0] += 1f;
			}
            if (Projectile.ai[0] == 0f)
            {
                if (Projectile.ai[1] == 0f && Projectile.localAI[0] >= 120f)
                {
                    Projectile.ai[1] += 1f;
                    if (Main.myPlayer == Projectile.owner && Main.player[Projectile.owner].statLife < Main.player[Projectile.owner].statLifeMax2)
					{
                        Main.player[Projectile.owner].HealEffect(20, false);
                        Main.player[Projectile.owner].statLife += 20;
                        if (Main.player[Projectile.owner].statLife > Main.player[Projectile.owner].statLifeMax2)
                        {
                            Main.player[Projectile.owner].statLife = Main.player[Projectile.owner].statLifeMax2;
                        }
                        NetMessage.SendData(MessageID.SpiritHeal, -1, -1, null, Projectile.owner, 1, 0f, 0f, 0, 0, 0);
                        Projectile.netUpdate = true;
                    }
                }
            }
        }
    }
}
