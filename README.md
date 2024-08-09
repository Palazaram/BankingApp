# BankingApp

## About the Application

This is a web application developed using ASP.NET Core Web API, implementing the Onion architecture.

### BankingApp.Core

This project contains all the core entities of the application and interfaces that define contracts for data interaction.

### BankingApp.Persistence

This project includes repositories that implement interfaces from `OutOfOffice.Core`.

### BankingApp.Application

This project houses application services that utilize repository methods to execute business logic.

### BankingApp

The main project that integrates all the above components.

## Unit Testing

Unit tests have been conducted for the project using the xUnit testing framework to ensure the correctness and reliability of the application's functionality.

### Test Coverage

The following components have been thoroughly tested:

  - **Controllers**: Validations, HTTP responses, and exception handling;
  
  - **Services**: Business logic, data manipulation, and integration with repositories;
  
  - **Models**: Data structures, validation logic, and data consistency.

## Running the Application

  - Set `BankingApp` as the default project;
  
  - Use the Rebuild Solution command to ensure all components are correctly compiled;
  
  - Start the application.
