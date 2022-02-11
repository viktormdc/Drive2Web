using System.Threading.Tasks;

namespace Analytics.Service.Interface.ScheduleTask
{
    public interface IScheduleTaskService
    {
        Task ProcessingFacebookApi();
        Task ProcessingInstagramApi();
        Task ProcessingTwiterApi();
        Task ProcessingAdPointer();
        Task ProcessingLinkedInApi();
        Task ProcessingGoogleApi();
        Task ProcessingYoutubeApi();

    }
}
