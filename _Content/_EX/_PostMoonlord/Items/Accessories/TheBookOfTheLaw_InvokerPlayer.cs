using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria;
using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader.IO;
using Terraria.GameInput;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata.Awakened;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened;
using AAModClassic._Content._Dev.__Hardmode.Items.Weapons;
using AAModClassic._Content.Bunny._PostMoonlord.NPCs.__BossRajahRabbitA;
using AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero.Awakened;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.Awakened;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Accessories
{
	public class TheBookOfTheLaw_InvokerPlayer : ModPlayer
	{
		public static TheBookOfTheLaw_InvokerPlayer ModPlayer(Player player)
		{
			return player.GetModPlayer<TheBookOfTheLaw_InvokerPlayer>();
		}
		public float BanishDamage;
		public float BanishDamageMult = 1f;
		public int BanishLimit = 1;

		public override void ResetEffects()
		{
			nohit = true;
			SpringInvoker = false;
			banishing = false;
			InvokedCaligula = false;
			BanishProjClear = false;
			Thebookoflaw = false;
			InvokerMadness = false;
			InvokerShow = false;
			BanishDamage = 0;
			ResetVariables();
		}

		public override void UpdateDead()
		{
			nohit = true;
			SpringInvoker = false;
			banishing = false;
			InvokedCaligula = false;
			BanishProjClear = false;
			Thebookoflaw = false;
			InvokerMadness = false;
			InvokerShow = false;
			BanishDamage = 0;
			ResetVariables();
		}

		private void ResetVariables()
		{
			BanishDamageMult = 1f;
			BanishLimit = 10;
		}
		public override void Initialize()
		{
			nohit = true;
			DarkCaligula = false;
			WindRaidjin = false;
			WaterCocytus = false;
			FirePurgatrio = false;
			EarthMagellanica = false;
			LightingMechaba = false;
			FinisherElysium = false;
			CaligulaSoul = new List<int>();
		}
		
		public override void SaveData(TagCompound tag)// tModPorter Suggestion: Edit tag parameter instead of returning new TagCompound 
		{
            tag.Add("CaligulaSoul", CaligulaSoul);
            tag.Add("DarkCaligula", DarkCaligula);
            tag.Add("WindRaidjin", WindRaidjin);
            tag.Add("WaterCocytus", WaterCocytus);
            tag.Add("FirePurgatrio", FirePurgatrio);
            tag.Add("EarthMagellanica", EarthMagellanica);
            tag.Add("LightingMechaba", LightingMechaba);
            tag.Add("FinisherElysium", FinisherElysium);

        }
		public override void LoadData(TagCompound tag)
		{
            if (!tag.TryGet("CaligulaSoul", out CaligulaSoul))
                CaligulaSoul = new List<int>();
            if (!tag.TryGet("DarkCaligula", out DarkCaligula))
                DarkCaligula = false;
            if (!tag.TryGet("WindRaidjin", out WindRaidjin))
                WindRaidjin = false;
            if (!tag.TryGet("WaterCocytus", out WaterCocytus))
                WaterCocytus = false;
            if (!tag.TryGet("FirePurgatrio", out FirePurgatrio))
                FirePurgatrio = false;
            if (!tag.TryGet("EarthMagellanica", out EarthMagellanica))
                EarthMagellanica = false;
            if (!tag.TryGet("LightingMechaba", out LightingMechaba))
                LightingMechaba = false;
            if (!tag.TryGet("FinisherElysium", out FinisherElysium))
                FinisherElysium = false;
        }
		
		/*
		public override void LoadLegacy(BinaryReader reader)
		{
			int num = reader.ReadInt32();
			if(num == 0)
			{
				BitsByte bitsByte = reader.ReadByte();
				DarkCaligula = bitsByte[0];
				WindRaidjin = bitsByte[1];
				WaterCocytus = bitsByte[2];
				FirePurgatrio = bitsByte[3];
				EarthMagellanica = bitsByte[4];
				LightingMechaba = bitsByte[5];
				FinisherElysium = bitsByte[6];
				return;
			}
		}
		*/
		
		public List<int> CaligulaSoul;
		public bool DarkCaligula;
		public bool WindRaidjin;
		public bool WaterCocytus;
		public bool FirePurgatrio;
		public bool EarthMagellanica;
		public bool LightingMechaba;
		public bool FinisherElysium;
		public bool banishing;
		public bool SpringInvoker;
		public bool InvokerShow;
		public bool InvokedCaligula;
		public bool InvokerMadness;
		public bool Thebookoflaw;
		public bool BanishProjClear;
		public bool nohit;
		private int InvokedCaligulaClaw = 0;
		private int ClawDir = 0;


		public override void UpdateLifeRegen()
		{
			if (InvokedCaligula)
			{
				Player.statLifeMax2 *= 2;
				Player.statDefense *= 2;
			}
		}
		public override void ProcessTriggers(TriggersSet triggersSet)
        {
			if (DarkCaligula && Thebookoflaw && SpringInvoker)
            {
                if (AAMod.AccessoryAbilityKey.JustPressed)
                {
                    Player.AddBuff(ModContent.BuffType<TheBookOfTheLaw_InvokedCaligula>(), 3600);
                }
            }
		}
		public override void FrameEffects()
		{
			bool soulAkuma = false;
			bool soulYamata = false;
			bool soulZero = false;
			bool soulShen = false;
			bool soulRajah = false;
			foreach(int soul in CaligulaSoul)
			{
				if(soul == ModContent.NPCType<AkumaAHead>())
                    soulAkuma = true;
                else if (soul == ModContent.NPCType<YamataABody>())
                    soulYamata = true;
                else if (soul == ModContent.NPCType<ZeroA>())
                    soulZero = true;
				//TODO: shen and rajah dont actually count their deaths when u banish them. related to them spawning a diff entity on death?
                else if (soul == ModContent.NPCType<ShenDoragonA>())
                    soulShen = true;
                else if (soul == ModContent.NPCType<RajahRabbitA>())
                    soulRajah = true;
            }

            if (soulAkuma && soulYamata && soulZero && soulShen && soulRajah)
                DarkCaligula = true;

            Main.NewText("start");
            Main.NewText(soulAkuma);
            Main.NewText(soulYamata);
            Main.NewText(soulZero);
            Main.NewText(soulShen);
            Main.NewText(soulRajah);
            Main.NewText("end");
            Main.NewText(soulAkuma && soulYamata && soulZero && soulShen && soulRajah);
            Main.NewText(DarkCaligula);

            if (Thebookoflaw && DarkCaligula)
                Player.AddBuff(ModContent.BuffType<TheBookOfTheLaw_InvokedCaligula>(), 3600);

            if (InvokerShow)
			{
                Player.head = EquipLoader.GetEquipSlot(Mod, "CerberusHelmet_Head", EquipType.Head);
                Player.body = EquipLoader.GetEquipSlot(Mod, "CerberusChestplate_Body", EquipType.Body);
                Player.legs = EquipLoader.GetEquipSlot(Mod, "CerberusLeggings_Legs", EquipType.Legs);
			}
			if (InvokedCaligula)
			{
                Player.head = EquipLoader.GetEquipSlot(Mod, "InvokedCaligula_Head", EquipType.Head);
                Player.body = EquipLoader.GetEquipSlot(Mod, "InvokedCaligula_Body", EquipType.Body);
                Player.legs = EquipLoader.GetEquipSlot(Mod, "InvokedCaligula_Legs", EquipType.Legs);

                if (Main.mouseLeft && Player.inventory[Player.selectedItem].damage > 0)
				{
					InvokedCaligulaClaw++;
					if(InvokedCaligulaClaw == 1)
					{
						float scaleFactor6 = 15f;
						Vector2 vector20 = Main.MouseWorld - Player.RotatedRelativePoint(Player.MountedCenter, true);
						vector20.Normalize();
						if (vector20.HasNaNs())
						{
							vector20 = Vector2.UnitX * Player.direction;
						}
						vector20 *= scaleFactor6;
						ClawDir = Projectile.NewProjectile(Player.GetSource_FromThis(), Player.position.X, Player.position.Y, vector20.X, vector20.Y, ModContent.ProjectileType<TheBookOfTheLaw_ClawSlash>(), (int)((DarkCaligula? 1200 : 600) * (Player.GetDamage(DamageClass.Summon).Flat + Player.GetDamage(DamageClass.Generic).Flat - 1)), 4f, Player.whoAmI, 0f, 0f);
					}
					else if(InvokedCaligulaClaw > 30)
					{
						Player.ChangeDir(Main.projectile[ClawDir].direction);
						InvokedCaligulaClaw = 0;
					}
				}
				
				else
				{
					InvokedCaligulaClaw = 0;
				}
				
			}
			if (SpringInvoker)
			{
				if (Math.Abs(Player.velocity.X) < 0.05 && Math.Abs(Player.velocity.Y) < 0.05 && (Player.itemAnimation == 0 || Player.inventory[Player.selectedItem].type == ModContent.ItemType<AleisterStaff>()))
				{
					if(Player.lifeRegen < 0) Player.lifeRegen /= 2;
					if (Player.lifeRegenTime > 90 && Player.lifeRegenTime < 1800)
					{
						Player.lifeRegenTime = 1800;
					}
					Player.lifeRegenTime += 4;
					Player.lifeRegen += 4;
					float Shine = Player.lifeRegenTime - 3000;
					Shine /= 300f;
					if (Shine > 0f)
					{
						if (Shine > 30f)
						{
							Shine = 30f;
						}
					}
					Player.lifeRegen += (int)Math.Round(Shine);
					if (Player.lifeRegen > 0 && Player.statLife < Player.statLifeMax2)
					{
						Player.lifeRegenCount++;
						if (Main.rand.Next(30000) < Player.lifeRegenTime || Main.rand.NextBool(30))
						{
							int num5 = Dust.NewDust(Player.position, Player.width, Player.height, DustID.Pixie, 0f, 0f, 200, default, 0.5f);
							Main.dust[num5].noGravity = true;
							Main.dust[num5].velocity *= 0.75f;
							Main.dust[num5].fadeIn = 1.3f;
							Vector2 vector = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
							vector.Normalize();
							vector *= Main.rand.Next(50, 100) * 0.04f;
							Main.dust[num5].velocity = vector;
							vector.Normalize();
							vector *= 34f;
							Main.dust[num5].position = Player.Center - vector;
						}
					}
				}
				
				if(Player.statLife <= Player.statLifeMax2 * 0.5)
				{
					Player.iceBarrier= true;
				}

				if (Player.statLife > Player.statLifeMax2 * 0.25f)
				{
					Player.hasPaladinShield = true;
					if (Player.whoAmI != Main.myPlayer && Player.miscCounter % 10 == 0)
					{
						int myPlayer = Main.myPlayer;
						if (Main.player[myPlayer].team == Player.team && Player.team != 0)
						{
							float num = Player.position.X - Main.player[myPlayer].position.X;
							float num2 = Player.position.Y - Main.player[myPlayer].position.Y;
							float num3 = (float)Math.Sqrt(num * num + num2 * num2);
							if (num3 < 800f)
							{
								Main.player[myPlayer].AddBuff(BuffID.PaladinsShield, 20, true);
							}
						}
					}
				}
			}
		}

		public override void OnHurt(Player.HurtInfo info)
		{
			nohit = false;
		}
		
	}
}