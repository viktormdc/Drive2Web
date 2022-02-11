using Analytics.Service.Interface.ScheduleTask;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleTask.Microservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleTaskController : ControllerBase
    {
        private readonly IScheduleTaskService _scheduleTaskService;

        public ScheduleTaskController(IScheduleTaskService scheduleTaskService)
        {
            this._scheduleTaskService = scheduleTaskService;
        }

        [HttpGet("scheduletask-facebook")]
        public async Task<ActionResult> GetFacebookTask()
        {
           await this._scheduleTaskService.ProcessingFacebookApi();
            return Ok(true);
        }
        [HttpGet("scheduletask-instagram")]
        public async Task<ActionResult> GetInstagramTask()
        {
            await this._scheduleTaskService.ProcessingInstagramApi();
            ///dlkadlsa
            return Ok(true);
        }

        [HttpGet("scheduletask-twitter")]
        public async Task<ActionResult> GetTwitterTask()
        {
            await this._scheduleTaskService.ProcessingTwiterApi();
            return Ok(true);
        }

        [HttpGet("scheduletask-linkedin")]
        public async Task<ActionResult> GetLinkedinTask()
        {
            await this._scheduleTaskService.ProcessingLinkedInApi();
            return Ok(true);
        }

        [HttpGet("scheduletask-adpointer")]
        public async Task<ActionResult> GetAdPointerTask()
        {
            await this._scheduleTaskService.ProcessingAdPointer();
            return Ok(true);
        }

        [HttpGet("scheduletask-google")]
        public async Task<ActionResult> GetGoogleTask()
        {
            await this._scheduleTaskService.ProcessingGoogleApi();
            return Ok(true);
        }

        [HttpGet("scheduletask-youtube")]
        public async Task<ActionResult> GetYoutubeTask()
        {
            await this._scheduleTaskService.ProcessingYoutubeApi();
            return Ok(true);
        }


    }
}
