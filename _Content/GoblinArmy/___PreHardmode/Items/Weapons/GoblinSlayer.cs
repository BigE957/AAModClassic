using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.GoblinArmy.___PreHardmode.Items.Weapons
{
    public class GoblinSlayer : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Goblin Slayer");
            /* Tooltip.SetDefault(@"Can be swung with left click and thrust forward with a right click
'The blade of a legendary goblin slayer'"); */
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

		public override void SetDefaults()
		{
			Item.damage = 30;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 46;
			Item.height = 46;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 2;
			Item.value = Item.sellPrice (0, 1, 0, 0);
			Item.rare = ItemRarityID.Orange;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.useStyle = ItemUseStyleID.Thrust;
            }
            else
            {
                Item.useStyle = ItemUseStyleID.Swing;
            }
            return base.CanUseItem(player);
        }

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.altFunctionUse == 2)
            {
                if ((double)player.itemAnimation > (double)player.itemAnimationMax * 0.666)
                {
                    player.itemLocation.X = -1000f;
                    player.itemLocation.Y = -1000f;
                    player.itemRotation = -1.3f * (float)player.direction;
                }
                else
                {
                    player.itemLocation.X = player.position.X + (float)player.width * 0.5f + ((float)heldItemFrame.Width * 0.5f - 4f) * (float)player.direction;
                    player.itemLocation.Y = player.position.Y + 24f + player.HeightOffsetHitboxCenter;
                    float num18 = (float)player.itemAnimation / (float)player.itemAnimationMax * (float)heldItemFrame.Width * (float)player.direction * player.GetAdjustedItemScale(Item) * 1.2f - (float)(10 * player.direction);
                    if (num18 > -4f && player.direction == -1)
                    {
                        num18 = -8f;
                    }
                    if (num18 < 4f && player.direction == 1)
                    {
                        num18 = 8f;
                    }
                    player.itemLocation.X -= num18;
                    player.itemRotation = 0.8f * (float)player.direction;
                }
                if (player.gravDir == -1f)
                {
                    player.itemRotation = 0f - player.itemRotation;
                    player.itemLocation.Y = player.position.Y + (float)player.height + (player.position.Y - player.itemLocation.Y);
                }
            }
            else
            {
                Vector2 zero;
                if ((double)player.itemAnimation < (double)player.itemAnimationMax * 0.333)
                {
                    float num4 = 10f;
                    if (heldItemFrame.Width > 32)
                    {
                        num4 = 14f;
                    }
                    if (heldItemFrame.Width >= 52)
                    {
                        num4 = 24f;
                    }
                    if (heldItemFrame.Width >= 64)
                    {
                        num4 = 28f;
                    }
                    if (heldItemFrame.Width >= 92)
                    {
                        num4 = 38f;
                    }
                    player.itemLocation.X = player.position.X + (float)player.width * 0.5f + ((float)heldItemFrame.Width * 0.5f - num4) * (float)player.direction;
                    player.itemLocation.Y = player.position.Y + 24f + player.HeightOffsetHitboxCenter;
                    zero = new Vector2(-4f, 1f);
                }
                else if ((double)player.itemAnimation < (double)player.itemAnimationMax * 0.666)
                {
                    float num5 = 10f;
                    if (heldItemFrame.Width > 32)
                    {
                        num5 = 18f;
                    }
                    if (heldItemFrame.Width >= 52)
                    {
                        num5 = 24f;
                    }
                    if (heldItemFrame.Width >= 64)
                    {
                        num5 = 28f;
                    }
                    if (heldItemFrame.Width >= 92)
                    {
                        num5 = 38f;
                    }
                    player.itemLocation.X = player.position.X + (float)player.width * 0.5f + ((float)heldItemFrame.Width * 0.5f - num5) * (float)player.direction;
                    num5 = 10f;
                    if (heldItemFrame.Height > 32)
                    {
                        num5 = 8f;
                    }
                    if (heldItemFrame.Height > 52)
                    {
                        num5 = 12f;
                    }
                    if (heldItemFrame.Height > 64)
                    {
                        num5 = 14f;
                    }
                    player.itemLocation.Y = player.position.Y + num5 + player.HeightOffsetHitboxCenter;
                    zero = new Vector2(-6f, -4f);
                }
                else
                {
                    float num6 = 6f;
                    if (heldItemFrame.Width > 32)
                    {
                        num6 = 14f;
                    }
                    if (heldItemFrame.Width >= 48)
                    {
                        num6 = 18f;
                    }
                    if (heldItemFrame.Width >= 52)
                    {
                        num6 = 24f;
                    }
                    if (heldItemFrame.Width >= 64)
                    {
                        num6 = 28f;
                    }
                    if (heldItemFrame.Width >= 92)
                    {
                        num6 = 38f;
                    }
                    player.itemLocation.X = player.position.X + (float)player.width * 0.5f - ((float)heldItemFrame.Width * 0.5f - num6) * (float)player.direction;
                    num6 = 10f;
                    if (heldItemFrame.Height > 32)
                    {
                        num6 = 10f;
                    }
                    if (heldItemFrame.Height > 52)
                    {
                        num6 = 12f;
                    }
                    if (heldItemFrame.Height > 64)
                    {
                        num6 = 14f;
                    }
                    player.itemLocation.Y = player.position.Y + num6 + player.HeightOffsetHitboxCenter;
                    zero = new Vector2(4f, -2f);
                }
                if (Item.type > -1 && ItemID.Sets.UsesBetterMeleeItemLocation[Item.type])
                {
                    player.itemLocation += zero * player.Directions;
                }
                player.itemRotation = ((float)player.itemAnimation / (float)player.itemAnimationMax - 0.5f) * (float)(-player.direction) * 3.5f - (float)player.direction * 0.3f;
                if (player.gravDir == -1f)
                {
                    player.itemRotation = 0f - player.itemRotation;
                    player.itemLocation.Y = player.position.Y + (float)player.height + (player.position.Y - player.itemLocation.Y);
                }
            }
        }

        public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
        {
            if (player.altFunctionUse == 2)
            {
                if ((double)player.itemAnimation > (double)player.itemAnimationMax * 0.666)
                {
                    noHitbox = true;
                }
                else
                {
                    if (player.direction == -1)
                    {
                        hitbox.X -= (int)((double)hitbox.Width * 1.4 - (double)hitbox.Width);
                    }
                    hitbox.Width = (int)((double)hitbox.Width * 1.4);
                    hitbox.Y += (int)((double)hitbox.Height * 0.6);
                    hitbox.Height = (int)((double)hitbox.Height * 0.6);
                }
            }
            else
            {
                if ((double)player.itemAnimation < (double)player.itemAnimationMax * 0.333)
                {
                    if (player.direction == -1)
                    {
                        hitbox.X -= (int)((double)hitbox.Width * 1.4 - (double)hitbox.Width);
                    }
                    hitbox.Width = (int)((double)hitbox.Width * 1.4);
                    hitbox.Y += (int)((double)hitbox.Height * 0.5 * (double)player.gravDir);
                    hitbox.Height = (int)((double)hitbox.Height * 1.1);
                }
                else if (!((double)player.itemAnimation < (double)player.itemAnimationMax * 0.666))
                {
                    if (player.direction == 1)
                    {
                        hitbox.X -= (int)((double)hitbox.Width * 1.2);
                    }
                    hitbox.Width *= 2;
                    hitbox.Y -= (int)(((double)hitbox.Height * 1.4 - (double)hitbox.Height) * (double)player.gravDir);
                    hitbox.Height = (int)((double)hitbox.Height * 1.4);
                }
            }
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
