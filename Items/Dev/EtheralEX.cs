using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Dev
{
    public class EtheralEX : BaseAAItem
	{
        
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
            Item.shoot = Mod.Find<ModProjectile>("EtheralEX").Type;
            Item.shootSpeed = 30f;           
            Item.expert = true; Item.expertOnly = true;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_NONE; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
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
            recipe.AddIngredient(null, "Etheral");
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }
    }
}