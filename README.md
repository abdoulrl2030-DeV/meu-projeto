# meu-projeto

Projeto exemplo com backend .NET 8 (API) e frontend React (Vite). Conteúdo:

- `backend/MeuProjeto.Api` - API .NET com autenticação JWT e SQLite.
- `frontend` - Aplicação React + Vite.
- `docker-compose.yml` - Orquestra serviços (backend + frontend).

Instruções rápidas:

1) Build e rodar com Docker Compose (recomendado):

```bash
docker compose build
docker compose up
```

- Frontend: http://localhost:3000
- Backend (Swagger): https://localhost:5000/swagger/index.html (ou http://localhost:5000/swagger)

2) Desenvolvimento frontend local:

```bash
cd frontend
npm install
npm run dev
```

3) Desenvolvimento backend local (dotnet):

```bash
cd backend/MeuProjeto.Api
dotnet restore
dotnet run
```

Segurança adicionada:

- Autenticação JWT com `Authorization: Bearer <token>` para rotas protegidas.
- Senhas armazenadas com `BCrypt`.
- CORS configurado (policy `DefaultCors`).

Próximos passos para produção:

- Trocar `Jwt:Key` em `appsettings.json` por um segredo forte (usar variáveis de ambiente em produção).
- Ativar HTTPS com certificados válidos (let's encrypt ou provedor de cloud).
- Usar um banco de dados gerenciado (Postgres, SQL Server) em vez de SQLite para produção.
- Configurar rate-limiting e WAF se necessário.
