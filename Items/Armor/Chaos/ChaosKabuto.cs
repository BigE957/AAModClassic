using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AAModClassic.Items.Armor.Chaos
{
    [AutoloadEquip(EquipType.Head)]
	public class ChaosKabuto : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chaos Kabuto");
			// Tooltip.SetDefault(@"25% increased melee damage");
        }

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 20;
			Item.value = 100000;
            Item.rare = ItemRarityID.Lime;
            Item.defense = 26;
		}

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += .25f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("ChaosDou").Type && legs.type == Mod.Find<ModItem>("ChaosGreaves").Type;
        }

        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.ChaosKabutoBonus");
            player.GetAttackSpeed(DamageClass.Melee) += .1f;
            player.aggro += 4;
            player.GetModPlayer<AAPlayer>().kindledSet = true;
            player.GetModPlayer<AAPlayer>().ChaosMe = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe;
            recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("BlazingKabuto").Type);
            recipe.AddIngredient(Mod.Find<ModItem>("ChaosCrystal").Type);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("RaiderHelm").Type);
            recipe.AddIngredient(Mod.Find<ModItem>("ChaosCrystal").Type);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
	}
}