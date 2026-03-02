using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Projectiles.Anubis;

namespace AAMod.Items.Magic
{
    public class AnubisBlockBook : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.damage = 130;                        
            Item.DamageType = DamageClass.Magic;                     
            Item.width = 24;
            Item.height = 28;
            Item.useTime = 90;
            Item.useAnimation = 90;
            Item.useStyle = 5;        
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.knockBack = 8;
            Item.mana = 20;             
            Item.UseSound = SoundID.Item21;            
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<BlockA>();  
            Item.shootSpeed = 11f;
            Item.rare = 8;
        }   

        public override void SetStaticDefaults()
        {
          /* DisplayName.SetDefault(
@"The Life And Epic Adventures
of Anubis the Wonder Dog
~Special Edition~"); */
          /* Tooltip.SetDefault(@"Left click to summon blocks that crush at your cursor's position Horizontally
Right click for vertical blocks instead"); */
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("BlockA").Type] >= 1 || player.ownedProjectileCounts[Mod.Find<ModProjectile>("BlockA1").Type] >= 1)
            {
                return false;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float X = Main.mouseX + Main.screenPosition.X;

            float Y = Main.mouseY + Main.screenPosition.Y;
            if (player.gravDir == -1f)
            {
                Y = Main.screenPosition.Y + Main.screenHeight - Main.mouseY;
            }

            if (player.altFunctionUse != 2)
            {
                int l = Projectile.NewProjectile(new Vector2(X - 600, Y), Vector2.Zero, ModContent.ProjectileType<BlockA>(), damage, knockBack, Main.myPlayer, 0, 0);
                int r = Projectile.NewProjectile(new Vector2(X + 600, Y), Vector2.Zero, ModContent.ProjectileType<BlockA>(), damage, knockBack, Main.myPlayer, 1, 0);
                Main.projectile[l].ai[1] = r;
                Main.projectile[l].Center = new Vector2(X - 600, Y);
                Main.projectile[r].ai[1] = l;
                Main.projectile[r].Center = new Vector2(X + 600, Y);
            }
            else
            {
                int u = Projectile.NewProjectile(new Vector2(X, Y - 600), Vector2.Zero, ModContent.ProjectileType<BlockA1>(), damage, knockBack, Main.myPlayer, 0, 0);
                int d = Projectile.NewProjectile(new Vector2(X, Y + 600), Vector2.Zero, ModContent.ProjectileType<BlockA1>(), damage, knockBack, Main.myPlayer, 1, 0);
                Main.projectile[u].ai[1] = d;
                Main.projectile[u].Center = new Vector2(X, Y - 600);
                Main.projectile[d].ai[1] = u;
                Main.projectile[d].Center = new Vector2(X, Y + 600);
            }
            return false;
        }
    }
}
