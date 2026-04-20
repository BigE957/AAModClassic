using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.___Content.Bunny.__Hardmode.Items.Materials;

namespace AAModClassic.___Content.Bunny.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
	public class HoppingHoodlumChestplate : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Hopping Hoodlum Shirt");
            /* Tooltip.SetDefault(@"10% increased melee speed
+1 max minion
Enemies are more likely to target you
Hopping Mad."); */
        }


        public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 20;
			Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 28;
		}

        public override void UpdateEquip(Player player)
		{
            player.GetAttackSpeed(DamageClass.Melee) += .1f;
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