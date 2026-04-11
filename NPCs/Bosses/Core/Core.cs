using System;
using System.Collections.Generic;
using System.IO;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.Materials;
using AAModClassic.Music;
using AAModClassic.NPCs.Bosses.Athena.Olympian;
using AAModClassic.NPCs.Bosses.Core.Projectiles;
using AAModClassic.NPCs.Enemies.Terrarium.Hardmode;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Core;

[AutoloadBossHead]
public class Core : ModNPC
{
	public float[] internalAI = new float[4];

	public int frameShell;

	public float RingRoatation;

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Biomite Core");
		Main.npcFrameCount[NPC.type] = 8;
	}

	public override void SetDefaults()
	{
		NPC.lifeMax = 6000;
		NPC.boss = true;
		NPC.defense = 0;
		NPC.damage = 40;
		NPC.width = 74;
		NPC.height = 70;
		NPC.aiStyle = -1;
		NPC.value = Item.sellPrice(0, 16);
		NPC.HitSound = SoundID.NPCHit4;
		NPC.DeathSound = SoundID.NPCDeath14;
		Music = MusicManagementSystem.MusicSlots["BiomiteCore"];
		NPC.knockBackResist = 0f;
		NPC.noGravity = true;
		NPC.alpha = 255;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		SendExtraAI(writer);
		if (Main.netMode == NetmodeID.Server || Main.dedServ)
		{
			writer.Write(internalAI[0]);
			writer.Write(internalAI[1]);
			writer.Write(internalAI[2]);
			writer.Write(internalAI[3]);
		}
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		ReceiveExtraAI(reader);
		if (Main.netMode == NetmodeID.MultiplayerClient)
		{
			internalAI[0] = reader.ReadSingle();
			internalAI[1] = reader.ReadSingle();
			internalAI[2] = reader.ReadSingle();
			internalAI[3] = reader.ReadSingle();
		}
	}

	public override void AI()
	{
		Vector2 val = Origin();
		Vector2 val2 = val + Vector16(140, 125) + Vector16(5, 4);
		Vector2 val3 = val + Vector16(106, 129) + Vector16(5, 4);
		Vector2 val4 = val + Vector16(174, 129) + Vector16(5, 4);
		Vector2 val5 = val + Vector16(113, 151) + Vector16(5, 4);
		Vector2 val6 = val + Vector16(167, 151) + Vector16(5, 4);
		Vector2 val7 = val + Vector16(140, 156) + Vector16(5, 4);
		if (NPC.ai[3] > 16f)
		{
			NPC.ai[3] = 1f;
		}
		if (internalAI[0] != 1f && !NPCExtensions.BeenKilled<Core>())
		{
			NPC.dontTakeDamage = true;
			NPC.Center = val2;
			if (internalAI[1] % 10f == 0f)
			{
				NPC.ai[3] += 1f;
			}
			internalAI[1] += 1f;
			if (internalAI[1] < 40f)
			{
				frameShell = 0;
			}
			if (internalAI[1] == 40f)
			{
				frameShell = 1;
			}
			if (internalAI[1] == 60f)
			{
				frameShell = 2;
			}
			if (internalAI[1] >= 80f)
			{
				frameShell = 3;
			}
			if (internalAI[1] >= 130f)
			{
				NPC.alpha -= 5;
			}
			if (internalAI[1] >= 220f && Main.netMode != NetmodeID.MultiplayerClient)
			{
				internalAI[0] += 1f;
				NPC.dontTakeDamage = false;
				NPC.netUpdate = true;
			}
			return;
		}
		if (!NPC.HasPlayerTarget)
		{
			NPC.TargetClosest();
		}
		Player player = Main.player[NPC.target];
		if (player.dead || !((Entity)player).active || NPC.position.X - Main.player[NPC.target].position.X > 6000f || NPC.position.X - Main.player[NPC.target].position.X < -6000f || NPC.position.Y - Main.player[NPC.target].position.Y > 6000f || NPC.position.Y - Main.player[NPC.target].position.Y < -6000f)
		{
			NPC.TargetClosest();
			player = Main.player[NPC.target];
			if (player.dead || !((Entity)player).active || NPC.position.X - Main.player[NPC.target].position.X > 6000f || NPC.position.X - Main.player[NPC.target].position.X < -6000f || NPC.position.Y - Main.player[NPC.target].position.Y > 6000f || NPC.position.Y - Main.player[NPC.target].position.Y < -6000f)
			{
				Item.NewItem(NPC.GetSource_GiftOrReward(), new Vector2(val.X + 144f, val.Y + 134f), ModContent.ItemType<TerraPrism>(), 1, false, 0, false, false);
				((Entity)NPC).active = false;
			}
		}
		NPC.ai[0] += 1f;
		if (NPC.ai[1] == 0f)
		{
			NPC.dontTakeDamage = true;
			if (NPC.ai[0] % 15f == 0f)
			{
				if (frameShell > 0)
				{
					frameShell--;
				}
				else
				{
					frameShell = 0;
				}
			}
			if (NPC.ai[0] > 75f && Main.netMode != NetmodeID.MultiplayerClient)
			{
				int num = Main.rand.Next(6);
				Vector2 center = NPC.Center;
				Vector2 val8 = center;
				int iters = 0;
				while (val8 == center)
				{
					val8 = (Vector2)(num switch
					{
						0 => val2, 
						1 => val4, 
						2 => val3, 
						3 => val5, 
						4 => val6, 
						_ => val7, 
					});
					iters++;
					if (iters > 100)
						break;
                }
				NPC.Center = val8;
				NPC.ai[0] = 0f;
				NPC.ai[1] = 1f;
				NPC.ai[3] = Main.rand.Next(1, 17);
				NPC.netUpdate = true;
			}
			return;
		}
		if (NPC.ai[1] == 1f)
		{
			if (NPC.ai[0] % 15f == 0f)
			{
				NPC.ai[3] += 1f;
			}
			int num2 = (Main.expertMode ? 60 : 90);
			if (NPC.ai[0] == (float)num2 && Main.netMode != NetmodeID.MultiplayerClient)
			{
				NPC.ai[0] = 0f;
				NPC.ai[1] = 2f;
				NPC.dontTakeDamage = false;
				NPC.netUpdate = true;
			}
			return;
		}
		if (NPC.ai[0] % 15f == 0f)
		{
			if (frameShell < 3)
			{
				frameShell++;
			}
			else
			{
				frameShell = 3;
			}
		}
		if (NPC.ai[0] > 90f)
		{
			switch ((int)NPC.ai[3])
			{
			default:
				if (NPC.ai[0] > 120f && NPC.CountNPCS(ModContent.NPCType<TerraProbe>()) + NPC.CountNPCS(ModContent.NPCType<TerraWatcher>()) < 5 && Main.netMode != NetmodeID.MultiplayerClient)
				{
					int num8 = ((Main.rand.NextBool(2)) ? ModContent.NPCType<TerraProbe>() : ModContent.NPCType<TerraWatcher>());
					int num9 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X + 100, (int)NPC.position.Y, num8, 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num9].Center = new Vector2(NPC.Center.X + 100f, NPC.Center.Y);

					num8 = ((Main.rand.NextBool(2)) ? ModContent.NPCType<TerraProbe>() : ModContent.NPCType<TerraWatcher>());
					int num10 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X - 100, (int)NPC.position.Y, num8, 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[num10].Center = new Vector2(NPC.Center.X - 100f, NPC.Center.Y);

					if (NPC.life < NPC.lifeMax / 2)
					{
						int num11 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y + 100, num8, 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[num11].Center = new Vector2(NPC.Center.X, NPC.Center.Y + 100f);
					}
					NPC.ai[0] = 0f;
					NPC.ai[1] = 0f;
					NPC.ai[3] = 0f;
				}
				break;
			case 2:
				if (NPC.ai[0] % 198f == 0f)
				{
					Sandstorm();
				}
				break;
			case 3:
				if (NPC.ai[0] % 91f == 0f)
				{
					for (int m = 0; m < 8; m++)
					{
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 5f * (float)Math.Sin((double)m * (Math.PI / 4.0)), 5f * (float)Math.Cos((double)m * (Math.PI / 4.0)), ProjectileID.CursedFlameHostile, 50, 1f, Main.myPlayer, -1f, 0f);
					}
				}
				break;
			case 5:
			{
				NPC.rotation += 0.01f;
				Vector2 val11 = NPC.rotation.ToRotationVector2();
				if (NPC.ai[0] % 6f == 0f)
				{
					SoundEngine.PlaySound(SoundID.Item34, NPC.position);
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, val11.X * 5f, val11.Y, ModContent.ProjectileType<InfernoBreath>(), 20, 0f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f - val11.X, 0f - val11.Y, ModContent.ProjectileType<InfernoBreath>(), 20, 0f, Main.myPlayer, 0f, 0f);
				}
				break;
			}
			case 7:
				if (NPC.ai[0] % 60f == 0f)
				{
					int num14 = Main.rand.Next(3, 7);
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 5f, (float)num14, ModContent.ProjectileType<HellFireball>(), 50, 1f, Main.myPlayer, -1f, 0f);
					num14 = Main.rand.Next(3, 7);
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, -5f, (float)(-num14), ModContent.ProjectileType<HellFireball>(), 50, 1f, Main.myPlayer, -1f, 0f);
					num14 = Main.rand.Next(3, 7);
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 5f, (float)(-num14), ModContent.ProjectileType<HellFireball>(), 50, 1f, Main.myPlayer, -1f, 0f);
					num14 = Main.rand.Next(3, 7);
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, -5f, (float)num14, ModContent.ProjectileType<HellFireball>(), 50, 1f, Main.myPlayer, -1f, 0f);
				}
				break;
			case 8:
			{
				Vector2 val12 = NPC.Center - new Vector2(125f, 100f);
				for (int k = 0; k < 10; k++)
				{
					Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), 250, 2, (Main.rand.NextBool(3)) ? 1 : 0);
				}
				if (NPC.ai[0] % 45f == 0f)
				{
					Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(val12.X + (float)Main.rand.Next(250), val12.Y), Vector2.Zero, ModContent.ProjectileType<Rock>(), 12, 0f, Main.myPlayer, 0f, 0f);
				}
				break;
			}
			case 10:
				if (NPC.ai[0] % 198f == 0f)
				{
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(4f, 4f), ModContent.ProjectileType<GlacierChunk>(), 12, 0f, Main.myPlayer, 0f, 0f);
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-4f, 4f), ModContent.ProjectileType<GlacierChunk>(), 12, 0f, Main.myPlayer, 0f, 0f);
				}
				break;
			case 11:
				if (NPC.ai[0] % 61f == 0f)
				{
					for (int l = 0; l < 6; l++)
					{
						Vector2 val13 = Main.player[NPC.target].Center - NPC.Center;
						val13.Y -= Math.Abs(val13.X) * 0.2f;
						val13.Normalize();
						val13 *= 8f;
						val13 += NPC.velocity / 3f;
						val13.X += (float)Main.rand.Next(-20, 21) * 0.08f;
						val13.Y += (float)Main.rand.Next(-20, 21) * 0.08f;
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, val13, ProjectileID.GoldenShowerHostile, 40, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				break;
			case 12:
				if (NPC.ai[0] % 20f == 0f)
				{
					int num6 = 6;
					int num7 = 0;
					if (Main.rand.NextBool(2))
					{
						num7 = 6;
					}
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)num6, (float)num7, ProjectileID.Skull, 50, 1f, Main.myPlayer, -1f, 0f);
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(-num6), (float)(-num7), ProjectileID.Skull, 50, 1f, Main.myPlayer, -1f, 0f);
					if (num7 != 0)
					{
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)num6, (float)(-num7), ProjectileID.Skull, 50, 1f, Main.myPlayer, -1f, 0f);
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(-num6), (float)num7, ProjectileID.Skull, 50, 1f, Main.myPlayer, -1f, 0f);
					}
				}
				break;
			case 15:
				if (NPC.ai[0] % 120f == 0f)
				{
					int num12 = 6;
					int num13 = 0;
					if (Main.rand.NextBool(2))
					{
						num13 = 6;
					}
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)num12, (float)num13, ModContent.ProjectileType<Rainbow>(), 50, 1f, Main.myPlayer, -1f, 0f);
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(-num12), (float)(-num13), ModContent.ProjectileType<Rainbow>(), 50, 1f, Main.myPlayer, -1f, 0f);
					if (num13 != 0)
					{
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)num12, (float)(-num13), ModContent.ProjectileType<Rainbow>(), 50, 1f, Main.myPlayer, -1f, 0f);
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(-num12), (float)num13, ModContent.ProjectileType<Rainbow>(), 50, 1f, Main.myPlayer, -1f, 0f);
					}
				}
				break;
			case 16:
			{
				if (NPC.ai[0] % 60f != 0f || Main.netMode == NetmodeID.MultiplayerClient)
				{
					break;
				}
				int[] array = new int[5];
				Vector2[] array2 = (Vector2[])(object)new Vector2[5];
				int num3 = 0;
				float num4 = 2000f;
				for (int i = 0; i < 255; i++)
				{
					if (!((Entity)Main.player[i]).active || Main.player[i].dead)
					{
						continue;
					}
					Vector2 center2 = Main.player[i].Center;
					if (Vector2.Distance(center2, NPC.Center) < num4 && Collision.CanHit(NPC.Center, 1, 1, center2, 1, 1))
					{
						array[num3] = i;
						array2[num3] = center2;
						if (++num3 >= array2.Length)
						{
							break;
						}
					}
				}
				for (int j = 0; j < num3; j++)
				{
					Vector2 val9 = array2[j] - NPC.Center;
					float num5 = Main.rand.Next(100);
					Vector2 val10 = Vector2.Normalize(val9.RotatedByRandom(0.7853981852531433)) * 14f;
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, val10.X, val10.Y, ModContent.ProjectileType<AthenaShock>(), NPC.damage, 0f, Main.myPlayer, val9.ToRotation(), num5);
				}
				break;
			}
			case 4:
			case 6:
			case 9:
			case 13:
			case 14:
				break;
			}
			int num15 = ((NPC.life < (int)((float)NPC.lifeMax * 0.66f)) ? 220 : ((NPC.life < NPC.lifeMax / 3) ? 260 : 300));
			if (NPC.ai[0] > (float)num15)
			{
				NPC.ai[0] = 0f;
				NPC.ai[1] = 0f;
			}
		}
		NPC.direction = (NPC.spriteDirection = 1);
	}

	public void Sandstorm()
	{
		List<Point> list = new List<Point>();
		Point val = (Main.player[NPC.target].Center + new Vector2(Main.player[NPC.target].velocity.X * 30f, 0f)).ToTileCoordinates();
		for (int i = 0; i < 1000; i++)
		{
			if (list.Count >= 3)
			{
				break;
			}
			bool flag = false;
			int num = Main.rand.Next(val.X - 30, val.X + 30 + 1);
			foreach (Point item in list)
			{
				if (Math.Abs(item.X - num) < 10)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				int startY = val.Y - 20;
				Collision.ExpandVertically(num, startY, out var _, out var bottomY, 1, 51);
				if (StrayMethods.CanSpawnSandstormHostile(new Vector2((float)num, (float)(bottomY - 15)) * 16f, 15, 15))
				{
					list.Add(new Point(num, bottomY - 15));
				}
			}
		}
		foreach (Point item2 in list)
		{
			Projectile.NewProjectile(NPC.GetSource_FromThis(), (float)(item2.X * 16), (float)(item2.Y * 16), 0f, 0f, ModContent.ProjectileType<SandstormProj>(), 0, 0f, Main.myPlayer, 0f, 0f);
		}
	}

    public override void HitEffect(NPC.HitInfo hit)
    {
		if (NPC.life <= 0)
		{
			int num = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.Terra);
			Dust obj = Main.dust[num];
			obj.velocity *= 0.5f;
			Main.dust[num].scale *= 1.3f;
			Main.dust[num].fadeIn = 1f;
			Main.dust[num].noGravity = false;
		}
	}

    public override void OnKill()
    {
        Main.rand.Next(10);
        if (true)//!Main.expertMode)
        {
            //Main.rand.Next(7); ?????????????
            Item.NewItem(NPC.GetSource_FromThis(), NPC.Center, ModContent.ItemType<TerraPrism>(), Main.rand.Next(1, 4), false, 0, false, false);
        }
        else
        {
            //NPC.DropBossBags(); bagless...
        }
    }

	public override void FindFrame(int frameHeight)
	{
		NPC.frameCounter += 1.0;
		if (NPC.frameCounter > 10.0)
		{
			NPC.frameCounter = 0.0;
			NPC.frame.Y += frameHeight;
			if (NPC.frame.Y > frameHeight * 7)
			{
				NPC.frame.Y = 0;
			}
		}
	}

	public static Vector2 Origin()
	{
		Point val = new((int)((float)Main.maxTilesX * 0.35f), (int)((float)Main.maxTilesY * 0.38f));
		if (Main.dungeonX < Main.maxTilesX / 2)
		{
            val = new((int)((float)Main.maxTilesX * 0.65f), (int)((float)Main.maxTilesY * 0.38f));
		}
		return new Vector2((float)val.X * 16f, (float)val.Y * 16f);
	}

	public static Vector2 Vector16(int x, int y)
	{
		return new Vector2((float)(x * 16), (float)(y * 16));
	}

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
		Texture2D texture = AAMod.instance.GetTexture("NPCs/Bosses/Core/CoreBack");
		Texture2D texture2 = TextureAssets.Npc[Type].Value;
		Texture2D texture3 = AAMod.instance.GetTexture("NPCs/Bosses/Core/CoreShell");
		Texture2D texture4 = AAMod.instance.GetTexture("NPCs/Bosses/Core/CoreGlow");
		Rectangle frame = BaseDrawing.GetFrame(frameShell, 156, 128, 0, 0);
		Rectangle frame2 = BaseDrawing.GetFrame((int)NPC.ai[3] - 1, 156, 128, 0, 0);
		Rectangle frame3 = BaseDrawing.GetFrame(0, 156, 128, 0, 0);
		BaseDrawing.DrawTexture(spriteBatch, texture, 0, NPC.position, NPC.width, NPC.height, 1f, 0f, 0, 1, frame3, drawColor, drawCentered: true);
		BaseDrawing.DrawTexture(spriteBatch, texture2, 0, NPC.position, NPC.width, NPC.height, 1f, 0f, 0, 8, NPC.frame, NPC.GetAlpha(GlowColor()), drawCentered: true);
		BaseDrawing.DrawTexture(spriteBatch, texture3, 0, NPC.position, NPC.width, NPC.height, 1f, 0f, 1, 4, frame, drawColor, drawCentered: true);
		BaseDrawing.DrawTexture(spriteBatch, texture4, 0, NPC.position, NPC.width, NPC.height, 1f, 0f, 0, 16, frame2, Color.White, drawCentered: true);
		return false;
	}

	public Color GlowColor()
	{
		return (Color)((int)NPC.ai[3] switch
		{
			1 => Color.Green, 
			2 => Color.Yellow, 
			3 => new Color(104, 90, 144), 
			4 => Color.DarkGreen, 
			5 => Color.OrangeRed, 
			6 => Color.MediumSlateBlue, 
			7 => Color.DarkOrange, 
			8 => Color.Sienna, 
			9 => new Color(50, 50, 60), 
			10 => Color.White, 
			11 => Color.Red, 
			12 => Color.DarkSlateBlue, 
			13 => Color.Indigo, 
			14 => Color.Blue, 
			15 => Color.Fuchsia, 
			16 => Color.DeepSkyBlue, 
			_ => Color.Green, 
		});
	}
}
