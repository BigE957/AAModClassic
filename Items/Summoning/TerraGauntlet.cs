using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Summoning
{
    public class TerraGauntlet : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.damage = 80;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Summon;
            Item.width = 18;
            Item.height = 42;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = Mod.Find<ModProjectile>("Minion1").Type;
            Item.buffType = Mod.Find<ModBuff>("TerraSummon").Type;
            Item.knockBack = 2;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item44;
            Item.autoReuse = false;
            Item.shootSpeed = 1f;
            Item.mana = 10;
        }
		
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Gauntlet");
            // Tooltip.SetDefault(@"Summons a Terra Squid, Terra Sphere, Terra Crawler, or Terra Weaver to Fight for you");
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim();
            }
            return base.UseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                return false;
            }
			
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			modPlayer.TerraSummon = true;
			player.AddBuff(Mod.Find<ModBuff>("TerraSummon").Type, 2, true);

			Vector2 point = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);

            int shootMe = Main.rand.Next(3);
            switch (shootMe)
            {
                case 0:
                    shootMe = Mod.Find<ModProjectile>("Minion1").Type;
                    break;
                case 1:
                    shootMe = Mod.Find<ModProjectile>("Minion2").Type;
                    break;
                case 2:
                    shootMe = Mod.Find<ModProjectile>("Minion3").Type;
                    break;
            }

            int i = Main.myPlayer;
            int num73 = damage;
            float num74 = knockBack;
            num74 = player.GetWeaponKnockback(Item, num74);
            player.itemTime = Item.useTime;
            int num78 = 0;
            int num79 = 0;
            Projectile.NewProjectile(point.X + Main.rand.Next(-50, 50), point.Y + Main.rand.Next(-50, 50), num78, num79, shootMe, num73, num74, i, 0f, 0f);

            return false;
        }
    }
}
