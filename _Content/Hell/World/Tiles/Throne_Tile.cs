using AAModClassic._Content.Hell.___PreHardmode.NPCs.__Friendly;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;


namespace AAModClassic._Content.Hell.World.Tiles
{
	public class Throne_Tile : ModTile
	{
        public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = false;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = false;
            TileObjectData.newTile.Width = 4;
            TileObjectData.newTile.Height = 5;
            TileObjectData.newTile.Direction = TileObjectDirection.PlaceLeft;
            TileObjectData.newTile.StyleWrapLimit = 2;
            TileObjectData.newTile.StyleMultiplier = 2;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.CoordinateHeights = [ 16, 16, 16, 16, 16 ];
            TileObjectData.newTile.UsesCustomCanPlace = true;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
            TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
            TileObjectData.newAlternate.Direction = TileObjectDirection.PlaceRight;
            TileObjectData.addAlternate(1);
            TileObjectData.addTile(Type);
            DustType = DustID.WoodFurniture;
            MinPick = 500;
            MineResist = 10f;
            TileID.Sets.DisableSmartCursor[Type] = true;
			LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Throne of Evil");
            AddMapEntry(new Color(130, 110, 100));
        }
        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer && !NPC.AnyNPCs(ModContent.NPCType<LuciferSitting>()))
            {
                Vector2 worldPos = new Point(i + 2, j + 5).ToWorldCoordinates(i < Main.maxTilesX / 2 ? -2 : 2, 0);
                int n = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)worldPos.X, (int)worldPos.Y, ModContent.NPCType<LuciferSitting>());
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, n);
                }
            }
        }
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }

        public override bool CanReplace(int i, int j, int tileTypeBeingPlaced) => false;

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}