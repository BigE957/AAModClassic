using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.BossStandard
{
    [AutoloadEquip(EquipType.Head)]
	public class AthenaMask : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Vanity.Masks";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Athena Mask");
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