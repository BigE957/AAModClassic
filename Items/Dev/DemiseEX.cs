using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic;

namespace AAModClassic.Items.Dev
{
    public class DemiseEX : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Catastrophe");
			/* Tooltip.SetDefault(@"An almighty greatblade that was once wielded by the demon lord
Left Click to unleash destructive demonic energy
Right Click to unleash catastrophic blades that fall from the sky
True Melee Strikes have a chance to instantly devour an enemy's soul
Demise EX"); */
		}
		public override void SetDefaults()
		{
			Item.damage = 350;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 58;
			Item.height = 58;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 5;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("DemiseSphereEX").Type;
            Item.shootSpeed = 13f;
            Item.expert = true;
            Item.expertOnly = true;
		}

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.staff[Item.type] = false;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.noMelee = false;
                Item.shoot = Mod.Find<ModProjectile>("DemiseBladeEX").Type;
                Item.shootSpeed = 15f;
            }
            else
            {
                Item.staff[Item.type] = true;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.noMelee = true;
                Item.shoot = Mod.Find<ModProjectile>("DemiseSphereEX").Type;
                Item.shootSpeed = 13f;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                Vector2 vector12 = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
                float num75 = Item.shootSpeed;
                for (int num120 = 0; num120 < 3; num120++)
                {
                    Vector2 vector2 = player.Center + new Vector2(-(float)Main.rand.Next(0, 401) * player.direction, -600f);
                    vector2.Y -= 100 * num120;
                    Vector2 vector13 = vector12 - vector2;
                    if (vector13.Y < 0f)
                    {
                        vector13.Y *= -1f;
                    }
                    if (vector13.Y < 20f)
                    {
                        vector13.Y = 20f;
                    }
                    vector13.Normalize();
                    vector13 *= num75;
                    float num82 = vector13.X;
                    float num83 = vector13.Y;
                    float speedX5 = num82;
                    float speedY6 = num83 + Main.rand.Next(-40, 41) * 0.02f;
                    Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), vector2.X, vector2.Y, speedX5, speedY6, Mod.Find<ModProjectile>("DemiseBladeEX").Type, damage * 3 / 2, knockback, Main.myPlayer);
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(15));
                    int p = Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, Mod.Find<ModProjectile>("DemiseSphereEX").Type, damage, knockback, player.whoAmI);
                    Main.projectile[p].Center = player.Center;
                }
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "Demise");
            recipe.AddIngredient(null, "EXSoul");
            recipe.Register();
        }
    }
}
