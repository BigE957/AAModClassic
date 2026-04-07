using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using AAModClassic.Projectiles;
using AAModClassic.Items.Boss;

namespace AAModClassic.Items.Dev
{
    public class FuryForgerEX : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Fury Greatforger");
			/* Tooltip.SetDefault(@"Striking enemies causes an explosion + sparks to fly from them
Fury Forger EX"); */
		}
		public override void SetDefaults()
		{
			Item.damage = 2500;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 82;
			Item.height = 88;
			Item.useTime = 35;
			Item.useAnimation = 35;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 4;
            Item.value = Item.sellPrice(0, 50, 0, 0);
            Item.rare = ItemRarityID.Cyan;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.expert = true; Item.expertOnly = true;
		}
		
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            SoundEngine.PlaySound(Mod.GetLegacySoundSlot(SoundType.Sound, "Sounds/Sounds/Forge"), player.Center);
            float spread = 45f * 0.0174f;
            double startAngle = Math.Atan2(player.velocity.X, player.velocity.Y) - spread / 2;
            double deltaAngle = spread / 12f;
            if (player.whoAmI == Main.myPlayer)
            {
                for (int i = 0; i < 6; i++)
                {
                    double offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                    Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), target.Center.X, target.Center.Y, (float)(Math.Sin(offsetAngle) * 5f), (float)(Math.Cos(offsetAngle) * 5f), ModContent.ProjectileType<SparkFury>(), Item.damage, 1.25f, player.whoAmI, 0f, 1f);
                    Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), target.Center.X, target.Center.Y, (float)(-Math.Sin(offsetAngle) * 5f), (float)(-Math.Cos(offsetAngle) * 5f), ModContent.ProjectileType<SparkFury>(), Item.damage, 1.25f, player.whoAmI, 0f, 1f);
                }
            }
            target.AddBuff(BuffID.Daybreak, 200);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<FuryForger>());
            recipe.AddIngredient(ModContent.ItemType<EXSoul>());
            recipe.Register();
        }
    }
}
