London Stock API
======================
A robust, CQRS-driven, .NET Core Web API for the London Stock Exchange, supporting trade transactions, per-broker stock tracking,StockStats using ticker and resilient business logic. 
Built with MediatR, Entity Framework Core, centralized FluentValidation,standard logging and error handling.

Architecture Overview
=======================
1. Implement CQRS design pattern using MediatR: All write (commands) and read (queries) operations are handled via MediatR, enabling easy separation and will be scalable.
2. Implement Clean Architecture:
    a. LondonStockAPI :HTTP endpoints, Global Exception middleware
	b. Stocks.Application: Command,Queries,Validation,DTOs,Mapping
	c. Stocks.Infrastructure: Implement ORM using Entity Framework 
	d. Stocks.Domain :Broker, BrokerStockInfo,Trade, StockStats
3. Logging & Error Handling:
   a. Centralized logging of all requests, responses, and errors using ASP.NET Core’s built-in ILogger.
   b. Implement Global exception middleware to handle exceptions.
   
4. Per-broker inventory: Each broker maintains separate stock limits for each ticker, with all business rules enforced at the command level.
   

Getting Started
====================
1. Clone the repository
  git clone https://github.com/bidyutmandal-2781/LondonStockAPI.git
  cd LondonStockAPI

2. Configure SQL Server 
Edit appsettings.json and set your connection string:

"ConnectionStrings": {
  "Database": "Server=<ServerName>;Database=<DatabaseName>;Trusted_Connection=True;TrustServerCertificate=True"
}

3.Apply Migrations and Seed Data

Update-Database

Solution Structure
====================

a. LondonStockApi
/Controllers
/Exceptions
appsettings.json

b. Stocks.Application
/Dtos
/Mapper
/Stocks
  /Command
  /Query
/Validation

c. Stocks.Infrastructure
/Configurations
/Extensions
/Migrations

d. Stocks.Domain
/Entities

Usage Examples:
=====================
Base URL : https://localhost:7186 


1. Register a broker : POST /api/brokers
Body:
{ "id": "HDFC-DEMAT", "name": "HDFC Securities" }

2. Add stocks to a broker: POST /api/brokers/:brokerId/stocks

Path- Parameter: 
brokerId : HDFC-DEMAT

Body:
{
  "ticker": "HDFCBANK",
  "totalStocks": 11000
}

3. Get all broker with stock details : GET /api/brokers/with-stocks
4. Get stock details by broker id : GET /api/brokers/:brokerId/stocks
Path- Parameter: 
brokerId : HDFC-DEMAT

5. Perform Valid trade transactions : POST /api/trades

Body:
{
  "ticker": "HDFCBANK",
  "price": 1500,
  "shares": 250,
  "brokerId": "HDFC-DEMAT"
}
6. Get all trade transactions: GET /api/trades

7.Get All stock stats: GET /api/stocks
8. Get stock stats by Ticket : GET /api/stocks/:tickerId
Path- Parameter: 
tickerId : HDFCBANK

9. Get stock stats by ticker range: POST /api/stocks/range
Body:
["OTEX","TECHM"]






	
   
