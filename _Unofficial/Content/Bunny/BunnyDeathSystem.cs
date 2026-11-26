using AAModClassic._Content.Bunny.__Hardmode.NPCs.__BossRajahRabbit;
using AAModClassic._Content.Bunny._PostMoonlord.NPCs.__BossRajahRabbitA;
using AAModClassic._Unofficial.Content.Bunny.___PreHardmode.Items;
using AAModClassic._Unofficial.Content.Bunny.___PreHardmode.NPCs;
using AAModClassic.Base;
using AAModClassic.Globals;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content.Bunny
{
    public enum RabbitDeathType : byte
    {
        KilledByPlayer,
        KilledByEnemy,
        KilledByCircumstance
    }

    public class BunnyDeathGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation) => entity.IsBunny();

        public RabbitDeathType DeathType = RabbitDeathType.KilledByCircumstance;
        public bool BeenHealed = false;
        public byte HostileIFrames = 0;

        public override bool PreAI(NPC npc)
        {
            if (HostileIFrames > 0)
            {
                if (--HostileIFrames <= 0)
                    npc.dontTakeDamageFromHostiles = false;
                else
                    npc.dontTakeDamageFromHostiles = true;
            }
            return true;
        }

        public override void OnKill(NPC npc)
        {
            RabbitTombstoneSystem.RegisterRabbitDeath(npc.Center, npc.type == NPCID.GoldBunny, DeathType);

            if (AAModGlobalNPC.Rajah != -1 || !npc.IsBunny())
                return;

            if (npc.GetGlobalNPC<BunnyDeathGlobalNPC>().DeathType != RabbitDeathType.KilledByPlayer)
                return;

            Player killer = FindClosestPlayerInRange(npc.Center, npc.width, npc.height, 1800f);
            RajahReputationPlayer repPlayer = killer.GetModPlayer<RajahReputationPlayer>();

            if (!repPlayer.Evil)
                return;

            if (!Main.hardMode && !repPlayer.BeyondSaving)
                return;

            bool awakened = NPC.downedMoonlord || NPCExtensions.BeenKilled<RajahRabbit>();

            if (awakened)
                BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.RajahGlobalInfo2") + killer.name.ToUpper() + "!", 107, 137, 179);
            else
                BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.Common.RajahGlobalInfo1"), 107, 137, 179);

            SpawnRajah(killer, new Vector2(npc.Center.X, npc.Center.Y - 2000), awakened);

            SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/Rajah"), new Vector2(npc.Center.X, npc.Center.Y - 2000));
        }

        public static void SpawnRajah(Player player, Vector2 npcCenter, bool awakened)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
                return;

            int RajahType = awakened ? ModContent.NPCType<RajahRabbitA>() : ModContent.NPCType<RajahRabbit>();

            if (NPC.AnyNPCs(RajahType))
                return;

            NPC npc = NPC.NewNPCDirect(NPC.GetBossSpawnSource(player.whoAmI), (int)npcCenter.X, (int)npcCenter.Y, RajahType, 0, 0, 0, player.whoAmI);
            npc.Center = npcCenter;
            npc.netUpdate = true;

            ChatUtils.Chat(npc.TypeName + " " + Language.GetTextValue("Mods.AAModClassic.Common.BosshasAwoken"), new Color(175, 75, 255));
        }

        public static Player FindClosestPlayerInRange(Vector2 Position, int Width, int Height, float range)
        {
            Player result = null;

            float shortestDistance = float.MaxValue;
            foreach (Player p in Main.ActivePlayers)
            {
                if (p.dead)
                    continue;

                float dist = MathF.Abs(p.position.X + (p.width / 2f) - (Position.X + (Width / 2f))) + Math.Abs(p.position.Y + (p.height / 2f) - (Position.Y + (Height / 2f)));
                if (dist > range)
                    continue;

                if (dist < shortestDistance)
                {
                    shortestDistance = dist;
                    result = p;
                }
            }

            return result;
        }
    }

    public class RabbitTombstoneSystem : ModSystem
    {
        internal readonly struct RabbitGraveData(Point point, bool golden, RabbitDeathType deathType)
        {
            internal readonly Point SpawnTile = point;
            internal readonly bool Golden = golden;
            internal readonly RabbitDeathType DeathType = deathType;
        }

        private static readonly List<RabbitGraveData> queuedRabbitTombstonePositions = [];
        private static int MaxHorizontalSearchRange => 10;

        private static int GraveSpawnCooldown = 0;

        internal static readonly Dictionary<Point, (RabbitGraveData data, DateTime time)> RecentlyPlacedTombstones = [];

        public override void OnWorldUnload()
        {
            queuedRabbitTombstonePositions.Clear();
            GraveSpawnCooldown = 0;
            RecentlyPlacedTombstones.Clear();
        }

        public override void PostUpdateEverything()
        {
            if (GraveSpawnCooldown > 0)
                GraveSpawnCooldown--;

            for (int i = queuedRabbitTombstonePositions.Count - 1; i >= 0; i--)
            {
                var data = queuedRabbitTombstonePositions[i];

                bool noPlayers = true;
                foreach (Player p in Main.ActivePlayers)
                {
                    if (p.DistanceSQ(data.SpawnTile.ToWorldCoordinates()) < 2073600) //1440^2
                    {
                        noPlayers = false;
                        break;
                    }
                }

                if (noPlayers)
                {
                    PlaceRabbitTombstone(data);
                    RecentlyPlacedTombstones.Add(data.SpawnTile, (data, DateTime.Now));
                    queuedRabbitTombstonePositions.RemoveAt(i);
                }
            }
        }

        public static void RegisterRabbitDeath(Vector2 deathPosition, bool golden, RabbitDeathType deathType)
        {
            if (!golden && (GraveSpawnCooldown > 0 || !Main.rand.NextBool(3)))
                return;

            if (TryFindTombstoneLocation(deathPosition.ToTileCoordinates(), out Point validTile))
            {
                queuedRabbitTombstonePositions.Add(new(validTile, golden, deathType));
                if (queuedRabbitTombstonePositions.Count > 5)
                    queuedRabbitTombstonePositions.RemoveAt(0);
                GraveSpawnCooldown = 1800;
                //Main.NewText("Tomb Time");
            }
        }

        private static Point ToAirTile(Point tilePos)
        {
            if (CollisionUtils.SurfaceTile(tilePos))
                tilePos.Y--;

            return tilePos;
        }

        private static bool TryFindTombstoneLocation(Point deathTile, out Point tilePos)
        {
            //bool deathTileWasSolid = CollisionUtils.SurfaceTile(deathTile);
            Point groundPoint = ToAirTile(CollisionUtils.FindSurfaceBelow(deathTile));
            bool groundValid = IsValidTombstoneLocation(groundPoint);

            if (groundValid)
            {
                tilePos = groundPoint;
                return true;
            }

            Point rightPoint = groundPoint;
            Point leftPoint = groundPoint;

            for (int offset = 1; offset <= MaxHorizontalSearchRange; offset++)
            {
                rightPoint.X++;
                rightPoint = ToAirTile(CollisionUtils.FindSurfaceAround(rightPoint));
                bool rightValid = IsValidTombstoneLocation(rightPoint);
                if (rightValid)
                {
                    tilePos = rightPoint;
                    return true;
                }

                leftPoint.X--;
                leftPoint = ToAirTile(CollisionUtils.FindSurfaceAround(leftPoint));
                bool leftValid = IsValidTombstoneLocation(leftPoint);
                if (leftValid)
                {
                    tilePos = leftPoint;
                    return true;
                }
            }

            tilePos = Point.Zero;
            return false;
        }

        private static bool IsValidTombstoneLocation(Point tilePos)
        {
            int tileType = ModContent.TileType<RabbitTombstone_Tile>();
            return TileObject.CanPlace(tilePos.X, tilePos.Y, tileType, 0, 0, out _);
        }

        private static void PlaceRabbitTombstone(RabbitGraveData data)
        {
            Point tilePos = data.SpawnTile;
            int tileType = ModContent.TileType<RabbitTombstone_Tile>();

            if (!TileObject.CanPlace(tilePos.X, tilePos.Y, tileType, 0, 0, out TileObject objectData))
                return;

            if (!TileObject.Place(objectData))
                return;

            NetMessage.SendObjectPlacement(-1, tilePos.X, tilePos.Y, objectData.type, objectData.style, objectData.alternate, objectData.random, 0);
            //SoundEngine.PlaySound(SoundID.Dig, new Vector2(tilePos.X * 16f, tilePos.Y * 16f));

            int signID = Sign.ReadSign(tilePos.X, tilePos.Y);
            if (signID >= 0)
            {
                Sign.TextSign(signID, GenerateTombstoneText());
                NetMessage.SendData(MessageID.ReadSign, -1, -1, null, signID, 0f, (int)(byte)new BitsByte(b1: true));
            }
        }

        private static int NameCount => 13;
        private static int IntroductionCount => 7;
        private static int QuoteCount => 8;

        private static string GenerateTombstoneText()
        {
            string localizationRoot = "Mods.AAModClassic.Tiles.RabbitTombstone";

            string name = Language.GetTextValue($"{localizationRoot}.Names.{Main.rand.Next(NameCount)}");
            string introduction = Language.GetTextValue($"{localizationRoot}.Introductions.{Main.rand.Next(IntroductionCount)}", name);
            string quote = Language.GetTextValue($"{localizationRoot}.Quotes.{Main.rand.Next(QuoteCount)}");
            DateTime now = DateTime.Now;
            string date = now.ToString("D");
            if (GameCulture.FromCultureName(GameCulture.CultureName.English).IsActive)
                date = now.ToString("MMMM d, yyy");

            return $"{introduction}\n{quote}\n{date}";
        }

        internal static void SpawnRajahAtTombstone(Point p, RabbitGraveData data)
        {
            Vector2 spawnPos = p.ToWorldCoordinates(16, 16);
            spawnPos += Vector2.UnitX * (Main.rand.NextBool() ? -180 : 180);
            if (NPC.downedMoonlord && !NPCExtensions.BeenKilled<RajahRabbitA>())
            {
                //Natural Champion Rajah (maybe mourning rajah based on rabbits killed)
                NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<AmbientRajahRabbit>(), ai0: (float)data.DeathType);
            }
            else if (Main.hardMode && !NPCExtensions.BeenKilled<RajahRabbit>())
            {
                //Natural Base Rajah (maybe mourning rajah based on rabbits killed)
                NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<AmbientRajahRabbit>(), ai0: (float)data.DeathType);
            }
            else if (!Main.hardMode)
            {
                NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<AmbientRajahRabbit>(), ai0: (float)data.DeathType);
            }
        }
    }
}
