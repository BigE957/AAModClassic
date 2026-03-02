using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria;
using System;
using System.IO;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader.IO;
using Terraria.GameInput;

namespace AAMod.Items.Dev.Invoker
{
	public class InvokerPlayer : ModPlayer
	{
		public static InvokerPlayer ModPlayer(Player player)
		{
			return player.GetModPlayer<InvokerPlayer>();
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

		public override void SaveData(TagCompound tag)/* tModPorter Suggestion: Edit tag parameter instead of returning new TagCompound */
		{
			List<string> list = new List<string>();
			if (DarkCaligula)
			{
				list.Add("DarkCaligula");
			}
			if (WindRaidjin)
			{
				list.Add("WindRaidjin");
			}
			if (WaterCocytus)
			{
				list.Add("WaterCocytus");
			}
			if (FirePurgatrio)
			{
				list.Add("FirePurgatrio");
			}
			if (EarthMagellanica)
			{
				list.Add("EarthMagellanica");
			}
			if (LightingMechaba)
			{
				list.Add("LightingMechaba");
			}
			if (FinisherElysium)
			{
				list.Add("FinisherElysium");
			}
			TagCompound tagCompound = new TagCompound();
			tagCompound.Add("InvokerSummon", list);
			tagCompound.Add("CaligulaSoul", CaligulaSoul);
			return tagCompound;
		}
		public override void LoadData(TagCompound tag)
		{
			IList<string> list = tag.GetList<string>("InvokerSummon");
			DarkCaligula = list.Contains("DarkCaligula");
			WindRaidjin = list.Contains("WindRaidjin");
			WaterCocytus = list.Contains("WaterCocytus");
			FirePurgatrio = list.Contains("FirePurgatrio");
			EarthMagellanica = list.Contains("EarthMagellanica");
			LightingMechaba = list.Contains("LightingMechaba");
			FinisherElysium = list.Contains("FinisherElysium");
			foreach(int k in tag.GetList<int>("CaligulaSoul"))
			{
				CaligulaSoul.Add(k);
			}
		}
		
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
                    Player.AddBuff(Mod.Find<ModBuff>("InvokedCaligulaSafe").Type, 3600);
                }
            }
		}
		public override void FrameEffects()
		{
			int soulcount = 0;
			foreach(int soul in CaligulaSoul)
			{
				if(soul == Mod.Find<ModNPC>("AkumaA").Type) soulcount ++;
				if(soul == Mod.Find<ModNPC>("YamataA").Type) soulcount ++;
				if(soul == Mod.Find<ModNPC>("ZeroProtocol").Type) soulcount ++;
				if(soul == Mod.Find<ModNPC>("ShenA").Type) soulcount ++;
				if(soul == Mod.Find<ModNPC>("SupremeRajah").Type) soulcount ++;
			}
			if(soulcount >= 5)
			{
				DarkCaligula = true;
			}
			if (Thebookoflaw && DarkCaligula)
			{
				Player.AddBuff(Mod.Find<ModBuff>("InvokedCaligulaSafe").Type, 3600);
			}
			if (InvokerShow)
			{
                Player.legs = EquipLoader.GetEquipSlot(Mod, "InvokerLegs", EquipType.Legs);
                Player.body = EquipLoader.GetEquipSlot(Mod, "InvokerBody", EquipType.Body);
                Player.head = EquipLoader.GetEquipSlot(Mod, "InvokerHead", EquipType.Head);
			}
			if (InvokedCaligula)
			{
                Player.legs = EquipLoader.GetEquipSlot(Mod, "InvokedCaligulaLegs", EquipType.Legs);
                Player.body = EquipLoader.GetEquipSlot(Mod, "InvokedCaligulaBody", EquipType.Body);
                Player.head = EquipLoader.GetEquipSlot(Mod, "InvokedCaligulaHead", EquipType.Head);
				
				if(Main.mouseLeft && Player.inventory[Player.selectedItem].damage > 0)
				{
					InvokedCaligulaClaw ++;
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
						ClawDir = Projectile.NewProjectile(Player.position.X, Player.position.Y, vector20.X, vector20.Y, Mod.Find<ModProjectile>("InvokedCaligulaShoot").Type, (int)((DarkCaligula? 1200 : 600) * (Player.GetDamage(DamageClass.Summon) + Player.GetDamage(DamageClass.Generic) - 1)), 4f, Player.whoAmI, 0f, 0f);
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
				if (Math.Abs(Player.velocity.X) < 0.05 && Math.Abs(Player.velocity.Y) < 0.05 && (Player.itemAnimation == 0 || Player.inventory[Player.selectedItem].type == Mod.Find<ModItem>("InvokerStaff").Type))
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
						if ((Main.rand.Next(30000) < Player.lifeRegenTime || Main.rand.Next(30) == 0))
						{
							int num5 = Dust.NewDust(Player.position, Player.width, Player.height, 55, 0f, 0f, 200, default, 0.5f);
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
								Main.player[myPlayer].AddBuff(43, 20, true);
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

	public class InvokedCaligulaShoot : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("InvokedCaligulaClaw");
			Main.projFrames[Projectile.type] = 28;
		}
		public override void SetDefaults()
		{
			Projectile.width = 68;
			Projectile.height = 64;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.tileCollide = false;
			Projectile.ownerHitCheck = true;
			Projectile.timeLeft = 30;
			Projectile.penetrate = -1;
			Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 12;
		}

		public override Color? GetAlpha(Color lightColor)
        {
            return Color.IndianRed;
        }

		public override void AI()
        {
			Player player = Main.player[Main.myPlayer];
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			float position1 = Main.mouseX + Main.screenPosition.X - vector.X;
			float position2 = Main.mouseY + Main.screenPosition.Y - vector.Y;
			Projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - Projectile.Size / 2f;
			if(player.direction == -1)
			{
				Projectile.rotation = (float)Math.Atan2(position2 * player.direction, position1 * player.direction) - player.fullRotation + MathHelper.ToRadians(180f);
			}
			else
			{
				Projectile.rotation = (float)Math.Atan2(position2 * player.direction, position1 * player.direction) + player.fullRotation;
			}
			int num1 = Projectile.frame + 1;
			Projectile.frame = num1;
			if (num1 >= Main.projFrames[Projectile.type])
			{
				Projectile.frame = 0;
			}
			Projectile.soundDelay--;
			if (Projectile.soundDelay <= 0)
			{
				SoundEngine.PlaySound(SoundID.Item1, Projectile.Center);
				Projectile.soundDelay = 12;
			}
			if(Main.mouseLeft)
			{
				float scaleFactor6 = 15f;
				Vector2 vector20 = Main.MouseWorld - player.RotatedRelativePoint(player.MountedCenter, true);
				vector20.Normalize();
				if (vector20.HasNaNs())
				{
					vector20 = Vector2.UnitX * player.direction;
				}
				vector20 *= scaleFactor6;
				if (vector20.X != Projectile.velocity.X || vector20.Y != Projectile.velocity.Y)
				{
					Projectile.netUpdate = true;
				}
				Projectile.velocity = vector20;
			}
			else
			{
				Projectile.Kill();
			}
			Vector2 vector21 = Projectile.Center + Projectile.velocity * 3f;
			Lighting.AddLight(vector21, 0.8f, 0.8f, 0.8f);
			if (Main.rand.Next(3) == 0)
			{
				int num2 = Dust.NewDust(vector21 - Projectile.Size / 2f, Projectile.width, Projectile.height, 63, Projectile.velocity.X, Projectile.velocity.Y, 100, default, 2f);
				Main.dust[num2].noGravity = true;
				Main.dust[num2].position -= Projectile.velocity;
			}
			player.ChangeDir(Main.projectile[Projectile.whoAmI].direction);

			for(int i=0; i < 200; i++)
			{
				if(Projectile.Hitbox.Intersects(Main.npc[i].Hitbox))
				{
					Main.npc[i].immune[Projectile.owner] = 0;
				}
			}
		}

		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
		{
			
			Player player = Main.player[Main.myPlayer];
			//damage = (int)((player.GetModPlayer<InvokerPlayer>().DarkCaligula? 1000 : 500) * (player.minionDamage + player.allDamage));
			crit = true;
			if(player.GetModPlayer<InvokerPlayer>().DarkCaligula)
			{
                string Lifelength = target.lifeMax + "";
				int regen = Main.rand.Next(2) == 0 ? 2*(Lifelength.Length + 1) : (Lifelength.Length + 1);
				player.statLife += regen;
				player.HealEffect(regen, true);
				if (player.statLife > player.statLifeMax2)
				{
					player.statLife = player.statLifeMax2;
				}
			}
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			target.immune[Projectile.owner] = 5;
		}
	}
}