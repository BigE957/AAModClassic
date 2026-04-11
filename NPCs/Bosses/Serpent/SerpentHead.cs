using System;
using System.IO;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Items.Boss.Serpent;
using AAModClassic.Items.Materials;
using AAModClassic.Items.Vanity.Mask;
using AAModClassic.Music;
using AAModClassic.UI.WorldGen;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Serpent
{
    [AutoloadBossHead]	
	public class SerpentHead : ModNPC
	{
        public int damage = 0;

        private static int CorruptHead;
        private static int CrimsonHead;
        private static int InfernoHead;
        private static int MireHead;
        private static int HallowHead;

        public override void Load()
        {
            CorruptHead = Mod.AddBossHeadTexture("AAModClassic/NPCs/Bosses/Serpent/Variants/SerpentHead_Corrupt", Type);
            CrimsonHead = Mod.AddBossHeadTexture("AAModClassic/NPCs/Bosses/Serpent/Variants/SerpentHead_Crimson", Type);
            InfernoHead = Mod.AddBossHeadTexture("AAModClassic/NPCs/Bosses/Serpent/Variants/SerpentHead_Inferno", Type);
            MireHead = Mod.AddBossHeadTexture("AAModClassic/NPCs/Bosses/Serpent/Variants/SerpentHead_Mire", Type);
            HallowHead = Mod.AddBossHeadTexture("AAModClassic/NPCs/Bosses/Serpent/Variants/SerpentHead_Hallow", Type);
        }
        
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Subzero Serpent");
            Main.npcFrameCount[NPC.type] = 4;       
        }

        public override void BossHeadSlot(ref int index)
        {
            index = (int)NPC.ai[2] switch
            {
                1 => CorruptHead,
                2 => CrimsonHead,
                3 => InfernoHead,
                4 => MireHead,
                5 => HallowHead,
                _ => index,
            };
        }


        public override void SetDefaults()
		{
			NPC.npcSlots = 5f;
            NPC.width = 32;
            NPC.height = 32;
            if (NPC.ai[2] == 2)
            {
                NPC.lifeMax = 7000;
            }
            else if (NPC.ai[2] == 5)
            {
                NPC.lifeMax = 15000;
            }
            else
            {
                NPC.lifeMax = 6000;
            }
            NPC.damage = 35;
            NPC.defense = 10;
            NPC.value = 50000f;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            AnimationType = NPCID.GiantWormHead;
            NPC.behindTiles = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit5;
            NPC.DeathSound = SoundID.NPCDeath7;
            NPC.netAlways = true;
            NPC.boss = true;
            Music = MusicManagementSystem.MusicSlots["Subzero"];
            NPC.alpha = 50;
            NPC.buffImmune[BuffID.Frostburn] = true;
        }

        private bool fireAttack;
        private int attackCounter;
        private int attackTimer;

		public bool tongueFlick = false;
		public bool tongueFlickDir = false;
		public int tongueFlickCounter = 0;
        private int RunOnce = 0;
        private int StopSnow = 0;

        public float[] internalAI = new float[5];
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
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.Read();
                internalAI[1] = reader.Read();
                internalAI[2] = reader.Read();
                internalAI[3] = reader.Read();
                internalAI[4] = reader.Read();
            }
        }

        public override void AI()
        {
            Rain();
            Player player = Main.player[NPC.target];
            RunAway(player);
            Attack(player);

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
                    Tile checkTile = WorldGenUtils.GetTileSafely(tX, tY);
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

            if (NPC.ai[3] > 0f)
            {
                NPC.realLife = (int)NPC.ai[3];
            }
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
            }
            NPC.velocity.Length();
            
            if (internalAI[4] != 1)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.ai[3] = NPC.whoAmI;
                    int previousSegment = NPC.whoAmI;
                    //NPC.realLife = NPC.whoAmI;
                    int Length = 12;
                    for (int a = 0; a <= Length; a++)
                    {
                        int type = ModContent.NPCType<SerpentBody>();
                        if (a == Length)
                        {
                            type = ModContent.NPCType<SerpentTail>();
                        }
                        int segment = NPC.NewNPC(NPC.GetSource_FromThis(), (int)(NPC.position.X + NPC.width / 2), (int)(NPC.position.Y + NPC.height), type, NPC.whoAmI, 0f, previousSegment, NPC.ai[2], NPC.whoAmI, 255);
                        Main.npc[segment].realLife = NPC.whoAmI;
                        NPC.ai[0] = segment;
                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, segment, 0f, 0f, 0f, 0, 0, 0);
                        previousSegment = (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) ? segment : (NPC.whoAmI = segment));
                    }
                    internalAI[4] = 1;
                    NPC.netUpdate = true;
                }
            }
            int posX = (int)(NPC.position.X / 16f);
            int centerX = (int)((NPC.position.X + NPC.width) / 16f);
            int posY = (int)(NPC.position.Y / 16f);
            int centerY = (int)((NPC.position.Y + NPC.height) / 16f);
            if (posX < 0)
            {
                posX = 0;
            }
            if (centerX > Main.maxTilesX)
            {
                centerX = Main.maxTilesX;
            }
            if (posY < 0)
            {
                posY = 0;
            }
            if (centerY > Main.maxTilesY)
            {
                centerY = Main.maxTilesY;
            }
            bool inRange = false;
            if (!inRange)
            {
                for (int x = posX; x < centerX; x++)
                {
                    for (int y = posY; y < centerY; y++)
                    {
                        if (Main.tile[x, y] != null && ((Main.tile[x, y].HasUnactuatedTile && (Main.tileSolid[Main.tile[x, y].TileType] || (Main.tileSolidTop[Main.tile[x, y].TileType] && Main.tile[x, y].TileFrameY == 0))) || Main.tile[x, y].LiquidAmount > 64))
                        {
                            Vector2 vector2;
                            vector2.X = x * 16;
                            vector2.Y = y * 16;
                            if (NPC.position.X + NPC.width > vector2.X && NPC.position.X < vector2.X + 16f && NPC.position.Y + NPC.height > vector2.Y && NPC.position.Y < vector2.Y + 16f)
                            {
                                inRange = true;
                                break;
                            }
                        }
                    }
                }
            }
            if (!inRange)
            {
                NPC.localAI[1] = 1f;
                Rectangle rectangle = new Rectangle((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height);
                int num46 = 1000;
                bool flag3 = true;
                if (NPC.position.Y > Main.player[NPC.target].position.Y)
                {
                    for (int target = 0; target < 255; target++)
                    {
                        if (Main.player[target].active)
                        {
                            Rectangle rectangle2 = new Rectangle((int)Main.player[target].position.X - num46, (int)Main.player[target].position.Y - num46, num46 * 2, num46 * 2);
                            if (rectangle.Intersects(rectangle2))
                            {
                                flag3 = false;
                                break;
                            }
                        }
                    }
                    if (flag3)
                    {
                        inRange = true;
                    }
                }
            }
            else
            {
                NPC.localAI[1] = 0f;
            }
            float maxDistance = 16f;
            float num48 = 0.1f;
            float num49 = 0.15f;

            if (NPC.ai[2] == 1 || NPC.ai[2] == 5)
            {
                num48 = 0.13f;
                num49 = 0.2f;
            }

            Vector2 center = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
            float targetX = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2;
            float targetY = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2;
            targetX = (int)(targetX / 16f) * 16;
            targetY = (int)(targetY / 16f) * 16;
            center.X = (int)(center.X / 16f) * 16;
            center.Y = (int)(center.Y / 16f) * 16;
            targetX -= center.X;
            targetY -= center.Y;
            float num52 = (float)Math.Sqrt(targetX * targetX + targetY * targetY);
            if (NPC.ai[1] > 0f && NPC.ai[1] < Main.npc.Length)
            {
                try
                {
                    center = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                    targetX = Main.npc[(int)NPC.ai[1]].position.X + Main.npc[(int)NPC.ai[1]].width / 2 - center.X;
                    targetY = Main.npc[(int)NPC.ai[1]].position.Y + Main.npc[(int)NPC.ai[1]].height / 2 - center.Y;
                }
                catch
                {
                }
                NPC.rotation = (float)Math.Atan2(targetY, targetX) + 1.57f;
                num52 = (float)Math.Sqrt(targetX * targetX + targetY * targetY);
                int num53 = (int)(44f * NPC.scale);
                num52 = (num52 - num53) / num52;
                targetX *= num52;
                targetY *= num52;
                NPC.velocity = Vector2.Zero;
                NPC.position.X = NPC.position.X + targetX;
                NPC.position.Y = NPC.position.Y + targetY;
                return;
            }
            if (!inRange)
            {
                NPC.TargetClosest(true);
                NPC.velocity.Y = NPC.velocity.Y + 0.15f;
                if (NPC.velocity.Y > maxDistance)
                {
                    NPC.velocity.Y = maxDistance;
                }
                if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < maxDistance * 0.4)
                {
                    if (NPC.velocity.X < 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X - num48 * 1.1f;
                    }
                    else
                    {
                        NPC.velocity.X = NPC.velocity.X + num48 * 1.1f;
                    }
                }
                else if (NPC.velocity.Y == maxDistance)
                {
                    if (NPC.velocity.X < targetX)
                    {
                        NPC.velocity.X = NPC.velocity.X + num48;
                    }
                    else if (NPC.velocity.X > targetX)
                    {
                        NPC.velocity.X = NPC.velocity.X - num48;
                    }
                }
                else if (NPC.velocity.Y > 4f)
                {
                    if (NPC.velocity.X < 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X + num48 * 0.9f;
                    }
                    else
                    {
                        NPC.velocity.X = NPC.velocity.X - num48 * 0.9f;
                    }
                }
            }
            else
            {
                if (NPC.soundDelay == 0)
                {
                    float num54 = num52 / 40f;
                    if (num54 < 10f)
                    {
                        num54 = 10f;
                    }
                    if (num54 > 20f)
                    {
                        num54 = 20f;
                    }
                    NPC.soundDelay = (int)num54;
                    SoundEngine.PlaySound(SoundID.WormDig, NPC.position);
                }
                num52 = (float)Math.Sqrt(targetX * targetX + targetY * targetY);
                float TargetPosX = Math.Abs(targetX);
                float TargetPosY = Math.Abs(targetY);
                float num57 = maxDistance / num52;
                targetX *= num57;
                targetY *= num57;
                if (((NPC.velocity.X > 0f && targetX > 0f) || (NPC.velocity.X < 0f && targetX < 0f)) && ((NPC.velocity.Y > 0f && targetY > 0f) || (NPC.velocity.Y < 0f && targetY < 0f)))
                {
                    if (NPC.velocity.X < targetX)
                    {
                        NPC.velocity.X = NPC.velocity.X + num49;
                    }
                    else if (NPC.velocity.X > targetX)
                    {
                        NPC.velocity.X = NPC.velocity.X - num49;
                    }
                    if (NPC.velocity.Y < targetY)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + num49;
                    }
                    else if (NPC.velocity.Y > targetY)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - num49;
                    }
                }
                if ((NPC.velocity.X > 0f && targetX > 0f) || (NPC.velocity.X < 0f && targetX < 0f) || (NPC.velocity.Y > 0f && targetY > 0f) || (NPC.velocity.Y < 0f && targetY < 0f))
                {
                    if (NPC.velocity.X < targetX)
                    {
                        NPC.velocity.X = NPC.velocity.X + num48;
                    }
                    else if (NPC.velocity.X > targetX)
                    {
                        NPC.velocity.X = NPC.velocity.X - num48;
                    }
                    if (NPC.velocity.Y < targetY)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + num48;
                    }
                    else if (NPC.velocity.Y > targetY)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - num48;
                    }
                    if (Math.Abs(targetY) < maxDistance * 0.2 && ((NPC.velocity.X > 0f && targetX < 0f) || (NPC.velocity.X < 0f && targetX > 0f)))
                    {
                        if (NPC.velocity.Y > 0f)
                        {
                            NPC.velocity.Y = NPC.velocity.Y + num48 * 2f;
                        }
                        else
                        {
                            NPC.velocity.Y = NPC.velocity.Y - num48 * 2f;
                        }
                    }
                    if (Math.Abs(targetX) < maxDistance * 0.2 && ((NPC.velocity.Y > 0f && targetY < 0f) || (NPC.velocity.Y < 0f && targetY > 0f)))
                    {
                        if (NPC.velocity.X > 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X + num48 * 2f;
                        }
                        else
                        {
                            NPC.velocity.X = NPC.velocity.X - num48 * 2f;
                        }
                    }
                }
                else if (TargetPosX > TargetPosY)
                {
                    if (NPC.velocity.X < targetX)
                    {
                        NPC.velocity.X = NPC.velocity.X + num48 * 1.1f;
                    }
                    else if (NPC.velocity.X > targetX)
                    {
                        NPC.velocity.X = NPC.velocity.X - num48 * 1.1f;
                    }
                    if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < maxDistance * 0.5)
                    {
                        if (NPC.velocity.Y > 0f)
                        {
                            NPC.velocity.Y = NPC.velocity.Y + num48;
                        }
                        else
                        {
                            NPC.velocity.Y = NPC.velocity.Y - num48;
                        }
                    }
                }
                else
                {
                    if (NPC.velocity.Y < targetY)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + num48 * 1.1f;
                    }
                    else if (NPC.velocity.Y > targetY)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - num48 * 1.1f;
                    }
                    if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < maxDistance * 0.5)
                    {
                        if (NPC.velocity.X > 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X + num48;
                        }
                        else
                        {
                            NPC.velocity.X = NPC.velocity.X - num48;
                        }
                    }
                }
            }
            NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;
            if (inRange)
            {
                if (NPC.localAI[0] != 1f)
                {
                    NPC.netUpdate = true;
                }
                NPC.localAI[0] = 1f;
            }
            else
            {
                if (NPC.localAI[0] != 0f)
                {
                    NPC.netUpdate = true;
                }
                NPC.localAI[0] = 0f;
            }
            if (((NPC.velocity.X > 0f && NPC.oldVelocity.X < 0f) || (NPC.velocity.X < 0f && NPC.oldVelocity.X > 0f) || (NPC.velocity.Y > 0f && NPC.oldVelocity.Y < 0f) || (NPC.velocity.Y < 0f && NPC.oldVelocity.Y > 0f)) && !NPC.justHit)
            {
                NPC.netUpdate = true;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (Main.netMode != NetmodeID.Server && !tongueFlick && Main.rand.Next(20) == 0)
            {
                tongueFlick = true;
            }
            if (tongueFlick)
            {
                if (tongueFlickDir)
                {
                    tongueFlickCounter--;
                    if (tongueFlickCounter <= 0)
                    {
                        tongueFlickCounter = 8;
                        NPC.frame.Y -= NPC.frame.Height;
                        if (NPC.frame.Y <= 0)
                            tongueFlick = tongueFlickDir = false;
                    }
                }
                else
                {
                    tongueFlickCounter--;
                    if (tongueFlickCounter <= 0)
                    {
                        tongueFlickCounter = 8;
                        NPC.frame.Y += NPC.frame.Height;
                        if (NPC.frame.Y >= (NPC.frame.Height * 3))
                            tongueFlickDir = true;
                    }
                }
            }
        }

        public void RunAway(Player player)
        {
            if (player.dead || !player.active || !player.ZoneSnow)
            {
                NPC.TargetClosest(true);
                if (player.dead || !player.active || !player.ZoneSnow)
                {
                    internalAI[0]++;
                    NPC.velocity.Y = NPC.velocity.Y + 0.8f;
                    if (internalAI[0] >= 300)
                    {
                        NPC.active = false;
                    }
                }
                else
                {
                    internalAI[0] = 0;
                }
            }
            else
            {
                if (NPC.alpha > 0)
                {
                    NPC.alpha -= 4;
                }
                else
                {
                    NPC.alpha = 0;
                }
            }
        }

        private void Rain()
        {
            if (NPC.ai[2] == 3 || NPC.ai[2] == 5)
            {
                NPC.defense = 32;
            }

            if (NPC.ai[2] == 4 || NPC.ai[2] == 5)
            {
                NPC.damage = 40;
            }

            if (Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) > 6000f || Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 6000f || Main.player[NPC.target].dead)
            {
                if (StopSnow == 0)
                {
                    RainStop();
                    StopSnow = 1;
                }
            }

            if (RunOnce == 0)
            {
                if (!Main.raining)
                {
                    int num = 86400;
                    int num5 = num / 24;
                    Main.rainTime = Main.rand.Next(num5 * 8, num);
                    if (Main.rand.Next(3) == 0)
                    {
                        Main.rainTime += Main.rand.Next(0, num5);
                    }
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.rainTime += Main.rand.Next(0, num5 * 2);
                    }
                    if (Main.rand.Next(5) == 0)
                    {
                        Main.rainTime += Main.rand.Next(0, num5 * 2);
                    }
                    if (Main.rand.Next(6) == 0)
                    {
                        Main.rainTime += Main.rand.Next(0, num5 * 3);
                    }
                    if (Main.rand.Next(7) == 0)
                    {
                        Main.rainTime += Main.rand.Next(0, num5 * 4);
                    }
                    if (Main.rand.Next(8) == 0)
                    {
                        Main.rainTime += Main.rand.Next(0, num5 * 5);
                    }
                    float num1 = 1f;
                    if (Main.rand.Next(2) == 0)
                    {
                        num1 += 0.05f;
                    }
                    if (Main.rand.Next(3) == 0)
                    {
                        num1 += 0.1f;
                    }
                    if (Main.rand.Next(4) == 0)
                    {
                        num1 += 0.15f;
                    }
                    if (Main.rand.Next(5) == 0)
                    {
                        num1 += 0.2f;
                    }
                    Main.rainTime = (int)(Main.rainTime * num1);
                    Main.raining = true;
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetMessage.SendData(MessageID.WorldData, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
                    }
                }
                RunOnce = 1;
            }
        }

        private void RainStop()
        {
            if (Main.raining)
            {
                Main.rainTime = 0;
                Main.raining = false;
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.WorldData, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
                }
            }
        }

        public void Attack(Player player)
        {
            if (Main.expertMode)
            {
                damage = NPC.damage / 4;
            }
            else
            {
                damage = NPC.damage / 2;
            }

            if (internalAI[1]++ > 300)
            {
                internalAI[1] = 0;
                internalAI[2] = Main.rand.Next(3);
                NPC.netUpdate = true;
            }

            if (internalAI[2] == 0)
            {
                if (NPC.CountNPCS(ModContent.NPCType<IceCrystal>()) < 3)
                {
                   NPC.NewNPC(NPC.GetSource_FromThis(), (int)player.position.X + Main.rand.Next(-500, 500), (int)player.position.Y + 500, ModContent.NPCType<IceCrystal>(), 0, NPC.ai[2], 0, 0, 0, NPC.target);
                }
                internalAI[2] = 2;
                NPC.netUpdate = true;
            }
            else if (internalAI[2] == 1)
            {
                attackCounter++;
                if (attackCounter >= 180 && fireAttack == false)
                {
                    attackCounter = 0;
                    fireAttack = true;
                    NPC.netUpdate = true;
                }
                if (fireAttack == true && Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
                {
                    attackTimer++;
                    if (attackTimer == 20 || attackTimer == 50 || attackTimer == 79)
                    {
                        int p = BaseAI.FireProjectile(Main.player[NPC.target].Center, NPC, ModContent.ProjectileType<IceBall2>(), damage, 3, 14f, 0, 0, -1);
                        Main.projectile[p].ai[1] = NPC.ai[2]; 
                        NPC.netUpdate = true;
                    }
                    if (attackTimer >= 80)
                    {
                        fireAttack = false;
                        attackTimer = 0;
                        attackCounter = 0;
                        NPC.netUpdate = true;
                    }
                }
            }
            else
            {
                attackCounter++;
                if (attackCounter == 400 && fireAttack == false)
                {
                    attackCounter = 0;
                    fireAttack = true;
                    NPC.netUpdate = true;
                }
                if (fireAttack == true)
                {
                    attackTimer++;

                    if ((attackTimer == 8 || attackTimer == 16 || attackTimer == 24 || attackTimer == 32 || attackTimer == 40 || attackTimer == 48 || attackTimer == 56 || attackTimer == 64 || attackTimer == 72 || attackTimer == 79) && !NPC.HasBuff(BuffID.Wet))
                    {
                        for (int i = 0; i < 5; ++i)
                        {
                            float num433 = 6f;
                            Vector2 PlayerDistance = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
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
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), PlayerDistance.X, PlayerDistance.Y, NPC.velocity.X * 1.5f, NPC.velocity.Y * 1.5f, ModContent.ProjectileType<SnowBreath>(), damage, 0, Main.myPlayer, 0, NPC.ai[2]);
                        }
                    }
                    if (attackTimer >= 80)
                    {
                        fireAttack = false;
                        attackTimer = 0;
                        attackCounter = 0;
                        NPC.netUpdate = true;
                    }
                }
            }

            if (internalAI[3]++ > 400 && NPC.CountNPCS(ModContent.NPCType<Enemies.Snow.SnakeHead>()) < 3)
            {
                for (int i = 0; i < 3 - NPC.CountNPCS(ModContent.NPCType<Enemies.Snow.SnakeHead>()); i++)
                {
                    AAModGlobalNPC.SpawnBoss(player, ModContent.NPCType<Enemies.Snow.SnakeHead>(), false, 0, 0, "Snake", false);
                }
                internalAI[3] = 0;
            }
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (Main.expertMode)
			{
                target.AddBuff(BuffID.Chilled, 200, true);
			}
			else
			{
                target.AddBuff(BuffID.Chilled, 100, true);
			}
		}

        public override void BossLoot(ref int potionType)
        {
            potionType = ItemID.HealingPotion;   //boss drops
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = (int)(NPC.lifeMax * 0.75f * balance);
			NPC.damage = (int)(NPC.damage * 0.85f);
		}

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int x = 0; x < 5; x++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.IceDust>(), hit.HitDirection, -1f, 0, default, 1f);
            }
            if (NPC.life == 0)
            {
                for (int x = 0; x < 5; x++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.SnowDustLight>(), hit.HitDirection, -1f, 0, default, 1f);
                }

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("SZSGoreHead").Type, 1f);
            }
        }

        public override void OnKill()
        {
            if (!Main.expertMode)
            {
                if (Main.rand.Next(7) == 0)
                {
                    Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<SerpentMask>());
                }
                NPC.DropLoot(ModContent.ItemType<SnowMana>(), 10, 15);
                string[] lootTable = { "BlizardBuster", "SerpentSpike", "Icepick", "SerpentSting", "Sickle", "SickleShot", "SnakeStaff", "SubzeroSlasher" };
                int loot = Main.rand.Next(lootTable.Length);
                NPC.DropLoot(Items.Vanity.Mask.SerpentMask.type, 1f / 7);
                if (Main.rand.Next(9) == 0)
                {
                    NPC.DropLoot(ModContent.ItemType<SnowflakeShuriken>(), 90, 120);
                }
                else
                {
                    NPC.DropLoot(Mod.Find<ModItem>(lootTable[loot]).Type);
                }
            }
            if (Main.expertMode)
            {
                NPC.DropLoot(ModContent.ItemType<Items.Boss.Serpent.SerpentBag>());
            }
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<SerpentTrophy>());
            }
            //NPC.value = 0f;
            //NPC.boss = false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D tex = TextureAssets.Npc[NPC.type].Value;

            switch ((int)NPC.ai[2])
            {
                case 0: break;
                case 1: tex = Mod.GetTexture("NPCs/Bosses/Serpent/Variants/Corrupt"); break;
                case 2: tex = Mod.GetTexture("NPCs/Bosses/Serpent/Variants/Crimson"); break;
                case 3: tex = Mod.GetTexture("NPCs/Bosses/Serpent/Variants/Inferno"); break;
                case 4: tex = Mod.GetTexture("NPCs/Bosses/Serpent/Variants/Mire"); break;
                case 5: tex = Mod.GetTexture("NPCs/Bosses/Serpent/Variants/Hallow"); break;
            }

            BaseDrawing.DrawTexture(spriteBatch, tex, 0, NPC, drawColor, true);
            return false;
        }
    }

    public class SerpentBody : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Subzero Serpent");
            Main.npcFrameCount[NPC.type] = 4;
        }

        public override void SetDefaults()
        {
            NPC.npcSlots = 5f;
            NPC.width = 32;
            NPC.height = 32;
            NPC.damage = 35;
            NPC.defense = 10;
            NPC.lifeMax = 6000;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            AnimationType = NPCID.GiantWormHead;
            NPC.behindTiles = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit5;
            NPC.DeathSound = SoundID.NPCDeath7;
            NPC.netAlways = true;
            NPC.boss = true;
            Music = MusicManagementSystem.MusicSlots["Subzero"];
            NPC.alpha = 50;
            NPC.dontCountMe = true;
        }

        public override void AI()
        {
            if (!Main.npc[NPC.realLife].active)
            {
                NPC.active = false;
                return;
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
                    Tile checkTile = WorldGenUtils.GetTileSafely(tX, tY);
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

            if (NPC.ai[3] > 0f)
            {
                NPC.realLife = (int)NPC.ai[3];
            }
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
            }
            NPC.velocity.Length();
            bool flag = false;
            if (NPC.ai[1] <= 0f)
            {
                flag = true;
            }
            else if (Main.npc[(int)NPC.ai[1]].life <= 0)
            {
                flag = true;
            }
            if (flag)
            {
                NPC.life = 0;
                NPC.HitEffect(0, 10.0);
                NPC.checkDead();
            }
            int num12 = (int)(NPC.position.X / 16f) - 1;
            int num13 = (int)((NPC.position.X + NPC.width) / 16f) + 2;
            int num14 = (int)(NPC.position.Y / 16f) - 1;
            int num15 = (int)((NPC.position.Y + NPC.height) / 16f) + 2;
            if (num12 < 0)
            {
                num12 = 0;
            }
            if (num13 > Main.maxTilesX)
            {
                num13 = Main.maxTilesX;
            }
            if (num14 < 0)
            {
                num14 = 0;
            }
            if (num15 > Main.maxTilesY)
            {
                num15 = Main.maxTilesY;
            }
            bool flag2 = false;
            if (!flag2)
            {
                for (int k = num12; k < num13; k++)
                {
                    for (int l = num14; l < num15; l++)
                    {
                        if (Main.tile[k, l] != null && ((Main.tile[k, l].HasUnactuatedTile && (Main.tileSolid[Main.tile[k, l].TileType] || (Main.tileSolidTop[Main.tile[k, l].TileType] && Main.tile[k, l].TileFrameY == 0))) || Main.tile[k, l].LiquidAmount > 64))
                        {
                            Vector2 vector2;
                            vector2.X = k * 16;
                            vector2.Y = l * 16;
                            if (NPC.position.X + NPC.width > vector2.X && NPC.position.X < vector2.X + 16f && NPC.position.Y + NPC.height > vector2.Y && NPC.position.Y < vector2.Y + 16f)
                            {
                                flag2 = true;
                                break;
                            }
                        }
                    }
                }
            }
            if (!flag2)
            {
                NPC.localAI[1] = 1f;
            }
            else
            {
                NPC.localAI[1] = 0f;
            }
            float num17 = 16f;
            if (Main.player[NPC.target].dead || Main.player[NPC.target].position.Y < Main.rockLayer)
            {
                flag2 = false;
                NPC.velocity.Y = NPC.velocity.Y + 1f;
                if (NPC.position.Y > (double)((Main.maxTilesY - 200) * 16))
                {
                    NPC.velocity.Y = NPC.velocity.Y + 1f;
                    num17 = 32f;
                }
                /*
                if (NPC.position.Y > (double)((Main.maxTilesY - 200) * 16))
                {
                    for (int a = 0; a < 200; a++)
                    {
                        if (Main.npc[a].type == ModContent.NPCType<ArmoredDiggerHead>() || Main.npc[a].type == ModContent.NPCType<ArmoredDiggerBody>() ||
                            Main.npc[a].type == ModContent.NPCType<ArmoredDiggerTail>())
                        {
                            Main.npc[a].active = false;
                        }
                    }
                }
                */
            }
            float num18 = 0.1f;
            float num19 = 0.15f;
            Vector2 vector3 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
            float num20 = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2;
            float num21 = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2;
            num20 = (int)(num20 / 16f) * 16;
            num21 = (int)(num21 / 16f) * 16;
            vector3.X = (int)(vector3.X / 16f) * 16;
            vector3.Y = (int)(vector3.Y / 16f) * 16;
            num20 -= vector3.X;
            num21 -= vector3.Y;
            float num22 = (float)Math.Sqrt(num20 * num20 + num21 * num21);
            if (NPC.ai[1] > 0f && NPC.ai[1] < Main.npc.Length)
            {
                try
                {
                    vector3 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                    num20 = Main.npc[(int)NPC.ai[1]].position.X + Main.npc[(int)NPC.ai[1]].width / 2 - vector3.X;
                    num21 = Main.npc[(int)NPC.ai[1]].position.Y + Main.npc[(int)NPC.ai[1]].height / 2 - vector3.Y;
                }
                catch
                {
                }
                NPC.rotation = (float)Math.Atan2(num21, num20) + 1.57f;
                num22 = (float)Math.Sqrt(num20 * num20 + num21 * num21);
                int num23 = (int)(44f * NPC.scale);
                num22 = (num22 - num23) / num22;
                num20 *= num22;
                num21 *= num22;
                NPC.velocity = Vector2.Zero;
                NPC.position.X = NPC.position.X + num20;
                NPC.position.Y = NPC.position.Y + num21;
                return;
            }
            if (!flag2)
            {
                NPC.TargetClosest(true);
                NPC.velocity.Y = NPC.velocity.Y + 0.15f;
                if (NPC.velocity.Y > num17)
                {
                    NPC.velocity.Y = num17;
                }
                if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < num17 * 0.4)
                {
                    if (NPC.velocity.X < 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X - num18 * 1.1f;
                    }
                    else
                    {
                        NPC.velocity.X = NPC.velocity.X + num18 * 1.1f;
                    }
                }
                else if (NPC.velocity.Y == num17)
                {
                    if (NPC.velocity.X < num20)
                    {
                        NPC.velocity.X = NPC.velocity.X + num18;
                    }
                    else if (NPC.velocity.X > num20)
                    {
                        NPC.velocity.X = NPC.velocity.X - num18;
                    }
                }
                else if (NPC.velocity.Y > 4f)
                {
                    if (NPC.velocity.X < 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X + num18 * 0.9f;
                    }
                    else
                    {
                        NPC.velocity.X = NPC.velocity.X - num18 * 0.9f;
                    }
                }
            }
            else
            {
                if (NPC.soundDelay == 0)
                {
                    float num24 = num22 / 40f;
                    if (num24 < 10f)
                    {
                        num24 = 10f;
                    }
                    if (num24 > 20f)
                    {
                        num24 = 20f;
                    }
                    NPC.soundDelay = (int)num24;
                    SoundEngine.PlaySound(SoundID.WormDig, NPC.position);
                }
                num22 = (float)Math.Sqrt(num20 * num20 + num21 * num21);
                float num25 = Math.Abs(num20);
                float num26 = Math.Abs(num21);
                float num27 = num17 / num22;
                num20 *= num27;
                num21 *= num27;
                if (((NPC.velocity.X > 0f && num20 > 0f) || (NPC.velocity.X < 0f && num20 < 0f)) && ((NPC.velocity.Y > 0f && num21 > 0f) || (NPC.velocity.Y < 0f && num21 < 0f)))
                {
                    if (NPC.velocity.X < num20)
                    {
                        NPC.velocity.X = NPC.velocity.X + num19;
                    }
                    else if (NPC.velocity.X > num20)
                    {
                        NPC.velocity.X = NPC.velocity.X - num19;
                    }
                    if (NPC.velocity.Y < num21)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + num19;
                    }
                    else if (NPC.velocity.Y > num21)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - num19;
                    }
                }
                if ((NPC.velocity.X > 0f && num20 > 0f) || (NPC.velocity.X < 0f && num20 < 0f) || (NPC.velocity.Y > 0f && num21 > 0f) || (NPC.velocity.Y < 0f && num21 < 0f))
                {
                    if (NPC.velocity.X < num20)
                    {
                        NPC.velocity.X = NPC.velocity.X + num18;
                    }
                    else if (NPC.velocity.X > num20)
                    {
                        NPC.velocity.X = NPC.velocity.X - num18;
                    }
                    if (NPC.velocity.Y < num21)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + num18;
                    }
                    else if (NPC.velocity.Y > num21)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - num18;
                    }
                    if (Math.Abs(num21) < num17 * 0.2 && ((NPC.velocity.X > 0f && num20 < 0f) || (NPC.velocity.X < 0f && num20 > 0f)))
                    {
                        if (NPC.velocity.Y > 0f)
                        {
                            NPC.velocity.Y = NPC.velocity.Y + num18 * 2f;
                        }
                        else
                        {
                            NPC.velocity.Y = NPC.velocity.Y - num18 * 2f;
                        }
                    }
                    if (Math.Abs(num20) < num17 * 0.2 && ((NPC.velocity.Y > 0f && num21 < 0f) || (NPC.velocity.Y < 0f && num21 > 0f)))
                    {
                        if (NPC.velocity.X > 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X + num18 * 2f;
                        }
                        else
                        {
                            NPC.velocity.X = NPC.velocity.X - num18 * 2f;
                        }
                    }
                }
                else if (num25 > num26)
                {
                    if (NPC.velocity.X < num20)
                    {
                        NPC.velocity.X = NPC.velocity.X + num18 * 1.1f;
                    }
                    else if (NPC.velocity.X > num20)
                    {
                        NPC.velocity.X = NPC.velocity.X - num18 * 1.1f;
                    }
                    if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < num17 * 0.5)
                    {
                        if (NPC.velocity.Y > 0f)
                        {
                            NPC.velocity.Y = NPC.velocity.Y + num18;
                        }
                        else
                        {
                            NPC.velocity.Y = NPC.velocity.Y - num18;
                        }
                    }
                }
                else
                {
                    if (NPC.velocity.Y < num21)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + num18 * 1.1f;
                    }
                    else if (NPC.velocity.Y > num21)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - num18 * 1.1f;
                    }
                    if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < num17 * 0.5)
                    {
                        if (NPC.velocity.X > 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X + num18;
                        }
                        else
                        {
                            NPC.velocity.X = NPC.velocity.X - num18;
                        }
                    }
                }
            }
            NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.localAI[0] == 0)
            {
                NPC.localAI[0] = 1;
                NPC.localAI[1] = Main.rand.Next(4);
            }
            NPC.frame.Y = (int)NPC.localAI[1] * NPC.frame.Height;
        }

        public override bool PreKill()
        {
            return false;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<SerpentHead>()))
            {
                return false;
            }
            return true;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int x = 0; x < 5; x++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.IceDust>(), hit.HitDirection, -1f, 0, default, 1f);
            }
            if (NPC.life == 0)
            {
                for (int x = 0; x < 5; x++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.SnowDustLight>(), hit.HitDirection, -1f, 0, default, 1f);
                }

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("SZSGoreBody").Type, 1f);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D tex = TextureAssets.Npc[NPC.type].Value;

            switch ((int)NPC.ai[2])
            {
                case 0: break;
                case 1: tex = Mod.GetTexture("NPCs/Bosses/Serpent/Variants/CorruptBody"); break;
                case 2: tex = Mod.GetTexture("NPCs/Bosses/Serpent/Variants/CrimsonBody"); break;
                case 3: tex = Mod.GetTexture("NPCs/Bosses/Serpent/Variants/InfernoBody"); break;
                case 4: tex = Mod.GetTexture("NPCs/Bosses/Serpent/Variants/MireBody"); break;
                case 5: tex = Mod.GetTexture("NPCs/Bosses/Serpent/Variants/HallowBody"); break;
            }

            BaseDrawing.DrawTexture(spriteBatch, tex, 0, NPC, drawColor, true);
            return false;
        }
    }

    public class SerpentTail : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Subzero Serpent");
            Main.npcFrameCount[NPC.type] = 1;
        }

        public override void SetDefaults()
        {
            NPC.npcSlots = 5f;
            NPC.width = 32;
            NPC.height = 32;
            NPC.damage = 35;
            NPC.defense = 10;
            NPC.lifeMax = 6000;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            AnimationType = NPCID.GiantWormHead;
            NPC.behindTiles = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit5;
            NPC.DeathSound = SoundID.NPCDeath7;
            NPC.netAlways = true;
            NPC.boss = true;
            Music = MusicManagementSystem.MusicSlots["Subzero"];
            NPC.alpha = 50;
            NPC.dontCountMe = true;
        }


        public override void AI()
        {
            if (NPC.realLife == -1 || !Main.npc[NPC.realLife].active)
            {
                NPC.active = false;
                return;
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
                    Tile checkTile = WorldGenUtils.GetTileSafely(tX, tY);
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

            if (NPC.ai[3] > 0f)
            {
                NPC.realLife = (int)NPC.ai[3];
            }
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(true);
            }
            NPC.velocity.Length();
            bool headActive = false;
            if (NPC.ai[1] <= 0f)
            {
                headActive = true;
            }
            else if (Main.npc[(int)NPC.ai[1]].life <= 0)
            {
                headActive = true;
            }
            if (headActive)
            {
                NPC.life = 0;
                NPC.HitEffect(0, 10.0);
                NPC.checkDead();
            }
            int num12 = (int)(NPC.position.X / 16f) - 1;
            int num13 = (int)((NPC.position.X + NPC.width) / 16f) + 2;
            int num14 = (int)(NPC.position.Y / 16f) - 1;
            int num15 = (int)((NPC.position.Y + NPC.height) / 16f) + 2;
            if (num12 < 0)
            {
                num12 = 0;
            }
            if (num13 > Main.maxTilesX)
            {
                num13 = Main.maxTilesX;
            }
            if (num14 < 0)
            {
                num14 = 0;
            }
            if (num15 > Main.maxTilesY)
            {
                num15 = Main.maxTilesY;
            }
            bool flag2 = false;
            if (!flag2)
            {
                for (int k = num12; k < num13; k++)
                {
                    for (int l = num14; l < num15; l++)
                    {
                        if (Main.tile[k, l] != null && ((Main.tile[k, l].HasUnactuatedTile && (Main.tileSolid[Main.tile[k, l].TileType] || (Main.tileSolidTop[Main.tile[k, l].TileType] && Main.tile[k, l].TileFrameY == 0))) || Main.tile[k, l].LiquidAmount > 64))
                        {
                            Vector2 vector2;
                            vector2.X = k * 16;
                            vector2.Y = l * 16;
                            if (NPC.position.X + NPC.width > vector2.X && NPC.position.X < vector2.X + 16f && NPC.position.Y + NPC.height > vector2.Y && NPC.position.Y < vector2.Y + 16f)
                            {
                                flag2 = true;
                                break;
                            }
                        }
                    }
                }
            }
            if (!flag2)
            {
                NPC.localAI[1] = 1f;
            }
            else
            {
                NPC.localAI[1] = 0f;
            }
            float num17 = 16f;
            float num18 = 0.1f;
            float num19 = 0.15f;
            Vector2 vector3 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
            float num20 = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2;
            float num21 = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2;
            num20 = (int)(num20 / 16f) * 16;
            num21 = (int)(num21 / 16f) * 16;
            vector3.X = (int)(vector3.X / 16f) * 16;
            vector3.Y = (int)(vector3.Y / 16f) * 16;
            num20 -= vector3.X;
            num21 -= vector3.Y;
            float num22 = (float)Math.Sqrt(num20 * num20 + num21 * num21);
            if (NPC.ai[1] > 0f && NPC.ai[1] < Main.npc.Length)
            {
                try
                {
                    vector3 = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                    num20 = Main.npc[(int)NPC.ai[1]].position.X + Main.npc[(int)NPC.ai[1]].width / 2 - vector3.X;
                    num21 = Main.npc[(int)NPC.ai[1]].position.Y + Main.npc[(int)NPC.ai[1]].height / 2 - vector3.Y;
                }
                catch
                {
                }
                NPC.rotation = (float)Math.Atan2(num21, num20) + 1.57f;
                num22 = (float)Math.Sqrt(num20 * num20 + num21 * num21);
                int num23 = (int)(44f * NPC.scale);
                num22 = (num22 - num23) / num22;
                num20 *= num22;
                num21 *= num22;
                NPC.velocity = Vector2.Zero;
                NPC.position.X = NPC.position.X + num20;
                NPC.position.Y = NPC.position.Y + num21;
                return;
            }
            if (!flag2)
            {
                NPC.TargetClosest(true);
                NPC.velocity.Y = NPC.velocity.Y + 0.15f;
                if (NPC.velocity.Y > num17)
                {
                    NPC.velocity.Y = num17;
                }
                if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < num17 * 0.4)
                {
                    if (NPC.velocity.X < 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X - num18 * 1.1f;
                    }
                    else
                    {
                        NPC.velocity.X = NPC.velocity.X + num18 * 1.1f;
                    }
                }
                else if (NPC.velocity.Y == num17)
                {
                    if (NPC.velocity.X < num20)
                    {
                        NPC.velocity.X = NPC.velocity.X + num18;
                    }
                    else if (NPC.velocity.X > num20)
                    {
                        NPC.velocity.X = NPC.velocity.X - num18;
                    }
                }
                else if (NPC.velocity.Y > 4f)
                {
                    if (NPC.velocity.X < 0f)
                    {
                        NPC.velocity.X = NPC.velocity.X + num18 * 0.9f;
                    }
                    else
                    {
                        NPC.velocity.X = NPC.velocity.X - num18 * 0.9f;
                    }
                }
            }
            else
            {
                if (NPC.soundDelay == 0)
                {
                    float num24 = num22 / 40f;
                    if (num24 < 10f)
                    {
                        num24 = 10f;
                    }
                    if (num24 > 20f)
                    {
                        num24 = 20f;
                    }
                    NPC.soundDelay = (int)num24;
                    SoundEngine.PlaySound(SoundID.WormDig, NPC.position);
                }
                num22 = (float)Math.Sqrt(num20 * num20 + num21 * num21);
                float num25 = Math.Abs(num20);
                float num26 = Math.Abs(num21);
                float num27 = num17 / num22;
                num20 *= num27;
                num21 *= num27;
                if (((NPC.velocity.X > 0f && num20 > 0f) || (NPC.velocity.X < 0f && num20 < 0f)) && ((NPC.velocity.Y > 0f && num21 > 0f) || (NPC.velocity.Y < 0f && num21 < 0f)))
                {
                    if (NPC.velocity.X < num20)
                    {
                        NPC.velocity.X = NPC.velocity.X + num19;
                    }
                    else if (NPC.velocity.X > num20)
                    {
                        NPC.velocity.X = NPC.velocity.X - num19;
                    }
                    if (NPC.velocity.Y < num21)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + num19;
                    }
                    else if (NPC.velocity.Y > num21)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - num19;
                    }
                }
                if ((NPC.velocity.X > 0f && num20 > 0f) || (NPC.velocity.X < 0f && num20 < 0f) || (NPC.velocity.Y > 0f && num21 > 0f) || (NPC.velocity.Y < 0f && num21 < 0f))
                {
                    if (NPC.velocity.X < num20)
                    {
                        NPC.velocity.X = NPC.velocity.X + num18;
                    }
                    else if (NPC.velocity.X > num20)
                    {
                        NPC.velocity.X = NPC.velocity.X - num18;
                    }
                    if (NPC.velocity.Y < num21)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + num18;
                    }
                    else if (NPC.velocity.Y > num21)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - num18;
                    }
                    if (Math.Abs(num21) < num17 * 0.2 && ((NPC.velocity.X > 0f && num20 < 0f) || (NPC.velocity.X < 0f && num20 > 0f)))
                    {
                        if (NPC.velocity.Y > 0f)
                        {
                            NPC.velocity.Y = NPC.velocity.Y + num18 * 2f;
                        }
                        else
                        {
                            NPC.velocity.Y = NPC.velocity.Y - num18 * 2f;
                        }
                    }
                    if (Math.Abs(num20) < num17 * 0.2 && ((NPC.velocity.Y > 0f && num21 < 0f) || (NPC.velocity.Y < 0f && num21 > 0f)))
                    {
                        if (NPC.velocity.X > 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X + num18 * 2f;
                        }
                        else
                        {
                            NPC.velocity.X = NPC.velocity.X - num18 * 2f;
                        }
                    }
                }
                else if (num25 > num26)
                {
                    if (NPC.velocity.X < num20)
                    {
                        NPC.velocity.X = NPC.velocity.X + num18 * 1.1f;
                    }
                    else if (NPC.velocity.X > num20)
                    {
                        NPC.velocity.X = NPC.velocity.X - num18 * 1.1f;
                    }
                    if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < num17 * 0.5)
                    {
                        if (NPC.velocity.Y > 0f)
                        {
                            NPC.velocity.Y = NPC.velocity.Y + num18;
                        }
                        else
                        {
                            NPC.velocity.Y = NPC.velocity.Y - num18;
                        }
                    }
                }
                else
                {
                    if (NPC.velocity.Y < num21)
                    {
                        NPC.velocity.Y = NPC.velocity.Y + num18 * 1.1f;
                    }
                    else if (NPC.velocity.Y > num21)
                    {
                        NPC.velocity.Y = NPC.velocity.Y - num18 * 1.1f;
                    }
                    if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < num17 * 0.5)
                    {
                        if (NPC.velocity.X > 0f)
                        {
                            NPC.velocity.X = NPC.velocity.X + num18;
                        }
                        else
                        {
                            NPC.velocity.X = NPC.velocity.X - num18;
                        }
                    }
                }
            }
            NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;
        }

        public override bool PreKill()
        {
            return false;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<SerpentHead>()))
            {
                return false;
            }
            return true;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int x = 0; x < 5; x++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.IceDust>(), hit.HitDirection, -1f, 0, default, 1f);
            }
            if (NPC.life == 0)
            {
                for (int x = 0; x < 5; x++)
                {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.SnowDustLight>(), hit.HitDirection, -1f, 0, default, 1f);
                }

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity * 0.2f, Mod.Find<ModGore>("SZSGoreTail").Type, 1f);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D tex = TextureAssets.Npc[NPC.type].Value;

            switch ((int)NPC.ai[2])
            {
                case 0: break;
                case 1: tex = Mod.GetTexture("NPCs/Bosses/Serpent/Variants/CorruptTail"); break;
                case 2: tex = Mod.GetTexture("NPCs/Bosses/Serpent/Variants/CrimsonTail"); break;
                case 3: tex = Mod.GetTexture("NPCs/Bosses/Serpent/Variants/InfernoTail"); break;
                case 4: tex = Mod.GetTexture("NPCs/Bosses/Serpent/Variants/MireTail"); break;
                case 5: tex = Mod.GetTexture("NPCs/Bosses/Serpent/Variants/HallowTail"); break;
            }

            BaseDrawing.DrawTexture(spriteBatch, tex, 0, NPC, drawColor, true);
            return false;
        }
    }
}