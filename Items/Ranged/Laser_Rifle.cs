using AAModClassic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged
{
    public class Laser_Rifle : BaseAAItem
	{
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Laser Carbine");
			// Tooltip.SetDefault("Uses energy cells as ammo");
		}

		public override void SetDefaults()
		{
			Item.damage = 60;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 46;
			Item.height = 22;
			Item.useAnimation = 14;
			Item.useTime = 14;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 2;
			Item.value = Item.sellPrice(0, 4, 72, 0);
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item12;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 22f;
			Item.useAmmo = ModContent.ItemType<Energy_Cell>();			
			Item.crit = 5;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_GUN; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-4, 2);
		}
	}
}
