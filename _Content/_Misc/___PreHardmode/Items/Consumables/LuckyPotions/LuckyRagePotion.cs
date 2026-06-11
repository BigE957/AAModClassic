using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content._Misc.___PreHardmode.Items.Consumables.LuckyPotions
{
    public class LuckyRagePotion : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Lucky Rage Potion");
			// Tooltip.SetDefault("Increases critical chance by 11%");
		}
		
		public override void SetDefaults()
		{
            Item.UseSound = SoundID.Item3;
            Item.useStyle = ItemUseStyleID.EatFood;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 15;
			Item.maxStack = Item.CommonMaxStack;
			Item.consumable = true;
			Item.width = 16;
			Item.height = 16;
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.rare = ItemRarityID.Lime;
			Item.buffType = ModContent.BuffType<LuckyRage>();
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

	public class LuckyRage : ModBuff
	{
        public override string Texture => "Terraria/Images/Buff_115";

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Rage");
			// Description.SetDefault("11% increased critical chance");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = false;
            BuffID.Sets.LongerExpertDebuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
		{
			player.buffImmune[115] = true;
			player.GetCritChance(DamageClass.Melee) += 11;
			player.GetCritChance(DamageClass.Ranged) += 11;
			player.GetCritChance(DamageClass.Magic) += 11;
			player.GetCritChance(DamageClass.Throwing) += 11;
		}
	}
}
