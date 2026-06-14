using Microsoft.Xna.Framework;
using Terraria;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.ID;
using AAModClassic.Globals;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Weapons;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;

namespace AAModClassic._Content.Void._PostMoonlord.Items._BossZero.Weapons
{
    public class RiftShredder : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
        
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Rift Shredder");
			// Tooltip.SetDefault("Shoots void stars that shred through reality itself");
        }

		public override void SetDefaults()
		{
            
			Item.damage = 190;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 94;
			Item.height = 70;
			Item.useTime = 22;
            Item.shoot = ModContent.ProjectileType<RiftShredder_Rift>();
            Item.shootSpeed = 10f;
            Item.useAnimation = 22;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 3;
			Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.UseSound = SoundID.Item15;
			Item.autoReuse = true;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ApocalyptitePlate>(), 5);
            recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 5);
            recipe.AddIngredient(ModContent.ItemType<BreakingDawn>());
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            Dust dust;
            dust = Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, ModContent.DustType<Dusts.VoidDust>(), 0f, 0f, 46, default, 1.25f);
			dust.noGravity = true;
        }
	}
}
