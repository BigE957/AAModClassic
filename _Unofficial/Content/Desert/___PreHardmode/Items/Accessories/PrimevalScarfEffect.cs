using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Inferno.Buffs;
using AAModClassic._Content.Mire.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic._Unofficial.Content._Tinker.EquipmentEffects;
using AAModClassic.Globals;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content.Desert.___PreHardmode.Items.Accessories
{
    public class PrimevalScarfEffect : EquipmentEffectData
    {
        public override void DoEffect(Player player)
        {
            player.GetModPlayer<PrimevalScarfPlayer>().effect = true;
        }
    }

    public class PrimevalScarfPlayer : EquipmentEffectPlayer
    {
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (effect && hit.Crit)
            {
                Vector2 velocity = new Vector2(Main.rand.NextFloat(-0.6f, 0.6f), -1) * 20;
                Projectile.NewProjectile(Player.GetSource_FromThis(), Player.Center, velocity, ModContent.ProjectileType<PrimevalScarf_DynaArrow>(), 15, 2, Main.myPlayer, target.whoAmI);
            }
        }
    }
}