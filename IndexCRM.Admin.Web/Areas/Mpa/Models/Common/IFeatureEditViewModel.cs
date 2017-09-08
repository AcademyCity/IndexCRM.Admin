﻿using System.Collections.Generic;
using Abp.Application.Services.Dto;
using IndexCRM.Admin.Editions.Dto;

namespace IndexCRM.Admin.Web.Areas.Mpa.Models.Common
{
    public interface IFeatureEditViewModel
    {
        List<NameValueDto> FeatureValues { get; set; }

        List<FlatFeatureDto> Features { get; set; }
    }
}