using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Terrarium.Buffs;
using AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Accessories;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Acropolis._PostMoonlord.Items._BossAthenaA.Accessories
{
    public class GoddessHarp : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Goddess Harp");
			/* Tooltip.SetDefault(@"Summons the seraph queen herself to fight with you
Athena is boosted by minion damage"); */
        }

	    public override void SetDefaults()
	    {
	        Item.width = 20;
	        Item.height = 26;
            Item.value = Item.buyPrice(0, 15, 0, 0);
            Item.rare = ItemRarityID.Purple;
	        Item.accessory = true;
            Item.expert = true;
	    }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                return true;

            return incomingItem.type != ModContent.ItemType<SeraphHarp>();
        }

        public override void RegisterEquipStats()
        {
            AddEffect<GoddessHarpEffect>();
        }

        public override void RegisterAccVanity()
        {
            AddEffect<GoddessHarpEffect>();
        }
    }
}
