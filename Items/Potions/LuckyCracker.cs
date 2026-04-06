using AAModClassic;
using AAModClassic.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Potions
{
    public class LuckyCracker : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Lucky Cracker");
			// Tooltip.SetDefault("She said it can make you lucky. Do you trust her?");
		}
		
		public override void SetDefaults()
		{
            Item.UseSound = SoundID.Item2;
            Item.useStyle = ItemUseStyleID.EatFood;
			Item.useTurn = true;
			Item.useAnimation = 15;
			Item.useTime = 15;
			Item.maxStack = 30;
			Item.consumable = true;
			Item.width = 16;
			Item.height = 16;
			Item.value = Item.sellPrice(0, 1, 0, 0);
			Item.rare = ItemRarityID.Lime;
			Item.buffType = BuffID.WellFed;
			Item.buffTime = 52000;
			Item.buffTime = 18000;
		}

		public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            player.AddBuff(ModContent.BuffType<CrasyLucky_Buff>(), 3600);
            return base.UseItem(player);
        }
	}
}
