using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using AAModClassic.Base.BaseMod.Base;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu.Weapons
{
	public class GalacticStormspike : ModItem
	{
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
            // DisplayName.SetDefault("Galactic Stormspike");
            BaseUtility.AddTooltips(Item, new string[] { "Shoots a branching ray of dark electricity" });
        }
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 25;
            Item.maxStack = 1;
            Item.rare = ItemRarityID.Yellow;
            Item.value = BaseUtility.CalcValue(0, 35, 55, 20);

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 15;
            Item.useTime = 15;
            //TODOSOC
            //Item.UseSound = new LegacySoundStyle(2, 15, SoundType.Sound);
            Item.damage = 190;
            Item.knockBack = 4;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 10;
            Item.autoReuse = true;
            Item.noMelee = true;	
            Item.shoot = ModContent.ProjectileType<Projectiles.GalacticStormspike_Stormray>();
            Item.shootSpeed = 4;	
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			int pID = Projectile.NewProjectile(Item.GetSource_FromThis(), position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
			return false;
		}
	}
}