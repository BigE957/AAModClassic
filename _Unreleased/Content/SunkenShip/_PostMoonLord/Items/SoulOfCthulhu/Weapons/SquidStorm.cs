using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu.Weapons
{
public class SquidStorm : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Squid Storm");
		// Tooltip.SetDefault("Casts tentacles from the R'lyehian depths");
	}

    public override void SetDefaults()
    {
        Item.damage = 300;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 14;
        Item.width = 28;
        Item.crit = 3;
        Item.height = 32;
        Item.useTime = 6;
        Item.useAnimation = 28;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 3.5f;
        Item.value = Item.buyPrice(1, 0, 0, 0);
        Item.rare = ItemRarityID.Pink;
        Item.UseSound = SoundID.Item103;
        Item.autoReuse = true;
        Item.shoot = ModContent.ProjectileType<Projectiles.CthulhuTentacle>();
        Item.shootSpeed = 12f;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	int i = Main.myPlayer;
		int num73 = damage;
		float num74 = knockback;
    	num74 = player.GetWeaponKnockback(Item, num74);
    	player.itemTime = Item.useTime;
    	Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
    	float num78 = Main.mouseX + Main.screenPosition.X - vector2.X;
		float num79 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
    	Vector2 value2 = new Vector2(num78, num79);
		value2.Normalize();
		Vector2 value3 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
		value3.Normalize();
		value2 = value2 * 4f + value3;
		value2.Normalize();
		value2 *= Item.shootSpeed;
		float num91 = Main.rand.Next(10, 80) * 0.001f;
		if (Main.rand.Next(2) == 0)
		{
			num91 *= -1f;
		}
		float num92 = Main.rand.Next(10, 80) * 0.001f;
		if (Main.rand.Next(2) == 0)
		{
			num92 *= -1f;
		}
		Projectile.NewProjectile(Item.GetSource_FromThis(), vector2.X, vector2.Y, value2.X, value2.Y, ModContent.ProjectileType<CthulhuTentacle>(), num73, num74, i, num92, num91);
    	return false;
	}
}}