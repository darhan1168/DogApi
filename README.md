# Dog API

Dog API is a RESTful web service designed to manage dog-related data. It allows users to perform various operations related to dogs, such as adding a new dog, retrieving dog details, and sorting dogs based on specific attributes. This API is built using .NET Core and follows the REST architectural style.

## Table of Contents
- [Features](#features)
- [Technologies Used](#technologies-used)

## Features
- **Add Dog**: Add a new dog to the database with details like name, color, tail length, and weight.
- **Get Dogs**: Retrieve a list of dogs with optional sorting based on attributes like tail length and weight.
- **Pagination**: Implement pagination to limit the number of dogs returned per request.
- **Error Handling**: Custom error handling for various scenarios, including validation errors and internal server errors.
- **Unit Tests**: Ensure the reliability and correctness of API functionality with comprehensive unit tests.
- **Request Limiting**: Enforce request rate limiting to handle a limited number of incoming requests per second, preventing overload and ensuring system stability. 

## Technologies Used
- **.NET Core**: The API is built using .NET Core, a cross-platform framework for building modern, cloud-based, and internet-connected applications.
- **Entity Framework Core**: Entity Framework Core is used as the Object-Relational Mapping (ORM) tool for interacting with the MS SQL database.
- **Web API**: Web API is utilized for building the RESTful API endpoints and handling HTTP requests in a flexible and scalable manner.
- **X.PagedList**: X.PagedList is utilized for implementing pagination in the API responses.
- **xUnitTest**: xUnitTest is used for writing unit tests to ensure the reliability and correctness of the API functionality.
- **Moq**: Moq is used for creating mock objects for unit testing.

