using AAModClassic._Content.Chaos.World.Tiles;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
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

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Tiles.Functional
{
    public class InfernoPylon_Tile : PylonAbstract_Tile
    {
        public override int PylonItemID => ModContent.ItemType<InfernoPylon>();
        public override Condition ShopCondition => InfernoConditions.InAnyInferno;
        public override bool TeleportBiomeRequirements => AAWorld.infernoTiles > 100;
        public override (float, float, float) LightColor => (0.9f, 0.6f, 0.2f);
        public override Color DustColor => new Color(0.7f, 0.5f, 0f, 1f);
    }
}
