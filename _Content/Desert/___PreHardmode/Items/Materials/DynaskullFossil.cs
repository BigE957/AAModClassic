using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAModClassic._Content.Desert.___PreHardmode.Items.Materials
{
    public class DynaskullFossil : BaseAAItem
    {
        public override void SetStaticDefaults()
        {

            // DisplayName.SetDefault("Dynaskull Fossil");
            // Tooltip.SetDefault("The energy of millions of years pulsates through this ancient fossil");
        }

        public override void SetDefaults()
        {

            Item.width = 16;
            Item.height = 16;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Blue;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<DynaskullFossil_Tile>(); //put your CustomBlock Tile name
        }
    }
}
