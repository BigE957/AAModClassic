using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic.Items.Materials;
using AAModClassic.___Content.Inferno.___PreHardmode.Items.Armor;
using AAModClassic.___Content.Snow.___PreHardmode.Items.Armor;

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
            return body.type == ModContent.ItemType<ChaosDou>() && legs.type == ModContent.ItemType<ChaosGreaves>();
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
            recipe.AddIngredient(ModContent.ItemType<BlazingHelmet>());
            recipe.AddIngredient(ModContent.ItemType<ChaosPrism>());
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RaiderHelmet>());
            recipe.AddIngredient(ModContent.ItemType<ChaosPrism>());
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
	}
}