using AAModClassic.Dusts;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items.Tiles.Decoration.DoomFurniture
{
    public class DoomClock_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            this.SetUpClock(ModContent.ItemType<DoomClock>());
            DustType = ModContent.DustType<DoomDust>();
        }

        public override void MouseOver(int i, int j) => FurnitureUtils.MouseOver(i, j, ModContent.ItemType<DoomClock>());

        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

        public override bool RightClick(int x, int y)
        {
            if (!AAWorld.downedZero)
            {
                Main.NewText(Language.GetTextValue("Mods.AAModClassic.Tiles.DoomClock_Tile.FlavorText.PreZero"), 200, 0, 0);
            }
            else
            {
                Main.NewText(Language.GetTextValue("Mods.AAModClassic.Tiles.DoomClock_Tile.FlavorText.PostZero"), 200, 0, 0);
            }
            return true;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            if (closer)
                Main.SceneMetrics.HasClock = true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch) => false;

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = 16;
            Texture2D tex = TextureAssets.Tile[Type].Value;
            Texture2D Glow = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            if (AAWorld.downedZero)
            {
                tex = ModContent.Request<Texture2D>(Texture + "0").Value;
                Glow = ModContent.Request<Texture2D>(Texture + "0_Glow").Value;
            }
            spriteBatch.Draw(tex, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            spriteBatch.Draw(Glow, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
    }
}