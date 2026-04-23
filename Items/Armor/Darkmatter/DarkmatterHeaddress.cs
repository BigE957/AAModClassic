using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System;
using Terraria.Localization;
using Terraria.ID;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;

namespace AAModClassic.Items.Armor.Darkmatter
{
    [AutoloadEquip(EquipType.Head)]
	public class DarkmatterHeaddress : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Darkmatter Headress");
			/* Tooltip.SetDefault(@"25% increased minion damage
Dark, yet still barely visible"); */

		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 24;
			Item.value = 300000;
			Item.defense = 20;
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

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = Mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Summon) += 0.25f;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<DarkmatterBreastplate>() && legs.type == ModContent.ItemType<DarkmatterGreaves>();
		}

		public override void UpdateArmorSet(Player player)
		{


            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.DarkmatterHeaddressBonus");
            player.GetModPlayer<HeadressEffects>().setBonus = true;
            player.armorEffectDrawShadowLokis = true;
        }

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DarkmatterBar>(), 25);
            recipe.AddIngredient(ModContent.ItemType<DarkEnergy>(), 10);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
	}
    public class HeadressEffects : ModPlayer
    {
        public bool setBonus = false;
        public override void ResetEffects()
        {
            setBonus = false;

        }
    }
    public class DarkMinions : GlobalProjectile
    {
        //power settings
        const int cooldownRate = 120;
        const float radius = 300;
        const int damageReductionPerBlast = 30;
        //

        int cooldown = 0;
        public int reduceDamage = 0;
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public override bool PreAI(Projectile projectile)
        {
            
            if(cooldown>0)
            {
                cooldown--;
            }
            if(projectile.minion && projectile.minionSlots >0 && projectile.active && Main.player[projectile.owner].GetModPlayer<HeadressEffects>().setBonus && cooldown == 0)
            {
                
                for(int p =0; p < Main.projectile.Length; p++)
                {
                    if((Main.projectile[p].Center-projectile.Center).Length() < radius - 100 && Main.projectile[p].active && Main.projectile[p].hostile && Main.projectile[p].damage>0)
                    {
                        DarkBlast(projectile);
                        break;
                    }
                }
            }
            if(projectile.damage >0 && projectile.hostile && reduceDamage > EstimatedDamage(projectile))
            {
                projectile.Kill();
            }
            return base.PreAI(projectile);
        }
        void DarkBlast(Projectile projectile)
        {
            for (int i = 0; i < 100; i++)
            {
                float theta = Main.rand.NextFloat(-(float)Math.PI, (float)Math.PI);
                Dust dust = Dust.NewDustPerfect(projectile.Center, ModContent.DustType<Dusts.DarkmatterDust>(), PolarVector(radius / 30, theta));
                dust.noGravity = true;
            }
            cooldown = (int)(cooldownRate / projectile.minionSlots);
            for (int p = 0; p < Main.projectile.Length; p++)
            {
                if((Main.projectile[p].Center - projectile.Center).Length() < radius && Main.projectile[p].active && Main.projectile[p].hostile && Main.projectile[p].damage > 0)
                {
                    Main.projectile[p].GetGlobalProjectile<DarkMinions>().reduceDamage += damageReductionPerBlast;
                }
            }
        }
        static int EstimatedDamage(Projectile projectile)
        {
            return projectile.damage * (Main.expertMode ? 4 : 2);
        }
        public override Color? GetAlpha(Projectile projectile, Color lightColor)
        {
            if(projectile.GetGlobalProjectile<DarkMinions>().reduceDamage >0)
            {
                float v = projectile.GetGlobalProjectile<DarkMinions>().reduceDamage / (float)EstimatedDamage(projectile);

                lightColor.R = (byte)(lightColor.R * (1f - (lightColor.R * v * .8f)));
                lightColor.G = (byte)(lightColor.G * (1f - (lightColor.R * v * .8f)));
                lightColor.B = (byte)(lightColor.B * (1f - (lightColor.R * v * .8f)));

                return lightColor;
            }
            return null;
        }
        public override void ModifyHitPlayer(Projectile projectile, Player target, ref Player.HurtModifiers modifiers)
        {
            modifiers.FinalDamage.Flat -= (int)(reduceDamage * (Main.expertMode ? .25f : .5f));
        }
        public static Vector2 PolarVector(float radius, float theta)
        {
            return new Vector2((float)Math.Cos(theta), (float)Math.Sin(theta)) * radius;
        }


    }
}