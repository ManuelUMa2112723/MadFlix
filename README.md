Caminho A — Projeto Blazor Web App
� LojaApp/
│
├── � Models/
│ ├── Produto.cs ← classes C# simples
│ └── Categoria.cs
│
├── � Data/
│ └── AppDbContext.cs ← EF Core — ligação à BD
│
├── � Repositories/
│ └── ProdutoRepository.cs ← padrão Repository — todas as queries
│
├── � Services/
│ └── ProdutoService.cs ← lógica de negócio — Strategy, Factory...
├── � Pages/
│ └── Produtos.razor ← UI — chama o Service diretamente
│
└── Program.cs ← regista tudo no Dependency Injection
