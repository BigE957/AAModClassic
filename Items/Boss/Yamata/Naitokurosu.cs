using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Yamata
{
    [AutoloadEquip(EquipType.Neck)]
    public class Naitokurosu : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Naitokurosu");
            /* Tooltip.SetDefault(@"8% increased ranged damage
Grants you the abilities of a true master ninja
Allows you to do a speedy dash
You move twice as fast and your ranged attacks & minions inflict Venom
While in the mire, you gain 18% increased ranged damage instead of 9%
At night, you move three times as fast and your ranged attacks & minions inflict Moonraze"); */
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.expert = true; Item.expertOnly = true;
            Item.accessory = true;
            Item.rare = ItemRarityID.Cyan; AARarity = 13;
        }


        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            Texture2D texture = Mod.GetTexture("Items/Boss/Yamata/Naitokurosu");
            Texture2D textureGlow = Mod.GetTexture("Glowmasks/Naitokurosu_Glow");
            Texture2D texture2 = Mod.GetTexture("Items/Boss/Yamata/NaitokurosuA");
            Texture2D texture2Glow = Mod.GetTexture("Glowmasks/NaitokurosuA_Glow");
            if (Main.dayTime)
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
            }
            return false;
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = Mod.GetTexture("Items/Boss/Yamata/Naitokurosu");
            Texture2D texture2 = Mod.GetTexture("Items/Boss/Yamata/NaitokurosuA");
            if (Main.dayTime)
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
            player.blackBelt = true;
            player.dash = 1;
            player.spikedBoots = 2;
            player.GetModPlayer<AAPlayer>().Naitokurosu = true;
            player.buffImmune[Mod.Find<ModBuff>("HydraToxin").Type] = true;
            player.buffImmune[Mod.Find<ModBuff>("Clueless").Type] = true;
            if (player.GetModPlayer<AAPlayer>().ZoneMire)
            {
                player.GetDamage(DamageClass.Ranged) += .18f;
            }
            else
            {
                player.GetDamage(DamageClass.Ranged) += .09f;
            }
            if (Main.dayTime)
            { 
                player.moveSpeed += 2f;
            }
            else
            {
                player.moveSpeed += 3f;
            }
        }
    }
}