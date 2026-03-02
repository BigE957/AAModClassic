using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AAMod.Items.Armor.Chaos
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
            Item.rare = 7;
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
            return body.type == Mod.Find<ModItem>("ChaosDou").Type && legs.type == Mod.Find<ModItem>("ChaosGreaves").Type;
        }

        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.AAMod.Common.ChaosMaskBonus");
			if (player.wet)
			{
				player.AddBuff(Mod.Find<ModBuff>("ChaosBuff").Type, 2);
            }
            player.accFlipper = true;
            player.ignoreWater = true;
        }
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(Mod.Find<ModItem>("AtlanteanHelm").Type);
			recipe.AddIngredient(null, "ChaosCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}