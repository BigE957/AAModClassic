using System.Collections.Generic;
using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content._Dev._PostMoonlord.Items.Weapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class Light : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Magic";
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Light");
			// Tooltip.SetDefault(@"Etheral EX");
        }

	    public override void SetDefaults()
	    {
	        Item.damage = 300;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 20;
	        Item.width = 60;
	        Item.height = 26;
	        Item.useTime = 10;
	        Item.useAnimation = 10;
	        Item.reuseDelay = 5;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.UseSound = SoundID.Item13;
	        Item.noMelee = true;
            Item.noUseGraphic = true;
			Item.channel = true;
	        Item.knockBack = 0f;
	        Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.channel = true;
            Item.shoot = ModContent.ProjectileType<Light_Holdout>();
            Item.shootSpeed = 30f;           
            Item.expert = true; Item.expertOnly = true;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(159, 207, 190);
	            }
	        }
	    }

        public override void AddRecipes()  //How to craft this sword
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Ethereal>());
            recipe.AddIngredient(ModContent.ItemType<EXSoul>());
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
    }
}