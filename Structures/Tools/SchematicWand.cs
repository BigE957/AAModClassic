#if DEBUG
using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;
using System.IO;
using Terraria.ID;
using Terraria.ModLoader;

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
                else if (session.Phase == SchematicToolPhase.Idle)
                {
                    SchematicExporter.OutputDirectory = null;
                    string path = Main.SavePath + "\\ModSources\\AAModClassic\\Structures\\Schematics\\";
                    Place(path + "LostKeep.aasch", Main.MouseWorld.ToTileCoordinates(), anchor: SchematicAnchor.TopLeft);
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
                    Main.NewText($"Region set: {session.Region.Width}x{session.Region.Height} at ({session.Region.X}, {session.Region.Y}).", Color.LightGreen);
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
                var sw = Stopwatch.StartNew();
                SchematicData data = SchematicLoader.ReadFile(path);
                Main.NewText("Reading: " + sw.ElapsedMilliseconds);
                sw.Restart();
                ResolvedSchematic resolved = SchematicLoader.Resolve(data);
                Main.NewText("Resolving: " + sw.ElapsedMilliseconds);
                sw.Restart();

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