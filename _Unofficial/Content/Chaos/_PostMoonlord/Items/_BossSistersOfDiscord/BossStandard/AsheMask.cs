using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.BossStandard
{
    [AutoloadEquip(EquipType.Head)]
	public class AsheMask : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Ashe Mask");
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