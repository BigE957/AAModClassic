using System;
using System.IO;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic._Unreleased.Content.Void.Dusts;
using AAModClassic.Items.Potions;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.Items.InfinityZero;
using AAModClassic.Items.Boss;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.Items.InfinityZero.BossStandard;
using ReLogic.Content;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.NPCs.InfinityZero
{
	[AutoloadBossHead]
	public class InfinityZero : ModNPC
	{
        public NPC Zero1;
        public NPC Zero2;
        public NPC Zero3;
        public NPC Zero4;
        public NPC Zero5;
        public NPC Zero6;
        public NPC Core;
        public bool ZerosSpawned = false;
        public bool Reseting = false;
        public Vector2 topVisualOffset = default;

        public static Asset<Texture2D> glowTex;


        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Infinity Zero; Mechanical Malice");
			Main.npcFrameCount[NPC.type] = 4;

            glowTex = ModContent.Request<Texture2D>(Texture + "_Glow");
        }
		public override void SetDefaults()
		{
			NPC.damage = 0;
            NPC.width = 420; 			
            NPC.height = 342;
            NPC.npcSlots = 100;
            NPC.scale = 1f;
            NPC.dontTakeDamage = true;
			NPC.lifeMax = 2500000;
			NPC.knockBackResist = 0f;
			NPC.aiStyle = -1;
			NPC.value = Item.buyPrice(30, 0, 0, 0);
			NPC.boss = true;
			for (int k = 0; k < NPC.buffImmune.Length; k++)
			{
				NPC.buffImmune[k] = true;
			}
			NPC.noGravity = true;
			NPC.noTileCollide = true;
			NPC.netAlways = true;
			NPC.chaseable = true;
			Music = Mod.GetSoundSlot(SoundType.Music, "_Unreleased/Sounds/Music/IZ");
			NPC.HitSound = SoundID.NPCHit44;
			NPC.DeathSound = Mod.GetLegacySoundSlot(SoundType.Sound, "_Unreleased/Sounds/Sounds/IZRoar");
            NPC.scale *= 1.4f;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance);
            NPC.damage = (int)(NPC.damage * 1.1f); 
        }

        public float[] customAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write((short)customAI[0]);
                writer.Write((short)customAI[1]);
                writer.Write((short)customAI[2]);
                writer.Write((short)customAI[3]);				
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {				
                customAI[0] = reader.ReadFloat();
                customAI[1] = reader.ReadFloat();
                customAI[2] = reader.ReadFloat();
                customAI[3] = reader.ReadFloat();				
            }
        }
        public int roarTimer = 200;
		public bool[] roared = new bool[3];
        private int testime = 60;
        private int StormTimer = 0;
        public override void AI()
		{
            NPC.timeLeft = 200;
            if (testime > 0)
            {
                testime--;
            }

            StormTimer++;
            if (StormTimer >= 750)
            {
                StormTimer = 0;
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, NPC.velocity.X * 2f, NPC.velocity.Y * 2f, ModContent.ProjectileType<InfinityZero_InfinityStorm>(), NPC.damage, 0, Main.myPlayer);
            }

            if (Main.netMode != NetmodeID.Server)
			{
				int ThreeQuartersHealth = NPC.lifeMax * (int).75f;
				int HalfHealth = NPC.lifeMax * (int).5f;
				int QuarterHealth = NPC.lifeMax * (int).25f;
				
				if(roarTimer > -1) roarTimer--;
				if (NPC.life <= ThreeQuartersHealth && !roared[0])
				{
					roared[0] = true;
					roarTimer = 200;
				}
				if (NPC.life <= HalfHealth && !roared[1])
				{
					roared[1] = true;
					roarTimer = 200;
				}
				if (NPC.life <= QuarterHealth && !roared[2])
				{
					roared[2] = true;
					roarTimer = 200;
				}
				if (roarTimer == 180)
				{
					SoundEngine.PlaySound(Mod.GetLegacySoundSlot(SoundType.Sound, "_Unreleased/Sounds/Sounds/IZRoar"), NPC.Center);
				}
			}

            Player player = Main.player[NPC.target];
            if (player != null)
            {
                float dist = NPC.Distance(player.Center);
                if (dist > 1200) //trigger teleporting stuff
                {
                    NPC.dontTakeDamage = true;
                    NPC.alpha += 10;
                    if (NPC.alpha >= 255) //teleport, you're invisible!
                    {
                        NPC.alpha = 254; //don't let it hit 255 or it will despawn!
                        Vector2 tele = new Vector2(player.Center.X, player.Center.Y);
                        NPC.Center = tele;
                        NPC.dontTakeDamage = false;
                        SoundEngine.PlaySound(Mod.GetLegacySoundSlot(SoundType.Sound, "_Unreleased/Sounds/Sounds/IZRoar"), NPC.Center);
                    }
                }
                else //you're close to the player, so make sure you're visible!
                {
                    NPC.dontTakeDamage = false; //to prevent you from being indestructible if the teleport is interrupted
                    NPC.alpha -= 25;
                    if (NPC.alpha <= 0)
                    {
                        NPC.alpha = 0;
                    }
                }
            }

            float movementMax = 1.5f;
			if(NPC.target > -1)
			{
				Player targetPlayer = Main.player[NPC.target];
				if(!targetPlayer.dead) //speed changes depending on how far the player is
				{
                    NPC.alpha -= 10;
                    if (NPC.alpha <= 0)
                    {
                        NPC.alpha = 0;
                    }
                    movementMax = MathHelper.Lerp(1f, 4f, Math.Min(1f, Math.Max(0f, Vector2.Distance(NPC.Center, targetPlayer.Center) / 1000f)));
				}
                if (targetPlayer.dead) //speed changes depending on how far the player is
                {
                    NPC.alpha += 10;
                    if (NPC.alpha >= 255)
                    {
                        NPC.active = false;
                    }
                }
            }
			//customAI is used here because the original ai and localAI are both used elsewhere. It is synced above.
            BaseAI.AIElemental(NPC, ref customAI, false, 0, false, false, 800f, 600f, 60, movementMax);
            if (!ZerosSpawned)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    int latestNPC = NPC.whoAmI;
					int handType = 0;
                    latestNPC = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y - 100, ModContent.NPCType<InfinityZeroHand1>(), 0, NPC.whoAmI);
                    Main.npc[latestNPC].ai[0] = NPC.whoAmI;
					Main.npc[latestNPC].ai[1] = handType;
					handType++;
                    Zero1 = Main.npc[latestNPC];
                    latestNPC = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y - 100, ModContent.NPCType<InfinityZeroHand1>(), 0, NPC.whoAmI);
                    Main.npc[latestNPC].ai[0] = NPC.whoAmI;
					Main.npc[latestNPC].ai[1] = handType;
					handType++;
                    Zero2 = Main.npc[latestNPC];
                    latestNPC = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y - 100, ModContent.NPCType<InfinityZeroHand1>(), 0, NPC.whoAmI);
                    Main.npc[latestNPC].ai[0] = NPC.whoAmI;
					Main.npc[latestNPC].ai[1] = handType;
					handType++;
                    Zero3 = Main.npc[latestNPC];
                    latestNPC = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y - 100, ModContent.NPCType<InfinityZeroHand2>(), 0, NPC.whoAmI);
                    Main.npc[latestNPC].ai[0] = NPC.whoAmI;
					Main.npc[latestNPC].ai[1] = handType;
					handType++;
                    Zero4 = Main.npc[latestNPC];
                    latestNPC = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y - 100, ModContent.NPCType<InfinityZeroHand2>(), 0, NPC.whoAmI);
                    Main.npc[latestNPC].ai[0] = NPC.whoAmI;
					Main.npc[latestNPC].ai[1] = handType;
					handType++;
                    Zero5 = Main.npc[latestNPC];
                    latestNPC = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y - 100, ModContent.NPCType<InfinityZeroHand2>(), 0, NPC.whoAmI);
                    Main.npc[latestNPC].ai[0] = NPC.whoAmI;
					Main.npc[latestNPC].ai[1] = handType;
                    Zero6 = Main.npc[latestNPC];
                    latestNPC = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y - 100, ModContent.NPCType<InfinityCore>(), 0, NPC.whoAmI);
                    Main.npc[latestNPC].realLife = NPC.whoAmI;
                    Main.npc[latestNPC].ai[0] = NPC.whoAmI;
                    Core = Main.npc[latestNPC];
                }
                ZerosSpawned = true;
            }
            if (testime == 0 && (Zero1 == null || Zero2 == null || Zero3 == null || Zero4 == null || Zero5 == null || Zero6 == null || !Zero1.active || !Zero2.active || !Zero3.active || !Zero4.active || !Zero5.active || !Zero6.active))
            {
                Reseting = true;
                testime = 60;
            }
            if ((Zero1 == null || !Zero1.active) && (Zero2 == null || !Zero2.active) && (Zero3 == null || !Zero3.active) && (Zero4 == null || !Zero4.active) && (Zero5 == null || !Zero5.active) && (Zero6 == null || !Zero6.active))
            {
                ZerosSpawned = false;
            }
            for (int m = NPC.oldPos.Length - 1; m > 0; m--)
            {
                NPC.oldPos[m] = NPC.oldPos[m - 1];
            }
            NPC.oldPos[0] = NPC.position;		
        }

        public bool Dead = false;

        public override void OnKill()
		{
            Dead = true;
            NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<Oblivion>(), 0, 0);
            AAPlayer.ZeroKills += 1;
            AAWorld_Unreleased.downedIZ = true;
            if (Main.expertMode)
            {
                NPC.DropLoot(ModContent.ItemType<InfinityZeroTreasureBag>());
            }
            else
            {
                NPC.DropLoot(ModContent.ItemType<Infinitium>(), 25, 35);
                string[] lootTable =
                {
                    "Genocide",
                    "Nova",
                    "Sagittarius",
                    "TotalDestruction",
                    "Annihilator",
                    "InfinityBlade"
                };
                int loot = Main.rand.Next(lootTable.Length);
                NPC.DropLoot(Mod.Find<ModItem>(lootTable[loot]).Type);
                NPC.DropLoot(ModContent.ItemType<EXSoul>());
            }
        }
        

        public override void FindFrame(int frameHeight)
        {
            if (roarTimer > -1)
            {
                NPC.frame.Y = 2 * frameHeight;
            } else
            {
                NPC.frame.Y = 0;
            }
        }

        public override void BossLoot(ref int potionType)
		{
			potionType = ModContent.ItemType<GrandHealingPotion>();
        }
		
		private void ModifyHit(ref int damage)
		{
            damage = (int)(damage * 0.6f);
            if (damage >= 800)
            {
                damage = 800;
            }
        }
		
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 3f;
			return null;
		}
		
		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}
		

        public bool quarterHealth = false;
        public bool threeQuarterHealth = false;
        public bool HalfHealth = false;
        public bool fifthHealth = false;
        public bool OpenCore = false;
        public bool FirstCoreLine = false;
        public int CoreTimer = 600;

        public override void HitEffect(NPC.HitInfo hit)
		{
            if (NPC.life <= NPC.lifeMax / 4 * 3 && threeQuarterHealth == false)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat("WARNING. Systems have reached 75% efficiency.", new Color(158, 3, 32));
                threeQuarterHealth = true;
                roarTimer = 200;
            }
            if (NPC.life <= NPC.lifeMax / 2 && HalfHealth == false)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat("Redirecting resources to offensive systems.", new Color(158, 3, 32));
                HalfHealth = true;
                NPC.defense = 225;
                InfinityZeroHand1.damageIdle = 250;
                InfinityZeroHand1.damageCharging = 350;
                roarTimer = 200;
            }
            if (NPC.life <= NPC.lifeMax / 4 && quarterHealth == false)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat("CRITICAL WARNING. Systems have reached 25% efficiency. Failure imminent.", new Color(158, 3, 32));
                quarterHealth = true;
                roarTimer = 200;
            }
            if (NPC.life <= NPC.lifeMax / 6 && !fifthHealth)
            {
                fifthHealth = true;
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat("Terrarian, you will not win this. Rerouting all resources to offensive systems.", new Color(158, 3, 32));
                NPC.defense = 175;
                InfinityZeroHand1.damageIdle = 350;
                InfinityZeroHand1.damageCharging = 500;
                roarTimer = 200;
            }
            if (NPC.ai[3] == 6)
            {
                CoreTimer--;
                OpenCore = true;
                if (Main.netMode != NetmodeID.MultiplayerClient && !FirstCoreLine)
                {
                    FirstCoreLine = true;
                    BaseUtility.Chat("Zero Units in critical condition. Rerouting resources to repair systems. Core defense temporarily disabled.", new Color(158, 3, 32));
                }
                if (CoreTimer <= 0)
                {
                    BaseUtility.Chat("Zero Units sufficiently repaired. Reengaging Core defense system.", new Color(158, 3, 32));
                    NPC.ai[3] = 0;
                    OpenCore = false;
                    CoreTimer = 600;
                    InfinityZeroHand1.RepairMode = false;
                    InfinityZeroHand1.RepairMode = false;
                }

            }
            if (NPC.life <= NPC.lifeMax / 6)
            {
                Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/LastStand");
            }
			if (NPC.life <= 0)
			{
				float randomSpread = Main.rand.Next(-50, 50) / 100;

				Gore.NewGore(NPC.GetSource_FromAI(), NPC.Center, NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("IZGore1").Type, 1.4f);
				Gore.NewGore(NPC.GetSource_FromAI(), NPC.Center, NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("IZGore2").Type, 1.4f);
				Gore.NewGore(NPC.GetSource_FromAI(), NPC.Center, NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("IZGore3").Type, 1.4f);
				Gore.NewGore(NPC.GetSource_FromAI(), NPC.Center, NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("IZGore4").Type, 1.4f);
				Gore.NewGore(NPC.GetSource_FromAI(), NPC.Center, NPC.velocity * randomSpread * Main.rand.NextFloat(), Mod.Find<ModGore>("IZGore5").Type, 1.4f);
				
                NPC.position.X = NPC.position.X + NPC.width / 2;
				NPC.position.Y = NPC.position.Y + NPC.height / 2;
				NPC.width = 400;
				NPC.height = 350;
				NPC.position.X = NPC.position.X - NPC.width / 2;
				NPC.position.Y = NPC.position.Y - NPC.height / 2;
				for (int num621 = 0; num621 < 60; num621++)
				{
					int num622 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.VoidDust_Unreleased>(), 0f, 0f, 100, default, 2f);
					Main.dust[num622].velocity *= 3f;
					if (Main.rand.Next(2) == 0)
					{
						Main.dust[num622].scale = 0.5f;
						Main.dust[num622].fadeIn = 1f + Main.rand.Next(10) * 0.1f;
					}
				}
				for (int num623 = 0; num623 < 90; num623++)
				{
					int num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<Dusts.VoidDust_Unreleased>(), 0f, 0f, 100, default, 3f);
					Main.dust[num624].noGravity = true;
					Main.dust[num624].velocity *= 5f;
					num624 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.CopperCoin, 0f, 0f, 100, default, 2f);
					Main.dust[num624].velocity *= 2f;
				}
			}
		}

        public void DrawCore(SpriteBatch spriteBatch, string coreTex, NPC core, Color drawColor, bool DrawUnder)
        {
            if (core != null && core.active)
            {
                BaseDrawing.DrawTexture(spriteBatch, Mod.GetTexture(coreTex), 0, NPC.Center, core.width, core.height, core.scale, core.rotation, core.spriteDirection, Main.npcFrameCount[core.type], core.frame, drawColor, false);
            }
        }

        public static Color infinityGlowRed = new Color(233, 53, 53);
        public static Color GetGlowAlpha(bool aura)
        {
            return (aura ? infinityGlowRed : Color.White) * (Main.mouseTextColor / 255f);
        }

        public Color GetRedAlpha()
        {
            return new Color(233, 53, 53) * (Main.mouseTextColor / 255f);
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;
        public bool saythelinezero = false;
		
		public Vector2 GetConnectionPoint(int handType)
		{
			float offsetX = 0, offsetY = 0;
			switch(handType)
			{
				case 0: offsetX = -62; offsetY = -80; break;
				case 1: offsetX = -32; offsetY = -44; break;
				case 2: offsetX = -46; offsetY = -20; break;
				case 3: offsetX = 62; offsetY = -80; break;
				case 4: offsetX = 32; offsetY = -44; break;
				case 5: offsetX = 46; offsetY = -20; break;		
				default: break;
			}
			offsetX *= 2f;
			offsetY *= 2f;
			return new Vector2(offsetX, offsetY);
		}		

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (auraDirection) 
            { 
                auraPercent += 0.1f; 
                auraDirection = auraPercent < 1f; 
            }
            else 
            { 
                auraPercent -= 0.1f; 
                auraDirection = auraPercent <= 0f; 
            }

            DrawCore(spriteBatch, "_Unreleased/NPCs/Bosses/Infinity/InfinityCore", Core, AAColor.Oblivion, false);

            if (fifthHealth)
            {
                Main.NewText("bluha");
                BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, drawColor);
                BaseDrawing.DrawAura(spriteBatch, glowTex.Value, 0, NPC, auraPercent, 1f, 0f, 0f, GetRedAlpha());
                BaseDrawing.DrawTexture(spriteBatch, glowTex.Value, 0, NPC, GetRedAlpha());
            }
            else
            {
                BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, BaseUtility.ColorClamp(BaseDrawing.GetNPCColor(NPC, NPC.Center + new Vector2(0, -30), true, 0f), GetGlowAlpha(true)));
                BaseDrawing.DrawAura(spriteBatch, glowTex.Value, 0, NPC, auraPercent, 1f, 0f, 0f, GetGlowAlpha(true));
                BaseDrawing.DrawTexture(spriteBatch, glowTex.Value, 0, NPC, GetGlowAlpha(false));
            }


            string ZeroTex = "_Unreleased/NPCs/Bosses/Infinity/InfinityZeroHand1";

            //bottom arms
            DrawZero(spriteBatch, ZeroTex, ZeroTex + "_Glow", Zero6, drawColor);
	        DrawZero(spriteBatch, ZeroTex, ZeroTex + "_Glow", Zero3, drawColor);	
            //middle arms
	        DrawZero(spriteBatch, ZeroTex, ZeroTex + "_Glow", Zero5, drawColor);		
            DrawZero(spriteBatch, ZeroTex, ZeroTex + "_Glow", Zero2, drawColor);
			//top arms
			DrawZero(spriteBatch, ZeroTex, ZeroTex + "_Glow", Zero4, drawColor);		
			DrawZero(spriteBatch, ZeroTex, ZeroTex + "_Glow", Zero1, drawColor);			

		
            return false;
        }

        public void DrawZero(SpriteBatch spriteBatch, string zeroTexture, string glowMaskTexture, NPC Zero, Color drawColor)
        {
            if (Zero != null && Zero.active && Zero.ModNPC != null && (Zero.ModNPC is InfinityZeroHand1 || Zero.ModNPC is InfinityZeroHand2))
            {
				InfinityZeroHand1 handNPC = (InfinityZeroHand1)Zero.ModNPC;
                string ArmTex = "_Unreleased/NPCs/Bosses/Infinity/IZArm";
                Texture2D ArmTex2D = Mod.GetTexture(ArmTex);
				Texture2D zeroTex = Mod.GetTexture(zeroTexture);
                Texture2D glowTex = Mod.GetTexture(glowMaskTexture);				
                Vector2 ArmOrigin = new Vector2(NPC.Center.X, NPC.Center.Y) + GetConnectionPoint(handNPC.handType);
                Vector2 connector = Zero.Center;
                BaseDrawing.DrawChain(spriteBatch, new Texture2D[] { ArmTex2D, ArmTex2D, ArmTex2D }, 0, ArmOrigin, connector, ArmTex2D.Height - 10f, null, 1f, false, null);
				BaseDrawing.DrawTexture(spriteBatch, zeroTex, 0, Zero, BaseUtility.ColorClamp(BaseDrawing.GetNPCColor(Zero), GetGlowAlpha(true)));
                if (fifthHealth)
                {
                    BaseDrawing.DrawAura(spriteBatch, glowTex, 0, Zero, auraPercent, 1f, 0f, 0f, GetGlowAlpha(true));
                    BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, Zero, GetRedAlpha());
                }
                else
                {
                    BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, Zero, GetGlowAlpha(false));
                }
            }
        }
    }
	
}
