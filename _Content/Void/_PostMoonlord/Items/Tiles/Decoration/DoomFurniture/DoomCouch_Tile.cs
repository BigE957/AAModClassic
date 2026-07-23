using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items.Tiles.Decoration.DoomFurniture
{
    public class DoomCouch_Tile : ModTile
	{
        private static Asset<Texture2D> GlowTexture = null;

        public override void SetStaticDefaults()
        {
            this.SetUpSofa(ModContent.ItemType<DoomCouch>());
            DustType = ModContent.DustType<DoomDust>();
        }

        public override void MouseOver(int i, int j) => FurnitureCommon.BenchMouseOver(i, j, ModContent.ItemType<DoomCouch>());

        public override void NumDust(int i, int j, bool fail, ref int num) => num = (fail ? 1 : 3);

        public override void ModifySittingTargetInfo(int i, int j, ref TileRestingInfo info) => FurnitureCommon.BenchSitInfo(i, j, ref info);

        public override bool RightClick(int i, int j) => FurnitureCommon.ChairRightClick(i, j);

        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => settings.player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance);

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];

            if (tile.IsTileInvisible && !Main.ShouldShowInvisibleWalls())
                return;

            int xFrameOffset = tile.TileFrameX;
            int yFrameOffset = tile.TileFrameY;

            GlowTexture ??= ModContent.Request<Texture2D>(Texture + "_Glow");
            Texture2D glowmask = GlowTexture.Value;

            Vector2 drawOffest = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
            Vector2 drawPosition = new Vector2(i * 16 - Main.screenPosition.X, j * 16 - Main.screenPosition.Y) + drawOffest;
            Color drawColour = Color.White;
            if (!tile.IsHalfBlock && tile.Slope == 0)
                spriteBatch.Draw(glowmask, drawPosition, new Rectangle(xFrameOffset, yFrameOffset, 18, 18), drawColour, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
            else if (tile.IsHalfBlock)
                spriteBatch.Draw(glowmask, drawPosition + new Vector2(0f, 8f), new Rectangle(xFrameOffset, yFrameOffset, 18, 8), drawColour, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
        }
    }
}
