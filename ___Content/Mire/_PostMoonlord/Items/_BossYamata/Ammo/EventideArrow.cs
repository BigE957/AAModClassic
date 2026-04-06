using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using System.Collections.Generic;
using AAModClassic.Globals;

namespace AAModClassic.___Content.Mire._PostMoonlord.Items._BossYamata.Weapons
{
    public class EventideArrow : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Eventide Arrow");
			/* Tooltip.SetDefault(@"Blinds its target with the darkness of the moonless night
Inflicts Moonraze
Non-consumable"); */
		}

		public override void SetDefaults()
		{
			Item.damage = 23;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 14;
			Item.height = 40;
			Item.consumable = false;
			Item.knockBack = 7f;
			Item.value = Item.sellPrice(0, 30, 0, 0); ;
			Item.rare = ItemRarityID.LightPurple;
			Item.shoot = ModContent.ProjectileType<EventideArrow_Proj>();
			Item.shootSpeed = 3f;
			Item.ammo = AmmoID.Arrow;
            Item.rare = ItemRarityID.Cyan; AARarity = 13;
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
            recipe.AddIngredient(null, "EventideAbyssium", 1);
            recipe.AddIngredient(null, "DreadScale", 1);
            recipe.AddIngredient(ItemID.MoonlordArrow, 999);
            recipe.AddTile(null, "ACS");
			recipe.Register();
		}
	}
}
