# Market Intel - Search Financial Data


[![Open Source](https://badges.frapsoft.com/os/v1/open-source.svg?v=103)](https://opensource.org/)


## Description


This open-source project allows you to fetch detailed financial data for companies directly from Financial Modeling Prep (FMP). You can search for companies by name or symbol, making it easy to access the information you need.

## Features


- Flexible Search: Quickly find the company data you're looking for, either by its full name or ticker symbol.
- Financial Modeling Prep Integration: Leverage the wealth of financial data provided by FMP, a reliable and up-to-date source.
- Detailed Data: Gain access to a wide range of financial information, including financial statements, key metrics, ratios, and more.
- Built with .NET and SQL Server: The project utilizes robust and proven technologies to ensure reliable performance and efficient data management.

## Installation


To run this project, you need to have the .NET SDK installed on your machine. You can download it from the official [.NET website](https://dotnet.microsoft.com/download).

To set up the project, make sure you have a SQL Server database available and configure the connection string in the application configuration file.

1. Clone the Repository:

  ```bash

  git clone https://github.com/JuanJSalzar/MarketIntel.git

  cd ./MarketIntel/
  
  cd ./Backend/

  ```

2. Configure Database Connection:


    Open the appsettings.json file and update the connection string (DefaultConnection) with your SQL Server credentials.


3. Get Your FMP API Key:


    Sign up for Financial Modeling Prep to get your free API key.
    Add your API key to the appsettings.json file.

Run the Project
    Run the following commands to build and run the project:

    dotnet buil

    dotnet run
## Contribution


Contributions are welcome. If you wish to contribute, please follow these steps:


1. Fork the repository.

2. Create a new branch (`git checkout -b feature/new-feature`).

3. Make your changes and commit them (`git commit -am 'Add new feature'`).

4. Push your branch (`git push origin feature/new-feature`).

5. Create a new Pull Request.


## License


This project is open source


## Authors and Credits


- **Juan J Salzar** - *Lead Developer*


## Contact


For questions or comments, please contact via [GitHub](https://github.com/JuanJSalzar).