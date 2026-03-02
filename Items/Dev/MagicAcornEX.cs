using AAModClassic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Dev
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
            Item.shoot = Mod.Find<ModProjectile>("DapperSquirrel1").Type;
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
			Item.buffType = Mod.Find<ModBuff>("DapperSquirrel").Type;
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
                        shootMe = Mod.Find<ModProjectile>("DapperSquirrel1").Type;
                        break;
                    case 1:
                        shootMe = Mod.Find<ModProjectile>("DapperSquirrel2").Type;
                        break;
                }
            }
            player.itemTime = Item.useTime;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            vector2.X = Main.mouseX + Main.screenPosition.X;
            vector2.Y = Main.mouseY + Main.screenPosition.Y;
            Projectile.NewProjectile(vector2.X, vector2.Y, 0, 0, shootMe, damage, knockBack, Main.myPlayer, 0f, 0f);
            return false;
        }
    }
}