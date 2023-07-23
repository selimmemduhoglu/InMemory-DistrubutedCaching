using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValueController : ControllerBase
    {
       readonly IMemoryCache _memoryCache;

        public ValueController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        //[HttpGet("set")]
        //public void SetName(string name)
        //{
        //    _memoryCache.Set("name", name);

        //}

        //[HttpGet("get")]
        //public string GetName()
        //{
        //    if (_memoryCache.TryGetValue<string>("name", out string name)) // bu şekilde kendimizi program run time da set edilmeden get edilmesine karşılık güvene almaktan dolayı kullanılmıştır.
        //    {
        //        return name.Substring(3);
        //    }
        //    return "false";
        //    //_memoryCache.Get("name"); 
        //    //return _memoryCache.Get<string>("name"); // bu şekilde kullanıldığında verilmiş olan değer string türünde geriye dönecektir.

        //}


        [HttpGet("setDate")]
        public void SetDate()
        {
            _memoryCache.Set<DateTime>("date", DateTime.Now, options : new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30), //en fazla 30 sn sonra cache temizlenir,
                SlidingExpiration = TimeSpan.FromSeconds(5) // 30 saniyeyi beklemeden eğer ki 5 sn sonra data get edilmezse yine silinir.
            });
        }

        [HttpGet("getDate")]
        public DateTime GetDate()   
        {
            return _memoryCache.Get<DateTime>("date");
        }


    }
}



// AddmemoryCache, servisini uygulamaya ekleyiniz.
// IMemoryCache, referansını inject ediniz.
// Set methoduyla veriyi cache leyebilir, Get methoduyla cache lenmiş veriyi elde edebilirsiniz.
// Remove fonsiyonuyla cache lenmiş veriyi silebilirsiniz.
// TryGetValue methoduyla kontrollü bir şekilde cache den veriiyi okuyabilirsiniz.




/*
 C#'da ASP.NET Core framework'ü içinde kullanılan controller sınıfları, MVC (Model-View-Controller) tasarım deseni için temel yapıları oluşturur. Bu controller sınıfları, ControllerBase veya Controller sınıfından türetilir. Her ikisi de controller oluşturmak için kullanılabilir, ancak aralarında bazı farklar vardır:
ControllerBase, Controller sınıfının daha hafif bir sürümüdür ve temel MVC işlevselliğine sahiptir. ASP.NET Core 2.1'den itibaren kullanıma sunulmuştur. Bu sınıf, eylemleri (actions) işlemek ve sonuçları (views, data vb.) geri döndürmek için temel mekanizmaları sağlar. ControllerBase, HTTP isteklerine yanıt vermek ve MVC yönergelerini yönetmek için yeterli özelliklere sahiptir. Özellikle API geliştirmek için kullanışlıdır.
Controller, ControllerBase'den türetilmiştir ve daha fazla MVC özelliği sağlar. Controller, ControllerBase'in üzerine ekstra özellikler ekler ve özellikle web uygulamalarında kullanım için daha kapsamlı bir deneyim sunar. Controller, özellikle Razor View Engine ile web sayfaları oluşturmak ve yönetmek için uygun bir seçenektir.
 Bir MVC uygulamasında, genellikle ControllerBase sınıfından türetilmiş controllerlar, API'ler oluşturmak veya sadece verileri JSON formatında döndürmek için kullanılırken, Controller sınıfından türetilmiş controllerlar, tam web uygulamaları oluşturmak ve Razor View Engine ile HTML sayfaları göstermek için kullanılır
 Dolayısıyla, seçim, uygulama ihtiyaçlarına ve kullanım senaryolarına bağlıdır. API geliştirmek veya sadece veri döndürmek için ControllerBase, tam web uygulamaları oluşturmak için ise Controller kullanmak uygun olacaktır.
 */


// Absolute Expiration : Cache deki datanın ne akdar tutulacağına dair net ömrünün belirtilmesidir. belirtilen ömür sona erdiğinde cache direkt olarak temizlenir. belirtilen ömür sona erdiğinde bu data temizlenecektir.

// Sliding Expiration : Cache'lenmiş datanın memory'de belirtilen süre periyodu zarfında tutulmasını belirtir. Belirtilen süre periyodu içerisinde cache'e yapılan erişim neticesinde  de datanın ömrü bir o kadar uzatılacaktır. Aksi taktirde belirtilen süre zarfında bir erişim söz konusu olmazsa cache temizlenecektir.

// ** Bir veriye ayrı ayrı oalrak Absulete ve Sliding Time verilebilir veyahutta 2 SÜRE DE VERİLEBİLİR.  **