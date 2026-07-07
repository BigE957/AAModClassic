using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.Attributes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items._BossEquinoxWorms.BossStandard
{
    [AutoloadEquip(EquipType.Head)]
    [AutoloadEquipGlow(EquipType.Head)]
    public class NightcrawlerMask : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Vanity.Masks";
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Nightcrawler Mask");
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