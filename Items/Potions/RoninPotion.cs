using AAModClassic.Buffs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Potions
{
    public class RoninPotion : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ronin Potion");
			// Tooltip.SetDefault("When you don't get hurt, you will have 3s immune time");
		}
		
		public override void SetDefaults()
        {
            Item.width = 20;
			Item.height = 34;
			Item.useTurn = true;
			Item.maxStack = Item.CommonMaxStack;
			Item.healLife = 50;
            Item.useAnimation = 17;
			Item.useTime = 17;
			Item.useStyle = ItemUseStyleID.EatFood;
			Item.UseSound = SoundID.Item3;
			Item.consumable = true;
			Item.potion = true;
			Item.value = 50000;
            Item.rare = ItemRarityID.LightRed;
		}

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if(player.statLife == player.statLifeMax2) player.AddBuff(ModContent.BuffType<Ronin_Buff>(), 180);
            return base.UseItem(player);
        }
	}
}