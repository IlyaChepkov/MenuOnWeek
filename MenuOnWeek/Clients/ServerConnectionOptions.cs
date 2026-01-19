namespace MenuOnWeek.Clients;

internal sealed class ServerConnectionOptions
{
    public static string SectionKey { get; } = "ServerConnection";

    public Uri? Uri { get; set; }
}
