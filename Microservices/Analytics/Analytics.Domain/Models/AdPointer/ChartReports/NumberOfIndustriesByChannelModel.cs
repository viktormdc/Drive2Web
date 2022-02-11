using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.AdPointer.ChartReports
{
    public class NumberOfIndustriesByChannelModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<string> SearchChannelItems { get; set; }
        public List<string> SearchIndustryItems { get; set; }
    }

    public class NumberOfIndustriesByChannelValidator : AbstractValidator<NumberOfIndustriesByChannelModel>
    {
        public NumberOfIndustriesByChannelValidator()
        {
            RuleFor(x => x.SearchIndustryItems).NotEmpty().WithMessage(Errors.ErrorModel.RequiredField);
            RuleFor(x => x.SearchChannelItems).NotEmpty().WithMessage(Errors.ErrorModel.RequiredField);
            RuleFor(x => x.StartDate).NotEmpty().WithMessage(Errors.ErrorModel.StartDateRequiredField);
            RuleFor(x => x.EndDate).NotEmpty().WithMessage(Errors.ErrorModel.EndDateRequiredField);


        }
    }
}
