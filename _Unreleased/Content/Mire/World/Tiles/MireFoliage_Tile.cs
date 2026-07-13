using AAModClassic._Content.Mire.___PreHardmode.Items.Consumables;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.World.Tiles;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;
using AAModClassic.UI.World;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Metadata;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Unreleased.Content.Mire.World.Tiles
{
    //TODO: is this accurate to weeds in 1.4? can we make this support flower boots?
    [LegacyName("Darkshroom_Tile", "BlackLotus_Tile")]
    public class MireFoliage_Tile : ModTile
	{
		public override void SetStaticDefaults()
		{
            Main.tileLighted[Type] = true;
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
                ModContent.TileType<MireGrass_Tile>(),
                ModContent.TileType<DepthMoss_Tile>()
            };
            TileObjectData.addTile(Type);

            DustType = ModContent.DustType<BogwoodDust>();
            HitSound = SoundID.Grass;
            AddMapEntry(new Color(0, 32, 137));

            base.SetStaticDefaults();
        }

        public override void EmitParticles(int i, int j, Tile tile, short tileFrameX, short tileFrameY, Color tileLight, bool visible)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && tileFrameX == 144 && Main.rand.Next(60) == 0)
            {
                int num37 = Dust.NewDust(new Vector2(i * 16, j * 16), 16, 16, ModContent.DustType<MireSporeDust>(), 0f, 0f, 250, default, 0.4f);
                Main.dust[num37].fadeIn = 0.7f;
            }
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Color tileLight = Lighting.GetColor(new(i, j));

            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && Main.tile[i, j].TileFrameX == 144)
            {
                float num17 = 1f + (270 - Main.mouseTextColor) / 400f;
                float num18 = 0.8f - (270 - Main.mouseTextColor) / 400f;
                r = 0.82f * num18;
                g = 0.21f * num17;
                b = 0.72f * num18;
            }
        }

        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            Tile tileBelow = Framing.GetTileSafely(i, j + 1);
            int type = -1;

            if (tileBelow.HasTile)
            {
                type = tileBelow.TileType;
            }

            if (type == ModContent.TileType<MireGrass_Tile>() || type == ModContent.TileType<DepthMoss_Tile>())
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
                if (Main.tile[i, j].TileFrameX == 144)
                    yield return new Item(ItemID.JungleSpores, Main.rand.Next(1, 3));
                else if (Main.tile[i, j].TileFrameX == 414)
                    yield return new Item(ModContent.ItemType<LunarMushroom>());
                else if (Main.tile[i, j].TileFrameX == 162)
                    yield return new Item(ModContent.ItemType<BlackLotus>());
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
            sightColor = Color.BlueViolet;
            return true;
        }
    }
}