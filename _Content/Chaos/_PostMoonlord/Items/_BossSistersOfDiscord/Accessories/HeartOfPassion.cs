using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Inferno.Buffs;
using AAModClassic.Globals;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Accessories
{
    public class HeartOfPassion : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Heart of Passion");
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
            AddEffect<HeartOfPassionDamageBoostEffect>();
            AddEffect<HeartOfPassionManaRegenEffect>();
            AddEffect<HeartOfPassionDebuffEffect>();
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if (equippedItem.type == ModContent.ItemType<HeartOfSorrow>())
                return false;

            return true;
        }
    }
}