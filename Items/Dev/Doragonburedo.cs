using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Dev
{
    public class Doragonburedo : BaseAAItem
    {
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doragonburedo");
            // Tooltip.SetDefault("'I'm gonna wipe their whole team' \n" + "-Jace");
            if (Main.netMode != NetmodeID.Server)
            {
                Asset<Texture2D>[] glowMasks = new Asset<Texture2D>[TextureAssets.GlowMask.Length + 1];
                for (int i = 0; i < TextureAssets.GlowMask.Length; i++)
                {
                    glowMasks[i] = TextureAssets.GlowMask[i];
                }
                glowMasks[glowMasks.Length - 1] = ModContent.Request<Texture2D>("AAModClassic/Glowmasks/" + GetType().Name + "_Glow");
                customGlowMask = (short)(glowMasks.Length - 1);
                TextureAssets.GlowMask = glowMasks;
            }
        }

        public override void SetDefaults()
        {
			Item.CloneDefaults(ItemID.Arkhalis);
            Item.glowMask = customGlowMask;
            Item.damage = 220;            //Sword damage
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;            //if it's melee
            Item.width = 56;              //Sword width
            Item.height = 56;             //Sword height
            Item.expert = true;
            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.knockBack = 6;      //Sword knockback
            Item.value = 100000;        
            Item.rare = ItemRarityID.Lime;
            Item.autoReuse = true;   //if it's capable of autoswing.
            Item.useTurn = true;
            Item.shoot = Mod.Find<ModProjectile>("Ryugen").Type;
        }
    }
}
