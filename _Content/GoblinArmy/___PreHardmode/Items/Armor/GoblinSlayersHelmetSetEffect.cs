using AAModClassic._Content.Bunny.__Hardmode.Items.Armor;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Mire._PostMoonlord.Items._BossYamata.BossStandard;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.WorldBuilding;

namespace AAModClassic._Content.GoblinArmy.___PreHardmode.Items.Armor
{
    public class GoblinSlayersHelmetSetDamageEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<GoblinSlayersHelmetSetDamagePlayer>().effect = true;
        }
    }

    public class GoblinSlayersHelmetSetDamagePlayer : EquipmentEffectPlayer
    {
        bool IsGoblin = false;

        public override void ResetEffects()
        {
            IsGoblin = false;
        }

        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (effect)
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
                    modifiers.FinalDamage.Flat *= 5;
                    IsGoblin = true;
                }
            }
        }

        public override void ModifyWeaponKnockback(Item item, ref StatModifier knockback)
        {
            if (IsGoblin)
            {
                knockback += 5f;
            }
        }
    }

    public class GoblinSlayersHelmetSetEnduranceEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<GoblinSlayersHelmetSetEndurancePlayer>().effect = true;
        }
    }

    public class GoblinSlayersHelmetSetEndurancePlayer : EquipmentEffectPlayer
    {
        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (effect)
            {
                if (npc.type == NPCID.GoblinArcher
                    || npc.type == NPCID.GoblinPeon
                    || npc.type == NPCID.GoblinScout
                    || npc.type == NPCID.GoblinSorcerer
                    || npc.type == NPCID.GoblinSummoner
                    || npc.type == NPCID.GoblinThief
                    || npc.type == NPCID.GoblinWarrior
                    || npc.type == NPCID.DD2GoblinBomberT1
                    || npc.type == NPCID.DD2GoblinBomberT2
                    || npc.type == NPCID.DD2GoblinBomberT3
                    || npc.type == NPCID.DD2GoblinT1
                    || npc.type == NPCID.DD2GoblinT2
                    || npc.type == NPCID.DD2GoblinBomberT3
                    || npc.type == NPCID.BoundGoblin
                    || npc.type == NPCID.GoblinTinkerer)
                {
                    hurtInfo.Damage = (int)(hurtInfo.Damage * 0.20f);
                }
            }
        }
    }
}