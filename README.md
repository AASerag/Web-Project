Healthcare Management System API
Student Name: Adam Ahmed Serag

Student ID: 211014233

Course: Web Engineering

Institution: Faculty of Engineering

Project Overview
This project is a RESTful Web API built using ASP.NET Core designed to manage healthcare operations. It provides a secure and optimized backend for handling patient data, medical records, doctor assignments, and prescriptions. The system implements professional software patterns, including Layered Architecture, Dependency Injection, and Data Transfer Objects (DTOs), to ensure maintainability and scalability.

System Architecture and Design Patterns
The application follows a structured approach to separate concerns and ensure clean code:

Layered Architecture: Logic is divided into Controllers (API endpoints), Services (business logic), and Data (database context and entities).

Dependency Injection (DI): Interfaces and implementation classes are registered within the service container to decouple components and improve testability.

Data Transfer Objects (DTOs): Used to control data exposure and prevent over-posting. This ensures that internal database entities are never exposed directly to the client.

Repository/Service Pattern: All database interactions are abstracted through specialized service classes, ensuring the controllers remain thin and focused on HTTP concerns.

Database Design and Relationships
The database schema implements three primary types of relational mappings using Entity Framework Core:

One-to-One (1:1): Every Patient is linked to a unique MedicalRecord. This relationship ensures that personal identity data is separated from sensitive clinical history while maintaining a direct link via a foreign key.

One-to-Many (1:N): A Doctor can be assigned to multiple Patients, while each Patient is assigned to exactly one primary Doctor.

Many-to-Many (N:N): Implemented using a join table named Prescriptions. This allows multiple Medications to be associated with multiple Patients, tracking specific details like dosage and duration within the join entity.

Security and Authentication
The system implements a robust security layer to protect sensitive healthcare data:

JWT Bearer Authentication: Stateless authentication is handled via JSON Web Tokens.

Role-Based Authorization: Endpoints are restricted based on user roles.

Admin: Has full CRUD (Create, Read, Update, Delete) permissions.

User/Student: Limited to Read-Only access for specific endpoints.

Refresh Tokens: Implemented as an advanced feature to allow for secure session renewal without requiring the user to re-authenticate frequently.

Industry Standard Security (HTTP-only Cookies):

Technical Explanation: While this API utilizes JWTs in the Authorization header, the industry standard for web applications often involves storing tokens in HTTP-only cookies. This is a critical defense against Cross-Site Scripting (XSS). By marking a cookie as HttpOnly, the browser prevents any client-side JavaScript from accessing the token. This ensures that even if a malicious script is executed on the page, the authentication session cannot be stolen.

Performance Optimization Logic
The API utilizes Language Integrated Query (LINQ) best practices to ensure high performance under load:

AsNoTracking: Used for all read-only operations to bypass the Entity Framework change tracker, significantly reducing memory consumption and processing time.

Query Projection: Instead of fetching entire database entities, the .Select() method is used to retrieve only the specific fields required by the DTO. This minimizes the payload size and optimizes SQL execution.

Asynchronous Programming: All database and I/O operations utilize async and await to prevent thread-blocking and improve the responsiveness of the web server.

Background Task Management (Hangfire)
The project integrates Hangfire for managing background processing and scheduled tasks:

Cron Jobs: A recurring system health check is scheduled to run automatically at defined intervals.

Dashboard: A secure administrative dashboard is accessible at /hangfire, allowing real-time monitoring of job success, retries, and failures.

Setup and Installation Instructions
Prerequisites: Ensure .NET 7.0 (or higher) and SQL Server LocalDB are installed.

Clone Repository: Download or clone the source code into a local directory.

Database Initialization:

Open the Package Manager Console in Visual Studio.

Execute the command: Update-Database to generate the schema from existing migrations.

Configuration: Review appsettings.json to ensure the connection string points to your local SQL instance.

Execution: Run the project using the Start button or press F5.

Documentation: Navigate to http://localhost:5075/swagger to access the interactive API documentation.

AI Collaboration Disclosure
This project utilized Google Gemini for architectural guidance, security auditing, and documentation assistance. AI was specifically used to:

Validate Entity Framework Core relationship configurations.

Refine LINQ projection logic for DTO optimization.

Draft technical explanations for industry-standard security protocols.

Assist in troubleshooting DTO property mapping errors.
