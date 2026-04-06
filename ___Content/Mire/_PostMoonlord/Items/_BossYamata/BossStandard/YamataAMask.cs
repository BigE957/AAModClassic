using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._PostMoonlord.Items._BossYamata.BossStandard
{
    [AutoloadEquip(EquipType.Head)]
	public class YamataAMask : BaseAAItem
    {
        public static int type;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Yamata No Orochi Mask");
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