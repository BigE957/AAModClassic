using Terraria.ModLoader;
using Terraria;


namespace AAMod.Items.Boss.Toad
{
    [AutoloadEquip(EquipType.Shoes)]
    public class ToadLeg : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Truffle Legs");
            /* Tooltip.SetDefault(@"Increased jump speed and allows auto-jump
You are immune to fall damage
Increased jump height"); */
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 34;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = 5;
            Item.accessory = true;
            Item.expertOnly = true;
            Item.expert = true; Item.expertOnly = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.autoJump = true;
            Player.jumpHeight = 20;
            player.jumpSpeedBoost += 1.5f;
            player.noFallDmg = true;
        }
    }
}