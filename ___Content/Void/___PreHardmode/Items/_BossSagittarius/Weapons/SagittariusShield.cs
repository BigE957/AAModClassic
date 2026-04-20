using Terraria;

namespace AAModClassic.___Content.Void.___PreHardmode.Items._BossSagittarius.Weapons
{
    public class SagittariusShield : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sagittarius Shield");
            /* Tooltip.SetDefault(@"Pressing the accessory ability hotkey puts up a barrier around you to protect you from damage
While shielded, you cannot use items
While shielded, your health regeneration is increased dramatically
Shield lasts for 5 seconds
Shield has a 5 minute cooldown"); */
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 50;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.accessory = true;
            Item.expert = true; Item.expertOnly = true;
        }

        public override void UpdateAccessory(Player p, bool hideVisual)
        {
            p.GetModPlayer<AAPlayer>().SagShield = true;
        }
    }
}