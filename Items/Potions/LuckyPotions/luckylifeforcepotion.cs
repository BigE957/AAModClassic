using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace AAMod.Items.Potions.LuckyPotions
{
    public class luckylifeforcepotion : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Lucky Lifeforce Potion");
			// Tooltip.SetDefault("Increases max life by 21%");
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
			Item.buffType = Mod.Find<ModBuff>("luckylifeforce").Type;
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

	public class luckylifeforce : ModBuff
	{
		public override bool IsLoadingEnabled(Mod mod)
		{
			texture = "Terraria/Buff_113";
			return Mod.Properties/* tModPorter Note: Removed. Instead, assign the properties directly (ContentAutoloadingEnabled, GoreAutoloadingEnabled, MusicAutoloadingEnabled, and BackgroundAutoloadingEnabled) */.Autoload;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Lifeforce");
			// Description.SetDefault("21% increased max life");
			Main.debuff[Type] = false;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = false;
			longerExpertDebuff/* tModPorter Note: Removed. Use BuffID.Sets.LongerExpertDebuff instead */ = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffImmune[113] = true;
			player.lifeForce = true;
			player.statLifeMax2 += (int)(player.statLifeMax * .21f);
		}
	}
}
