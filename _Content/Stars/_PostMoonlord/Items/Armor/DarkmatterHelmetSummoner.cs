using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System;
using Terraria.Localization;
using Terraria.ID;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class DarkmatterHelmetSummoner : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Darkmatter Headress");
			/* Tooltip.SetDefault(@"25% increased minion damage
Dark, yet still barely visible"); */

		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 24;
			Item.value = 300000;
			Item.defense = 20;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
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

        public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Summon) += 0.25f;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<DarkmatterChestplate>() && legs.type == ModContent.ItemType<DarkmatterLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{


            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.DarkmatterHeaddressBonus");
            player.GetModPlayer<DarkmatterHelmetSummonerPlayer>().setBonus = true;
            player.armorEffectDrawShadowLokis = true;
        }

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DarkmatterBar>(), 25);
            recipe.AddIngredient(ModContent.ItemType<DarkEnergy>(), 10);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
	}
}