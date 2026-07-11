using AAModClassic._Content.Chaos.World.Tiles;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic._Content.Mire.World.Biomes;
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

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Tiles.Functional
{
    public class MirePylon_Tile : PylonAbstract_Tile
    {
        public override int PylonItemID => ModContent.ItemType<MirePylon>();
        public override Condition ShopCondition => MireConditions.InAnyMire;
        public override bool TeleportBiomeRequirements => AAWorld.mireTiles > 100;
        public override (float, float, float) LightColor => (0.9f, 0.3f, 0.9f);
        public override Color DustColor => new Color(0.7f, 0.1f, 0.7f, 1f);
    }
}
