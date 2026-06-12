using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Void._PostMoonlord.Items.Armor;
using AAModClassic._Unreleased.Content.Void._PostMoonLord.Items._BossInfinityZero;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;


namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    [AutoloadEquipGlow(EquipType.Body)]
    public class InfinitySlayerChestplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Infinity Slayer Chestplate");
            /* Tooltip.SetDefault(@"35% increased ranged damage and critical strike chance
12% increased damage resistance
25% decreased ammo consumption
Infinite power and malice flows through this armor"); */
		}

		public override void SetDefaults()
		{
			Item.width = 30;
			Item.height = 20;
            Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.defense = 52;
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) *= 1.35f;
            player.endurance *= 1.12f;
            player.ammoCost75 = true;
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
            recipe.AddIngredient(ModContent.ItemType<DoomsdayChestplate>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Infinitium>(), 15);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
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
    }
}