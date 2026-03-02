using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Equinox
{
    public class Equinox : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Equinox");
            /* Tooltip.SetDefault(
@"Turns the holder into a werewolf at night and a merfolk when entering water
Gives immensely increased stats
'True balance'"); */
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
            Item.expert = true; Item.expertOnly = true;
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
            player.lifeRegen += 6;
            player.statDefense += 9;
            player.GetAttackSpeed(DamageClass.Melee) += 0.10f;
            player.GetCritChance(DamageClass.Melee) += 5;
            player.GetCritChance(DamageClass.Ranged) += 5;
            player.GetCritChance(DamageClass.Magic) += 5;
            player.pickSpeed -= 0.35f;
            player.GetKnockback(DamageClass.Summon).Base += 0.75f;
            player.GetDamage(DamageClass.Generic) += 0.17f;
            player.GetCritChance(DamageClass.Throwing) += 5;
            player.nightVision = true;
            player.GetModPlayer<AAPlayer>().RStar = true;
            player.accMerman = true;
            player.wolfAcc = true;
            if (hideVisual)
            {
                player.hideMerman = true;
                player.hideWolf = true;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CelestialShell, 1);
            recipe.AddIngredient(null, "RadiantStar", 1);
            recipe.AddIngredient(null, "DarkVoid", 1);
            recipe.AddIngredient(null, "Stardust", 20);
            recipe.AddIngredient(null, "DarkEnergy", 20);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }
    }
}