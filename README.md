[![Build Status](https://dev.azure.com/joshmonreal/repository-pattern/_apis/build/status/josh-monreal.repository-pattern)](https://dev.azure.com/joshmonreal/repository-pattern/_build/latest?definitionId=5)

# Repository Pattern
This project is a proof-of-concept console application that showcases the repository pattern by using two persistence frameworks - **Entity Framework** and **Stored Procedures**. I have also used dependency injection to decouple the different application layers (i.e. Business and Data layers). The following are the technologies/frameworks that I used for this project.

- .NET Core 2.0 - Console application
- .NET Core 2.1 - Tests
- .NET Standard 2.0 - Class libraries
- Unity 5.8.6 - Dependency injection
- AutoMapper 7.0.1 - Object-object mapper
- Entity Framework Core 2.0.1 - ORM
- NUnit 3.10.1 - Unit and integration testing framework
- Moq 4.10.0 - Mocking tool


## Getting Started
### Prerequisites
- In order to view the source code you should install **Visual Studio** on your computer. You may refer to this [**LINK**](https://visualstudio.microsoft.com/) to download the software. Afterwards, please ensure that you have .NET Core 2.1 or later installed on your machine.

- You will need to create your own database to use the application. Since it uses the code-first approach of EF Core all you need to do is to execute the `Update-Database` command via the Package Manager Console.
  - Under RPContext.cs comment the entire logic inside the `OnConfiguring(DbContextOptionsBuilder optionsBuilder)` method
    
    ``` csharp
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      //Comment everything here...
    }
    ```
    
  - Put the below code inside the forementioned method
  
    ``` csharp
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlServer("YOUR_CONNECTION_STRING");
    }
    ```
    
## Running the Tests
In order to run the unit tests you may use the Test Explorer of Visual Studio. However, for the integration tests you will not be able to run them. Only the build pipeline of the master branch will be able to do so.

- In Visual Studio click **Test**
- Click **Windows**
- Click **Test Explorer**
