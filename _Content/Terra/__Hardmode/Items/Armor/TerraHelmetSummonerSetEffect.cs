using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Armor;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Terra.__Hardmode.Items.Armor
{
    public class TerraHelmetSummonerSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<TerraHelmetSummonerSetPlayer>().effect = true;

            if (player.whoAmI == Main.myPlayer)
            {
                if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial) && player.FindBuffIndex(ModContent.BuffType<ChampionHelmetSummonerSetEffect_Buff>()) == -1)
                {
                    player.AddBuff(ModContent.BuffType<TerraHelmetSummonerSetEffect_Buff>(), 3600, true);
                }
                if (player.ownedProjectileCounts[ModContent.ProjectileType<TerraHelmetSummonerSetEffect_TerraCrystal>()] < 1)
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<TerraHelmetSummonerSetEffect_TerraCrystal>(), (int)player.GetDamage(DamageClass.Summon).ApplyTo(60), 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }
    }

    public class TerraHelmetSummonerSetPlayer : EquipmentEffectPlayer
    {
        public int CrystalMode = 0;

        public override void PostUpdate()
        {
            base.PostUpdate();

            if (effect)
            {
                if (AAMod.ArmorAbilityKey.JustPressed)
                {
                    CrystalMode++;
                    if (CrystalMode > 2)
                    {
                        CrystalMode = 0;
                    }
                }
                if (CrystalMode == 2)
                {
                    Player.lifeRegen += 12;
                    Player.statDefense.FinalMultiplier *= 1.2f;
                    Player.GetDamage(DamageClass.Generic) /= 2;
                }
            }
        }
    }
}