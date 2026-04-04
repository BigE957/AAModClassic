using AAModClassic;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Dev
{
    public class DuckstepGunEX : BaseAAItem
    {
        
        public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Duckstep R.E.M.I.X.");
            // Tooltip.SetDefault(@"Duckstep Launcher EX");
        }

		public override void SetDefaults()
		{
            
			Item.damage = 320;
			Item.DamageType = DamageClass.Magic;
            Item.mana = 10;
            Item.width = 80;
			Item.height = 42;
			Item.useTime = 10;
			Item.useAnimation = 10;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4;
			Item.value = 3000000;
            Item.expert = true; Item.expertOnly = true;
			Item.UseSound = new Terraria.Audio.SoundStyle("AAModClassic/Sounds/Sounds/QUAK");
            Item.autoReuse = true;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 15f;
            Item.shoot = ModContent.ProjectileType<Duck>();
            Item.rare = ItemRarityID.Red;
            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow";
            glowmaskDrawType = GLOWMASKTYPE_GUN;
            glowmaskDrawColor = Color.White;  
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = new Color(158, 255, 61);
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DuckstepGun");
            recipe.AddIngredient(null, "EXSoul");
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float numberProjectiles = 3 + Main.rand.Next(3);
			float rotation = MathHelper.ToRadians(45);
			position += Vector2.Normalize(velocity) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
				Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI);
			}
			return false;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(10, 0);
		}
	}
}
