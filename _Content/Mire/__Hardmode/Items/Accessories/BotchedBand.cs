using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.__Hardmode.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class BotchedBand : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Botched Band");
            /* Tooltip.SetDefault(
@"10% Increased movement speed and damage"); */
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 24;
            Item.value = Item.sellPrice(0, 8, 0, 0);
            Item.rare = ItemRarityID.LightPurple;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed += .1f;
            player.GetDamage(DamageClass.Generic) += .1f;
        }
    }
}