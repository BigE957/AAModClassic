using AAModClassic._Content._Dev.__Hardmode.Items.Accessories;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration.RazewoodFurniture;
using AAModClassic._Content.Inferno.World.Tiles;
using AAModClassic._Content.Terrarium.World.Tiles;
using AAModClassic._Unreleased.Content.LostKeep._Hardmode.NPCs.__BossBiomiteCore;
using AAModClassic._Unreleased.Content.LostKeep.World.Tiles;
using AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Keep;
using AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Terra;
using AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Paintings;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAModClassic._Unreleased.Content.LostKeep.World.Biomes
{
    internal class LostKeepTexGenAssets : ModSystem
    {
        internal static TexGenData KeepTileData;
        internal static TexGenData KeepWallData;
        internal static TexGenData KeepSlopeData;
        internal static TexGenData KeepPlatformData;
        internal static TexGenData KeepObjectData;

        public override void OnModLoad()
        {
            KeepTileData = TexGen.GetTextureForGen("AAModClassic/_Unreleased/Content/LostKeep/World/Biomes/LostKeep");
            KeepWallData = TexGen.GetTextureForGen("AAModClassic/_Unreleased/Content/LostKeep/World/Biomes/LostKeepWall");
            KeepSlopeData = TexGen.GetTextureForGen("AAModClassic/_Unreleased/Content/LostKeep/World/Biomes/LostKeepSlope");
            KeepPlatformData = TexGen.GetTextureForGen("AAModClassic/_Unreleased/Content/LostKeep/World/Biomes/LostKeepPlatforms");
            KeepObjectData = TexGen.GetTextureForGen("AAModClassic/_Unreleased/Content/LostKeep/World/Biomes/LostKeepObjects");
        }
    }

    public class LostKeepGeneration : MicroBiome
	{
		private static bool ShouldAvoidLocation(Point p, bool leniant, bool desperate)
		{
			Tile tile = Framing.GetTileSafely(p);

			if (!leniant && (
				tile.TileType == TileID.JungleGrass ||
                tile.TileType == TileID.Hive))
            {
                //AAMod.instance.Logger.Info("Lost Keep Placement Failed, Encountered Tile of type: " + tile.TileType);
                return true;
            }

			if(!desperate && (
				tile.TileType == TileID.Sandstone ||
                tile.TileType == TileID.SnowBlock ||
                tile.TileType == TileID.IceBlock))
			{
                //AAMod.instance.Logger.Info("Lost Keep Placement Failed, Encountered Tile of type: " + tile.TileType);
                return true;
            }

            if (
				tile.TileType == TileID.Ash ||          
                tile.TileType == TileID.Crimstone ||
                tile.TileType == TileID.Ebonstone ||
                tile.TileType == TileID.LihzahrdBrick ||
				tile.TileType == TileID.BlueDungeonBrick ||
				tile.TileType == TileID.GreenDungeonBrick ||
				tile.TileType == TileID.PinkDungeonBrick)
			{
				//AAMod.instance.Logger.Info("Lost Keep Placement Failed, Encountered Tile of type: " + tile.TileType);
				return true;
			}

			return false;
		}

        public override bool Place(Point origin, StructureMap structures)
        {
            int attempts = 0;
            int maxAttempts = 20000;
            Point placementPoint = origin;
            bool placementSucceeded = false;
			int maxHeightUp = 550;
			if (WorldGenUtils.GetWorldSize() == 1)
				maxHeightUp = 300;

            do
            {
                bool canGenerateInLocation = true;

                if (!structures.CanPlace(new Rectangle(placementPoint.X, placementPoint.Y, LostKeepTexGenAssets.KeepTileData.Width, LostKeepTexGenAssets.KeepTileData.Height), WorldGenUtils.AllTilesAllowed, 0))
                {
                    canGenerateInLocation = false;
                }

                if (canGenerateInLocation)
                {
                    int fullX = placementPoint.X + LostKeepTexGenAssets.KeepTileData.Width;
                    int fullY = placementPoint.Y + LostKeepTexGenAssets.KeepTileData.Height;

                    for (int x = placementPoint.X; x < fullX; x++)
                    {
                        for (int y = placementPoint.Y; y < fullY; y++)
                        {
                            if (ShouldAvoidLocation(new Point(x, y), attempts > 4000, attempts > 12500))
                            {
                                canGenerateInLocation = false;
                                break;
                            }
                        }
                        if (!canGenerateInLocation)
                            break;
                    }
                }

                if (canGenerateInLocation)
                {
                    AAMod.instance.Logger.Info("Lost Keep successfully placed after " + attempts + " attempts.");
                    origin = placementPoint;
                    placementSucceeded = true;
                    break;
                }

                int radius = (int)MathHelper.Lerp(200, 1600, attempts / (float)maxAttempts);
                int targetX = Math.Clamp(origin.X + WorldGen.genRand.Next(-radius, radius), 50, Main.maxTilesX - (50 + LostKeepTexGenAssets.KeepTileData.Width));
                int targetY = Main.maxTilesY - 450 - Main.rand.Next(0, maxHeightUp);
                placementPoint = new Point(targetX, targetY);

            } while (attempts++ < maxAttempts);

            if (!placementSucceeded)
            {
                AAMod.instance.Logger.Warn("Lost Keep placement failed after " + maxAttempts + " attempts.");
            }

            WorldGenUtils.AddProtectedStructure(new Rectangle(origin.X, origin.Y, LostKeepTexGenAssets.KeepTileData.Width, LostKeepTexGenAssets.KeepTileData.Height), 20);

            Dictionary<Color, int> ColorToTile = new Dictionary<Color, int>();
			ColorToTile[new(128, 128, 128)] = ModContent.TileType<KeepBrick_Tile>();
			ColorToTile[new(64, 64, 64)] = ModContent.TileType<TerraBrick_Tile>();
			ColorToTile[new(0, 128, 0)] = ModContent.TileType<TerraCrystalBack_Tile>();
			ColorToTile[new(0, 64, 0)] = ModContent.TileType<TerraPillar_Tile>();
			ColorToTile[new(128, 0, 0)] = ModContent.TileType<TerraWood_Tile>();
			ColorToTile[new(0, 255, 255)] = ModContent.TileType<PermeableTerraWood_Tile>();
			Color black = default(Color);
			ColorToTile[new(0, 0, 64)] = ModContent.TileType<TerraLeaves_Tile>();
			ColorToTile[new(64, 0, 0)] = ModContent.TileType<ScorchedShingles_Tile>();
			ColorToTile[new(255, 0, 255)] = ModContent.TileType<TerraVault_Tile>();
			ColorToTile[new(0, 0, 255)] = TileID.Glass;
			ColorToTile[new(255, 255, 255)] = -1;
			Color black3 = Color.Black;
			ColorToTile[black3] = -2;
			Dictionary<Color, int> colorToTile = ColorToTile;
			Dictionary<Color, int> dictionary2 = new Dictionary<Color, int>();
			dictionary2[new(0, 255, 0)] = ModContent.WallType<KeepBrick_Wall>();
			dictionary2[new(255, 0, 0)] = TileID.Banners;
			dictionary2[new(255, 0, 255)] = TileID.Dressers;
			dictionary2[new(0, 0, 255)] = TileID.Containers;
			dictionary2[new(255, 255, 255)] = -1;
			black = Color.Black;
			dictionary2[black] = -2;
			Dictionary<Color, int> colorToWall = dictionary2;
			WorldUtils.Gen(origin, new Shapes.Rectangle(280, 230), Actions.Chain((GenAction[])(object)new GenAction[3]
			{
				new WorldGenUtils.InWorld(),
				new Actions.SetLiquid(0, (byte)0),
				new Actions.SetSlope(0)
			}));
			TexGen texGenerator = TexGen.GetTexGenerator(LostKeepTexGenAssets.KeepTileData, colorToTile, LostKeepTexGenAssets.KeepWallData, colorToWall, null, LostKeepTexGenAssets.KeepSlopeData);
			int placeX = origin.X;
			int placeY = origin.Y;

            AAWorld_Unreleased.lostKeepOrigin = new(placeX, placeY);

            texGenerator.Generate(placeX, placeY, silent: true, sync: true);
			Dictionary<Color, int> dictionary3 = new Dictionary<Color, int>();
			dictionary3[new(255, 0, 0)] = TileID.AmberGemspark;
			dictionary3[new(0, 255, 0)] = TileID.TopazGemspark;
			dictionary3[new(255, 0, 255)] = TileID.AmethystGemspark;
			dictionary3[Color.Black] = -1;
			Dictionary<Color, int> colorToTile2 = dictionary3;
			TexGen.GetTexGenerator(LostKeepTexGenAssets.KeepPlatformData, colorToTile2).Generate(placeX, placeY, silent: true, sync: true);
			for (int i = origin.X; i < origin.X + LostKeepTexGenAssets.KeepPlatformData.Width; i++)
			{
				for (int j = origin.Y; j < origin.Y + LostKeepTexGenAssets.KeepPlatformData.Height; j++)
				{
					if (Main.tile[i, j].TileType == TileID.AmberGemspark)
					{
						Main.tile[i, j].ClearTile();
						WorldGen.PlaceTile(i, j, ModContent.TileType<KeepPlatform_Tile>(), mute: true);
						WorldGen.SlopeTile(i, j, 1);
					}
					if (Main.tile[i, j].TileType == TileID.TopazGemspark)
					{
						Main.tile[i, j].ClearTile();
						WorldGen.PlaceTile(i, j, ModContent.TileType<KeepPlatform_Tile>(), mute: true);
						WorldGen.SlopeTile(i, j, 2);
					}
					if (Main.tile[i, j].TileType == TileID.AmethystGemspark)
					{
						Main.tile[i, j].ClearTile();
						WorldGen.PlaceTile(i, j, ModContent.TileType<KeepPlatform_Tile>(), mute: true);
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
			TexGen.GetTexGenerator(LostKeepTexGenAssets.KeepObjectData, colorToTile3).Generate(placeX, placeY, silent: true, sync: true);
			for (int i = origin.X; i < origin.X + LostKeepTexGenAssets.KeepObjectData.Width; i++)
			{
				for (int j = origin.Y; j < origin.Y + LostKeepTexGenAssets.KeepObjectData.Height; j++)
				{
					if (Main.tile[i, j].TileType == TileID.AmberGemspark)
					{
						Main.tile[i, j].ClearTile();
						WorldGen.PlaceTile(i, j, ModContent.TileType<KeepLamp_Tile>());
					}
					if (Main.tile[i, j].TileType == TileID.TopazGemspark)
					{
						Main.tile[i, j].ClearTile();
						WorldGen.PlaceTile(i, j, ModContent.TileType<KeepLantern_Tile>());
					}
					if (Main.tile[i, j].TileType == TileID.AmethystGemspark)
					{
						Main.tile[i, j].ClearTile();
						WorldGen.PlaceTile(i, j, ModContent.TileType<KeepChandelier_Tile>());
					}
					if (Main.tile[i, j].TileType == TileID.RubyGemspark)
					{
						Main.tile[i, j].ClearTile();
					}
					if (Main.tile[i, j].TileType == TileID.DiamondGemspark)
					{
						Main.tile[i, j].ClearTile();
						WorldGen.PlaceTile(i, j, ModContent.TileType<KeepTable_Tile>());
					}
					if (Main.tile[i, j].TileType == TileID.EmeraldGemspark)
					{
						Main.tile[i, j].ClearTile();
					}
					if (Main.tile[i, j].TileType == TileID.SapphireGemspark)
					{
						Main.tile[i, j].ClearTile();
						WorldGen.PlaceTile(i, j, ModContent.TileType<KeepBookcase_Tile>());
					}
					if (Main.tile[i, j].TileType == TileID.AmberGemsparkOff)
					{
						Main.tile[i, j].ClearTile();
						WorldGen.PlaceTile(i, j, ModContent.TileType<TerraStatue_Tile>());
					}
					if (Main.tile[i, j].TileType == TileID.TopazGemsparkOff)
					{
						Main.tile[i, j].ClearTile();
						WorldGen.PlaceTile(i, j, ModContent.TileType<TerraBed_Tile>());
					}
					if (Main.tile[i, j].TileType == TileID.AmethystGemsparkOff)
					{
						Main.tile[i, j].ClearTile();
						WorldGen.PlaceTile(i, j, ModContent.TileType<TerraBath_Tile>());
					}
					if (Main.tile[i, j].TileType == TileID.RubyGemsparkOff)
					{
						Main.tile[i, j].ClearTile();
						WorldGen.PlaceTile(i, j, ModContent.TileType<TerraSink_Tile>());
					}
					if (Main.tile[i, j].TileType == TileID.DiamondGemsparkOff)
					{
						Main.tile[i, j].ClearTile();
						WorldGen.PlaceTile(i, j, ModContent.TileType<TerraChandelier_Tile>());
					}
					if (Main.tile[i, j].TileType == TileID.EmeraldGemsparkOff)
					{
						Main.tile[i, j].ClearTile();
						WorldGen.PlaceTile(i, j, ModContent.TileType<TerraLantern_Tile>());
					}
					if (Main.tile[i, j].TileType == TileID.SapphireGemsparkOff)
					{
						Main.tile[i, j].ClearTile();
						WorldGen.PlaceTile(i, j, ModContent.TileType<RazewoodLantern_Tile>());
					}
					if (Main.tile[i, j].TileType == TileID.LivingFire)
					{
						Main.tile[i, j].ClearTile();
						WorldGen.PlaceTile(i, j, ModContent.TileType<RazewoodBed_Tile>());
					}
					if (Main.tile[i, j].TileType == TileID.LivingFrostFire)
					{
						Main.tile[i, j].ClearTile();
						WorldGen.PlaceTile(i, j, ModContent.TileType<RazewoodDresser_Tile>());
					}
					if (Main.tile[i, j].TileType == TileID.LivingCursedFire)
					{
						Main.tile[i, j].ClearTile();
						WorldGen.PlaceTile(i, j, 96);
					}
				}
			}
			WorldGen.PlaceTile(origin.X + 32, origin.Y + 137, ModContent.TileType<AleisterBook_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 36, origin.Y + 137, ModContent.TileType<AleisterBook_Tile>(), mute: true);
			WorldGen.PlaceChest(origin.X + 238, origin.Y + 104, (ushort)ModContent.TileType<TerraDresser_Tile>());
			WorldGen.PlaceTile(origin.X + 226, origin.Y + 104, ModContent.TileType<TerraBookcase_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 97, origin.Y + 60, ModContent.TileType<KeepDoor1_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 192, origin.Y + 60, ModContent.TileType<KeepDoor1_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 209, origin.Y + 60, ModContent.TileType<KeepDoor1_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 223, origin.Y + 60, ModContent.TileType<KeepDoor1_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 31, origin.Y + 152, ModContent.TileType<KeepDoor1_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 27, origin.Y + 162, ModContent.TileType<KeepDoor1_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 29, origin.Y + 183, ModContent.TileType<KeepDoor1_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 247, origin.Y + 152, ModContent.TileType<KeepDoor1_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 251, origin.Y + 162, ModContent.TileType<KeepDoor1_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 249, origin.Y + 183, ModContent.TileType<KeepDoor1_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 62, origin.Y + 80, ModContent.TileType<KeepDoor1_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 80, origin.Y + 130, ModContent.TileType<KeepDoor2_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 80, origin.Y + 158, ModContent.TileType<KeepDoor2_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 209, origin.Y + 130, ModContent.TileType<KeepDoor2_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 209, origin.Y + 158, ModContent.TileType<KeepDoor2_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 73, origin.Y + 130, ModContent.TileType<KeepDoor2_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 216, origin.Y + 102, ModContent.TileType<KeepDoor2_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 127, origin.Y + 114, ModContent.TileType<KeepDoor2S_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 160, origin.Y + 114, ModContent.TileType<KeepDoor2S_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 73, origin.Y + 109, ModContent.TileType<KeepDoor3_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 228, origin.Y + 108, ModContent.TileType<KeepDoor3_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 103, origin.Y + 174, ModContent.TileType<CoreDoor_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 186, origin.Y + 174, ModContent.TileType<CoreDoor_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 122, origin.Y + 181, ModContent.TileType<CoreDoor_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 162, origin.Y + 181, ModContent.TileType<CoreDoor_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 115, origin.Y + 79, ModContent.TileType<ShenPainting_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 127, origin.Y + 79, ModContent.TileType<CRajahPainting_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 139, origin.Y + 79, ModContent.TileType<IZPainting_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 151, origin.Y + 79, ModContent.TileType<SoCPainting_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 163, origin.Y + 79, ModContent.TileType<MushmadPainting_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 175, origin.Y + 79, ModContent.TileType<DecayPainting_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 103, origin.Y + 90, ModContent.TileType<FulgurusPainting_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 133, origin.Y + 90, ModContent.TileType<AkumaPainting_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 156, origin.Y + 90, ModContent.TileType<YamataPainting_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 186, origin.Y + 90, ModContent.TileType<ZeroPainting_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 103, origin.Y + 100, ModContent.TileType<DaedalusPainting_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 119, origin.Y + 100, ModContent.TileType<HotJPainting_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 135, origin.Y + 100, ModContent.TileType<AnubisPainting_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 154, origin.Y + 100, ModContent.TileType<ValkyriePainting_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 170, origin.Y + 100, ModContent.TileType<NKPainting_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 186, origin.Y + 100, ModContent.TileType<LuciferPainting_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 89, origin.Y + 110, ModContent.TileType<RajahPainting_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 105, origin.Y + 110, ModContent.TileType<UmbraPainting_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 121, origin.Y + 110, ModContent.TileType<GreedPainting_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 168, origin.Y + 110, ModContent.TileType<AcropolisPainting_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 184, origin.Y + 110, ModContent.TileType<SanguinePainting_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 200, origin.Y + 110, ModContent.TileType<ShipPainting_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 31, origin.Y + 87, ModContent.TileType<WizardPainting_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 30, origin.Y + 136, ModContent.TileType<TerraPainting_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 230, origin.Y + 102, ModContent.TileType<KingQueenPainting_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 223, origin.Y + 142, ModContent.TileType<JojoBagginsPainting_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 144, origin.Y + 134, ModContent.TileType<CoreActivator_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 140, origin.Y + 125, ModContent.TileType<Core_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 106, origin.Y + 129, ModContent.TileType<Core_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 174, origin.Y + 129, ModContent.TileType<Core_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 113, origin.Y + 151, ModContent.TileType<Core_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 167, origin.Y + 151, ModContent.TileType<Core_Tile>(), mute: true);
			WorldGen.PlaceTile(origin.X + 140, origin.Y + 156, ModContent.TileType<Core_Tile>(), mute: true);
			return true;
		}
	}
}