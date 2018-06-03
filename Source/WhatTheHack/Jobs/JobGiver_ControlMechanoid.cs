﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Verse;
using Verse.AI;

namespace WhatTheHack.Jobs
{
    class JobGiver_ControlMechanoid : ThinkNode_JobGiver
    {
        protected override Job TryGiveJob(Pawn pawn)
        {
            if(pawn.RemoteControlLink() != null)
            {
                Job job = new Job(WTH_DefOf.ControlMechanoid);
                job.count = 1;
                return job;
            }
            return null;
        }
    }
}
