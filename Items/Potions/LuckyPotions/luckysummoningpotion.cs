using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using AAModClassic;

namespace AAModClassic.Items.Potions.LuckyPotions
{
    public class LuckySummoningPotion : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Lucky Summoning Potion");
			// Tooltip.SetDefault("Increases your max number of minions and turrents");
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
			Item.buffType = ModContent.BuffType<LuckySummoning>();
			Item.buffTime = 21600;
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

	public class LuckySummoning : ModBuff
	{
        public override string Texture =>  "Terraria/Images/Buff_110";

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Summoning");
			// Description.SetDefault("Increased max number of minions and turrents");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = false;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
		{
			player.buffImmune[110] = true;
			player.maxMinions += 2;
			player.maxTurrets ++;
		}
	}
}
