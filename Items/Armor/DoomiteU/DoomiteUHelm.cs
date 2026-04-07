using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using AAModClassic;
using AAModClassic.Items.Materials;


namespace AAModClassic.Items.Armor.DoomiteU
{
    [AutoloadEquip(EquipType.Head)]
	public class DoomiteUHelm : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Dark Doomite Helmet");
            // Tooltip.SetDefault(@"Increases minion damage by 5%");
        }

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 3;
        }
		
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<DoomiteUPlate>() && legs.type == ModContent.ItemType<DoomiteUGreaves>();
		}

		public override void UpdateArmorSet(Player player)
		{
		    player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.DoomiteUHelmBonus");
            player.maxMinions += 2;
			player.GetKnockback(DamageClass.Summon).Base += 1f;
        }
		
		public override void UpdateEquip(Player player)
		{
            player.GetDamage(DamageClass.Summon) += 0.05f;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DoomiteScrap>(), 6);
            recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}