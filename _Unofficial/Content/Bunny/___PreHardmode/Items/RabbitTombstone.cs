using AAModClassic._Unofficial.Content.Bunny.___PreHardmode.NPCs;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Unofficial.Content.Bunny.___PreHardmode.Items
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

    public class RabbitTombstone_Tile : ModTile
    {
        private static int RandomStyleRange => 1;
        private static int RajahAtGraveTimeMinutes => 2;

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

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
                return;

            Point p = new(i, j);

            if (RabbitTombstoneSystem.RecentlyPlacedTombstones.TryGetValue(p, out var value))
            {
                if ((DateTime.Now - value.time).TotalMinutes < RajahAtGraveTimeMinutes && !NPC.AnyNPCs(ModContent.NPCType<AmbientRajahRabbit>()))
                    RabbitTombstoneSystem.SpawnRajahAtTombstone(p, value.data);
                RabbitTombstoneSystem.RecentlyPlacedTombstones.Remove(p);
            }
        }
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
