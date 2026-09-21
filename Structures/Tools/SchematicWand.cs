#if DEBUG
using AAModClassic._Content._Dev.__Hardmode.Items.Accessories;
using AAModClassic._Content.Acropolis._PostMoonlord.Items.Materials;
using AAModClassic._Content.Acropolis._PostMoonlord.Items.Tiles.Decoration;
using AAModClassic._Content.Acropolis.World.Biomes;
using AAModClassic._Content.Acropolis.World.Tiles;
using AAModClassic._Content.Hoard.World.Biomes;
using AAModClassic._Content.Hoard.World.Tiles;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration.RazewoodFurniture;
using AAModClassic._Content.Inferno.World.Tiles;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic._Content.Mire.World.Tiles;
using AAModClassic._Content.Terrarium.World.Biomes;
using AAModClassic._Content.Terrarium.World.Tiles;
using AAModClassic._Unreleased.Content.LostKeep._Hardmode.NPCs.__BossBiomiteCore;
using AAModClassic._Unreleased.Content.LostKeep.World.Biomes;
using AAModClassic._Unreleased.Content.LostKeep.World.Tiles;
using AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Keep;
using AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Furniture.Terra;
using AAModClassic._Unreleased.Content.LostKeep.World.Tiles.Paintings;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAModClassic.Structures.Tools
{
    public class SchematicWand : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.MulticolorWrench;

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.autoReuse = false;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Purple;
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player) => Main.netMode == NetmodeID.SinglePlayer;

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI != Main.myPlayer)
                return true;

            SchematicEditSession session = SchematicToolSystem.Session;
            if (session == null)
                return true;

            if (player.altFunctionUse == 2)
            {
                if (session.Phase == SchematicToolPhase.AwaitingSecondCorner)
                {
                    session.Cancel();
                    Main.NewText("Schematic selection cancelled.", Color.Orange);
                }
                else if(session.Phase == SchematicToolPhase.Idle)
                {
                    SchematicExporter.OutputDirectory = null;
                    bool place = false;

                    if (!place)
                    {
                        ushort dummyTile = TileID.ShimmerBlock;
                        ushort dummyWall = WallID.ShimmerBlockWall;

                        Point origin = Main.MouseWorld.ToTileCoordinates();

                        var warnings = new List<string>();

                        void build()
                        {
                            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
                            {
                                [new Color(255, 0, 0)] = -2,
                                [new Color(255, 255, 255)] = -2, //turn into air
                                [Color.Black] = -1 //don't touch when genning		
                            };

                            TexGen gen = TexGen.GetTexGenerator(HoardTexGenAssets.HoardDeletionData, colorToTile);
                            gen.Generate(origin.X, origin.Y, true, true);

                            colorToTile = new Dictionary<Color, int>
                            {
                                [new Color(255, 0, 0)] = ModContent.TileType<GreedStone_Tile>(),
                                [new Color(0, 0, 255)] = ModContent.TileType<GreedBrick_Tile>(),
                                [new Color(255, 255, 255)] = -2,
                                [Color.Black] = -1
                            };

                            Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
                            {
                                [new Color(255, 0, 0)] = -2,
                                [Color.Black] = -1
                            };

                            HashSet<int> protectedTiles = [
                                ModContent.TileType<GreedStone_Tile>(),
                                ModContent.TileType<GreedBrick_Tile>(),
                            ];

                            gen = TexGen.GetTexGenerator(HoardTexGenAssets.HoardTileData, colorToTile, HoardTexGenAssets.HoardWallData, colorToWall, unbreakableTiles: protectedTiles);
                            gen.Generate(origin.X, origin.Y, true, true);

                            HoardGeneration.HoardChest(origin.X + 19, origin.Y + 55);
                            HoardGeneration.HoardChest(origin.X + 38, origin.Y + 67, 1);
                            HoardGeneration.HoardChest(origin.X + 25, origin.Y + 34);
                            HoardGeneration.HoardChest(origin.X + 41, origin.Y + 27);
                            HoardGeneration.HoardChest(origin.X + 53, origin.Y + 38);
                            HoardGeneration.HoardChest(origin.X + 49, origin.Y + 54);
                            HoardGeneration.HoardChest(origin.X + 67, origin.Y + 70, 2);
                            HoardGeneration.HoardChest(origin.X + 79, origin.Y + 61);
                            HoardGeneration.HoardChest(origin.X + 72, origin.Y + 41);
                            HoardGeneration.HoardChest(origin.X + 95, origin.Y + 45);
                            HoardGeneration.HoardChest(origin.X + 107, origin.Y + 57);
                            HoardGeneration.HoardChest(origin.X + 121, origin.Y + 33);
                            HoardGeneration.HoardChest(origin.X + 131, origin.Y + 48, 3);
                            HoardGeneration.HoardChest(origin.X + 130, origin.Y + 69);

                            WorldGen.PlaceObject(origin.X + 80, origin.Y + 88, ModContent.TileType<GreedAltar_Tile>());
                            NetMessage.SendObjectPlacement(-1, origin.X + 80, origin.Y + 88, ModContent.TileType<GreedAltar_Tile>(), 0, 0, -1, -1);
                        }

                        string saved = SchematicExporter.CaptureToFile(
                            build: build,
                            origin: origin,
                            width: HoardTexGenAssets.HoardTileData.Width,
                            height: HoardTexGenAssets.HoardTileData.Height,
                            dummyTile: dummyTile,
                            dummyWall: dummyWall,
                            fileName: "Hoard",
                            warnings: warnings
                        );

                        Main.NewText($"Wrote {saved}", Color.LightGreen);
                        foreach (string w in warnings)
                            Main.NewText("Warning: " + w, Color.Orange);
                    }
                    else
                        Place(SchematicExporter.OutputDirectory + "/LostKeep.aasch", Main.MouseWorld.ToTileCoordinates(), anchor: SchematicAnchor.TopLeft);
                }
                return true;
            }

            Point tile = Main.MouseWorld.ToTileCoordinates();

            switch (session.Phase)
            {
                case SchematicToolPhase.Idle:
                    session.BeginSelection(tile);
                    Main.NewText($"Corner 1 set at ({session.FirstCorner.X}, {session.FirstCorner.Y}). Click the opposite corner.", Color.LightGreen);
                    break;

                case SchematicToolPhase.AwaitingSecondCorner:
                    session.CompleteSelection(tile);
                    Main.NewText($"Region set: {session.Region.Width}x{session.Region.Height} at ({session.Region.X}, {session.Region.Y}). Use /schem for commands.", Color.LightGreen);
                    break;
            }

            return true;
        }

        private static void Place(string path, Point pos, bool flip = false, SchematicAnchor anchor = SchematicAnchor.BottomCenter)
        {
            if (!File.Exists(path))
            {
                Main.NewText($"No schematic found at '{path}'.", Color.Orange);
                return;
            }

            try
            {
                SchematicData data = SchematicLoader.ReadFile(path);
                ResolvedSchematic resolved = SchematicLoader.Resolve(data);

                Rectangle area = SchematicPlacement.ResolveArea(pos, anchor, data.Width, data.Height);
                if (area.X < 0 || area.Y < 0 || area.Right > Main.maxTilesX || area.Bottom > Main.maxTilesY)
                {
                    Main.NewText("That placement would extend outside the world.", Color.Orange);
                    return;
                }

                var options = new SchematicPlaceOptions { Anchor = anchor, FlipHorizontal = flip };
                PlacedSchematic placed = SchematicPlacement.Place(resolved, pos, options);

                Main.NewText($"Placed '{Path.GetFileName(path)}' ({data.Width}x{data.Height}{(flip ? ", flipped" : string.Empty)}) at ({placed.Area.X}, {placed.Area.Y}).", Color.LightGreen);
                foreach (PlacedMarker marker in placed.Markers)
                    Main.NewText($"  marker '{marker.Name}' at ({marker.Area.X}, {marker.Area.Y}) {marker.Area.Width}x{marker.Area.Height}", Color.LightSkyBlue);
                foreach (string warning in placed.Warnings)
                    Main.NewText("Warning: " + warning, Color.Orange);
            }
            catch (Exception e)
            {
                Main.NewText("Placement failed: " + e.Message, Color.Red);
            }
        }
    }
}
#endif