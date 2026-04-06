using System;
using System.Collections.Generic;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu.Weapons
{
public class CthulhuCannon : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Cthulhu Cannon");
		// Tooltip.SetDefault("Fires reality-breaking bombs");
	}

    public override void SetDefaults()
    {
        Item.damage = 400;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 98;
        Item.height = 32;
        Item.useTime = 30;
        Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 0f;
        Item.value = 5000000;
        Item.UseSound = SoundID.Item11;
        Item.autoReuse = true;
        Item.shootSpeed = 14f;
        Item.shoot = ModContent.ProjectileType<Projectiles.CthulhuCannon_CthulhuBomb>();
        Item.useAmmo = 771;
    }
    
    public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = AAColor.Cthulhu;
            }
        }
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Projectile.NewProjectile(Item.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}
}}