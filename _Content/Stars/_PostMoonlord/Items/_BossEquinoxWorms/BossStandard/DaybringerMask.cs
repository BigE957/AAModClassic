using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.Attributes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items._BossEquinoxWorms.BossStandard
{
    [AutoloadEquip(EquipType.Head)]
    [AutoloadEquipGlow(EquipType.Head)]
    public class DaybringerMask : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Vanity.Masks";
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Daybringer Mask");
		}

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 24;
            Item.rare = ItemRarityID.Green;
            Item.vanity = true;
        }
    }
}