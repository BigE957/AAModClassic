using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

using AAMod.NPCs.Bosses.Yamata.Awakened;
using System.IO;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.NPCs.Bosses.Yamata
{
    [AutoloadBossHead]
    public class YamataHead : ModNPC
    {
        public int projDamage = 0;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Yamata");
            Main.npcFrameCount[NPC.type] = 3;
        }

        public override void SetDefaults()
        {
			NPC.lifeMax = 550000;
            NPC.damage = 90;
            NPC.defense = 100;
            NPC.width = 78;
            NPC.height = 60;
            NPC.npcSlots = 0;
            NPC.noTileCollide = true;
            NPC.noGravity = true;
            NPC.DeathSound = Mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/Sounds/YamataRoar");
            Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Yamata");
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public int varTime = 0;

        public int YvarOld = 0;

        public int XvarOld = 0;
        public int numberOfAttacks = 0;
        public int endAttack = 0;
        public int damage = 0;
        public float moveSpeedBoost = .04f;
        public NPC Body;
        public Yamata yamata = null;
        public bool HoriSwitch = false;
        public int f = 1;
        public float TargetDirection = (float)Math.PI / 2;
        public float s = 1;
        public static bool fireAttack;
        private int attackFrame;
        private int attackCounter;
        private int attackTimer;
        public int fireTimer = 0;
        public static bool EATTHELITTLEMAGGOT = false;
        public bool Quote1;
        public bool Quote2;
        public bool Quote3;
        public bool Quote4;
        public bool Quote5;
        public bool Quote6;
        public bool QuoteSaid;
        public static int HeadFrame = 0;

        public float[] internalAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
                writer.Write(EATTHELITTLEMAGGOT);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
                EATTHELITTLEMAGGOT = reader.ReadBool();
            }
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.damage = (int)(NPC.damage * .8f);
            NPC.lifeMax = (int)(NPC.lifeMax * 0.5f * bossLifeScale);
        }

        public override void AI()
        {
            int attackpower = 130;
            if (Main.expertMode)
            {
                damage = NPC.damage / 4;
            }
            else
            {
                damage = NPC.damage / 2;
            }
	        if (Body == null)
            {
                NPC npcBody = Main.npc[(int)NPC.ai[0]];
                if (npcBody.type == ModContent.NPCType<Yamata>() || npcBody.type == ModContent.NPCType<YamataA>())
                {
                    Body = npcBody;
					yamata = (Yamata)npcBody.ModNPC;
                }
            }
			if(Body == null)
				return;
            if (!Body.active)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) //force a kill to prevent 'ghost hands'
                {
                    NPC.life = 0;
                    NPC.checkDead();
                    NPC.netUpdate = true;
                }
                return;
            }

            NPC.realLife = Body.whoAmI;
			NPC.timeLeft = 100;
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];
            
            NPC.alpha = Body.alpha;
            if (NPC.alpha > 0)
            {
                NPC.damage = 0;
            }
            else
            {
                NPC.damage = attackpower;
            }

            Laugh();

            int roarSound = Mod.GetSoundSlot(SoundType.Item, "Sounds/Sounds/YamataRoar");

            Vector2 PlayerDistance = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
            float num433 = 6f;
            float PlayerPosX = Main.player[NPC.target].position.X + (Main.player[NPC.target].width / 2) - PlayerDistance.X;
            float PlayerPosY = Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2) - PlayerDistance.Y;
            float PlayerPos = (float)Math.Sqrt(PlayerPosX * PlayerPosX + PlayerPosY * PlayerPosY);
            PlayerPos = num433 / PlayerPos;
            PlayerPosX *= PlayerPos;
            PlayerPosY *= PlayerPos;
            PlayerPosY += Main.rand.Next(-40, 41) * 0.01f;
            PlayerPosX += Main.rand.Next(-40, 41) * 0.01f;
            PlayerPosY += NPC.velocity.Y * 0.5f;
            PlayerPosX += NPC.velocity.X * 0.5f;
            PlayerDistance.X -= PlayerPosX * 1f;
            PlayerDistance.Y -= PlayerPosY * 1f;

            if (NPC.alpha <= 0)
            {
                internalAI[2]++;
            }
            if (internalAI[2] == 399)
            {
                QuoteSaid = false;
                SoundEngine.PlaySound(roarSound, NPC.Center);
                internalAI[1] = Main.rand.Next(4);
            }

            if (internalAI[2] >= 400)
            {
                Attacks(internalAI[1]);
            }

            if (internalAI[2] >= 600)
            {
                EATTHELITTLEMAGGOT = false;
                internalAI[2] = 0;
            }

            if (NPC.ai[3] == 1)
            {
                attackCounter++;
                if (attackCounter > 10)
                {
                    attackFrame++;
                    attackCounter = 0;
                }
                if (attackFrame >= 3)
                {
                    attackFrame = 2;
                }
            }

            if (!player.active || player.dead || !Body.active)
            {
                NPC.TargetClosest(false);
                player = Main.player[NPC.target];
                if (!player.active || player.dead || !Body.active)
                {
                    if (NPC.timeLeft > 10)
                    {
                        NPC.timeLeft = 10;
                    }
                    return;
                }
            }
            fireTimer++;
            if (fireTimer >= 240 && NPC.ai[3] == 0)
            {
                SoundEngine.PlaySound(roarSound, NPC.Center);
                NPC.ai[3] = 1;
                fireTimer = 0;
            }
            projDamage = NPC.damage / 6;
            if (NPC.ai[3] == 1)
            {
                attackTimer++;
                if (Main.rand.Next(3) == 0)
                {
                    if (attackTimer == 40)
                    {
                        SoundEngine.PlaySound(SoundID.Item20, NPC.Center);
                        int proj2 = Projectile.NewProjectile(NPC.Center.X + Main.rand.Next(-20, 20), NPC.Center.Y + Main.rand.Next(-20, 20), NPC.velocity.X * 2f, NPC.velocity.Y * 2f, Mod.Find<ModProjectile>("YamataBomb").Type, projDamage, 0, Main.myPlayer);
                        Main.projectile[proj2].damage = projDamage;
                        attackTimer = 0;
                        attackFrame = 0;
                        attackCounter = 0;
                    }
                    if (attackTimer >= 80)
                    {
                        NPC.ai[3] = 0;
                    }
                }
                else
                {
                    if (attackTimer == 8 || attackTimer == 16 || attackTimer == 24 || attackTimer == 32 || attackTimer == 40 || attackTimer == 48 || attackTimer == 56 || attackTimer == 64 || attackTimer == 72 || attackTimer == 79)
                    {
                        SoundEngine.PlaySound(SoundID.Item20, NPC.Center);
                        for (int i = 0; i < 5; ++i)
                        {
                            if (Main.netMode != NetmodeID.MultiplayerClient)
                            {
                                Projectile.NewProjectile(PlayerDistance.X, PlayerDistance.Y, PlayerPosX * 2f, PlayerPosY * 2f, Mod.Find<ModProjectile>("YamataBreath").Type, projDamage, 0f, Main.myPlayer);
                            }
                        }
                        
                    }
                    if (attackTimer >= 80)
                    {
                        NPC.ai[3] = 0;
                        attackTimer = 0;
                        attackFrame = 0;
                        attackCounter = 0;
                    }
                }

            }

            Vector2 moveTo = new Vector2(Body.Center.X + NPC.ai[1], Body.Center.Y - (130f + NPC.ai[2])) - NPC.Center;
            NPC.velocity = moveTo * moveSpeedBoost;
            NPC.rotation = 0;
            NPC.position += Body.position - Body.oldPosition;

            if (Yamata.TeleportMeBitch)
            {
                Yamata.TeleportMeBitch = false;
                NPC.Center = yamata.NPC.Center;
                return;
            }
        }


        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public void Attacks(float AttackType)
        {
            Player player = Main.player[NPC.target];

            bool sayQuote = Main.rand.Next(3) == 0;
            if (AttackType == 0f)
            {
                if (!QuoteSaid && sayQuote)
                {
                    laughTimer = 120;
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat((!Quote1) ? Lang.BossChat("YamataHead1") : Lang.BossChat("YamataHead2"), new Color(45, 46, 70));
                    QuoteSaid = true;
                    Quote1 = true;
                }
                BaseAI.ShootPeriodic(NPC, new Vector2(player.position.X, player.position.Y - 1), player.width, player.height, ModContent.ProjectileType<YamataVenom>(), ref internalAI[3], 6, projDamage, 9f, true, new Vector2(20f, 15f));
            }
            if (AttackType == 1f)
            {
                if (!QuoteSaid && sayQuote)
                {
                    laughTimer = 120;
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat((!Quote3) ? Lang.BossChat("YamataHead3") : Lang.BossChat("YamataHead4"), new Color(45, 46, 70));
                    QuoteSaid = true;
                    Quote3 = true;
                }
                BaseAI.ShootPeriodic(NPC, new Vector2(player.position.X, -4f), player.width, player.height, ModContent.ProjectileType<YamataStorm>(), ref internalAI[3], 40, projDamage, 10f, true, new Vector2(20f, 15f));
            }
            if (AttackType == 2f)
            {
                if (!QuoteSaid && sayQuote)
                {
                    laughTimer = 120;
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat((!Quote3) ? Lang.BossChat("YamataHead5") : Lang.BossChat("YamataHead6"), new Color(45, 46, 70));
                    QuoteSaid = true;
                    Quote3 = true;
                }
                BaseAI.ShootPeriodic(NPC, new Vector2(player.position.X, player.position.Y - 1), player.width, player.height, ModContent.ProjectileType<YamataBlast>(), ref internalAI[3], 15, projDamage, 10f, true, new Vector2(20f, 15f));
            }
            if (AttackType == 3f)
            {
                if (!QuoteSaid && sayQuote)
                {
                    laughTimer = 120;
                    if (Main.netMode != NetmodeID.MultiplayerClient) AAMod.Chat((!Quote4) ? (Lang.BossChat("YamataHead7") + (player.Male ? Lang.BossChat("male2") : Lang.BossChat("fimale2")) + Lang.BossChat("YamataHead8")) : Lang.BossChat("YamataHead9"), new Color(45, 46, 70));
                    QuoteSaid = true;
                    Quote4 = true;
                }
                EATTHELITTLEMAGGOT = true;
            }
        }

        int laughTimer = 0;
        bool Laughing = false;

        public void Laugh()
        {
            if (laughTimer > 0 && !Laughing)
            {
                CombatText.NewText(NPC.getRect(), new Color(45, 46, 70), "NYEH", true, true);
                Laughing = true;
            }
            else if (laughTimer <= 0)
            {
                Laughing = false;
            }
            if (Laughing)
            {
                laughTimer--;
                if (laughTimer % 20 == 0 && laughTimer != 120)
                {
                    CombatText.NewText(NPC.getRect(), new Color(45, 46, 70), "HEH", true, true);
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.ai[3] == 1 || NPC.ai[2] >= 400)
            {
                if (NPC.frameCounter++ < 5)
                {
                    NPC.frame.Y = 1 * frameHeight;
                }
                else
                {
                    NPC.frame.Y = 2 * frameHeight;
                }
            }
            else
            {

                NPC.frame.Y = 0 * frameHeight;
                NPC.frameCounter = 0;
            }
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[NPC.target];
            if (player.vortexStealthActive && projectile.CountsAsClass(DamageClass.Ranged))
            {
                damage /= 2;
                crit = false;
            }
            if (projectile.penetrate == -1 && !projectile.minion)
            {
                damage = (int)(damage * .2f);
            }
            else if (projectile.penetrate >= 1)
            {
                projectile.damage *= (int).2;
            }
            else if (projectile.type == ProjectileID.LastPrismLaser)
            {
                damage = (int)(damage * .05f);
            }
        }

        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            if (projectile.type == ProjectileID.LastPrismLaser)
            {
                projectile.damage = (int)(projectile.damage * .01f);
            }
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }
        // We use this hook to prevent any loot from dropping. We do this because this is a multistage npc and it shouldn't drop anything until the final form is dead.
        public override bool PreKill()
        {
            return false;
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<Yamata>()) || NPC.AnyNPCs(ModContent.NPCType<YamataA>()))
            {
                return false;
            }
            return true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            return false;
        }
    }
}
