using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class GoblinSlayer : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Goblin Slayer");
			/* Tooltip.SetDefault(@"Can be swung with left click and thrust forward with a right click
'The blade of a legendary goblin slayer'"); */
		}

		public override void SetDefaults()
		{
			Item.damage = 30;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 46;
			Item.height = 46;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = 1;
			Item.knockBack = 2;
			Item.value = Item.sellPrice (0, 1, 0, 0);
			Item.rare = 3;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
		}

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useStyle = 3;
            }
            else
            {
                Item.useStyle = 1;
            }
            return base.CanUseItem(player);
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.type == NPCID.GoblinArcher
                || target.type == NPCID.GoblinPeon
                || target.type == NPCID.GoblinScout
                || target.type == NPCID.GoblinSorcerer
                || target.type == NPCID.GoblinSummoner
                || target.type == NPCID.GoblinThief
                || target.type == NPCID.GoblinWarrior
                || target.type == NPCID.DD2GoblinBomberT1
                || target.type == NPCID.DD2GoblinBomberT2
                || target.type == NPCID.DD2GoblinBomberT3
                || target.type == NPCID.DD2GoblinT1
                || target.type == NPCID.DD2GoblinT2
                || target.type == NPCID.DD2GoblinBomberT3
                || target.type == NPCID.BoundGoblin
                || target.type == NPCID.GoblinTinkerer)
            {
                Item.damage = 60;
                target.AddBuff(BuffID.Bleeding, 400);
            }
            else
            {
                Item.damage = 30;
            }
        }
	}
}
