AgriEnergy Connect
AgriEnergy Connect is a secure web application designed to connect farmers and employees for managing agricultural products. The system allows farmers to manage their product listings, while employees can manage farmer profiles and filter products. The app uses role-based authentication and ensures secure access to sensitive data.
Features
1. Database Development and Integration
- Relational database design for managing:
  - Farmers and their profiles
  - Agricultural products and related details
- Sample data to simulate real-world scenarios for demonstration.
2. User Role Definition and Authentication System
- Farmer Role:
  - Farmers can add products to their profile and view their own product listings.
- Employee Role:
  - Employees can add new farmer profiles.
  - Employees can view and filter products across all farmers, based on product type and date range.
- Secure role-based authentication implemented using ASP.NET Identity, ensuring access control.
3. Functional Features
For Farmers:
- Add new products with details such as:
  - Name
  - Category
  - Production date
- View personal product listings.
For Employees:
- Add new farmer profiles with essential details.
- View and filter products from any farmer based on:
  - Date range
  - Product type
4. User Interface Design and Usability
- Responsive design ensuring accessibility on desktops, tablets, and smartphones.
- Intuitive navigation and clear layout for better user experience.
- Accurate data presentation to prevent ambiguity or errors.
5. Data Accuracy and Validation
- Data validation to ensure consistency and accuracy.
- Error-handling mechanisms to prevent system crashes and data corruption.
Technologies Used
- ASP.NET Core MVC
- Entity Framework Core
- ASP.NET Identity
- CSS
- SQL Server 
Getting Started
Prerequisites
- Visual Studio 
- SQL Server
- .NET Core SDK (8.0 or later)
Installation Steps
1. Clone the repository:
   https://github.com/MveloK/AgriEnergy-Connect.git

2. Navigate to the project directory:
   cd AgriEnergy-Connect

3. Restore NuGet packages:
   dotnet restore

4. Update appsettings.json with your DB connection string.

5. Run migrations:
   dotnet ef database update

6. Run the application:
   dotnet run

7. Visit http://localhost:5000 in your browser.
Authentication
The application supports two roles:
- Farmer: Can manage their own product listings.
- Employee: Can manage farmer profiles and filter products.

Test Credentials:
- Employee:
  - Username: Alice Johnson and email : alice.johnson@example.com
  - Password: Password123!
- Farmer:
  - Username: Bob Smith and email : bob.smith@example.com
  - Password: SecurePass1!
Database Schema
- Farmers: Name, Email, etc.
- Products: Name, Category, Production Date, FarmerId
- Users: Managed by ASP.NET Identity
- Roles: Farmer, Employee (ASP.NET Identity roles)
Contributions
Contributions are welcome! Fork the repository, create a feature branch, and submit a pull request.
License
This project is licensed under the MIT License. See the LICENSE file for details.
Contact
If you have any questions or need help, reach out at: ST10076452@rcconnect.edu.za
This readme file was formatted by ChatGPT
