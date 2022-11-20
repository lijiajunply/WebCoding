using WebCoding.BlazorServer.Model;
using System.ComponentModel.DataAnnotations;
namespace WebCoding.BlazorServer.Data;

public class CodingDataModel
{
    public string _code { get; set; }
    public Lang _lang { get; set; }

    public string RUN()
    {
        var a = new DebugModel(_lang, _code);
        return a.RunCode();
    }
}