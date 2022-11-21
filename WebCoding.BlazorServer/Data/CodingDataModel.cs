using WebCoding.BlazorServer.Model;
using System.ComponentModel.DataAnnotations;
namespace WebCoding.BlazorServer.Data;

public class CodingDataModel
{
    [Required(ErrorMessage = "不能为空")]
    [Display(Name ="Code")]
    public string _code { get; set; }
    
    [Display(Name ="Lang")]
    public Lang _lang { get; set; }

    public string RUN()
    {
        var a = new DebugModel(_lang, _code);
        return a.RunCode();
    }
}