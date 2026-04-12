using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic.Items.Boss.Zero;
using AAModClassic.___Content.Void._PostMoonlord.Items.Materials;


namespace AAModClassic.Items.Armor.Doomsday
{
    [AutoloadEquip(EquipType.Legs)]
	public class DoomsdayLeggings : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Doomsday Assault Greaves");
			/* Tooltip.SetDefault(@"18% increased movement speed
120 increased mana
The power to destroy entire planets rests in this armor"); */

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

        public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 18;
			Item.value = 3000000;
			Item.defense = 28;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.18f;
            player.statManaMax2 += 120;
            player.GetModPlayer<AAPlayer>().MaxMovespeedboost += .18f;
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

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ApocalyptitePlate>(), 18);
            recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 5);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}