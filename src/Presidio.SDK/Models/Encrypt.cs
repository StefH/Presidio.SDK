namespace Presidio.Models;

public class Encrypt : IAnonymizer
{
    public string Type => "encrypt";

    public string Key { get; set; }
}