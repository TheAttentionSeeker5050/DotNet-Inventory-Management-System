# Inventory Management System with ASP.NET

The purpose of this project is demostrating the proficiency in developing project using ASP.NET Core Web API's, Blazor Web Assembly Apps, Entity Framework and Azure Cloud deployment and maintenance. It consist of an inventory management system where the user (company staff) will be able to add, delete, modify and check status of company inventory. This project will continue growing and integrating even more features

## Technologies
- .Net API Controller apps
- Entity Framework
- Blazor
- CSS bootstrap and Icons
- Azure Cloud Services, Azure DB on deployment
- Some pipeline (TBD)
- Testing Libraries, and Cypress client testing

## Data configuration
Created 3 basic models:
- Product References: It references products with the same caracteristics, they belong to a category, and have different units
- Product categories: A group of different products that share simmilar caracteristics
- Product items: An individual instance of product reference. It represent an unit of product in stock. Other properties can involve location, and that can even be other table itself.

## Solution Components
- Inventorium.API: The backend server
- Inventorium.Web: The frontend, developed in Blazor Wasm
- Inventorium.Dtos: Data transfer object. Based on the data model instances above, the Dtos are an object that carries data between processes. The motivation for its use is that communication between processes is usually done resorting to remote interfaces (e.g., web services), where each call is an expensive operation.
- Inventorium.Models: We have the models into a separate class library, that is in case we need the data in the frontend exactly as it is in the model.


# Changelog

## 2024-07-06 
- Beginning of the project

## 2024-08-03
- Finished first part of the project
- User can add, explore and see details of product items, references, and categories
- Search functionality
- Added project profiles for development and production services
- Exported to linux server

### Future changes
- Edit and delete product items on frontend blazor pages, api endpoints already
finished
- Add more columns to the models
- Add statistics page
- Add better widgets for product item reference and product reference category. When categories and references grows in size then the form will get difficult to use. A better option can be an input field with autocomplete suggestions