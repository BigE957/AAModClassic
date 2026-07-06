using AAModClassic._Content.Bunny.__Hardmode.NPCs.__BossRajahRabbit;
using AAModClassic._Content.Bunny._PostMoonlord.Items._BossRajahRabbitA.Weapons;
using AAModClassic._Content.Bunny.Projectiles;
using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny._PostMoonlord.NPCs.__BossRajahRabbitA
{
    public class RajahRabbitA_TheAvenger : RajahRabbit_ThePunisher, ILocalizedModType
    {
        public new string LocalizationCategory => "Projectiles";
        public override string Texture => ModContent.GetInstance<TheAvenger_Holdout>().Texture;

        public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("The Avenger");
        }
    }
}