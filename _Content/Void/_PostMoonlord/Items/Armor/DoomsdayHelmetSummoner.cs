using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;


namespace AAModClassic._Content.Void._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class DoomsdayHelmetSummoner : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doomsday Tactical Visor");
            /* Tooltip.SetDefault(@"50% increased minion damage
The power to destroy entire planets rests in this armor"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 3000000;
            Item.defense = 28;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += .5f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<DoomsdayChestplate>() && legs.type == ModContent.ItemType<DoomsdayLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			
			player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.DoomsdayMaskBonus");

            player.maxMinions += 5;
            player.AddBuff(BuffID.Hunter, 2);
            player.AddBuff(BuffID.NightOwl, 2);
            player.GetModPlayer<AAPlayer>().zeroSet1 = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ApocalyptitePlate>(), 15);
            recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 5);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
			recipe.Register();
		}
	}
}