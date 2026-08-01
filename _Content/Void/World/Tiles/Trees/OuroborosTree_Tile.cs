using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.World.Tiles.Trees
{
    public class OuroborosTree_Tile : ModTree
    {
        public override TreePaintingSettings TreeShaderSettings => new()
        {
            UseSpecialGroups = true,
            SpecialGroupMinimalHueValue = 11f / 72f,
            SpecialGroupMaximumHueValue = 0.25f,
            SpecialGroupMinimumSaturationValue = 0.88f,
            SpecialGroupMaximumSaturationValue = 1f
        };

        public override void SetStaticDefaults()
        {
            GrowsOnTileId = [ModContent.TileType<ApocalyptiteOre_Tile>(), ModContent.TileType<DoomGrass_Tile>(), ModContent.TileType<Doomstone_Tile>(), ModContent.TileType<DoomstoneB_Tile>()];
        }

        public override int DropWood()
        {
            return AAMod.instance.Find<ModItem>("OuroborosWood").Type;
        }

        public override Asset<Texture2D> GetTexture()
        {
            return ModContent.Request<Texture2D>("AAModClassic/_Content/Void/World/Tiles/Trees/OuroborosTree_Tile");
        }

        public override Asset<Texture2D> GetBranchTextures()
        {
            return ModContent.Request<Texture2D>("AAModClassic/_Content/Void/World/Tiles/Trees/OuroborosTree_Tile_Branches");
        }

        public override Asset<Texture2D> GetTopTextures()
        {
            return ModContent.Request<Texture2D>("AAModClassic/_Content/Void/World/Tiles/Trees/OuroborosTree_Tile_Top");
        }

        public override int SaplingGrowthType(ref int style)
        {
            style = 0;
            return AAMod.instance.Find<ModTile>("OuroborosSapling_Tile").Type;
        }
    }
}
