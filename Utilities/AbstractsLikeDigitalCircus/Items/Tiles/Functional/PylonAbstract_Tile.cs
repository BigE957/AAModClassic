using AAModClassic._Content.Chaos.World.Tiles;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Functional;
using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.Tiles.Functional;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;
using Terraria.ObjectData;

namespace AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.Tiles.Functional
{
    public abstract class PylonAbstract_Tile : ModPylon
    {
        public new string LocalizationCategory => "Tiles";

        // firtst three are inferno pylon stuff, last two are ocean pylon light and dust
        public virtual int PylonItemID => ModContent.ItemType<InfernoPylon>();
        public virtual Condition ShopCondition => InfernoConditions.InAnyInferno;
        public virtual bool TeleportBiomeRequirements => AAWorld.infernoTiles > 100;
        public virtual (float, float, float) LightColor => (0.4f, 0.4f, 1.15f);
        public virtual Color DustColor => new Color(0.2f, 0.2f, 0.95f, 1f);

        public const int CrystalVerticalFrameCount = 8;

        public Asset<Texture2D> crystalTexture;
        public static Asset<Texture2D> crystalHighlightTexture;
        public Asset<Texture2D> mapIcon;

        public override void Load()
        {
            crystalTexture = ModContent.Request<Texture2D>(Texture + "_Crystal");
            crystalHighlightTexture = ModContent.Request<Texture2D>(FilePathUtils.TexturePath<PylonAbstract_Tile>() + "_CrystalHighlight");
            mapIcon = ModContent.Request<Texture2D>(Texture + "_MapIcon");
        }

        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;

            VanillaFallbackOnModDeletion = TileID.TeleportationPylon;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.newTile.StyleHorizontal = true;
            TEModdedPylon moddedPylon = ModContent.GetInstance<PylonAbstract_TileEntity>();
            TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(moddedPylon.PlacementPreviewHook_CheckIfCanPlace, 1, 0, true);
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(moddedPylon.Hook_AfterPlacement, -1, 0, false);

            TileObjectData.addTile(Type);

            TileID.Sets.InteractibleByNPCs[Type] = true;
            TileID.Sets.PreventsSandfall[Type] = true;
            TileID.Sets.AvoidedByMeteorLanding[Type] = true;

            AddToArray(ref TileID.Sets.CountsAsPylon);

            LocalizedText pylonName = CreateMapEntryName();
            AddMapEntry(Color.White, pylonName);
        }

        public override NPCShop.Entry GetNPCShopEntry()
        {
            NPCShop.Entry shopEntry = base.GetNPCShopEntry();
            shopEntry.AddCondition(ShopCondition);

            return shopEntry;
        }

        public override void MouseOver(int i, int j)
        {
            Main.LocalPlayer.cursorItemIconEnabled = true;
            Main.LocalPlayer.cursorItemIconID = PylonItemID;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            ModContent.GetInstance<PylonAbstract_TileEntity>().Kill(i, j);
        }

        public override bool ValidTeleportCheck_BiomeRequirements(TeleportPylonInfo pylonInfo, SceneMetrics sceneData)
        {
            return TeleportBiomeRequirements;
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = LightColor.Item1 * 0.75f;
            g = LightColor.Item2 * 0.75f;
            b = LightColor.Item3 * 0.75f;
        }

        public override void SpecialDraw(int i, int j, SpriteBatch spriteBatch)
        {
            DefaultDrawPylonCrystal(spriteBatch, i, j, crystalTexture, crystalHighlightTexture, new Vector2(-1f, -12f), new Color(255, 255, 255, 0) * 0.1f, DustColor, 10, CrystalVerticalFrameCount);
        }

        public override void DrawMapIcon(ref MapOverlayDrawContext context, ref string mouseOverText, TeleportPylonInfo pylonInfo, bool isNearPylon, Color drawColor, float deselectedScale, float selectedScale)
        {
            bool mouseOver = DefaultDrawMapIcon(ref context, mapIcon, pylonInfo.PositionInTiles.ToVector2() + new Vector2(1.5f, 2f), drawColor, deselectedScale, selectedScale);
            DefaultMapClickHandle(mouseOver, pylonInfo, ContentSamples.ItemsByType[PylonItemID].ModItem.DisplayName.Key, ref mouseOverText);
        }
    }
}
