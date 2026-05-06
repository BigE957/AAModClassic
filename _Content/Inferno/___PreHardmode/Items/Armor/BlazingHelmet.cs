using AAModClassic._Content.Desert.___PreHardmode.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class BlazingHelmet : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Blazing Kabuto");
			/* Tooltip.SetDefault(@"1% increased Damage Resistance
3% increased Melee Damage
Forged in the flames of the blazing sun"); */
        }

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 20;
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 8;
		}

        public override void UpdateEquip(Player player)
        {
            player.endurance += .01f;
            player.GetDamage(DamageClass.Melee) += 0.03f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
            return body.type == ModContent.ItemType<BlazingChestplate>() && legs.type == ModContent.ItemType<BlazingLeggings>();
        }

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.BlazingBonus");
            player.aggro += 4;
            player.GetModPlayer<AAPlayer>().kindledSet = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<KindledHelmet>());
            recipe.AddIngredient(ItemID.Coral, 5);
            recipe.AddIngredient(ModContent.ItemType<DynaskullFossil>(), 10);
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
	}
}