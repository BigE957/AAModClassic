using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.__PLACEHOLDER.ore.projs;
using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.__PLACEHOLDER.ore
{
    public class OreCannonEX : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ultimate Ore Cannon");
            /* Tooltip.SetDefault(@"Uses Any Ore as Ammunition
Certain ores have special effects when shot
Legendary Weapon
OreCannonEX"); */
        }

        public override void SetDefaults()
        {

            Item.damage = 700;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 50;
            Item.height = 20;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 0;
			Item.shoot = ProjectileID.PurificationPowder;
            Item.UseSound = SoundID.Item14;
            Item.shootSpeed = 14f;
            Item.expert = true; 
			Item.expertOnly = true;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity12;
                }
            }
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, -3);
        }

        public int projType = -1;

        public override bool CanUseItem(Player player)
        {
			if (player.itemAnimation == 0)
			{
                bool flag = false;
                int oreindex = -1;
                for (int m = 0; m < 50; m++)
                {
                    Item item = player.inventory[m];
                    
                    if (item != null && (Config.LuckyOre.TryGetValue(item.type, out oreindex) || item.type == ItemID.Hellstone) && item.stack > 0) 
                    {
                        oreindex = m;
                        projType = item.type;
                        flag = true;
                        break;
                    }
                }
				if (flag)
				{
					player.inventory[oreindex].stack -= 1;
                    if (player.inventory[oreindex].stack <= 0)
                    {
                        player.inventory[oreindex].TurnToAir();
                    }
                    return true;
				}
			}
            return false;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
            int p = Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position, velocity, ModContent.ProjectileType<OreChunk>(), damage + Damage(), knockback, player.whoAmI);
            Main.projectile[p].ai[1] = projType;
            if (Main.projectile[p].ai[1] == ItemID.TinOre || Main.projectile[p].ai[1] == ItemID.CopperOre)
            {
                Main.projectile[p].velocity *= .5f;
                if (Main.projectile[p].ai[1] == ItemID.TinOre)
                {
                    Main.projectile[p].knockBack *= 1.3f;
                }
            }
            else if(Main.projectile[p].ai[1] == ItemID.SilverOre)
            {
                Main.projectile[p].penetrate = 2;
            }
            else if (Main.projectile[p].ai[1] == ItemID.CrimtaneOre)
            {
                Main.projectile[p].knockBack *= 1.5f;
            }
            else if (Main.projectile[p].ai[1] == ItemID.Meteorite)
            {
                int num90 = 3;
                if (Main.rand.NextBool(3))
                {
                    num90 ++;
                }
                for (int num91 = 0; num91 < num90; num91++)
                {
                    Vector2 vector2 = new Vector2(player.position.X + player.width * 0.5f + Main.rand.Next(201) * -(float)player.direction + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
                    vector2.X = (vector2.X * 10f + player.Center.X) / 11f + Main.rand.Next(-100, 101);
                    vector2.Y -= 150 * num91;
                    float num82 = Main.mouseX + Main.screenPosition.X - vector2.X;
                    float num83 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
                    if (num83 < 0f)
                    {
                        num83 *= -1f;
                    }
                    if (num83 < 20f)
                    {
                        num83 = 20f;
                    }
                    float num92 = num82 + Main.rand.Next(-40, 41) * 0.03f;
                    float speedY2 = num83 + Main.rand.Next(-40, 41) * 0.03f;
                    num92 *= Main.rand.Next(75, 150) * 0.01f;
                    vector2.X += Main.rand.Next(-50, 51);
                    Vector2 speedfinal = Vector2.Normalize(new Vector2(num92, speedY2)) * velocity.Length();
                    Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), vector2.X, vector2.Y, speedfinal.X, speedfinal.Y, ModContent.ProjectileType<OreChunk>(), damage + Damage(), knockback, player.whoAmI, 0f, ItemID.Meteorite);
                }
            }
            else if (Main.projectile[p].ai[1] == ItemID.CobaltOre)
            {
                Main.projectile[p].velocity *= 1.5f;
            }
            else if (Main.projectile[p].ai[1] == ItemID.PalladiumOre)
            {
                Main.projectile[p].velocity *= 1.3f;
            }
            else if (Main.projectile[p].ai[1] == ItemID.AdamantiteOre)
            {
                Main.projectile[p].scale *= 1.5f;
                Main.projectile[p].width *= 2;
                Main.projectile[p].height *= 2;
                Main.projectile[p].damage = (int)(Main.projectile[p].damage * 1.3);
            }
            else if (Main.projectile[p].ai[1] == ItemID.TitaniumOre)
            {
                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(20));
                    Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<OreChunk>(), damage + (int)(Damage() * 0.8), knockback, player.whoAmI, 0, ItemID.TitaniumOre);
                }
            }
            else if(Main.projectile[p].ai[1] == ItemID.LunarOre)
            {
                Main.projectile[p].velocity *= 2;
            }
            else if(Main.projectile[p].ai[1] == ModContent.ItemType<RadiumOre>())
            {
                Main.projectile[p].damage = (int)(Main.projectile[p].damage / 1.3);
                Main.projectile[p].velocity /= 2;
            }
            return false;
		}

        public int Damage()
        {
            int orevalue = 0;
            if(Config.LuckyOre.TryGetValue(projType, out orevalue))
            {
                return (int)Math.Exp(orevalue * 0.94/100);
            }
            else if(projType == ItemID.Hellstone)
            {
                return (int)Math.Exp(500 * 0.94/100);
            }
            else
            {
                return (int)Math.Exp(100 * 0.94/100);
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<OreCannon>(), 1);
            recipe.AddIngredient(ModContent.ItemType<EXSoul>(), 1);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}
