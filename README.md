# 🎲 Fichality - Sistema Web para Fichas Dinâmicas de RPG

O **Fichality** é um projeto de Trabalho de Conclusão de Curso (TCC) focado em resolver a rigidez das fichas tradicionais de RPG de mesa. A plataforma permite a criação de blocos e campos altamente dinâmicos armazenados em formato JSON.

## 🚀 Tecnologias Utilizadas

- **Back-end:** C# | ASP.NET Core Web API (.NET 8)
- **Banco de Dados:** SQL Server | Entity Framework Core (Migrations & Fluent API)
- **Front-end:** HTML5 | CSS3 | JavaScript (Fetch API)
- **Criptografia:** BCrypt.Net

## 🛠️ Arquitetura e Modelagem

- Persistência de estruturas dinâmicas utilizando `NVARCHAR(MAX)` no SQL Server.
- Mapeamento relacional de usuários com chave estrangeira (1:N) e índice único de e-mail.
- Autenticação de contas com hash seguro de senhas.

## ✒️ Autor

Desenvolvido por **César Batitucci** como parte do TCC em Análise e Desenvolvimento de Sistemas.
