using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossOrthrusX.BossStandard;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRaiderUltima.BossStandard;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRetriever.BossStandard;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._CrossMod.CalamityMod.LoreItems
{
    public class SiegeUnitsLore : LoreItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.rare = ItemRarityID.Pink;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RetrieverTrophy>());
            recipe.AddTile(TileID.Bookcases);
            recipe.Register();

            Recipe recipe2 = CreateRecipe();
            recipe2.AddIngredient(ModContent.ItemType<OrthrusXTrophy>());
            recipe2.AddTile(TileID.Bookcases);
            recipe2.Register();

            Recipe recipe3 = CreateRecipe();
            recipe3.AddIngredient(ModContent.ItemType<RaiderUltimaTrophy>());
            recipe3.AddTile(TileID.Bookcases);
            recipe3.Register();
        }
    }
}
