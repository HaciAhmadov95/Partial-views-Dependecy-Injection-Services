using Fiorella.Data;
using Fiorella.Models;
using Fiorella.Services.Interface;
using Fiorella.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public HomeController(AppDbContext context,
                             IProductService productService,
                             ICategoryService categoryService)
        {
            _context = context;
            _productService = productService;
            _categoryService = categoryService;
        }


        public async Task<IActionResult> Index()
        {
            List<Slider> slider = await _context.Sliders.ToListAsync();
            SliderInfo sliderInfo = await _context.SlidersInfo.FirstOrDefaultAsync();
            List<Category> categories = await _categoryService.GetCategoriesAsync();
            List<Product> products = await _productService.GetAllAsync();
            List<Surprise> surprises = await _context.Surprise.ToListAsync();
            List<SurpriseList> surpriseLists = await _context.SurpriseLists.ToListAsync();
            List<Expert> experts = await _context.Experts.Include(m => m.Positions).ToListAsync();
            List<Blog> blogs = await _context.Blogs.Where(m => !m.SoftDeleted).ToListAsync();


            HomeVM model = new()
            {
                Sliders = slider,
                SliderInfo = sliderInfo,
                Categories = categories,
                Products = products,
                Surprises = surprises,
                SurpriseLists = surpriseLists,
                Experts = experts,
                Blogs = blogs

            };

            return View(model);
        }
    }
}