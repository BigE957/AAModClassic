using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.UI.WorldGen;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Weapons
{
    public class BlackHoleBlaster : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Black Hole Blaster");
            // Tooltip.SetDefault("Occasionally fires off a rocket that explodes into a vortex when it collides with a tile");
        }
        public override void SetDefaults()
        {
            Item.damage = 200;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 80;
            Item.height = 34;
            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true; //so the item's animation doesn't do damage
            Item.knockBack = 2.5f;
            Item.value = 4000000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = new SoundStyle("AAModClassic/Sounds/BHB");
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<BlackHoleBlaster_RedBullet>(); //idk why but all the guns in the vanilla source have this
            Item.shootSpeed = 18f;
            Item.crit = 45;
            Item.useAmmo = AmmoID.Bullet;
        }

        

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-15, -5);
        }

        public int cooldown;
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            cooldown++;
            double rotationA = -0.15;
            for (int i = 0; i < Main.rand.Next(2, 4); i++)
            {
                rotationA += Main.rand.NextFloat(0.02f, 0.1f);
                Vector2 vector = velocity.RotatedBy(rotationA, default);
                float randomArbitraryOffset = 2.8f;
                Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position.X + vector.X * randomArbitraryOffset - 0.2f * vector.Y, position.Y + vector.Y * randomArbitraryOffset + 0.2f * vector.X, vector.X, vector.Y, ModContent.ProjectileType<BlackHoleBlaster_RedBullet>(), damage, knockback, player.whoAmI, 0f, 0f);
            }
            if (cooldown == 10)
            {
                Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position, velocity / 2f, ModContent.ProjectileType<BlackHoleBlaster_Rocket>(), damage, knockback, player.whoAmI, 0f, 0f);
                cooldown = 0;
            }
            if (Main.rand.Next(1, 25) == 1)
                Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position, velocity / 2f, ModContent.ProjectileType<BlackHoleBlaster_BlackRocket>(), damage, knockback, player.whoAmI, 0f, 0f);

            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ItemID.VortexBeater, 1);
            recipe.AddIngredient(ModContent.ItemType<ApocalyptitePlate>(), 5);
            recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 5);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}