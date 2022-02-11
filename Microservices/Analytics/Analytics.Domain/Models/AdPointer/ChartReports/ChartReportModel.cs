using System;
using System.Collections.Generic;
using System.Text;
using Analytics.Domain.Models.Helper.SelectListItem;
using FluentValidation;

namespace Analytics.Domain.Models.AdPointer.ChartReports
{

    public class ChartReportModel
    {
        public string UserId { get; set; }
        public SelectListItem SearchFilterItem { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

    }

    public class ChartReportValidator : AbstractValidator<ChartReportModel>
    {
        public ChartReportValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().WithMessage(Errors.ErrorModel.TheUserIsNullOrEmpty);
            RuleFor(x => x.StartDate).NotEmpty().WithMessage(Errors.ErrorModel.StartDateRequiredField);
            RuleFor(x => x.EndDate).NotEmpty().WithMessage(Errors.ErrorModel.EndDateRequiredField);
            RuleFor(x => x.SearchFilterItem.Key).NotEmpty().WithMessage(Errors.ErrorModel.RequiredField);

        }
    }
}
