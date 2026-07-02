using AAModClassic._Content.Inferno.Buffs;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Accessories
{
    public class AshProofVest0 : AshProofVest3
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ash-Proof Vest");
            // Tooltip.SetDefault(@"Temporary accessory to completly remove Ash Rain");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 6));
        }
    }
}