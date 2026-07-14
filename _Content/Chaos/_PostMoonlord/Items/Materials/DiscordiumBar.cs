using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Globals;
using ReLogic.Content;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items.Materials
{
    public class DiscordiumBar : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        public static Asset<Texture2D> glowmask;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Discordium");
            // Tooltip.SetDefault("The World Chaoses melded together into a single, powerful bar");
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(4, 9));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
            if (Main.netMode != NetmodeID.Server)
                glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
        }

        

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 10;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = 10000;
            Item.rare = ItemRarityID.Purple;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DaybreakIncineriteBar>(), 1);
            recipe.AddIngredient(ModContent.ItemType<EventideAbyssiumBar>(), 1);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.Magenta.ToVector3() * 0.55f * Main.essScale);
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            var frame = TextureAssets.Item[Type].Frame(1, 10, 0, (int)(Main.GlobalTimeWrappedHourly * 8) % 8);
            var position = Item.Center - Main.screenPosition;
            var origin = frame.Size() / 2f;
            spriteBatch.Draw(glowmask.Value, position, frame, lightColor, rotation, origin, scale, SpriteEffects.None, 0);
        }
    }
}