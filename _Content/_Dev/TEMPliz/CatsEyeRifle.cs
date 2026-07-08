using AAModClassic._Content._Dev.TEMPliz.projs;
using AAModClassic._Content.Mire.___PreHardmode.Items.Weapons;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.TEMPliz
{
    public class CatsEyeRifle : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Ranged";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cat's Eye Rifle");
            /* Tooltip.SetDefault(@"Fires Shadow bolts
Doesn't require ammo
'QUICK HIDE THE LOLI STASH'
-Liz"); */

            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<ArchwitchWand>();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }

        public override void SetDefaults()
        {
            Item.damage = 430;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged; 
            Item.width = 72; 
            Item.height = 22;
            Item.useTime = 30; 
            Item.useAnimation = 30; 
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.shoot = ModContent.ProjectileType<CatsEye>();
            Item.knockBack = 12;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.rare = ItemRarityID.Cyan; 
            Item.UseSound = SoundID.Item40;
            Item.autoReuse = false; 
            Item.shootSpeed = 20f;
            Item.crit = 0;
	
			customNameColor = new Color(121, 21, 214); //custom name color				
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }
    }
}