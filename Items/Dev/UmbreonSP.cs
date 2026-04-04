using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic;

namespace AAModClassic.Items.Dev
{
    public class UmbreonSP : BaseAAItem
	{
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Blade of Night");
			// Tooltip.SetDefault("A dark sword from a dark creature.");
		}
		
		public override void SetDefaults()
		{
			Item.damage = 200;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 84;
			Item.height = 84;
			Item.useTime = 26;
			Item.useAnimation = 26;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5;
			Item.value = Item.sellPrice(0, 1, 50, 0);
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item71;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<UmbreonSPProjectile>();
			Item.shootSpeed = 20f;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }
    }
}
