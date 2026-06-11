using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using AAModClassic.Globals;
using AAModClassic.Projectiles;
using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class RadiumHelmetSummoner : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Radium Hat");
			/* Tooltip.SetDefault(@"35% increased minion damage
Shines with the light of a starry night sky"); */
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 24;
			Item.value = 300000;
			Item.defense = 18;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
        }

        

        public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Summon) += 0.35f;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<RadiumChestplate>() && legs.type == ModContent.ItemType<RadiumLeggings>();
        }

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.RadiumHatBonus1") + (int)player.GetDamage(DamageClass.Summon).ApplyTo(RadiumHelmetSummonerPlayer_RadMinions.baseBlastDamage) + " " + Language.GetTextValue("Mods.AAModClassic.Common.RadiumHatBonus2");
            player.GetModPlayer<RadiumHelmetSummonerPlayer>().setBonus = true;
        }

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RadiumBar>(), 25);
            recipe.AddIngredient(ModContent.ItemType<RadiantPhoton>(), 10);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
	}
}