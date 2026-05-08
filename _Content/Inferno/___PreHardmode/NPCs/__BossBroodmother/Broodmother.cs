using AAModClassic._Content.Inferno.___PreHardmode.Items._BossBroodmother.BossStandard;
using AAModClassic._Content.Inferno.___PreHardmode.Items._BossBroodmother.Pets;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Pets;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Weapons;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.CrossMod;
using AAModClassic.Items.Ranged;
using AAModClassic.Music;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.NPCs.__BossBroodmother
{
    [AutoloadBossHead]
    public class Broodmother : ModNPC
    {
        public static Asset<Texture2D> Glowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Broodmother");
            Main.npcFrameCount[NPC.type] = 6;

            Glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
        }

        public override void SetDefaults()
        {
            NPC.aiStyle = NPCAIStyleID.FaceClosestPlayer;
            NPC.width = 130;
            NPC.height = 164;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.chaseable = true;
            NPC.damage = 35;
            Music = MusicManagementSystem.MusicSlots["Broodmother"];
            NPC.defense = 10;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.netAlways = true;
            NPC.friendly = false;
            NPC.lifeMax = 6000;
            NPC.value = Item.sellPrice(0, 5, 0, 0);
            NPC.behindTiles = true;
            NPC.knockBackResist = 0f;
            NPC.HitSound = SoundID.NPCHit6;
            NPC.DeathSound = SoundID.NPCDeath8;
            NPC.npcSlots = 200;
        }

        public int frame = 0;

        public int FrameTex = 0;

        public int damage = 0;

        public override void FindFrame(int frameHeight)
        {
            if (NPC.frameCounter++ > 3)
            {
                NPC.frame.Y += 296;
                NPC.frameCounter = 0;
                if (NPC.frame.Y >= 1776)
                {
                    NPC.frame.Y = 0;
                    FrameTex += 1;
                    if (FrameTex > 1)
                    {
                        FrameTex = 0;
                    }
                }
            }
            NPC.frame.Width = TextureAssets.Npc[NPC.type].Value.Width / 2;
            if (FrameTex >= 1)
                NPC.frame.X = TextureAssets.Npc[NPC.type].Value.Width / 2;
            else
                NPC.frame.X = 0;

        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public float[] internalAI = new float[6];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
                writer.Write(internalAI[4]);
                writer.Write(internalAI[5]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadSingle();
                internalAI[1] = reader.ReadSingle();
                internalAI[2] = reader.ReadSingle();
                internalAI[3] = reader.ReadSingle();
                internalAI[4] = reader.ReadSingle();
                internalAI[5] = reader.ReadSingle();
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<BroodmotherTreasureBag>()));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<BroodmotherTrophy>(), 10));

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<BroodmotherMask>(), 7));

            if (ContentReplacementSystem.NeedToReplaceContent)
                notExpertRule.OnSuccess(ItemDropRule.OneFromOptions(1, ModContent.ItemType<Pyrosphere>(), ModContent.ItemType<Firebuster>(), ModContent.ItemType<Volley>(), ModContent.ItemType<DragonSoul>(), ModContent.ItemType<DragonsGuard>()));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ScorchedEgg>(), 10));

            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<ScorchedScale>(), 1, 50, 75));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<IncineriteOre>(), 1, 75, 100));

            npcLoot.Add(notExpertRule);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            //BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 6, NPC.frame, drawColor, true);
            //BaseDrawing.DrawTexture(spriteBatch, Glowmask.Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 6, NPC.frame, ColorUtils.COLOR_GLOWPULSE, true);
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.SpriteEffectDirection(), 0f);
            spriteBatch.Draw(Glowmask.Value, NPC.Center - screenPos, NPC.frame, ColorUtils.COLOR_GLOWPULSE, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.SpriteEffectDirection(), 0f);
            return false;
        }

        public override void BossLoot(ref int potionType)
        {
            potionType = ItemID.HealingPotion;
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
            NPC.damage = (int)(NPC.damage * 0.6f);
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
			bool isDead = NPC.life <= 0;
            if (isDead)          //this make so when the npc has 0 life(dead) he will spawn this
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("BroodGoreBack").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("BroodGoreHand").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("BroodGoreHand").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("BroodGoreHead").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("BroodGorePlate1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("BroodGorePlate2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("BroodGorePlate3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("BroodGoreWingchunk1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("BroodGoreWingchunk2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("BroodGoreWingchunk3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("BroodGoreWingchunk4").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("BroodGoreWingchunk1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("BroodGoreWingchunk2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("BroodGoreWingchunk3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("BroodGoreWingchunk4").Type, 1f);
                /*
                for (int m = 0; m < 12; m++)
				{
					Vector2 offset = new Vector2(Main.rand.Next(NPC.width), Main.rand.Next(NPC.height));
					Gore.NewGore(NPC.GetSource_Death(), NPC.position + offset, NPC.velocity * 0.2f, Mod.Find<ModGore>("BroodGore3").Type, 1f + (float)Main.rand.NextDouble() * 0.5f);
				}
                */
            }
			for (int m = 0; m < (isDead ? 45 : 6); m++)
			{
				Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Torch, NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, Color.White, isDead? 3f : 1.5f);
			}	
        }

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            if (Main.rand.NextBool(2) || Main.expertMode && Main.rand.Next(0) == 0)       //Chances for it to inflict the debuff
            {
                target.AddBuff(BuffID.OnFire, Main.rand.Next(100, 180));       //Main.rand.Next part is the length of the buff, so 8.3 seconds to 16.6 seconds
            }
        }

		public int projectileInterval = 300; //how long until you fire projectiles
        private int projectileTimer = 0;
        private float pos = 250;
        private readonly int MaxMinions = Main.hardMode ? 4 : 3;
		public const float AISTATE_RUNAWAY = -1f, AISTATE_FLYABOVEPLAYER = 0f, AISTATE_FIREBREATH = 1f, AISTATE_FIREBOMB = 2f, AISTATE_SPAWNEGGS = 3f;

		public override void AI()
        {
            if (Main.expertMode)
            {
                damage = NPC.damage / 4;
            }
            else
            {
                damage = NPC.damage / 2;
            }
            if (internalAI[1] == AISTATE_RUNAWAY)
            {
                NPC.noTileCollide = true;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = 0;
                internalAI[0]++;

                if (NPC.timeLeft < 10)
                    NPC.timeLeft = 10;
                NPC.velocity.X *= 0.9f;

                if (internalAI[0] > 300)
                {
                    NPC.velocity.Y -= 0.1f;
                    if (NPC.velocity.Y > 15f) NPC.velocity.Y = 15f;
                    NPC.rotation = 0f;
                    if(NPC.position.Y + NPC.velocity.Y <= 0f && Main.netMode != NetmodeID.MultiplayerClient) { BaseAI.KillNPC(NPC); NPC.netUpdate = true; }
                }
                return;
            }

            int Minions = NPC.CountNPCS(ModContent.NPCType<DragonEgg>()) + NPC.CountNPCS(ModContent.NPCType<Broodmini>());

            if (Main.netMode != NetmodeID.MultiplayerClient && internalAI[0]++ >= 120)
            {
                internalAI[0] = 0;
                internalAI[1] = Minions < MaxMinions ? Main.rand.Next(4) : Main.rand.Next(3);
                NPC.ai = new float[4];
                if (internalAI[1] == AISTATE_FLYABOVEPLAYER)
                {
                    NPC.ai[1] = 1 + Main.rand.Next(2);
                }
                else
                if (internalAI[1] == AISTATE_SPAWNEGGS)
                {
                    NPC.ai[1] = NPC.ai[1] == 0 ? 1 : 0;
                }
                NPC.netUpdate = true;
            }
            pos = NPC.ai[1] == 0 ? -250 : 250;

            if (Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) > 4000f || Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 4000f)
            {
                NPC.active = false;
            }

            if (!Main.player[NPC.target].ZoneAnyInferno())
            {
                NPC.dontTakeDamage = true;
                NPC.damage = 130;
            }
            else
            {
                NPC.dontTakeDamage = false;
                NPC.damage = NPC.defDamage;
            }

            NPC.TargetClosest();
            Player player = Main.player[NPC.target];
            if (internalAI[1] != AISTATE_RUNAWAY)
            {
                if (!Main.dayTime)
                {
                    internalAI[1] = AISTATE_RUNAWAY;
                    NPC.ai = new float[4];
                    return;
                }
                if (player.dead || !player.active)
                {
                    NPC.TargetClosest();
                    if (player.dead || !player.active)
                    {
                        internalAI[1] = AISTATE_RUNAWAY;
                        NPC.ai = new float[4];
                        return;
                    }
                }
            }

            Vector2 wantedVelocity = player.Center - new Vector2(pos, 250);
            MoveToPoint(wantedVelocity);

            if (internalAI[1] == AISTATE_FIREBREATH)
            {
                NPC.localAI[2] += 1f;
                if (NPC.localAI[2] > 22f)
                {
                    NPC.localAI[2] = 0f;
                    SoundEngine.PlaySound(SoundID.Item34, NPC.position);
                }
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    internalAI[2]++;
                    if (internalAI[2] > 10f)
                    {
                        if(Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
                        {
                            BaseAI.ShootPeriodic(NPC, player.position, player.width, player.height, ModContent.ProjectileType<Broodmother_FireBreath>(), ref internalAI[3], 5, damage, 12, true, new Vector2(0, 40f));
                        }
                        else
                        {
                            int j = (int) NPC.position.Y / 16;
                            int i = (int) player.position.Y / 16;
                            if(i > j && internalAI[2] % 90 == 0 && Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                for(int index = -2; index < 2; index++)
                                {
                                    for(int loop = i; loop > j; loop--)
                                    {
                                        if(Main.tile[(int) player.position.X / 16 + index * 20, loop].HasTile && Main.tileSolid[Main.tile[(int) player.position.X / 16 + index * 10, loop].TileType] && (Main.tile[(int) player.position.X / 16 + index * 20, loop + 1].HasTile || !Main.tileSolid[Main.tile[(int) player.position.X / 16 + index * 20, loop + 1].TileType]))
                                        {
                                            int id = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position.X + index * 320, loop * 16, 0, 12f, ProjectileID.GeyserTrap, damage, 0, Main.myPlayer, 0f, 0f);
                                            Main.projectile[id].hostile = true;
                                            Main.projectile[id].friendly = false;
                                            break;
                                        }
                                    }
                                    for(int loop = i + 20; loop > j; loop--)
                                    {
                                        if(Main.tile[(int) player.position.X / 16 + index * 20 - 10, loop].HasTile && Main.tileSolid[Main.tile[(int) player.position.X / 16 + index * 10 - 10, loop].TileType] && (Main.tile[(int) player.position.X / 16 + index * 20 - 10, loop - 1].HasTile || !Main.tileSolid[Main.tile[(int) player.position.X / 16 + index * 20 - 10, loop - 1].TileType]))
                                        {
                                            int id = Projectile.NewProjectile(NPC.GetSource_FromThis(), player.position.X + index * 320 - 160, loop * 16, 0, -12f, ProjectileID.GeyserTrap, damage, 0, Main.myPlayer, 0f, 0f);
                                            Main.projectile[id].hostile = true;
                                            Main.projectile[id].friendly = false;
                                            break;
                                        }
                                    }
                                }
                            }

                        }
                    }
                    if (internalAI[2] > 180)
                    {
                        internalAI[0] = 0;
                        internalAI[1] = 0;
                        internalAI[2] = 0;
                        NPC.ai = new float[4];
                        NPC.netUpdate = true;
                    }
                }
            }
            else if (internalAI[1] == AISTATE_SPAWNEGGS)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    projectileTimer++;
                    if (projectileTimer >= projectileInterval && projectileTimer % 20 == 0)
                    {
                        if (projectileTimer > projectileInterval + 60)
                            projectileTimer = 0;
                        Vector2 firePos = new Vector2(NPC.Center.X + 32 * NPC.direction, NPC.Center.Y + 40f);
                        firePos = BaseUtility.RotateVector(NPC.Center, firePos, NPC.rotation); //+ (npc.direction == -1 ? (float)Math.PI : 0f)));
                        if (Minions < MaxMinions)
                        {
                            int NPCID = NPC.NewNPC(NPC.GetSource_FromThis(), (int)firePos.X, (int)firePos.Y, ModContent.NPCType<DragonEgg>(), NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
                            Main.npc[NPCID].velocity.Y = 4f;
                            Main.npc[NPCID].netUpdate = true;
                        }
                    }
                }
            }
            else if (internalAI[1] == AISTATE_FIREBOMB)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) //only fire bombs when (attempting to) fly above the player
                {
                    projectileTimer++;
                    if (projectileTimer >= projectileInterval && projectileTimer % 10 == 0)
                    {
                        if (projectileTimer > projectileInterval + 50)
                            projectileTimer = 0;
                        Vector2 dir = new Vector2(NPC.velocity.X * 2f + 4f * NPC.direction, NPC.velocity.Y * 0.5f + 1f);
                        Vector2 firePos = new Vector2(NPC.Center.X + 64 * NPC.direction, NPC.Center.Y + 10f);
                        firePos = BaseUtility.RotateVector(NPC.Center, firePos, NPC.rotation); //+ (npc.direction == -1 ? (float)Math.PI : 0f)));
                        int projID = Projectile.NewProjectile(NPC.GetSource_FromThis(), firePos, dir, ModContent.ProjectileType<Broodmother_MagmaBall>(), damage, 1, 255);
                        Main.projectile[projID].netUpdate = true;
                    }
                }
            }
        }

		public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
            spriteEffects = NPC.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 9f;
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
    }
}