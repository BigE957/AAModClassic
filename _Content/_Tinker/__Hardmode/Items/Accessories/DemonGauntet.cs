using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using AAModClassic.Globals;


namespace AAModClassic._Content._Tinker.__Hardmode.Items.Accessories
{

    [AutoloadEquip(EquipType.HandsOn, EquipType.HandsOff)]
    public class DemonGauntlet : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Demon Gauntlet");
            /* Tooltip.SetDefault(
@"Enemies are more likely to target you
14% Increased Melee Damage
10% Increased melee speed
Increased Melee Knockback
Melee Attacks Inflict a different debuff depending on your world evil
Inflicts Ichor in Crimson Worlds/Cursed Flame in Corruption worlds"); */
            
        }

        public override void SetDefaults()
        {
            Item.width = 45;
            Item.height = 48;
            Item.value = Item.sellPrice(0, 12, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.accessory = true;
            Item.defense = 8;
            
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = ModContent.Request<Texture2D>("AAModClassic/Glowmasks/" + GetType().Name + "_Glow").Value;
            Color GlowColor = AAColor.CursedInferno;
            if (WorldGen.crimson)
            {
                GlowColor = AAColor.Ichor;
            }
            spriteBatch.Draw
                (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                GlowColor,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
                );
        }

        public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Texture2D texture = ModContent.Request<Texture2D>("AAModClassic/Glowmasks/" + GetType().Name + "_Glow").Value;
            Texture2D texture2 = TextureAssets.Item[Item.type].Value;
            Color GlowColor = AAColor.CursedInferno;
            if (WorldGen.crimson)
            {
                GlowColor = AAColor.Ichor;
            }
            spriteBatch.Draw(texture2, position, null, drawColor, 0, origin, scale, SpriteEffects.None, 0f);
            for (int i = 0; i < 4; i++)
            {
                //Vector2 offsetPositon = Vector2.UnitY.RotatedBy(MathHelper.PiOver2 * i) * 2;
                spriteBatch.Draw(texture, position, null, GlowColor, 0, origin, scale, SpriteEffects.None, 0f);

            }

            return false;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Melee) += 0.14f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
            player.aggro += 5;
            player.GetModPlayer<AAPlayer>().demonGauntlet = true;
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.FireGauntlet, 1);
                recipe.AddIngredient(ItemID.FleshKnuckles, 1);
                recipe.AddIngredient(ItemID.SoulofNight, 10);
                recipe.AddIngredient(ItemID.Ichor, 10);
                recipe.AddTile(TileID.TinkerersWorkbench);
                recipe.Register();
            }
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.FireGauntlet, 1);
                recipe.AddIngredient(ItemID.PutridScent, 1);
                recipe.AddIngredient(ItemID.SoulofNight, 10);
                recipe.AddIngredient(ItemID.CursedFlame, 10);
                recipe.AddTile(TileID.TinkerersWorkbench);
                recipe.Register();
            }
        }

    }
}