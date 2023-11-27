using System.Text.Json.Serialization;

namespace HPH.ParkRunChamps.Cli; 

public class HphParkRunChampsConfiguration {
    public string MembersListSheetId { get; set; } = string.Empty;
    public string ParkRunChampsSheetId { get; set; } = string.Empty;
    
    [JsonPropertyName("installed")]
    public GSheetsConfig Installed { get; set; }
}

public class GSheetsConfig {
    [JsonPropertyName("client_id")]
    public string ClientId { get; set; }
    
    [JsonPropertyName("project_id")]
    public string ProjectId { get; set; }
    
    [JsonPropertyName("auth_uri")]
    public Uri AuthUri { get; set; }
    
    [JsonPropertyName("token_uri")]
    public Uri TokenUri { get; set; }
    
    [JsonPropertyName("auth_provider_x509_cert_url")]
    public string AuthProviderX509CertUrl { get; set; }
    
    [JsonPropertyName("client_secret")]
    public string ClientSecret { get; set; }
    
    [JsonPropertyName("redirect_uris")]
    public Uri[] RedirectUris { get; set; }
}