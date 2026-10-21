using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAModClassic.Utilities;

public enum DrawLayer
{
    BeforeAllTiles,
    BeforeSolidTiles,
    BeforeNPCs,
    AfterNPCs,
    BeforeProjectiles,
    AfterProjectiles,
    AfterPlayers,
    AfterDusts,
    AfterEverything,
}