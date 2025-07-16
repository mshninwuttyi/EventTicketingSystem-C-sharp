namespace EventTicketingSystem.CSharp.Shared.Constants;

public class Queries
{
    public static string sp_sequencecode { get; } = "SELECT sp_sequencecode(@id)";
}