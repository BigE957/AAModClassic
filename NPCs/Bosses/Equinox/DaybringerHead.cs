using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

using AAMod.Dusts;
using System.IO;

namespace AAMod.NPCs.Bosses.Equinox
{
    [AutoloadBossHead]	
	public class DaybringerHead : ModNPC
	{	
		public bool nightcrawler = false;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Daybringer");
            Main.npcFrameCount[NPC.type] = 1;
		}

		public override void SetDefaults()
		{
            NPC.lifeMax = 100000;
            NPC.damage = 125;
            NPC.defense = 100;
            NPC.value = Item.sellPrice(0, 10, 0, 0);
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPC.knockBackResist = 0f;
            NPC.width = 68;
            NPC.height = 68;
            NPC.boss = true;
            NPC.aiStyle = -1;
			NPC.timeLeft = 500;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.DeathSound = null;
			NPC.HitSound = SoundID.NPCHit4;
			NPC.DeathSound = SoundID.NPCDeath14;
            Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Equinox");
            SceneEffectPriority = SceneEffectPriority.BossHigh;
            bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = Mod.Find<ModItem>("EquinoxBag").Type;
		}

        public float[] internalAI = new float[8];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                if(NPC.type == Mod.Find<ModNPC>("DaybringerHead").Type || NPC.type == Mod.Find<ModNPC>("NightcrawlerHead").Type)
                {
                    writer.Write(internalAI[0]);
                    writer.Write(internalAI[1]);
                    writer.Write(internalAI[2]);
                    writer.Write(internalAI[3]);
                    writer.Write(internalAI[4]);
                    writer.Write(internalAI[5]);
                    writer.Write(internalAI[6]);
                    writer.Write(internalAI[7]);
                    
                    writer.Write(preShootingSun);
                    writer.Write(preDeathRay);
                    writer.Write(isDeathRay);
                    writer.Write(CloudCooldown);
                }

                writer.Write(initCustom);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                if(NPC.type == Mod.Find<ModNPC>("DaybringerHead").Type || NPC.type == Mod.Find<ModNPC>("NightcrawlerHead").Type)
                {
                    internalAI[0] = reader.ReadFloat(); //DaybringerCounter
                    internalAI[1] = reader.ReadFloat(); //NightclawerCounter
                    internalAI[2] = reader.ReadFloat();
                    internalAI[3] = reader.ReadFloat();
                    internalAI[4] = reader.ReadFloat(); //DaybringerPosCheck
                    internalAI[5] = reader.ReadFloat(); //VelocitySave
                    internalAI[6] = reader.ReadFloat(); //VelocitySave
                    internalAI[7] = reader.ReadFloat();
                    
                    preShootingSun = reader.ReadBoolean();
                    preDeathRay = reader.ReadBoolean();
                    isDeathRay = reader.ReadBoolean();
                    CloudCooldown = reader.ReadInt();
                }

                initCustom = reader.ReadBoolean();
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void BossHeadRotation(ref float rotation)
		{
			rotation = NPC.rotation;
		}

		public override bool CheckActive()
		{
			NPC.timeLeft--;
			return NPC.timeLeft < 50;
		}

		public void HandleDayNightCycle()
		{
			bool daybringerExists = NPC.AnyNPCs(ModContent.NPCType<DaybringerHead>());
			bool nightcrawlerExists = NPC.AnyNPCs(ModContent.NPCType<NightcrawlerHead>());
			if (daybringerExists && nightcrawlerExists)
            {
                if((NPC.type == Mod.Find<ModNPC>("DaybringerHead").Type && Main.dayTime && !preShootingSun) || (NPC.type == Mod.Find<ModNPC>("NightcrawlerHead").Type && !Main.dayTime && !preDeathRay))
                {
                    if (Main.expertMode)
                    {
                        Main.fastForwardTime/* tModPorter Note: Removed. Suggestion: IsFastForwardingTime(), fastForwardTimeToDawn or fastForwardTimeToDusk */ = true;
                        Main.dayRate = 20;
                    }else
                    {
                        Main.fastForwardTime/* tModPorter Note: Removed. Suggestion: IsFastForwardingTime(), fastForwardTimeToDawn or fastForwardTimeToDusk */ = true;
                        Main.dayRate = 15;
                    }
                }
                else if((NPC.type == Mod.Find<ModNPC>("DaybringerHead").Type && preShootingSun) || (NPC.type == Mod.Find<ModNPC>("NightcrawlerHead").Type && (preDeathRay || isDeathRay)))
                {
                    Main.dayRate = 0;
                    Main.fastForwardTime/* tModPorter Note: Removed. Suggestion: IsFastForwardingTime(), fastForwardTimeToDawn or fastForwardTimeToDusk */ = false;
                    Main.time --;
                }
            }else
            if ((daybringerExists && !nightcrawlerExists))
            {
                Main.fastForwardTime/* tModPorter Note: Removed. Suggestion: IsFastForwardingTime(), fastForwardTimeToDawn or fastForwardTimeToDusk */ = true;
                Main.dayTime = true;
                Main.dayRate = 0;
                if(preShootingSun)
                {
                    Main.fastForwardTime/* tModPorter Note: Removed. Suggestion: IsFastForwardingTime(), fastForwardTimeToDawn or fastForwardTimeToDusk */ = false;
                    Main.time --;
                }
            }else
            if ((!daybringerExists && nightcrawlerExists))
            {
                Main.fastForwardTime/* tModPorter Note: Removed. Suggestion: IsFastForwardingTime(), fastForwardTimeToDawn or fastForwardTimeToDusk */ = true;
                Main.dayTime = false;
                Main.dayRate = 0;
                if(preDeathRay || isDeathRay)
                {
                    Main.fastForwardTime/* tModPorter Note: Removed. Suggestion: IsFastForwardingTime(), fastForwardTimeToDawn or fastForwardTimeToDusk */ = false;
                    Main.time --;
                }
            }else
            {
                Main.dayRate = 1;
                Main.fastForwardTime/* tModPorter Note: Removed. Suggestion: IsFastForwardingTime(), fastForwardTimeToDawn or fastForwardTimeToDusk */ = false;
            }
		}
        bool preDeathRay = false;
        bool isDeathRay = false;
        bool preShootingSun = false;
		bool prevWormStronger = false;
		bool initCustom = false;
        public int CloudCount = Main.expertMode ? 8 : 6;
        public int CloudCooldown = 400;

