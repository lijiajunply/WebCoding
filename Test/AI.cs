using System.Net.Http.Json;

namespace Test;

public class AI
{
    public static async void Test()
    {
        
    }
}

public class AIModel
{
    public string Model { get; set; } = "gpt-3.5-turbo";
    public List<Message> Messages { get; set; } = [];
    public bool Stream { get; set; } = true;
}

public class Message
{
    public string Role { get; set; } = "user";
    public string Content { get; set; } = "";
}