using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeFramework.Core.Domain.Products;
using WeFramework.Core.Paging;
using WeFramework.Service.Products;
using WeFramework.Web.Core.Mvc;
using WeFramework.Web.Core.Security;
using WeFramework.Web.Infrastructure;
using WeFramework.Web.Models.Products;
using AutoMapper.Execution;

namespace WeFramework.Web.Controllers
{
    [ActionAuthorize]
    public class ProductController : BaseController
    {
        private readonly IProductService productService;

        private readonly IMapper mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        public ActionResult Index(string keyword, int page = 1)
        {
            IPagedList<Product> products = productService.GetProducts(keyword, page, 15);

            var productModels = mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(products);

            var viewModel = new StaticPagedList<ProductModel>(productModels, products.GetMetaData());

            return View(viewModel);

            //return Request.IsAjaxRequest() ? (ActionResult)PartialView("ProductListPartial", viewModel) : View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ModelStateValidFilter]
        public ActionResult Create(ProductModel model)
        {
            Product product = mapper.Map<ProductModel, Product>(model);

            productService.CreteProduct(product);

            return RedirectToAction(nameof(Index));
        }
    }
}