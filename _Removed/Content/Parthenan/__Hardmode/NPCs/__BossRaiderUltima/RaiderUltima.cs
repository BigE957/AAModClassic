using AAModClassic._Content.Inferno.___PreHardmode.Items._BossBroodmother.BossStandard;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Pets;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRaiderUltima.BossStandard;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRaiderUltima.Pets;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.CrossMod;
using AAModClassic.Items.Ranged;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.NPCs.__BossRaiderUltima
{

    [AutoloadBossHead]
    public class RaiderUltima : ModNPC
    {
        public static Asset<Texture2D> Glowmask1;
        public static Asset<Texture2D> Glowmask2;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Raider Ultima");
            Main.npcFrameCount[NPC.type] = 8;

            Glowmask1 = ModContent.Request<Texture2D>(Texture + "_Glow1");
            Glowmask2 = ModContent.Request<Texture2D>(Texture + "_Glow2");
        }

        public override void SetDefaults()
        {
            NPC.aiStyle = 0;
            NPC.width = 202;
            NPC.height = 196;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.chaseable = true;
            NPC.damage = 70;
            NPC.defense = 30;
            NPC.lifeMax = 30000;
            NPC.value = Item.sellPrice(0, 10, 0, 0);
            NPC.buffImmune[BuffID.Ichor] = true;
            NPC.lavaImmune = true;
            NPC.boss = true;
            NPC.netAlways = true;
            NPC.friendly = false;
            //TODO
            //NPC.HitSound = new LegacySoundStyle(3, 4, SoundType.Sound);
            //NPC.DeathSound = new LegacySoundStyle(4, 14, SoundType.Sound);
            //bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = Mod.Find<ModItem>("RaiderBag").Type;
            //Music = Mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Siege");
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
            if (Main.netMode == 2 || Main.dedServ)
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
            if (Main.netMode == 1)
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
            npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<RaiderUltimaTreasureBag>()));

            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<RaiderUltimaTrophy>(), 10));

            LeadingConditionRule notExpertRule = new(new Conditions.NotExpert());

            //notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<BroodmotherMask>(), 7));

            notExpertRule.OnSuccess(ItemDropRule.Common(ItemID.SoulofFright, 1, 20, 40));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<CyberneticEgg>(), 10));
            notExpertRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<FulguriteBar>(), 1, 30, 64));

            npcLoot.Add(notExpertRule);
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;   //boss drops
        }
        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);  //boss life scale in expertmode
            NPC.damage = (int)(NPC.damage * 0.8f);  //boss damage increase in expermode
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)          //this make so when the npc has 0 life(dead) he will spawn this
            {
                //TODO
                //Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/RaiderGore1"), 1f);
                //Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/RaiderGore2"), 1f);
                //Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/RaiderGore3"), 1f);
                //Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/RaiderGore4"), 1f);
                //Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/RaiderGore5"), 1f);
                //Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/RaiderGore6"), 1f);
                //Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/RaiderGore7"), 1f);
                //Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/RaiderGoreJaw"), 1f);
                //Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/RaiderGoreHorn"), 1f);
            }
        }

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            if (Main.rand.Next(2) == 0 || Main.expertMode && Main.rand.Next(0) == 0)       //Chances for it to inflict the debuff
            {
                target.AddBuff(BuffID.Electrified, Main.rand.Next(100, 180));       //Main.rand.Next part is the length of the buff, so 8.3 seconds to 16.6 seconds
            }
        }

        public int projectileInterval = 300; //how long until you fire projectiles
        private int projectileTimer = 0;
        public int ProjectileChoice = 0;

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter > 8)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += 192;
                bool isCharging = internalAI[1] == AISTATE_CHARGEATPLAYER; //all ai states between charges
                if (isCharging && (NPC.frame.Y >= 192 * 8 || NPC.frame.Y < 192 * 5))
                {
                    NPC.frame.Y = 192 * 4;
                }
                else
                if (!isCharging && NPC.frame.Y >= 192 * 4)
                {
                    NPC.frame.Y = 192 * 0;
                }
            }
        }

        public Color color;

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D glowTex = Glowmask1.Value;
            Texture2D glowTex1 = Glowmask2.Value;
            color = BaseUtility.MultiLerpColor(Main.player[Main.myPlayer].miscCounter % 100 / 100f, BaseDrawing.GetLightColor(NPC.position), BaseDrawing.GetLightColor(NPC.position), Color.Violet, BaseDrawing.GetLightColor(NPC.position), Color.Violet, BaseDrawing.GetLightColor(NPC.position));
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, drawColor);
            BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, NPC, color);
            BaseDrawing.DrawTexture(spriteBatch, glowTex1, 0, NPC, Color.White);
            return false;
        }

        int MaxMinions = Main.expertMode ? 8 : 5;
        private float pos = 250;
        public const float AISTATE_RUNAWAY = -1f, AISTATE_FLYABOVEPLAYER = 0f, AISTATE_ROCKETS = 1f, AISTATE_SHOCKBOMB = 2f, AISTATE_CHARGEATPLAYER = 3f, AISTATE_SPAWNEGGS = 4f;

        public override void AI()
        {
            Player player = Main.player[NPC.target];

            int Minions = NPC.CountNPCS(ModContent.NPCType<RaiderEgg>()) + NPC.CountNPCS(ModContent.NPCType<Raidmini>());
            color = BaseUtility.MultiLerpColor(Main.player[Main.myPlayer].miscCounter % 100 / 100f, BaseDrawing.GetLightColor(NPC.position), BaseDrawing.GetLightColor(NPC.position), Color.Violet, BaseDrawing.GetLightColor(NPC.position), Color.Violet, BaseDrawing.GetLightColor(NPC.position));

            Lighting.AddLight((int)(NPC.Center.X + NPC.width / 2) / 16, (int)(NPC.position.Y + NPC.height / 2) / 16, color.R / 255, color.G / 255, color.B / 255);

            NPC.TargetClosest();
            if (Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest();
                if (Main.player[NPC.target].dead || !Main.player[NPC.target].active)
                {
                    internalAI[1] = AISTATE_RUNAWAY;
                    NPC.ai = new float[4];
                    NPC.netUpdate = true;
                }
            }

            if (internalAI[1] == AISTATE_RUNAWAY)
            {
                NPC.noTileCollide = true;
                NPC.ai[1] = 0;
                NPC.ai[2] = 0;
                NPC.ai[3] = 0;
                internalAI[0]++;

                NPC.dontTakeDamage = true;

                if (NPC.timeLeft < 10)
                    NPC.timeLeft = 10;
                NPC.velocity.X *= 0.9f;

                if (internalAI[0] > 300)
                {
                    NPC.velocity.Y -= 4;
                    NPC.netUpdate = true;
                    if (NPC.position.Y + NPC.velocity.Y <= 0f && Main.netMode != 1) { BaseAI.KillNPC(NPC); NPC.netUpdate = true; }
                    return;
                }
                return;
            }

            if (Main.netMode != 1)
            {
                internalAI[0]++;
                if (internalAI[0] >= 180)
                {
                    internalAI[0] = 0;
                    internalAI[1] = Minions < MaxMinions ? Main.rand.Next(5) : Main.rand.Next(4);
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
                    if (internalAI[1] == AISTATE_CHARGEATPLAYER)
                    {
                        SelectPoint = true;
                        NPC.netUpdate = true;
                    }
                }
            }
            pos = NPC.ai[1] == 0 ? -250 : 250;

            if (Main.dayTime)
            {
                internalAI[1] = AISTATE_RUNAWAY;
                NPC.ai = new float[4];
                NPC.netUpdate = true;
            }

            Vector2 wantedVelocity = player.Center - new Vector2(pos, 250);
            MoveToPoint(wantedVelocity);

            if (Main.dayTime)
            {
                internalAI[1] = AISTATE_RUNAWAY;
                NPC.netUpdate = true;
            }

            if (internalAI[1] == AISTATE_ROCKETS)
            {
                if (Main.netMode != 1)
                {
                    internalAI[2]++;
                    if (!NPC.AnyNPCs(ModContent.NPCType<RaiderUltima_RaiderRocket>()))
                    {
                        for (int i = 0; i < (Main.expertMode ? 5 : 4); i++)
                        {
                            NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<RaiderUltima_RaiderRocket>(), 0);
                        }
                        NPC.netUpdate = true;
                    }
                    if (internalAI[2] > 90)
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
                if (Main.netMode != 1)
                {
                    projectileTimer++;
                    if (projectileTimer > 20)
                    {
                        projectileTimer = 0;
                        Vector2 firePos = new Vector2(NPC.Center.X + 32 * NPC.direction, NPC.Center.Y + 40f);
                        firePos = BaseUtility.RotateVector(NPC.Center, firePos, NPC.rotation); //+ (npc.direction == -1 ? (float)Math.PI : 0f)));
                        if (Minions < MaxMinions)
                        {
                            int NPCID = NPC.NewNPC( NPC.GetSource_FromThis(), (int)firePos.X, (int)firePos.Y, ModContent.NPCType<RaiderEgg>(), NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
                            Main.npc[NPCID].velocity.Y = 4f;
                            Main.npc[NPCID].netUpdate = true;
                        }
                        NPC.netUpdate = true;
                    }
                }
            }
            else if (internalAI[1] == AISTATE_CHARGEATPLAYER)
            {
                if (Main.netMode != 1)
                {
                    if (SelectPoint)
                    {
                        float Point = 500 * NPC.direction;
                        MovePoint = player.Center + new Vector2(Point, 500f);
                        SelectPoint = false;
                        internalAI[5] = 1;
                        NPC.netUpdate = true;
                    }
                }
                Charge(MovePoint);

                if (Vector2.Distance(NPC.Center, MovePoint) < 5)
                {
                    internalAI[0] = 0;
                    internalAI[1] = 0;
                    internalAI[2] = 0;
                    internalAI[5] = 0;
                    NPC.ai = new float[4];
                    NPC.netUpdate = true;
                }
            }
            else if (internalAI[1] == AISTATE_SHOCKBOMB)
            {
                if (Main.netMode != 1) //only fire bombs when (attempting to) fly above the player
                {
                    projectileTimer++;
                    if (projectileTimer >= projectileInterval && projectileTimer % 10 == 0)
                    {
                        if (projectileTimer > projectileInterval + 50)
                            projectileTimer = 0;
                        Vector2 dir = new Vector2(NPC.velocity.X * 2f + 4f * NPC.direction, NPC.velocity.Y * 0.5f + 1f);
                        Vector2 firePos = new Vector2(NPC.Center.X + 64 * NPC.direction, NPC.Center.Y + 28f);
                        firePos = BaseUtility.RotateVector(NPC.Center, firePos, NPC.rotation); //+ (npc.direction == -1 ? (float)Math.PI : 0f)));
                        int projID = Projectile.NewProjectile(NPC.GetSource_FromThis(), firePos, dir, ModContent.ProjectileType<RaiderUltima_RaidSphere>(), NPC.damage / (Main.expertMode ? 2 : 4), 1, 255);
                        Main.projectile[projID].netUpdate = true;
                    }
                }
            }
            if (internalAI[5] == 1 && Main.netMode != 1)
            {
                internalAI[5] = 2;
                NPC.netUpdate = true;
            }
            else if (internalAI[5] == 2 && Main.netMode != 1)
            {
                NPC.netUpdate = false;
            }
        }

        public Vector2 MovePoint;
        public bool SelectPoint = false;

        public void Charge(Vector2 point)
        {
            float MeleeSpeed = 18f;
            float velMultiplier = 1f;
            Vector2 dist = point - NPC.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < MeleeSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / MeleeSpeed);
            }
            if (length < 200f)
            {
                MeleeSpeed *= 0.5f;
            }
            if (length < 100f)
            {
                MeleeSpeed *= 0.5f;
            }
            if (length < 50f)
            {
                MeleeSpeed *= 0.5f;
            }
            NPC.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            NPC.velocity *= MeleeSpeed;
            NPC.velocity *= velMultiplier;
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