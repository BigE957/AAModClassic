using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Boss.Shen
{
    public class ChaosScale : BaseAAItem
    {
        public short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            if (Main.netMode != NetmodeID.Server)
            {
                Texture2D[] glowMasks = new Texture2D[TextureAssets.GlowMask.Value.Length + 1];
                for (int i = 0; i < TextureAssets.GlowMask.Value.Length; i++)
                {
                    glowMasks[i] = TextureAssets.GlowMask[i].Value;
                }
                glowMasks[glowMasks.Length - 1] = Mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                TextureAssets.GlowMask.Value = glowMasks;
            }
            // DisplayName.SetDefault("Chaos Scale");
            // Tooltip.SetDefault("Chaos radiates from this blazing scale");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 6));
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity14;
                }
            }
        }

        // TODO -- Velocity Y smaller, post NewItem?
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 42;
            Item.maxStack = 999;
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