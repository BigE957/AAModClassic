using AAModClassic;
using AAModClassic.Items.Ranged.Ammo;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Ranged
{
    public class Alien_Rifle : BaseAAItem
	{
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Alien Rifle");
			// Tooltip.SetDefault("Uses energy cells as ammo");
		}

		public override void SetDefaults()
		{
			Item.damage = 94;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 48;
			Item.height = 18;
			Item.useAnimation = 9;
			Item.useTime = 9;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 2;
			Item.value = Item.sellPrice(0, 10, 0, 0);
			Item.rare = ItemRarityID.Yellow;
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
