# SnappFood! Technology Department - Online Store API

This project is a simple implementation of an online store application with a focus on clean code and simplicity. 
The API provides endpoints to manage products, orders, and users without the need for an authentication system.

## Entities

### Product
- Id
- Title
- InventoryCount
- Price
- Discount (percentage)

### User
- Id
- Name
- Orders

### Order
- Id
- Product
- CreationDate
- Buyer
- UpdatedDate

## Requirements

The following endpoints need to be implemented as REST APIs:

1. **Add a Product:**
   - Endpoint: POST /api/products
   - Description: Adds a new product with a predefined `InventoryCount`. The product's title must be less than 40 characters, and its title must be unique.

2. **Update InventoryCount:**
   - Endpoint: PUT /api/products/{id}/inventory
   - Description: Increases the `InventoryCount` of a product and updates the product.

3. **Get Product by ID:**
   - Endpoint: GET /api/products/{id}
   - Description: Retrieves a product by ID, considering the current discount value to provide proper pricing.

4. **Buy Product:**
   - Endpoint: POST /api/products/{id}/buy
   - Description: Updates the inventory count of a product and adds an order to the user's orders list.

5. **Unit Tests:**
   - Description: Write unit tests for two use cases of the application.

### Side Notes

- Use `ef core` as the ORM (code first).
- Implement a simple caching procedure (like in-memory cache) for the "Get a product by ID" endpoint to reduce database hits.
- There is no need for an authentication system.
- Both simplicity and cleanliness of code are essential.
- Add some sample users without any orders for initial testing.

## Getting Started

1. Make sure you have .NET Core SDK installed.
2. Clone this repository.
3. Update the database connection string in the `appsettings.json` file.
4. Run `dotnet ef database update` to apply the migrations and create the database.
5. Run the application using `dotnet run`.

## API Documentation

The API documentation will be available at `/swagger` endpoint when the application is running.

## Testing

You can use tools like Postman or cURL to test the API endpoints. Unit tests are also available in the `Tests` folder.

## Dependencies

The project uses EF Core, ASP.NET Core, and other standard C# libraries.

## Contributions

Contributions are welcome. Feel free to fork the repository and submit pull requests.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.