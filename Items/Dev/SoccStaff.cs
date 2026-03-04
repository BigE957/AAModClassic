using AAModClassic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Dev
{
    public class SoccStaff : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Socc on a Stick");
            /* Tooltip.SetDefault(@"Summons a cotton god to fight for you
Only one Socc may exist. 
Any summons after one has been summoned will result in a regular Sock
Sock Puppet Staff EX"); */
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 14f;
            Item.shoot = Mod.Find<ModProjectile>("SoccMinion").Type;
            Item.damage = 240;
            Item.width = 60;
            Item.height = 56;
            Item.UseSound = SoundID.Item44;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.knockBack = 5f;
            Item.rare = ItemRarityID.Yellow;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 20;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int i = Main.myPlayer;
            float num74 = knockback;
            num74 = player.GetWeaponKnockback(Item, num74);
            player.itemTime = Item.useTime;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            vector2.X = Main.mouseX + Main.screenPosition.X;
            vector2.Y = Main.mouseY + Main.screenPosition.Y;
            if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("SoccMinion").Type] > 0)
            {
                Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), vector2.X, vector2.Y, 0, 0, Mod.Find<ModProjectile>("SockPuppetEX").Type, damage, num74, i, 0f, 0f);
            }
            else
            {
                Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), vector2.X, vector2.Y, 0, 0, Mod.Find<ModProjectile>("SoccMinion").Type, (int)(damage * 1.5f), num74, i, 0f, 0f);
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "SockStaff", 1);
            recipe.AddIngredient(null, "EXSoul", 1);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }
    }
}