using AAModClassic._Content.Mire.Buffs;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons
{
    public class Masamune : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Melee";
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Masamune");
            /* Tooltip.SetDefault(@"Left click to quickly slash at your foes with the blade
Ignores invicibility frames
Right click to shoot a blade wave"); */
		}

		public override void SetDefaults()
		{
            Item.damage = 350;
            Item.width = 70; 
            Item.height = 80;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.channel = true;
            Item.useAnimation = 25;
            Item.useTime = 15;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 5;
            Item.knockBack = 4f;
            Item.autoReuse = false;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.shoot = ModContent.ProjectileType<Masamune_Surasshu>();
            Item.shootSpeed = 15f;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
        }

        

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2)
            {
                Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
                Item.noMelee = false;
                Item.noUseGraphic = false;
                Item.damage = 250;
                Item.channel = false;
                Item.useAnimation = 15;
                Item.useTime = 15;
                Item.useStyle = ItemUseStyleID.Swing;
                Item.autoReuse = true;
                Item.shoot = ModContent.ProjectileType<Masamune_Slash>();
                Item.shootSpeed = 12f;
            }
            else
            {
                Item.damage = 350;
                Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
                Item.noMelee = true;
                Item.noUseGraphic = true;
                Item.channel = true;
                Item.useAnimation = 25;
                Item.useTime = 5;
                Item.useStyle = ItemUseStyleID.Shoot;
                Item.autoReuse = false;
                Item.shoot = ModContent.ProjectileType<Masamune_Surasshu>();
                Item.shootSpeed = 15f;
            }
            return base.CanUseItem(player);
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<Moonraze_Buff>(), 600);
        }
    }
}