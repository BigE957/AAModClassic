using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.BossStandard
{
    [AutoloadEquip(EquipType.Head)]
	public class YamataMask : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Vanity.Masks";
        public static int type;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Yamata Mask");
		}

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 26;
            Item.rare = ItemRarityID.Green;
            Item.vanity = true;
        }
    }
}