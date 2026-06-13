namespace RecruitProApp.Application.Common.Interfaces
{
    public interface IEmailCustomService
    {
        Task SendAsync(string to, string subject, string body);
    }
}