        public override bool PreAI()
        {
            bool isHead = NPC.type == Mod.Find<ModNPC>("DaybringerHead").Type || NPC.type == Mod.Find<ModNPC>("NightcrawlerHead").Type;
            if (Main.netMode != 1 && !initCustom)
            {
                initCustom = true;
                internalAI[7] += NPC.whoAmI % 7 * 12; //so it doesn't pew all at once
                NPC.velocity.X += 0.1f;
                NPC.velocity.Y -= 4f;
            }
            if (isHead)
            {
                HandleDayNightCycle();
            }

            bool isDay = Main.dayTime;
            bool wormStronger = (nightcrawler && !isDay) || (!nightcrawler && isDay);

            float wormDistance = -26f;
            int aiCount = 2;
            float moveSpeedMax = 16f;
            NPC.damage = 125;
            NPC.defense = 100;

            if (wormStronger != prevWormStronger)
            {
                int dustType = nightcrawler ? ModContent.DustType<NightcrawlerDust>() : ModContent.DustType<DaybringerDust>();
                for (int k = 0; k < 10; k++)
                {
                    int dustID = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType, (int)(NPC.velocity.X * 0.2f), (int)(NPC.velocity.Y * 0.2f), 0, default, 1.5f);
                    Main.dust[dustID].noGravity = true;
                }
            }

            if (wormStronger)
            {
                if(Main.netMode == 0) 
                {
                    NPC.width = 136;
                    NPC.height = 136;
                    wormDistance = -52f;
                }
                aiCount = !nightcrawler ? 6 : 4;
                moveSpeedMax = !nightcrawler ? 15f : 12f;
                NPC.damage = 150;
                NPC.defense = !nightcrawler ? 120 : 150;
            }

            for (int m = 0; m < aiCount; m++)
            {
                int Length = nightcrawler ? 24 : 30;
                int[] wormTypes = nightcrawler ? new int[] { Mod.Find<ModNPC>("NightcrawlerHead").Type, Mod.Find<ModNPC>("NightcrawlerBody").Type, Mod.Find<ModNPC>("NightcrawlerTail").Type } : new int[] { Mod.Find<ModNPC>("DaybringerHead").Type, Mod.Find<ModNPC>("DaybringerBody").Type, Mod.Find<ModNPC>("DaybringerTail").Type };
                AAAI.AIWorm(NPC, wormTypes, Length, wormDistance, moveSpeedMax, 0.07f, true, false, false, false, false, false);
            }

            if (isHead) //prevents despawn and allows them to run away
            {
                bool foundTarget = TargetClosest();
                if (foundTarget)
                {
                    NPC.timeLeft = 300;
                }
                else
                {
                    if (NPC.timeLeft > 50) NPC.timeLeft = 50;
                    NPC.velocity.Y -= 0.2f;
                    if (NPC.velocity.Y < -20f) NPC.velocity.Y = -20f;
                    return false;
                }
            }
            else
            {
                NPC.timeLeft = 300; //pieces should not despawn naturally, only despawn when the head does
            }
            
            Player target = Main.player[NPC.target];
            
