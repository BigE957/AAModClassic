using AAModClassic._Content.Chaos.___PreHardmode.Items._BossGripsOfChaos.Weapons;
using AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items.Weapons
{
    public class CyberBaton : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cyber Baton");
            // Tooltip.SetDefault(@"Summons a cyber claw to fight with you");
        }

        public override void SetDefaults()
        {
            Item.useStyle = 1;
            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<CyberBaton_CyberClaw>();
            Item.damage = 40;
            Item.width = 52;
            Item.height = 52;
            Item.UseSound = SoundID.Item44;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.knockBack = 5f;
            Item.rare = 3;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 5;
            Item.noUseGraphic = true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int shootMe = ModContent.ProjectileType<CyberBaton_CyberClaw>();
            player.itemTime = Item.useTime;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            vector2.X = Main.mouseX + Main.screenPosition.X;
            vector2.Y = Main.mouseY + Main.screenPosition.Y;
            Projectile.NewProjectile(player.GetSource_FromThis(), vector2.X, vector2.Y, 0, 0, shootMe, damage, 5, Item.playerIndexTheItemIsReservedFor, 0f, 0f);
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ClawBaton>(), 1);
            recipe.AddIngredient(ModContent.ItemType<FulguriteBar>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}