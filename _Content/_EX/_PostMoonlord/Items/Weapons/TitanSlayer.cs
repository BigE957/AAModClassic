using AAModClassic._Content._Dev._PostMoonlord.Items.Weapons;
using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class TitanSlayer : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
        private static bool RogueMaxxing = false;
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Titan Slayer");
            /* Tooltip.SetDefault(@"Left click to quickly swing the axe
Right click to throw the axe
Titan Axe EX"); */
            if (ModLoader.TryGetMod("CalamityMod", out _))
                RogueMaxxing = true;
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
            Item.shoot = ModContent.ProjectileType<TitanSlayer_Proj>(); 
            Item.shootSpeed = 15f;
            Item.expert = true;
            Item.UseSound = SoundID.Item1;
            if (ModLoader.TryGetMod("Redemption", out var redemption))
                redemption.Call("setAxeBonus", Item);
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
                Item.DamageType = RogueMaxxing ? DamageClass.Throwing : DamageClass.Ranged;
                Item.shoot = ModContent.ProjectileType<TitanSlayer_Proj>();
            }
            else
            {
                Item.damage = 450;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
                Item.shoot = ModContent.ProjectileType<TitanSlayer_Slash>();
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
            recipe.AddIngredient(ModContent.ItemType<TitanAxe>(), 1);
            recipe.AddIngredient(ModContent.ItemType<EXSoul>(), 1);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }
}