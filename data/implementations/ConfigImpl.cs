
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

    public async Task<List<CategoryDto>> getAllCategories()
    {
        List<CategoryDto> _result = new List<CategoryDto>();
        IEnumerable<XElement> op = _el.Descendants("Category");
        await Task.Run(() =>
                   {
                       foreach (XElement s in op)
                       {

                           if (s.Element("Description").Value != null) {
                           _result.Add(new CategoryDto(
                            
                           ));


                            // _result.Add(s.Element("Description").Value);
                             
                             
                             
                             
                             
                             
                              }
                       }
                   }
        );
return _result;
}

    public Task<List<CategoryDto>> getAllowedCategories(string[] categoryIds)
    {
        throw new NotImplementedException();
    }

    Task<List<CategoryDto>> IConfig.getAllCategories()
    {
        throw new NotImplementedException();
    }
}
