using AAModClassic._Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.Weapons;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.UI.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny._PostMoonlord.Items.Armor
{
    public class ChampionHelmetSummonerSetEffect_BaronBunny : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Baron Bunny");
			Main.projFrames[Projectile.type] = 8;
        }
    	
        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                Projectile.width = 66;
                Projectile.height = 60;
            }
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 18000;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            Projectile.minionSlots = 0;
        }

        int MeterF = 0;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!player.dead && player.GetModPlayer<ChampionHelmetSummonerSetPlayer>().effect)
            {
                Projectile.timeLeft = 2;
            }

            float shootInterval = 90f;

            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                if ((float)player.statLife < (float)player.statLifeMax2 * (1f / 5f))
                {
                    MeterF = 4;
                    shootInterval = 20f;
                    Projectile.damage = (int)player.GetDamage(DamageClass.Summon).ApplyTo(250);
                }
                else if ((float)player.statLife < (float)player.statLifeMax2 * (2f / 5f))
                {
                    MeterF = 3; 
                    shootInterval = 40f;
                    Projectile.damage = (int)player.GetDamage(DamageClass.Summon).ApplyTo(210);
                }
                else if ((float)player.statLife < (float)player.statLifeMax2 * (3f / 5f))
                {
                    MeterF = 2;
                    shootInterval = 60f;
                    Projectile.damage = (int)player.GetDamage(DamageClass.Summon).ApplyTo(190);
                }
                else if ((float)player.statLife < (float)player.statLifeMax2 * (4f / 5f))
                {
                    MeterF = 1;
                    shootInterval = 70f;
                    Projectile.damage = (int)player.GetDamage(DamageClass.Summon).ApplyTo(170);
                }
                else
                {
                    MeterF = 0;
                    Projectile.damage = (int)player.GetDamage(DamageClass.Summon).ApplyTo(150);
                }
            }
            else
            {
                // this is a shortened ver of the og code. this is basically what it was doing
                if (player.statLife < player.statLifeMax2 * (4 / 5))
                {
                    MeterF = 1; shootInterval = 70f;
                    Projectile.damage = (int)player.GetDamage(DamageClass.Summon).ApplyTo(170);
                }
                else
                {
                    MeterF = 5;
                    Projectile.damage = (int)player.GetDamage(DamageClass.Summon).ApplyTo(150);
                }
            }

            float num633 = 700f;
			float num634 = 800f;
			float num635 = 1200f;
			float num636 = 150f;
			float num637 = 0.05f;
			for (int num638 = 0; num638 < 1000; num638++)
			{
				bool flag23 = Main.projectile[num638].type == ModContent.ProjectileType<ChampionHelmetSummonerSetEffect_BaronBunny>();
				if (num638 != Projectile.whoAmI && Main.projectile[num638].active && Main.projectile[num638].owner == Projectile.owner && flag23 && Math.Abs(Projectile.position.X - Main.projectile[num638].position.X) + Math.Abs(Projectile.position.Y - Main.projectile[num638].position.Y) < Projectile.width)
				{
					if (Projectile.position.X < Main.projectile[num638].position.X)
					{
						Projectile.velocity.X = Projectile.velocity.X - num637;
					}
					else
					{
						Projectile.velocity.X = Projectile.velocity.X + num637;
					}
					if (Projectile.position.Y < Main.projectile[num638].position.Y)
					{
						Projectile.velocity.Y = Projectile.velocity.Y - num637;
					}
					else
					{
						Projectile.velocity.Y = Projectile.velocity.Y + num637;
					}
				}
			}
			bool flag24 = false;
			if (flag24)
			{
				return;
			}
			Vector2 vector46 = Projectile.position;
			bool flag25 = false;
			if (Projectile.ai[0] != 1f)
			{
				Projectile.tileCollide = false;
			}
			if (Projectile.tileCollide && WorldGen.SolidTile(Framing.GetTileSafely((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16)))
			{
				Projectile.tileCollide = false;
			}
			for (int num645 = 0; num645 < 200; num645++)
			{
				NPC nPC2 = Main.npc[num645];
				if (nPC2.CanBeChasedBy(Projectile, false))
				{
					float num646 = Vector2.Distance(nPC2.Center, Projectile.Center);
					if ((Vector2.Distance(Projectile.Center, vector46) > num646 && num646 < num633 || !flag25) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, nPC2.position, nPC2.width, nPC2.height))
					{
						num633 = num646;
						vector46 = nPC2.Center;
						flag25 = true;
					}
				}
			}
			float num647 = num634;
			if (flag25)
			{
				num647 = num635;
			}
			if (Vector2.Distance(player.Center, Projectile.Center) > num647)
			{
				Projectile.ai[0] = 1f;
				Projectile.tileCollide = false;
				Projectile.netUpdate = true;
			}
			if (flag25 && Projectile.ai[0] == 0f)
			{
				Vector2 vector47 = vector46 - Projectile.Center;
				float num648 = vector47.Length();
				vector47.Normalize();
				if (num648 > 200f)
				{
					float scaleFactor2 = 6f;
					vector47 *= scaleFactor2;
					Projectile.velocity = (Projectile.velocity * 40f + vector47) / 41f;
				}
				else
				{
					float num649 = 4f;
					vector47 *= -num649;
					Projectile.velocity = (Projectile.velocity * 40f + vector47) / 41f;
				}
			}
			else
			{
				bool flag26 = false;
				if (!flag26)
				{
					flag26 = Projectile.ai[0] == 1f;
				}
				float num650 = 6f;
				if (flag26)
				{
					num650 = 15f;
				}
				Vector2 center2 = Projectile.Center;
				Vector2 vector48 = player.Center - center2 + new Vector2(0f, -60f);
				float num651 = vector48.Length();
				if (num651 > 200f && num650 < 8f)
				{
					num650 = 8f;
				}
				if (num651 < num636 && flag26 && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
				{
					Projectile.ai[0] = 0f;
					Projectile.netUpdate = true;
				}
				if (num651 > 2000f)
				{
					Projectile.position.X = Main.player[Projectile.owner].Center.X - Projectile.width / 2;
					Projectile.position.Y = Main.player[Projectile.owner].Center.Y - Projectile.height / 2;
					Projectile.netUpdate = true;
				}
				if (num651 > 70f)
				{
					vector48.Normalize();
					vector48 *= num650;
					Projectile.velocity = (Projectile.velocity * 40f + vector48) / 41f;
				}
				else if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
				{
					Projectile.velocity.X = -0.15f;
					Projectile.velocity.Y = -0.05f;
				}
			}

            int thisBugIsImportant = WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) ? 3 : 4;
            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 10)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
            }
            if (Projectile.frame > thisBugIsImportant)
            {
                Projectile.frame = 0;
            }

			if (Projectile.ai[1] > 0f)
            {
                Projectile.ai[1] += Main.rand.Next(1, 4);
            }
            if (Projectile.ai[1] > shootInterval)
            {
                Projectile.ai[1] = 0f;
                Projectile.netUpdate = true;
            }

			if (Projectile.ai[0] == 0f)
			{
				float scaleFactor3 = 10f;
				if (flag25 && Projectile.ai[1] == 0f)
				{
					Projectile.ai[1] += 1f;
                    int num658 = Main.rand.Next(3);
                    if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
					{
                        switch (num658)
                        {
                            case 0:
                                num658 = ModContent.ProjectileType<RabbitBeam>();
                                break;
                            case 1:
                                num658 = ModContent.ProjectileType<ChampionHelmetSummonerSetEffect_BaronsSpear>();
                                break;
                            case 2:
                                num658 = ModContent.ProjectileType<ChampionHelmetSummonerSetEffect_Carrot>();
                                break;

                        }
                    }

                    if (Main.myPlayer == Projectile.owner && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, vector46, 0, 0))
					{
                        if (num658 == ModContent.ProjectileType<ChampionHelmetSummonerSetEffect_Carrot>())
                        {
                            float spread = 45f * 0.0174f;
                            Vector2 dir = Vector2.Normalize(vector46 - Projectile.Center);
                            dir *= scaleFactor3;
                            float baseSpeed = (float)Math.Sqrt(dir.X * dir.X + dir.Y * dir.Y);
                            double startAngle = Math.Atan2(dir.X, dir.Y) - .1d;
                            double deltaAngle = spread / 6f;
                            for (int i = 0; i < 3; i++)
                            {
                                double offsetAngle = startAngle + deltaAngle * i;
                                Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), num658, Projectile.damage, 5, Main.myPlayer);
                            }
                        }
                        else
                        {
                            Vector2 value19 = vector46 - Projectile.Center;
                            value19.Normalize();
                            value19 *= scaleFactor3;
                            int num659 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, value19.X, value19.Y, num658, Projectile.damage, 0f, Main.myPlayer, 0f, 0f);
                            Main.projectile[num659].timeLeft = 300;
                            Main.projectile[num659].hostile = false;
                            Main.projectile[num659].friendly = true;
                            Projectile.netUpdate = true;
                        }
                        Projectile.netUpdate = true;
                    }
				}
			}

            Projectile.rotation = 0;
        }

        //TODO: so much of this is broken and probably should be fixed
        public override bool PreDraw(ref Color lightColor)
        {
            Player player = Main.player[Projectile.owner];

            Texture2D tex = TextureAssets.Projectile[Projectile.type].Value;
            Texture2D Mad = ModContent.Request<Texture2D>(Texture + "_Mad").Value;
            Texture2D Eyes = ModContent.Request<Texture2D>(Texture + "_MadEyes").Value;
            Texture2D Meter = ModContent.Request<Texture2D>(Texture + "_Meter").Value;
            Texture2D MeterGlow = ModContent.Request<Texture2D>(Texture + "_Meter_Glow").Value;

            int frameBonusShit = 0;
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                if (Projectile.ai[1] <= 10)
                    frameBonusShit = 4;
                else
                    frameBonusShit = 0;
            }

            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame + frameBonusShit, tex.Width, tex.Height / 8, 0, 0);
            Rectangle MeterFrame = BaseDrawing.GetFrame(MeterF, Meter.Width, Meter.Height / 5, 0, 0);

            Vector2 MeterOffset = new Vector2(Meter.Width / 2, 52);

            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                if ((float)player.statLife < (float)player.statLifeMax2 * (1f / 5f))
                {
                    int shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingRainbowDye);
                    BaseDrawing.DrawTexture(Main.spriteBatch, MeterGlow, shader, Projectile.Center - MeterOffset, MeterFrame.Width, MeterFrame.Height, Projectile.scale, Projectile.rotation, 0, 5, MeterFrame, Color.White, true);
                    BaseDrawing.DrawTexture(Main.spriteBatch, Eyes, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 8, frame, Main.DiscoColor, true);
                }

                BaseDrawing.DrawTexture(Main.spriteBatch, Meter, 0, Projectile.Center - MeterOffset, MeterFrame.Width, MeterFrame.Height, Projectile.scale, Projectile.rotation, 0, 5, MeterFrame, Color.White, true);
            }

            BaseDrawing.DrawTexture(Main.spriteBatch, tex, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 8, frame, lightColor, true);
            
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                if ((float)player.statLife < (float)player.statLifeMax2 * (3f / 5f))
                {
                    BaseDrawing.DrawTexture(Main.spriteBatch, Mad, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 8, frame, lightColor, true);
                }

                if ((float)player.statLife < (float)player.statLifeMax2 * (1f / 5f))
                {
                    int shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingRainbowDye);
                    BaseDrawing.DrawTexture(Main.spriteBatch, MeterGlow, shader, Projectile.Center - MeterOffset, MeterFrame.Width, MeterFrame.Height, Projectile.scale, Projectile.rotation, 0, 5, MeterFrame, Color.White, true);
                    BaseDrawing.DrawTexture(Main.spriteBatch, Eyes, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, 0, 8, frame, Main.DiscoColor, true);
                }
            }

            return false;
        }

    }
}