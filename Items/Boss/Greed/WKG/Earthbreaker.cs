using AAModClassic.Buffs;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Greed.WKG
{
    public class Earthbreaker : BaseAAItem
    {
        public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Earthbreaker");
            /* Tooltip.SetDefault(@"Hitting an airborne always crits and sends the target flying into the ground
Concussive force of the hit also has a 50% chance to confuse the struck enemy
If the enemy hits the ground after being hit, they will take damage"); */
        }
		public override void SetDefaults()
		{
			Item.damage = 240;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 80;
			Item.height = 90;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 20;
			Item.UseSound = SoundID.Item1;
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

        public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (Main.rand.Next(2) == 0)
            {
                target.AddBuff(BuffID.Confused, 300);
            }
            if (target.velocity.Y != 0)
            {
                modifiers.SetCrit();
                if (target.knockBackResist > 0 || !target.boss)
                {
                    target.AddBuff(ModContent.BuffType<Falling_Buff>(), 120);
                    target.GetGlobalNPC<FallDamage>().damage = (int)modifiers.FinalDamage.Flat;
                }
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.velocity.Y != 0)
            {
                if (target.knockBackResist > 0 || !target.boss)
                {
                    target.velocity.Y += hit.Knockback * 1.5f * target.knockBackResist;
                    target.velocity.X = 0;
                }
                int num = 4;
                for (int k = 0; k < 10; k++)
                {
                    Dust dust = Main.dust[Dust.NewDust(target.position, target.width, target.height, DustID.Gold)];
                    Dust expr_16B_cp_0 = dust;
                    expr_16B_cp_0.velocity.Y -= 3f + num * 1.5f;
                    Dust expr_18D_cp_0 = dust;
                    expr_18D_cp_0.velocity.Y *= Main.rand.NextFloat();
                    dust.scale += num * 0.03f;
                }
                for (int l = 0; l < 10; l++)
                {
                    Dust dust2 = Main.dust[Dust.NewDust(target.position, target.width, target.height, DustID.Gold)];
                    Dust expr_1EA_cp_0 = dust2;
                    expr_1EA_cp_0.velocity.Y -= 1f + num;
                    Dust expr_206_cp_0 = dust2;
                    expr_206_cp_0.velocity.Y *= Main.rand.NextFloat();
                }

                Vector2 bottom = target.Bottom;
                for (float num3 = 0f; num3 < 10; num3++)
                {
                    Dust dust3 = Dust.NewDustDirect(target.position, target.width, target.height, DustID.Stone, 0f, 0f, 0, default, 1f);
                    dust3.alpha = 0;
                    dust3.position = bottom;
                    Dust expr_336_cp_0 = dust3;
                    expr_336_cp_0.velocity.Y -= 3f;
                    Dust expr_34E_cp_0 = dust3;
                    expr_34E_cp_0.velocity.X *= 0.5f;
                    dust3.fadeIn = 0.5f + Main.rand.NextFloat() * 0.5f;
                }
                for (float num4 = 0f; num4 < 10; num4++)
                {
                    Dust dust4 = Dust.NewDustDirect(target.position, target.width, target.height, DustID.Stone, 0f, 0f, 0, default, 1f);
                    dust4.position = bottom;
                    Dust expr_433_cp_0 = dust4;
                    expr_433_cp_0.velocity.Y -= 5f;
                    Dust expr_44B_cp_0 = dust4;
                    expr_44B_cp_0.velocity.X *= 0.8f;
                    dust4.fadeIn = 0.5f + Main.rand.NextFloat() * 0.5f;
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(ModContent.ItemType<StoneSlammer>(), 1);
            recipe.AddIngredient(ModContent.ItemType<CovetiteBar>(), 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
