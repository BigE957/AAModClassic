using AAModClassic;
using AAModClassic.Globals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.AH
{
    public class Masamune : BaseAAItem
    {
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
            Item.shoot = ModContent.ProjectileType<Surasshu>();
            Item.shootSpeed = 15f;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> list)
        {
            foreach (Terraria.ModLoader.TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity12;
                }
            }
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
                Item.shoot = ModContent.ProjectileType<MasamuneSlash>();
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
                Item.shoot = ModContent.ProjectileType<Surasshu>();
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