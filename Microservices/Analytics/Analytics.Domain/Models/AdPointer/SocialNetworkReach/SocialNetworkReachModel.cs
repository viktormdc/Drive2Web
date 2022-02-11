using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Analytics.Domain.Models.AdPointer.SocialNetworkReach
{
   public class SocialNetworkReachModel
    {
        public string UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

   public class SocialNetworkReachValidator : AbstractValidator<SocialNetworkReachModel>
   {
       public SocialNetworkReachValidator()
       {
           RuleFor(x => x.UserId).NotEmpty().WithMessage(Errors.ErrorModel.TheUserIsNullOrEmpty);
           RuleFor(x => x.StartDate).NotEmpty().WithMessage(Errors.ErrorModel.StartDateRequiredField);
           RuleFor(x => x.EndDate).NotEmpty().WithMessage(Errors.ErrorModel.EndDateRequiredField);
           
       }
   }
}
