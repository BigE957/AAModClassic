using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.AH
{
    public class ChaosBaton : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Baton");
            // Tooltip.SetDefault(@"Summons a discordian claw to fight with you");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shootSpeed = 14f;
            Item.shoot = Mod.Find<ModProjectile>("ChaosBaton").Type;
            Item.damage = 100;
            Item.width = 52;
            Item.noMelee = true;
            Item.height = 52;
            Item.UseSound = SoundID.Item44;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.noMelee = true;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.knockBack = 5f;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 12;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 5;
            Item.noUseGraphic = true;
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

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2)
            {
                player.MinionNPCTargetAim(true);
                player.UpdateMinionTarget();
            }
            else
            {
                int shootMe = Main.rand.Next(2);
                {
                    switch (shootMe)
                    {
                        case 0:
                            shootMe = Mod.Find<ModProjectile>("AbyssClaw").Type;
                            break;
                        default:
                            shootMe = Mod.Find<ModProjectile>("BlazeClaw").Type;
                            break;
                    }
                }
                player.itemTime = Item.useTime;
                Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
                vector2.X = Main.mouseX + Main.screenPosition.X;
                vector2.Y = Main.mouseY + Main.screenPosition.Y;
                Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), vector2.X, vector2.Y, 0, 0, shootMe, damage, 5, Item.playerIndexTheItemIsReservedFor, 0f, 0f);
                return true;
            }
            return false;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "ClawBaton", 1);
            recipe.AddIngredient(null, "DaybreakIncinerite", 5);
            recipe.AddIngredient(null, "EventideAbyssium", 5);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }
    }
}