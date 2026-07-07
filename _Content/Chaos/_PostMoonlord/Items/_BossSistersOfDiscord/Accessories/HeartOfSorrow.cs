using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Accessories
{
    public class HeartOfSorrow : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Heart of Sorrow");
            /* Tooltip.SetDefault(@"Your melee and ranged attacks grow stronger the less health you have
Melee and Ranged inflict Hydratoxin
Below 2/3 of your maximum life, Your movement speed is doubled
Below 1/3 of your maximum life, your melee and ranged attacks inflict Moonraze instead of Hydratoxin"); */
        }

        public override void SetDefaults()
        {
            Item.width = 66;
            Item.height = 78;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
            Item.expert = true;
            Item.defense = 3;
        }

        public override void RegisterEquipEffects()
        {
            AddEffect<HeartOfSorrowDamageBoostEffect>();
            AddEffect<HeartOfSorrowMovementSpeedEffect>();
            AddEffect<HeartOfSorrowDebuffEffect>();
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if (equippedItem.type == ModContent.ItemType<HeartOfPassion>())
                return false;

            return true;
        }
    }
}