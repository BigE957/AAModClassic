using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace AAModClassic._Unreleased.NPCs.Bosses.Infinity
{
    [AutoloadBossHead]
    public class IZHand2 : IZHand1
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
