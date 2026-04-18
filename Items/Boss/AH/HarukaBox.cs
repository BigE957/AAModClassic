using AAModClassic.___Content.Chaos._PostMoonlord.Items._BossHaruka.Armor;
using AAModClassic.Globals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.AH
{
    public class HarukaBox : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.consumable = true;
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> list)
        {
            foreach (Terraria.ModLoader.TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity12;
                }
            }
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Haruka's Lockbox");
            /* Tooltip.SetDefault(@"Right click to open
Contains a set of Midnight Assassin clothes"); */
        }

        public override bool CanRightClick()
        {
            return true;
        }
        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MidnightAssassinHelmet>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MidnightAssassinChestplate>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MidnightAssassinLeggings>());
        }
    }
}
