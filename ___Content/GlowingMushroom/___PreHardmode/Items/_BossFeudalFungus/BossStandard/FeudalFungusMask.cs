using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.GlowingMushroom.___PreHardmode.Items._BossFeudalFungus.BossStandard
{
    [AutoloadEquip(EquipType.Head)]
	public class FeudalFungusMask : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Feudal Fungus Mask");
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