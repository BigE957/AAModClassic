using AAModClassic._Content.Acropolis._PostMoonlord.Items._BossAthenaA.Accessories;
using AAModClassic.UI.World;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories
{
    public class SeraphHarp : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Accessories";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Seraph Harp");
			/* Tooltip.SetDefault(@"Summons a seraph to fight for you
Seraph is boosted by minion damage"); */
		}

	    public override void SetDefaults()
	    {
	        Item.width = 20;
	        Item.height = 26;
            Item.value = Item.buyPrice(0, 15, 0, 0);
            Item.rare = ItemRarityID.Yellow;
	        Item.accessory = true;
            Item.expert = true;
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                return true;

            return incomingItem.type != ModContent.ItemType<GoddessHarp>();
        }

        public override void RegisterEquipEffects()
        {
            AddEffect<SeraphHarpEffect>();
        }

        public override void RegisterVanityEffects()
        {
            AddEffect<SeraphHarpEffect>();
        }
    }
}
