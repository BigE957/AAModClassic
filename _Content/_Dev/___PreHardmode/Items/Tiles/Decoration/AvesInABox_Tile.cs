using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAModClassic._Content._Dev.___PreHardmode.Items.Tiles.Decoration
{
    public class AvesInABox_Tile : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.Table | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
            TileObjectData.addTile(Type);
            AnimationFrameHeight = 54;

            LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Aves In A Box");
			AddMapEntry(new Color(100, 200, 100), name);
			DustType = DustID.t_LivingWood;
			TileID.Sets.DisableSmartCursor[Type] = true;
            RegisterItemDrop(ModContent.ItemType<AvesInABox>());
        }

        public bool Quack = false;
        public int QuackTimer = 90;

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = 1;
		}

        public override bool RightClick(int i, int j)
        {
            if (!Quack)
            {
                QuackTimer = 40;
                SoundEngine.PlaySound(new SoundStyle("AAModClassic/Sounds/QUAK"));
                Quack = true;
                return true;
            }
            return false;
		}
        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (QuackTimer <= 0)
            {
                frame = -1;
                Quack = false;
            }
            if (Quack)
            {
                frame = 0;
                QuackTimer--;
            }
        }
    }
}