            if (NPC.type == Mod.Find<ModNPC>("NightcrawlerHead").Type)
            {
                if(isDeathRay)
                {
                    goto ExtraAI;
                }
                if(preDeathRay)
                {
                    NPC.defense = 9999;
                    if((NPC.Center - target.Center).Length() < 300f)
                    {
                        isDeathRay = true;
                        NPC.netUpdate = true;
                    }

                    if (NPC.Center.X < target.Center.X)
                    {
                        NPC.velocity.X += 0.5f;
                        if (NPC.velocity.X < 0)
                            NPC.velocity.X += 0.5f * 2;
                    }
                    else
                    {
                        NPC.velocity.X -= 0.5f;
                        if (NPC.velocity.X > 0)
                            NPC.velocity.X -= 0.5f * 2;
                    }
                    if (NPC.Center.Y < target.Center.Y)
                    {
                        NPC.velocity.Y += 0.5f;
                        if (NPC.velocity.Y < 0)
                            NPC.velocity.Y += 0.5f * 2;
                    }
                    else
                    {
                        NPC.velocity.Y -= 0.5f;
                        if (NPC.velocity.Y > 0)
                            NPC.velocity.Y -= 0.5f * 2;
                    }

                    if(NPC.velocity.X > 30f) NPC.velocity.X = 30f;
                    if(NPC.velocity.Y > 30f) NPC.velocity.Y = 30f;

                    internalAI[5] = NPC.velocity.X;
                    internalAI[6] = NPC.velocity.Y;
                }
            }
            if (NPC.type == Mod.Find<ModNPC>("DaybringerHead").Type)
            {
                if(preShootingSun)
                {
                    NPC.defense = 9999;
                    NPC.TargetClosest(false);
                    goto ExtraAI;
                }
            }

            if(!isHead)
            {
                NPC.defense = Main.npc[NPC.realLife].defense;
            }
            goto Normal;

