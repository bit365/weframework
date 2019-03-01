using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeFramework.Core.Data;
using WeFramework.Core.Domain.Products;
using WeFramework.Core.Paging;

namespace WeFramework.Service.Products
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> productRepository;

        public ProductService(IRepository<Product> userRepository)
        {
            this.productRepository = userRepository;
        }

        public void CreteProduct(Product product)
        {
            this.productRepository.Insert(product);
        }

        public void DeleteProduct(Product product)
        {
            this.productRepository.Update(product);
        }

        public Product GetProduct(int id)
        {
            return this.productRepository.GetById(id);
        }

        public IEnumerable<Product> GetProducts()
        {
            return productRepository.Table;
        }

        public IPagedList<Product> GetProducts(string keyword, int pageNumber, int pageSize)
        {
            var products = productRepository.Table;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                products = products.Where(p=>p.Name.Contains(keyword));
            }

            return products.ToPagedList(m=>m.ID,pageNumber,pageSize);
        }

        public void UpdateProduct(Product product)
        {
            productRepository.Update(product);
        }
    }
}
