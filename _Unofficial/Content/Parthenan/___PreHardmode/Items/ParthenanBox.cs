using AAModClassic._Content.Mire.___PreHardmode.Items._BossHydra.BossStandard;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content.Parthenan.___PreHardmode.Items
{
    public class ParthenanBox : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Music Box (Parthenan)");
            // Tooltip.SetDefault(@"Plays 'Maelstrom' by Tyeski");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<ParthenanBox_Tile>();
            Item.width = 24;
            Item.height = 24;
            Item.rare = ItemRarityID.LightRed;
            Item.value = 10000;
            Item.accessory = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(ModContent.ItemType<FulguriteBar>(), 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.Register();
        }
    }
}
