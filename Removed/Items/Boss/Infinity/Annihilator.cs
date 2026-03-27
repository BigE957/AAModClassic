using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using System.Collections.Generic;
using Terraria.Audio;
using AAModClassic;
using AAModClassic.Globals;

namespace AAModClassic.Removed.Items.Boss.Infinity
{
    public class Annihilator : ModItem
	{
        
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Fires a quantum laser that creates an immensely powerful singularity");
            
        }

        public override void SetDefaults()
		{
			Item.damage = 420;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 34;
			Item.height = 58;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 0;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            //TODOIZ
            //Item.UseSound = new LegacySoundStyle(2, 75, Terraria.Audio.SoundType.Sound);
            Item.autoReuse = true;
            Item.shoot = Mod.Find<ModProjectile>("Anhialation").Type;
			Item.shootSpeed = 8f;
            
		}


        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Mod.GetTexture("Removed/Glowmasks/" + GetType().Name + "_Glow");
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

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.IZ;
                }
            }
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, 0);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Neutralizer", 1);
            recipe.AddIngredient(null, "Infinitium", 12);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }
    }
}
