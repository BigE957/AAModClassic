using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class BotchedBand : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 24;
            Item.value = Item.sellPrice(0, 8, 0, 0);
            Item.rare = 6;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed += .1f;
            player.GetDamage(DamageClass.Generic) += .1f;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Botched Band");
            /* Tooltip.SetDefault(
@"10% Increased movement speed and damage"); */
        }

    }
}