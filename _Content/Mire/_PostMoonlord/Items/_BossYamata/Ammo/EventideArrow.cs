using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System.Collections.Generic;
using AAModClassic.Globals;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;

namespace AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Ammo
{
    public class EventideArrow : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Ammo";
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Eventide Arrow");
			/* Tooltip.SetDefault(@"Blinds its target with the darkness of the moonless night
Inflicts Moonraze
Non-consumable"); */
		}

		public override void SetDefaults()
		{
			Item.damage = 23;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 14;
			Item.height = 40;
			Item.consumable = false;
			Item.knockBack = 7f;
			Item.value = Item.sellPrice(0, 30, 0, 0); ;
			Item.rare = ItemRarityID.LightPurple;
			Item.shoot = ModContent.ProjectileType<EventideArrow_Proj>();
			Item.shootSpeed = 3f;
			Item.ammo = AmmoID.Arrow;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EventideAbyssiumBar>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DreadScale>(), 1);
            recipe.AddIngredient(ItemID.MoonlordArrow, 999);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
			recipe.Register();
		}
	}
}
