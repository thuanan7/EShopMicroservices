﻿namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Catagory, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid ProductId);

    public class CreateProductCommandHandler(IDocumentSession session)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // create product entity from command object
            var product = new Product
            {
                Name = command.Name,
                Catagory = command.Catagory,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            // TODO: save product entity to database
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            // return CreateProductResult with product id
            return new CreateProductResult(product.Id);
        }
    }
}