            ExtraAI:
            if(NPC.type == Mod.Find<ModNPC>("NightcrawlerHead").Type)
            {
                NPC.defense = 9999;
                NPC.TargetClosest(false);
                NPC.velocity = new Vector2(internalAI[5], internalAI[6]);
                
                if(internalAI[2] < 120)
                {
                    Vector2 newvelocity = NPC.velocity + Vector2.Normalize(NPC.velocity.RotatedBy((float)Math.PI/2)) * 0.58f;
                    NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;
                    NPC.velocity = Vector2.Normalize(newvelocity) * 16f;
                }
                else
                {
                    Vector2 newvelocity = NPC.velocity + Vector2.Normalize(NPC.velocity.RotatedBy((float)Math.PI/2)) * 0.03625f;
                    NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;
                    NPC.velocity = Vector2.Normalize(newvelocity) * 4f;
                }

                if (internalAI[2]++ == 90)
                {
                    for (int i = 0; i < Main.maxNPCs; i+=2)
                    {
                        if (Main.npc[i].active && Main.npc[i].type == Mod.Find<ModNPC>("NightcrawlerBody").Type && Main.npc[i].realLife == NPC.whoAmI)
                        {
                            if (Main.netMode != 1)
                            {
                                Vector2 speed = Vector2.Normalize(new Vector2(1f, 0f).RotatedBy(Main.npc[i].rotation + 3.1415f)) * 8f;
                                Projectile.NewProjectile(Main.npc[i].Center.X, Main.npc[i].Center.Y, speed.X, speed.Y, Mod.Find<ModProjectile>("NightclawerDeathraySmall").Type, NPC.damage / 2, 0, Main.myPlayer, 0, i);
                            }
                        }
                    }
                }
                if (internalAI[2] >= 90)
                {
                    for(int deathRay = 0; deathRay < Main.maxProjectiles; deathRay++)
                    {
                        if(Main.projectile[deathRay].active && Main.projectile[deathRay].type == Mod.Find<ModProjectile>("NightclawerDeathraySmall").Type || Main.projectile[deathRay].type == Mod.Find<ModProjectile>("NightclawerDeathray").Type && Main.projectile[deathRay].ai[1] == NPC.whoAmI)
                        {
                            return false;
                        }
                    }
                }

                internalAI[5] = NPC.velocity.X;
                internalAI[6] = NPC.velocity.Y;

                if(internalAI[2] > 400)
                {
                    internalAI[2] = 0;
                    isDeathRay = false;
                    preDeathRay = false;
                    NPC.netUpdate = true;
                }
            }
            if(NPC.type == Mod.Find<ModNPC>("DaybringerHead").Type)
            {
                NPC.defense = 9999;
                NPC.TargetClosest(false);
                NPC.velocity = new Vector2(internalAI[5], internalAI[6]);
                Vector2 targetpos = target.Center - new Vector2(0, 2000f);
                Vector2 targetpos2 = target.Center - new Vector2(1000f, 1000f);
                Vector2 targetpos3 = target.Center - new Vector2(-1000f, 1000f);

                if(internalAI[4] == 0)
                {
                    if(Math.Abs(NPC.Center.X - targetpos.X) + Math.Abs(NPC.Center.Y - targetpos.Y) < 100f)
                    {
                        internalAI[4] = 1f;
                    }
                }
                else if(internalAI[4] == 1)
                {
                    targetpos = targetpos2;
                    if(Math.Abs(NPC.Center.X - targetpos.X) + Math.Abs(NPC.Center.Y - targetpos.Y) < 100f)
                    {
                        internalAI[4] = 2f;
                        if(Main.netMode != 1)
                        {
                            for (int i = 0; i < Main.maxNPCs; i+= 3)
                            {
                                if (Main.npc[i].active && Main.npc[i].type == Mod.Find<ModNPC>("DaybringerBody").Type && Main.npc[i].realLife == NPC.whoAmI && AAGlobalProjectile.CountProjectiles(Mod.Find<ModProjectile>("DaybringerSun").Type) < 3)
                                {
                                    Vector2 speed = Vector2.Normalize(new Vector2(1f, 0f).RotatedBy(Main.npc[i].rotation + 3.1415f)) * 8f;
                                    Projectile.NewProjectile(Main.npc[i].Center.X, Main.npc[i].Center.Y, -speed.X, -speed.Y, Mod.Find<ModProjectile>("DaybringerSun").Type, NPC.damage / 3, 1, 255);
                                }
                            }
                        }
                    }
                }
                else if(internalAI[4] == 2)
                {
                    targetpos = targetpos3;
                    if(Math.Abs(NPC.Center.X - targetpos.X) + Math.Abs(NPC.Center.Y - targetpos.Y) < 100f)
                    {
                        internalAI[4] = 1f;
                        if(Main.netMode != 1)
                        {
                            for (int i = 0; i < Main.maxNPCs; i+= 3)
                            {
                                if (Main.npc[i].active && Main.npc[i].type == Mod.Find<ModNPC>("DaybringerBody").Type && Main.npc[i].realLife == NPC.whoAmI && AAGlobalProjectile.CountProjectiles(Mod.Find<ModProjectile>("DaybringerSun").Type) < 3)
                                {
                                    Vector2 speed = Vector2.Normalize(new Vector2(1f, 0f).RotatedBy(Main.npc[i].rotation + 3.1415f)) * 8f;
                                    Projectile.NewProjectile(Main.npc[i].Center.X, Main.npc[i].Center.Y, -speed.X, -speed.Y, Mod.Find<ModProjectile>("DaybringerSun").Type, NPC.damage / 3, 1, 255);
                                }
                            }
                        }
                    }
                }
                if (internalAI[3] % 200 == 60 && Main.netMode != 1)
                {
                    Vector2 speed = Vector2.Normalize(NPC.velocity) * 8f;
                    Projectile.NewProjectile(NPC.Center.X, NPC.Center.Y, speed.X, speed.Y, Mod.Find<ModProjectile>("DaybringerSun").Type, NPC.damage / 2, 1, 255);
                }
                if (NPC.Center.X < targetpos.X)
                {
                    NPC.velocity.X += 0.5f;
                    if (NPC.velocity.X < 0)
                        NPC.velocity.X += 0.5f * 2;
                }
                else
                {
                    NPC.velocity.X -= 0.5f;
                    if (NPC.velocity.X > 0)
                        NPC.velocity.X -= 0.5f * 2;
                }
                if (NPC.Center.Y < targetpos.Y)
                {
                    NPC.velocity.Y += 0.5f;
                    if (NPC.velocity.Y < 0)
                        NPC.velocity.Y += 0.5f * 2;
                }
                else
                {
                    NPC.velocity.Y -= 0.5f;
                    if (NPC.velocity.Y > 0)
                        NPC.velocity.Y -= 0.5f * 2;
                }

                if(NPC.velocity.X > 30f) NPC.velocity.X = 30f;
                if(NPC.velocity.Y > 30f) NPC.velocity.Y = 30f;

                NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;

                internalAI[5] = NPC.velocity.X;
                internalAI[6] = NPC.velocity.Y;

                if(internalAI[3]++ > 700)
                {
                    internalAI[3] = 0;
                    internalAI[5] = 0;
                    internalAI[6] = 0;
                    preShootingSun = false;
                    NPC.netUpdate = true;
                }
            }
            return false;

            Normal:

            NPC.spriteDirection = 1;
            prevWormStronger = wormStronger;

            if (NPC.type == ModContent.NPCType<NightcrawlerHead>() && NPC.CountNPCS(ModContent.NPCType<NCCloud>()) < CloudCount && CloudCooldown > 0 && Main.netMode != 1)
            {
                CloudCooldown--;

                if (CloudCooldown <= 0)
                {
                    CloudCooldown = 0;
                }
            }

