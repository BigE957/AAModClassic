using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.Attributes;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRetriever.BossStandard
{
    [AutoloadEquip(EquipType.Head)]
    [AutoloadEquipGlow(EquipType.Head)]
    public class RetrieverMask : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Vanity.Masks";
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Retriever Mask");

            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;

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