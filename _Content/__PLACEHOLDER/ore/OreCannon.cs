using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic.Globals;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic._Content.Hoard._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Hoard.__Hardmode.Items._BossGreed.Weapons;
using AAModClassic._Content.__PLACEHOLDER.ore.projs;
using AAModClassic._Content.Hallow.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;


namespace AAModClassic._Content.__PLACEHOLDER.ore
{
    public class OreCannon : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Ore Cannon");
            /* Tooltip.SetDefault(@"Uses Some Ores as Ammunition
Certain ores have special effects when shot"); */
        }

        public override void SetDefaults()
        {

            Item.damage = 300;
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

        readonly int[] Ores = new int[]
        {
            ItemID.CopperOre,
            ItemID.TinOre,
            ItemID.IronOre,
            ItemID.LeadOre,
            ItemID.SilverOre,
            ItemID.TungstenOre,
            ItemID.GoldOre,
            ItemID.PlatinumOre,
            ItemID.Meteorite,
            ItemID.DemoniteOre,
            ItemID.CrimtaneOre,
            ModContent.ItemType<AbyssiumOre>(),
            ModContent.ItemType<IncineriteOre>(),
            ItemID.Hellstone,
            ItemID.CobaltOre,
            ItemID.PalladiumOre,
            ItemID.MythrilOre,
            ItemID.OrichalcumOre,
            ItemID.AdamantiteOre,
            ItemID.TitaniumOre,
            ModContent.ItemType<HallowedOre>(),
            ItemID.ChlorophyteOre,
            ItemID.LunarOre,
            ModContent.ItemType<DarkmatterOre>(),
            ModContent.ItemType<RadiumOre>(),
            ModContent.ItemType<DaybreakIncineriteOre>(),
            ModContent.ItemType<EventideAbyssiumOre>(),
            ModContent.ItemType<ApocalyptiteOre>(),
        };
        public int projType = -1;

        public override bool CanUseItem(Player player)
        {
            int itemIndex = -1;
            if (player.itemAnimation == 0)
            {
                if (BasePlayer.HasItem(player, Ores, ref itemIndex, default, false, false))
                {
 					Item itemFired = player.inventory[itemIndex];
 					BasePlayer.ReduceSlot(player, itemIndex, 1);
                    if (itemFired.type == ItemID.CopperOre) projType = 0;
 					if (itemFired.type == ItemID.TinOre) projType = 1;
 					if (itemFired.type == ItemID.IronOre) projType = 2;
					if (itemFired.type == ItemID.LeadOre) projType = 3;
					if (itemFired.type == ItemID.SilverOre) projType = 4;
					if (itemFired.type == ItemID.TungstenOre) projType = 5;
					if (itemFired.type == ItemID.GoldOre) projType = 6;
					if (itemFired.type == ItemID.PlatinumOre) projType = 7;
					if (itemFired.type == ItemID.Meteorite) projType = 8;
					if (itemFired.type == ItemID.DemoniteOre) projType = 9;
					if (itemFired.type == ItemID.CrimtaneOre) projType = 10;
					if (itemFired.type == ModContent.ItemType<AbyssiumOre>()) projType = 11;
					if (itemFired.type == ModContent.ItemType<IncineriteOre>()) projType = 12;
					if (itemFired.type == ItemID.Hellstone) projType = 13;
					if (itemFired.type == ItemID.CobaltOre) projType = 14;
					if (itemFired.type == ItemID.PalladiumOre) projType = 15;
					if (itemFired.type == ItemID.MythrilOre) projType = 16;
					if (itemFired.type == ItemID.OrichalcumOre) projType = 17;
					if (itemFired.type == ItemID.AdamantiteOre) projType = 18;
					if (itemFired.type == ItemID.TitaniumOre) projType = 19;
					if (itemFired.type == ModContent.ItemType<HallowedOre>()) projType = 20;
					if (itemFired.type == ItemID.ChlorophyteOre) projType = 21;
					if (itemFired.type == ItemID.LunarOre) projType = 22;
                    if (itemFired.type == ModContent.ItemType<DarkmatterOre>()) projType = 23;
                    if (itemFired.type == ModContent.ItemType<RadiumOre>()) projType = 24;
                    if (itemFired.type == ModContent.ItemType<DaybreakIncineriteOre>()) projType = 25;
                    if (itemFired.type == ModContent.ItemType<EventideAbyssiumOre>()) projType = 26;
                    if (itemFired.type == ModContent.ItemType<ApocalyptiteOre>()) projType = 27;
                    return true;
 				}
 			}
            return false;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
 		{
            int p = Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position, velocity, ModContent.ProjectileType<OreChunkHM>(), damage, knockback, player.whoAmI, 0, projType);
            if (Main.projectile[p].ai[1] == 10)
            {
                 Main.projectile[p].knockBack *= 1.5f;
            }
            if (Main.projectile[p].ai[1] == 19)
            {
                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = velocity.RotatedByRandom(MathHelper.ToRadians(20));
                    Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockback, player.whoAmI, 0, Main.projectile[p].ai[1]);
                }
            }
            Main.projectile[p].DamageType = DamageClass.Ranged;
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<GildedGlock>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CovetiteBar>(), 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
