using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using AAModClassic.Base.BaseMod.Base;

namespace AAModClassic._Content.Inferno.World.Tiles
{
    public class DragonAltarUnsafe_Tile : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileLavaDeath[Type] = false;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.Origin = new Point16(0, 0);
            TileObjectData.addTile(Type);
            Main.tileHammer[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;
			LocalizedText name = CreateMapEntryName();
            Main.tileLighted[Type] = true;
            // name.SetDefault("Dragon Altar");
            DustType = ModContent.DustType<Dusts.IncineriteDust>();
            AddMapEntry(new Color(160, 100, 0), name);
            AdjTiles = new int[] { TileID.DemonAltar };
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            //delte this and you die
            Player player = Main.LocalPlayer;
            if (!Main.hardMode)
            {
                if (blockDamaged == true)
                {
                    DamagePlayer(player);
                    blockDamaged = false;
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            Color color = BaseUtility.ColorMult(AAPlayer.IncineriteColor, 0.7f);
            r = color.R / 255f; g = color.G / 255f; b = color.B / 255f;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            WorldGen.SmashAltar(i, j);
        }

        public static void DamagePlayer (Player player)
        {
            player.statLife -= player.statLifeMax / 10;
        }
	}
}