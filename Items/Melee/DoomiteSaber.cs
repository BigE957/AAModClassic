using AAModClassic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee
{
    public class DoomiteSaber : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doomite Saber");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 27;
            Item.useTime = 27;
            Item.width = 46;
            Item.height = 46;
            Item.damage = 32;
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item1;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.rare = ItemRarityID.Orange;
            Item.value = 5400;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.shoot = Mod.Find<ModProjectile>("DoomShot").Type;
            Item.shootSpeed = 8f;
        }

        private static int shoot;

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            shoot++;
            if (shoot % 3 != 0) return false;

            shoot = 0;
            return true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(Mod, "Doomite", 20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}