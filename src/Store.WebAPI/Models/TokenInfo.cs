namespace Store.WebAPI.Models;

public class TokenInfo
{
    public required string Token { get; set; }
    public required DateTime ExpirationDateUtc { get; set; }
}
