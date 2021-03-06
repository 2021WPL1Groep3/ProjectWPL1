﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestingPlanner.Viewmodels
{
    class ViewModelApproveJRQueue: AbstractViewModelCollectionRQ
    {
        //Constructor
        public ViewModelApproveJRQueue() : base()
        {
            Load();
        }

        // Function used in code behind
        // Loads all JR IDs in LB
        public void Load()
        {
            // Get unapproved JR's
            var requestIds = _dao.GetAllJobRequests().Where(rq => rq.JrStatus == "To approve");
            IdRequestsOnly.Clear();

            foreach (var requestId in requestIds)
            {
                IdRequestsOnly.Add(requestId);
            }
        }
    }
}
