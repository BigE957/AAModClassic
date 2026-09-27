using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace AAModClassic._Content.Crimson.___PreHardmode.Items.Armor
{
    public class FleshrendHelmetSetEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<FleshrendHelmetSetPlayer>().effect = true;
        }
    }

    public class FleshrendHelmetSetPlayer : EquipmentEffectPlayer
    {
        public override void OnHitByAnything(Player.HurtInfo hurtInfo, NPC npc = null, Projectile proj = null)
        {
            if (effect && Main.rand.NextBool(2))
            {
                if (Player.whoAmI == Main.myPlayer)
                {
                    for (int i = 0; i < 40; i++)
                    {
                        Vector2 position = new Vector2(Player.Center.X - 40, Player.Center.Y - 40);
                        Dust.NewDust(position, 80, 80, DustID.RainCloud, 0f, 0f, 124, new Color(255, 50, 0), 1f);
                    }

                    for (int i = 0; i < Main.maxNPCs; i++)
                    {
                        NPC target = Main.npc[i];
                        float dist = npc.Distance(Player.Center);

                        if (target.active && !target.dontTakeDamage && !target.friendly && target.immune[Player.whoAmI] == 0 && dist < 100f)
                        {
                            Player.ApplyDamageToNPC(target, 30, 0, 0, false);
                        }
                    }
                }
            }
        }
    }
}