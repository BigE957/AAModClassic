using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.Items._BossInfinityZero.BossStandard
{
    class InfinityZeroBox_Tile : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileObsidianKill[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.LavaDeath = false;
			TileObjectData.newTile.DrawYOffset = 2;
			TileObjectData.addTile(Type);
            TileID.Sets.DisableSmartCursor[Type] = true;
            LocalizedText name = CreateMapEntryName();
            // name.SetDefault("Music Box");
            RegisterItemDrop(ModContent.ItemType<InfinityZeroBox>());
            DustType = ModContent.DustType<Dusts.VoidDust_Unreleased>();
            AddMapEntry(new Color(200, 200, 200), name);
		}

        public Texture2D glowTex;
        public bool glow = true;

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            if (!glow) return;
            Color color = BaseUtility.ColorMult(ZAAPlayer.ZeroColor, 0.7f);
            r = color.R / 255f; g = color.G / 255f; b = color.B / 255f;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            if (glow && tile != null && tile.HasTile && tile.TileType == Type)
            {
                if (glowTex == null) glowTex = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
                BaseDrawing.DrawTileTexture(sb, glowTex, x, y, true, false, false, null, AAGlobalTile.GetZeroColorDim);
            }
        }

		public override void MouseOver(int i, int j)
		{
			Player player = Main.LocalPlayer;
			player.noThrow = 2;
			player.cursorItemIconEnabled = true;
			player.cursorItemIconID = ModContent.ItemType<InfinityZeroBox>();
		}
	}
}
