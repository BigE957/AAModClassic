using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Unofficial.Bunny.Items
{
    public class RabbitTombstone : ModItem
    {
        public override void SetDefaults()
        {
            Item.autoReuse = false;
            Item.useTurn = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.rare = ItemRarityID.White;
            Item.useTime = 15;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 30;
            Item.height = 44;
            Item.value = 0;
            Item.createTile = ModContent.TileType<RabbitTombstone_Tile>();
            Item.placeStyle = 0;
        }
    }

    public class RabbitTombstoneSystem : ModSystem
    {
        internal readonly struct RabbitGraveData(Point point, bool golden)
        {
            internal readonly Point SpawnTile = point;
            internal readonly bool Golden = golden;
        }
        private static readonly List<RabbitGraveData> queuedRabbitTombstonePositions = [];
        private static int MaxHorizontalSearchRange => 10;

        private static int GraveSpawnCooldown = 0;

        public override void OnWorldUnload()
        {
            queuedRabbitTombstonePositions.Clear();
            GraveSpawnCooldown = 0;
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
                    if (p.DistanceSQ(data.SpawnTile.ToWorldCoordinates()) < 1440000) //1200^2
                    {
                        noPlayers = false;
                        break;
                    }
                }

                if (noPlayers)
                {
                    PlaceRabbitTombstone(data);
                    queuedRabbitTombstonePositions.RemoveAt(i);
                }
            }
        }

        public static void RegisterRabbitDeath(Vector2 deathPosition, bool golden)
        {
            if (!golden && (GraveSpawnCooldown > 0 || !Main.rand.NextBool(3)))
                return;

            if (TryFindTombstoneLocation(deathPosition.ToTileCoordinates(), out Point validTile))
            {
                queuedRabbitTombstonePositions.Add(new(validTile, golden));
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
            bool deathTileWasSolid = CollisionUtils.SurfaceTile(deathTile);
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
    }

    public class RabbitTombstone_Tile : ModTile
    {
        private static int RandomStyleRange => 1;

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            TileID.Sets.TileInteractRead[Type] = true;
            Main.tileSign[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.Height = 3;

            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 18 };
            TileObjectData.newTile.CoordinatePaddingFix = new Point16(0, 2);
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.DrawYOffset = 2;
            if (RandomStyleRange > 1)
                TileObjectData.newTile.RandomStyleRange = RandomStyleRange;

            TileObjectData.newTile.StyleHorizontal = true;

            TileObjectData.newTile.Origin = new Point16(0, TileObjectData.newTile.Height - 1);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);

            TileObjectData.addTile(Type);

            DustType = DustID.Stone;
            AddMapEntry(Color.Gray, Language.GetText("ItemName.Tombstone"));
        }

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
        public override void KillMultiTile(int i, int j, int frameX, int frameY) => Sign.KillSign(i, j);
    }

    public class RabbitTombstone_Projectile : ModProjectile
    {
        public override string Texture => FilePathUtils.TexturePath<RabbitTombstone>();

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.IsAGravestone[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.knockBack = 12f;
            Projectile.width = 24;
            Projectile.height = 24;
            Projectile.aiStyle = ProjAIStyleID.GraveMarker;
            Projectile.penetrate = -1;

            if (Main.getGoodWorld)
            {
                Projectile.friendly = true;
                Projectile.hostile = true;
            }
        }

        public override bool PreAI()
        {
            if (Projectile.velocity.Y == 0f)
                Projectile.velocity.X *= 0.98f;

            Projectile.rotation += Projectile.velocity.X * 0.1f;
            Projectile.velocity.Y += 0.2f;
            if (Projectile.owner != Main.myPlayer)
                return false;

            int potentialPlacementX = (int)((Projectile.position.X + (Projectile.width / 2)) / 16f);
            int potentialPlacementY = (int)((Projectile.position.Y + Projectile.height - 4f) / 16f);
            bool placementSuccessful = false;

            TileObject objectData = default;
            if (TileObject.CanPlace(potentialPlacementX, potentialPlacementY, ModContent.TileType<RabbitTombstone_Tile>(), 0/*TileStyle*/, Projectile.direction, out objectData))
                placementSuccessful = TileObject.Place(objectData);

            if (placementSuccessful)
            {
                NetMessage.SendObjectPlacement(-1, potentialPlacementX, potentialPlacementY, objectData.type, objectData.style, objectData.alternate, objectData.random, Projectile.direction);
                SoundEngine.PlaySound(SoundID.Dig, new Vector2(potentialPlacementX * 16, potentialPlacementY * 16));

                int signID = Sign.ReadSign(potentialPlacementX, potentialPlacementY);
                if (signID >= 0)
                {
                    Sign.TextSign(signID, Projectile.miscText);
                    NetMessage.SendData(MessageID.ReadSign, -1, -1, null, signID, 0f, (int)(byte)new BitsByte(b1: true));
                }

                Projectile.Kill();
            }
            return false;
        }
    }
}
