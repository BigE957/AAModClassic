using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons
{
    public class FuryFlame : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Fury Flame");
            // Tooltip.SetDefault("Allows you to blast explosive flames at your foes");
        }

        public override void SetDefaults()
        {
            Item.damage = 140;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.width = 64;
            Item.height = 46;
            Item.useTime = 2;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<FuryFlame_FuryFire>();
            Item.mana = 4;
            Item.knockBack = 0;
            Item.value = Item.sellPrice(0, 25, 0, 0);
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shootSpeed = 7f;
            Item.noUseGraphic = true;
        }

        

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position, velocity, type, damage, knockback, Main.myPlayer, 7f);
            return false;
        }
    }
}
