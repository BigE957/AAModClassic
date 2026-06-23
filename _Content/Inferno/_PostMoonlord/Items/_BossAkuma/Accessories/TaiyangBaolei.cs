using AAModClassic._Content.Inferno.Buffs;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Accessories
{
    [AutoloadEquip(EquipType.Shield)]
    public class TaiyangBaolei : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Taiyang Baolei");
            /* Tooltip.SetDefault(@"Makes you immune to almost all debuffs
Allows you to do a fiery, explosive dash
Your melee & magic attacks set enemies ablaze
During the day, you gain 6% damage resistance and your melee & magic attacks inflict daybroken instead of 'On Fire!'"); */
        }

        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 32;
            Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.expert = true;
            Item.accessory = true;
            Item.defense = 8;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = TextureAssets.Item[Type].Value;
            Texture2D textureGlow = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            Texture2D texture2 = ModContent.Request<Texture2D>(Texture + "A").Value;
            Texture2D texture2Glow = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            if (!Main.dayTime)
            {
                spriteBatch.Draw
                (
                    texture,
                    new Vector2
                    (
                        Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                        Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                    ),
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    lightColor,
                    rotation,
                    texture.Size() * 0.5f,
                    scale,
                    SpriteEffects.None,
                    0f
                );
                spriteBatch.Draw
                (
                    textureGlow,
                    new Vector2
                    (
                        Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                        Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                    ),
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    lightColor,
                    rotation,
                    texture.Size() * 0.5f,
                    scale,
                    SpriteEffects.None,
                    0f
                );

                return false;
            }
            else
            {
                spriteBatch.Draw
                (
                    texture2,
                    new Vector2
                    (
                        Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                        Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                    ),
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    lightColor,
                    rotation,
                    texture.Size() * 0.5f,
                    scale,
                    SpriteEffects.None,
                    0f
                );
                spriteBatch.Draw
                (
                    texture2Glow,
                    new Vector2
                    (
                        Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                        Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                    ),
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    lightColor,
                    rotation,
                    texture.Size() * 0.5f,
                    scale,
                    SpriteEffects.None,
                    0f
                );

                return false;
            }
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = TextureAssets.Item[Type].Value;
            Texture2D texture2 = ModContent.Request<Texture2D>(Texture + "A").Value;
            if (!Main.dayTime)
            {
                spriteBatch.Draw(texture, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            }
            else
            {
                spriteBatch.Draw(texture2, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            }
            return false;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<AAPlayer>().Baolei = true;
            player.dashType = 3;
            player.buffImmune[20] = true;
            player.buffImmune[22] = true;
            player.buffImmune[23] = true;
            player.buffImmune[30] = true;
            player.buffImmune[31] = true;
            player.buffImmune[32] = true;
            player.buffImmune[33] = true;
            player.buffImmune[35] = true;
            player.buffImmune[36] = true;
            player.buffImmune[38] = true;
            player.buffImmune[44] = true;
            player.buffImmune[46] = true;
            player.buffImmune[47] = true;
            player.buffImmune[67] = true;
            player.buffImmune[69] = true;
            player.buffImmune[70] = true;
            player.buffImmune[120] = true;
            player.buffImmune[144] = true;
            player.buffImmune[153] = true;
            player.buffImmune[156] = true;
            player.buffImmune[195] = true;
            player.buffImmune[196] = true;
            player.buffImmune[197] = true;
            player.buffImmune[203] = true;
            player.buffImmune[ModContent.BuffType<DragonFire_Buff>()] = true;
            player.buffImmune[ModContent.BuffType<BurningAsh_Buff>()] = true;
            player.noKnockback = true;
            if (Main.dayTime)
            {
                player.endurance += 0.06f;
            }
        }
    }
}