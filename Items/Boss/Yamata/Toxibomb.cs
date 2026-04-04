using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using AAModClassic;
using AAModClassic.Globals;

namespace AAModClassic.Items.Boss.Yamata        //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class Toxibomb : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 250;                      
            Item.DamageType = DamageClass.Magic;  
            Item.width = 32;     
            Item.height = 28;    
            Item.useTime = 26; 
            Item.useAnimation = 26; 
            Item.useStyle = ItemUseStyleID.Shoot;        
            Item.noMelee = true;   
            Item.knockBack = 1; 
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.mana = 9;
            Item.UseSound = SoundID.Item20; 
            Item.autoReuse = true; 
            Item.shoot = ModContent.ProjectileType<SmallToxibomb>();  
            Item.shootSpeed = 20f;
            Item.rare = ItemRarityID.Cyan; AARarity = 13;
        }

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Abyssal Bomb");
			/* Tooltip.SetDefault(@"Fires off explosive spirit bombs
Small chance to fire an awakened bomb that explodes into abyss souls"); */
		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity13;
                }
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (Main.rand.Next(3) == 0)
            {
                type = ModContent.ProjectileType<Toxibomb>();
            }
            return true;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "EventideAbyssium", 5);
            recipe.AddIngredient(null, "DreadScale", 5);
            recipe.AddIngredient(null, "BogBomb", 1);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }
    }
}
