using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using AAModClassic.Globals;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.Items.InfinityZero.Weapons
{
    public class TotalDestruction : ModItem
    {
        
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Total Destruction");
            // Tooltip.SetDefault("Destroys everything in front of you with a destructive laser");
        }

        public override void SetDefaults()
        {
            
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 7;
            Item.useTime = 7;
            Item.mana = 10;
            Item.shootSpeed = 16f;
            Item.knockBack = 0f;
            Item.width = 122;
            Item.reuseDelay = 5;
            Item.height = 32;
            Item.damage = 250;
            Item.UseSound = SoundID.Item13;
            Item.channel = true;
            Item.shoot = ModContent.ProjectileType<TotalDestruction>();
            Item.value = Item.sellPrice(1, 0, 0, 0);
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.autoReuse = true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-53, -4);
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Mod.GetTexture("_Unreleased/Glowmasks/" + GetType().Name + "_Glow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * .5f + 2f
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
        
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "DoomRay");
			recipe.AddIngredient(null, "Infinitium", 12);
	        recipe.AddTile(null, "ACS");
	        recipe.Register();
		}
	}
}
