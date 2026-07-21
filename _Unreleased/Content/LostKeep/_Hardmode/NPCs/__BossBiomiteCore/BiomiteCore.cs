using AAModClassic._Content.Acropolis._PostMoonlord.NPCs.__BossAthenaA;
using AAModClassic._Content.Terrarium.__Hardmode.Items.Materials;
using AAModClassic._Content.Terrarium.__Hardmode.NPCs;
using AAModClassic._Content.Terrarium.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Music;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.LostKeep._Hardmode.NPCs.__BossBiomiteCore;

[AutoloadBossHead]
public class BiomiteCore : ModNPC
{
	public float[] internalAI = new float[4];

	public int frameShell;

	//public float RingRoatation;

    public static Asset<Texture2D> Glowmask;
    public static Asset<Texture2D> CoreBack;
    public static Asset<Texture2D> CoreFront;

    public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Biomite Core");
		Main.npcFrameCount[NPC.type] = 8;

		if (!Main.dedServ)
		{
			Glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
			CoreBack = ModContent.Request<Texture2D>(Texture + "_Back");
			CoreFront = ModContent.Request<Texture2D>(Texture + "_Front");
		}
        NPCID.Sets.BossBestiaryPriority.Add(Type);
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
		NPC.value = Item.buyPrice(0, 16);
		NPC.HitSound = SoundID.NPCHit4;
		NPC.DeathSound = SoundID.NPCDeath14;
		Music = MusicManagementSystem.MusicSlots["BiomiteCore"];
		NPC.knockBackResist = 0f;
		NPC.noGravity = true;
        if (!NPC.IsABestiaryIconDummy)
            NPC.alpha = 255;
        SpawnModBiomes = [ModContent.GetInstance<TerrariumBiome>().Type];
    }

    public override void OnSpawn(IEntitySource source)
    {
        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.CoreSpawn"), new Color(175, 75, 255));
		internalAI[0] = 1;
		NPC.netUpdate = true;
    }

    public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(internalAI[0]);
		writer.Write(internalAI[1]);
		writer.Write(internalAI[2]);
		writer.Write(internalAI[3]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		internalAI[0] = reader.ReadSingle();
		internalAI[1] = reader.ReadSingle();
		internalAI[2] = reader.ReadSingle();
		internalAI[3] = reader.ReadSingle();
	}

	public override void AI()
	{
        NPC.netOffset = Vector2.Zero;

        Vector2 val = AAWorld_Unreleased.lostKeepOrigin.ToWorldCoordinates(0, 0);
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
		if (internalAI[0] != 1f && !NPCExtensions.BeenKilled<BiomiteCore>())
		{
			NPC.dontTakeDamage = true;
			NPC.Center = val2;
			NPC.Center = val2;
			NPC.netUpdate = true;
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
			if (internalAI[1] >= 220f)
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
		if (player.dead || !player.active || NPC.Center.DistanceSQ(NPC.Center) > 36000000)
		{
			NPC.TargetClosest();
			player = Main.player[NPC.target];
			if (player.dead || !player.active || NPC.Center.DistanceSQ(NPC.Center) > 36000000)
			{
				if(Main.netMode != NetmodeID.MultiplayerClient)
					Item.NewItem(NPC.GetSource_GiftOrReward(), new Vector2(val.X + 144f, val.Y + 134f), ModContent.ItemType<TerraPrism>(), 1, false, 0, false, false);
				NPC.active = false;
				return;
			}
		}
		NPC.ai[0] += 1f;
		if (NPC.ai[1] == 0f)
		{
			NPC.dontTakeDamage = true;
            NPC.netUpdate = true;
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
			if (NPC.ai[0] > 75f)
			{
				int num = Main.rand.Next(6);
				Vector2 center = NPC.Center;
				Vector2 moveTo = center;
				int iters = 0;
				while (moveTo == center)
				{
					moveTo = num switch
					{
						0 => val2, 
						1 => val4, 
						2 => val3, 
						3 => val5, 
						4 => val6, 
						_ => val7, 
					};
					iters++;
					if (iters > 100)
						break;
                }
				NPC.Center = moveTo;
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
			int num2 = Main.expertMode ? 60 : 90;
			if (NPC.ai[0] == num2)
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
					if (NPC.ai[0] > 120f && NPC.CountNPCS(ModContent.NPCType<UnityProbe>()) + NPC.CountNPCS(ModContent.NPCType<UnityWatcher>()) < 5)
					{
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							int num8 = ((Main.rand.NextBool(2)) ? ModContent.NPCType<UnityProbe>() : ModContent.NPCType<UnityWatcher>());
							int num9 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X + 100, (int)NPC.position.Y, num8, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
							Main.npc[num9].Center = new Vector2(NPC.Center.X + 100f, NPC.Center.Y);

							num8 = ((Main.rand.NextBool(2)) ? ModContent.NPCType<UnityProbe>() : ModContent.NPCType<UnityWatcher>());
							int num10 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X - 100, (int)NPC.position.Y, num8, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
							Main.npc[num10].Center = new Vector2(NPC.Center.X - 100f, NPC.Center.Y);

							if (NPC.life < NPC.lifeMax / 2)
							{
								int num11 = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y + 100, num8, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
								Main.npc[num11].Center = new Vector2(NPC.Center.X, NPC.Center.Y + 100f);
							}
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
					if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[0] % 91f == 0f)
						for (int m = 0; m < 8; m++)
						{
							Vector2 velocity = (m * MathHelper.PiOver4).ToRotationVector2() * 5f;
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, velocity, ProjectileID.CursedFlameHostile, 50, 1f, -1, -1f, 0f);
						}
					break;
				case 5:
					NPC.rotation += 0.01f;
					Vector2 val11 = NPC.rotation.ToRotationVector2();
					if (NPC.ai[0] % 6f == 0f)
					{
						SoundEngine.PlaySound(SoundID.Item34, NPC.position);
						if (Main.netMode != NetmodeID.MultiplayerClient)
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, val11.X * 5f, val11.Y, ModContent.ProjectileType<BiomiteCore_FireBreath>(), 20, 0f, -1, 0f, 0f);
							Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f - val11.X, 0f - val11.Y, ModContent.ProjectileType<BiomiteCore_FireBreath>(), 20, 0f, -1, 0f, 0f);
						}
					}
					break;
				case 7:
                    if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[0] % 60f == 0f)
					{
						int num14 = Main.rand.Next(3, 7);
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 5f, (float)num14, ModContent.ProjectileType<BiomiteCore_HellfireBlast>(), 50, 1f, -1, -1f, 0f);
						num14 = Main.rand.Next(3, 7);
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, -5f, (float)(-num14), ModContent.ProjectileType<BiomiteCore_HellfireBlast>(), 50, 1f, -1, -1f, 0f);
						num14 = Main.rand.Next(3, 7);
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 5f, (float)(-num14), ModContent.ProjectileType<BiomiteCore_HellfireBlast>(), 50, 1f, -1, -1f, 0f);
						num14 = Main.rand.Next(3, 7);
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, -5f, (float)num14, ModContent.ProjectileType<BiomiteCore_HellfireBlast>(), 50, 1f, -1, -1f, 0f);
					}
					break;
				case 8:
					Vector2 val12 = NPC.Center - new Vector2(125f, 100f);
					for (int k = 0; k < 10; k++)
						Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), 250, 2, (Main.rand.NextBool(3)) ? 1 : 0);

                    if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[0] % 45f == 0f)
						Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(val12.X + (float)Main.rand.Next(250), val12.Y), Vector2.Zero, ModContent.ProjectileType<BiomiteCore_Boulder>(), 12, 0f, -1, 0f, 0f);

					break;
				case 10:
                    if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[0] % 198f == 0f)
					{
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(4f, 4f), ModContent.ProjectileType<BiomiteCore_GlacierBomb>(), 12, 0f, -1, 0f, 0f);
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-4f, 4f), ModContent.ProjectileType<BiomiteCore_GlacierBomb>(), 12, 0f, -1, 0f, 0f);
					}
					break;
				case 11:
                    if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[0] % 61f == 0f)
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
							Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, val13, ProjectileID.GoldenShowerHostile, 40, 0f, -1, 0f, 0f);
						}
					}
					break;
				case 12:
                    if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[0] % 20f == 0f)
					{
						int num6 = 6;
						int num7 = 0;
						if (Main.rand.NextBool(2))
						{
							num7 = 6;
						}
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)num6, (float)num7, ProjectileID.Skull, 50, 1f, -1, -1f, 0f);
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(-num6), (float)(-num7), ProjectileID.Skull, 50, 1f, -1, -1f, 0f);
						if (num7 != 0)
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)num6, (float)(-num7), ProjectileID.Skull, 50, 1f, -1, -1f, 0f);
							Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(-num6), (float)num7, ProjectileID.Skull, 50, 1f, -1, -1f, 0f);
						}
					}
					break;
				case 15:
                    if (Main.netMode != NetmodeID.MultiplayerClient && NPC.ai[0] % 120f == 0f)
					{
						int num12 = 6;
						int num13 = 0;
						if (Main.rand.NextBool(2))
						{
							num13 = 6;
						}
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(num12, num13), ModContent.ProjectileType<BiomiteCore_Rainbow>(), 50, 1f, -1, -1f, 0f);
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-num12, -num13), ModContent.ProjectileType<BiomiteCore_Rainbow>(), 50, 1f, -1, -1f, 0f);
						if (num13 != 0)
						{
							Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(num12, -num13), ModContent.ProjectileType<BiomiteCore_Rainbow>(), 50, 1f, -1, -1f, 0f);
							Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, new Vector2(-num12, num13), ModContent.ProjectileType<BiomiteCore_Rainbow>(), 50, 1f, -1, -1f, 0f);
						}
					}
					break;
				case 16:
					if (NPC.ai[0] % 60f != 0f || Main.netMode == NetmodeID.MultiplayerClient)
						break;
					
					int[] array = new int[5];
					Vector2[] targetArray = new Vector2[5];
					int targetCount = 0;
					float maxDist = 2000f;
					foreach(Player p in Main.ActivePlayers)
					{
						if (p.dead)
							continue;
						
						Vector2 center2 = p.Center;
						if (Vector2.Distance(center2, NPC.Center) < maxDist && Collision.CanHit(NPC.Center, 1, 1, center2, 1, 1))
						{
							array[targetCount] = p.whoAmI;
							targetArray[targetCount] = center2;
							targetCount++;

                        }

                        if (targetCount >= targetArray.Length)
							break;                 
					}
					for (int j = 0; j < targetCount; j++)
					{
						Vector2 dir = targetArray[j] - NPC.Center;
						float randSeed = Main.rand.Next(100);
						Vector2 velocity = Vector2.Normalize(dir.RotatedByRandom(0.7853981852531433)) * 14f;
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, velocity, ModContent.ProjectileType<ChargedOwlRune_OlympianStorm>(), 40, 0f, -1, dir.ToRotation(), randSeed);
					}
					break;
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
		if(Main.netMode != NetmodeID.MultiplayerClient)
			foreach (Point item2 in list)
				Projectile.NewProjectile(NPC.GetSource_FromThis(), (float)(item2.X * 16), (float)(item2.Y * 16), 0f, 0f, ModContent.ProjectileType<BiomiteCore_ForbiddenStorm>(), 0, 0f, -1, 0f, 0f);
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

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TerraPrism>(), 1, 1, 4));
    }

	public override void FindFrame(int frameHeight)
	{
		NPC.frameCounter += 1.0;
		if (NPC.frameCounter > 10.0)
		{
			NPC.frameCounter = 0.0;
			if (++NPC.frame.Y > 7)
			{
				NPC.frame.Y = 0;
			}
		}
	}

	public static Vector2 Vector16(int x, int y) => new(x * 16, y * 16);

    public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
		Texture2D coreBack = CoreBack.Value;
		Texture2D core = TextureAssets.Npc[Type].Value;
		Texture2D coreShell = CoreFront.Value;
		Texture2D coreGlow = Glowmask.Value;
		Rectangle frame = coreShell.Frame(1, 4, 0, frameShell);
        Rectangle frame2 = coreGlow.Frame(1, 16, 0, (int)NPC.ai[3] - 1);
        Rectangle frame3 = core.Frame(1, 8, 0, NPC.frame.Y);
        spriteBatch.Draw(coreBack, NPC.Center - screenPos, null, drawColor, 0f, coreBack.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
        spriteBatch.Draw(core, NPC.Center - screenPos, frame3, GlowColor(), 0f, frame3.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
        spriteBatch.Draw(coreShell, NPC.Center - screenPos, frame, drawColor, 0f, frame.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
        spriteBatch.Draw(coreGlow, NPC.Center - screenPos, frame2, Color.White, 0f, frame2.Size() * 0.5f, 1f, SpriteEffects.None, 0f);
        return false;
	}

	public Color GlowColor()
	{
		return (int)NPC.ai[3] switch
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
		};
	}
}
