using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.Weapons
{
    public class ThrowingCrescent : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Melee";
		public override void SetDefaults()
		{

            Item.damage = 300;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 30;
            Item.height = 30;
			Item.useTime = 5;
			Item.useAnimation = 8;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 1;
			Item.value = Item.sellPrice(0, 30, 0, 0);
			Item.shootSpeed = 15f;
			Item.shoot = ModContent.ProjectileType<ThrowingCrescent_Proj>();
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Throwing Crescent");
            // Tooltip.SetDefault("");
        }


        

        public override bool CanUseItem(Player player)       //this make that you can shoot only 1 boomerang at once
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
                {
                    return false;
                }
            }
            return true;
        }
        public override void AddRecipes()
        {
                Recipe recipe = CreateRecipe();
				recipe.AddIngredient(ModContent.ItemType<EventideAbyssiumBar>(), 5);
                recipe.AddIngredient(ModContent.ItemType<DreadScale>(), 5);
                recipe.AddIngredient(ItemID.LightDisc, 5);
				recipe.AddTile(TileID.LunarCraftingStation);
                recipe.Register();
		}
    }
}
