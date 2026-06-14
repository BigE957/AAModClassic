using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content._Dev.DevTools
{
    public class NoodleSword : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("[DEV] Noodle Sword");
            // Tooltip.SetDefault(@"Top 10 op weapons in video games");
        }

        public override void SetDefaults()
        {
            Item.damage = 10000;     
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;    
            Item.width = 64;            
            Item.height = 70;         
            Item.useTime = 17;   
            Item.useAnimation = 17;     
            Item.useStyle = ItemUseStyleID.Swing;       
            Item.knockBack = 4;   
            Item.value = 0;        
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;   
            Item.useTurn = true;
            Item.expert = true; Item.expertOnly = true;
			Item.shoot = ModContent.ProjectileType<Noodle>();
			Item.shootSpeed = 9f;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = TextureAssets.Item[Item.type].Value;
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
    }
}
