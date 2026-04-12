using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using AAModClassic.Projectiles.Akuma;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic.Items.Materials;

namespace AAModClassic.Items.Boss.Akuma
{
    public class SunStorm : BaseAAItem
  {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Sunstorm");
			/* Tooltip.SetDefault(@"Summons orbiting fireballs which home to enemies after some time
Right click and hold to release and aim manually"); */
        }

        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.mana = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.damage = 450;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.width = 40;
            Item.height = 40;
            Item.shoot = ModContent.ProjectileType<SunstormFireball>();
            Item.shootSpeed = 20f;
            Item.knockBack = 4.5f;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.DamageType = DamageClass.Magic;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 13;
            Item.noMelee = true;
            Item.UseSound = SoundID.Item124;
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

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            bool AnyOrbiters = AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<SunstormFireball>());
            for (int Loops = 0; Loops < 4; Loops++)
            {
                Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position, velocity, type, damage, knockback, Main.myPlayer, 0, 0);
            }

            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<DaybreakIncineriteBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<CrucibleScale>(), 5);
            recipe.AddIngredient(ItemID.LunarFlareBook, 1);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}
