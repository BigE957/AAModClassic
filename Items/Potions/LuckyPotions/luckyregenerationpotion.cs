using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using AAModClassic;

namespace AAModClassic.Items.Potions.LuckyPotions
{
    public class LuckyRegenerationPotion : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Lucky Regeneration Potion");
			// Tooltip.SetDefault("Provides life regeneration");
		}
		
		public override void SetDefaults()
		{
            Item.UseSound = SoundID.Item3;
            Item.useStyle = ItemUseStyleID.EatFood;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 15;
			Item.maxStack = 30;
			Item.consumable = true;
			Item.width = 16;
			Item.height = 16;
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.rare = ItemRarityID.Lime;
			Item.buffType = ModContent.BuffType<LuckyRegeneration_Buff>();
			Item.buffTime = 18000;
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
	}

	public class LuckyRegeneration : ModBuff
	{
        public override string Texture => "Terraria/Images/Buff_2";

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Regeneration");
			// Description.SetDefault("Provides life regeneration");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = false;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffImmune[2] = true;
			player.lifeRegen += 5;
		}
	}
}
