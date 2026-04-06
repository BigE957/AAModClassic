using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AAModClassic.Items.Armor.Chaos
{
    [AutoloadEquip(EquipType.Head)]
	public class ChaosMask : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Chaos Mask");
            /* Tooltip.SetDefault(@"Increases maximum mana by 80
Increases magic damage by 20%
Increases magic crit by 20%
Allows you to breath underwater"); */
        }

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.defense = 18;
        }
		
		public override void UpdateEquip(Player player)
		{
			player.manaCost -= 0.3f;
            player.GetDamage(DamageClass.Magic) += 0.20f;
            player.gills = true;
            player.GetCritChance(DamageClass.Magic) += 20;
			player.statManaMax2 += 80;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ChaosDou>() && legs.type == ModContent.ItemType<ChaosGreaves>();
        }

        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.ChaosMaskBonus");
			if (player.wet)
			{
				player.AddBuff(ModContent.BuffType<ChaosBuff_Buff>(), 2);
            }
            player.accFlipper = true;
            player.ignoreWater = true;
        }
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<AtlanteanHelm>());
			recipe.AddIngredient(null, "ChaosCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}