using AAModClassic._Content.Chaos.World.Tiles;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.RedMushroom.World.Biomes;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.Tiles.Functional;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;
using Terraria.ObjectData;

namespace AAModClassic._Content.RedMushroom.___PreHardmode.Items.Tiles.Functional
{
    public class RedMushroomPylon_Tile : PylonAbstract_Tile
    {
        public override int PylonItemID => ModContent.ItemType<RedMushroomPylon>();
        public override Condition ShopCondition => AAConditions.InAnyRedMushroom;
        public override bool TeleportBiomeRequirements => AAWorld.mushTiles > 100;
        public override (float, float, float) LightColor => (0.8f, 0.7f, 0.3f);
        public override Color DustColor => new Color(0.6f, 0.5f, 0.1f, 1f);
    }
}
