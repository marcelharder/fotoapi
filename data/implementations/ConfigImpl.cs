
using System.Xml.Linq;

namespace api.data.implementations;

public class ConfigImpl : IConfig
{

    private XElement _el;
    private IWebHostEnvironment _env;
    private UserManager<AppUser> _userManager;
    private IHttpContextAccessor _ht;

    public ConfigImpl(IWebHostEnvironment env, IHttpContextAccessor ht, UserManager<AppUser> userManager)
    {
        _env = env;
        _ht = ht;
        _userManager = userManager;

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
                           if (s.Element("Description").Value != null)
                           {
                               var help = new CategoryDto();
                               help.Id = Convert.ToInt32(s.Element("ID").Value);
                               help.Description = s.Element("Description").Value;
                               help.MainPhoto = s.Element("MainPhoto").Value;
                               _result.Add(help);
                           }
                       }
                   }
        );
        return _result;
    }

    public async Task<List<CategoryDto>> getAllowedCategories()
    {
        List<CategoryDto> _result = new List<CategoryDto>();
        // get the user that is loggedIn
        var loggedinUser = await _userManager.FindByNameAsync(_ht.HttpContext.User.Identity.Name);
        
        if(loggedinUser.AllowedToSee != null){
        
        
        List<int> catArray = loggedinUser.AllowedToSee.Split(',')
         .Select(t => int.Parse(t))
         .ToList();
        IEnumerable<XElement> op = _el.Descendants("Category");
        await Task.Run(() =>
                   {
                       foreach (XElement s in op)
                       {
                           if (s.Element("Description").Value != null)
                           {
                               var id = Convert.ToInt32(s.Element("ID").Value);
                               if (catArray.Contains(id))
                               {
                                   var help = new CategoryDto();
                                   help.Id = id;
                                   help.Description = s.Element("Description").Value;
                                   help.MainPhoto = s.Element("MainPhoto").Value;
                                   _result.Add(help);
                               }
                           }
                       }
                   }
        );
        }


        return _result;
    }

    public async Task<string> getDescriptionFromCategory(int category)
    {
        var result = "";
         IEnumerable<XElement> op = _el.Descendants("Category");
         await Task.Run(() =>
                   {


                      
                      var selectedElement = op.FirstOrDefault(t => t.Element("ID").Value == category.ToString());
                      result = selectedElement.Element("Description").Value;
                       

                   }
        );
        return result;
    }
}
