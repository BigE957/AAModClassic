using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Weapons
{
    public class Mossket : BaseAAItem
    {

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.width = 24;
            Item.height = 28;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.useAmmo = AmmoID.Bullet;
            Item.UseSound = SoundID.Item11;
            Item.damage = 15;
            Item.shootSpeed = 12f;
            Item.noMelee = true;
            Item.knockBack = .5f;
            Item.value = 50000;
            Item.scale = 1f;
            Item.rare = ItemRarityID.Blue;
            Item.DamageType = DamageClass.Ranged;
        }
		
		public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12, 0);
        }
    }
}
