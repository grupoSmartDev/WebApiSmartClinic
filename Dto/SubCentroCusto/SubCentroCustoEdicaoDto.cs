﻿using WebApiSmartClinic.Models;

namespace WebApiSmartClinic.Dto.SubCentroCusto
{
    public class SubCentroCustoEdicaoDto : SubCentroCustoModel
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public int CentroCustoId { get; set; }
    }
}
