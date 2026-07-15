using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Mire.__Hardmode.Items.Tiles.Decoration
{
    class LivingBogtoxinBlock_Tile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true; 

            TileID.Sets.CanPlaceNextToNonSolidTile[Type] = true;

            DustType = ModContent.DustType<YamataDust>();

            AddMapEntry(new Color(LivingBogtoxinBlock.LightColor));

            AnimationFrameHeight = 90;
        }
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = LivingBogtoxinBlock.LightColor.X;
            g = LivingBogtoxinBlock.LightColor.Y;
            b = LivingBogtoxinBlock.LightColor.Z;
        }
        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
        {
            offsetY = 2;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frame = Main.tileFrame[TileID.LivingFire];
        }
    }
}
