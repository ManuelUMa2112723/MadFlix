# Relatório do Projeto MadFlix - Versão Atualizada

## 1. Introdução

O MadFlix é uma plataforma de festivais de cinema desenvolvida em Blazor Server com .NET 10, que permite aos utilizadores explorar festivais, ver o catálogo de filmes, comprar diferentes tipos de acesso, comentar e criar listas pessoais. O sistema segue a proposta do enunciado das imagens, com foco em catálogo de filmes, festival management, experiência social e simulação de streaming.

## 2. Objetivo do Projeto

O objetivo do MadFlix é oferecer uma aplicação web que permita:

- Criação e gestão de festivais de cinema
- Exploração de catálogo de filmes por festival
- Compra de acessos de várias modalidades
- Histórico de compras e acessos
- Funcionalidade social com comentários e listas pessoais
- Utilização de Web Services para obter dados de filmes
- Arquitetura extensível e modular

## 3. Tecnologias Utilizadas

- **Framework**: ASP.NET Core / Blazor Server
- **ORM**: Entity Framework Core 10
- **Base de Dados**: SQLite (madflix.db)
- **Arquitetura**: Dependency Injection, Repository Pattern, Factory Pattern, Observer Pattern
- **Autenticação**: Cookies com roles (User, Admin)
- **UI**: Bootstrap para interface responsiva
- **Integração Externa**: TMDb API para dados de filmes (https://api.themoviedb.org/3)

## 4. Funcionalidades Implementadas

### 4.1 Gestão de Festivais ✅

Implementação completa de:
- **Criação de festivais** com: Nome, Género, Descrição, Data de início, Data de fim
- **Visualização** de festivais disponíveis (Components/Pages/Festivais.razor)
- **Adição de filmes** a festivais (um filme pode estar em múltiplos festivais)
- **Consulta de catálogo** de filmes por festival com interface visual

**Padrões utilizados**: Factory Pattern (FestivalFactory), Observer Pattern (para notificações)

### 4.2 Catálogo de Filmes por Festival ✅

- Cada festival tem filmes associados com relacionamento one-to-many
- Visualização do catálogo com informações de: título, ano, género, sinopse, poster, IMDb rating, runtime, trailer
- Filmes importados dinamicamente da TMDb API ou consultados de cache local
- Integração com FilmeService que busca dados completos: diretor, atores (top 5), trailer URL

### 4.3 Tipos de Acesso e Carrinho ✅

Tipos de acesso implementados:

1. **Passe Diário**: Acesso a todas as sessões de um dia específico do festival (2 sessões fixas: 11:30-16:30 e 18:00-23:00)
2. **Passe Completo**: Acesso a todas as sessões durante toda a duração do festival
3. **Aluguer Digital**: Acesso a um filme específico por período de tempo (ex: 48 horas)
4. **Bilhete de Sessão**: Acesso a um filme/sessão específica

**Funcionalidades do carrinho**:
- Seleção de tipo de acesso
- Cálculo automático de preço conforme tipo e duração
- Persistência de itens do carrinho em base de dados
- Histórico completo de bilhetes e acessos em Components/Pages/Bilhetes.razor

**Estados de bilhetes**:
- Ativos (válidos agora)
- Pendentes (válidos no futuro)
- Expirados (vencidos)

### 4.4 Experiência Social ✅

#### Comentários e Avaliações
- Sistema de **comentários com avaliação em estrelas** (1-5) por filme
- Cada comentário armazena: utilizador, texto, número de estrelas, data/hora
- Remoção de comentários disponível para **administradores** (role-based)
- Visualização em FilmeDetalhes.razor com interface intuitiva

#### Listas Pessoais
- **Criar listas de filmes** personalizadas (ex: "Favoritos", "A ver depois")
- **Adicionar/remover filmes** das listas
- **Gestão de listas** no perfil do utilizador (Components/Pages/Perfil.razor)
- Acesso rápido a filmes agrupados por categoria pessoal

### 4.5 Simulação de Streaming

- **Sem streaming real** de ficheiros de vídeo
- **Player embebido com YouTube** para trailers
- **Página de reprodução** em FilmePlayer.razor que simula experiência de visualização
- **Validação de acesso**: Só permite ver filme se utilizador tiver bilhete válido para esse filme
- **Rastreamento de visualizações**: Modelo FilmeVisualizacao guarda registo de cada visualização

## 5. Arquitetura e Modelo de Domínio

### 5.1 Estrutura do Projeto

**Program.cs** configura:
- Componentes Blazor com renderização interativa
- Autenticação baseada em cookies
- Entity Framework Core com SQLite
- Serviços de aplicação com Dependency Injection

**Separação clara entre camadas**:
- `Models/` - Entidades de domínio
- `Services/` - Lógica de negócio
- `Repositories/` - Acesso a dados
- `Components/Pages/` - Interfaces de utilizador (Blazor)
- `Configuration/` - Configurações
- `Endpoints/` - Rotas HTTP (Auth endpoints)
- `DependencyInjection/` - Registro de serviços

### 5.2 Modelos Principais

**Entidades de Domínio**:
- `Festival` - Festival com dados básicos e relacionamentos
- `Filme` - Filme com metadata de TMDb, diretor, atores
- `Utilizador` - User com autenticação
- `ComentarioFilme` - Avaliação com estrelas e texto
- `ListaPessoal` e `ListaPessoalFilme` - Organização pessoal de filmes
- `FilmeCompra` - Registo de compra de acesso a filme
- `AcessoFestivalCompra` - Registo de compra de passe de festival
- `Carrinho` e `CarrinhoItem` - Carrinho de compras
- `FilmeVisualizacao` - Rastreamento de visualizações
- `UserNotification` - Sistema de notificações

**Padrões de Design Utilizados**:
- **Factory Pattern**: `FestivalFactory`, `AcessoFactoryConcrete` (criação de diferentes tipos de acesso)
- **Observer Pattern**: `IFestivalSubject`, `FestivalNotificationObserver` (notificações em tempo real)
- **Repository Pattern**: Interfaces `IFilmeRepository`, `IUtilizadorRepository`, etc.
- **Strategy Pattern**: Diferentes estratégias de acesso (`TipoAcesso`)

### 5.3 Persistência

- Dados armazenados em **SQLite** via Entity Framework Core
- **Migrations** presentes para versionamento do schema:
  - InitialCreate
  - AddCarrinho
  - AddComentariosFilmes
  - AddListaPessoal
  - AddFilmeCompraAcessoWindow
  - AddAcessoFestivalCompra
  - AddIsAdmin
  - AddTrailerUrl
  - Outras migrações
- Suporte a **aplicação automática de migrations** no startup

## 6. Casos de Uso Principais

1. **Utilizador autentica-se** e navegada pelos festivais disponíveis
2. **Abre catálogo de filmes** de um festival específico
3. **Seleciona tipo de acesso** (passe diário, completo ou aluguer) e adiciona ao carrinho
4. **Procede ao checkout** e simula pagamento
5. **Consulta histórico de bilhetes** com status (ativo, pendente, expirado)
6. **Abre um filme**, vê trailer embed de YouTube e pode **comentar/avaliar** (1-5 estrelas)
7. **Cria listas pessoais** de filmes e adiciona conteúdos para organização
8. **Administrador cria festivais** e associa filmes do catálogo
9. **Administrador remove comentários** inadequados

## 7. Alinhamento com o Enunciado

### O Que Está Implementado ✅

| Requisito | Status | Observações |
|-----------|--------|------------|
| Criação e gestão de festivais | ✅ Completo | Interface funcional com todos os campos |
| Exploração de catálogo de filmes por festival | ✅ Completo | Catálogo dinâmico com dados de TMDb |
| Sessões e janelas de acesso | ⚠️ Parcial | 2 sessões fixas hardcoded (não variáveis por festival) |
| Compra de diferentes tipos de acesso | ✅ Completo | 4 tipos implementados |
| Histórico de bilhetes/acessos | ✅ Completo | Com filtragem por estado |
| Listas pessoais | ✅ Completo | Criação, edição, remoção |
| Comentários e avaliações | ✅ Completo | Com sistema de estrelas (1-5) |
| Autenticação e autorização | ✅ Completo | Roles (User, Admin) |
| Simulação de visualização | ✅ Completo | Player com trailer embebido |
| Integração com serviço externo de filmes | ✅ Completo | TMDb API |

### O Que NÃO Está Implementado ❌

| Requisito | Status | Observações |
|-----------|--------|------------|
| Votação para prémios do público | ❌ Não existe | Sem implementação de votação ou prémios |
| Recomendações inteligentes | ⚠️ Rudimentar | Apenas 8 filmes com melhor IMDb rating; Home.razor usa lista hardcoded |
| Streaming real | ❌ Não existe | Apenas trailers embed de YouTube |
| Sessões com horários variáveis | ❌ Não existe | Sessões são estáticas em `SessaoFestivalCatalog.cs` |
| Bilhetes para sessões específicas | ⚠️ Parcial | Conceito existe mas sem verdadeira entidade de Sessão |
| Sistema de recomendação por género/preferências | ❌ Não existe | Sem engine de recomendação personalizada |

## 8. Detalhes Técnicos Importantes

### Sessões de Festival

As "sessões" são **definidas de forma estática** em `Models/Acessos/SessoesFestival/SessaoFestivalCatalog.cs`:

```csharp
public static class SessaoFestivalCatalog
{
    public static IReadOnlyList<SessaoFestival> SessoesPasseDiario { get; } =
    [
        new SessaoFestival("Sessao 1", new TimeSpan(11, 30, 0), new TimeSpan(16, 30, 0)),
        new SessaoFestival("Sessao 2", new TimeSpan(18, 0, 0), new TimeSpan(23, 0, 0))
    ];
}
```

**Limitações**:
- Apenas 2 sessões fixas por dia de festival
- Não há possibilidade de criar sessões adicionais
- Não há modelo de base de dados para Sessão
- Os acessos (AcessoFestivalCompra) são ao Festival como um todo, não a uma sessão específica

### Recomendações

Existe um método `GetRecomendadosAsync()` no FilmeService que retorna os 8 filmes com melhor IMDb rating:

```csharp
public async Task<List<Filme>> GetRecomendadosAsync()
{
    return await _context.Filmes
        .OrderByDescending(f => f.ImdbRating)
        .Take(8)
        .ToListAsync();
}
```

**MAS** na página Home.razor, as recomendações são **hardcoded**:
```csharp
private readonly string[] titulosRecomendados =
[
    "The Matrix",
    "Interstellar",
    "The Godfather",
    "The Dark Knight",
    "Pulp Fiction",
    "Fight Club"
];
```

Não há recomendação **personalizadas** baseadas em:
- Histórico de visualizações do utilizador
- Géneros preferidos
- Filmes já avaliados
- Outros utilizadores similares

## 9. Pontos de Força e Pontos para Melhorar

### Pontos de Força ✅

1. **Arquitetura bem estruturada**: Separação clara de responsabilidades
2. **Padrões de design**: Factory, Observer, Repository bem aplicados
3. **Integração com API externa**: Dados de filmes atualizados
4. **Sistema de roles e autenticação**: Diferenciação User/Admin
5. **Interface responsiva**: Bootstrap bem utilizado
6. **Persistência em base de dados**: EF Core com migrations
7. **Experiência social**: Comentários e listas pessoais

### Pontos para Melhoria 🔧

1. **Votação de prémios**: Completamente ausente
2. **Recomendações inteligentes**: Apenas hardcoded ou por rating
3. **Sessões dinâmicas**: Apenas 2 sessões fixas
4. **Streaming real**: Não implementado (apenas trailers)
5. **Notificações em tempo real**: Estrutura existe mas poderia ser expandida
6. **Analytics**: Sem dashboard de estatísticas
7. **Pagamentos reais**: Apenas simulação (sem integração com gateway)

## 10. Documentação de Estrutura

### Estrutura do Projeto Completa

```
MadFlix/
├── Components/
│   ├── Pages/           # Páginas Blazor (Festivais, FilmeDetalhes, Bilhetes, etc.)
│   ├── Layout/          # Layout base
│   └── _Imports.razor   # Imports globais
├── Models/
│   ├── Acessos/         # Tipos de acesso
│   ├── Festivais/       # Festival e padrões
│   ├── Pagamentos/      # Modelos de pagamento
│   └── *.cs             # Entidades principais
├── Services/
│   ├── Auth/            # Autenticação
│   ├── Carrinhos/       # Lógica de carrinho
│   ├── Festivais/       # Lógica de festivals
│   ├── Filmes/          # Lógica de filmes
│   └── Notifications/   # Sistema de notificações
├── Repositories/
│   ├── EfFilmeRepository.cs
│   ├── IFilmeRepository.cs
│   └── Outros repositórios...
├── Data/
│   └── AppDbContext.cs  # Contexto EF Core
├── Migrations/          # Histórico de schema
├── DependencyInjection/
│   └── ServiceCollectionExtensions.cs
├── Endpoints/
│   └── AuthEndpointRouteBuilderExtensions.cs
├── Extensions/
│   ├── ClaimsPrincipalExtensions.cs
│   ├── NavigationHelpers.cs
│   └── WebApplicationExtensions.cs
├── wwwroot/             # Assets estáticos
├── appsettings.json     # Configuração
└── Program.cs           # Startup
```

## 11. Como Executar

```bash
# Restaurar pacotes
dotnet restore

# Aplicar migrations
dotnet ef database update

# Executar aplicação
dotnet run
```

A aplicação estará disponível em `https://localhost:7000` (ou a porta configurada em launchSettings.json).

## 12. Conclusão

O MadFlix é uma plataforma de festivais de cinema **funcional e bem arquitetada** que cobre a maioria dos requisitos essenciais do enunciado:

✅ **Funcionalidades principais implementadas**: Gestão de festivais, catálogo de filmes, compra de acessos, histórico, comentários, listas pessoais

⚠️ **Funcionalidades parcialmente implementadas**: Sessões (apenas 2 hardcoded), recomendações (apenas por rating ou hardcoded)

❌ **Funcionalidades não implementadas**: Votação de prémios, recomendações personalizadas, streaming real

**Para melhorar a classificação académica**, recomenda-se:

1. **Implementar votação de prémios** com entidade e interface para votação pública
2. **Criar motor de recomendação** baseado em histórico e preferências do utilizador
3. **Expandir sistema de sessões** com possibilidade de criar horários dinâmicos
4. **Melhorar documentação** com diagramas UML (casos de uso, domínio, sequência)
5. **Adicionar testes unitários** e testes de integração

A arquitetura atual é sólida e extensível, facilitando a adição destas funcionalidades sem refactoring major.
