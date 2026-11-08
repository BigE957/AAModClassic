using AAModClassic._Content.Void.___PreHardmode.Items.Tiles.Decoration;
using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using SpiritReforged.Content.Forest.Walls;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._CrossMod.SpiritReforged.Trellises
{
    [ExtendsFromMod("SpiritReforged")]
    public class OuroborosWoodTrellis : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.CrossMod";
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.DefaultToPlaceableWall(ModContent.WallType<OuroborosWoodTrellis_Wall>());
        }

        public override void AddRecipes()
        {
            CreateRecipe(4).AddIngredient(ModContent.ItemType<OuroborosWood>()).AddTile(TileID.Sawmill).Register();
            CreateRecipe(4).AddIngredient(ModContent.ItemType<OuroborosWoodCrossedTrellis>(), 4).AddTile(TileID.Sawmill).Register();
        }
    }

    [ExtendsFromMod("SpiritReforged")]
    public class OuroborosWoodTrellis_Wall : Trellis
    {
        public override void AddEntry()
        {
            DustType = ModContent.DustType<DoomDust>();
            AddMapEntry(new Color(25, 12, 10));
        }
    }

    [ExtendsFromMod("SpiritReforged")]
    public class OuroborosWoodCrossedTrellis : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.CrossMod";
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.DefaultToPlaceableWall(ModContent.WallType<OuroborosWoodCrossedTrellis_Wall>());
        }

        public override void AddRecipes()
        {
            CreateRecipe(4).AddIngredient(ModContent.ItemType<OuroborosWoodTrellis>(), 4).AddTile(TileID.Sawmill).Register();
        }
    }

    [ExtendsFromMod("SpiritReforged")]
    public class OuroborosWoodCrossedTrellis_Wall : OuroborosWoodTrellis_Wall { }
}
