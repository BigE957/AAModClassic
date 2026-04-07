using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic;
using AAModClassic.Items.Armor.Abyssal;
using AAModClassic.Items.Materials;

namespace AAModClassic.Items.Armor.Chaos
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
            return body.type == ModContent.ItemType<ChaosDou>() && legs.type == ModContent.ItemType<ChaosGreaves>();
        }

        public override void UpdateArmorSet(Player player){
            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.ChaosFukumenBonus");
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
			recipe.AddIngredient(ModContent.ItemType<AbyssalFukumen>());
			recipe.AddIngredient(ModContent.ItemType<ChaosCrystal>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Items.Armor.Dynaskull.Dynaskull>());
            recipe.AddIngredient(ModContent.ItemType<ChaosCrystal>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}