            if(isDay && !preShootingSun)
            {
                if(isHead && NPC.type == Mod.Find<ModNPC>("DaybringerHead").Type)
                {
                    internalAI[0] += 1f;
                    if(internalAI[0] % 360 == 0)
                    {
                        for(int playerid = 0; playerid < 255; playerid++)
                        {
                            if(Main.player[playerid].active && !Main.player[playerid].dead && Main.player[playerid] != null && Main.player[playerid].ownedProjectileCounts[Mod.Find<ModProjectile>("DaybringerStars").Type] <= 0)
                            {
                                if (NPC.life > NPC.lifeMax / 2)
                                {
                                    if (Main.rand.Next(2) == 0)
                                    {
                                        Projectile.NewProjectile(Main.player[playerid].Center.X - 200f, Main.player[playerid].Center.Y + 200f, 0, 0, Mod.Find<ModProjectile>("DaybringerStars").Type, NPC.damage / 3, 5, playerid, -200f, playerid);
                                        Projectile.NewProjectile(Main.player[playerid].Center.X, Main.player[playerid].Center.Y - 300f, 0, 0, Mod.Find<ModProjectile>("DaybringerStars").Type, NPC.damage / 3, 5, playerid, 0, playerid);
                                        Projectile.NewProjectile(Main.player[playerid].Center.X + 200f, Main.player[playerid].Center.Y + 200f, 0, 0, Mod.Find<ModProjectile>("DaybringerStars").Type, NPC.damage / 3, 5, playerid, 200f, playerid);
                                    }
                                    else
                                    {
                                        Projectile.NewProjectile(Main.player[playerid].Center.X + 200f, Main.player[playerid].Center.Y - 200f, 0, 0, Mod.Find<ModProjectile>("DaybringerStars").Type, NPC.damage / 3, 5, playerid, -200f, playerid);
                                        Projectile.NewProjectile(Main.player[playerid].Center.X, Main.player[playerid].Center.Y + 300f, 0, 0, Mod.Find<ModProjectile>("DaybringerStars").Type, NPC.damage / 3, 5, playerid, 0, playerid);
                                        Projectile.NewProjectile(Main.player[playerid].Center.X - 200f, Main.player[playerid].Center.Y - 200f, 0, 0, Mod.Find<ModProjectile>("DaybringerStars").Type, NPC.damage / 3, 5, playerid, 200f, playerid);
                                    }
                                }
                                else
                                {
                                    if (Main.rand.Next(2) == 0)
                                    {
                                        Projectile.NewProjectile(Main.player[playerid].Center.X - 200f, Main.player[playerid].Center.Y + 200f, 0, 0, Mod.Find<ModProjectile>("DaybringerStars").Type, NPC.damage / 3, 5, playerid, -200f, playerid);
                                        Projectile.NewProjectile(Main.player[playerid].Center.X + 200f, Main.player[playerid].Center.Y + 200f, 0, 0, Mod.Find<ModProjectile>("DaybringerStars").Type, NPC.damage / 3, 5, playerid, 200f, playerid);
                                        Projectile.NewProjectile(Main.player[playerid].Center.X - 200f, Main.player[playerid].Center.Y - 200f, 0, 0, Mod.Find<ModProjectile>("DaybringerStars").Type, NPC.damage / 3, 5, playerid, -200f, playerid);
                                        Projectile.NewProjectile(Main.player[playerid].Center.X + 200f, Main.player[playerid].Center.Y - 200f, 0, 0, Mod.Find<ModProjectile>("DaybringerStars").Type, NPC.damage / 3, 5, playerid, 200f, playerid);
                                    }
                                    else
                                    {
                                        Projectile.NewProjectile(Main.player[playerid].Center.X, Main.player[playerid].Center.Y + 300f, 0, 0, Mod.Find<ModProjectile>("DaybringerStars").Type, NPC.damage / 3, 5, playerid, 0, playerid);
                                        Projectile.NewProjectile(Main.player[playerid].Center.X, Main.player[playerid].Center.Y - 300f, 0, 0, Mod.Find<ModProjectile>("DaybringerStars").Type, NPC.damage / 3, 5, playerid, 0, playerid);
                                        Projectile.NewProjectile(Main.player[playerid].Center.X + 300f, Main.player[playerid].Center.Y, 0, 0, Mod.Find<ModProjectile>("DaybringerStars").Type, NPC.damage / 3, 5, playerid, 0, playerid);
                                        Projectile.NewProjectile(Main.player[playerid].Center.X - 300f, Main.player[playerid].Center.Y, 0, 0, Mod.Find<ModProjectile>("DaybringerStars").Type, NPC.damage / 3, 5, playerid, 0, playerid);
                                    }
                                }
                            }
                        }
                    }
                    if(internalAI[0] % 120 == 30 && Main.netMode != 1)
                    {
                        for (int i = 0; i < Main.maxNPCs; i += 2)
                        {
                            if (Main.npc[i].active && Main.npc[i].type == Mod.Find<ModNPC>("DaybringerBody").Type && Main.npc[i].realLife == NPC.whoAmI)
                            {
                                Vector2 speed = Vector2.Normalize(new Vector2(1f, 0f).RotatedBy(Main.npc[i].rotation + 3.1415f)) * 12f;
                                Projectile.NewProjectile(Main.npc[i].Center.X, Main.npc[i].Center.Y, speed.X, speed.Y, Mod.Find<ModProjectile>("DayBringerDarts").Type, NPC.damage / 3, 0, Main.myPlayer);
                                speed = -speed;
                                Projectile.NewProjectile(Main.npc[i].Center.X, Main.npc[i].Center.Y, speed.X, speed.Y, Mod.Find<ModProjectile>("DayBringerDarts").Type, NPC.damage / 3, 0, Main.myPlayer);
                            }
                        }
                    }
                    if(internalAI[0] % 120 == 60 && Main.netMode != 1)
                    {
                        for (int i = 0; i < Main.maxNPCs; i+=4)
                        {
                            if (Main.npc[i].active && Main.npc[i].type == Mod.Find<ModNPC>("DaybringerBody").Type && Main.npc[i].realLife == NPC.whoAmI && Main.rand.Next(15) == 0)
                            {
                                Vector2 speed = Vector2.Normalize(new Vector2(1f, 0f).RotatedBy(Main.npc[i].rotation + 3.1415f)) * 8f;
                                Projectile.NewProjectile(Main.npc[i].Center.X, Main.npc[i].Center.Y, speed.X, speed.Y, Mod.Find<ModProjectile>("DaybringerOrb").Type, NPC.damage / 3, 0, Main.myPlayer, 0, NPC.whoAmI);
                                speed = -speed;
                                Projectile.NewProjectile(Main.npc[i].Center.X, Main.npc[i].Center.Y, speed.X, speed.Y, Mod.Find<ModProjectile>("DaybringerOrb").Type, NPC.damage / 3, 0, Main.myPlayer, 0, NPC.whoAmI);
                            }
                        }
                    }
                    

                    if(internalAI[0] > 1200)
                    {
                        if(Main.expertMode) preShootingSun = true;
                        internalAI[0] = 0f;
                        NPC.netUpdate = true;
                    }
                }
            }
            if(!isDay && !preDeathRay)
            {
                if(isHead && NPC.type == Mod.Find<ModNPC>("NightcrawlerHead").Type)
                {
                    internalAI[1] += 1f;
                    if (Main.netMode != 1 && CloudCooldown <= 0)
                    {
                        for(int i = 0; i < 200; i++)
                        {
                            if(Main.npc[i].type == Mod.Find<ModNPC>("NCCloud").Type)
                            {
                                Main.npc[i].life = 0;
                                Main.npc[i].NPCLoot();
                                Main.npc[i].active = false;
                            } 
                        }
                        CloudCooldown = 400;
                        float rotation = 2f * (float)Math.PI / CloudCount;
                        for (int m = 0; m < CloudCount; m++)
                        {
                            int n = NPC.NewNPC((int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("NCCloud").Type, 0, 0, 0, 0, rotation * m);
                            if (Main.netMode == 2 && n < 200)
                                NetMessage.SendData(23, -1, -1, null, n);
                        }
                    }
                    
                    if(internalAI[1] % 380 == 90 && Main.netMode != 1)
                    {
                        for (int i = 0; i < Main.maxNPCs; i+= 4)
                        {
                            if (Main.npc[i].active && Main.npc[i].type == Mod.Find<ModNPC>("NightcrawlerBody").Type && Main.npc[i].realLife == NPC.whoAmI)
                            {
                                Vector2 speed = Vector2.Normalize(new Vector2(1f, 0f).RotatedBy(Main.npc[i].rotation + 3.1415f)) * .5f;
                                Projectile.NewProjectile(Main.npc[i].Center.X, Main.npc[i].Center.Y, speed.X, speed.Y, Mod.Find<ModProjectile>("NightclawerScythe").Type, NPC.damage / 3, 0, Main.myPlayer, NPC.rotation, NPC.spriteDirection);
                                speed = -speed;
                                Projectile.NewProjectile(Main.npc[i].Center.X, Main.npc[i].Center.Y, speed.X, speed.Y, Mod.Find<ModProjectile>("NightclawerScythe").Type, NPC.damage / 3, 0, Main.myPlayer, NPC.rotation, NPC.spriteDirection);
                            }
                        }
                    }

                    
                    if(internalAI[1] % 120 == 90 && Main.netMode != 1)
                    {
                        for (int i = 0; i < Main.maxNPCs; i++)
                        {
                            if (Main.npc[i].active && Main.npc[i].type == Mod.Find<ModNPC>("NightcrawlerBody").Type && Main.npc[i].realLife == NPC.whoAmI && Main.rand.Next(10) == 0)
                            {
                                Vector2 speed = Vector2.Normalize(new Vector2(1f, 0f).RotatedBy(Main.npc[i].rotation + 3.1415f));
                                speed = (Main.rand.Next(2) == 0 ? 1: -1) * speed;
                                float ai = Main.rand.Next(120);
                                Vector2 speedR = Vector2.Normalize(speed.RotatedByRandom(0.6)) * 20f;
                                Projectile.NewProjectile(Main.npc[i].Center.X, Main.npc[i].Center.Y, speedR.X, speedR.Y, Mod.Find<ModProjectile>("NightclawerLaser").Type, NPC.damage / 3, 0, Main.myPlayer, speed.ToRotation() + 1000f, ai);
                            }
                        }
                    }

                    if(internalAI[1] > 1200)
                    {
                        internalAI[1] = 0f;
                        if(Main.expertMode) preDeathRay = true;
                        NPC.netUpdate = true;
                    }
                }
            }
            return false;
        }

