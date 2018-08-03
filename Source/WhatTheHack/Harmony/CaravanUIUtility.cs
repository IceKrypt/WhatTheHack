﻿using Harmony;
using RimWorld;
using RimWorld.Planet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;

namespace WhatTheHack.Harmony
{
    [HarmonyPatch(typeof(CaravanUIUtility), "AddPawnsSections")]
    class CaravanUIUtility_AddPawnsSections
    {
        static void Postfix(ref TransferableOneWayWidget widget, List<TransferableOneWay> transferables)
        {
            IEnumerable<TransferableOneWay> mechs = from x in transferables
                                                     where x.ThingDef.category == ThingCategory.Pawn
                                                     && ((Pawn)x.AnyThing).RaceProps.IsMechanoid
                                                     && ((Pawn)x.AnyThing).IsHacked()
                                                     select x;

            widget.AddSection("WTH_MechanoidsSection".Translate(), mechs);
        }
    }
}
