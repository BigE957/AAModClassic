using System;
using System.IO;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Greed
{
    [AutoloadBossHead]
	public class Greed : ModNPC
	{
        public int damage = 0;
        bool loludided = false;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Greed");
            Main.npcFrameCount[NPC.type] = 3;
		}

		public override void SetDefaults()
		{
			NPC.npcSlots = 5f;
            NPC.width = 38;
            NPC.height = 38;
            NPC.damage = 35;
            NPC.defense = 25;
            NPC.lifeMax = 50000;
            NPC.value = Item.buyPrice(0, 5, 0, 0);
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.behindTiles = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.Tink;
            NPC.DeathSound = SoundID.Item14;
            NPC.netAlways = true;
            NPC.boss = true;
            Music = Mod.GetSoundSlot(Terraria.Audio.SoundType.Music, "Sounds/Music/Greed");
            NPC.alpha = 255;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
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
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
                internalAI[4] = reader.ReadFloat();
                internalAI[5] = reader.ReadFloat();
            }
        }

        public override bool PreAI()
        {
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];
            if (Main.expertMode)
            {
                damage = NPC.damage / 4;
            }
            else
            {
                damage = NPC.damage / 2;
            }

            if (NPC.alpha <= 0)
            {
                NPC.alpha = 0;
            }
            else
            {
                NPC.alpha -= 3;
                if (NPC.alpha != 0)
                {
                    for (int spawnDust = 0; spawnDust < 4; spawnDust++)
                    {
                        int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.GoldCoin, 0f, 0f, 100, default, 2f);
                        Main.dust[num935].noGravity = true;
                        Main.dust[num935].noLight = true;
                    }
                }
            }


            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                internalAI[2]++;
                internalAI[3]++;
                if (internalAI[2] >= 260)
                {
                    MinionSummon();
                    internalAI[2] = 0;
                    NPC.netUpdate = true;
                }
                if (internalAI[3] == 340)
                {
                    internalAI[5] = Main.rand.Next(2);
                    NPC.netUpdate = true;
                }
                if (internalAI[3] > 540)
                {
                    internalAI[3] = 0;
                    NPC.netUpdate = true;
                }
            }

            if (internalAI[3] >= 340)
            {
                if (internalAI[5] == 0)
                {
                    BaseAI.ShootPeriodic(NPC, player.position, player.width, player.height, ModContent.ProjectileType<GreedCoin>(), ref internalAI[4], 30, NPC.damage / 4, 10, false);
                }
                else
                {

                }
            }

            if (!Main.gamePaused && Main.rand.Next(60) == 0 && Main.LocalPlayer.findTreasure)
            {
                int num52 = Dust.NewDust(NPC.Center, 16, 16, DustID.TreasureSparkle, 0f, 0f, 150, default, 0.3f);
                Main.dust[num52].fadeIn = 1f;
                Main.dust[num52].velocity *= 0.1f;
                Main.dust[num52].noLight = true;
            }

            NPC.spriteDirection = NPC.velocity.X > 0 ? -1 : 1;
            NPC.ai[1]++;
            if (NPC.ai[1] >= 1200)
                NPC.ai[1] = 0;
            NPC.TargetClosest(true);
            if (!Main.player[NPC.target].active || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
                if (!Main.player[NPC.target].active || Main.player[NPC.target].dead)
                {
                    NPC.ai[3]++;
                    NPC.velocity.Y = NPC.velocity.Y + 0.11f;
                    if (NPC.ai[3] >= 300)
                    {
                        NPC.active = false;
                    }
                }
                else
                    NPC.ai[3] = 0;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.ai[0] == 0)
                {
                    NPC.realLife = NPC.whoAmI;
                    int latestNPC = NPC.whoAmI;

                    for (int i = 0; i < 24; ++i)
                    {
                        latestNPC = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("GreedBody").Type, NPC.whoAmI, 0, latestNPC);
                        Main.npc[latestNPC].realLife = NPC.whoAmI;
                        Main.npc[latestNPC].ai[2] = i;
                        Main.npc[latestNPC].ai[3] = NPC.whoAmI;
                    }

                    NPC.ai[0] = 1;
                    NPC.netUpdate = true;
                }
            }

            bool collision = true;

            float speed = 16f;
            float acceleration = 0.12f;

            Vector2 npcCenter = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
            float targetXPos = Main.player[NPC.target].position.X + (Main.player[NPC.target].width / 2);
            float targetYPos = Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2);

            float targetRoundedPosX = (int)(targetXPos / 16.0) * 16;
            float targetRoundedPosY = (int)(targetYPos / 16.0) * 16;
            npcCenter.X = (int)(npcCenter.X / 16.0) * 16;
            npcCenter.Y = (int)(npcCenter.Y / 16.0) * 16;
            float dirX = targetRoundedPosX - npcCenter.X;
            float dirY = targetRoundedPosY - npcCenter.Y;

            float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
            if (!collision)
            {
                NPC.TargetClosest(true);
                NPC.velocity.Y = NPC.velocity.Y + 0.11f;
                if (NPC.velocity.Y > speed)
                    NPC.velocity.Y = speed;
                if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < speed * 0.4)
                {
                    if (NPC.velocity.X < 0.0)
                        NPC.velocity.X = NPC.velocity.X - acceleration * 1.1f;
                    else
                        NPC.velocity.X = NPC.velocity.X + acceleration * 1.1f;
                }
                else if (NPC.velocity.Y == speed)
                {
                    if (NPC.velocity.X < dirX)
                        NPC.velocity.X = NPC.velocity.X + acceleration;
                    else if (NPC.velocity.X > dirX)
                        NPC.velocity.X = NPC.velocity.X - acceleration;
                }
                else if (NPC.velocity.Y > 4.0)
                {
                    if (NPC.velocity.X < 0.0)
                        NPC.velocity.X = NPC.velocity.X + acceleration * 0.9f;
                    else
                        NPC.velocity.X = NPC.velocity.X - acceleration * 0.9f;
                }
            }
            else
            {
                if (NPC.soundDelay == 0)
                {
                    float num1 = length / 40f;
                    if (num1 < 10.0)
                        num1 = 10f;
                    if (num1 > 20.0)
                        num1 = 20f;
                    NPC.soundDelay = (int)num1;
                }
                float absDirX = Math.Abs(dirX);
                float absDirY = Math.Abs(dirY);
                float newSpeed = speed / length;
                dirX *= newSpeed;
                dirY *= newSpeed;
                if (NPC.velocity.X > 0.0 && dirX > 0.0 || NPC.velocity.X < 0.0 && dirX < 0.0 || NPC.velocity.Y > 0.0 && dirY > 0.0 || NPC.velocity.Y < 0.0 && dirY < 0.0)
                {
                    if (NPC.velocity.X < dirX)
                        NPC.velocity.X = NPC.velocity.X + acceleration;
                    else if (NPC.velocity.X > dirX)
                        NPC.velocity.X = NPC.velocity.X - acceleration;
                    if (NPC.velocity.Y < dirY)
                        NPC.velocity.Y = NPC.velocity.Y + acceleration;
                    else if (NPC.velocity.Y > dirY)
                        NPC.velocity.Y = NPC.velocity.Y - acceleration;
                    if (Math.Abs(dirY) < speed * 0.2 && (NPC.velocity.X > 0.0 && dirX < 0.0 || NPC.velocity.X < 0.0 && dirX > 0.0))
                    {
                        if (NPC.velocity.Y > 0.0)
                            NPC.velocity.Y = NPC.velocity.Y + acceleration * 2f;
                        else
                            NPC.velocity.Y = NPC.velocity.Y - acceleration * 2f;
                    }
                    if (Math.Abs(dirX) < speed * 0.2 && (NPC.velocity.Y > 0.0 && dirY < 0.0 || NPC.velocity.Y < 0.0 && dirY > 0.0))
                    {
                        if (NPC.velocity.X > 0.0)
                            NPC.velocity.X = NPC.velocity.X + acceleration * 2f;
                        else
                            NPC.velocity.X = NPC.velocity.X - acceleration * 2f;
                    }
                }
                else if (absDirX > absDirY)
                {
                    if (NPC.velocity.X < dirX)
                        NPC.velocity.X = NPC.velocity.X + acceleration * 1.1f;
                    else if (NPC.velocity.X > dirX)
                        NPC.velocity.X = NPC.velocity.X - acceleration * 1.1f;
                    if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < speed * 0.5)
                    {
                        if (NPC.velocity.Y > 0.0)
                            NPC.velocity.Y = NPC.velocity.Y + acceleration;
                        else
                            NPC.velocity.Y = NPC.velocity.Y - acceleration;
                    }
                }
                else
                {
                    if (NPC.velocity.Y < dirY)
                        NPC.velocity.Y = NPC.velocity.Y + acceleration * 1.1f;
                    else if (NPC.velocity.Y > dirY)
                        NPC.velocity.Y = NPC.velocity.Y - acceleration * 1.1f;
                    if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < speed * 0.5)
                    {
                        if (NPC.velocity.X > 0.0)
                            NPC.velocity.X = NPC.velocity.X + acceleration;
                        else
                            NPC.velocity.X = NPC.velocity.X - acceleration;
                    }
                }
            }
            NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;

            int tileX = (int)(NPC.position.X / 16f) - 1;
            int tileCenterX = (int)(NPC.Center.X / 16f) + 2;
            int tileY = (int)(NPC.position.Y / 16f) - 1;
            int tileCenterY = (int)(NPC.Center.Y / 16f) + 2;
            if (tileX < 0) { tileX = 0; }
            if (tileCenterX > Main.maxTilesX) { tileCenterX = Main.maxTilesX; }
            if (tileY < 0) { tileY = 0; }
            if (tileCenterY > Main.maxTilesY) { tileCenterY = Main.maxTilesY; }
            for (int tX = tileX; tX < tileCenterX; tX++)
            {
                for (int tY = tileY; tY < tileCenterY; tY++)
                {
                    Tile checkTile = BaseWorldGen.GetTileSafely(tX, tY);
                    if (checkTile != null && ((checkTile.HasUnactuatedTile && (Main.tileSolid[checkTile.TileType] || (Main.tileSolidTop[checkTile.TileType] && checkTile.TileFrameY == 0))) || checkTile.LiquidAmount > 64))
                    {
                        Vector2 tPos;
                        tPos.X = tX * 16;
                        tPos.Y = tY * 16;
                        if (NPC.position.X + NPC.width > tPos.X && NPC.position.X < tPos.X + 16f && NPC.position.Y + NPC.height > tPos.Y && NPC.position.Y < tPos.Y + 16f)
                        {
                            if (Main.rand.Next(100) == 0 && checkTile.HasUnactuatedTile)
                            {
                                WorldGen.KillTile(tX, tY, true, true, false);
                            }
                        }
                    }
                }
            }

            if (player.position.Y < (Main.worldSurface * 16.0))
            {
                if (loludided == false)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("GreedFalse1"), Color.Goldenrod);
                    loludided = true;
                }
                NPC.velocity.Y = NPC.velocity.Y + 1f;
                if (NPC.position.Y - NPC.height - NPC.velocity.Y >= Main.maxTilesY && Main.netMode != NetmodeID.MultiplayerClient) { BaseAI.KillNPC(NPC); NPC.netUpdate2 = true; }
            }

            if (Main.player[NPC.target].dead || Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) > 6000f || Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 6000f)
            {
                if (loludided == false)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("GreedFalse2"), Color.Goldenrod);
                    loludided = true;
                }
                NPC.velocity.Y = NPC.velocity.Y - 1f;
                if (NPC.position.Y < 0)
                {
                    NPC.velocity.Y = NPC.velocity.Y - 1f;
                }
                if (NPC.position.Y < 0)
                {
                    for (int num957 = 0; num957 < 200; num957++)
                    {
                        if (Main.npc[num957].aiStyle == NPC.aiStyle)
                        {
                            Main.npc[num957].active = false;
                        }
                    }
                }
            }

            if (collision)
            {
                if (NPC.localAI[0] != 1)
                    NPC.netUpdate = true;
                NPC.localAI[0] = 1f;
            }
            else
            {
                if (NPC.localAI[0] != 0.0)
                    NPC.netUpdate = true;
                NPC.localAI[0] = 0.0f;
            }
            if ((NPC.velocity.X > 0.0 && NPC.oldVelocity.X < 0.0 || NPC.velocity.X < 0.0 && NPC.oldVelocity.X > 0.0 || NPC.velocity.Y > 0.0 && NPC.oldVelocity.Y < 0.0 || NPC.velocity.Y < 0.0 && NPC.oldVelocity.Y > 0.0) && !NPC.justHit)
                NPC.netUpdate = true;

            return false;
        }

        public bool truehit = false;

        public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            timer = 40;
            if (item.pick > 0)
            {
                NPC.StrikeNPC(NPC.CalculateHitInfo(damage + item.pick, player.direction, true, hit.Knockback));
                truehit = true;
            }
        }

        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            timer = 40;
        }

        int timer = 0;

        public override void FindFrame(int frameHeight)
        {
            if (timer > 0)
            {
                timer--;
            }
            else
            {
                timer = 0;
            }
            if (NPC.type == ModContent.NPCType<GreedBody>())
            {
                NPC.frame.Y = frameHeight * (int)NPC.ai[2];
            }
            if (NPC.type == ModContent.NPCType<Greed>())
            {
                if (timer > 0)
                {
                    NPC.frame.Y = frameHeight;
                }
                else
                {
                    NPC.frame.Y = 0;
                }
            }
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;   //boss drops
            AAWorld.downedSerpent = true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            if (NPC.type != ModContent.NPCType<Greed>())
            {
                return false;
            }
            scale = 1.5f;
            return true;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.75f * balance);
			NPC.damage = (int)(NPC.damage * 0.85f);
		}

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Dirt, hit.HitDirection, -1f, 0, default, 1f);
            }
            if (NPC.life == 0)
            {
                for (int k = 0; k < 5; k++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Dirt, hit.HitDirection, -1f, 0, default, 1f);
                }
            }
        }

        public override void OnKill()
        {
            AAWorld.downedGreed = true;
            if (NPC.downedMoonlord)
            {
                int a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<GreedTransition>());
                Main.npc[a].Center = NPC.Center;
                return;
            }
            else
            {
                if (!Main.expertMode)
                {
                    if (Main.rand.Next(7) == 0)
                    {
                        NPC.DropLoot(Mod.Find<ModItem>("GreedMask").Type);
                    }
                    Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("StoneShell").Type, Main.rand.Next(20, 25));
                    string[] lootTable = { "GildedGlock", "GoldDigger", "Miner" };
                    int loot = Main.rand.Next(lootTable.Length);
                    NPC.DropLoot(Mod.Find<ModItem>(lootTable[loot]).Type);
                }
                if (Main.expertMode)
                {
                    NPC.DropLoot(Mod.Find<ModItem>("GreedBag").Type);
                }
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("GreedTrophy").Type);
            }
            NPC.value = 0f;
            NPC.boss = false;
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;

            NPC.position.Y += NPC.height * 0.5f;

            BaseDrawing.DrawTexture(spriteBatch, texture, 0, NPC, drawColor);

            NPC.position.Y -= NPC.height * 0.5f;

            return false;
        }

        public void MinionSummon()
        {
            int Xint = Main.rand.Next(-400, 400);
            int Yint = Main.rand.Next(-400, 400);
            int MinionChoice = Main.rand.Next(11);
            if (MinionChoice == 0)
            {
                int a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, 0);
                Main.npc[a].Center = new Vector2(NPC.Center.X + Xint, NPC.Center.Y + Yint);
                Xint = Main.rand.Next(-400, 400);
                Yint = Main.rand.Next(-400, 400);
                a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, 2);
                Main.npc[a].Center = new Vector2(NPC.Center.X + Xint, NPC.Center.Y + Yint);
                Xint = Main.rand.Next(-400, 400);
                Yint = Main.rand.Next(-400, 400);
                a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, 4);
                Main.npc[a].Center = new Vector2(NPC.Center.X + Xint, NPC.Center.Y + Yint);
                Xint = Main.rand.Next(-400, 400);
                Yint = Main.rand.Next(-400, 400);
                a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, 6);
                Main.npc[a].Center = new Vector2(NPC.Center.X + Xint, NPC.Center.Y + Yint);
            }
            else if (MinionChoice == 1)
            {
                int a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, 1);
                Main.npc[a].Center = new Vector2(NPC.Center.X + Xint, NPC.Center.Y + Yint);
                Xint = Main.rand.Next(-400, 400);
                Yint = Main.rand.Next(-400, 400);
                a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, 3);
                Main.npc[a].Center = new Vector2(NPC.Center.X + Xint, NPC.Center.Y + Yint);
                Xint = Main.rand.Next(-400, 400);
                Yint = Main.rand.Next(-400, 400);
                a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, 5);
                Main.npc[a].Center = new Vector2(NPC.Center.X + Xint, NPC.Center.Y + Yint);
                Xint = Main.rand.Next(-400, 400);
                Yint = Main.rand.Next(-400, 400);
                a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, 7);
                Main.npc[a].Center = new Vector2(NPC.Center.X + Xint, NPC.Center.Y + Yint);
            }
            else if (MinionChoice == 2)
            {
                int a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, 8);
                Main.npc[a].Center = new Vector2(NPC.Center.X + Xint, NPC.Center.Y + Yint);
                Xint = Main.rand.Next(-400, 400);
                Yint = Main.rand.Next(-400, 400);
                a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, 9);
                Main.npc[a].Center = new Vector2(NPC.Center.X + Xint, NPC.Center.Y + Yint);
            }
            else if (MinionChoice == 3)
            {
                int a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, 10);
                Main.npc[a].Center = new Vector2(NPC.Center.X + Xint, NPC.Center.Y + Yint);
                Xint = Main.rand.Next(-400, 400);
                Yint = Main.rand.Next(-400, 400);
                a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, 11);
                Main.npc[a].Center = new Vector2(NPC.Center.X + Xint, NPC.Center.Y + Yint);
            }
            else if (MinionChoice == 4)
            {
                int a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, 12);
                Main.npc[a].Center = new Vector2(NPC.Center.X + Xint, NPC.Center.Y + Yint);
                Xint = Main.rand.Next(-400, 400);
                Yint = Main.rand.Next(-400, 400);
                a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, 13);
                Main.npc[a].Center = new Vector2(NPC.Center.X + Xint, NPC.Center.Y + Yint);
            }
            else if (MinionChoice == 5)
            {
                int a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, 14);
                Main.npc[a].Center = new Vector2(NPC.Center.X + Xint, NPC.Center.Y + Yint);
                Xint = Main.rand.Next(-400, 400);
                Yint = Main.rand.Next(-400, 400);
                a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, 16);
                Main.npc[a].Center = new Vector2(NPC.Center.X + Xint, NPC.Center.Y + Yint);
                Xint = Main.rand.Next(-400, 400);
                Yint = Main.rand.Next(-400, 400);
                a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, 18);
                Main.npc[a].Center = new Vector2(NPC.Center.X + Xint, NPC.Center.Y + Yint);
            }
            else if (MinionChoice == 6)
            {
                int a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, 15);
                Main.npc[a].Center = new Vector2(NPC.Center.X + Xint, NPC.Center.Y + Yint);
                Xint = Main.rand.Next(-400, 400);
                Yint = Main.rand.Next(-400, 400);
                a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, 17);
                Main.npc[a].Center = new Vector2(NPC.Center.X + Xint, NPC.Center.Y + Yint);
                Xint = Main.rand.Next(-400, 400);
                Yint = Main.rand.Next(-400, 400);
                a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, 19);
                Main.npc[a].Center = new Vector2(NPC.Center.X + Xint, NPC.Center.Y + Yint);
            }
            else if (MinionChoice == 7)
            {
                int a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, 20);
                Main.npc[a].Center = new Vector2(NPC.Center.X + Xint, NPC.Center.Y + Yint);
                Xint = Main.rand.Next(-400, 400);
                Yint = Main.rand.Next(-400, 400);
                a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, 20);
                Main.npc[a].Center = new Vector2(NPC.Center.X + Xint, NPC.Center.Y + Yint);
            }
            else if (MinionChoice == 8)
            {
                int a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, 21);
                Main.npc[a].Center = new Vector2(NPC.Center.X + Xint, NPC.Center.Y + Yint);
                Xint = Main.rand.Next(-400, 400);
                Yint = Main.rand.Next(-400, 400);
                a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, 21);
                Main.npc[a].Center = new Vector2(NPC.Center.X + Xint, NPC.Center.Y + Yint);
            }
            else if (MinionChoice == 9)
            {
                int a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, 22);
                Main.npc[a].Center = new Vector2(NPC.Center.X + Xint, NPC.Center.Y + Yint);
            }
            else if (MinionChoice == 10)
            {
                int a = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, 23);
                Main.npc[a].Center = new Vector2(NPC.Center.X + Xint, NPC.Center.Y + Yint);
            }
        }
    }

    [AutoloadBossHead]
    public class GreedBody : Greed
    {
        public override string Texture { get { return "AAModClassic/NPCs/Bosses/Greed/GreedBody"; } }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Greed");
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
            Main.npcFrameCount[NPC.type] = 22;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.dontCountMe = true;
            NPC.alpha = 255;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override bool PreKill()
        {
            return false;
        }

        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            if(!truehit)
            {
                modifiers.TargetDamageMultiplier *= .3f;
            }
            else
            {
                truehit = false;
            }
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
		{
            if (projectile.type == Mod.Find<ModProjectile>("OreChunk").Type && projectile.ai[1] == ItemID.GoldOre && NPC.ai[2] == 6)
            {
                damage += (int)(NPC.defense * (Main.expertMode? .75 : .5f));
                truehit = true;
            }
		}

        public override bool PreAI()
        {
            NPC.defense = Def();
            Vector2 chasePosition = Main.npc[(int)NPC.ai[1]].Center;
            Vector2 directionVector = chasePosition - NPC.Center;
            NPC.spriteDirection = (directionVector.X > 0f) ? 1 : -1;
            if (NPC.ai[3] > 0)
                NPC.realLife = (int)NPC.ai[3];
            if (NPC.target < 0 || NPC.target == byte.MaxValue || Main.player[NPC.target].dead)
                NPC.TargetClosest(true);
            if (Main.player[NPC.target].dead && NPC.timeLeft > 300)
                NPC.timeLeft = 300;

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!Main.npc[(int)NPC.ai[3]].active || Main.npc[(int)NPC.ai[3]].type != Mod.Find<ModNPC>("Greed").Type)
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 10.0);
                    NPC.active = false;
                    NetMessage.SendData(MessageID.DamageNPC, -1, -1, null, NPC.whoAmI, -1f, 0.0f, 0.0f, 0, 0, 0);
                }
            }

            if (NPC.ai[1] < (double)Main.npc.Length)
            {
                Vector2 npcCenter = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                float dirX = Main.npc[(int)NPC.ai[1]].position.X + Main.npc[(int)NPC.ai[1]].width / 2 - npcCenter.X;
                float dirY = Main.npc[(int)NPC.ai[1]].position.Y + Main.npc[(int)NPC.ai[1]].height / 2 - npcCenter.Y;
                NPC.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                float dist = (length - NPC.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;

                if (dirX < 0f)
                {
                    NPC.spriteDirection = 1;

                }
                else
                {
                    NPC.spriteDirection = -1;
                }

                NPC.velocity = Vector2.Zero;
                NPC.position.X = NPC.position.X + posX;
                NPC.position.Y = NPC.position.Y + posY;
            }

            int tileX = (int)(NPC.position.X / 16f) - 1;
            int tileCenterX = (int)(NPC.Center.X / 16f) + 2;
            int tileY = (int)(NPC.position.Y / 16f) - 1;
            int tileCenterY = (int)(NPC.Center.Y / 16f) + 2;
            if (tileX < 0) { tileX = 0; }
            if (tileCenterX > Main.maxTilesX) { tileCenterX = Main.maxTilesX; }
            if (tileY < 0) { tileY = 0; }
            if (tileCenterY > Main.maxTilesY) { tileCenterY = Main.maxTilesY; }
            for (int tX = tileX; tX < tileCenterX; tX++)
            {
                for (int tY = tileY; tY < tileCenterY; tY++)
                {
                    Tile checkTile = BaseWorldGen.GetTileSafely(tX, tY);
                    if (checkTile != null && ((checkTile.HasUnactuatedTile && (Main.tileSolid[checkTile.TileType] || (Main.tileSolidTop[checkTile.TileType] && checkTile.TileFrameY == 0))) || checkTile.LiquidAmount > 64))
                    {
                        Vector2 tPos;
                        tPos.X = tX * 16;
                        tPos.Y = tY * 16;
                        if (NPC.position.X + NPC.width > tPos.X && NPC.position.X < tPos.X + 16f && NPC.position.Y + NPC.height > tPos.Y && NPC.position.Y < tPos.Y + 16f)
                        {
                            if (Main.rand.Next(100) == 0 && checkTile.HasUnactuatedTile)
                            {
                                WorldGen.KillTile(tX, tY, true, true, false);
                            }
                        }
                    }
                }
            }

            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead || !Main.player[NPC.target].active)
            {
                NPC.TargetClosest(true);
            }
            if (NPC.alpha <= 0)
            {
                NPC.alpha = 0;
                return false;
            }
            else
            {
                for (int spawnDust = 0; spawnDust < 4; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.GoldCoin, 0f, 0f, 100, default, 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
                NPC.alpha -= 3;
                return false;
            }
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<Greed>()))
            {
                return false;
            }
            NPC.active = false;
            return true;
        }

        public int Def()
        {
            switch ((int)NPC.ai[2])
            {
                case 0:
                    return NPC.defense = 6; //Copper
                case 1:
                    return NPC.defense = 7; //tin
                case 2:
                    return NPC.defense = 9; //Iron
                case 3:
                    return NPC.defense = 11; //Lead
                case 4:
                    return NPC.defense = 13; //Silver
                case 5:
                    return NPC.defense = 15; //Tungsten
                case 6:
                    return NPC.defense = 16; //Gold
                case 7:
                    return NPC.defense = 20; //Platinum
                case 8:
                    return NPC.defense = 19; //Shadow
                case 9:
                    return NPC.defense = 19; //Crimson
                case 10:
                    return NPC.defense = 15; //Abyssium
                case 11:
                    return NPC.defense = 21; //Incinerite
                case 12:
                    return NPC.defense = 25; //Hellstone
                case 13:
                    return NPC.defense = 26; //Cobalt
                case 14:
                    return NPC.defense = 32; //Paladium
                case 15:
                    return NPC.defense = 37; //Mythril
                case 16:
                    return NPC.defense = 42; //Oricalcum
                case 17:
                    return NPC.defense = 50; //Adamantite
                case 18:
                    return NPC.defense = 49; //Titanium
                case 19:
                    return NPC.defense = 50; //Hallowed
                case 20:
                    return NPC.defense = 56; //Chlorophyte
                default:
                    return NPC.defense = 30; //Tail
            }
        }


        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Texture2D glow = Mod.GetTexture("Glowmasks/GreedBody_Glow");

            NPC.position.Y += NPC.height * 0.5f;

            BaseDrawing.DrawTexture(spriteBatch, texture, 0, NPC, drawColor);
            if (Main.LocalPlayer.findTreasure)
            {
                Color color = drawColor;
                byte b2 = 200;
                byte b3 = 170;
                if (color.R < b2)
                {
                    color.R = b2;
                }
                if (color.G < b3)
                {
                    color.G = b3;
                }
                color.A = Main.mouseTextColor;
                BaseDrawing.DrawTexture(spriteBatch, glow, 0, NPC, color);
            }

            NPC.position.Y -= NPC.height * 0.5f;
            return false;
        }
    }
}