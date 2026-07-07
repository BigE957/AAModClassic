using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items._BossHydra.BossStandard
{
    [AutoloadEquip(EquipType.Head)]
	public class HydraMask3 : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Vanity.Masks";
        public static int type;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Hydra Mask");
		}

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 26;
            Item.rare = ItemRarityID.Green;
            Item.vanity = true;
        }
    }
}