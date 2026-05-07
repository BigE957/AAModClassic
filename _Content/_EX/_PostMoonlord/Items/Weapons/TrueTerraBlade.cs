using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic.Items.Boss;
using AAModClassic.Items.Materials;
using AAModClassic.Tiles.Crafters;
using AAModClassic._Content.Terrarium.Buffs;
using AAModClassic.Projectiles;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class TrueTerraBlade : BaseAAItem
	{
		
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("True Terra Blade");//<--- Item name here
			/* Tooltip.SetDefault(@"Shoots homing projectiles that inflict terrablaze
Terra Blade EX"); */
        }
        public override void SetDefaults()
		{
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = SoundID.Item1;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.damage = 1200;
			Item.useAnimation = 21;
			Item.useTime = 21;
			Item.width = 62;
			Item.height = 74;
			Item.shoot = ModContent.ProjectileType<TerraShotEX>();
			Item.shootSpeed = 7f;
			Item.knockBack = 7f;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.value = Item.sellPrice(0, 20, 0, 0);
			Item.autoReuse = true;
			Item.crit = 8;
            Item.expert = true; Item.expertOnly = true;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(ModContent.BuffType<Terrablaze_Buff>(), 600);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TerraBlade);
            recipe.AddIngredient(ModContent.ItemType<EXSoul>());
            recipe.AddIngredient(ModContent.ItemType<TerraPrism>(), 1);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
    }
}