		public int playerTooFarDist = 16000; //1000 tile radius, these worms move fast!		
		public bool TargetClosest()
		{
			int[] players = BaseAI.GetPlayers(NPC.Center, Math.Min(20000f, playerTooFarDist * 3));
			float dist = 999999999f;
			int foundPlayer = -1;
			for (int m = 0; m < players.Length; m++)
			{
				Player p = Main.player[players[m]];
				if (Vector2.Distance(p.Center, NPC.Center) < dist)
				{
					dist = Vector2.Distance(p.Center, NPC.Center);
					foundPlayer = p.whoAmI;
				}
			}
			if (foundPlayer != -1)
			{
				BaseAI.SetTarget(NPC, foundPlayer);
				return true;
			}
			return false;
		}

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.65f * bossLifeScale);
			NPC.damage = (int)(NPC.damage * 0.85f);
		}

		bool spawnedGore = false;
        public override void HitEffect(NPC.HitInfo hit)
        {
			int dustType = nightcrawler ? ModContent.DustType<NightcrawlerDust>() : ModContent.DustType<DaybringerDust>();
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType, hitDirection, -1f, 0, default, 1.2f);
            }
            if (NPC.life <= 0 || (NPC.life - damage <= 0))
            {			
				Main.dayRate = 1;
                Main.fastForwardTime/* tModPorter Note: Removed. Suggestion: IsFastForwardingTime(), fastForwardTimeToDawn or fastForwardTimeToDusk */ = false;	
				if(!spawnedGore)
				{
					spawnedGore = true;
					bool isHead = NPC.type == Mod.Find<ModNPC>("DaybringerHead").Type || NPC.type == Mod.Find<ModNPC>("NightcrawlerHead").Type;
					bool isBody = NPC.type == Mod.Find<ModNPC>("DaybringerBody").Type || NPC.type == Mod.Find<ModNPC>("NightcrawlerBody").Type;						
					if(nightcrawler)
					{
						if(isHead)
						{
							Gore.NewGore(NPC.position, NPC.velocity * 0.2f, Mod.GetGoreSlot("Gores/NCGore1"), 1f);	
							Gore.NewGore(NPC.position, NPC.velocity * 0.2f, Mod.GetGoreSlot("Gores/NCGore2"), 1f);						
						}else
						if(isBody)
						{
							Gore.NewGore(NPC.position, NPC.velocity * 0.2f, Mod.GetGoreSlot("Gores/NCGore3"), 1f);							
						}else
						{
							Gore.NewGore(NPC.position, NPC.velocity * 0.2f, Mod.GetGoreSlot("Gores/NCGore4"), 1f);						
						}
					}else
					{
						if(isHead)
						{
							Gore.NewGore(NPC.position, NPC.velocity * 0.2f, Mod.GetGoreSlot("Gores/DBGore1"), 1f);	
							Gore.NewGore(NPC.position, NPC.velocity * 0.2f, Mod.GetGoreSlot("Gores/DBGore2"), 1f);						
						}else
						if(isBody)
						{
							Gore.NewGore(NPC.position, NPC.velocity * 0.2f, Mod.GetGoreSlot("Gores/DBGore3"), 1f);							
						}else
						{
							Gore.NewGore(NPC.position, NPC.velocity * 0.2f, Mod.GetGoreSlot("Gores/DBGore4"), 1f);						
						}					
					}
					for (int k = 0; k < 15; k++)
					{
						Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType, hitDirection, -1f, 0, default, 1.5f);
					}
				}
            }
        }

        public override void OnKill()
        {
            int otherWormAlive = nightcrawler ? Mod.Find<ModNPC>("DaybringerHead").Type : Mod.Find<ModNPC>("NightcrawlerHead").Type;
            if (!nightcrawler)
            {
                AAWorld.downedDB = true;
                BaseAI.DropItem(NPC, Mod.Find<ModItem>("DBTrophy").Type, 1, 1, 15, true);
            }
            else
            {
                AAWorld.downedNC = true;
                BaseAI.DropItem(NPC, Mod.Find<ModItem>("NCTrophy").Type, 1, 1, 15, true);
            }
            if (NPC.CountNPCS(otherWormAlive) == 0)
            {
                AAWorld.downedEquinox = true;
            }
			string wormType = nightcrawler ? "Nightcrawler" : "Daybringer";
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>(wormType + "Trophy").Type);
			}
			if (Main.expertMode)
			{
                NPC.DropBossBags();
			}
			else
			{
				if (Main.rand.Next(7) == 0)
				{
					Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>(wormType + "Mask").Type);
				}
                if (!nightcrawler)
                {
                    Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("Stardust").Type, Main.rand.Next(30, 75));
                }
                else
                {
                    Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("DarkEnergy").Type, Main.rand.Next(30, 75));
                }
                if (AAWorld.RadiumOre)
                {
                    Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("StarIdol").Type);
                }
			}
        }

		public Color GetAuraAlpha()
		{
			Color c = Color.White * (Main.mouseTextColor / 255f);
			//c.A = 255;
			return c;
		}

        public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            MakeSegmentsImmune(NPC, player.whoAmI);
            ModifyCritArea(NPC, ref crit);
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            MakeSegmentsImmune(NPC, projectile.owner);
            ModifyCritArea(NPC, ref crit);
            if (projectile.penetrate != 1)
            {
                for(int i = 0; i < Main.maxNPCs; i ++)
                {
                    if(Main.npc[i].active && (Main.npc[i].whoAmI == NPC.realLife || (Main.npc[i].realLife >= 0 && Main.npc[i].realLife == NPC.realLife)))
                    {
                        Main.npc[i].immune[projectile.owner] = 10;
                    }
                }
                damage = (int)(damage * .44f);
            }
            if (NPC.type != Mod.Find<ModNPC>("DaybringerHead").Type && NPC.type != Mod.Find<ModNPC>("NightcrawlerHead").Type)
            {
                damage = (int)(damage * .76f);
            }
        }

        private void ModifyCritArea(NPC npc, ref bool crit)
        {
            if (npc.realLife >= 0)
            {
                if (npc.whoAmI == npc.realLife)
                {
                    crit = true;
                }
                if (npc.ai[0] == 0)
                {
                    crit = false;
                }
            }
        }

        public override void UpdateLifeRegen(ref int damage)
        {
            if (NPC.realLife >= 0 && NPC.whoAmI != NPC.realLife)
            {
                damage = 0;
                NPC.lifeRegen = 0;
            }
        }

        public void MakeSegmentsImmune(NPC npc, int id)
        {
            if (npc.realLife >= 0)
            {
                bool last = false;
                NPC parent = Main.npc[npc.realLife];
                parent.lifeRegen = npc.lifeRegen;
                int i = 0;
                while (parent.ai[0] > 0 || last)
                {
                    if (i++ > 200) { return; }
                    parent.immune[id] = npc.immune[id];
                    for (int j = 0; j < npc.buffType.Length; j++)
                    {
                        if (npc.buffType[j] > 0 && npc.buffTime[j] > 0)
                        {
                            parent.buffType[j] = npc.buffType[j];
                            parent.buffTime[j] = npc.buffTime[j];
                        }
                    }
                    if (last) { break; }
                    parent = Main.npc[(int)parent.ai[0]];
                    if (parent.ai[0] == 0) { last = true; }
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            bool wormStronger = (nightcrawler && !Main.dayTime) || (!nightcrawler && Main.dayTime);
            Texture2D tex = TextureAssets.Npc[NPC.type].Value;
            NPC.width = 68;
            NPC.height = 68;
            if (wormStronger)
            {
                string texName = "NPCs/Bosses/Equinox/";
                if (NPC.type == Mod.Find<ModNPC>("DaybringerHead").Type) { texName += "DaybringerHeadBig"; }
                else
                if (NPC.type == Mod.Find<ModNPC>("DaybringerBody").Type) { texName += "DaybringerBodyBig"; }
                else
                if (NPC.type == Mod.Find<ModNPC>("DaybringerTail").Type) { texName += "DaybringerTailBig"; }
                else
                if (NPC.type == Mod.Find<ModNPC>("NightcrawlerHead").Type) { texName += "NightcrawlerHeadBig"; }
                else
                if (NPC.type == Mod.Find<ModNPC>("NightcrawlerBody").Type) { texName += "NightcrawlerBodyBig"; }
                else
                if (NPC.type == Mod.Find<ModNPC>("NightcrawlerTail").Type) { texName += "NightcrawlerTailBig"; }
                tex = Mod.GetTexture(texName);

                int diff = Main.LocalPlayer.miscCounter % 50;
                float diffFloat = diff / 50f;
                float auraPercent = BaseUtility.MultiLerp(diffFloat, 0f, 1f, 0f); //did it this way so it's syncronized between all the segments
                BaseDrawing.DrawAura(spritebatch, tex, 0, NPC, auraPercent, 2f, 0f, 0f, GetAuraAlpha());
            }
            BaseDrawing.DrawTexture(spritebatch, tex, 0, NPC, Color.White); //GetAuraAlpha());				
            return false;
        }
    }
}