using AAModClassic._Removed.Content._Tinker._PostMoonlord.Items.Accessories;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.GoblinArmy._PostMoonlord.Items.Consumables
{
    public class GoblinTinkererDoll : BaseAAItem
    {
        public new string LocalizationCategory => "Items.Consumables";
        public override void SetStaticDefaults()
        {
            /*DisplayName.SetDefault("Goblin Tinkerer Doll");
            Tooltip.SetDefault(@"I'm sorry, little one...");*/
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 28;
            Item.rare = 0;
            Item.value = 50000;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            base.ModifyTooltips(list);
            
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(204, 102, 0);
                }
            }
        }

        public override void PostUpdate()
        {
            if (Item.lavaWet)
            {
                for (int i = 0; i < 200; ++i)
                {
                    if (Main.npc[i].type == NPCID.GoblinTinkerer && Main.npc[i].active)
                    {
                        Player player = Main.LocalPlayer;
                        player.QuickSpawnItem(player.GetSource_GiftOrReward(), ModContent.ItemType<SoulStone>());
                        Main.npc[i].StrikeInstantKill();
                        if (Main.netMode != 1) 
                            BaseUtility.Chat("The soul stone materializes in your hand", 180, 120, 0);
                    }
                }
            }
        }
    }
}
