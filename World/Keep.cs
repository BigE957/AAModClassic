using System.Collections.Generic;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Items.Dev.DevTile.Tiles;
using AAModClassic.Tiles;
using AAModClassic.Tiles.Boss;
using AAModClassic.Tiles.Bricks;
using AAModClassic.Tiles.Decoration;
using AAModClassic.Tiles.Furniture.Keep;
using AAModClassic.Tiles.Furniture.Razewood;
using AAModClassic.Tiles.Furniture.Terra;
using AAModClassic.Tiles.Keep;
using AAModClassic.UI.WorldGen;
using AAModClassic.Walls;
using AAModClassic.Walls.Bricks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using static Terraria.WorldBuilding.Actions;

namespace AAModClassic.World;

public class Keep : MicroBiome
{
	public override bool Place(Point origin, StructureMap structures)
	{
		Mod instance = AAMod.instance;
		Dictionary<Color, int> ColorToTile = new Dictionary<Color, int>();
		ColorToTile[new(128, 128, 128)] = ModContent.TileType<KeepBrick>();
		ColorToTile[new(64, 64, 64)] = ModContent.TileType<TerraBrick>();
		ColorToTile[new(0, 128, 0)] = ModContent.TileType<TerraCrystalBack>();
		ColorToTile[new(0, 64, 0)] = ModContent.TileType<TerraPillar>();
		ColorToTile[new(128, 0, 0)] = ModContent.TileType<TerraWoodSolid>();
		ColorToTile[new(0, 255, 255)] = ModContent.TileType<TerraWood>();
		Color black = default(Color);
		ColorToTile[new(0, 0, 64)] = ModContent.TileType<TerraLeaves>();
		ColorToTile[new(64, 0, 0)] = ModContent.TileType<ScorchedShinglesS>();
		ColorToTile[new(255, 0, 255)] = ModContent.TileType<TerraVault>();
		ColorToTile[new(0, 0, 255)] = TileID.Glass;
		ColorToTile[new(255, 255, 255)] = -1;
		Color black3 = Color.Black;
		ColorToTile[black3] = -2;
		Dictionary<Color, int> colorToTile = ColorToTile;
		Dictionary<Color, int> dictionary2 = new Dictionary<Color, int>();
		dictionary2[new(0, 255, 0)] = ModContent.WallType<KeepBrickWall>();
		dictionary2[new(255, 0, 0)] = TileID.Banners;
		dictionary2[new(255, 0, 255)] = TileID.Dressers;
		dictionary2[new(0, 0, 255)] = TileID.Containers;
		dictionary2[new(255, 255, 255)] = -1;
		black = Color.Black;
		dictionary2[black] = -2;
		Dictionary<Color, int> colorToWall = dictionary2;
		WorldUtils.Gen(origin, new Shapes.Rectangle(280, 230), Actions.Chain((GenAction[])(object)new GenAction[3]
		{
			new InWorld(),
			(GenAction)new SetLiquid(0, (byte)0),
			(GenAction)new SetSlope(0)
		}));
		TexGen texGenerator = TexGen.GetTexGenerator(TexGenAssets.KeepTileData, colorToTile, TexGenAssets.KeepWallData, colorToWall, null, TexGenAssets.KeepSlopeData);
		int x = origin.X;
		int y = origin.Y;
		texGenerator.Generate(x, y, silent: true, sync: true);
		Dictionary<Color, int> dictionary3 = new Dictionary<Color, int>();
		dictionary3[new(255, 0, 0)] = TileID.AmberGemspark;
		dictionary3[new(0, 255, 0)] = TileID.TopazGemspark;
		dictionary3[new(255, 0, 255)] = TileID.AmethystGemspark;
		dictionary3[Color.Black] = -1;
		Dictionary<Color, int> colorToTile2 = dictionary3;
		TexGen.GetTexGenerator(TexGenAssets.KeepPlatformData, colorToTile2).Generate(x, y, silent: true, sync: true);
		for (int i = origin.X; i < origin.X + TexGenAssets.KeepPlatformData.Width; i++)
		{
			for (int j = origin.Y; j < origin.Y + TexGenAssets.KeepPlatformData.Height; j++)
			{
				if (Main.tile[i, j].TileType == TileID.AmberGemspark)
				{
					Main.tile[i, j].ClearTile();
					WorldGen.PlaceTile(i, j, ModContent.TileType<KeepPlatform>(), mute: true);
					WorldGen.SlopeTile(i, j, 1);
				}
				if (Main.tile[i, j].TileType == TileID.TopazGemspark)
				{
					Main.tile[i, j].ClearTile();
					WorldGen.PlaceTile(i, j, ModContent.TileType<KeepPlatform>(), mute: true);
					WorldGen.SlopeTile(i, j, 2);
				}
				if (Main.tile[i, j].TileType == TileID.AmethystGemspark)
				{
					Main.tile[i, j].ClearTile();
					WorldGen.PlaceTile(i, j, ModContent.TileType<KeepPlatform>(), mute: true);
				}
			}
		}
		Dictionary<Color, int> dictionary4 = new Dictionary<Color, int>();
		dictionary4[new(255, 0, 0)] = TileID.AmberGemspark;
		dictionary4[new(0, 255, 0)] = TileID.TopazGemspark;
		dictionary4[new(0, 0, 255)] = TileID.AmethystGemspark;
		dictionary4[new(128, 128, 128)] = TileID.RubyGemspark;
		dictionary4[new(64, 64, 64)] = TileID.DiamondGemspark;
		dictionary4[new(255, 255, 0)] = TileID.EmeraldGemspark;
		dictionary4[new(128, 0, 0)] = TileID.SapphireGemspark;
		dictionary4[new(0, 255, 255)] = TileID.AmberGemsparkOff;
		dictionary4[new(128, 128, 0)] = TileID.TopazGemsparkOff;
		dictionary4[new(0, 128, 128)] = TileID.AmethystGemsparkOff;
		dictionary4[new(128, 0, 128)] = TileID.RubyGemsparkOff;
		dictionary4[new(0, 0, 128)] = TileID.DiamondGemsparkOff;
		dictionary4[new(0, 128, 0)] = TileID.EmeraldGemsparkOff;
		dictionary4[new(64, 0, 64)] = TileID.SapphireGemsparkOff;
		dictionary4[new(0, 0, 64)] = TileID.LivingFire;
		dictionary4[new(64, 64, 0)] = TileID.LivingFrostFire;
		dictionary4[new(64, 0, 0)] = TileID.LivingCursedFire;
		dictionary4[Color.Black] = -1;
		Dictionary<Color, int> colorToTile3 = dictionary4;
        TexGen.GetTexGenerator(TexGenAssets.KeepObjectData, colorToTile3).Generate(x, y, silent: true, sync: true);
		for (int i = origin.X; i < origin.X + TexGenAssets.KeepObjectData.Width; i++)
		{
			for (int j = origin.Y; j < origin.Y + TexGenAssets.KeepObjectData.Height; j++)
			{
				if (Main.tile[i, j].TileType == TileID.AmberGemspark)
				{
					Main.tile[i, j].ClearTile();
					WorldGen.PlaceTile(i, j, ModContent.TileType<KeepLamp>());
				}
				if (Main.tile[i, j].TileType == TileID.TopazGemspark)
				{
					Main.tile[i, j].ClearTile();
					WorldGen.PlaceTile(i, j, ModContent.TileType<KeepLantern>());
				}
				if (Main.tile[i, j].TileType == TileID.AmethystGemspark)
				{
					Main.tile[i, j].ClearTile();
					WorldGen.PlaceTile(i, j, ModContent.TileType<KeepChandelier>());
				}
				if (Main.tile[i, j].TileType == TileID.RubyGemspark)
				{
					Main.tile[i, j].ClearTile();
				}
				if (Main.tile[i, j].TileType == TileID.DiamondGemspark)
				{
					Main.tile[i, j].ClearTile();
					WorldGen.PlaceTile(i, j, ModContent.TileType<KeepTable>());
				}
				if (Main.tile[i, j].TileType == TileID.EmeraldGemspark)
				{
					Main.tile[i, j].ClearTile();
				}
				if (Main.tile[i, j].TileType == TileID.SapphireGemspark)
				{
					Main.tile[i, j].ClearTile();
					WorldGen.PlaceTile(i, j, ModContent.TileType<KeepBookcase>());
				}
				if (Main.tile[i, j].TileType == TileID.AmberGemsparkOff)
				{
					Main.tile[i, j].ClearTile();
					WorldGen.PlaceTile(i, j, ModContent.TileType<TerraStatue>());
				}
				if (Main.tile[i, j].TileType == TileID.TopazGemsparkOff)
				{
					Main.tile[i, j].ClearTile();
					WorldGen.PlaceTile(i, j, ModContent.TileType<TerraBed>());
				}
				if (Main.tile[i, j].TileType == TileID.AmethystGemsparkOff)
				{
					Main.tile[i, j].ClearTile();
					WorldGen.PlaceTile(i, j, ModContent.TileType<TerraBath>());
				}
				if (Main.tile[i, j].TileType == TileID.RubyGemsparkOff)
				{
					Main.tile[i, j].ClearTile();
					WorldGen.PlaceTile(i, j, ModContent.TileType<TerraSink>());
				}
				if (Main.tile[i, j].TileType == TileID.DiamondGemsparkOff)
				{
					Main.tile[i, j].ClearTile();
					WorldGen.PlaceTile(i, j, ModContent.TileType<TerraChandelier>());
				}
				if (Main.tile[i, j].TileType == TileID.EmeraldGemsparkOff)
				{
					Main.tile[i, j].ClearTile();
					WorldGen.PlaceTile(i, j, ModContent.TileType<TerraLantern>());
				}
				if (Main.tile[i, j].TileType == TileID.SapphireGemsparkOff)
				{
					Main.tile[i, j].ClearTile();
					WorldGen.PlaceTile(i, j, ModContent.TileType<RazewoodLantern>());
				}
				if (Main.tile[i, j].TileType == TileID.LivingFire)
				{
					Main.tile[i, j].ClearTile();
					WorldGen.PlaceTile(i, j, ModContent.TileType<RazewoodBed>());
				}
				if (Main.tile[i, j].TileType == TileID.LivingFrostFire)
				{
					Main.tile[i, j].ClearTile();
					WorldGen.PlaceTile(i, j, ModContent.TileType<RazewoodDresser>());
				}
				if (Main.tile[i, j].TileType == TileID.LivingCursedFire)
				{
					Main.tile[i, j].ClearTile();
					WorldGen.PlaceTile(i, j, 96);
				}
			}
		}
		WorldGen.PlaceTile(origin.X + 32, origin.Y + 137, ModContent.TileType<InvokerBookTile>(), mute: true);
		WorldGen.PlaceTile(origin.X + 36, origin.Y + 137, ModContent.TileType<InvokerBookTile>(), mute: true);
		WorldGen.PlaceChest(origin.X + 238, origin.Y + 104, (ushort)ModContent.TileType<TerraDresser>());
		WorldGen.PlaceTile(origin.X + 226, origin.Y + 104, ModContent.TileType<TerraBookcase>(), mute: true);
		WorldGen.PlaceTile(origin.X + 97, origin.Y + 60, ModContent.TileType<KeepDoor1>(), mute: true);
		WorldGen.PlaceTile(origin.X + 192, origin.Y + 60, ModContent.TileType<KeepDoor1>(), mute: true);
		WorldGen.PlaceTile(origin.X + 209, origin.Y + 60, ModContent.TileType<KeepDoor1>(), mute: true);
		WorldGen.PlaceTile(origin.X + 223, origin.Y + 60, ModContent.TileType<KeepDoor1>(), mute: true);
		WorldGen.PlaceTile(origin.X + 31, origin.Y + 152, ModContent.TileType<KeepDoor1>(), mute: true);
		WorldGen.PlaceTile(origin.X + 27, origin.Y + 162, ModContent.TileType<KeepDoor1>(), mute: true);
		WorldGen.PlaceTile(origin.X + 29, origin.Y + 183, ModContent.TileType<KeepDoor1>(), mute: true);
		WorldGen.PlaceTile(origin.X + 247, origin.Y + 152, ModContent.TileType<KeepDoor1>(), mute: true);
		WorldGen.PlaceTile(origin.X + 251, origin.Y + 162, ModContent.TileType<KeepDoor1>(), mute: true);
		WorldGen.PlaceTile(origin.X + 249, origin.Y + 183, ModContent.TileType<KeepDoor1>(), mute: true);
		WorldGen.PlaceTile(origin.X + 62, origin.Y + 80, ModContent.TileType<KeepDoor1>(), mute: true);
		WorldGen.PlaceTile(origin.X + 80, origin.Y + 130, ModContent.TileType<KeepDoor2>(), mute: true);
		WorldGen.PlaceTile(origin.X + 80, origin.Y + 158, ModContent.TileType<KeepDoor2>(), mute: true);
		WorldGen.PlaceTile(origin.X + 209, origin.Y + 130, ModContent.TileType<KeepDoor2>(), mute: true);
		WorldGen.PlaceTile(origin.X + 209, origin.Y + 158, ModContent.TileType<KeepDoor2>(), mute: true);
		WorldGen.PlaceTile(origin.X + 73, origin.Y + 130, ModContent.TileType<KeepDoor2>(), mute: true);
		WorldGen.PlaceTile(origin.X + 216, origin.Y + 102, ModContent.TileType<KeepDoor2>(), mute: true);
		WorldGen.PlaceTile(origin.X + 127, origin.Y + 114, ModContent.TileType<KeepDoor2S>(), mute: true);
		WorldGen.PlaceTile(origin.X + 160, origin.Y + 114, ModContent.TileType<KeepDoor2S>(), mute: true);
		WorldGen.PlaceTile(origin.X + 73, origin.Y + 109, ModContent.TileType<KeepDoor3>(), mute: true);
		WorldGen.PlaceTile(origin.X + 228, origin.Y + 108, ModContent.TileType<KeepDoor3>(), mute: true);
		WorldGen.PlaceTile(origin.X + 103, origin.Y + 174, ModContent.TileType<CoreDoor>(), mute: true);
		WorldGen.PlaceTile(origin.X + 186, origin.Y + 174, ModContent.TileType<CoreDoor>(), mute: true);
		WorldGen.PlaceTile(origin.X + 122, origin.Y + 181, ModContent.TileType<CoreDoor>(), mute: true);
		WorldGen.PlaceTile(origin.X + 162, origin.Y + 181, ModContent.TileType<CoreDoor>(), mute: true);
		WorldGen.PlaceTile(origin.X + 115, origin.Y + 79, ModContent.TileType<ShenPainting>(), mute: true);
		WorldGen.PlaceTile(origin.X + 127, origin.Y + 79, ModContent.TileType<CRajahPainting>(), mute: true);
		WorldGen.PlaceTile(origin.X + 139, origin.Y + 79, ModContent.TileType<IZPainting>(), mute: true);
		WorldGen.PlaceTile(origin.X + 151, origin.Y + 79, ModContent.TileType<SoCPainting>(), mute: true);
		WorldGen.PlaceTile(origin.X + 163, origin.Y + 79, ModContent.TileType<MushmadPainting>(), mute: true);
		WorldGen.PlaceTile(origin.X + 175, origin.Y + 79, ModContent.TileType<DecayPainting>(), mute: true);
		WorldGen.PlaceTile(origin.X + 103, origin.Y + 90, ModContent.TileType<FulgurusPainting>(), mute: true);
		WorldGen.PlaceTile(origin.X + 133, origin.Y + 90, ModContent.TileType<AkumaPainting>(), mute: true);
		WorldGen.PlaceTile(origin.X + 156, origin.Y + 90, ModContent.TileType<YamataPainting>(), mute: true);
		WorldGen.PlaceTile(origin.X + 186, origin.Y + 90, ModContent.TileType<ZeroPainting>(), mute: true);
		WorldGen.PlaceTile(origin.X + 103, origin.Y + 100, ModContent.TileType<DaedalusPainting>(), mute: true);
		WorldGen.PlaceTile(origin.X + 119, origin.Y + 100, ModContent.TileType<HotJPainting>(), mute: true);
		WorldGen.PlaceTile(origin.X + 135, origin.Y + 100, ModContent.TileType<AnubisPainting>(), mute: true);
		WorldGen.PlaceTile(origin.X + 154, origin.Y + 100, ModContent.TileType<ValkyriePainting>(), mute: true);
		WorldGen.PlaceTile(origin.X + 170, origin.Y + 100, ModContent.TileType<NKPainting>(), mute: true);
		WorldGen.PlaceTile(origin.X + 186, origin.Y + 100, ModContent.TileType<LuciferPainting>(), mute: true);
		WorldGen.PlaceTile(origin.X + 89, origin.Y + 110, ModContent.TileType<RajahPainting>(), mute: true);
		WorldGen.PlaceTile(origin.X + 105, origin.Y + 110, ModContent.TileType<UmbraPainting>(), mute: true);
		WorldGen.PlaceTile(origin.X + 121, origin.Y + 110, ModContent.TileType<GreedPainting>(), mute: true);
		WorldGen.PlaceTile(origin.X + 168, origin.Y + 110, ModContent.TileType<AcropolisPainting>(), mute: true);
		WorldGen.PlaceTile(origin.X + 184, origin.Y + 110, ModContent.TileType<SanguinePainting>(), mute: true);
		WorldGen.PlaceTile(origin.X + 200, origin.Y + 110, ModContent.TileType<ShipPainting>(), mute: true);
		WorldGen.PlaceTile(origin.X + 31, origin.Y + 87, ModContent.TileType<WizardPainting>(), mute: true);
		WorldGen.PlaceTile(origin.X + 30, origin.Y + 136, ModContent.TileType<TerraPainting>(), mute: true);
		WorldGen.PlaceTile(origin.X + 230, origin.Y + 102, ModContent.TileType<KingQueenPainting>(), mute: true);
		WorldGen.PlaceTile(origin.X + 223, origin.Y + 142, ModContent.TileType<JojoBagginsPainting>(), mute: true);
		WorldGen.PlaceTile(origin.X + 144, origin.Y + 134, ModContent.TileType<CoreActivator>(), mute: true);
		WorldGen.PlaceTile(origin.X + 140, origin.Y + 125, ModContent.TileType<Core>(), mute: true);
		WorldGen.PlaceTile(origin.X + 106, origin.Y + 129, ModContent.TileType<Core>(), mute: true);
		WorldGen.PlaceTile(origin.X + 174, origin.Y + 129, ModContent.TileType<Core>(), mute: true);
		WorldGen.PlaceTile(origin.X + 113, origin.Y + 151, ModContent.TileType<Core>(), mute: true);
		WorldGen.PlaceTile(origin.X + 167, origin.Y + 151, ModContent.TileType<Core>(), mute: true);
		WorldGen.PlaceTile(origin.X + 140, origin.Y + 156, ModContent.TileType<Core>(), mute: true);
		return true;
	}

	public static int GetWorldSize()
	{
		if (Main.maxTilesX == 4200)
		{
			return 1;
		}
		if (Main.maxTilesX == 6400)
		{
			return 2;
		}
		if (Main.maxTilesX == 8400)
		{
			return 3;
		}
		return 1;
	}
}
