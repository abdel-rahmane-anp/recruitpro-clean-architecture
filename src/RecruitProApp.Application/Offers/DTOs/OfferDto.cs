namespace RecruitProApp.Application.Offers.DTOs
{
    public record OfferDto(
        Guid Id, 
        string Title, 
        string Description, 
        DateTime PublicationDate
    );
}
