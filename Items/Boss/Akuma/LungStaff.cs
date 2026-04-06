using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic;
using AAModClassic.Globals;
using AAModClassic.Projectiles.Akuma.Lung;
using AAModClassic.Buffs;

namespace AAModClassic.Items.Boss.Akuma
{
    public class LungStaff : BaseAAItem
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
            Item.shoot = ModContent.ProjectileType<LungHead>();
            Item.width = 64;
            Item.height = 64;
            Item.UseSound = SoundID.Item44;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.noMelee = true;
            Item.knockBack = 2f;
            Item.buffType = ModContent.BuffType<LungMinion_Buff>();
            Item.DamageType = DamageClass.Summon;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 13;
            Item.value = Item.sellPrice(0, 30, 0, 0);

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity13;
                }
            }
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
            int num74 = Item.shoot;
            int num76 = damage;
            float num77 = Item.knockBack;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num81 = Main.mouseX + Main.screenPosition.X - vector2.X;
            float num82 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
            for (int num186 = 0; num186 < 1000; num186++)
            {
                if (Main.projectile[num186].active && Main.projectile[num186].owner == Main.myPlayer)
                {
                    if (num184 == -1 && Main.projectile[num186].type == ModContent.ProjectileType<LungHead>())
                    {
                        num184 = num186;
                    }
                    if (num185 == -1 && Main.projectile[num186].type == ModContent.ProjectileType<LungTail>())
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
                int num187 = Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), vector2.X, vector2.Y, num81, num82, num74, num76, num77, Main.myPlayer, 0f, 0f);
                num187 = Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), vector2.X, vector2.Y, num81, num82, ModContent.ProjectileType<LungBody>(), num76, num77, Main.myPlayer, num187, 0f);
                int num188 = num187;
				for (int z = 0; z < (int)((player.maxMinions - player.slotsMinions) * 2); z++)
				{
					num187 = Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), vector2.X, vector2.Y, num81, num82, ModContent.ProjectileType<LungBody>(), num76, num77, Main.myPlayer, num187, 0f);
					Main.projectile[num188].localAI[1] = num187;
					num188 = num187;
				}
                num187 = Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), vector2.X, vector2.Y, num81, num82, ModContent.ProjectileType<LungTail>(), num76, num77, Main.myPlayer, num187, 0f);
                Main.projectile[num188].localAI[1] = num187;
            }
            else
            {
                int previous = (int) Main.projectile[num185].ai[0];
                int current = 0;

                current = Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position, velocity, ModContent.ProjectileType<LungBody>(), damage, knockback, player.whoAmI,
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
            recipe.AddIngredient(null, "DaybreakIncinerite", 5);
            recipe.AddIngredient(null, "CrucibleScale", 5);
            recipe.AddIngredient(null, "DragonriderStaff", 1);
            recipe.AddTile(null, "ACS");
            recipe.Register();
        }
    }
}
