using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using AAModClassic;

namespace AAModClassic.Items.Armor.Stone
{
    [AutoloadEquip(EquipType.Head)]
	public class StoneSoldierMask : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Stone Soldier Helmet");
			/* Tooltip.SetDefault(@"Increases mining speed by 10%
Provides light & spelunker effect when worn"); */
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 16;
		}

		public override void UpdateEquip(Player player)
		{
			player.findTreasure = true;
			player.pickSpeed -= 0.15f;

			Lighting.AddLight((int)player.Center.X, (int)player.Center.Y, 1f, 0.95f, .8f);
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == Mod.Find<ModItem>("StoneSoldierPlate").Type && legs.type == Mod.Find<ModItem>("StoneSoldierGreaves").Type;
        }

        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.AAMod.Equipset.StoneSoldierMaskBonus");

			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			modPlayer.StoneSoldier = true;

			player.discountAvailable = true;
			player.coins = true;
			player.goldRing = true;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MiningHelmet);
            recipe.AddIngredient(null, "StoneShell", 6);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}