# Movie Entity-Framework Database & API
The project consists of two parts:
1) The Entity-Framework code-first database migration.
2) A Web API of CRUD services with GUI and documentation through Swagger.

## Entity-Framework
Entity-Framework is used to create a code-first database migration through DbContext implementation.
Using DbSets to create tables for Franchise, Movie and Character with appropriate columns, including Primary and Foreign Keys.
Franchise, Movie and Character data is seeded on initial migration.

## Web API
The Web API implements several services with CRUD functionality for use in database interactions.
The services are implemented through interface abstraction for increased flexibility of future extensions.
AutoMapper is used to map data to and from Data Transfer Objects (DTO), so as to limit the use and exposure of nonessential data.
The API is documented and made easy to utilize through Swagger.

The services functionality includes:
1) Creating, Reading, Updating and Deleting all or specific Franchises, Movies and Characters.
2) Updating relationships/foreign keys between Franchises, Movies and Characters.
