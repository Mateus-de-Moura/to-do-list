# To-Do-List

Projeto  de cadastro de tasks, com funcionalidades de create, update, delete e GetAll.
Front-End criado  com  angular 18 e Back-End com api .net core 8 .

## Tecnologias

Liste as tecnologias utilizadas no projeto. Exemplo:

- **Linguagem:** C#, JavaScript, jQuery.
- **Frameworks:** ASP.NET, Angular, .
- **Banco de Dados:** SQL Server.

## Scripts Banco de Dados

### Create DataBase
```sql
CREATE DATABASE Tasks

````

### Create Table

```sql
USE [Tasks]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Task](
	[Id] [uniqueidentifier] NOT NULL,
	[Completed] [bit] NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



```

## Instalação
Instruções para instalar o projeto:

```bash
# Clone o repositório
git clone https://github.com/Mateus-de-Moura/to-do-list.git

# Acesse o diretório do projeto
cd nome-do-repo

# Instale as dependências
npm install


