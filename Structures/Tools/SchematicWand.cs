#if DEBUG
using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic._Content.Inferno.World.Tiles;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic._Content.Mire.World.Tiles;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Conversions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using static AAModClassic.Utilities.WorldGenUtils;

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
                    bool place = true;

                    if (!place)
                    {
                        ushort dummyTile = TileID.ShimmerBlock;
                        ushort dummyWall = WallID.ShimmerBlockWall;

                        Point origin = Main.MouseWorld.ToTileCoordinates();

                        var warnings = new List<string>();

                        void build()
                        {
                            Dictionary<Color, int> colorToTile = new()
                            {
                                [new Color(0, 0, 255)] = ModContent.TileType<Depthstone_Tile>(),
                                [new Color(255, 128, 0)] = ModContent.TileType<Darkmud_Tile>(),
                                [new Color(0, 255, 255)] = ModContent.TileType<DepthMoss_Tile>(),
                                [new Color(0, 255, 0)] = ModContent.TileType<AbyssGrass_Tile>(),
                                [new Color(255, 0, 0)] = ModContent.TileType<AbyssWood_Tile>(),
                                [new Color(128, 0, 0)] = ModContent.TileType<AbyssWoodSolid_Tile>(),
                                [new Color(255, 255, 0)] = ModContent.TileType<AbyssVines_Tile>(),
                                [new Color(255, 0, 255)] = ModContent.TileType<AbyssLeaves_Tile>(),
                                [new Color(150, 150, 150)] = -2, //turn into air
                                [Color.Black] = -1 //don't touch when genning
                            };

                            Dictionary<Color, int> colorToWall = new()
                            {
                                [new Color(0, 0, 255)] = ModContent.WallType<DepthstoneWall_Wall>(),
                                [Color.Black] = -1 //don't touch when genning
                            };

                            TexGen gen = TexGen.GetTexGenerator(MireTexGenAssets.LakeTileData, colorToTile, MireTexGenAssets.LakeWallData, colorToWall, MireTexGenAssets.LakeLiquidData);

                            int genX = origin.X;
                            int genY = origin.Y;
                            gen.Generate(genX, genY, true, true);


                            WorldGen.PlaceObject(genX + 24, genY + 203, ModContent.TileType<HydraPod_Tile>());
                            WorldGen.PlaceObject(genX + 43, genY + 211, ModContent.TileType<HydraPod_Tile>());
                            WorldGen.PlaceObject(genX + 59, genY + 221, ModContent.TileType<HydraPod_Tile>());
                            WorldGen.PlaceObject(genX + 81, genY + 223, ModContent.TileType<HydraPod_Tile>());
                            WorldGen.PlaceObject(genX + 103, genY + 231, ModContent.TileType<HydraPod_Tile>());
                            WorldGen.PlaceObject(genX + 124, genY + 222, ModContent.TileType<HydraPod_Tile>());
                            WorldGen.PlaceObject(genX + 143, genY + 216, ModContent.TileType<HydraPod_Tile>());
                            WorldGen.PlaceObject(genX + 161, genY + 214, ModContent.TileType<HydraPod_Tile>());
                            WorldGen.PlaceObject(genX + 171, genY + 205, ModContent.TileType<HydraPod_Tile>());
                            NetMessage.SendObjectPlacement(-1, genX + 25, genY + 204, ModContent.TileType<HydraPod_Tile>(), 0, 0, -1, -1);
                            NetMessage.SendObjectPlacement(-1, genX + 43, genY + 211, ModContent.TileType<HydraPod_Tile>(), 0, 0, -1, -1);
                            NetMessage.SendObjectPlacement(-1, genX + 59, genY + 221, ModContent.TileType<HydraPod_Tile>(), 0, 0, -1, -1);
                            NetMessage.SendObjectPlacement(-1, genX + 81, genY + 223, ModContent.TileType<HydraPod_Tile>(), 0, 0, -1, -1);
                            NetMessage.SendObjectPlacement(-1, genX + 103, genY + 231, ModContent.TileType<HydraPod_Tile>(), 0, 0, -1, -1);
                            NetMessage.SendObjectPlacement(-1, genX + 124, genY + 222, ModContent.TileType<HydraPod_Tile>(), 0, 0, -1, -1);
                            NetMessage.SendObjectPlacement(-1, genX + 143, genY + 216, ModContent.TileType<HydraPod_Tile>(), 0, 0, -1, -1);
                            NetMessage.SendObjectPlacement(-1, genX + 161, genY + 214, ModContent.TileType<HydraPod_Tile>(), 0, 0, -1, -1);
                            NetMessage.SendObjectPlacement(-1, genX + 171, genY + 205, ModContent.TileType<HydraPod_Tile>(), 0, 0, -1, -1);
                        }

                        string saved = SchematicExporter.CaptureToFile(
                            build: build,
                            origin: origin,
                            width: MireTexGenAssets.LakeTileData.Width,
                            height: MireTexGenAssets.LakeTileData.Height,
                            dummyTile: dummyTile,
                            dummyWall: dummyWall,
                            fileName: "Mire_Lake",
                            warnings: warnings
                        );

                        Main.NewText($"Wrote {saved}", Color.LightGreen);
                        foreach (string w in warnings)
                            Main.NewText("Warning: " + w, Color.Orange);
                    }
                    else
                        Place(SchematicExporter.OutputDirectory + "/Mire_Lake.aasch", Main.MouseWorld.ToTileCoordinates(), anchor: SchematicAnchor.TopLeft);
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