using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Items.Boss.Rajah;

namespace AAModClassic.___Content.Bunny.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
	public class HoppingHoodlumLeggings : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Hopping Hoodlum Paws");
            /* Tooltip.SetDefault(@"10% increased movement speed
9% increased melee critical strike chance
+1 Max Minion
Enemies are more likely to target you
Hopping Mad."); */
        }

		public override void SetDefaults()
		{
            Item.width = 22;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.defense = 17;
            Item.rare = ItemRarityID.Yellow;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += .08f;
            player.GetCritChance(DamageClass.Melee) += 8;
            player.maxMinions += 1;
            player.aggro += 2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RajahPelt>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}