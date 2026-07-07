using AAModClassic._Content.Inferno.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Pets;
using AAModClassic._Content.Inferno.__Hardmode.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Consumables;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.__Hardmode.Items.Consumables
{
    public class NullCrate : VoidCrate, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.GrabBags.Crates";
        public override int Tile => ModContent.TileType<NullCrate_Tile>();
        public override bool Hardmode => true;
        public override int? ShimmerInto => ModContent.ItemType<VoidCrate>();
    }
}