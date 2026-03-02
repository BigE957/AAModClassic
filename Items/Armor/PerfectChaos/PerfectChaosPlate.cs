using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Armor.PerfectChaos
{
    [AutoloadEquip(EquipType.Body)]
	public class PerfectChaosPlate : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Chaos Slayer Plate");
            /* Tooltip.SetDefault(@"4% increased damage resistance
+75 Max Life
The power of discordian rage radiates from this armor"); */
        }


        public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 20;
			Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.rare = 9;
            AARarity = 14;
            Item.defense = 60;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity14;
                }
            }
        }

        public override void UpdateEquip(Player player)
		{
            player.endurance += .04f;
            player.GetAttackSpeed(DamageClass.Melee) += .15f;
            player.statLifeMax2 += 75;
        }
		
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DracoPlate", 1);
            recipe.AddIngredient(null, "DreadPlate", 1);
            recipe.AddIngredient(null, "Discordium", 10);
            recipe.AddIngredient(null, "ChaosScale", 10);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D Glow = Mod.GetTexture("Glowmasks/PerfectChaosPlate_Glow");
            spriteBatch.Draw(Glow, position, null, AAColor.Shen3, 0, origin, scale, SpriteEffects.None, 0f);
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
                AAColor.Shen3,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }
    }
}