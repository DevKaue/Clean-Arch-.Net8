# 📋 Task App - Gerenciador de Tarefas

Este projeto é um **aplicativo de gerenciamento de tarefas** desenvolvido com **.NET 8** e arquitetura **CQRS**, ideal para criar, listar, atualizar e excluir tarefas de forma segura e eficiente.

---

## 🚀 Tecnologias Utilizadas

- **.NET 8**  
- **CQRS (Command Query Responsibility Segregation)**  
- **MediatR** – Para comunicação desacoplada entre comandos e handlers  
- **AutoMapper** – Para mapeamento entre entidades e DTOs  
- **FluentValidation** – Para validação robusta dos dados de entrada  
- **Autenticação via JWT** – Para garantir segurança nas requisições  
- **SQL Server** – Banco de dados relacional utilizado

---

## 📁 Estrutura do Projeto

```bash
TasksApp/
├── API/               # Endpoints da Web API (.NET 8)
├── Application/       # Lógica de aplicação (Handlers, DTOs, Validations)
├── Domain/            # Entidades e interfaces do domínio
├── Infra/             # Acesso a dados, contexto EF, repositórios
├── Services/          # Serviços de negócio e integrações
└── TasksApp.sln
```

---

## 🔐 Autenticação

A autenticação é baseada em **JWT (JSON Web Token)**. Após o login, o token gerado deve ser enviado no header `Authorization` com o prefixo `Bearer` nas próximas requisições:

```
Authorization: Bearer <seu-token-jwt>
```

---

## ✅ Funcionalidades

- [x] Cadastro de usuário com validação
- [x] Login e geração de token JWT
- [x] Criação de tarefas
- [x] Listagem de tarefas (por usuário)
- [x] Atualização e exclusão de tarefas
- [x] Validações personalizadas com FluentValidation
---

## 🛠️ Como Executar

### Pré-requisitos

- .NET 8 SDK
- SQL Server instalado e configurado
- Visual Studio ou VS Code

### Configurações

1. Clone o repositório:

```bash
git clone https://github.com/seu-usuario/task-app.git
cd task-app
```

2. Configure o `appsettings.json` com a sua connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TaskAppDb;Trusted_Connection=True;"
},
"JwtSettings": {
  "Secret": "sua-chave-secreta-aqui",
  "Issuer": "TaskAppAPI",
  "Audience": "TaskAppClient",
  "ExpirationMinutes": 60
}
```

3. Execute as migrations:

```bash
dotnet ef database update
```

4. Rode a aplicação:

```bash
dotnet run --project API
```

---

## 🧪 Exemplos de Requisições

### 📤 Criar Tarefa

```http
POST /api/tarefas
Authorization: Bearer <seu-token>
Content-Type: application/json

{
  "titulo": "Estudar CQRS",
  "descricao": "Revisar padrões de arquitetura",
  "dataLimite": "2025-06-30"
}
```

### 📥 Obter Tarefas

```http
GET /api/tarefas
Authorization: Bearer <seu-token>
```

---

## 📌 Padrões e Boas Práticas

- CQRS com MediatR para separação clara entre comandos e consultas
- Mapeamento automático com AutoMapper
- Validações limpas com FluentValidation
- Segurança via JWT
- Injeção de dependência nativa do .NET
  
---

## 👨‍💻 Autor

**Kauê Wendt Sabino**  

