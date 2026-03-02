using AAModClassic.Globals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.AH
{
    public class HeartOfPassion : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Heart of Passion");
            /* Tooltip.SetDefault(@"Your magic attacks and minions grow stronger the less health you have
Magic attacks and Minions inflict 'On Fire!'
Below 2/3 of your maximum life, Your mana regenerates much faster
Below 1/3 of your maximum life, your magic attacks and minions inflict Daybroken instead of 'On Fire'"); */
        }
        public override void SetDefaults()
        {
            Item.width = 66;
            Item.height = 78;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
            Item.expert = true; Item.expertOnly = true;
            Item.defense = 3;
        }
        

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Magic) += 1 - player.statLife / player.statLifeMax;
            player.GetDamage(DamageClass.Summon) += 1 - player.statLife / player.statLifeMax;
            player.GetModPlayer<AAPlayer>().HeartP = true;

            if (player.statLife > (player.statLifeMax * (2/3)))
            {
                player.manaRegenBonus += 6;
            }
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == ModContent.ItemType<HeartOfSorrow>())
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity12;
                }
            }
        }
    }
}