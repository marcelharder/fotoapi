
using System.Xml.Linq;

namespace api.data.implementations;

public class ConfigImpl : IConfig
{

    private XElement _el;
    private IWebHostEnvironment _env;

    public ConfigImpl(IWebHostEnvironment env)
    {
        _env = env;

        var content = _env.ContentRootPath;
        var filename = "data/xml/categories.xml";
        var test = Path.Combine(content, filename);
        XElement el = XElement.Load($"{test}");
        _el = el;
    }

    public async Task<List<string>> getCategories()
    {
        List<string> _result = new List<string>();
        IEnumerable<XElement> op = _el.Descendants("Category");
        await Task.Run(() =>
                   {
                       foreach (XElement s in op)
                       {
                           if (s.Element("Description").Value != null) { _result.Add(s.Element("Description").Value); }
                       }
                   }
        );
return _result;
}
}
