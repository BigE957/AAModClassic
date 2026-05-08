using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons.RuneBook
{
    public class CCRuneBookPage : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("A Page of RuneBook");
            /* Tooltip.SetDefault(@"Summons runes according to your minion slots
When player has 1 minion slot, it summons bunny rune.
When player has 2 minion slots, it summons bunny and discord rune.
When player has 3 minion slots, it summons bunny, discord and energy rune."); */
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = Color.Gold;
                }
            }
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = 100000;
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            modPlayer.CCBook = true;
            if(hideVisual)
            {
                modPlayer.CCBook = false;
                player.ClearBuff(ModContent.BuffType<CCRune_Buff>());
            }
        }
    }
}