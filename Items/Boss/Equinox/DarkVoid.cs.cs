using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Equinox
{
    [AutoloadEquip(EquipType.HandsOn)]
    public class DarkVoid : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = 11;
            Item.accessory = true;
            Item.expert = true; Item.expertOnly = true;
        }

        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dark Void");
            /* Tooltip.SetDefault(
@"Gives immensely increased stats at night
'Dark and spooky'"); */
        }


        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
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

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Main.dayTime)
            {
                player.lifeRegen += 5;
                player.statDefense += 8;
                player.GetAttackSpeed(DamageClass.Melee) += 0.10f;
                player.GetCritChance(DamageClass.Melee) += 4;
                player.GetCritChance(DamageClass.Ranged) += 4;
                player.GetCritChance(DamageClass.Magic) += 4;
                player.pickSpeed -= 0.30f;
                player.GetKnockback(DamageClass.Summon).Base += 0.7f;
                player.GetDamage(DamageClass.Generic) += 0.17f;
                player.GetCritChance(DamageClass.Throwing) += 4;
            }
            player.nightVision = true;
        }

    }
}