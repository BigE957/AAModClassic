using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AAModClassic._Unreleased.Content.Void._PostMoonLord.NPCs.InfinityZero
{
    [AutoloadBossHead]
    public class InfinityZeroHand2 : InfinityZeroHand1
    {
		public override string Texture
		{
			get
			{
				return "AAModClassic/_Unreleased/NPCs/Bosses/Infinity/IZHand1";
			}
		}			
		
        public override void SetDefaults()
        {
			base.SetDefaults();
			leftHand = false;
        }
	}
}
