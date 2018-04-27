﻿using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using WhatTheHack.Storage;

namespace WhatTheHack.Harmony
{

    [HarmonyPatch(typeof(StatWorker), "IsDisabledFor")]
    static class StatWorker_IsDisabledFor
    {
        static bool Prefix(Thing thing, ref bool __result)
        {
            if(thing is Pawn && ((Pawn)thing).RaceProps.IsMechanoid)
            {
                Pawn pawn = (Pawn)thing;
                ExtendedDataStorage store = Base.Instance.GetExtendedDataStorage();
                if(store != null)
                {
                    ExtendedPawnData pawnData = store.GetExtendedDataFor(pawn);
                    if (pawnData.isHacked)
                    {
                        __result = false;
                        return false;
                    }
                }
            }
            return true;
        }

    }
    
}
