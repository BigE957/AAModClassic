using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.World.Tiles;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.World.Tiles;
using AAModClassic.Dusts;
using AAModClassic.UI.World;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Unreleased.Content.Inferno.World.Tiles
{
    //TODO: is this accurate to weeds in 1.4? can we make this support flower boots?
    public class InfernoFoliage_Tile : ModTile
	{
        public override void SetStaticDefaults()
        {
            Main.tileCut[Type] = true;
            Main.tileSolid[Type] = false;
            Main.tileNoAttach[Type] = true;
            Main.tileNoFail[Type] = true;
            Main.tileLavaDeath[Type] = true;
            Main.tileWaterDeath[Type] = true;
            Main.tileFrameImportant[Type] = true;

            TileID.Sets.ReplaceTileBreakUp[Type] = true;
            TileID.Sets.SwaysInWindBasic[Type] = true;
            TileID.Sets.IgnoredByGrowingSaplings[Type] = true;

            TileMaterials.SetForTileId(Type, TileMaterials._materialsByName["Plant"]);

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinateHeights = new int[]
            {
                20
            };
            TileObjectData.newTile.Style = 0;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.newTile.AnchorValidTiles = new int[]
            {
                ModContent.TileType<InfernoGrass_Tile>()
            };
            TileObjectData.addTile(Type);

            DustType = ModContent.DustType<RazeleafDust>();
            HitSound = SoundID.Grass;
            AddMapEntry(new Color(0, 32, 137));

            base.SetStaticDefaults();
        }

        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            Tile tileBelow = Framing.GetTileSafely(i, j + 1);
            int type = -1;

            if (tileBelow.HasTile)
            {
                type = tileBelow.TileType;
            }

            if (type == ModContent.TileType<InfernoGrass_Tile>())
            {
                return true;
            }

            WorldGen.KillTile(i, j);

            return true;
        }

        public override IEnumerable<Item> GetItemDrops(int i, int j)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
            {
                if (Main.tile[i, j].TileFrameX == 414)
                    yield return new Item(ModContent.ItemType<SolarMushroom>());
            }

            Vector2 worldPosition = new Vector2(i, j).ToWorldCoordinates();
            Player nearestPlayer = Main.player[Player.FindClosest(worldPosition, 16, 16)];
            if (nearestPlayer.active)
            {
                if (nearestPlayer.HeldItem.type == ItemID.Sickle)
                    yield return new Item(ItemID.Hay, Main.rand.Next(1, 2 + 1));
            }
        }

        public override bool IsTileBiomeSightable(int i, int j, ref Color sightColor)
        {
            sightColor = Color.OrangeRed;
            return true;
        }
    }
}