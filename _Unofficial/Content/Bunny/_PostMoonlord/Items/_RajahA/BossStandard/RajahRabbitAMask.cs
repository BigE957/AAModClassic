using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content.Bunny._PostMoonlord.Items._RajahA.BossStandard
{
    [AutoloadEquip(EquipType.Head)]
	public class RajahRabbitAMask : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Champion Rajah Rabbit Mask");
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