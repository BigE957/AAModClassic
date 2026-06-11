using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Armor;
using AAModClassic.Utilities.Attributes;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;


namespace AAModClassic._Content.Inferno._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    [AutoloadEquipGlow(EquipType.Head)]
    public class DraconianSunHelmet : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Draconian Sun Kabuto");
			/* Tooltip.SetDefault(@"20% increased melee critical chance
3% increased damage resistance
+25 Max Life
The blazing fury of the Inferno rests in this armor"); */

		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 22;
			Item.value = 3000000;
			Item.defense = 38;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        

        public override void UpdateEquip(Player player)
		{
			player.GetCritChance(DamageClass.Melee) += 20;
            player.endurance += .03f;
            player.statLifeMax2 += 25;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<DraconianSunChestplate>() && legs.type == ModContent.ItemType<DraconianSunLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.DracoHelmBonus");

            player.buffImmune[46] = true;
            player.buffImmune[47] = true;
            player.AddBuff(BuffID.Shine, 2);
            player.GetModPlayer<AAPlayer>().dracoSet = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DaybreakIncineriteBar>(), 15);
            recipe.AddIngredient(ModContent.ItemType<CrucibleScale>(), 5);
            recipe.AddIngredient(ModContent.ItemType<KindledHelmet>(), 1);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
			recipe.Register();
		}
	}
}