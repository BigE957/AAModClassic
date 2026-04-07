using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using AAModClassic.Globals;
using AAModClassic.Projectiles;
using AAModClassic.Tiles.Crafters;
using AAModClassic.Items.Materials;

namespace AAModClassic.Items.Armor.Radium
{
    [AutoloadEquip(EquipType.Head)]
	public class RadiumHat : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Radium Hat");
			/* Tooltip.SetDefault(@"35% increased minion damage
Shines with the light of a starry night sky"); */
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 24;
			Item.value = 300000;
			Item.defense = 18;
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

        public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Summon) += 0.35f;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<RadiumPlatemail>() && legs.type == ModContent.ItemType<RadiumCuisses>();
        }

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.RadiumHatBonus1") + (int)(player.GetDamage(DamageClass.Summon)).ApplyTo(RadMinions.baseBlastDamage) + " " + Language.GetTextValue("Mods.AAModClassic.Common.RadiumHatBonus2");
            player.GetModPlayer<HatEffects>().setBonus = true;
        }

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RadiumBar>(), 25);
            recipe.AddIngredient(ModContent.ItemType<Stardust>(), 10);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
	}
    public class HatEffects : ModPlayer
    {
        public bool setBonus = false;
        public override void ResetEffects()
        {
            setBonus = false;

        }
    }
    public class RadMinions : GlobalProjectile
    {
        //power settings
        const int cooldownRate = 120;
        const float radius = 300;
        public const int baseBlastDamage = 200;
        //

        int cooldown = 0;
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public override bool PreAI(Projectile projectile)
        {

            if (cooldown > 0)
            {
                cooldown--;
            }
            if (projectile.minion && projectile.minionSlots > 0 && projectile.active && Main.player[projectile.owner].GetModPlayer<HatEffects>().setBonus && cooldown == 0)
            {

                for (int n = 0; n < Main.npc.Length; n++)
                {
                    if ((Main.npc[n].Center - projectile.Center).Length() < radius - 100 && Main.npc[n].CanBeChasedBy())
                    {
                        SunBlast(projectile);
                        break;
                    }
                }
            }
            
            return base.PreAI(projectile);
        }
        void SunBlast(Projectile projectile)
        {
            for (int i = 0; i < 100; i++)
            {
                float theta = Main.rand.NextFloat(-(float)Math.PI, (float)Math.PI);
                Dust dust = Dust.NewDustPerfect(projectile.Center, ModContent.DustType<Dusts.RadiumDust>(), PolarVector(radius / 30, theta));
                dust.noGravity = true;
            }
            cooldown = (int)(cooldownRate / projectile.minionSlots);
            Projectile.NewProjectile(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<RadiumSetbonusBlast>(), (int)(baseBlastDamage * Main.player[projectile.owner].GetDamage(DamageClass.Summon)).Flat, 0f, projectile.owner, radius);
            
        }
        public static Vector2 PolarVector(float radius, float theta)
        {
            return new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta)) * radius;
        }


    }
}