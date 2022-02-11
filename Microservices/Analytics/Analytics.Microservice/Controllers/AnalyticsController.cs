using Analytics.Domain.Models.AdPointer.ChartReports;
using Analytics.Domain.Models.AdPointer.SocialNetworkReach;
using Analytics.Service.Interface.Analytics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using FluentValidation.Results;
using Analytics.Domain.Models.Helper.ModalState;

namespace Analytics.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        #region Fields

        private readonly IAnalyticsService _analyticsService;

        #endregion

        #region Ctor

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            this._analyticsService = analyticsService;
        }

        #endregion

        #region Methods

        #region AdPointer Report

        [HttpGet("getsearchfilter")]
        public async Task<ActionResult> GetSearchFilter(string userId)
        {

            try
            {
                 
                var searchFilderItems = await _analyticsService.GetSearchFilter(userId);

                return new JsonResult(searchFilderItems);
            }
            catch (Exception e)
            {
                await _analyticsService.InsertLogError("SystemError", "GetSearchFilter =>" + e.Message, userId);
                var error = new ValidationResult();               
                error.Errors.Add(new ValidationFailure("SystemError", "GetSearchFilter =>" + e.Message) );                 
                return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(error)));
            }

            
        }

        [HttpPost]
        [Route("generatechartreportads")]
        public async Task<ActionResult> GenerateChartReportAds(ChartReportModel model)
        {
            try
            {
                var validator = new ChartReportValidator();
                var result = validator.Validate(model);

                if (!result.IsValid)
                {
                    return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(result)));
                }

                var numberOfAds = await _analyticsService.GenerateChartReportAds(model);

                return new JsonResult(numberOfAds);
            }
            catch (Exception e)
            {
                await _analyticsService.InsertLogError("SystemError", "GenerateChartReportAds =>" + e.Message, model.UserId);
                var error = new ValidationResult();
                error.Errors.Add(new ValidationFailure("SystemError", "GenerateChartReportAds =>" + e.Message));
                return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(error)));

            }
        }

        [HttpPost]
        [Route("generatenumberofbrands")]
        public async Task<ActionResult> GenerateNumberOfBrands(ChartReportModel model)
        {
            try
            {
                var validator = new ChartReportValidator();
                var result = validator.Validate(model);

                if (!result.IsValid)
                {
                    return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(result)));
                }

                var numberOfAds = await _analyticsService.GenerateChartReportBrands(model);
                return new JsonResult(numberOfAds);
            }
            catch (Exception e)
            {
                await _analyticsService.InsertLogError("SystemError", "GenerateNumberOfBrands =>" + e.Message, model.UserId);
                var error = new ValidationResult();
                error.Errors.Add(new ValidationFailure("SystemError", "GenerateNumberOfBrands =>" + e.Message));
                return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(error)));
            }
        }

        [HttpPost]
        [Route("generatenumberofcompanies")]
        public async Task<ActionResult> GenerateNumberOfCompanies(ChartReportModel model)
        {
            try
            {
                
                var validator = new ChartReportValidator();
                var result = validator.Validate(model);

                if (!result.IsValid)
                {
                    return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(result)));
                }

                var numberOfAds = await _analyticsService.GenerateChartReportCompanies(model);
                return new JsonResult(numberOfAds);
            }
            catch (Exception e)
            {
                await _analyticsService.InsertLogError("SystemError", "GenerateNumberOfCompanies =>" + e.Message, model.UserId);
                var error = new ValidationResult();
                error.Errors.Add(new ValidationFailure("SystemError", "GenerateNumberOfCompanies =>" + e.Message));
                return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(error)));
            }
        }

        [HttpPost]
        [Route("generatenumberofindustries")]
        public async Task<ActionResult> GenerateNumberOfIndustries(ChartReportModel model)
        {
            try
            {


                var validator = new ChartReportValidator();
                var result = validator.Validate(model);

                if (!result.IsValid)
                {
                    return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(result)));
                }

                var numberOfAds = await _analyticsService.GenerateChartReportIndustries(model);
                return new JsonResult(numberOfAds);
            }
            catch (Exception e)
            {
                await _analyticsService.InsertLogError("SystemError", "GenerateNumberOfIndustries =>" + e.Message, model.UserId);
                var error = new ValidationResult();
                error.Errors.Add(new ValidationFailure("SystemError", "GenerateNumberOfIndustries =>" + e.Message));
                return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(error)));
            }
        }

        [HttpGet("getsearchfilterads")]
        public async Task<ActionResult> GetSearchFilterAds(string filtersearchid)
        {
            try
            {


                var searchFilderItems = await _analyticsService.GetSearchFilterAds(filtersearchid);

                return new JsonResult(searchFilderItems);
            }
            catch (Exception e)
            {
                await _analyticsService.InsertLogError("SystemError", "GetSearchFilterAds =>" + e.Message, filtersearchid);
                var error = new ValidationResult();
                error.Errors.Add(new ValidationFailure("SystemError", "GetSearchFilterAds =>" + e.Message));
                return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(error)));
            }
        }

        [HttpGet("getchannels")]
        public async Task<ActionResult> GetChannels()
        {
            try
            {
                var searchFilderItems = await _analyticsService.GetChannels();

                return new JsonResult(searchFilderItems);
            }
            catch (Exception e)
            {
                await _analyticsService.InsertLogError("SystemError", "GetChannels =>" + e.Message, string.Empty);
                var error = new ValidationResult();
                error.Errors.Add(new ValidationFailure("SystemError", "GetChannels =>" + e.Message));
                return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(error)));
            }

        }

        [HttpPost]
        [Route("generatenumberofadsbychannel")]
        public async Task<ActionResult> GenerateNumberOfAdsByChannel(NumberOfAdsByChannelModel model)
        {
            try
            {
                var validator = new NumberOfAdsByChannelValidator();
                var result = validator.Validate(model);

                if (!result.IsValid)
                {
                    return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(result)));
                }

                var numberOfAds = await _analyticsService.GenerateNumberOfAdsByChannel(model);

                return new JsonResult(numberOfAds);
            }
            catch (Exception e)
            {
                await _analyticsService.InsertLogError("SystemError", "GenerateNumberOfAdsByChannel =>" + e.Message, string.Empty);
                var error = new ValidationResult();
                error.Errors.Add(new ValidationFailure("SystemError", "GenerateNumberOfAdsByChannel =>" + e.Message));
                return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(error)));
            }

        }

        [HttpGet("getsearchfilterbrands")]
        public async Task<ActionResult> GetSearchFilterBrands(string filtersearchid)
        {
            try
            {
                var searchFilderBrandItems = await _analyticsService.GetSearchFilterBrands(filtersearchid);
                return new JsonResult(searchFilderBrandItems);
            }
            catch (Exception e)
            {
                await _analyticsService.InsertLogError("SystemError", "GetSearchFilter =>" + e.Message, filtersearchid);
                var error = new ValidationResult();
                error.Errors.Add(new ValidationFailure("SystemError", "GetSearchFilterBrands =>" + e.Message));
                return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(error)));
            }

        }

        [HttpPost]
        [Route("generatenumberofbrandsbychannel")]
        public async Task<ActionResult> GenerateNumberOfBrandsByChannel(NumberOfBrandsByChannelModel model)
        {
            try
            {
                var validator = new NumberOfBrandsByChannelValidator();
                var result = validator.Validate(model);

                if (!result.IsValid)
                {
                    return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(result)));
                }

                var returnResult = await _analyticsService.GenerateNumberOfBrandsByChannel(model);

                return new JsonResult(returnResult);
            }
            catch (Exception e)
            {
                await _analyticsService.InsertLogError("SystemError", "GenerateNumberOfBrandsByChannel =>" + e.Message, string.Empty);
                var error = new ValidationResult();
                error.Errors.Add(new ValidationFailure("SystemError", "GenerateNumberOfBrandsByChannel =>" + e.Message));
                return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(error)));
            }
        }

        [HttpGet("getsearchfiltercompanies")]
        public async Task<ActionResult> GetSearchFilterCompanies(string filtersearchid)
        {
            try
            {
                var searchFilderCompaniesItems = await _analyticsService.GetSearchFilterCompanies(filtersearchid);

                return new JsonResult(searchFilderCompaniesItems);
            }
            catch (Exception e)
            {
                await _analyticsService.InsertLogError("SystemError", "GetSearchFilterCompanies =>" + e.Message, filtersearchid);
                var error = new ValidationResult();
                error.Errors.Add(new ValidationFailure("SystemError", "GetSearchFilterCompanies =>" + e.Message));
                return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(error)));
            }

        }

        [HttpPost]
        [Route("generatenumberofcompaniesbychannel")]
        public async Task<ActionResult> GenerateNumberOfCompaniesByChannel(NumberOfCompaniesByChannelModel model)
        {
            try
            {
                var validator = new NumberOfCompaniesByChannelValidator();
                var result = validator.Validate(model);

                if (!result.IsValid)
                {
                    return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(result)));
                }

                var returnResult = await _analyticsService.GenerateNumberOfCompaniesByChannel(model);

                return new JsonResult(returnResult);
            }
            catch (Exception e)
            {
                await _analyticsService.InsertLogError("SystemError", "GenerateNumberOfCompaniesByChannel =>" + e.Message, string.Empty);
                var error = new ValidationResult();
                error.Errors.Add(new ValidationFailure("SystemError", "GenerateNumberOfCompaniesByChannel =>" + e.Message));
                return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(error)));
            }

        }

        [HttpGet("getsearchfilterindustries")]
        public async Task<ActionResult> GetSearchFilterIndustries(string filtersearchid)
        {
            try
            {
                var searchFilderIndustriesItems = await _analyticsService.GetSearchFilterIndustries(filtersearchid);

                return new JsonResult(searchFilderIndustriesItems);
            }
            catch (Exception e)
            {
                await _analyticsService.InsertLogError("SystemError", "GetSearchFilterIndustries =>" + e.Message, filtersearchid);
                var error = new ValidationResult();
                error.Errors.Add(new ValidationFailure("SystemError", "GetSearchFilterIndustries =>" + e.Message));
                return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(error)));
            }

        }

        [HttpPost]
        [Route("generatenumberofindustriesbychannel")]
        public async Task<ActionResult> GenerateNumberOfIndustriesByChannel(NumberOfIndustriesByChannelModel model)
        {
            try
            {
                var validator = new NumberOfIndustriesByChannelValidator();
                var result = validator.Validate(model);

                if (!result.IsValid)
                {
                    return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(result)));
                }

                var returnResult = await _analyticsService.GenerateNumberOfIndustriesByChannel(model);

                return new JsonResult(returnResult);
            }
            catch (Exception e)
            {
                await _analyticsService.InsertLogError("SystemError", "GenerateNumberOfIndustriesByChannel =>" + e.Message, string.Empty);
                var error = new ValidationResult();
                error.Errors.Add(new ValidationFailure("SystemError", "GenerateNumberOfIndustriesByChannel =>" + e.Message));
                return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(error)));
            }
        }

        #endregion

        #region SocialNetwork Report

        [HttpPost]
        [Route("generatesocialnetworkreach")]
        public async Task<ActionResult> GenerateSocialNetworkReach(SocialNetworkReachModel model)
        {
            try
            {
                var validator = new SocialNetworkReachValidator();
                var result = validator.Validate(model);

                if (!result.IsValid)
                {
                    return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(result)));
                }
                var socialNetworkReaches = await _analyticsService.GenerateSocialNetworkReach(model);

                return new JsonResult(socialNetworkReaches);
            }
            catch (Exception e)
            {
                await _analyticsService.InsertLogError("SystemError", "GenerateSocialNetworkReach =>" + e.Message, model.UserId);
                var error = new ValidationResult();
                error.Errors.Add(new ValidationFailure("SystemError", "GenerateSocialNetworkReach =>" + e.Message));
                return BadRequest(new JsonResult(ModalStateHelper.ModalStateException(error)));
            }

        }



        #endregion

        #endregion
    }
}
