using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic._Content.Inferno.__Hardmode.Items.Weapons;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;

namespace AAModClassic._Content.Inferno._PostMoonlord.Items._BossAkuma.Weapons
{
    public class AncientLungStaff : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ancient Lung Staff");
            /* Tooltip.SetDefault(
                @"Summons an ancient lung to fight for you"); */
        }

        public override void SetDefaults()
        {
            Item.mana = 20;
            Item.damage = 100;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 10f;
            Item.shoot = ModContent.ProjectileType<AncientLungStaff_LungHead>();
            Item.width = 64;
            Item.height = 64;
            Item.UseSound = SoundID.Item44;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.noMelee = true;
            Item.knockBack = 2f;
            Item.buffType = ModContent.BuffType<LungMinion_Buff>();
            Item.DamageType = DamageClass.Summon;
            Item.rare = ModContent.RarityType<AncientsRarity>();
            Item.value = Item.sellPrice(0, 30, 0, 0);
        }

        
		
		public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim(true);
            }
            return base.UseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			if (player.altFunctionUse == 2)
            {
                return false;
            }

            if (player.maxMinions - player.slotsMinions < 0.5) return false;
			
			player.AddBuff(ModContent.BuffType<LungMinion_Buff>(), 2, true);

            int num184 = -1;
            int num185 = -1;
            int projType = Item.shoot;
            float num77 = Item.knockBack;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num81 = Main.mouseX + Main.screenPosition.X - vector2.X;
            float num82 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
            for (int num186 = 0; num186 < 1000; num186++)
            {
                if (Main.projectile[num186].active && Main.projectile[num186].owner == Main.myPlayer)
                {
                    if (num184 == -1 && Main.projectile[num186].type == ModContent.ProjectileType<AncientLungStaff_LungHead>())
                    {
                        num184 = num186;
                    }
                    if (num185 == -1 && Main.projectile[num186].type == ModContent.ProjectileType<AncientLungStaff_LungTail>())
                    {
                        num185 = num186;
                    }
                    if (num184 != -1 && num185 != -1)
                    {
                        break;
                    }
                }
            }


            if (num184 == -1 && num185 == -1)
            {
                num81 = 0f;
                num82 = 0f;
                vector2.X = Main.mouseX + Main.screenPosition.X;
                vector2.Y = Main.mouseY + Main.screenPosition.Y;
                int num187 = Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), vector2.X, vector2.Y, num81, num82, projType, damage, num77, Main.myPlayer, 0f, 0f);
                num187 = Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), vector2.X, vector2.Y, num81, num82, ModContent.ProjectileType<AncientLungStaff_LungBody>(), damage, num77, Main.myPlayer, num187, 0f);
                int num188 = num187;
				for (int z = 0; z < (int)((player.maxMinions - player.slotsMinions) * 2); z++)
				{
					num187 = Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), vector2.X, vector2.Y, num81, num82, ModContent.ProjectileType<AncientLungStaff_LungBody>(), damage, num77, Main.myPlayer, num187, 0f);
					Main.projectile[num188].localAI[1] = num187;
					num188 = num187;
				}
                num187 = Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), vector2.X, vector2.Y, num81, num82, ModContent.ProjectileType<AncientLungStaff_LungTail>(), damage, num77, Main.myPlayer, num187, 0f);
                Main.projectile[num188].localAI[1] = num187;
            }
            else
            {
                int previous = (int) Main.projectile[num185].ai[0];
                int current = 0;

                current = Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position, velocity, ModContent.ProjectileType<AncientLungStaff_LungBody>(), damage, knockback, player.whoAmI,
                Projectile.GetByUUID(Main.myPlayer, previous), 0f);

                previous = current;

                Main.projectile[current].localAI[1] = num185;
                
                Main.projectile[num185].ai[0] = current;
                Main.projectile[num185].netUpdate = true;
                Main.projectile[num185].ai[1] = 1f;
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<DaybreakIncineriteBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<CrucibleScale>(), 5);
            recipe.AddIngredient(ModContent.ItemType<DragonsPike>(), 1);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}
