# Documentação de Arquitetura - Payment Flow

## 1. Visão Geral
O projeto **Payment Flow** é uma aplicação baseada em .NET Core 9 que gerencia um fluxo de pagamentos, permitindo a realização de débitos, créditos e consultas consolidadas de saldo diário. A aplicação implementa autenticação via OAuth 2.0 e segue os princípios de **SOLID** e **Domain-Driven Design (DDD)**.

## 2. Tecnologias Utilizadas
- **.NET Core 9** - Framework principal da aplicação.
- **SQL Server 2019** - Banco de dados relacional.
- **Entity Framework Core** - ORM para gerenciamento de banco de dados.
- **XUnit** - Framework de testes unitários.
- **JWT (JSON Web Token)** - Implementação de autenticação via OAuth 2.0.
- **Logger** - Utilização do `ILogger` para logging estruturado.

## 3. Arquitetura do Sistema
A aplicação adota a arquitetura baseada em camadas e princípios do **DDD (Domain-Driven Design)**, sendo dividida em:

### 3.1 Camadas
- **Apresentação (API Layer)**: Contém os controllers responsáveis por expor os endpoints.
- **Aplicação (Application Layer)**: Contém os serviços que encapsulam regras de negócio de alto nível.
- **Domínio (Domain Layer)**: Contém as entidades, interfaces e regras de negócio centrais.
- **Infraestrutura (Infrastructure Layer)**: Implementação de persistência e repositórios para acesso ao banco de dados.

### 3.2 Estrutura do Projeto
```
PaymentFlow/
├── PaymentFlow.Api/            # Camada de Apresentação (Controllers)
├── PaymentFlow.Application/    # Camada de Aplicação (Serviços)
├── PaymentFlow.Domain/         # Camada de Domínio (Entidades, Interfaces)
├── PaymentFlow.Infrastructure/ # Camada de Infraestrutura (Repositórios, DBContext)
├── PaymentFlow.Tests/          # Testes Unitários (XUnit)
```

## 4. Fluxo de Pagamentos
O fluxo de pagamentos consiste nas seguintes operações principais:
1. **Autenticação do usuário** - O usuário solicita um token de acesso via JWT.
2. **Crédito e Débito** - O usuário pode registrar transações de crédito ou débito.
3. **Consulta de Saldo Diário** - O usuário pode visualizar um relatório consolidado de saldo.

## 5. Segurança e Autenticação
A autenticação é realizada via OAuth 2.0 utilizando **JWT (JSON Web Token)**. Cada requisição protegida deve incluir o token no cabeçalho `Authorization`.

## 6. Banco de Dados
A aplicação utiliza **SQL Server 2019** com **Entity Framework Core** para persistência. As principais tabelas incluem:
- **Payments**: Registra as transações financeiras (débito/crédito).
- **Users**: Registra os usuários autenticados.

## 7. Testes
O projeto implementa testes unitários utilizando **XUnit**, cobrindo os seguintes aspectos:
- Testes de Serviços (Application Layer)
- Testes de Repositórios (Infrastructure Layer)
- Testes de API (Controllers)

## 8. Conclusão
A arquitetura do **Payment Flow** segue as melhores práticas de **SOLID** e **DDD**, garantindo escalabilidade, modularidade e testabilidade. A autenticação via **OAuth 2.0** e a persistência via **Entity Framework Core** tornam o sistema seguro e eficiente.

