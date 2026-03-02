using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AAMod.Items.Armor.Chaos
{
    [AutoloadEquip(EquipType.Head)]
	public class ChaosFukumen : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Chaos Fukumen");
            // Tooltip.SetDefault(@"24% increased ranged critical strike chance");
        }

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.value = 50000;
			Item.rare = ItemRarityID.Lime;
			Item.defense = 15;
		}

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Ranged) += 24;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("ChaosDou").Type && legs.type == Mod.Find<ModItem>("ChaosGreaves").Type;
        }

        public override void UpdateArmorSet(Player player){
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.ChaosFukumenBonus");
            player.GetDamage(DamageClass.Ranged) += .25f;
            player.aggro -= 7;
            player.GetModPlayer<AAPlayer>().ChaosRa = true;
            player.ammoCost75 = true;
            player.nightVision = true;
			player.detectCreature = true;
        }

        public override void AddRecipes()
		{
            Recipe recipe;
            recipe = CreateRecipe();
			recipe.AddIngredient(Mod.Find<ModItem>("AbyssalFukumen").Type);
			recipe.AddIngredient(null, "ChaosCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(Mod.Find<ModItem>("Dynaskull").Type);
            recipe.AddIngredient(null, "ChaosCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}