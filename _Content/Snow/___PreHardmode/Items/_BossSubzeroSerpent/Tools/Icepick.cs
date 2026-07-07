using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Snow.___PreHardmode.Items._BossSubzeroSerpent.Tools
{
    public class Icepick : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Tools";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Icepick");
        }

        public override void SetDefaults()
        {

            Item.damage = 10;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 46;
            Item.height = 42;
            Item.useTime = 13;
            Item.useAnimation = 20;
            Item.pick = 105;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 3;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }


        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Chilled, 120);
        }
    }
}
