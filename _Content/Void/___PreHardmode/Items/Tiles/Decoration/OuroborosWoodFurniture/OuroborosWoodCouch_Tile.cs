using AAModClassic.Dusts;
using AAModClassic.Globals;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Tiles.Decoration.OuroborosWoodFurniture
{
    public class OuroborosWoodCouch_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetUpSofa(ModContent.ItemType<OuroborosWoodCouch>());
            DustType = ModContent.DustType<DoomDust>();
        }

        public override void MouseOver(int i, int j) => FurnitureUtils.BenchMouseOver(i, j, ModContent.ItemType<OuroborosWoodCouch>());

        public override void NumDust(int i, int j, bool fail, ref int num) => num = (fail ? 1 : 3);

        public override void ModifySittingTargetInfo(int i, int j, ref TileRestingInfo info) => FurnitureUtils.BenchSitInfo(i, j, ref info);

        public override bool RightClick(int i, int j) => FurnitureUtils.ChairRightClick(i, j);

        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => settings.player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance);

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = tile.TileFrameY == 36 ? 18 : 16;
            Main.spriteBatch.Draw(ModContent.Request<Texture2D>(Texture + "_Glow").Value, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, height), AAColor.Glow, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}
