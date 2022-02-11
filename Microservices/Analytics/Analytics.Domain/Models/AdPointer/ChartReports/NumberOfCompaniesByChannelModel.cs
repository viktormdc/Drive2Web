using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Analytics.Domain.Models.AdPointer.ChartReports
{
    public class NumberOfCompaniesByChannelModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<string> SearchChannelItems { get; set; }
        public List<string> SearchCompaniesItems { get; set; }
    }

    public class NumberOfCompaniesByChannelValidator : AbstractValidator<NumberOfCompaniesByChannelModel>
    {
        public NumberOfCompaniesByChannelValidator()
        {
            RuleFor(x => x.SearchCompaniesItems).NotEmpty().WithMessage(Errors.ErrorModel.RequiredField);
            RuleFor(x => x.SearchChannelItems).NotEmpty().WithMessage(Errors.ErrorModel.RequiredField);
            RuleFor(x => x.StartDate).NotEmpty().WithMessage(Errors.ErrorModel.StartDateRequiredField);
            RuleFor(x => x.EndDate).NotEmpty().WithMessage(Errors.ErrorModel.EndDateRequiredField);


        }
    }
}
