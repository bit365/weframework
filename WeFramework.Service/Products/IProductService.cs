using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeFramework.Core.Domain.Products;
using WeFramework.Core.Paging;

namespace WeFramework.Service.Products
{
    public interface IProductService
    {
        void CreteProduct(Product product);

        void UpdateProduct(Product product);

        Product GetProduct(int id);

        void DeleteProduct(Product product);

        IEnumerable<Product> GetProducts();

        IPagedList<Product> GetProducts(string keyword, int pageNumber, int pageSize);
    }
}
