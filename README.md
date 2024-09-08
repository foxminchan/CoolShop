# CoolShop Application

<p align="justify">
This demo showcases an e-commerce application built using several microservices based on .NET Core, running on Dapr. It illustrates how to integrate small, independent microservices into a larger system, following microservice architectural principles.
</p>

<div>
  <a href="https://codespaces.new/foxminchan/CoolShop?quickstart=1">
    <img alt="Open in GitHub Codespaces" src="https://github.com/codespaces/badge.svg">
  </a>
</div>

## Pre-requisites to Run the Application

- [Install & start Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Install .NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or later, and [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/)
- [Install Dapr CLI](https://docs.dapr.io/getting-started/install-dapr-cli/)
- [Install Node.js](https://nodejs.org/en/download/)

## Architecture Overview

<img src="./docs/CoolShop.png" alt="CoolShop Architecture" width="100%">

<p align="justify">
The CoolShop application, as depicted in the diagram, is built with .NET Core and Dapr, an event-driven runtime that simplifies the development of resilient microservice applications. It consists of the following microservices:
</p>

<p align="justify">

- The API gateway abstracts the client from the underlying microservices, providing a single entry point for all requests. It is built with [YARP (Yet Another Reverse Proxy)](https://microsoft.github.io/reverse-proxy/) and is responsible for routing requests to the appropriate microservices.
- For identity management, the application uses the [Keycloak](https://www.keycloak.org/) identity and access management solution. Keycloak provides a single sign-on solution for all your applications, allowing you to secure your applications and services with minimum effort.
- The set of core backend microservices includes functionality required for an eCommerce store. Each is self-contained and independent of the others. The services include:
  - The catalog service manages the products available in the store.
  - The cart service manages the shopping cart for each user.
  - The order service manages the orders placed by users.
  - The inventory service manages the stock levels of products.
  - The promotion service manages the promotions available in the store.
  - The notification service sends notifications to users.
  - The rating service manages the ratings and reviews of products.
- Each service integrates elements of [Clean Architecture](https://jasontaylor.dev/clean-architecture-getting-started/) and [Vertical Slice Architecture](https://www.jimmybogard.com/vertical-slice-architecture/), resulting in a clean, maintainable, and testable codebase.
- For website development, the application uses [Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor) Interactive Server **_[This feature is under development]_**.

</p>

## Running the Application

1. Clone the repository:

```bash
git clone https://github.com/foxminchan/CoolShop
cd CoolShop
```

2. Start Dapr:

```bash
dapr uninstall --all && dapr init
```

3. Install dependencies:

```bash
make run
```

4. Run the following command to set the user secrets for the `CoolShop.AppHost` project

```bash
dotnet user-secrets set "keycloak-username" "your-username"
dotnet user-secrets set "keycloak-password" "your-password"
dotnet user-secrets set "postgres-user" "postgres"
dotnet user-secrets set "postgres-password" "your-password"
```

5. Start the application:

```bash
dotnet run --project ./src/CoolShop.AppHost/CoolShop.AppHost.csproj
```

> [!WARNING]
> Docker Desktop must be running before starting the application.

## Support

If you like this project, please consider supporting it by starring ⭐ and sharing it with your friends!

## Project References & Credits

- [eShopOnDapr](https://github.com/dotnet-architecture/eShopOnDapr)
- [eShopSupport](https://github.com/dotnet/eShopSupport)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
