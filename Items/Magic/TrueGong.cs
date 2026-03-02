using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Magic
{
    public class TrueGong : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.width = 50;
            Item.height = 64;
            Item.maxStack = 1;

            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = 6;
			Item.damage = 50;                        
            Item.DamageType = DamageClass.Magic;
			Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = 5;        
            Item.noMelee = true;
            Item.knockBack = 4;
			Item.mana = 13;             
            Item.UseSound = Mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/MOARGONG"); 
            Item.autoReuse = true;
            Item.shoot = 122;
			Item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The True Gong");
            // Tooltip.SetDefault("MORE GONG");
        }

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    float spread = 25f * 0.0174f;
		    float baseSpeed = (float)Math.Sqrt((speedX * speedX) + (speedY * speedY));
		    double startAngle = Math.Atan2(speedX, speedY)- (spread/2);
		    double deltaAngle = spread/5f;
		    double offsetAngle;
		    int i;
		    for (i = 0; i < 5;i++ )
		    {
		    	offsetAngle = startAngle + (deltaAngle * i);
		    	Projectile.NewProjectile(position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), Item.shoot, damage, knockBack, Main.myPlayer);
		    }
		    return false;
		}

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Gong");
            recipe.AddIngredient(ItemID.BrokenHeroSword);
            recipe.AddTile(TileID.MythrilAnvil);   
            recipe.Register();
        }
    }
}
