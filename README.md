# MadFlix

## Elementos do grupo

- Manuel Fernandes - 2112723
- Ricardo Nunes - 2118823
- Juan Agustin Gomes Olim - 2030418
- Luis Andrade - 2131023

## Como correr o projeto

Restaurar dependencias:


dotnet restore


Aplicar migrations / atualizar a base de dados:


dotnet ef database update --project MadFlix/MadFlix.csproj


Correr o projeto:


dotnet run --project MadFlix/MadFlix.csproj


## Base de dados e credenciais

O projeto usa SQLite:

```json
"DefaultConnection": "Data Source=madflix.db"
```

A aplicacao permite criar conta na pagina de registo.

Para testar funcionalidades de administrador, a conta deve ter `IsAdmin = 1` na tabela `Utilizadores`.


