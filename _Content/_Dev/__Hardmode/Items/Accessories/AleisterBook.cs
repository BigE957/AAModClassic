using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using AAModClassic._Content._Dev.Invoker;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOff)]
	public class AleisterBook : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Aleister Book");
            /* Tooltip.SetDefault(@"A Legendary Book of the Mega Therion.
10% increased minion damage
+2 minion slots
Maybe you could make it stronger..?
There's a note written on the cover: 
I need more powerful souls, *****,*********,**********"); */
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
            Item.width = 18;
            Item.height = 20;
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 1;
            Item.expertOnly = true;
            Item.useTime = 30;
            Item.useAnimation = 30;
        }

        public override bool CanUseItem(Player player)
		{
            return false;
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Summon) += .1f;
            player.maxMinions += 2;

            InvokerPlayer InvokerPlayer = InvokerPlayer.ModPlayer(player);
            //InvokerPlayer.BanishProjClear = true;  //This need change.
            InvokerPlayer.Thebookoflaw = true;
        }
    }
}