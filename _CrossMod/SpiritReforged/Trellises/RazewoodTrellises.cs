using AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using SpiritReforged.Content.Forest.Walls;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._CrossMod.SpiritReforged.Trellises
{
    [ExtendsFromMod("SpiritReforged")]
    public class RazewoodTrellis : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.CrossMod";
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.DefaultToPlaceableWall(ModContent.WallType<RazewoodTrellis_Wall>());
        }

        public override void AddRecipes()
        {
            CreateRecipe(4).AddIngredient(ModContent.ItemType<Razewood>()).AddTile(TileID.Sawmill).Register();
            CreateRecipe(4).AddIngredient(ModContent.ItemType<RazewoodCrossedTrellis>(), 4).AddTile(TileID.Sawmill).Register();
        }
    }

    [ExtendsFromMod("SpiritReforged")]
    public class RazewoodTrellis_Wall : Trellis
    {
        public override void AddEntry()
        {
            DustType = ModContent.DustType<RazewoodDust>();
            AddMapEntry(new Color(25, 12, 10));
        }
    }

    [ExtendsFromMod("SpiritReforged")]
    public class RazewoodCrossedTrellis : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.CrossMod";
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.DefaultToPlaceableWall(ModContent.WallType<RazewoodCrossedTrellis_Wall>());
        }

        public override void AddRecipes()
        {
            CreateRecipe(4).AddIngredient(ModContent.ItemType<RazewoodTrellis>(), 4).AddTile(TileID.Sawmill).Register();
        }
    }

    [ExtendsFromMod("SpiritReforged")]
    public class RazewoodCrossedTrellis_Wall : RazewoodTrellis_Wall { }
}
