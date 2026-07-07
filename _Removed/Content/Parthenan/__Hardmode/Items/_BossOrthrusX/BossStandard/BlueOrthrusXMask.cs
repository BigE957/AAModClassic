using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossOrthrusX.BossStandard
{
    [AutoloadEquip(EquipType.Head)]
	public class BlueOrthrusXMask : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Vanity.Masks";
        public static int type;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Blue Orthrus X Mask");
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