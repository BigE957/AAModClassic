using AAModClassic;
using AAModClassic.___Content.Mire.Buffs;
using AAModClassic.Projectiles.AH;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Dev
{
    public class TitanAxeEX : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Titan Slayer");
            /* Tooltip.SetDefault(@"Left click to quickly swing the axe
Right click to throw the axe
Titan Axe EX"); */
		}

		public override void SetDefaults()
		{
            Item.CloneDefaults(ItemID.Arkhalis);
            Item.damage = 300;
            Item.width = 94; 
            Item.height = 96;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 20;
            Item.knockBack = 4f;
            Item.autoReuse = false;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.shoot = ModContent.ProjectileType<Surasshu>();
            Item.shootSpeed = 15f;
            Item.expert = true; Item.expertOnly = true;
            Item.UseSound = SoundID.Item1;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2)
            {
                Item.damage = 300;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.DamageType = DamageClass.Throwing;
                Item.shoot = ModContent.ProjectileType<Projectiles.TitanAxeEX>();
            }
            else
            {
                Item.damage = 450;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
                Item.shoot = ModContent.ProjectileType<Projectiles.TitanEX>();
            }
            return base.CanUseItem(player);
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Moonraze_Buff>(), 600);
            target.AddBuff(BuffID.Daybreak, 600);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(1);
            recipe.AddIngredient(null, "TitanAxe", 1);
            recipe.AddIngredient(null, "EXSoul", 1);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }
    }
}