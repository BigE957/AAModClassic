using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic._Content._EX._PostMoonlord.Items.Accessories;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Accessories
{
    [AutoloadEquip(EquipType.HandsOff)]
	public class AleisterBook : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Accessories";
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

            TheBookOfTheLaw_InvokerPlayer InvokerPlayer = TheBookOfTheLaw_InvokerPlayer.ModPlayer(player);
            //InvokerPlayer.BanishProjClear = true;  //This need change.
            InvokerPlayer.Thebookoflaw = true;
        }
    }

    [AutoloadEquip(EquipType.Head)]
    public class InvokerHead : EquipTexture
    {
    }

    [AutoloadEquip(EquipType.Body)]
    public class InvokerBody : EquipTexture
    {
        public override void PreUpdateVanitySet(Player player)
        {
            ArmorIDs.Body.Sets.HidesTopSkin[Slot] = true;
        }
    }

    [AutoloadEquip(EquipType.Legs)]
    public class InvokerLegs : EquipTexture
    {
        public override void PreUpdateVanitySet(Player player)
        {
            ArmorIDs.Legs.Sets.HidesBottomSkin[Slot] = true;
        }
    }
}