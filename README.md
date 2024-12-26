# Products API
This is a simple REST API built with ASP.NET Core 8 for managing "Products." The API supports basic CRUD operations (Create, Read, Update, Delete) and uses Entity Framework Core for data access with an SQLite database.

## API Endpoints
- **GET /api/products**: Get all products.
- **GET /api/products/{id}**: Get a product by ID.
- **POST /api/products**: Add a new product.
- **PUT /api/products/{id}**: Update a product by ID.
- **DELETE /api/products/{id}**: Delete a product by ID.

---

## Steps to Run
   Ensure the connection string in `appsettings.json` is set:
       "ConnectionStrings": {
         "DefaultConnection": "YOUR_DB_CONNECTION_STRING"
       }

   1. Clone the Repository
      git clone https://github.com/shashi-kant95/ShgardiProductAPI.git
      cd ProductAPI
   2. Install Dependencies
      dotnet restore
   3. Apply Migrations
      dotnet ef migrations add InitialCreate
      dotnet ef database update
   4. Run the Application
      dotnet run
   The API will be available at `http://localhost:5057/swagger/index.html


