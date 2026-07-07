using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using AAModClassic._Content._Misc.___PreHardmode.Items.Weapons;
using AAModClassic._Content.Terra.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content._Misc.__Hardmode.Items.Weapons
{
    public class TrueGong : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Magic";
        public override void SetDefaults()
        {

            Item.width = 50;
            Item.height = 64;
            Item.maxStack = Item.CommonMaxStack;

            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.LightPurple;
			Item.damage = 50;                        
            Item.DamageType = DamageClass.Magic;
			Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;        
            Item.noMelee = true;
            Item.knockBack = 4;
			Item.mana = 13;             
            Item.UseSound = new SoundStyle("AAModClassic/Sounds/MOARGONG"); 
            Item.autoReuse = true;
            Item.shoot = ProjectileID.TopazBolt;
			Item.shootSpeed = 10f;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("True Gong");
            // Tooltip.SetDefault("MORE GONG");
        }

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
		    float spread = 25f * 0.0174f;
            float baseSpeed = (float)Math.Sqrt(velocity.X * velocity.X + velocity.Y * velocity.Y);
            double startAngle = Math.Atan2(velocity.X, velocity.Y) - spread/2;
		    double deltaAngle = spread/5f;
		    double offsetAngle;
		    int i;
		    for (i = 0; i < 5;i++ )
		    {
		    	offsetAngle = startAngle + deltaAngle * i;
		    	Projectile.NewProjectile(source, position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), Item.shoot, damage, knockback, Main.myPlayer);
		    }
		    return false;
		}

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Gong>());
            recipe.AddIngredient(ItemID.ChlorophyteBar, 24);
            recipe.AddTile(TileID.MythrilAnvil);   
            recipe.Register();
        }
    }
}
