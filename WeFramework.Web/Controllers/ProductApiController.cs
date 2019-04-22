using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WeFramework.Core.Domain.Products;
using WeFramework.Core.Paging;
using WeFramework.Service.Products;
using WeFramework.Web.Core.Security;
using WeFramework.Web.Models.Products;

namespace WeFramework.Web.Controllers
{
    public class ProductApiController : ApiController
    {
        private readonly IProductService productService;

        private readonly IMapper mapper;

        private readonly IConfigurationProvider mapperConfigProvider;

        public ProductApiController(IProductService productService, IMapper mapper, IConfigurationProvider mapperConfigProvider)
        {
            this.productService = productService;
            this.mapper = mapper;
            this.mapperConfigProvider = mapperConfigProvider;
        }

        // GET: api/ProductApi
        public IQueryable<ProductModel> GetProducts()
        {
            return productService.GetProducts().AsQueryable().ProjectTo<ProductModel>(mapperConfigProvider);
        }

        // GET: api/ProductApi/5
        [ResponseType(typeof(ProductModel))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = productService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<Product, ProductModel>(product));
        }

        // PUT: api/ProductApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.ID)
            {
                return BadRequest();
            }

            Product product = productService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            mapper.Map(model, product);

            productService.UpdateProduct(product);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ProductApi
        [ResponseType(typeof(ProductModel))]
        public IHttpActionResult PostProduct(ProductModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product product = mapper.Map<ProductModel, Product>(model);
            productService.CreteProduct(product);

            return CreatedAtRoute("DefaultApi", new { id = model.ID }, model);
        }

        // DELETE: api/ProductApi/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = productService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            productService.DeleteProduct(product);

            return Ok(mapper.Map<ProductModel>(product));
        }
    }
}
