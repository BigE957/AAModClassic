using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Misc.__Hardmode.Items.Weapons
{
    public class ApollosWrath : BaseAAItem
    {

        public override void SetDefaults()
        {
            Item.damage = 78;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 24;
            Item.height = 52;
            Item.useTime = 14;
            Item.useAnimation = 14;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ProjectileID.ShadowBeamFriendly;
            Item.knockBack = 2;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shootSpeed = 4f;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Apollo's Wrath");
            /* Tooltip.SetDefault(@"Shoots Shadow beams
Doesn't use Ammo"); */
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile p = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
            p.DamageType = DamageClass.Ranged;
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PulseBow, 1);
            recipe.AddIngredient(ItemID.SoulofMight, 20);
			recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 0);
        }
    }
}
