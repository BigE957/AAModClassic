using Terraria;
using Terraria.ModLoader;
using AAModClassic.Items.Vanity.Moon.Shiny;

namespace AAModClassic.Items.Vanity.Moon
{
    public class MoonBag : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Lunar Insect's Bag");
            // Tooltip.SetDefault("<right> to open \n'All the essentials for impersonating the Moon Bee!'");
        }

        public override void SetDefaults()
        {
            Item.maxStack = 1;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.expert = true; Item.expertOnly = true;  
        }

        public override bool CanRightClick()
        {
            return true;
        }

 		public override void RightClick(Player player)
        {
            if (Main.hardMode)
            {
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MoonWings>());
            }
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<Pets.LunaminiJar>());
            if (player.GetModPlayer<AAPlayer>().ShinyCheck())
            {
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<ShinyMoonHood>());
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<ShinyMoonRobe>());
                player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<ShinyMoonBoots>());
                return;
            }
			player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MoonHood>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MoonRobe>());
            player.QuickSpawnItem(Item.GetSource_Loot(), ModContent.ItemType<MoonBoots>());
        }
    }
}