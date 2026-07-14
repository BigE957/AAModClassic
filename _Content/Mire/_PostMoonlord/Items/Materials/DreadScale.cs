using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire._PostMoonlord.Items.Materials
{
    public class DreadScale : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        public static Asset<Texture2D> glowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dread Scale");
            // Tooltip.SetDefault("The power of the dread moon is in your hands");
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(4, 9));
            ItemID.Sets.AnimatesAsSoul[Type] = true;
            if (Main.netMode != NetmodeID.Server)
                glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 34;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.Indigo.ToVector3() * 0.55f * Main.essScale);
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            var frame = TextureAssets.Item[Type].Frame(1, 10, 0, (int)(Main.GlobalTimeWrappedHourly * 8) % 9);
            var position = Item.Center - Main.screenPosition;
            var origin = frame.Size() / 2f;
            spriteBatch.Draw(glowmask.Value, position, frame, lightColor, rotation, origin, scale, SpriteEffects.None, 0);
        }
    }
}