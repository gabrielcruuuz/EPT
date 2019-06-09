# EPT
Projeto de TCC - Universidade Anhembi Morumbi

O projeto foi desenvolvido utilizando a linguagem de programção C# juntamente com o .NET Framework e banco de dados SQL Server

Requerimentos para rodar o sistema: 
Visual Studio,
SQL Server

Guia de como rodar a aplicação:

1. Baixe todo o projeto
2. Crie um banco de dados com o nome de "EPT"
3. Rode o script com o nome de "Script_Banco_EPT" que está dentro do projeto em seu banco de dados criados.
4. Abra seu Visaul Studio e inicialize a solução localizada dentro da pasta EPT (EPT.sln)
5. Modifique o arquivo Web.Config alterando a String de conexão para o seu banco de dados criado no passo 2/3.
6. Rode o projeto

Caso queira você também pode adicionar novos usuarios diretamente na tabela tbUsuario dentro do banco de dados ou utilizar os genericos para exemplo:

RA: 123
senha: 123
Perfil : Aluno

RA: 123456
senha: 123
Perfil : Professor

RA: 123456789
senha: 123
Perfil : Coordenador
