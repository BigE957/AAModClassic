using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using AAModClassic;

namespace AAModClassic.Items.Armor.Viking
{
    [AutoloadEquip(EquipType.Body)]
	public class VikingPlate : BaseAAItem
	{
		public static int counter = 0;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Viking Platemail");
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
			Item.value = Item.sellPrice (0, 0, 5, 0);
			Item.rare = ItemRarityID.Orange;
			Item.defense = 9;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.GetDamage(DamageClass.Melee) += 0.07f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return head.type == Mod.Find<ModItem>("VikingHelm").Type && legs.type == Mod.Find<ModItem>("VikingBoots").Type;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.VikingPlateBonus");
            player.endurance += .04f;
        }
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null,"RelicBar", 14);
            recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}