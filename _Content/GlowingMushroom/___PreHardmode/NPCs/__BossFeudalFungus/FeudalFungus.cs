using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items._BossFeudalFungus.BossStandard;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items.Consumables;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items.Materials;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items.Tools;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items._BossMushroomMonarch.BossStandard;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tools;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items.Weapons;
using AAModClassic._CrossMod.CalamityMod.LoreItems;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs.__BossFeudalFungus
{
    [AutoloadBossHead]
    public class FeudalFungus : ModNPC
    {
        public int damage = 0;

		public override void SendExtraAI(BinaryWriter writer)
		{
			base.SendExtraAI(writer);
			if(Main.netMode == NetmodeID.Server || Main.dedServ)
			{
				writer.Write(internalAI[0]);
				writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
                writer.Write(internalAI[4]);
            }
		}

		public override void ReceiveExtraAI(BinaryReader reader)
		{
			base.ReceiveExtraAI(reader);
			if(Main.netMode == NetmodeID.MultiplayerClient)
			{
                internalAI[0] = reader.ReadSingle();
                internalAI[1] = reader.ReadSingle();
                internalAI[2] = reader.ReadSingle();
                internalAI[3] = reader.ReadSingle();
                internalAI[4] = reader.ReadSingle();
            }	
		}	

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Feudal Fungus");
            Main.npcFrameCount[NPC.type] = 8;
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 1200;   //boss life
            NPC.damage = 24;  //boss damage
            NPC.defense = 12;    //boss defense
            NPC.knockBackResist = 0f;   //this boss will behavior like the DemonEye  //boss frame/animation 
            NPC.value = Item.buyPrice(0, 0, 50, 0);
            NPC.aiStyle = NPCAIStyleID.Unicorn;
            NPC.width = 74;
            NPC.height = 108;
            NPC.npcSlots = 1f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = false;
            NPC.buffImmune[46] = true;
            NPC.buffImmune[47] = true;
            NPC.netAlways = true;
            NPC.noGravity = true;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            Music = MusicManagementSystem.MusicSlots["Fungus"];
            if (!NPC.IsABestiaryIconDummy)
                NPC.alpha = 255;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.SurfaceMushroom,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundMushroom,
            ]);
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public static int AISTATE_HOVER = 0, AISTATE_FLIER = 1, AISTATE_SHOOT = 2;
		public float[] internalAI = new float[5];
		
        public override void AI()
        {
            damage = 12;
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];
             
            if (Main.dayTime && player.position.Y < Main.worldSurface || !player.ZoneGlowshroom)
            {
                NPC.velocity *= 0;

                if (NPC.velocity.X <= .1f && NPC.velocity.X >= -.1f)
                {
                    NPC.velocity.X = 0;
                }
                if (NPC.velocity.Y <= .1f && NPC.velocity.Y >= -.1f)
                {
                    NPC.velocity.Y = 0;
                }

                NPC.alpha += 10;

                if (NPC.alpha >= 255)
                {
                    NPC.active = false;
                }
                return;
            }
            NPC.alpha -= 10;
            if (NPC.alpha < 0)
            {
                NPC.alpha = 0;
            }
            NPC.frameCounter++;
            if (NPC.frameCounter >= 10)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += 90;
                if (NPC.frame.Y > 90 * 7)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y = 0;
                }
            }

            NPC.noTileCollide = true;

            if (Main.netMode != NetmodeID.MultiplayerClient && internalAI[1] != AISTATE_SHOOT)
			{
                internalAI[0]++;
                if (internalAI[0] >= 180)
                {
                    internalAI[0] = 0;
                    internalAI[1] = Main.rand.Next(3);
                    NPC.ai = new float[4];
                    NPC.netUpdate = true;
                }
            }
			if(internalAI[1] == AISTATE_HOVER) 
            {
                BaseAI.AISpaceOctopus(NPC, ref NPC.ai, player.Center, 0.15f, 4f, 170, 56f, FireMagic);
            }
            else if (internalAI[1] == AISTATE_FLIER) 
            {
                BaseAI.AIFlier(NPC, ref NPC.ai, true, 0.1f,0.04f, 5f, 3f, false, 1);
            }
            else if (internalAI[1] == AISTATE_SHOOT)
            {
                BaseAI.AISpaceOctopus(NPC, ref NPC.ai, player.Center, 0.15f, 4f, 170, 56f, null);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    internalAI[0]++;
                }
                if (internalAI[0] >= 60)
                {
                    int attack = Main.rand.Next(4);
                    internalAI[1] = Main.rand.Next(3);
                    internalAI[0] = 0;
                    FungusAttack(attack);
                    NPC.netUpdate = true;
                }
            }

            NPC.rotation = 0;

            if (internalAI[4] ++ > 90 && Main.expertMode && Main.netMode != NetmodeID.MultiplayerClient)
            {
                internalAI[4] = 0;
                Vector2 pos = new Vector2(player.Center.X + Main.rand.Next(70, 150) * (Main.rand.NextBool(2) ? 1: -1), player.Center.Y + Main.rand.Next(70, 150) * (Main.rand.NextBool(2) ? 1: -1));
                Vector2 velocity = Vector2.Normalize(player.Center - pos) * .1f;
                int proj = Projectile.NewProjectile(NPC.GetSource_FromThis(), pos.X, pos.Y, velocity.X, velocity.Y, ModContent.ProjectileType<FeudalFungus_FungusCloud>(), damage, 0, Main.myPlayer, 0f, 0f);
                Main.projectile[proj].timeLeft = 720;
                Main.projectile[proj].alpha = 255;
            }
        }


        public float[] shootAI = new float[4];

        public void FireMagic(NPC npc, Vector2 velocity)
        {
            Player player = Main.player[npc.target];
            BaseAI.ShootPeriodic(npc, player.position, player.width, player.height, ModContent.ProjectileType<FeudalFungus_SporeBlast>(), ref shootAI[0], 5, damage, 8f, false, new Vector2(20f, 15f));
        }

        public override void BossLoot(ref int potionType)
        {   //boss drops
            potionType = ItemID.ManaPotion;
            
        }

        public override void OnKill()
        {
        AADowned.downedFeudalFungus = true;
        AADowned.SyncWorldData();
            Projectile.NewProjectile(NPC.GetSource_Death(), NPC.Center, NPC.velocity, ModContent.ProjectileType<FeudalFungusLeave>(), 0, 0, 255, NPC.scale);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<FeudalFungusTreasureBag>()));

            npcLoot.AddLoreItemDrop<FeudalFungus>(ModContent.ItemType<FeudalFungusLore>());

            LeadingConditionRule masterMode = new(new AAConditions.RevOrMaster());

            masterMode.OnSuccess(ItemDropRule.Common(ModContent.ItemType<FeudalFungusRelic>()));

            npcLoot.Add(masterMode);

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FeudalFungusTrophy>(), 10));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GlowingSporeBag>(), 1, 30, 35));

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<FeudalFungusMask>(), 7));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<GlowingMushium>(), 1, 25, 35));

            notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, [ModContent.ItemType<GlowingMushpick>(), ModContent.ItemType<GlowingMushmallet>()]));

            npcLoot.Add(notExpertRule);
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);  //boss life scale in expertmode
            NPC.damage = (int)(NPC.damage * 0.6f);
        }

        public void FungusAttack(int Attack)
        {
            if (Attack == 0)
            {
                if (NPC.CountNPCS(ModContent.NPCType<GlowingMushling>()) < 4)
                {
                    for (int i = 0; i < (Main.expertMode ? 3 : 2); i++)
                    {
                        NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<GlowingMushling>());
                    }
                }
                else
                {
                    float spread = 12f * 0.0174f;
                    double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y) - spread / 2;
                    double deltaAngle = spread / (Main.expertMode ? 5 : 4);
                    double offsetAngle;
                    for (int i = 0; i < (Main.expertMode ? 5 : 4); i++)
                    {
                        offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Sin(offsetAngle) * 6f), (float)(Math.Cos(offsetAngle) * 6f), ModContent.ProjectileType<FeudalFungus_FungusCloud>(), damage, 0, Main.myPlayer, 0f, 1f);
                    }
                }
            }
            else if (Attack == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<FeudalFungus_FungusFlier>());
                }
            }
            else if (Attack == 2)
            {
                float spread = 12f * 0.0174f;
                double startAngle = Math.Atan2(NPC.velocity.X, NPC.velocity.Y) - spread / 2;
                double deltaAngle = spread / (Main.expertMode ? 5 : 4);
                double offsetAngle;
                for (int i = 0; i < (Main.expertMode ? 5 : 4); i++)
                {
                    offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Sin(offsetAngle) * 6f), (float)(Math.Cos(offsetAngle) * 6f), ModContent.ProjectileType<FeudalFungus_FungusCloud>(), damage, 0, Main.myPlayer, 0f, 1f);
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<FeudalFungus_FungalSpore>(), 0, i);
                }
            }
        }

        public void MoveToPoint(Vector2 point, bool goUpFirst = false)
        {
            float moveSpeed = 4f;
            if (moveSpeed == 0f || NPC.Center == point) return; //don't move if you have no move speed
            float velMultiplier = 1f;
            Vector2 dist = point - NPC.Center;
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
            NPC.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            NPC.velocity *= moveSpeed;
            NPC.velocity *= velMultiplier;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D glowTex = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, NPC.IsABestiaryIconDummy ? Color.White : NPC.GetAlpha(drawColor), NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, SpriteEffects.None, 0);
            spriteBatch.Draw(glowTex, NPC.Center - screenPos, NPC.frame, AAColor.Glow, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, SpriteEffects.None, 0);
            return false;
        }
    }

    
}


