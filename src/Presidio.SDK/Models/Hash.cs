namespace Presidio.Models;

public class Hash : IAnonymizer
{
    public string Type => "hash";

    public string HashType { get; set; } = "sha256"; // sha256 or sha512
}