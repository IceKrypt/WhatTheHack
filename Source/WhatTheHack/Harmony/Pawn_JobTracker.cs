﻿using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using Verse.AI;
using WhatTheHack.Buildings;
using WhatTheHack.Storage;

namespace WhatTheHack.Harmony
{
    [HarmonyPatch(typeof(Pawn_JobTracker), "DetermineNextJob")]
    static class Pawn_JobTracker_DetermineNextJob
    {
        static void Postfix(ref Pawn_JobTracker __instance, ref ThinkResult __result)
        {
            
            Pawn pawn = Traverse.Create(__instance).Field("pawn").GetValue<Pawn>();
            if (!pawn.RaceProps.IsMechanoid)
            {
                return;
            }
            if(pawn.IsHacked() && pawn.OnMechanoidPlatform())
            {
                Job job = new Job(WTH_DefOf.Mechanoid_Rest, pawn.CurrentBed());
                job.count = 1;
                __result = new ThinkResult(job, __result.SourceNode, __result.Tag, false);

            }
            if(pawn.IsHacked() && !pawn.IsActivated())
            { 
                Building_MechanoidPlatform closestAvailablePlatform = Utilities.GetAvailableMechanoidPlatform(pawn, pawn);
                if(closestAvailablePlatform != null)
                {
                    Job job = new Job(WTH_DefOf.Mechanoid_Rest, closestAvailablePlatform);
                    __result = new ThinkResult(job, __result.SourceNode, __result.Tag, false);
                }                
            }

        }
    }
}