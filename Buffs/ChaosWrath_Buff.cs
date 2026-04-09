using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class ChaosWrath_Buff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaotic Wrath");
            // Description.SetDefault("Pain only makes you stronger");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (modPlayer.perfectChaosRa)
            {
                if (player.statLife <= player.statLifeMax2 * .2f)
                {
                    player.GetDamage(DamageClass.Ranged) += .4f;
                    player.GetCritChance(DamageClass.Ranged) += 7;
                }
                else if (player.statLife <= player.statLifeMax2 * .4f)
                {
                    player.GetDamage(DamageClass.Ranged) += .3f;
                    player.GetCritChance(DamageClass.Ranged) += 14;
                }
                else if (player.statLife <= player.statLifeMax2 * .6f)
                {
                    player.GetDamage(DamageClass.Ranged) += .2f;
                    player.GetCritChance(DamageClass.Ranged) += 21;
                }
                else if (player.statLife <= player.statLifeMax2 * .8f)
                {
                    player.GetDamage(DamageClass.Ranged) += .1f;
                    player.GetCritChance(DamageClass.Ranged) += 28;
                }
            }
            else if (modPlayer.perfectChaosSu)
            {
                if (player.statLife <= player.statLifeMax2 * .2f)
                {
                    player.GetDamage(DamageClass.Summon) += .60f;
                }
                else if (player.statLife <= player.statLifeMax2 * .4f)
                {
                    player.GetDamage(DamageClass.Summon) += .45f;
                }
                else if (player.statLife <= player.statLifeMax2 * .6f)
                {
                    player.GetDamage(DamageClass.Summon) += .3f;
                }
                else if (player.statLife <= player.statLifeMax2 * .8f)
                {
                    player.GetDamage(DamageClass.Summon) += .15f;
                }
            }
            else if (modPlayer.perfectChaosMe)
            {
                if (player.statLife <= player.statLifeMax2 * .2f)
                {
                    player.endurance += .06f;
                    player.GetDamage(DamageClass.Melee) += .4f;
                }
                else if (player.statLife <= player.statLifeMax2 * .4f)
                {
                    player.endurance += .04f;
                    player.GetDamage(DamageClass.Melee) += .3f;
                }
                else if (player.statLife <= player.statLifeMax2 * .6f)
                {
                    player.endurance += .02f;
                    player.GetDamage(DamageClass.Melee) += .2f;
                }
                if (player.statLife <= player.statLifeMax2 * .8f)
                {
                    player.endurance += .01f;
                    player.GetDamage(DamageClass.Melee) += .1f;
                }
            }
            else if (modPlayer.perfectChaosMa)
            {
                if (player.statLife <= player.statLifeMax2 * .2f)
                {
                    player.manaCost *= 0;
                    player.GetDamage(DamageClass.Magic) += .4f;
                }
                else if (player.statLife <= player.statLifeMax2 * .4f)
                {
                    player.manaCost *= .25f;
                    player.GetDamage(DamageClass.Magic) += .3f;
                }
                else if (player.statLife <= player.statLifeMax2 * .6f)
                {
                    player.manaCost *= .5f;
                    player.GetDamage(DamageClass.Magic) += .2f;
                }
                else if (player.statLife <= player.statLifeMax2 * .8f)
                {
                    player.manaCost *= .75f;
                    player.GetDamage(DamageClass.Magic) += .1f;
                }
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}