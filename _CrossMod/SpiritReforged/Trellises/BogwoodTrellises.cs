using AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using SpiritReforged.Content.Forest.Walls;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._CrossMod.SpiritReforged.Trellises
{
    [ExtendsFromMod("SpiritReforged")]
    public class BogwoodTrellis : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.CrossMod";
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.DefaultToPlaceableWall(ModContent.WallType<BogwoodTrellis_Wall>());
        }

        public override void AddRecipes()
        {
            CreateRecipe(4).AddIngredient(ModContent.ItemType<Bogwood>()).AddTile(TileID.Sawmill).Register();
            CreateRecipe(4).AddIngredient(ModContent.ItemType<BogwoodCrossedTrellis>(), 4).AddTile(TileID.Sawmill).Register();
        }
    }

    [ExtendsFromMod("SpiritReforged")]
    public class BogwoodTrellis_Wall : Trellis
    {
        public override void AddEntry()
        {
            DustType = ModContent.DustType<BogwoodDust>();
            AddMapEntry(new Color(25, 12, 10));
        }
    }

    [ExtendsFromMod("SpiritReforged")]
    public class BogwoodCrossedTrellis : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.CrossMod";
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.DefaultToPlaceableWall(ModContent.WallType<BogwoodCrossedTrellis_Wall>());
        }

        public override void AddRecipes()
        {
            CreateRecipe(4).AddIngredient(ModContent.ItemType<BogwoodTrellis>(), 4).AddTile(TileID.Sawmill).Register();
        }
    }

    [ExtendsFromMod("SpiritReforged")]
    public class BogwoodCrossedTrellis_Wall : BogwoodTrellis_Wall { }
}
