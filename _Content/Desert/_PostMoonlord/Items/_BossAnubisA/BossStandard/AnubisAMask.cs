using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Desert._PostMoonlord.Items._BossAnubisA.BossStandard
{
    [AutoloadEquip(EquipType.Head)]
	public class AnubisAMask : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Vanity.Masks";
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Forsaken Anubis Mask");
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