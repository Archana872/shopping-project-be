using WebApplication1.DataModel;
using WebApplication1.RepositryLayer;

namespace WebApplication1.BusinessLogic;

public class ProductService
{
    private readonly ProductRepository _repository;

    public ProductService(ProductRepository repository)
    {
        _repository = repository;
    }

    public string? CreateProduct(CreateProductRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return "Product name is required.";
        }

        if (request.Price <= 0)
        {
            return "Price must be greater than 0.";
        }

        if (request.Stock < 0)
        {
            return "Stock cannot be negative.";
        }

        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock
        };

        _repository.AddProduct(product);
        return null;
    }

    public List<Product> GetAllProducts()
    {
        return _repository.GetAllProducts();
    }

    public Product? GetProductById(int id)
    {
        if (id <= 0)
        {
            return null;
        }

        return _repository.GetProductById(id);
    }

    public string? UpdateProduct(int id, UpdateProductRequest request)
    {
        if (id <= 0)
        {
            return "Invalid product id.";
        }

        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return "Product name is required.";
        }

        if (request.Price <= 0)
        {
            return "Price must be greater than 0.";
        }

        if (request.Stock < 0)
        {
            return "Stock cannot be negative.";
        }

        var existingProduct = _repository.GetProductById(id);
        if (existingProduct is null)
        {
            return "Product not found.";
        }

        var product = new Product
        {
            Id = id,
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock
        };

        _repository.UpdateProduct(product);
        return null;
    }

    public string? DeleteProduct(int id)
    {
        if (id <= 0)
        {
            return "Invalid product id.";
        }

        var existingProduct = _repository.GetProductById(id);
        if (existingProduct is null)
        {
            return "Product not found.";
        }

        _repository.DeleteProduct(id);
        return null;
    }
}
