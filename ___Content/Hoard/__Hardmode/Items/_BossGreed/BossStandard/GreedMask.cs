using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Hoard.__Hardmode.Items._BossGreed.BossStandard
{
    [AutoloadEquip(EquipType.Head)]
	public class GreedMask : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Greed Mask");
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