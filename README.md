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





<img width="1180" height="730" alt="DiagramaDominio" src="https://github.com/user-attachments/assets/c49cadba-2628-4269-9bba-0b9a953ca110" />


<img width="2918" height="1349" alt="ModeloRelacional(BD)" src="https://github.com/user-attachments/assets/640e55f6-4472-416e-ba2a-e030743567fd" />
