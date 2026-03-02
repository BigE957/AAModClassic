using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;

namespace AAModClassic.NPCs.Bosses.MushroomMonarch
{
    internal class FakeMonarchMushroom : ModProjectile
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mushroom");
        }

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 24;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 14400;
        }

        private bool isGrabbing = false;

        public override bool OnTileCollide(Vector2 oldVelocity)
		{
            if(isGrabbing)
            {
                Projectile.velocity.X = 0f;
                Projectile.velocity.Y = 0f;
            }
			return false;
		}

        public override void AI()
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<MushroomMonarch>()))
            {
                Projectile.Kill();
                return;
            }

            float num = 0.1f;
            float num2 = 7f;

            Projectile.velocity.Y = Projectile.velocity.Y + num;
            if (Projectile.velocity.Y > num2)
            {
                Projectile.velocity.Y = num2;
            }
            Projectile.velocity.X = Projectile.velocity.X * 0.95f;
            if (Projectile.velocity.X < 0.1 && Projectile.velocity.X > -0.1)
            {
                Projectile.velocity.X = 0f;
            }

            Vector2 tile = new Vector2(Projectile.Center.X, Projectile.Center.Y + Projectile.height / 2);
            bool tileCheck = TileID.Sets.Platforms[Main.tile[(int)(tile.X / 16), (int)(tile.Y / 16)].TileType];
            if (tileCheck) 
            {
                Projectile.velocity.X = 0f;
                Projectile.velocity.Y = 0f;
            }

            for(int i = 0; i < 200; i++)
            {
                if(Main.player[i].active && (Main.player[i].Center - Projectile.Center).Length() < 88)
                {
                    if (Main.player[i].position.X + Main.player[i].width * 0.5 > Projectile.position.X + Projectile.width * 0.5)
                    {
                        if (Projectile.velocity.X < 4f + Main.player[i].velocity.X)
                        {
                            Projectile.velocity.X = Projectile.velocity.X + 0.45f;
                        }
                        if (Projectile.velocity.X < 0f)
                        {
                            Projectile.velocity.X = Projectile.velocity.X + 0.45f * 0.75f;
                        }
                    }
                    else
                    {
                        if (Projectile.velocity.X > -4f + Main.player[i].velocity.X)
                        {
                            Projectile.velocity.X = Projectile.velocity.X - 0.45f;
                        }
                        if (Projectile.velocity.X > 0f)
                        {
                            Projectile.velocity.X = Projectile.velocity.X - 0.45f * 0.75f;
                        }
                    }
                    if (Main.player[i].position.Y + Main.player[i].height * 0.5 > Projectile.position.Y + Projectile.height * 0.5)
                    {
                        if (Projectile.velocity.Y < 4f)
                        {
                            Projectile.velocity.Y = Projectile.velocity.Y + 0.45f;
                        }
                        if (Projectile.velocity.Y < 0f)
                        {
                            Projectile.velocity.Y = Projectile.velocity.Y + 0.45f * 0.75f;
                        }
                    }
                    else
                    {
                        if (Projectile.velocity.Y > -4f)
                        {
                            Projectile.velocity.Y = Projectile.velocity.Y - 0.45f;
                        }
                        if (Projectile.velocity.Y > 0f)
                        {
                            Projectile.velocity.Y = Projectile.velocity.Y - 0.45f * 0.75f;
                        }
                    }
                    isGrabbing = true;
                }

                if(Main.player[i].active && (Main.player[i].Center - Projectile.Center).Length() < 10)
                {
                    SoundEngine.PlaySound(SoundID.Item2, Projectile.position);
                    Main.player[i].HealEffect(-5, false);
                    Main.player[i].statLife -= 5;
                    NetMessage.SendData(MessageID.SpiritHeal, -1, -1, null, i, -5, 0f, 0f, 0, 0, 0);
                    if (Main.player[i].statLife <= 0)
                    {
                        Main.player[i].KillMe(PlayerDeathReason.ByProjectile(i, Projectile.whoAmI), 1000.0, 0, false);
                    }
                    Projectile.Kill();
                }
            }
            
        }
    }
}