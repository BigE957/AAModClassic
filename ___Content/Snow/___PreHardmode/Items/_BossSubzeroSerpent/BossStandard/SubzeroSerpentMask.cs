using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Snow.___PreHardmode.Items._BossSubzeroSerpent.BossStandard
{
    [AutoloadEquip(EquipType.Head)]
	public class SubzeroSerpentMask : BaseAAItem
	{
        public static int type;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Subzero Serpent Mask");
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