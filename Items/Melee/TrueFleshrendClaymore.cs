using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
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
			Item.shoot = Mod.Find<ModProjectile>("TrueFleshClaymoreShot").Type;
            Item.shootSpeed = 12f;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override void AddRecipes()
		{
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(Mod, "FleshrendClaymore", 1);
                recipe.AddIngredient(ItemID.BrokenHeroSword, 1);
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
            }
		}


        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
	       player.HealEffect(damage / 20);
        }
    }
}
