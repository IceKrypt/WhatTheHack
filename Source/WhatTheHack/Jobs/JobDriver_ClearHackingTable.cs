﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using Verse.AI;
using WhatTheHack.Buildings;

namespace WhatTheHack.Jobs
{
    class JobDriver_ClearHackingTable : JobDriver
    {
        protected Pawn Takee
        {
            get
            {
                return (Pawn)this.job.GetTarget(TargetIndex.A).Thing;
            }
        }
        protected Building_HackingTable HackingTable
        {
            get
            {
                return (Building_HackingTable)this.job.GetTarget(TargetIndex.B).Thing;
            }
        }
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            //return true;
            return this.pawn.Reserve(this.Takee, this.job, 1, -1, null) && this.pawn.Reserve(this.HackingTable, this.job, 1, -1, null);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDestroyedOrNull(TargetIndex.A);
            this.FailOnDestroyedOrNull(TargetIndex.B);
            this.FailOnAggroMentalState(TargetIndex.A);
            //this.FailOn(() => !this.DropPod.Accepts(this.Takee));
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.OnCell).FailOnDestroyedNullOrForbidden(TargetIndex.A).FailOnDespawnedNullOrForbidden(TargetIndex.B).FailOn(() => !this.pawn.CanReach(this.Takee, PathEndMode.OnCell, Danger.Deadly, false, TraverseMode.ByPawn)).FailOnSomeonePhysicallyInteracting(TargetIndex.A);
            yield return Toils_Haul.StartCarryThing(TargetIndex.A, false, false, false);
            yield return Toils_Misc.FindRandomAdjacentReachableCell(TargetIndex.B, TargetIndex.C);
            yield return Toils_Goto.GotoThing(TargetIndex.C, PathEndMode.OnCell);

            //yield return Toils_Haul.CarryHauledThingToCell(TargetIndex.A)
            yield return new Toil
            {
                initAction = delegate
                {
                     this.pawn.carryTracker.TryDropCarriedThing(pawn.Position, ThingPlaceMode.Direct, out Thing thing, null);
                },
                defaultCompleteMode = ToilCompleteMode.Instant
            };
        }
    }
}
