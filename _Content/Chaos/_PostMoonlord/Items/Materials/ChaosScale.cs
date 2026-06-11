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
    public class ChaosScale : BaseAAItem
    {
        public short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Asset<Texture2D>[] glowMasks = new Asset<Texture2D>[TextureAssets.GlowMask.Length + 1];
                for (int i = 0; i < TextureAssets.GlowMask.Length; i++)
                {
                    glowMasks[i] = TextureAssets.GlowMask[i];
                }
                glowMasks[glowMasks.Length - 1] = ModContent.Request<Texture2D>(Texture + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                TextureAssets.GlowMask = glowMasks;
            }
            // DisplayName.SetDefault("Chaos Scale");
            // Tooltip.SetDefault("Chaos radiates from this blazing scale");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 6));
        }

        

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 42;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.glowMask = customGlowMask;
        }

        // The following 2 methods are purely to show off these 2 hooks. Don't use them in your own code.


        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.Indigo.ToVector3() * 0.55f * Main.essScale);
        }
    }
}