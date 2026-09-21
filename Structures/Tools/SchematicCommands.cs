#if DEBUG
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace AAModClassic.Structures.Tools
{
    /// <summary>
    /// Dev controls for the schematic tools:
    ///   /schem wand                                   give yourself the selection wand
    ///   /schem status                                 describe the current session
    ///   /schem cancel                                 discard the session (region, masks, markers)
    ///   /schem mark &lt;name&gt; [w h]                     add a marker at your feet, optionally w x h tiles
    ///   /schem export [name]                          write the region to an .aasch file
    ///   /schem list                                   list exported schematics
    ///   /schem place &lt;name&gt; [flip] [anchor]          place a schematic at your feet (anchor: tl tc tr cl c cr bl bc br; default bc)
    ///   /schem undo                                   restore the area covered by the last placement (last 5 are kept)
    /// </summary>
    public class SchematicCommand : ModCommand
    {
        private const int MaxUndo = 5;

        private static readonly List<(SchematicData Data, Point Corner)> UndoHistory = [];

        private static readonly Dictionary<string, SchematicAnchor> Anchors = new(StringComparer.OrdinalIgnoreCase)
        {
            ["tl"] = SchematicAnchor.TopLeft,
            ["tc"] = SchematicAnchor.TopCenter,
            ["tr"] = SchematicAnchor.TopRight,
            ["cl"] = SchematicAnchor.CenterLeft,
            ["c"] = SchematicAnchor.Center,
            ["cr"] = SchematicAnchor.CenterRight,
            ["bl"] = SchematicAnchor.BottomLeft,
            ["bc"] = SchematicAnchor.BottomCenter,
            ["br"] = SchematicAnchor.BottomRight
        };

        public override CommandType Type => CommandType.Chat;
        public override string Command => "schem";
        public override string Usage => "/schem <wand|status|cancel|mark <name> [w h]|export [name]|list|place <name> [flip] [anchor]|undo>";
        public override string Description => "Schematic editor dev tools.";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            SchematicEditSession session = SchematicToolSystem.Session;
            if (session == null)
            {
                caller.Reply("The schematic tools aren't loaded.", Color.Red);
                return;
            }

            string sub = args.Length > 0 ? args[0].ToLowerInvariant() : "status";

            switch (sub)
            {
                case "wand":
                    caller.Player.QuickSpawnItem(new EntitySource_Misc("SchematicTool"), ModContent.ItemType<SchematicWand>());
                    caller.Reply("Given a Schematic Wand.", Color.LightGreen);
                    break;

                case "status":
                    caller.Reply(Describe(session), Color.White);
                    break;

                case "cancel":
                    session.Cancel();
                    caller.Reply("Schematic session discarded.", Color.Orange);
                    break;

                case "mark":
                    Mark(caller, session, args);
                    break;

                case "export":
                    Export(caller, session, args);
                    break;

                case "list":
                    ListSchematics(caller);
                    break;

                case "place":
                    Place(caller, args);
                    break;

                case "undo":
                    Undo(caller);
                    break;

                default:
                    caller.Reply("Usage: " + Usage, Color.Orange);
                    break;
            }
        }

        private static string Describe(SchematicEditSession session)
        {
            return session.Phase switch
            {
                SchematicToolPhase.AwaitingSecondCorner => $"First corner at ({session.FirstCorner.X}, {session.FirstCorner.Y}); waiting for the opposite corner.",
                SchematicToolPhase.Editing => $"Region {session.Region.Width}x{session.Region.Height} at ({session.Region.X}, {session.Region.Y}); " + $"{session.Markers.Count} marker(s); name '{session.Name}'.",
                _ => "No selection. Use the wand (/schem wand) to pick two corners.",
            };
        }

        #region Editing
        private static void Mark(CommandCaller caller, SchematicEditSession session, string[] args)
        {
            if (session.Phase != SchematicToolPhase.Editing)
            {
                caller.Reply("Select a region first.", Color.Orange);
                return;
            }
            if (args.Length < 2)
            {
                caller.Reply("Usage: /schem mark <name> [w h]", Color.Orange);
                return;
            }

            int width = 1;
            int height = 1;
            if (args.Length >= 4 && (!int.TryParse(args[2], out width) || !int.TryParse(args[3], out height) || width < 1 || height < 1))
            {
                caller.Reply("Width and height must be positive whole numbers.", Color.Orange);
                return;
            }

            Point tile = caller.Player.Center.ToTileCoordinates();
            session.AddMarker(args[1], new Rectangle(tile.X, tile.Y, width, height));
            caller.Reply($"Marker '{args[1]}' added at ({tile.X}, {tile.Y}), {width}x{height}.", Color.LightGreen);
        }

        private static void Export(CommandCaller caller, SchematicEditSession session, string[] args)
        {
            if (session.Phase != SchematicToolPhase.Editing)
            {
                caller.Reply("Select a region first.", Color.Orange);
                return;
            }

            if (args.Length >= 2)
                session.Name = args[1];

            var warnings = new List<string>();
            try
            {
                string path = SchematicExporter.ExportToFile(session, warnings);
                caller.Reply($"Exported to {path}", Color.LightGreen);
            }
            catch (Exception e)
            {
                caller.Reply("Export failed: " + e.Message, Color.Red);
                return;
            }

            foreach (string warning in warnings)
                caller.Reply("Warning: " + warning, Color.Orange);
        }
        #endregion

        #region Placement testing
        private static void ListSchematics(CommandCaller caller)
        {
            string directory = SchematicExporter.OutputDirectory;
            if (!Directory.Exists(directory))
            {
                caller.Reply("Nothing exported yet.", Color.Orange);
                return;
            }

            string[] names = Directory.GetFiles(directory, "*.aasch").Select(Path.GetFileNameWithoutExtension).OrderBy(n => n).ToArray();
            caller.Reply(names.Length == 0 ? "Nothing exported yet." : "Schematics: " + string.Join(", ", names), Color.White);
        }

        private static void Place(CommandCaller caller, string[] args)
        {
            if (args.Length < 2)
            {
                caller.Reply("Usage: /schem place <name> [flip] [anchor: tl tc tr cl c cr bl bc br]", Color.Orange);
                return;
            }

            bool flip = false;
            SchematicAnchor anchor = SchematicAnchor.BottomCenter;
            for (int i = 2; i < args.Length; i++)
            {
                if (args[i].Equals("flip", StringComparison.OrdinalIgnoreCase))
                {
                    flip = true;
                }
                else if (Anchors.TryGetValue(args[i], out SchematicAnchor parsed))
                {
                    anchor = parsed;
                }
                else
                {
                    caller.Reply($"Unknown option '{args[i]}'.", Color.Orange);
                    return;
                }
            }

            string path = Path.Combine(SchematicExporter.OutputDirectory, args[1] + ".aasch");
            if (!File.Exists(path))
            {
                caller.Reply($"No schematic named '{args[1]}'. Try /schem list.", Color.Orange);
                return;
            }

            try
            {
                SchematicData data = SchematicLoader.ReadFile(path);
                ResolvedSchematic resolved = SchematicLoader.Resolve(data);

                Point pos = caller.Player.Bottom.ToTileCoordinates();
                Rectangle area = SchematicPlacement.ResolveArea(pos, anchor, data.Width, data.Height);
                if (area.X < 0 || area.Y < 0 || area.Right > Main.maxTilesX || area.Bottom > Main.maxTilesY)
                {
                    caller.Reply("That placement would extend outside the world.", Color.Orange);
                    return;
                }

                // Snapshot what's about to be overwritten so /schem undo can put it back.
                PushUndo(Capture(area), new Point(area.X, area.Y));

                var options = new SchematicPlaceOptions { Anchor = anchor, FlipHorizontal = flip };
                PlacedSchematic placed = SchematicPlacement.Place(resolved, pos, options);

                caller.Reply($"Placed '{args[1]}' ({data.Width}x{data.Height}{(flip ? ", flipped" : string.Empty)}) at ({placed.Area.X}, {placed.Area.Y}).", Color.LightGreen);
                foreach (PlacedMarker marker in placed.Markers)
                    caller.Reply($"  marker '{marker.Name}' at ({marker.Area.X}, {marker.Area.Y}) {marker.Area.Width}x{marker.Area.Height}", Color.LightSkyBlue);
                foreach (string warning in placed.Warnings)
                    caller.Reply("Warning: " + warning, Color.Orange);
            }
            catch (Exception e)
            {
                caller.Reply("Placement failed: " + e.Message, Color.Red);
            }
        }

        private static void Undo(CommandCaller caller)
        {
            if (UndoHistory.Count == 0)
            {
                caller.Reply("Nothing to undo.", Color.Orange);
                return;
            }

            (SchematicData data, Point corner) = UndoHistory[^1];
            UndoHistory.RemoveAt(UndoHistory.Count - 1);

            try
            {
                SchematicPlacement.Place(SchematicLoader.Resolve(data), corner, new SchematicPlaceOptions { Anchor = SchematicAnchor.TopLeft });
                caller.Reply($"Restored the {data.Width}x{data.Height} area at ({corner.X}, {corner.Y}).", Color.LightGreen);
            }
            catch (Exception e)
            {
                caller.Reply("Undo failed: " + e.Message, Color.Red);
            }
        }

        /// <summary> Reads an area back through the exporter, with no keep masks, so it can be restored exactly. </summary>
        private static SchematicData Capture(Rectangle area)
        {
            var temp = new SchematicEditSession();
            temp.BeginSelection(new Point(area.X, area.Y));
            temp.CompleteSelection(new Point(area.Right - 1, area.Bottom - 1));
            return SchematicExporter.Export(temp, []);
        }

        private static void PushUndo(SchematicData data, Point corner)
        {
            UndoHistory.Add((data, corner));
            if (UndoHistory.Count > MaxUndo)
                UndoHistory.RemoveAt(0);
        }
        #endregion
    }
}
#endif