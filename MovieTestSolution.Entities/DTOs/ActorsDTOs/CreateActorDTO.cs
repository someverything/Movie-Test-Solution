﻿using MovieTestSolution.Entities.Concrete.Common;
using MovieTestSolution.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.Entities.DTOs.ActorsDTOs
{
    public class CreateActorDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
