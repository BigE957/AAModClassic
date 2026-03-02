using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Armor.PerfectChaos
{
    [AutoloadEquip(EquipType.Legs)]
	public class PerfectChaosGreaves : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chaos Slayer Greaves");
            /* Tooltip.SetDefault(@"45% increased movement speed
2% increased damage resistance
The power of discordian rage radiates from this armor"); */
        }

		public override void SetDefaults()
		{
            Item.width = 22;
            Item.height = 16;
            Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.defense = 35;
            Item.rare = 9;
            AARarity = 14;
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
            player.endurance += .02f;
            player.moveSpeed += .45f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DracoLeggings", 1);
            recipe.AddIngredient(null, "DreadBoots", 1);
            recipe.AddIngredient(null, "Discordium", 4);
            recipe.AddIngredient(null, "ChaosScale", 4);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }

        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D Glow = Mod.GetTexture("Glowmasks/PerfectChaosGreaves_Glow");
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