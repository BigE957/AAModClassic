using AAModClassic._Content._EX._PostMoonlord.Items.Accessories;
using AAModClassic._Content.Bunny._PostMoonlord.NPCs.__BossRajahRabbitA;
using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossShenDoragon.Awakened;
using AAModClassic._Content.Inferno._PostMoonlord.NPCs.__BossAkuma.Awakened;
using AAModClassic._Content.Mire._PostMoonlord.NPCs.__BossYamata.Awakened;
using AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero.Awakened;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.Weapons;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace AAModClassic._Content._Dev.__Hardmode.Items.Weapons
{
	public class AleisterStaff : BanishDamageItemAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Summon";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Aleister Staff");
			// Tooltip.SetDefault("");

            Item.staff[Item.type] = true;

        }

		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
            string text = "";
			Player player = Main.LocalPlayer;
			if(!player.GetModPlayer<TheBookOfTheLaw_InvokerPlayer>().Thebookoflaw)
			{
				text += Language.GetTextValue("Mods.AAModClassic.Common.InvokerStaff1");
			}
			else
			{
				text += Language.GetTextValue("Mods.AAModClassic.Common.InvokerStaff2");
			}
			foreach (TooltipLine tooltipLine in tooltips)
			{
				if (tooltipLine != null && tooltipLine.Name == "Damage")
				{
					string[] splitText = tooltipLine.Text.Split(' ');
					string damageValue = splitText.First();
					string damageWord = splitText.Last();
					if(Main.LocalPlayer.GetModPlayer<TheBookOfTheLaw_InvokerPlayer>().Thebookoflaw) 
					{
						tooltipLine.Text = damageValue + " " + Language.GetTextValue("Mods.AAModClassic.Common.InvokerDamage1") + damageWord;
					}
				}
				if (tooltipLine != null && tooltipLine.Name == "Tooltip0")
				{
					tooltipLine.Text = text;
				}
			}
		}

        public override void SafeSetDefaults()
        {
			Item.scale = 0.65f;
			Item.width = 41;
			Item.height = 41;
			Item.rare = ItemRarityID.Purple;
			Item.damage = 200;
			Item.noMelee = true;
			Item.autoReuse = true;
			Item.reuseDelay = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useTime = 16;
			Item.useAnimation = 16;
			Item.shoot = ModContent.ProjectileType<AleisterStaff_Proj>(); 
			Item.shootSpeed = 40f;
			Item.value = Item.buyPrice(10, 36, 0, 0);
        }
		public override bool CanUseItem(Player player)
		{
			if(!player.GetModPlayer<TheBookOfTheLaw_InvokerPlayer>().Thebookoflaw)
			{
				Item.noMelee = false;
				Item.staff[Item.type] = false;
				Item.useStyle = ItemUseStyleID.Swing;
				Item.damage = (int)(player.GetDamage(DamageClass.Summon)).ApplyTo(200);
				Item.DamageType = DamageClass.Summon;
				return true;
			}
			else if(player.GetModPlayer<TheBookOfTheLaw_InvokerPlayer>().Thebookoflaw)
			{
				Item.noMelee = true;
				Item.staff[Item.type] = true;
				Item.useStyle = ItemUseStyleID.Shoot;
				return true;
			}
			return true;
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			if (player.altFunctionUse != 2 && player.GetModPlayer<TheBookOfTheLaw_InvokerPlayer>().Thebookoflaw)
			{
				Projectile.NewProjectile(Item.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y, ModContent.ProjectileType<AleisterStaff_Proj>(), damage, knockback, player.whoAmI, 0f, 0f);
			}
			if (player.altFunctionUse == 2 && player.GetModPlayer<TheBookOfTheLaw_InvokerPlayer>().SpringInvoker)
			{
				if(!player.GetModPlayer<TheBookOfTheLaw_InvokerPlayer>().InvokerMadness)
				{
					player.AddBuff(ModContent.BuffType<AleisterStaff_InvokerOfMadness>(), player.GetModPlayer<TheBookOfTheLaw_InvokerPlayer>().DarkCaligula? 30:3000);
					player.GetModPlayer<TheBookOfTheLaw_InvokerPlayer>().BanishDamage = Item.damage * 5;
					player.GetModPlayer<TheBookOfTheLaw_InvokerPlayer>().banishing = true;
				}
			}
			return false;
		}

        public override Vector2? HoldoutOrigin()
		{
			return new Vector2(38, 42);
		}

		public override bool AltFunctionUse(Player player)
		{
			return !(!player.GetModPlayer<TheBookOfTheLaw_InvokerPlayer>().DarkCaligula && player.GetModPlayer<TheBookOfTheLaw_InvokerPlayer>().InvokedCaligula) && player.GetModPlayer<TheBookOfTheLaw_InvokerPlayer>().SpringInvoker;
		}

    }

	public class AleisterStaffGlobalNPC : GlobalNPC
	{
		public override bool InstancePerEntity
		{
			get
			{
				return true;
			}
		}
		public bool Banished;
		public bool IsBeingBanished = false;
		public int BanishCount = 0;
		public bool CaligulaSoulFight = false;

		public override void ResetEffects(NPC npc)
		{
			Banished = false;
		}

		public void BanishAction(NPC npc)
		{
			npc.velocity.X = 0;
			npc.velocity.Y = 0;
			npc.scale -= 0.01f;
			npc.alpha += 4;

			if(BanishCount > 70 || npc.alpha >= 250 || npc.scale < 0.05f)
			{
				Projectile.NewProjectile(Projectile.GetSource_None(), npc.Center.X, npc.Center.Y, 0f, 0f, ModContent.ProjectileType<AleisterStaff_InvokedHeal>(), 0, 0f, Main.LocalPlayer.whoAmI, Main.LocalPlayer.whoAmI, npc.lifeMax * 0.01f);
				
				if(npc.type == NPCID.MoonLordHead || npc.type == NPCID.MoonLordHand)
				{
					for(int i = 0; i < 200 ; i++)
					{
						if(Main.npc[i].type == NPCID.MoonLordCore || Main.npc[i].type == NPCID.MoonLordHead || Main.npc[i].type == NPCID.MoonLordHand)
						{
							Main.npc[i].active = false;
							Main.npc[i].NPCLoot();
						}
					}
				}

				
				if(npc.realLife >= 0) 
				{
					if(npc.type == NPCID.EaterofWorldsHead) Main.npc[npc.realLife].boss = true;
					Main.npc[npc.realLife].NPCLoot();//This need change in AAMod
					for(int i = 0; i < 200 ; i++)
					{
						if(Main.npc[i].realLife == npc.realLife)
						{
							Main.npc[i].NPCLoot();
							Main.npc[i].active = false;
						}
					}
					NPCLoader.CheckDead(Main.npc[npc.realLife]);
					Main.npc[npc.realLife].checkDead();
					Main.npc[npc.realLife].netUpdate = true;
				}
				npc.NPCLoot();//This need change in AAMod
				NPCLoader.CheckDead(npc);
				npc.checkDead();
				npc.active = false;
				npc.life = 0;
				npc.netUpdate = true;
				BanishCount = 0;
			}
		}
		public override void UpdateLifeRegen(NPC npc, ref int damage) 
		{
			int InvokedCount = 0;
			foreach(Projectile p in Main.ActiveProjectiles)
			{ 
				int num9 = (int)p.ai[1];
				if (p.type == ModContent.ProjectileType<AleisterStaff_Proj>() && p.ai[0] == 1f && npc == Main.npc[num9]) 
				{
					InvokedCount++;
					npc.lifeRegen -= 10 * InvokedCount;
				}
			}

			TheBookOfTheLaw_InvokerPlayer InvokerPlayer = Main.LocalPlayer.GetModPlayer<TheBookOfTheLaw_InvokerPlayer>();

			if(npc.boss)
			{
				if(!InvokerPlayer.nohit)
				{
					InvokerPlayer.nohit = false;
				}
				bool flag = (Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem].type == ModContent.ItemType<AleisterStaff>() || Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem].type == ItemID.RodofDiscord) && Main.LocalPlayer.GetModPlayer<TheBookOfTheLaw_InvokerPlayer>().SpringInvoker && Main.LocalPlayer.GetModPlayer<TheBookOfTheLaw_InvokerPlayer>().Thebookoflaw;
				if(npc.life/npc.lifeMax > 0.95)
				{
					CaligulaSoulFight = true;
				}
				else if(InvokerPlayer.CaligulaSoul.Contains(npc.type))
				{
					CaligulaSoulFight = false;
				}
				else if(!flag)
				{
					CaligulaSoulFight = false;
				}
				else if(!InvokerPlayer.nohit)
				{
					CaligulaSoulFight = false;
				}
			}


			if(!npc.townNPC && (npc.life < InvokerPlayer.BanishDamage * InvokerPlayer.BanishDamageMult * InvokedCount) && InvokerPlayer.banishing && (npc.active || npc.life > 0))
			{
				npc.GetGlobalNPC<AleisterStaffGlobalNPC>().IsBeingBanished = true;
			}
			if((IsBeingBanished && !npc.townNPC && (npc.active || npc.life > 0)) || (!npc.townNPC && (npc.life < InvokerPlayer.BanishDamage) && InvokerPlayer.banishing && (npc.active || npc.life > 0)))
			{
				IsBeingBanished = true;
				BanishCount ++;
				if(BanishCount == 1)
				{
					Projectile.NewProjectile(Projectile.GetSource_None(), npc.Center.X, npc.Center.Y, 0f, 0f, ModContent.ProjectileType<AleisterStaff_InvokedRune>(), 0, 0f, Main.LocalPlayer.whoAmI, 1f, npc.whoAmI);
					
					if(npc.type == NPCID.MoonLordHead || npc.type == NPCID.MoonLordHand)
					{
						for(int i = 0; i < 200 ; i++)
						{
							if(Main.npc[i].type == NPCID.MoonLordCore || Main.npc[i].type == NPCID.MoonLordHead || Main.npc[i].type == NPCID.MoonLordHand)
							{
								Projectile.NewProjectile(Projectile.GetSource_None(), npc.Center.X, npc.Center.Y, 0f, 0f, ModContent.ProjectileType<AleisterStaff_InvokedRune>(), 0, 0f, Main.LocalPlayer.whoAmI, 1f, npc.whoAmI);
							}
						}
					}
				}
				BanishAction(npc);
			}
			return;
		}
		
		public override bool PreKill(NPC npc)
		{
			if(Main.LocalPlayer.inventory[Main.LocalPlayer.selectedItem].type == ModContent.ItemType<AleisterStaff>() && Main.LocalPlayer.GetModPlayer<TheBookOfTheLaw_InvokerPlayer>().SpringInvoker && Main.LocalPlayer.GetModPlayer<TheBookOfTheLaw_InvokerPlayer>().Thebookoflaw)
			{
            	//Main.player[Main.myPlayer].GetModPlayer<InvokerPlayer>().BanishProjClear = true; // Just for test.
				float nump7 = 4f;
				float nump8 = Main.rand.Next(-100, 101);
				float nump9 = Main.rand.Next(-100, 101);
				float nump10 = (float)Math.Sqrt(nump8 * nump8 + nump9 * nump9);
				nump10 = nump7 / nump10;
				nump8 *= nump10;
				nump9 *= nump10;
				int[] array = new int[200];
				int num3 = 0;
				int num4 = 0;
				for (int i = 0; i < 200; i++)
				{
					if (Main.npc[i].CanBeChasedBy(this, false))
					{
						float num5 = Math.Abs(Main.npc[i].position.X + Main.npc[i].width / 2 - npc.position.X + npc.width / 2) + Math.Abs(Main.npc[i].position.Y + Main.npc[i].height / 2 - npc.position.Y + npc.height / 2);
						if (num5 < 800f)
						{
							if (Collision.CanHit(npc.position, 1, 1, Main.npc[i].position, Main.npc[i].width, Main.npc[i].height) && num5 > 50f)
							{
								array[num4] = i;
								num4++;
							}
							else if (num4 == 0)
							{
								array[num3] = i;
								num3++;
							}
						}
					}
				}
				if (num3 == 0 && num4 == 0)
				{
					return true;
				}
				int num6;
				if (num4 > 0)
				{
					num6 = array[Main.rand.Next(num4)];
				}
				else
				{
					num6 = array[Main.rand.Next(num3)];
				}
				if(npc.lifeMax >= 1000) Projectile.NewProjectile(Projectile.GetSource_None(), npc.Center.X, npc.Center.Y, 0f, 0f, ModContent.ProjectileType<AleisterStaff_InvokedHeal>(), 0, 0f, Main.LocalPlayer.whoAmI, Main.LocalPlayer.whoAmI, (npc.life > npc.lifeMax? npc.life : npc.lifeMax) * 0.001f);
				if(npc.damage != 0) 
				{
					if((npc.realLife >= 0 && npc.realLife == npc.whoAmI) || npc.realLife < 0) 
						Projectile.NewProjectile(Projectile.GetSource_None(), npc.Center.X, npc.Center.Y, nump8, nump9, ModContent.ProjectileType<AleisterStaff_InvokedDamage>(), npc.damage * 20, 0f, Main.LocalPlayer.whoAmI, num6, 0f);
				}
				if(npc.GetGlobalNPC<AleisterStaffGlobalNPC>().CaligulaSoulFight && !Main.LocalPlayer.GetModPlayer<TheBookOfTheLaw_InvokerPlayer>().DarkCaligula && (npc.type == ModContent.NPCType<ZeroA>() || npc.type == ModContent.NPCType<YamataABody>() || npc.type == ModContent.NPCType<AkumaAHead>() || npc.type == ModContent.NPCType<ShenDoragonA>() || npc.type == ModContent.NPCType<RajahRabbitA>()))
				{
					Projectile.NewProjectile(Projectile.GetSource_None(), npc.Center.X, npc.Center.Y, nump8, nump9, ModContent.ProjectileType<AleisterStaff_InvokedDamage>(), 0, 0f, Main.LocalPlayer.whoAmI, Main.LocalPlayer.whoAmI, npc.type);
				}
			}
			return true;
		}
		
	}
}