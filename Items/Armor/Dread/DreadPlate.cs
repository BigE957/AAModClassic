using AAModClassic;
using AAModClassic.Globals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.Items.Armor.Dread
{
    [AutoloadEquip(EquipType.Body)]
	public class DreadPlate : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Dread Moon Gi");
			/* Tooltip.SetDefault(@"35% increased ranged damage
20% increased movement speed
+50 Max Life
The abyssal wrath of the Mire rests in this armor"); */
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 20;
			Item.value = 3000000;
			Item.defense = 44;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 13;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity13;
                }
            }
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += .35f;
            player.moveSpeed += .2f;
            player.GetModPlayer<AAPlayer>().MaxMovespeedboost += .2f;
            player.statLifeMax2 += 50;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "EventideAbyssium", 20);
            recipe.AddIngredient(null, "DreadScale", 5);
            recipe.AddIngredient(null, "DepthGi", 1);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }
    }
}