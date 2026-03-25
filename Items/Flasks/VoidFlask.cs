using AAModClassic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Flasks
{
    public class VoidFlask : BaseAAItem
	{
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 26;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.shoot = Mod.Find<ModProjectile>("BlackSolution").Type;
            Item.shootSpeed = 1f;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(0, 0, 1, 0);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.noUseGraphic = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Void Flask");
            // Tooltip.SetDefault(@"Spreads the Void");
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(1, 4));
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2)
            {
                Item.shoot = Mod.Find<ModProjectile>("VoidFlask").Type;
                Item.shootSpeed = 9f;
            }
            else
            {
                Item.shoot = Mod.Find<ModProjectile>("BlackSolution").Type;
                Item.shootSpeed = 2f;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == Mod.Find<ModProjectile>("VoidFlask").Type)
            {
                Projectile.NewProjectile(source, position, velocity, type, 0, 0, Main.myPlayer, 4);
                return false;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}
