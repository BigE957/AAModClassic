#if DEBUG
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace AAModClassic.Structures.Tools
{
    public class SchematicToolSystem : ModSystem
    {
        public static SchematicEditSession Session { get; private set; }
        public static SchematicToolSettings Settings { get; private set; }
        public static SchematicEditHistory History { get; private set; }

        private static readonly Color RegionColor = Color.White;
        private static readonly Color PreviewColor = Color.Yellow;
        private static readonly Color KeepTileColor = Color.DeepSkyBlue;
        private static readonly Color KeepWallColor = Color.Orange;
        private static readonly Color MarkerColor = Color.LimeGreen;

        private UserInterface _ui;
        private SchematicEditorState _editorState;
        private GameTime _lastUpdateGameTime;

        public override void OnModLoad()
        {
            Session = new SchematicEditSession();
            Settings = new SchematicToolSettings();
            History = new SchematicEditHistory();

            if (!Main.dedServ)
                _ui = new UserInterface();
        }

        public override void Unload()
        {
            Session = null;
            Settings = null;
            History = null;
            _ui = null;
            _editorState = null;
            SchematicRegionHandles.Reset();
            SchematicCanvasTool.Reset();
            SchematicCamera.Release();
        }

        public override void OnWorldUnload()
        {
            Session?.Cancel();
            History?.Clear();
            Settings?.Reset();
            SchematicRegionHandles.Reset();
            SchematicCanvasTool.Reset();
            SchematicCamera.Release();
        }

        #region Update / UI
        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateGameTime = gameTime;

            SchematicEditSession session = Session;
            if (session == null || _ui == null)
                return;

            switch (session.Phase)
            {
                case SchematicToolPhase.Idle:
                    if (_ui.CurrentState != null)
                        _ui.SetState(null);

                    History.Clear();
                    Settings.Reset();
                    SchematicRegionHandles.Reset();
                    SchematicCanvasTool.Reset();
                    SchematicPicker.Reset();
                    SchematicCamera.Release();
                    break;

                case SchematicToolPhase.AwaitingSecondCorner:
                    SchematicCamera.UpdateKeyboardPan();
                    break;

                case SchematicToolPhase.Editing:
                    if (_ui.CurrentState == null)
                    {
                        if (_editorState == null)
                        {
                            _editorState = new SchematicEditorState(session, Settings, History);
                            _editorState.Activate();
                        }
                        _ui.SetState(_editorState);
                    }

                    _ui.Update(gameTime);
                    bool overUi = _editorState.MouseOverPanel;

                    HandleHotkeys(session);

                    bool picking = SchematicPicker.Update(session, Settings, overUi);
                    if (picking)
                    {
                        SchematicRegionHandles.Reset();
                        SchematicCanvasTool.Reset();
                    }
                    else
                    {
                        SchematicCanvasTool.Update(session, Settings, History, overUi);
                        if (Settings.Tool == EditTool.Region)
                            SchematicRegionHandles.Update(session, overUi);
                        else
                            SchematicRegionHandles.Reset();
                    }

                    SchematicCamera.UpdateKeyboardPan();
                    break;
            }
        }

        private static void HandleHotkeys(SchematicEditSession session)
        {
            if (PlayerInput.WritingText)
                return;

            KeyboardState now = Main.keyState;
            KeyboardState old = Main.oldKeyState;

            bool ctrl = now.IsKeyDown(Keys.LeftControl) || now.IsKeyDown(Keys.RightControl);
            if (!ctrl)
                return;

            bool zPressed = now.IsKeyDown(Keys.Z) && !old.IsKeyDown(Keys.Z);
            bool yPressed = now.IsKeyDown(Keys.Y) && !old.IsKeyDown(Keys.Y);
            bool shift = now.PressingShift();

            if (zPressed && !shift)
                History.Undo(session);
            else if (yPressed || (zPressed && shift))
                History.Redo(session);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (index == -1)
                return;

            layers.Insert(index, new LegacyGameInterfaceLayer(
                "AAModClassic: Schematic Editor",
                () =>
                {
                    if (_ui?.CurrentState != null)
                        _ui.Draw(Main.spriteBatch, _lastUpdateGameTime ?? new GameTime());
                    return true;
                },
                InterfaceScaleType.UI));
        }

        public override void ModifyScreenPosition() => SchematicCamera.Apply();
        #endregion

        #region World overlay
        public override void PostDrawTiles()
        {
            SchematicEditSession session = Session;
            if (session == null || session.Phase == SchematicToolPhase.Idle)
                return;

            SpriteBatch sb = Main.spriteBatch;
            sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);

            Texture2D pixel = TextureAssets.MagicPixel.Value;

            if (session.Phase == SchematicToolPhase.AwaitingSecondCorner)
            {
                Point cursor = SchematicEditSession.ClampToWorld(Main.MouseWorld.ToTileCoordinates());
                DrawRegion(sb, pixel, SchematicEditSession.RectFromCorners(session.FirstCorner, cursor), PreviewColor);
            }
            else
            {
                DrawMask(sb, pixel, session.KeepTiles, KeepTileColor * 0.35f);
                DrawMask(sb, pixel, session.KeepWalls, KeepWallColor * 0.35f);
                DrawRegion(sb, pixel, session.Region, RegionColor);
                DrawMarkers(sb, pixel, session);
                DrawStandInMatches(sb, pixel, session);

                if (Settings.Picking != PickTarget.None)
                    DrawPickerCursor(sb, pixel);
                else if (Settings.Tool == EditTool.Region)
                    SchematicRegionHandles.Draw(sb, pixel, session.Region);
                else
                    DrawCanvasCursor(sb, pixel);
            }

            sb.End();
        }

        private static Rectangle ToScreen(Rectangle tiles) => new(
            (int)(tiles.X * 16 - Main.screenPosition.X),
            (int)(tiles.Y * 16 - Main.screenPosition.Y),
            tiles.Width * 16,
            tiles.Height * 16);

        private static void DrawOutline(SpriteBatch sb, Texture2D pixel, Rectangle screen, Color color, int thickness = 2)
        {
            sb.Draw(pixel, new Rectangle(screen.X, screen.Y, screen.Width, thickness), color);
            sb.Draw(pixel, new Rectangle(screen.X, screen.Bottom - thickness, screen.Width, thickness), color);
            sb.Draw(pixel, new Rectangle(screen.X, screen.Y, thickness, screen.Height), color);
            sb.Draw(pixel, new Rectangle(screen.Right - thickness, screen.Y, thickness, screen.Height), color);
        }

        private static void DrawRegion(SpriteBatch sb, Texture2D pixel, Rectangle tiles, Color color)
        {
            Rectangle screen = ToScreen(tiles);
            sb.Draw(pixel, screen, color * 0.08f);
            DrawOutline(sb, pixel, screen, color);
        }

        private static void DrawMask(SpriteBatch sb, Texture2D pixel, CellMask mask, Color color)
        {
            if (mask.IsEmpty)
                return;

            int startX = Math.Max(0, (int)(Main.screenPosition.X / 16f) - 1);
            int startY = Math.Max(0, (int)(Main.screenPosition.Y / 16f) - 1);
            int endX = Math.Min(Main.maxTilesX - 1, startX + Main.screenWidth / 16 + 3);
            int endY = Math.Min(Main.maxTilesY - 1, startY + Main.screenHeight / 16 + 3);

            for (int y = startY; y <= endY; y++)
            {
                int runStart = -1;
                for (int x = startX; x <= endX + 1; x++)
                {
                    bool on = x <= endX && mask[x, y];
                    if (on && runStart < 0)
                    {
                        runStart = x;
                    }
                    else if (!on && runStart >= 0)
                    {
                        sb.Draw(pixel, ToScreen(new Rectangle(runStart, y, x - runStart, 1)), color);
                        runStart = -1;
                    }
                }
            }
        }

        private static void DrawMarkers(SpriteBatch sb, Texture2D pixel, SchematicEditSession session)
        {
            foreach (SessionMarker marker in session.Markers)
            {
                Rectangle screen = ToScreen(marker.Area);
                sb.Draw(pixel, screen, MarkerColor * 0.25f);
                DrawOutline(sb, pixel, screen, MarkerColor, 1);
                Utils.DrawBorderString(sb, marker.Name, new Vector2(screen.X, screen.Y - 18), Color.White, 0.8f);
            }
        }

        private static void DrawCanvasCursor(SpriteBatch sb, Texture2D pixel)
        {
            if (SchematicCanvasTool.HoverVisible)
            {
                Point hover = SchematicCanvasTool.HoverCell;
                Rectangle cell = ToScreen(new Rectangle(hover.X, hover.Y, 1, 1));

                if (Settings.Tool == EditTool.Paint)
                {
                    Color color = Settings.Layer == PaintLayer.KeepTile ? KeepTileColor : KeepWallColor;
                    if (Settings.Mode == PaintMode.Fill)
                        sb.Draw(pixel, cell, color * 0.45f);
                    DrawOutline(sb, pixel, cell, color);
                }
                else
                {
                    DrawOutline(sb, pixel, cell, MarkerColor);
                }
            }

            if (SchematicCanvasTool.MarkerDragPreview.HasValue)
            {
                Rectangle screen = ToScreen(SchematicCanvasTool.MarkerDragPreview.Value);
                sb.Draw(pixel, screen, MarkerColor * 0.3f);
                DrawOutline(sb, pixel, screen, MarkerColor);
            }
        }

        private static readonly Color StandInColor = Color.Violet;

        private static void DrawStandInMatches(SpriteBatch sb, Texture2D pixel, SchematicEditSession session)
        {
            if (!session.KeepTileStandIn.HasValue && !session.KeepWallStandIn.HasValue)
                return;

            int startX = Math.Max(0, (int)(Main.screenPosition.X / 16f) - 1);
            int startY = Math.Max(0, (int)(Main.screenPosition.Y / 16f) - 1);
            int endX = Math.Min(Main.maxTilesX - 1, startX + Main.screenWidth / 16 + 3);
            int endY = Math.Min(Main.maxTilesY - 1, startY + Main.screenHeight / 16 + 3);

            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    Tile tile = Main.tile[x, y];
                    bool matchesTile = session.KeepTileStandIn.HasValue && tile.HasTile && tile.TileType == session.KeepTileStandIn.Value;
                    bool matchesWall = session.KeepWallStandIn.HasValue && tile.WallType != WallID.None && tile.WallType == session.KeepWallStandIn.Value;

                    if (matchesTile || matchesWall)
                        sb.Draw(pixel, ToScreen(new Rectangle(x, y, 1, 1)), StandInColor * 0.45f);
                }
            }
        }

        private static void DrawPickerCursor(SpriteBatch sb, Texture2D pixel)
        {
            if (!SchematicPicker.HoverCell.HasValue)
                return;

            Rectangle cell = ToScreen(new Rectangle(SchematicPicker.HoverCell.Value.X, SchematicPicker.HoverCell.Value.Y, 1, 1));
            DrawOutline(sb, pixel, cell, StandInColor);
        }
        #endregion
    }

    public sealed class SchematicEditorState(SchematicEditSession session, SchematicToolSettings settings, SchematicEditHistory history) : UIState
    {
        private const float PanelWidth = 260f;
        private const float PanelHeight = 596f;
        private const float BodyTop = 104f;
        private const float BodyHeight = 380f;
        private const int CancelConfirmFrames = 180;
        private const int MarkersPerPage = 4;

        private static readonly Color ButtonNormal = new Color(63, 82, 151) * 0.8f;
        private static readonly Color ButtonHover = new Color(90, 115, 205) * 0.95f;
        private static readonly Color ActiveNormal = new Color(45, 130, 85) * 0.9f;
        private static readonly Color ActiveHover = new Color(65, 165, 110) * 0.95f;
        private static readonly Color DisabledNormal = new Color(60, 60, 75) * 0.7f;
        private static readonly Color DisabledHover = new Color(75, 75, 95) * 0.8f;

        private enum Edge
        {
            Left,
            Right,
            Top,
            Bottom
        }

        private readonly SchematicEditSession _session = session;
        private readonly SchematicToolSettings _settings = settings;
        private readonly SchematicEditHistory _history = history;

        private UIPanel _panel;
        private UIElement _body;
        private SchematicTextBox _nameBox;
        private UIText _statusText;

        private readonly UIButton<string>[] _tabButtons = new UIButton<string>[3];
        private UIElement _regionTab;
        private UIElement _paintTab;
        private UIElement _markerTab;

        // Region tab
        private UIText _sizeText;
        private UIText _originText;
        private readonly UIText[] _edgeTexts = new UIText[4];

        // Paint tab
        private UIButton<string> _layerTileButton;
        private UIButton<string> _layerWallButton;
        private UIButton<string> _brushButton;
        private UIButton<string> _fillButton;
        private UIText _tileStandInText;
        private UIText _wallStandInText;

        // Markers tab
        private SchematicTextBox _markerBox;
        private UIElement _markerList;
        private UIText _pageText;
        private int _markerPage;
        private int _markerSignature = int.MinValue;

        // Bottom row
        private UIButton<string> _undoButton;
        private UIButton<string> _redoButton;
        private UIButton<string> _cancelButton;
        private double _cancelArmedUntil = -1d;

        public bool MouseOverPanel { get; private set; }

        #region Build
        public override void OnInitialize()
        {
            _panel = new UIPanel();
            _panel.Width.Set(PanelWidth, 0f);
            _panel.Height.Set(PanelHeight, 0f);
            _panel.Left.Set(-PanelWidth - 20f, 1f);
            _panel.Top.Set(80f, 0f);
            _panel.BackgroundColor = new Color(20, 24, 44) * 0.92f;
            _panel.SetPadding(0f);
            Append(_panel);

            AddText(_panel, "Schematic Editor", 10f, 8f, 0.95f);

            AddText(_panel, "Name", 10f, 42f, 0.8f);
            _nameBox = new SchematicTextBox(_session.Name) { Placeholder = "file name", MaxLength = 48 };
            _nameBox.Left.Set(56f, 0f);
            _nameBox.Top.Set(36f, 0f);
            _nameBox.Width.Set(PanelWidth - 64f, 0f);
            _nameBox.Height.Set(28f, 0f);
            _nameBox.Committed += text => _session.Name = string.IsNullOrWhiteSpace(text) ? "Untitled" : text.Trim();
            _panel.Append(_nameBox);

            string[] tabLabels = ["Region", "Paint", "Markers"];
            for (int i = 0; i < _tabButtons.Length; i++)
            {
                EditTool tool = (EditTool)i;
                _tabButtons[i] = AddButton(_panel, tabLabels[i], 8f + i * 82f, 72f, 80f, 26f, () => ShowTab(tool));
            }

            _body = new UIElement();
            _body.Left.Set(8f, 0f);
            _body.Top.Set(BodyTop, 0f);
            _body.Width.Set(PanelWidth - 16f, 0f);
            _body.Height.Set(BodyHeight, 0f);
            _panel.Append(_body);

            BuildRegionTab();
            BuildPaintTab();
            BuildMarkerTab();

            _statusText = AddText(_panel, string.Empty, 10f, 492f, 0.7f);

            _undoButton = AddButton(_panel, "Undo", 8f, 516f, 120f, 26f, Undo);
            _undoButton.BackgroundColor = ButtonNormal;
            _undoButton.AltPanelColor = DisabledNormal;
            _undoButton.HoverPanelColor = ButtonHover;
            _undoButton.AltHoverPanelColor = DisabledHover;
            _undoButton.UseAltColors = () => !_history.CanUndo;

            _redoButton = AddButton(_panel, "Redo", 132f, 516f, 120f, 26f, Redo);
            _redoButton.BackgroundColor = ButtonNormal;
            _redoButton.AltPanelColor = DisabledNormal;
            _redoButton.HoverPanelColor = ButtonHover;
            _redoButton.AltHoverPanelColor = DisabledHover;
            _redoButton.UseAltColors = () => !_history.CanRedo;

            AddButton(_panel, "Export", 8f, 552f, 120f, 34f, Export);
            _cancelButton = AddButton(_panel, "Cancel", 132f, 552f, 120f, 34f, OnCancelClicked);
            _cancelButton.BackgroundColor = new Color(150, 50, 50) * 0.8f;
            _cancelButton.HoverPanelColor = new Color(200, 70, 70) * 0.95f;
        }

        public override void OnActivate()
        {
            base.OnActivate();
            if (_panel == null)
                return;

            _markerPage = 0;
            _markerSignature = int.MinValue;
            RefreshPaintButtons();
            ShowTab(EditTool.Region);
        }

        private static UIElement NewTab()
        {
            var tab = new UIElement();
            tab.Width.Set(0f, 1f);
            tab.Height.Set(0f, 1f);
            return tab;
        }

        private void BuildRegionTab()
        {
            _regionTab = NewTab();

            _sizeText = AddText(_regionTab, string.Empty, 2f, 0f, 0.8f);
            _originText = AddText(_regionTab, string.Empty, 2f, 20f, 0.8f);
            AddText(_regionTab, "Drag handles or edges, or use +/-  (Shift = 10)", 2f, 42f, 0.62f);

            for (int i = 0; i < 4; i++)
            {
                Edge edge = (Edge)i;
                float rowY = 62f + i * 28f;
                _edgeTexts[i] = AddText(_regionTab, string.Empty, 2f, rowY + 4f, 0.8f);
                AddButton(_regionTab, "-", 150f, rowY, 42f, 24f, () => Nudge(edge, false));
                AddButton(_regionTab, "+", 196f, rowY, 42f, 24f, () => Nudge(edge, true));
            }

            AddText(_regionTab, "Camera  (arrow keys also pan)", 2f, 178f, 0.7f);
            AddButton(_regionTab, "TL", 0f, 198f, 56f, 24f, () => JumpToCorner(false, false));
            AddButton(_regionTab, "TR", 61f, 198f, 56f, 24f, () => JumpToCorner(true, false));
            AddButton(_regionTab, "BL", 122f, 198f, 56f, 24f, () => JumpToCorner(false, true));
            AddButton(_regionTab, "BR", 183f, 198f, 56f, 24f, () => JumpToCorner(true, true));
            AddButton(_regionTab, "Center", 0f, 228f, 118f, 24f, JumpToCenter);
            AddButton(_regionTab, "Follow player", 122f, 228f, 118f, 24f, SchematicCamera.Release);
        }

        private void BuildPaintTab()
        {
            _paintTab = NewTab();

            AddText(_paintTab, "Layer", 2f, 0f, 0.8f);
            _layerTileButton = AddButton(_paintTab, "Tile keep", 0f, 20f, 120f, 28f, () => SetLayer(PaintLayer.KeepTile));
            _layerWallButton = AddButton(_paintTab, "Wall keep", 124f, 20f, 120f, 28f, () => SetLayer(PaintLayer.KeepWall));

            AddText(_paintTab, "Tool", 2f, 58f, 0.8f);
            _brushButton = AddButton(_paintTab, "Brush", 0f, 78f, 120f, 28f, () => SetMode(PaintMode.Brush));
            _fillButton = AddButton(_paintTab, "Fill", 124f, 78f, 120f, 28f, () => SetMode(PaintMode.Fill));

            AddText(_paintTab, "Left click paints, right click erases.", 2f, 114f, 0.65f);
            AddText(_paintTab, "Fill stops at tile/wall changes and painted cells.", 2f, 130f, 0.62f);
            AddText(_paintTab, "Air in front of a wall counts as different.", 2f, 144f, 0.62f);

            AddButton(_paintTab, "Auto exterior (both layers)", 0f, 168f, 244f, 28f, AutoExterior);
            AddButton(_paintTab, "Clear this layer", 0f, 200f, 244f, 28f, ClearLayer);

            AddText(_paintTab, "Ctrl+Z / Ctrl+Y also undo and redo.", 2f, 236f, 0.65f);

            AddText(_paintTab, "Keep stand-ins (eyedropper)", 2f, 246f, 0.7f);

            _tileStandInText = AddText(_paintTab, string.Empty, 2f, 266f, 0.7f);
            AddButton(_paintTab, "Pick tile", 0f, 286f, 78f, 26f, () => StartPicking(PickTarget.TileStandIn));
            AddButton(_paintTab, "Clear", 82f, 286f, 78f, 26f, () => _session.KeepTileStandIn = null);

            _wallStandInText = AddText(_paintTab, string.Empty, 2f, 318f, 0.7f);
            AddButton(_paintTab, "Pick wall", 0f, 338f, 78f, 26f, () => StartPicking(PickTarget.WallStandIn));
            AddButton(_paintTab, "Clear", 82f, 338f, 78f, 26f, () => _session.KeepWallStandIn = null);
        }

        private void BuildMarkerTab()
        {
            _markerTab = NewTab();

            AddText(_markerTab, "Marker name", 2f, 0f, 0.8f);
            _markerBox = new SchematicTextBox(_settings.MarkerName) { Placeholder = "name", MaxLength = 32 };
            _markerBox.Left.Set(0f, 0f);
            _markerBox.Top.Set(20f, 0f);
            _markerBox.Width.Set(244f, 0f);
            _markerBox.Height.Set(28f, 0f);
            _markerTab.Append(_markerBox);

            AddText(_markerTab, "Click = point, drag = area, right-click removes.", 2f, 54f, 0.62f);

            _markerList = new UIElement();
            _markerList.Left.Set(0f, 0f);
            _markerList.Top.Set(76f, 0f);
            _markerList.Width.Set(244f, 0f);
            _markerList.Height.Set(112f, 0f);
            _markerTab.Append(_markerList);

            AddButton(_markerTab, "<", 0f, 196f, 44f, 24f, () => { _markerPage--; _markerSignature = int.MinValue; });
            _pageText = AddText(_markerTab, string.Empty, 78f, 201f, 0.75f);
            AddButton(_markerTab, ">", 200f, 196f, 44f, 24f, () => { _markerPage++; _markerSignature = int.MinValue; });
        }
        #endregion

        #region Update
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            MouseOverPanel = _panel.ContainsPoint(Main.MouseScreen);
            if (MouseOverPanel)
                Main.LocalPlayer.mouseInterface = true;

            _settings.MarkerName = _markerBox.Text;

            RefreshRegionTexts();

            if (_settings.Tool == EditTool.Markers)
            {
                int signature = MarkerSignature();
                if (signature != _markerSignature)
                {
                    _markerSignature = signature;
                    RebuildMarkerList();
                }
            }

            if (!_nameBox.Focused && _nameBox.Text != _session.Name)
                _nameBox.Text = _session.Name;

            if (_cancelArmedUntil >= 0d && Main.timeForVisualEffects > _cancelArmedUntil)
                DisarmCancel();
        }

        private void RefreshRegionTexts()
        {
            Rectangle r = _session.Region;

            _sizeText.SetText($"Size: {r.Width} x {r.Height}");
            _originText.SetText($"Top-left tile: ({r.X}, {r.Y})");

            _edgeTexts[(int)Edge.Left].SetText($"Left    {r.X}");
            _edgeTexts[(int)Edge.Right].SetText($"Right   {r.Right - 1}");
            _edgeTexts[(int)Edge.Top].SetText($"Top     {r.Y}");
            _edgeTexts[(int)Edge.Bottom].SetText($"Bottom  {r.Bottom - 1}");

            _tileStandInText.SetText(_session.KeepTileStandIn.HasValue ? $"Tile: type {_session.KeepTileStandIn.Value}" : "Tile: (none)");
            _wallStandInText.SetText(_session.KeepWallStandIn.HasValue ? $"Wall: type {_session.KeepWallStandIn.Value}" : "Wall: (none)");
        }

        private void RefreshPaintButtons()
        {
            SetActive(_layerTileButton, _settings.Layer == PaintLayer.KeepTile);
            SetActive(_layerWallButton, _settings.Layer == PaintLayer.KeepWall);
            SetActive(_brushButton, _settings.Mode == PaintMode.Brush);
            SetActive(_fillButton, _settings.Mode == PaintMode.Fill);
        }

        private void StartPicking(PickTarget target)
        {
            _settings.Picking = target;
            Main.NewText(target == PickTarget.TileStandIn
                ? "Click a placed tile to use as the Keep-Tile stand-in. Right-click to cancel."
                : "Click a tile with a wall to use as the Keep-Wall stand-in. Right-click to cancel.", Color.LightGreen);
        }

        private int MarkerSignature()
        {
            var hash = new HashCode();
            hash.Add(_session.Markers.Count);
            foreach (SessionMarker marker in _session.Markers)
            {
                hash.Add(marker.Name);
                hash.Add(marker.Area.X);
                hash.Add(marker.Area.Y);
                hash.Add(marker.Area.Width);
                hash.Add(marker.Area.Height);
            }
            return hash.ToHashCode();
        }

        private void RebuildMarkerList()
        {
            _markerList.RemoveAllChildren();

            int count = _session.Markers.Count;
            int pages = Math.Max(1, (count + MarkersPerPage - 1) / MarkersPerPage);
            _markerPage = Math.Clamp(_markerPage, 0, pages - 1);
            _pageText.SetText($"{_markerPage + 1} / {pages}   ({count})");

            if (count == 0)
                AddText(_markerList, "No markers yet.", 2f, 4f, 0.75f);

            for (int row = 0; row < MarkersPerPage; row++)
            {
                int index = _markerPage * MarkersPerPage + row;
                if (index >= count)
                    break;

                SessionMarker marker = _session.Markers[index];
                float y = row * 28f;

                AddText(_markerList, Truncate(marker.Name, 15), 2f, y + 5f, 0.75f);
                AddButton(_markerList, "Go", 148f, y, 44f, 24f, () => GoToMarker(marker));
                AddButton(_markerList, "X", 196f, y, 40f, 24f, () => RemoveMarker(marker));
            }

            _markerList.Recalculate();
        }
        #endregion

        #region Actions
        private void ShowTab(EditTool tool)
        {
            _markerBox?.Unfocus();
            _settings.Tool = tool;

            _body.RemoveAllChildren();
            _body.Append(tool == EditTool.Region ? _regionTab : tool == EditTool.Paint ? _paintTab : _markerTab);
            _body.Recalculate();

            for (int i = 0; i < _tabButtons.Length; i++)
                SetActive(_tabButtons[i], i == (int)tool);

            _markerSignature = int.MinValue;
        }

        private void SetLayer(PaintLayer layer)
        {
            _settings.Layer = layer;
            RefreshPaintButtons();
        }

        private void SetMode(PaintMode mode)
        {
            _settings.Mode = mode;
            RefreshPaintButtons();
        }

        private void Nudge(Edge edge, bool grow)
        {
            int amount = Main.keyState.PressingShift() ? 10 : 1;
            int delta = grow ? amount : -amount;

            Rectangle r = _session.Region;
            int left = r.X;
            int right = r.Right;
            int top = r.Y;
            int bottom = r.Bottom;

            switch (edge)
            {
                case Edge.Left: left = Math.Clamp(left - delta, 0, right - 1); break;
                case Edge.Right: right = Math.Clamp(right + delta, left + 1, Main.maxTilesX); break;
                case Edge.Top: top = Math.Clamp(top - delta, 0, bottom - 1); break;
                case Edge.Bottom: bottom = Math.Clamp(bottom + delta, top + 1, Main.maxTilesY); break;
            }

            _session.SetRegion(new Rectangle(left, top, right - left, bottom - top));
        }

        private void JumpToCorner(bool right, bool bottom)
        {
            Rectangle r = _session.Region;
            SchematicCamera.JumpTo(new Vector2((right ? r.Right : r.X) * 16f, (bottom ? r.Bottom : r.Y) * 16f));
        }

        private void JumpToCenter()
        {
            Rectangle r = _session.Region;
            SchematicCamera.JumpTo(new Vector2((r.X + r.Right) * 8f, (r.Y + r.Bottom) * 8f));
        }

        private static void GoToMarker(SessionMarker marker)
        {
            Rectangle a = marker.Area;
            SchematicCamera.JumpTo(new Vector2((a.X + a.Right) * 8f, (a.Y + a.Bottom) * 8f));
        }

        private void RemoveMarker(SessionMarker marker)
        {
            int index = _session.Markers.IndexOf(marker);
            if (index < 0)
                return;

            _session.Markers.RemoveAt(index);
            _history.Push(new MarkerRemovedAction(index, marker));
        }

        private void Undo() => SetStatus(_history.Undo(_session) ? "Undid last edit." : "Nothing to undo.");

        private void Redo() => SetStatus(_history.Redo(_session) ? "Redid edit." : "Nothing to redo.");

        private void AutoExterior()
        {
            var action = new PaintStrokeAction();
            int changed = SchematicPaintOps.AutoExterior(_session, action);
            if (action.Count > 0)
                _history.Push(action);

            SetStatus(changed == 0 ? "Auto exterior: nothing to fill." : $"Auto exterior: {changed} cells marked.");
        }

        private void ClearLayer()
        {
            var action = new PaintStrokeAction();
            int cleared = SchematicPaintOps.ClearLayer(_session, _settings.Layer, action);
            if (action.Count > 0)
                _history.Push(action);

            SetStatus(cleared == 0 ? "Layer is already empty." : $"Cleared {cleared} cells.");
        }

        private void Export()
        {
            _nameBox.Unfocus();
            _session.Name = string.IsNullOrWhiteSpace(_nameBox.Text) ? "Untitled" : _nameBox.Text.Trim();

            var warnings = new List<string>();
            try
            {
                string path = SchematicExporter.ExportToFile(_session, warnings);
                Main.NewText("Exported to " + path, Color.LightGreen);
                foreach (string warning in warnings)
                    Main.NewText("Warning: " + warning, Color.Orange);

                string suffix = warnings.Count == 0 ? string.Empty : $" ({warnings.Count} warning{(warnings.Count == 1 ? "" : "s")})";
                SetStatus($"Exported {_session.Name}.aasch{suffix}");
            }
            catch (Exception e)
            {
                Main.NewText("Export failed: " + e.Message, Color.Red);
                SetStatus("Export failed: " + e.Message);
            }
        }

        private void OnCancelClicked()
        {
            if (_cancelArmedUntil < 0d)
            {
                _cancelArmedUntil = Main.timeForVisualEffects + CancelConfirmFrames;
                _cancelButton.SetText("Sure?");
                return;
            }

            DisarmCancel();
            _session.Cancel();
            SchematicRegionHandles.Reset();
            SchematicCanvasTool.Reset();
            SchematicCamera.Release();
        }

        private void DisarmCancel()
        {
            _cancelArmedUntil = -1d;
            _cancelButton.SetText("Cancel");
        }
        #endregion

        #region Element helpers
        private static UIText AddText(UIElement parent, string text, float x, float y, float scale)
        {
            var element = new UIText(text, scale);
            element.Left.Set(x, 0f);
            element.Top.Set(y, 0f);
            parent.Append(element);
            return element;
        }

        private static UIButton<string> AddButton(UIElement parent, string text, float x, float y, float width, float height, Action onClick)
        {
            var button = new UIButton<string>(new(text));
            button.Left.Set(x, 0f);
            button.Top.Set(y, 0f);
            button.Width.Set(width, 0f);
            button.Height.Set(height, 0f);
            button.OnLeftClick += (evt, element) => onClick();
            parent.Append(button);
            return button;
        }

        private static void SetActive(UIButton<string> button, bool active)
        {
            button.BackgroundColor = active ? ActiveNormal : ButtonNormal;
            button.HoverPanelColor = active ? ActiveHover : ButtonHover;
        }

        private void SetStatus(string text) => _statusText.SetText(Truncate(text, 40));

        private static string Truncate(string text, int maxLength) => text.Length <= maxLength ? text : string.Concat(text.AsSpan(0, maxLength - 3), "...");
        #endregion
    }

    public enum RegionHandle
    {
        None,
        TopLeft,
        Top,
        TopRight,
        Right,
        BottomRight,
        Bottom,
        BottomLeft,
        Left
    }

    public static class SchematicRegionHandles
    {
        private const float HandleSizePx = 12f;
        private const float HitRadiusPx = 12f;
        private const int EdgeScrollMarginPx = 40;
        private const float EdgeScrollSpeed = 14f;

        private static readonly RegionHandle[] All =
        [
            RegionHandle.TopLeft, RegionHandle.Top, RegionHandle.TopRight, RegionHandle.Right,
            RegionHandle.BottomRight, RegionHandle.Bottom, RegionHandle.BottomLeft, RegionHandle.Left
        ];

        private static readonly RegionHandle[] Corners =
        [
            RegionHandle.TopLeft, RegionHandle.TopRight, RegionHandle.BottomRight, RegionHandle.BottomLeft
        ];

        private static bool _wasMouseDown;

        public static RegionHandle Hovered { get; private set; }
        public static RegionHandle Dragging { get; private set; }

        public static void Reset()
        {
            Hovered = RegionHandle.None;
            Dragging = RegionHandle.None;
        }

        public static Vector2 WorldPosition(Rectangle region, RegionHandle handle)
        {
            float left = region.X * 16f;
            float right = region.Right * 16f;
            float top = region.Y * 16f;
            float bottom = region.Bottom * 16f;
            float midX = (left + right) * 0.5f;
            float midY = (top + bottom) * 0.5f;

            return handle switch
            {
                RegionHandle.TopLeft => new Vector2(left, top),
                RegionHandle.Top => new Vector2(midX, top),
                RegionHandle.TopRight => new Vector2(right, top),
                RegionHandle.Right => new Vector2(right, midY),
                RegionHandle.BottomRight => new Vector2(right, bottom),
                RegionHandle.Bottom => new Vector2(midX, bottom),
                RegionHandle.BottomLeft => new Vector2(left, bottom),
                RegionHandle.Left => new Vector2(left, midY),
                _ => Vector2.Zero
            };
        }

        public static void Update(SchematicEditSession session, bool mouseOverUi)
        {
            bool down = Main.mouseLeft;
            bool pressed = down && !_wasMouseDown;
            _wasMouseDown = down;

            if (session.Phase != SchematicToolPhase.Editing)
            {
                Reset();
                return;
            }

            Player player = Main.LocalPlayer;
            Vector2 mouse = Main.MouseWorld;

            if (Dragging == RegionHandle.None)
            {
                Hovered = RegionHandle.None;
                if (mouseOverUi)
                    return;

                Hovered = HitTest(session.Region, mouse, HitRadiusPx / SchematicCamera.Zoom);
                if (Hovered == RegionHandle.None)
                    return;

                player.mouseInterface = true;
                if (pressed)
                    Dragging = Hovered;
                return;
            }

            player.mouseInterface = true;

            if (!down)
            {
                Dragging = RegionHandle.None;
                return;
            }

            ApplyDrag(session, Dragging, mouse);
            AutoScroll();
        }

        private static RegionHandle HitTest(Rectangle region, Vector2 mouse, float radius)
        {
            RegionHandle best = RegionHandle.None;
            float bestDistance = radius;

            foreach (RegionHandle corner in Corners)
            {
                float distance = Vector2.Distance(mouse, WorldPosition(region, corner));
                if (distance <= bestDistance)
                {
                    bestDistance = distance;
                    best = corner;
                }
            }
            if (best != RegionHandle.None)
                return best;

            float left = region.X * 16f;
            float right = region.Right * 16f;
            float top = region.Y * 16f;
            float bottom = region.Bottom * 16f;

            Consider(RegionHandle.Top, new Vector2(left, top), new Vector2(right, top));
            Consider(RegionHandle.Bottom, new Vector2(left, bottom), new Vector2(right, bottom));
            Consider(RegionHandle.Left, new Vector2(left, top), new Vector2(left, bottom));
            Consider(RegionHandle.Right, new Vector2(right, top), new Vector2(right, bottom));
            return best;

            void Consider(RegionHandle edge, Vector2 a, Vector2 b)
            {
                float distance = DistanceToSegment(mouse, a, b);
                if (distance <= bestDistance)
                {
                    bestDistance = distance;
                    best = edge;
                }
            }
        }

        private static float DistanceToSegment(Vector2 p, Vector2 a, Vector2 b)
        {
            Vector2 ab = b - a;
            float lengthSquared = ab.LengthSquared();
            float t = lengthSquared <= 0f ? 0f : MathHelper.Clamp(Vector2.Dot(p - a, ab) / lengthSquared, 0f, 1f);
            return Vector2.Distance(p, a + ab * t);
        }

        private static void ApplyDrag(SchematicEditSession session, RegionHandle handle, Vector2 mouse)
        {
            Rectangle region = session.Region;
            int left = region.X;
            int right = region.Right;
            int top = region.Y;
            int bottom = region.Bottom;

            int gridX = (int)MathF.Round(mouse.X / 16f);
            int gridY = (int)MathF.Round(mouse.Y / 16f);

            bool movesLeft = handle is RegionHandle.TopLeft or RegionHandle.Left or RegionHandle.BottomLeft;
            bool movesRight = handle is RegionHandle.TopRight or RegionHandle.Right or RegionHandle.BottomRight;
            bool movesTop = handle is RegionHandle.TopLeft or RegionHandle.Top or RegionHandle.TopRight;
            bool movesBottom = handle is RegionHandle.BottomLeft or RegionHandle.Bottom or RegionHandle.BottomRight;

            if (movesLeft) left = Math.Clamp(gridX, 0, right - 1);
            if (movesRight) right = Math.Clamp(gridX, left + 1, Main.maxTilesX);
            if (movesTop) top = Math.Clamp(gridY, 0, bottom - 1);
            if (movesBottom) bottom = Math.Clamp(gridY, top + 1, Main.maxTilesY);

            session.SetRegion(new Rectangle(left, top, right - left, bottom - top));
        }

        private static void AutoScroll()
        {
            Vector2 direction = Vector2.Zero;

            if (Main.mouseX < EdgeScrollMarginPx) direction.X = -1f;
            else if (Main.mouseX > Main.screenWidth - EdgeScrollMarginPx) direction.X = 1f;

            if (Main.mouseY < EdgeScrollMarginPx) direction.Y = -1f;
            else if (Main.mouseY > Main.screenHeight - EdgeScrollMarginPx) direction.Y = 1f;

            if (direction != Vector2.Zero)
                SchematicCamera.Pan(direction * EdgeScrollSpeed / SchematicCamera.Zoom);
        }

        public static void Draw(SpriteBatch sb, Texture2D pixel, Rectangle region)
        {
            float zoom = SchematicCamera.Zoom;
            RegionHandle active = Dragging != RegionHandle.None ? Dragging : Hovered;
            Color activeColor = Dragging != RegionHandle.None ? Color.Yellow : Color.White;

            DrawEdgeHighlight(sb, pixel, region, active, activeColor, 4f / zoom);

            float size = HandleSizePx / zoom;
            float border = 1f / zoom;

            foreach (RegionHandle handle in All)
            {
                Vector2 center = WorldPosition(region, handle) - Main.screenPosition;
                Color color = handle == active ? activeColor : Color.LightGray;

                sb.Draw(pixel, Box(center, size + border * 2f), Color.Black);
                sb.Draw(pixel, Box(center, size), color);
            }

            Vector2 labelPos = WorldPosition(region, RegionHandle.Top) - Main.screenPosition + new Vector2(-24f, -30f / zoom);
            Utils.DrawBorderString(sb, $"{region.Width} x {region.Height}", labelPos, Color.White, 0.9f);
        }

        private static void DrawEdgeHighlight(SpriteBatch sb, Texture2D pixel, Rectangle region, RegionHandle handle, Color color, float thickness)
        {
            float left = region.X * 16f - Main.screenPosition.X;
            float right = region.Right * 16f - Main.screenPosition.X;
            float top = region.Y * 16f - Main.screenPosition.Y;
            float bottom = region.Bottom * 16f - Main.screenPosition.Y;

            bool onTop = handle is RegionHandle.Top or RegionHandle.TopLeft or RegionHandle.TopRight;
            bool onBottom = handle is RegionHandle.Bottom or RegionHandle.BottomLeft or RegionHandle.BottomRight;
            bool onLeft = handle is RegionHandle.Left or RegionHandle.TopLeft or RegionHandle.BottomLeft;
            bool onRight = handle is RegionHandle.Right or RegionHandle.TopRight or RegionHandle.BottomRight;

            if (onTop) sb.Draw(pixel, new Rectangle((int)left, (int)(top - thickness * 0.5f), (int)(right - left), (int)thickness), color);
            if (onBottom) sb.Draw(pixel, new Rectangle((int)left, (int)(bottom - thickness * 0.5f), (int)(right - left), (int)thickness), color);
            if (onLeft) sb.Draw(pixel, new Rectangle((int)(left - thickness * 0.5f), (int)top, (int)thickness, (int)(bottom - top)), color);
            if (onRight) sb.Draw(pixel, new Rectangle((int)(right - thickness * 0.5f), (int)top, (int)thickness, (int)(bottom - top)), color);
        }

        private static Rectangle Box(Vector2 center, float size) =>
            new((int)(center.X - size * 0.5f), (int)(center.Y - size * 0.5f), Math.Max(1, (int)size), Math.Max(1, (int)size));
    }

    public static class SchematicCamera
    {
        private const float KeyboardPanSpeed = 16f;
        private const float KeyboardPanSpeedFast = 40f;

        public static bool Free { get; private set; }

        public static Vector2 Center { get; private set; }

        public static float Zoom => Math.Max(0.1f, Main.GameViewMatrix.Zoom.X);

        public static void EnsureFree()
        {
            if (Free)
                return;

            Center = Main.screenPosition + new Vector2(Main.screenWidth, Main.screenHeight) * 0.5f;
            Free = true;
        }

        public static void JumpTo(Vector2 worldCenter)
        {
            Center = ClampToWorld(worldCenter);
            Free = true;
        }

        public static void Pan(Vector2 worldDelta)
        {
            EnsureFree();
            Center = ClampToWorld(Center + worldDelta);
        }

        public static void Release() => Free = false;

        public static void UpdateKeyboardPan()
        {
            if (PlayerInput.WritingText || !Main.hasFocus)
                return;

            KeyboardState keys = Main.keyState;
            Vector2 direction = Vector2.Zero;
            if (keys.IsKeyDown(Keys.Left)) direction.X -= 1f;
            if (keys.IsKeyDown(Keys.Right)) direction.X += 1f;
            if (keys.IsKeyDown(Keys.Up)) direction.Y -= 1f;
            if (keys.IsKeyDown(Keys.Down)) direction.Y += 1f;

            if (direction == Vector2.Zero)
                return;

            float speed = keys.PressingShift() ? KeyboardPanSpeedFast : KeyboardPanSpeed;
            Pan(direction * speed / Zoom);
        }

        public static void Apply()
        {
            if (Free)
                Main.screenPosition = Center - new Vector2(Main.screenWidth, Main.screenHeight) * 0.5f;
        }

        private static Vector2 ClampToWorld(Vector2 p) =>
            Vector2.Clamp(p, Vector2.Zero, new Vector2(Main.maxTilesX * 16f, Main.maxTilesY * 16f));
    }

    public sealed class SchematicTextBox(string text = "") : UIElement
    {
        private string _text = text ?? string.Empty;

        public int MaxLength { get; set; } = 40;
        public string Placeholder { get; set; } = string.Empty;
        public bool Focused { get; private set; }

        public event Action<string> Committed;

        public string Text
        {
            get => _text;
            set => _text = value ?? string.Empty;
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            base.LeftClick(evt);

            if (Focused)
                return;

            Focused = true;
            Main.clrInput();
        }

        public void Unfocus()
        {
            if (!Focused)
                return;

            Focused = false;
            Committed?.Invoke(_text);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Focused && Main.mouseLeft && !ContainsPoint(Main.MouseScreen))
                Unfocus();
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (Focused)
            {
                PlayerInput.WritingText = true;
                Main.instance.HandleIME();

                string typed = Main.GetInputText(_text);
                if (typed.Length > MaxLength)
                    typed = typed.Substring(0, MaxLength);
                _text = typed;

                if (Main.inputTextEnter || Main.inputTextEscape)
                {
                    Main.inputTextEnter = false;
                    Main.inputTextEscape = false;
                    Unfocus();
                }
            }

            Rectangle box = GetDimensions().ToRectangle();
            var pixel = TextureAssets.MagicPixel.Value;

            spriteBatch.Draw(pixel, box, new Color(10, 12, 24) * 0.9f);
            Color border = Focused ? Color.Yellow : new Color(90, 100, 150);
            spriteBatch.Draw(pixel, new Rectangle(box.X, box.Y, box.Width, 1), border);
            spriteBatch.Draw(pixel, new Rectangle(box.X, box.Bottom - 1, box.Width, 1), border);
            spriteBatch.Draw(pixel, new Rectangle(box.X, box.Y, 1, box.Height), border);
            spriteBatch.Draw(pixel, new Rectangle(box.Right - 1, box.Y, 1, box.Height), border);

            string display;
            Color textColor;
            if (_text.Length == 0 && !Focused)
            {
                display = Placeholder;
                textColor = Color.Gray;
            }
            else
            {
                bool caretOn = Focused && (int)(Main.timeForVisualEffects / 30d) % 2 == 0;
                display = _text + (caretOn ? "|" : string.Empty);
                textColor = Color.White;
            }

            Utils.DrawBorderString(spriteBatch, display, new Vector2(box.X + 6, box.Y + box.Height * 0.5f - 10f), textColor, 0.8f);
        }
    }

    public static class SchematicCanvasTool
    {
        public static Point HoverCell { get; private set; }
        public static bool HoverVisible { get; private set; }

        public static Rectangle? MarkerDragPreview { get; private set; }

        private static PaintStrokeAction _stroke;
        private static bool _strokeValue;
        private static Point _lastCell;

        private static bool _markerDragging;
        private static Point _markerStart;

        private static bool _wasLeftDown;
        private static bool _wasRightDown;

        public static void Reset()
        {
            _stroke = null;
            _markerDragging = false;
            MarkerDragPreview = null;
            HoverVisible = false;
        }

        public static void Update(SchematicEditSession session, SchematicToolSettings settings, SchematicEditHistory history, bool mouseOverUi)
        {
            bool left = Main.mouseLeft;
            bool right = Main.mouseRight;
            bool leftPressed = left && !_wasLeftDown;
            bool rightPressed = right && !_wasRightDown;
            _wasLeftDown = left;
            _wasRightDown = right;

            HoverVisible = false;

            if (settings.Tool == EditTool.Region)
            {
                FinishStroke(history);
                _markerDragging = false;
                MarkerDragPreview = null;
                return;
            }

            bool busy = _stroke != null || _markerDragging;
            if (mouseOverUi && !busy)
                return;

            Main.LocalPlayer.mouseInterface = true;

            Point cell = Main.MouseWorld.ToTileCoordinates();
            HoverCell = cell;
            HoverVisible = true;

            if (settings.Tool == EditTool.Paint)
                UpdatePaint(session, settings, history, cell, left, right, leftPressed, rightPressed);
            else
                UpdateMarkers(session, settings, history, cell, left, rightPressed, leftPressed);
        }

        #region Paint
        private static void UpdatePaint(SchematicEditSession session, SchematicToolSettings settings, SchematicEditHistory history,
            Point cell, bool left, bool right, bool leftPressed, bool rightPressed)
        {
            PaintLayer layer = settings.Layer;
            Rectangle region = session.Region;

            if (settings.Mode == PaintMode.Fill)
            {
                if (region.Contains(cell) && (leftPressed || rightPressed))
                {
                    var action = new PaintStrokeAction();
                    SchematicPaintOps.FloodFill(session, layer, cell, leftPressed, action);
                    if (action.Count > 0)
                        history.Push(action);
                }
                return;
            }

            if (_stroke == null)
            {
                if (region.Contains(cell) && (leftPressed || rightPressed))
                {
                    _stroke = new PaintStrokeAction();
                    _strokeValue = leftPressed;
                    _lastCell = cell;
                    SchematicPaintOps.SetCell(session, layer, cell.X, cell.Y, _strokeValue, _stroke);
                }
                return;
            }

            bool held = _strokeValue ? left : right;
            if (!held)
            {
                FinishStroke(history);
                return;
            }

            foreach (Point p in SchematicPaintOps.Line(_lastCell, cell))
                if (region.Contains(p))
                    SchematicPaintOps.SetCell(session, layer, p.X, p.Y, _strokeValue, _stroke);
            _lastCell = cell;
        }

        private static void FinishStroke(SchematicEditHistory history)
        {
            if (_stroke == null)
                return;

            if (_stroke.Count > 0)
                history.Push(_stroke);
            _stroke = null;
        }
        #endregion

        #region Markers
        private static void UpdateMarkers(SchematicEditSession session, SchematicToolSettings settings, SchematicEditHistory history,
            Point cell, bool left, bool rightPressed, bool leftPressed)
        {
            Rectangle region = session.Region;

            if (!_markerDragging)
            {
                if (leftPressed && region.Contains(cell))
                {
                    _markerDragging = true;
                    _markerStart = cell;
                }
                else if (rightPressed)
                {
                    RemoveMarkerAt(session, history, cell);
                }
                return;
            }

            Point end = new(Math.Clamp(cell.X, region.X, region.Right - 1), Math.Clamp(cell.Y, region.Y, region.Bottom - 1));
            Rectangle area = SchematicEditSession.RectFromCorners(_markerStart, end);
            MarkerDragPreview = area;

            if (rightPressed)
            {
                _markerDragging = false;
                MarkerDragPreview = null;
                return;
            }

            if (!left)
            {
                string name = string.IsNullOrWhiteSpace(settings.MarkerName) ? "Marker" : settings.MarkerName.Trim();
                var marker = new SessionMarker(name, area);
                session.Markers.Add(marker);
                history.Push(new MarkerAddedAction(marker));

                _markerDragging = false;
                MarkerDragPreview = null;
            }
        }

        private static void RemoveMarkerAt(SchematicEditSession session, SchematicEditHistory history, Point cell)
        {
            for (int i = session.Markers.Count - 1; i >= 0; i--)
            {
                SessionMarker marker = session.Markers[i];
                if (!marker.Area.Contains(cell))
                    continue;

                session.Markers.RemoveAt(i);
                history.Push(new MarkerRemovedAction(i, marker));
                return;
            }
        }
        #endregion
    }

    public static class SchematicPaintOps
    {
        public static CellMask MaskFor(SchematicEditSession session, PaintLayer layer) =>
            layer == PaintLayer.KeepTile ? session.KeepTiles : session.KeepWalls;

        public static bool SetCell(SchematicEditSession session, PaintLayer layer, int x, int y, bool value, PaintStrokeAction record)
        {
            CellMask mask = MaskFor(session, layer);
            if (mask[x, y] == value)
                return false;

            mask[x, y] = value;
            record?.Record(x, y, layer);
            return true;
        }

        public static int FloodFill(SchematicEditSession session, PaintLayer layer, Point start, bool value, PaintStrokeAction record)
        {
            Rectangle r = session.Region;
            if (!r.Contains(start))
                return 0;

            CellMask mask = MaskFor(session, layer);
            int target = Signature(layer, start.X, start.Y);
            bool targetPainted = mask[start.X, start.Y];
            if (targetPainted == value)
                return 0;
            var visited = new bool[r.Width * r.Height];
            var stack = new Stack<int>();
            int changed = 0;

            int startIndex = Index(r, start.X, start.Y);
            visited[startIndex] = true;
            stack.Push(startIndex);

            while (stack.Count > 0)
            {
                int index = stack.Pop();
                int x = r.X + index / r.Height;
                int y = r.Y + index % r.Height;

                if (SetCell(session, layer, x, y, value, record))
                    changed++;

                TryPush(x + 1, y);
                TryPush(x - 1, y);
                TryPush(x, y + 1);
                TryPush(x, y - 1);
            }

            return changed;

            void TryPush(int nx, int ny)
            {
                if (!r.Contains(nx, ny))
                    return;

                int i = Index(r, nx, ny);
                if (visited[i] || Signature(layer, nx, ny) != target || mask[nx, ny] != targetPainted)
                    return;

                visited[i] = true;
                stack.Push(i);
            }
        }

        public static int AutoExterior(SchematicEditSession session, PaintStrokeAction record)
        {
            Rectangle r = session.Region;
            var visited = new bool[r.Width * r.Height];
            var stack = new Stack<int>();
            int changed = 0;

            for (int x = r.X; x < r.Right; x++)
            {
                TrySeed(x, r.Y);
                TrySeed(x, r.Bottom - 1);
            }
            for (int y = r.Y; y < r.Bottom; y++)
            {
                TrySeed(r.X, y);
                TrySeed(r.Right - 1, y);
            }

            while (stack.Count > 0)
            {
                int index = stack.Pop();
                int x = r.X + index / r.Height;
                int y = r.Y + index % r.Height;

                bool tileChanged = SetCell(session, PaintLayer.KeepTile, x, y, true, record);
                bool wallChanged = SetCell(session, PaintLayer.KeepWall, x, y, true, record);
                if (tileChanged || wallChanged)
                    changed++;

                TrySeed(x + 1, y);
                TrySeed(x - 1, y);
                TrySeed(x, y + 1);
                TrySeed(x, y - 1);
            }

            return changed;

            void TrySeed(int nx, int ny)
            {
                if (!r.Contains(nx, ny))
                    return;

                int i = Index(r, nx, ny);
                if (visited[i])
                    return;

                Tile tile = Main.tile[nx, ny];
                if (tile.HasTile || tile.WallType != WallID.None)
                    return;

                visited[i] = true;
                stack.Push(i);
            }
        }

        public static int ClearLayer(SchematicEditSession session, PaintLayer layer, PaintStrokeAction record)
        {
            Rectangle r = session.Region;
            CellMask mask = MaskFor(session, layer);
            int cleared = 0;

            for (int x = r.X; x < r.Right; x++)
                for (int y = r.Y; y < r.Bottom; y++)
                    if (mask[x, y] && SetCell(session, layer, x, y, false, record))
                        cleared++;

            return cleared;
        }

        public static IEnumerable<Point> Line(Point a, Point b)
        {
            int dx = Math.Abs(b.X - a.X);
            int dy = Math.Abs(b.Y - a.Y);
            int sx = a.X < b.X ? 1 : -1;
            int sy = a.Y < b.Y ? 1 : -1;
            int error = dx - dy;
            int x = a.X;
            int y = a.Y;

            while (true)
            {
                yield return new Point(x, y);
                if (x == b.X && y == b.Y)
                    yield break;

                int doubled = 2 * error;
                if (doubled > -dy) { error -= dy; x += sx; }
                if (doubled < dx) { error += dx; y += sy; }
            }
        }

        private static int Index(Rectangle r, int x, int y) => (x - r.X) * r.Height + (y - r.Y);

        private static int Signature(PaintLayer layer, int x, int y)
        {
            Tile tile = Main.tile[x, y];
            if (layer == PaintLayer.KeepWall)
                return tile.WallType;

            if (tile.HasTile)
                return tile.TileType;
            return tile.WallType != WallID.None ? -2 : -1;
        }
    }

    public abstract class SchematicEditAction
    {
        public abstract void Undo(SchematicEditSession session);
        public abstract void Redo(SchematicEditSession session);
    }

    public sealed class PaintStrokeAction : SchematicEditAction
    {
        private readonly List<(int X, int Y, PaintLayer Layer)> _changes = [];

        public int Count => _changes.Count;

        public void Record(int x, int y, PaintLayer layer) => _changes.Add((x, y, layer));

        public override void Undo(SchematicEditSession session) => FlipAll(session);
        public override void Redo(SchematicEditSession session) => FlipAll(session);

        private void FlipAll(SchematicEditSession session)
        {
            foreach ((int x, int y, PaintLayer layer) in _changes)
            {
                CellMask mask = SchematicPaintOps.MaskFor(session, layer);
                mask[x, y] = !mask[x, y];
            }
        }
    }

    public sealed class MarkerAddedAction(SessionMarker marker) : SchematicEditAction
    {
        private readonly SessionMarker _marker = marker;

        public override void Undo(SchematicEditSession session) => session.Markers.Remove(_marker);
        public override void Redo(SchematicEditSession session) => session.Markers.Add(_marker);
    }

    public sealed class MarkerRemovedAction(int index, SessionMarker marker) : SchematicEditAction
    {
        private readonly int _index = index;
        private readonly SessionMarker _marker = marker;

        public override void Undo(SchematicEditSession session) => session.Markers.Insert(Math.Min(_index, session.Markers.Count), _marker);
        public override void Redo(SchematicEditSession session) => session.Markers.Remove(_marker);
    }

    public sealed class SchematicEditHistory
    {
        private const int MaxDepth = 200;

        private readonly List<SchematicEditAction> _undo = [];
        private readonly List<SchematicEditAction> _redo = [];

        public bool CanUndo => _undo.Count > 0;
        public bool CanRedo => _redo.Count > 0;

        public void Push(SchematicEditAction action)
        {
            _undo.Add(action);
            _redo.Clear();

            if (_undo.Count > MaxDepth)
                _undo.RemoveAt(0);
        }

        public bool Undo(SchematicEditSession session)
        {
            if (_undo.Count == 0)
                return false;

            SchematicEditAction action = _undo[^1];
            _undo.RemoveAt(_undo.Count - 1);
            action.Undo(session);
            _redo.Add(action);
            return true;
        }

        public bool Redo(SchematicEditSession session)
        {
            if (_redo.Count == 0)
                return false;

            SchematicEditAction action = _redo[^1];
            _redo.RemoveAt(_redo.Count - 1);
            action.Redo(session);
            _undo.Add(action);
            return true;
        }

        public void Clear()
        {
            _undo.Clear();
            _redo.Clear();
        }
    }

    public enum PickTarget
    {
        None,
        TileStandIn,
        WallStandIn
    }

    public static class SchematicPicker
    {
        private static bool _wasMouseDown;
        private static bool _wasRightDown;

        public static Point? HoverCell { get; private set; }

        public static void Reset()
        {
            _wasMouseDown = false;
            _wasRightDown = false;
            HoverCell = null;
        }

        public static bool Update(SchematicEditSession session, SchematicToolSettings settings, bool mouseOverUi)
        {
            bool leftDown = Main.mouseLeft;
            bool leftPressed = leftDown && !_wasMouseDown;
            _wasMouseDown = leftDown;

            bool rightDown = Main.mouseRight;
            bool rightPressed = rightDown && !_wasRightDown;
            _wasRightDown = rightDown;

            if (settings.Picking == PickTarget.None)
            {
                HoverCell = null;
                return false;
            }

            if (rightPressed)
            {
                settings.Picking = PickTarget.None;
                HoverCell = null;
                Main.NewText("Pick cancelled.", Color.Orange);
                return true;
            }

            if (mouseOverUi)
            {
                HoverCell = null;
                return true;
            }

            Main.LocalPlayer.mouseInterface = true;

            Point cell = Main.MouseWorld.ToTileCoordinates();
            HoverCell = cell;

            if (!leftPressed)
                return true;

            Tile tile = Main.tile[cell.X, cell.Y];

            if (settings.Picking == PickTarget.TileStandIn)
            {
                if (!tile.HasTile)
                {
                    Main.NewText("That spot has no tile - pick a placed tile.", Color.Orange);
                    return true;
                }

                session.KeepTileStandIn = tile.TileType;
                Main.NewText($"Keep-Tile stand-in set to tile type {tile.TileType}.", Color.LightGreen);
            }
            else
            {
                if (tile.WallType == WallID.None)
                {
                    Main.NewText("That spot has no wall - pick a tile with a wall behind it.", Color.Orange);
                    return true;
                }

                session.KeepWallStandIn = tile.WallType;
                Main.NewText($"Keep-Wall stand-in set to wall type {tile.WallType}.", Color.LightGreen);
            }

            settings.Picking = PickTarget.None;
            return true;
        }
    }

    public enum EditTool
    {
        Region,
        Paint,
        Markers
    }

    public enum PaintLayer
    {
        KeepTile,
        KeepWall
    }

    public enum PaintMode
    {
        Brush,
        Fill
    }

    public sealed class SchematicToolSettings
    {
        public EditTool Tool { get; set; } = EditTool.Region;
        public PaintLayer Layer { get; set; } = PaintLayer.KeepTile;
        public PaintMode Mode { get; set; } = PaintMode.Brush;
        public PickTarget Picking { get; set; } = PickTarget.None;
        public string MarkerName { get; set; } = "Marker";

        public void Reset()
        {
            Tool = EditTool.Region;
            Layer = PaintLayer.KeepTile;
            Mode = PaintMode.Brush;
            Picking = PickTarget.None;
            MarkerName = "Marker";
        }
    }
}
#endif