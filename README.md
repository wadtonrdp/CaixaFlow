# 📦 Caixaflow

> Plataforma de controle de estoque, equipamentos e pedidos para redes com várias unidades e sede.

---

## 📌 Sobre o Projeto

O **Caixaflow** é uma plataforma web para gerenciar o fluxo de estoque, equipamentos e pedidos entre a sede e as unidades de uma rede. O sistema conta com controle de acesso por papel, permitindo que cada nível da operação enxergue e movimente apenas o que é da sua responsabilidade — sem depender de planilha ou mensagem perdida no WhatsApp.

O projeto nasceu como um sistema sob medida para uma empresa com sede e agências, e evoluiu para uma proposta generalizável, pensada para qualquer rede com múltiplas unidades: varejo, assistência técnica, concessionárias, franquias e distribuidoras.

---

## 👥 Estrutura de Acesso

Os níveis de acesso são configuráveis conforme o tamanho e a estrutura de cada empresa. Abaixo, o exemplo de 4 níveis usado na implantação piloto:

| Nível | Descrição | Permissões |
|---|---|---|
| **Unidade** | Opera apenas na unidade em que foi cadastrado | Controla o próprio estoque, cadastra equipamentos, abre pedidos |
| **Supervisão local** | Responde pela unidade em que foi designado | Acompanha os usuários da própria unidade e reporta ao regional |
| **Regional** | Acompanha um grupo de unidades | Visualiza estoque e pedidos das unidades sob sua gestão |
| **Sede** | Visão irrestrita do sistema | Cadastro/bloqueio de usuários, estoque completo, aprova ou nega pedidos |

```
Sede
 └── Regional
       └── Supervisão local
             └── Unidade
```

> 💡 A hierarquia acima reflete a estrutura da empresa que serviu de piloto para o projeto. Em outras implantações, os nomes e a quantidade de níveis podem ser ajustados sem alterar a lógica do sistema.

---

## 🚀 Funcionalidades

- 📦 Controle de estoque por unidade e sede
- 🖥️ Cadastro e rastreamento de equipamentos
- 📋 Criação e acompanhamento de pedidos, com status visível em cada etapa
- ✅ Aprovação e negação de pedidos, com histórico completo
- 👤 Gerenciamento de usuários com acesso por papel
- 🌐 Painel adaptado para cada nível de acesso (unidade, supervisão, regional, sede)

---

## 🖼️ Telas do Projeto

O design do produto — landing page e aplicação interna — está prototipado em HTML/CSS estático, servindo de referência visual para a implementação em Next.js.

| Tela | Descrição |
|---|---|
| `index.html` | Landing page do produto, com proposta de valor, recursos e planos |
| `login.html` | Tela de entrada, com seleção de nível de acesso |
| `dashboard.html` | Visão geral: indicadores, estoque por unidade, pedidos recentes |
| `estoque.html` | Listagem de itens com filtro por unidade e status (Ok / Baixo / Crítico) |
| `pedidos.html` | Quadro de pedidos em formato kanban (Pendente → Aprovado → Negado) |

> _Adicione aqui capturas de tela ou GIFs das telas em funcionamento assim que a implementação estiver de pé — é a primeira coisa que um recrutador olha._

---

## 🛠️ Tecnologias Utilizadas

### Front-end
![Next.js](https://img.shields.io/badge/Next.js-000000?style=for-the-badge&logo=nextdotjs&logoColor=white)
![React](https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB)
![TypeScript](https://img.shields.io/badge/TypeScript-007ACC?style=for-the-badge&logo=typescript&logoColor=white)

### Back-end
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![EF Core](https://img.shields.io/badge/Entity%20Framework%20Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)

### Banco de Dados
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white)

### Testes
![xUnit](https://img.shields.io/badge/xUnit-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)

---

## 📁 Estrutura do Repositório

```
caixaflow/
├── design/            # Protótipos estáticos das telas (HTML/CSS)
│   ├── index.html
│   ├── login.html
│   ├── dashboard.html
│   ├── estoque.html
│   └── pedidos.html
├── frontend/          # Aplicação Next.js + React + TypeScript
│   ├── src/
│   │   ├── app/
│   │   ├── components/
│   │   └── services/
│   └── package.json
├── backend/           # API em C# (.NET)
│   ├── Controllers/
│   ├── Models/
│   ├── Services/
│   ├── Migrations/
│   ├── Tests/
│   └── Caixaflow.sln
├── docs/              # Documentação técnica
│   ├── api.md
│   └── banco.md
└── README.md
```

---

## ⚙️ Como Rodar Localmente

### Pré-requisitos

- Node.js 18+
- .NET SDK 8+
- PostgreSQL

### Front-end

```bash
cd frontend
npm install
cp .env.example .env.local
npm run dev
```

### Back-end

```bash
cd backend
dotnet restore
cp appsettings.example.json appsettings.json
dotnet ef database update
dotnet run
```

> A aplicação estará disponível em `http://localhost:5124/swagger` para documentação e testes da API.

### Rodando os testes

```bash
cd backend
dotnet test
```

---

## 🔑 Variáveis de Ambiente

### Front-end (`.env.local`)

| Variável | Descrição |
|---|---|
| `NEXT_PUBLIC_API_URL` | URL base da API do back-end |

### Back-end (`appsettings.json`)

| Variável | Descrição |
|---|---|
| `ConnectionStrings__DefaultConnection` | String de conexão com o PostgreSQL |
| `Jwt__Secret` | Chave secreta para geração de tokens JWT |
| `Jwt__ExpiresInMinutes` | Tempo de expiração do token |

---

## 📐 Arquitetura

```
[Usuário] → [Next.js / React] → [API REST C#/.NET] → [PostgreSQL]
```

O front-end consome a API via requisições HTTP (REST). A autenticação é feita com JWT, e o controle de acesso é gerenciado pelo back-end com base no papel do usuário.

---

## 🗃️ Modelo de Dados

> _Adicione aqui um diagrama de entidade-relacionamento (ER) representando as principais tabelas (Usuários, Unidades, Equipamentos, Pedidos, Estoque) e seus relacionamentos. Ferramentas como dbdiagram.io ou o próprio `dotnet ef migrations` ajudam a gerar isso rapidamente._

---

## 👨‍💻 Time de Desenvolvimento

| Área | Desenvolvedor |
|---|---|
| Front-end (Next.js, React, TypeScript, design de produto) | [@wadtonrdp](https://github.com/wadtonrdp) |
| Back-end (C#, .NET, PostgreSQL) | [@tutuzimaia](https://github.com/tutuzimaia) |

---

## 📄 Licença

© 2026 Caixaflow — Todos os direitos reservados.

Este projeto é de uso comercial. Nenhuma parte do código pode ser copiada, distribuída ou modificada sem autorização prévia dos autores.
