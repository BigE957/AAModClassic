using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic._Content.Crimson.___PreHardmode.Items.Weapons;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Crimson.__Hardmode.Items.Weapons
{
    public class TrueFleshrendClaymore : BaseAAItem
	{
        
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("True Fleshrend Claymore");
			/* Tooltip.SetDefault(@"Inflics Ichor on your target
Despite the name, it's not actually made of flesh"); */
        }
		public override void SetDefaults()
		{
            
			Item.damage = 150;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 75;
			Item.height = 71;
			Item.useTime = 29;
			Item.useAnimation = 29;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 8;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<TrueFleshClaymore_FleshBeam>();
            Item.shootSpeed = 12f;
        }

        public override void AddRecipes()
		{
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<FleshrendClaymore>(), 1);
                recipe.AddIngredient(ItemID.SoulofFright, 20);
                recipe.AddIngredient(ItemID.SoulofMight, 20);
                recipe.AddIngredient(ItemID.SoulofSight, 20);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }
		}


        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
	       player.HealEffect(damageDone / 20);
        }
    }
}
