using Microsoft.Xna.Framework.Graphics;
using Terraria;
using System;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using AAModClassic;
using AAModClassic.Globals;

namespace AAModClassic.Items.Armor.Darkmatter
{
    [AutoloadEquip(EquipType.Head)]
	public class DarkmatterMask : BaseAAItem
	{
        
        public override void SetStaticDefaults()
        {
            
            // DisplayName.SetDefault("Darkmatter Mask");
			/* Tooltip.SetDefault(@"10% increased magic damage
15% decreased mana usage
Dark, yet still barely visible"); */

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

        public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 18;
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

        public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Magic) += 0.10f;
            player.manaCost *= .85f;
        }

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<DarkmatterBreastplate>() && legs.type == ModContent.ItemType<DarkmatterGreaves>();
		}

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.DarkmatterMaskBonus1") + (int)(player.GetDamage(DamageClass.Magic)).ApplyTo(100) + " " + Language.GetTextValue("Mods.AAModClassic.Common.DarkmatterMaskBonus2") + player.GetCritChance(DamageClass.Magic) + Language.GetTextValue("Mods.AAModClassic.Common.DarkmatterMaskBonus3");
            player.GetModPlayer<DarkmatterMaskEffects>().setBonus = true;
            player.GetModPlayer<DarkmatterMaskEffects>().sunSiphon = false;
            player.armorEffectDrawShadowLokis = true;
            
            for (int i = 0; i < 15; i++)
            {
                Vector2 offset = new Vector2();
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                offset.X += (float)(Math.Sin(angle) * 300);
                offset.Y += (float)(Math.Cos(angle) * 300);
                Dust dust = Main.dust[Dust.NewDust(player.Center + offset - new Vector2(4, 4), 0, 0,  ModContent.DustType<DarkmatterDust>(), 0, 0, 100, default, 1f)];
                dust.velocity = player.velocity;
                dust.noGravity = true;
            }
        }

		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DarkMatter", 25);
            recipe.AddIngredient(null, "DarkEnergy", 10);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }
	}
    public class DarkmatterMaskEffects : ModPlayer
    {
        public bool setBonus = false;
        public int[] npcCooldown = new int[Main.npc.Length];
        public bool sunSiphon = false;
        public override void ResetEffects()
        {
            setBonus = false;
            
        }
        public override void PreUpdate()
        {
            if(setBonus)
            {
                for (int n = 0; n < Main.npc.Length; n++)
                {
                    if (npcCooldown[n] > 0)
                    {
                        npcCooldown[n]--;
                    }
                    if (Main.npc[n].CanBeChasedBy() && npcCooldown[n] == 0 && (Main.npc[n].Center - Player.Center).Length() < 300)
                    {
                        
                        npcCooldown[n] = 30;
                        int type = ModContent.ProjectileType<DarkLeech>();
                        if (sunSiphon)
                        {
                            type = ModContent.ProjectileType<SunSiphon>();
                        }

                        Projectile.NewProjectile(Main.npc[n].GetSource_FromThis(), Main.npc[n].Center, Vector2.Zero, type, (int)(Player.GetDamage(DamageClass.Magic)).ApplyTo(100f), 0f, Player.whoAmI, n);
                    }
                }
            }
            
        }
        
    }
    
}