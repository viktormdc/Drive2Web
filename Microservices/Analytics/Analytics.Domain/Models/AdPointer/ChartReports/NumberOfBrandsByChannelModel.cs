using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.AdPointer.ChartReports
{
    public class NumberOfBrandsByChannelModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<string> SearchChannelItems { get; set; }
        public List<string> SearchBrandlItems { get; set; }
    }

    public class NumberOfBrandsByChannelValidator : AbstractValidator<NumberOfBrandsByChannelModel>
    {
        public NumberOfBrandsByChannelValidator()
        {
            RuleFor(x => x.SearchBrandlItems).NotEmpty().WithMessage(Errors.ErrorModel.RequiredField);
            RuleFor(x => x.SearchChannelItems).NotEmpty().WithMessage(Errors.ErrorModel.RequiredField);
            RuleFor(x => x.StartDate).NotEmpty().WithMessage(Errors.ErrorModel.StartDateRequiredField);
            RuleFor(x => x.EndDate).NotEmpty().WithMessage(Errors.ErrorModel.EndDateRequiredField);


        }
    }
}
