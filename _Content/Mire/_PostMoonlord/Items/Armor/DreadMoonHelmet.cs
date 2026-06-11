using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic.Globals;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Armor;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;


namespace AAModClassic._Content.Mire._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class DreadMoonHelmet : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dread Moon Fukumen");
			/* Tooltip.SetDefault(@"24% increased ranged critical chance
20% increased movement speed
+15 Max Life
The abyssal wrath of the Mire rests in this armor"); */

		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 22;
			Item.value = 3000000;
			Item.defense = 36;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        

        public override void UpdateEquip(Player player)
        {
            player.GetCritChance(DamageClass.Ranged) += 24;
            player.moveSpeed += .2f;
            player.GetModPlayer<AAPlayer>().MaxMovespeedboost += .2f;
            player.statLifeMax2 += 15;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<DreadMoonChestplate>() && legs.type == ModContent.ItemType<DreadMoonLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			
			player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.DreadHelmBonus");

            player.buffImmune[24] = true;
            player.buffImmune[39] = true;
            player.buffImmune[44] = true;
            player.buffImmune[67] = true;
            player.AddBuff(BuffID.Shine, 2);
            player.GetModPlayer<AAPlayer>().dreadSet = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EventideAbyssiumBar>(), 15);
            recipe.AddIngredient(ModContent.ItemType<DreadScale>(), 5);
            recipe.AddIngredient(ModContent.ItemType<DepthHelmet>(), 1);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
			recipe.Register();
		}
	}
}