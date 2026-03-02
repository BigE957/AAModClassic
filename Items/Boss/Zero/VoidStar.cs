using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class VoidStar : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Void Star");
            // Tooltip.SetDefault("Fires a dark, spinning vortex that homes in on enemies");
            Item.staff[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.useStyle = 5;
            Item.useAnimation = 60;
            Item.useTime = 60;
            Item.shootSpeed = 10f;
            Item.knockBack = 0f;
            Item.width = 30;
            Item.height = 26;
            Item.damage = 700;
            Item.UseSound = SoundID.Item20;
            Item.shoot = Mod.Find<ModProjectile>("VoidStarPF").Type;
            Item.mana = 18;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.rare = 9; AARarity = 13;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity13;
                }
            }
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
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "ApocalyptitePlate", 5);
            recipe.AddIngredient(null, "UnstableSingularity", 5);
            recipe.AddIngredient(ItemID.NebulaArcanum);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }
    }
}
