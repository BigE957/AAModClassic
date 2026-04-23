using AAModClassic.___Content.Inferno.___PreHardmode.Items.Accessories;
using AAModClassic.___Content.Inferno.___PreHardmode.Items.Pets;
using AAModClassic.___Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic.___Content.Void.___PreHardmode.Items.Consumables;
using AAModClassic.___Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.Items.Magic;
using AAModClassic.Items.Melee;
using AAModClassic.Items.Ranged;
using AAModClassic.Items.Summoning;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items.Consumables;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Void.__Hardmode.Items.Consumables
{
    public class NullCrate : VoidCrate
    {
        public override int Tile => ModContent.TileType<NullCrate_Tile>();
        public override bool Hardmode => true;
        public override int? ShimmerInto => ModContent.ItemType<VoidCrate>();
    }
}