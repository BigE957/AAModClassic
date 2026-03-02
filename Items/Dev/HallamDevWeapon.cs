using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Dev
{
    public class HallamDevWeapon : BaseAAItem
	{
        
        public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Prismeow Spectrum");
            /* Tooltip.SetDefault(@"Summons a Legendary Rainbow Cat at cursor point
Shoots Rainbow Bolts that move in the direction of your cursor
Warning: Using this WILL lag your game!
Prismeow EX"); */
            Item.staff[Item.type] = true;
        }

		public override void SetDefaults()
		{
            
			Item.damage = 50;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 200;
			Item.width = 52;
            Item.height = 52;
			Item.useTime = 60;
			Item.useAnimation = 60;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 3;
			Item.value = Item.sellPrice(0, 30, 0, 0);
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = SoundID.Item44;
			Item.autoReuse = false;
			Item.shoot = Mod.Find<ModProjectile>("RainbowCatPro").Type;
			Item.shootSpeed = 0f;
            Item.expert = true; Item.expertOnly = true;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_NONE; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            position = Main.MouseWorld;
            return true;
        }
        

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(null, "Prismeow");
                recipe.AddIngredient(null, "EXSoul");
                recipe.Register();
            }
        }
    }
}