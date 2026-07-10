using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Misc.__Hardmode.Items.Weapons
{
    public class Tizona : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Melee";
		public override void SetDefaults()
		{
			Item.damage = 66;           //The damage of your weapon
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;          //Is your weapon a melee weapon?
			Item.width = 48;            //Weapon's texture's width
			Item.height = 48;           //Weapon's texture's height
			Item.useTime = 26;          //The time span of using the weapon. Remember in terraria, 60 frames is a second.
			Item.useAnimation = 26;         //The time span of the using animation of the weapon, suggest set it the same as useTime.
			Item.useStyle = ItemUseStyleID.Swing;          //The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			Item.knockBack = 4;         //The force of knockback of the weapon. Maximum is 20
			Item.value = Item.sellPrice(gold: 1);           //The value of the weapon
			Item.rare = ItemRarityID.Green;              //The rarity of the weapon, from -1 to 13
			Item.UseSound = SoundID.Item1;      //The sound when the weapon is using
			Item.autoReuse = true;          //Whether the weapon can use automatically by pressing mousebutton
		}

		public override void AddRecipes()
		{
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.Excalibur);
				recipe.AddRecipeGroup("AAModClassic:AdamantiteBar", 15);
                recipe.AddIngredient(ItemID.SoulofSight, 5);
                recipe.AddIngredient(ItemID.SoulofMight, 5);
                recipe.AddIngredient(ItemID.SoulofFright, 5);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }
		}

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
		{
			// Add Onfire buff to the NPC for 1 second
			// 60 frames = 1 second
			target.AddBuff(BuffID.Bleeding, 300);
		}

		// Star Wrath/Starfury style weapon. Spawn projectiles from sky that aim towards mouse.
		// See Source code for Star Wrath projectile to see how it passes through tiles.
		/*	The following changes to SetDefaults 
		 	item.shoot = 503;
			item.shootSpeed = 8f;
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockback)
		{
			Vector2 target = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
			float ceilingLimit = target.Y;
			if (ceilingLimit > player.Center.Y - 200f)
			{
				ceilingLimit = player.Center.Y - 200f;
			}
			for (int i = 0; i < 3; i++)
			{
				position = player.Center + new Vector2((-(float)Main.rand.Next(0, 401) * player.direction), -600f);
				position.Y -= (100 * i);
				Vector2 heading = target - position;
				if (heading.Y < 0f)
				{
					heading.Y *= -1f;
				}
				if (heading.Y < 20f)
				{
					heading.Y = 20f;
				}
				heading.Normalize();
				heading *= velocity.Length();
				speedX = heading.X;
				speedY = heading.Y + Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(position, velocity, type, damage * 2, knockback, player.whoAmI, 0f, ceilingLimit);
			}
			return false;
		}*/
	}
}
