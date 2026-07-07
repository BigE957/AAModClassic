using AAModClassic._Content._Tinker.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Accessories;
using AAModClassic._Content.Chaos.Buffs;
using AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Accessories;
using AAModClassic._Content.Inferno.Buffs;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Accessories;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic._Content.Terrarium.Buffs;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class ClawOfChaos : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Claw of Chaos");
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 30;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Expert;
            Item.expert = true;
            Item.accessory = true;
        }

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Default).Flat += 5;
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if (equippedItem.type == ModContent.ItemType<BulwarkOfChaos>())
                return false;

            return true;
        }
    }
}