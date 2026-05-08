using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class MagicAcornEX : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dapper Acorn");
            /* Tooltip.SetDefault(@"Attracts squirrels to fight with you for glory.
'Multiplayer = big meme'
-Fargowilta
Magic Acorn EX"); */
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 14f;
            Item.shoot = ModContent.ProjectileType<MagicAcornEX_DapperSquirrel1>();
            Item.damage = 200;
            Item.width = 20;
            Item.height = 20;
            Item.UseSound = SoundID.Item44;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 1, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 10;
			Item.buffType = ModContent.BuffType<DapperSquirrel_Buff>();
        }
		
		public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 3600, true);
			}
		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int shootMe = Main.rand.Next(2);
            {
                switch (shootMe)
                {
                    case 0:
                        shootMe = ModContent.ProjectileType<MagicAcornEX_DapperSquirrel1>();
                        break;
                    case 1:
                        shootMe = ModContent.ProjectileType<MagicAcornEX_DapperSquirrel2>();
                        break;
                }
            }
            player.itemTime = Item.useTime;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            vector2.X = Main.mouseX + Main.screenPosition.X;
            vector2.Y = Main.mouseY + Main.screenPosition.Y;
            Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), vector2.X, vector2.Y, 0, 0, shootMe, damage, knockback, Main.myPlayer, 0f, 0f);
            return false;
        }
    }
}