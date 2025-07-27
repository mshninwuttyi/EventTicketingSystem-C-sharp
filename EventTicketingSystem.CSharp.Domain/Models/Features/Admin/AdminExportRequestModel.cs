namespace EventTicketingSystem.CSharp.Domain.Models.Features.Admin;

public class AdminExportRequestModel
{
    public string Format { get; set; }

    public List<AdminListModel> AdminList { get; set; }
}