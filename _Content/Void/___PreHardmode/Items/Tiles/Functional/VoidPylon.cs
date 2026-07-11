using AAModClassic._Content.Chaos.World.Tiles;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Functional;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;
using Terraria.ObjectData;

namespace AAModClassic._Content.Void.___PreHardmode.Items.Tiles.Functional
{
    public class VoidPylon : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Placeables.Functional";

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<VoidPylon_Tile>());

            Item.SetShopValues(ItemRarityColor.Blue1, Item.buyPrice(gold: 10));
        }
    }
}
