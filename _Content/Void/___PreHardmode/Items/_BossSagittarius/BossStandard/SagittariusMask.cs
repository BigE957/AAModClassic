using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void.___PreHardmode.Items._BossSagittarius.BossStandard
{
    [AutoloadEquip(EquipType.Head)]
	public class SagittariusMask : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Vanity.Masks";
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Sagittarius Mask");
		}

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 26;
            Item.rare = ItemRarityID.Green;
            Item.vanity = true;
        }
    }
}