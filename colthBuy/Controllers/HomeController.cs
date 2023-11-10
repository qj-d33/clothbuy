using colthBuy.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace colthBuy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //賣家文件取得地方，IWebHostEnvironment 是 ASP.NET Core 中的介面
        private readonly IWebHostEnvironment _hostEnvironment;

        //當控制器初始化時，hostingEnvironment 就會被注入進來，之後你就可以在控制器的方法中使用 _hostingEnvironment 來取得 Web 主機環境的相關資訊。
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult seller()
        {
            return View();
        }
        public IActionResult upper()
        {
            return View();
        }
        public IActionResult lower()
        {
            return View();
        }
        public IActionResult coat()
        {
            return View();
        }


        //_hostEnvironment 是通過注入 IWebHostEnvironment 服務而獲得的，用於獲取 wwwroot 文件夾的绝對路徑
        [HttpPost]
        public async Task<IActionResult>Upload(IFormFile fileUpload)//使用異步方法執行
        {
            if(fileUpload != null&& fileUpload.Length>0)
            {
                //Guid.NewGuid() 是 .NET 框架中的一個方法，用於生成全局唯一標識符（GUID）
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileUpload.FileName;
                var filePath=Path.Combine(_hostEnvironment.WebRootPath,"images",uniqueFileName);
                //FileStream為文件流，可以將文件以異步方式創建並寫入磁碟;FileMode.Create表示以創建模式打開文件，如果文件已存在，則會被覆蓋；如果文件不存在，則會創建新文件
                using (var stream =new FileStream(filePath,FileMode.Create))
                {
                    await fileUpload.CopyToAsync(stream);//該方法異步地從上傳的文件中複製内容到给定的流對象
                }
                //處理上傳完成後的操作
                return RedirectToAction("Coat");
            }
            return View();//如果上傳失敗就會回到原本coat頁面
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}