Student Name: Adam Ahmed Serag

Student ID: 211014233

Course: Web Engineering Graduation Project

🚀 How to Run the Project
Follow these steps to set up the environment and run the API:

Open the Solution: Open the .sln file in Visual Studio 2022.

Restore Packages: Visual Studio should automatically restore the NuGet packages (JWT Bearer, EF Core, Hangfire).

Setup the Database:

Open the Package Manager Console (Tools > NuGet Package Manager > Package Manager Console).

Run the command: Update-Database

Note: This will create the AdamAhmedWebDb on your local SQL server.

Run the App: Press F5 or the Start button.

Access Swagger: The documentation will open at http://localhost:5075/swagger.

🛠️ Technologies Used
ASP.NET Core Web API: The primary framework for building the RESTful services.

Entity Framework Core (EF Core): Used as the ORM to manage database operations asynchronously.

SQL Server (LocalDB): The database engine used for persistent storage.

JWT (JSON Web Tokens): Implemented for secure, stateless authentication and Role-Based Authorization.

Hangfire: A background job orchestrator used for the Cron Job requirements (monitoring system health).

LINQ (Language Integrated Query): Used for data optimization, specifically utilizing .Select() projections and .AsNoTracking() for high-performance read-only queries.

Swagger (OpenAPI): Provides the interactive documentation and testing interface for all endpoints.

🔒 Security: HTTP-only Cookies Standard
Why are HTTP-only cookies an industry standard for authentication security?

In modern web development, while JWTs can be stored in LocalStorage, HTTP-only cookies are the preferred industry standard because:

XSS Protection: Cookies marked as HttpOnly cannot be accessed by client-side JavaScript. This prevents malicious scripts (Cross-Site Scripting) from stealing a user's session token.

Reduced Attack Surface: By keeping the token out of the reachable browser memory, the risk of "token sniffing" by third-party libraries or browser extensions is significantly reduced.

📸 API Documentation \& Screenshots
All endpoints are fully documented and testable via the Swagger UI.

Authentication: /api/Auth/login and /api/Auth/refresh

Patients: Full CRUD with 1:1 and 1:N relationships.

Prescriptions: Complex Many-to-Many logic.

Hangfire Dashboard: Accessible at /hangfire.

(Screenshots of working endpoints are included in the /Screenshots folder of this submission).

