using AAModClassic.Globals;
using AAModClassic.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.Items._BossInfinityZero
{
    public class Infinitium : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Materials";
        private static Asset<Texture2D> GlowTexture;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Infinitium");
            // Tooltip.SetDefault("Pure, unpredictable malice");
            // ticksperframe, frameCount
            
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(5, 10));
            if (Main.netMode != NetmodeID.Server)
                GlowTexture = ModContent.Request<Texture2D>(Texture + "_Glow");


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

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 52;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
        }

        public override void PostUpdate()
        {
            Lighting.AddLight(Item.Center, Color.Red.ToVector3() * 0.55f * Main.essScale);
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            var frame = TextureAssets.Item[Type].Frame(1, 10, 0, (int)(Main.GlobalTimeWrappedHourly * 10) % 10);
            var position = Item.Center - Main.screenPosition;
            var origin = frame.Size() / 2f;
            spriteBatch.Draw(TextureAssets.Item[Type].Value, position, frame, lightColor, rotation, origin, scale, SpriteEffects.None, 0);
            spriteBatch.Draw(GlowTexture.Value, position, frame, lightColor, rotation, origin, scale, SpriteEffects.None, 0);
        }
    }
}