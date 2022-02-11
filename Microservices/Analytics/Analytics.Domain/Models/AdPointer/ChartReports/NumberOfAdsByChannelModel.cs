using System;
using System.Collections.Generic;
using System.Text;
using Analytics.Domain.Models.Helper.SelectListItem;
using FluentValidation;
namespace Analytics.Domain.Models.AdPointer.ChartReports
{
    public class NumberOfAdsByChannelModel
    {
       
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<string> SearchChannelItems { get; set; }
        public List<string> SearchAdItems { get; set; }
    }

    public class NumberOfAdsByChannelValidator : AbstractValidator<NumberOfAdsByChannelModel>
    {
        public NumberOfAdsByChannelValidator()
        {
            RuleFor(x => x.SearchAdItems).NotEmpty().WithMessage(Errors.ErrorModel.RequiredField);
            RuleFor(x => x.SearchChannelItems).NotEmpty().WithMessage(Errors.ErrorModel.RequiredField);
            RuleFor(x => x.StartDate).NotEmpty().WithMessage(Errors.ErrorModel.StartDateRequiredField);
            RuleFor(x => x.EndDate).NotEmpty().WithMessage(Errors.ErrorModel.EndDateRequiredField);
          

        }
    }
}
