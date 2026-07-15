using AAModClassic.Globals;
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

namespace AAModClassic._Content.Chaos._PostMoonlord.Items.Materials
{
    public class ChaosScale : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        public static Asset<Texture2D> glowmask;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Scale");
            // Tooltip.SetDefault("Chaos radiates from this blazing scale");
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(4, 6));
            ItemID.Sets.AnimatesAsSoul[Type] = true;

            Item.ResearchUnlockCount = 25;

            if (Main.netMode != NetmodeID.Server)
                glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 42;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Cyan;
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.Indigo.ToVector3() * 0.55f * Main.essScale);
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            var frame = TextureAssets.Item[Type].Frame(1, 10, 0, (int)(Main.GlobalTimeWrappedHourly * 8) % 6);
            var position = Item.Center - Main.screenPosition;
            var origin = frame.Size() / 2f;
            spriteBatch.Draw(glowmask.Value, position, frame, lightColor, rotation, origin, scale, SpriteEffects.None, 0);
        }
    }
}