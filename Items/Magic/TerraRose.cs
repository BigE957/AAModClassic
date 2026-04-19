using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace AAModClassic.Items.Magic
{
    public class TerraRose : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Rose");
            /* Tooltip.SetDefault(@"Some say this staff was used by the legendary hero themselves
Projectiles go through walls
Right Clicking fires a piercing rose"); */
			Item.staff[Item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}

		public override void SetDefaults()
		{
			Item.damage = 150;
			Item.DamageType = DamageClass.Magic;
			Item.mana = 18;
			Item.width = 68;
			Item.height = 60;
			Item.useTime = 12;
			Item.useAnimation = 12;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 6;
            Item.value = Item.sellPrice(0, 20, 0, 0);
            Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item20;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<Projectiles.TerraRoseShot>();
			Item.shootSpeed = 15f;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.shoot = ModContent.ProjectileType<Projectiles.TerraRose_Proj>();
                Item.damage = 40;
                Item.useTime = 30;
                Item.useAnimation = 30;
                Item.knockBack = 1;
            }
            else
            {
                Item.shoot = ModContent.ProjectileType<Projectiles.TerraRoseShot>();
                Item.damage = 150;
                Item.useTime = 12;
                Item.useAnimation = 12;
                Item.knockBack = 6;
            }
            return base.CanUseItem(player);
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = Mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            Texture2D texture2 = TextureAssets.Item[Item.type].Value;
            spriteBatch.Draw(texture2, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            for (int i = 0; i < 4; i++)
            {
                spriteBatch.Draw(texture, position, null, Main.DiscoColor, 0, origin, scale, SpriteEffects.None, 0f);
            }
            return false;
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
                Main.DiscoColor,
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
			recipe.AddIngredient(ModContent.ItemType<TrueManaRose>(), 1);
            recipe.AddIngredient(ItemID.RainbowRod, 1);
            recipe.AddIngredient(ModContent.ItemType<Materials.TerraPrism>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
    }
}