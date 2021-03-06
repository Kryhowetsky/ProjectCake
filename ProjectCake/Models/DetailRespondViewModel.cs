﻿using ProjectCake.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCake.Models
{
    public class DetailRespondViewModel
    {
        public ProductViewModel ProductViewModel { get; set; }
        public RespondViewModel CreateRespondViewModel { get; set; }
        public List<RespondViewModel> Responses { get; set; }
    }
}
