using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Ammo
{
    public class UnstablePowerCell : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Ammo";
        public static Asset<Texture2D> glowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Unstable Power Cell");
            /* Tooltip.SetDefault(@"Acts as a bullet
Non-consumable"); */

            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(10, 4));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
            if (Main.netMode != NetmodeID.Server)
                glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
        }

        public override void SetDefaults()
		{
			Item.damage = 40;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 20;
			Item.height = 32;
			Item.consumable = false;
			Item.knockBack = 7f;
			Item.value = Item.sellPrice(0, 30, 0, 0);
			Item.rare = ItemRarityID.LightPurple;
			Item.shoot = ModContent.ProjectileType<UnstablePowerCell_Proj>();
			Item.shootSpeed = 0f;
			Item.ammo = AmmoID.Bullet;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe(1);
			recipe.AddIngredient(ItemID.MoonlordBullet, 999);
            recipe.AddIngredient(ModContent.ItemType<ApocalyptitePlate>(), 1);
            recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 1);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
			recipe.Register();
		}

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            var frame = TextureAssets.Item[Type].Frame(1, 10, 0, (int)(Main.GlobalTimeWrappedHourly * 5) % 4);
            var position = Item.Center - Main.screenPosition;
            var origin = frame.Size() / 2f;
            spriteBatch.Draw(glowmask.Value, position, frame, lightColor, rotation, origin, scale, SpriteEffects.None, 0);
        }
    }
}
