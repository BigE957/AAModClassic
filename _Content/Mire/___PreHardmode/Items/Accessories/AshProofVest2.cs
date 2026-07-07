using AAModClassic._Content.Inferno.Buffs;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Accessories
{
    public class AshProofVest2 : AshProofVest3, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ash-Proof Vest");
            // Tooltip.SetDefault(@"Lingering in the firestorm for too long will degrade this accessory and cause it to break...");
        }
    }
}