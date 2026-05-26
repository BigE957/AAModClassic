using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Unreleased.Content.SunkenShip.World.Tiles
{
    public class CthulhuPortal_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            Main.tileSolid[Type] = false;
            HitSound = SoundID.Dig;
            DustType = DustID.Dirt;
            AddMapEntry(new Color(0, 80, 100));
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            Color color = BaseUtility.ColorMult(AAColor.Cthulhu, 0.7f);
            r = color.R / 255f; g = color.G / 255f; b = color.B / 255f;
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }

        public override bool PreDraw(int x, int y, SpriteBatch sb)
        {
            Main.instance.TilesRenderer.AddSpecialPoint(x, y, Terraria.GameContent.Drawing.TileDrawing.TileCounterType.CustomNonSolid);
            return false;
        }

        public override void SpecialDraw(int x, int y, SpriteBatch spriteBatch)
        {
            Texture2D PortalTex = ModContent.Request<Texture2D>(Texture + "_Portal").Value;
            Texture2D PortalTex2 = ModContent.Request<Texture2D>(Texture + "_Portal2").Value;

            spriteBatch.Draw(PortalTex, new Point(x, y).ToWorldCoordinates() - Main.screenPosition, null, AAColor.Cthulhu, -Main.GlobalTimeWrappedHourly, PortalTex.Size() * 0.5f, 1f, 0, 0);
            spriteBatch.Draw(PortalTex2, new Point(x, y).ToWorldCoordinates() - Main.screenPosition, null, AAColor.Cthulhu, Main.GlobalTimeWrappedHourly, PortalTex2.Size() * 0.5f, 1f, 0, 0);
        }
    }
}