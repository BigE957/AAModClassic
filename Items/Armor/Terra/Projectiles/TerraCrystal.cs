using AAModClassic;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Armor.Terra.Projectiles
{
    public class TerraCrystal : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Crystal");
            Main.projFrames[Projectile.type] = 3;
		}

		public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 42;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft *= 5;
            Projectile.light = 0.4f;
            Projectile.ignoreWater = true;
            Projectile.minion = true;
            Projectile.minionSlots = 0;
        }

		Vector2 PlayerPoint = Vector2.Zero;
        float intAI = 0;

        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(intAI);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                intAI = reader.ReadFloat();
            }
        }

        public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (!modPlayer.TerraSu)
			{
				Projectile.Kill();
				return;
			}

            switch (modPlayer.CrystalMode)
            {
                case 0: Projectile.frame = 0; break;
                case 1: Projectile.frame = 1; break;
                case 2: Projectile.frame = 2; break;
                default: Projectile.frame = 0; break;
            }

            if (modPlayer.CrystalMode != 1)
			{
				PlayerPoint.X = player.Center.X - Projectile.width / 2;
				PlayerPoint.Y = player.Center.Y - Projectile.height / 2 + player.gfxOffY - 60f;

				MoveToPoint(PlayerPoint);

				if (player.gravDir == -1f)
				{
					Projectile.position.Y = Projectile.position.Y + 120f;
					Projectile.rotation = 3.14f;
				}
				else
				{
					Projectile.rotation = 0f;
				}

				if (modPlayer.CrystalMode == 0)
				{
					if (Projectile.owner == Main.myPlayer)
					{
						if (intAI != 0f)
						{
                            intAI -= 1f;
							return;
						}
						float num396 = Projectile.position.X;
						float num397 = Projectile.position.Y;
						float num398 = 700f;
						bool flag11 = false;
						for (int num399 = 0; num399 < 200; num399++)
						{
							if (Main.npc[num399].CanBeChasedBy(this, true))
							{
								float num400 = Main.npc[num399].position.X + Main.npc[num399].width / 2;
								float num401 = Main.npc[num399].position.Y + Main.npc[num399].height / 2;
								float num402 = Math.Abs(Projectile.position.X + Projectile.width / 2 - num400) + Math.Abs(Projectile.position.Y + Projectile.height / 2 - num401);
								if (num402 < num398 && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, Main.npc[num399].position, Main.npc[num399].width, Main.npc[num399].height))
								{
									num398 = num402;
									num396 = num400;
									num397 = num401;
									flag11 = true;
								}
							}
						}
						if (flag11)
						{
							float num403 = 12f;
							Vector2 vector29 = new Vector2(Projectile.position.X + Projectile.width * 0.5f, Projectile.position.Y + Projectile.height * 0.5f);
							float num404 = num396 - vector29.X;
							float num405 = num397 - vector29.Y;
							float num406 = (float)Math.Sqrt(num404 * num404 + num405 * num405);
							num406 = num403 / num406;
							num404 *= num406;
							num405 *= num406;
							int p = Projectile.NewProjectile(Projectile.Center.X - 4f, Projectile.Center.Y, num404, num405, ModContent.ProjectileType<TerraSphere>(), Player.crystalLeafDamage, Player.crystalLeafKB, Projectile.owner, 0f, 0f);
							Main.projectile[p].melee = false/* tModPorter Suggestion: Remove. See Item.DamageType */;
							Main.projectile[p].minion = true;
							Main.projectile[p].minionSlots = 0;
                            intAI = 50f;
							return;
						}
					}
				}
                else
                {
                    if (player.statLife < player.statLifeMax2)
                    {
                        if (Main.rand.Next(3) == 0)
                        {
                            Vector2 Vel = (player.Center - player.Center) * 0.05f;
                            int dustID = Dust.NewDust(Projectile.Center, 0, 0, DustID.GoldFlame, Vel.X, Vel.Y, 50);
                            Main.dust[dustID].noGravity = true;
                        }
                    }
                }
			}
			else
			{
                Projectile.rotation = Projectile.velocity.X * 0.04f;
                if (Math.Abs(Projectile.velocity.X) > 0.2)
                {
                    Projectile.spriteDirection = -Projectile.direction;
                }
                float num633 = 700f;
                float num634 = 800f;
                float num635 = 1200f;
                float num636 = 150f;
                float num637 = 0.05f;
                for (int num638 = 0; num638 < 1000; num638++)
                {
                    bool flag23 = Main.projectile[num638].type == Mod.Find<ModProjectile>("TerraCrystal").Type;
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
                if (Projectile.ai[0] == 2f)
                {
                    Projectile.ai[1] += 1f;
                    Projectile.extraUpdates = 1;
                    if (Projectile.ai[1] > 40f)
                    {
                        Projectile.ai[1] = 1f;
                        Projectile.ai[0] = 0f;
                        Projectile.extraUpdates = 0;
                        Projectile.numUpdates = 0;
                        Projectile.netUpdate = true;
                    }
                    else
                    {
                        flag24 = true;
                    }
                }
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
                if (player.HasMinionAttackTargetNPC)
                {
                    NPC nPC2 = Main.npc[player.MinionAttackTargetNPC];
                    if (nPC2.CanBeChasedBy(Projectile, false))
                    {
                        float num646 = Vector2.Distance(nPC2.Center, Projectile.Center);
                        if (((Vector2.Distance(Projectile.Center, vector46) > num646 && num646 < num633) || !flag25) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, nPC2.position, nPC2.width, nPC2.height))
                        {
                            vector46 = nPC2.Center;
                            flag25 = true;
                        }
                    }
                }
                else
                {
                    for (int num645 = 0; num645 < 200; num645++)
                    {
                        NPC nPC2 = Main.npc[num645];
                        if (nPC2.CanBeChasedBy(Projectile, false))
                        {
                            float num646 = Vector2.Distance(nPC2.Center, Projectile.Center);
                            if (((Vector2.Distance(Projectile.Center, vector46) > num646 && num646 < num633) || !flag25) && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, nPC2.position, nPC2.width, nPC2.height))
                            {
                                num633 = num646;
                                vector46 = nPC2.Center;
                                flag25 = true;
                            }
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
                        float scaleFactor2 = 8f;
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
                    float num650 = 5f; //6
                    if (flag26)
                    {
                        num650 = 12f; //15
                    }
                    Vector2 center2 = Projectile.Center;
                    Vector2 vector48 = player.Center - center2 + new Vector2(0f, -30f); //-60
                    float num651 = vector48.Length();
                    if (num651 > 200f && num650 < 6.5f) //200 and 8
                    {
                        num650 = 6.5f; //8
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
                        Projectile.velocity.X = -0.2f;
                        Projectile.velocity.Y = -0.1f;
                    }
                }
                if (Projectile.ai[1] > 0f)
                {
                    Projectile.ai[1] += Main.rand.Next(1, 4);
                }
                if (Projectile.ai[1] > 80f)
                {
                    Projectile.ai[1] = 0f;
                    Projectile.netUpdate = true;
                }
                if (Projectile.ai[0] == 0f)
                {
                    float scaleFactor3 = 24f;
                    int num658 = ModContent.ProjectileType<TerraSphere>();
                    if (flag25 && Projectile.ai[1] == 0f)
                    {
                        Projectile.ai[1] += 1f;
                        if (Main.myPlayer == Projectile.owner && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, vector46, 0, 0))
                        {
                            Vector2 value19 = vector46 - Projectile.Center;
                            value19.Normalize();
                            value19 *= scaleFactor3;
                            int num659 = Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, value19.X, value19.Y, num658, (int)(80 * player.GetDamage(DamageClass.Summon)), 0f, Main.myPlayer, 0f, 0f);
                            Main.projectile[num659].melee = false/* tModPorter Suggestion: Remove. See Item.DamageType */; 
                            Main.projectile[num659].minion = true;
                            Main.projectile[num659].minionSlots = 0;
                            Main.projectile[num659].timeLeft = 300;
                            Projectile.netUpdate = true;
                        }
                    }
                }
            }
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(ref Color lightColor)
        {
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }

            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / 3, 0, 0);

            BaseDrawing.DrawAura(sb, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, auraPercent, 1.4f, Projectile.scale, Projectile.rotation, Projectile.direction, 3, frame, 0, 0, Color.White);
            BaseDrawing.DrawTexture(sb, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile.position, Projectile.width, Projectile.height, Projectile.scale, Projectile.rotation, Projectile.direction, 3, frame, Projectile.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);

            return false;
        }

        public void MoveToPoint(Vector2 point)
		{
			float moveSpeed = 20f;
			float velMultiplier = 1f;
			Vector2 dist = point - Projectile.Center;
			float length = dist == Vector2.Zero ? 0f : dist.Length();
			if (length < moveSpeed)
			{
				velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
			}
			if (length < 200f)
			{
				moveSpeed *= 0.5f;
			}
			if (length < 100f)
			{
				moveSpeed *= 0.5f;
			}
			if (length < 50f)
			{
				moveSpeed *= 0.5f;
			}
			if (length < 10f)
			{
				moveSpeed *= 0.01f;
			}
			Projectile.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
			Projectile.velocity *= moveSpeed;
			Projectile.velocity *= velMultiplier;
		}
	}
}
