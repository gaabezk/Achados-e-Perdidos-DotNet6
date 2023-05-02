# API Achados e Perdidos

API para gerenciamento de usuários e posts do sistema Achados e Perdidos.

## Tecnologias Utilizadas

- .NET Core 7.0
- Entity Framework Core v7
- AutoMapper 12.0.0
- FluentValidation 11.4.0
- JWT Bearer Authentication 7.02
- MySQL
- Swagger/OpenAPI

## Endpoints

### Usuários

| Endpoint                          | Método HTTP  | Descrição                                                                                     | Política de autorização   |
|-----------------------------------|--------------|-----------------------------------------------------------------------------------------------|---------------------------|
| api/User                          | POST         | Cadastra um usuário no sistema.                                                               | Nenhuma                   |
| api/User                          | GET          | Lista todos os usuários do sistema.                                                           | Admin                     |
| api/User/{id}                     | GET          | Busca um usuário pelo ID.                                                                     | Admin                     |
| api/User/{id}                     | PUT          | Edita os dados de um usuário pelo ID.                                                         | Autenticado               |
| api/User/pass                     | PUT          | Edita a senha de um usuário pelo email e senha antiga.                                        | Autenticado               |
| api/User/role?id={id}&role={role} | PUT          | Edita a role de um usuário pelo ID.                                                           | Admin                     |
| api/User/{id}                     | DELETE       | Remove um usuário do banco de dados.                                                          | Admin                     |



### Posts

| Endpoint                       | Método HTTP | Descrição                                                                                           | Política de autorização |
|--------------------------------|-------------|-----------------------------------------------------------------------------------------------------|-------------------------|
| /api/post                      | GET         | Recupera todos os posts existentes                                                                  | Nenhuma                 |
| /api/post                      | POST        | Cadastra um post no sistema passando o ID do usuário                                                | Usuário                 |
| /api/post/{id:guid}            | GET         | Busca um post pelo ID                                                                               | Nenhuma                 |
| /api/post/status               | GET         | Busca posts pelo status                                                                             | Usuário                 |
| /api/post/user                 | GET         | Busca posts pelo ID do usuário                                                                      | Usuário                 |
| /api/post                      | PUT         | Edita os dados de um post pelo ID                                                                   | Usuário                 |
| /api/post/status               | PUT         | Edita os status de um post pelo ID                                                                  | Administrador           |
| /api/post/{id:guid}            | DELETE      | Remove um post do banco de dados pelo ID                                                            | Administrador           |



### Autenticação

| Endpoint  | Método HTTP | Descrição                                        |
|-----------|-------------|--------------------------------------------------|
| api/login | POST        | Faz o login de um usuário e retorna um token JWT |
| api/user  | POST        | Cria um novo usuário e retorna um token JWT      |


### Autorização

A API utiliza autenticação e autorização via token JWT. Alguns endpoints são protegidos apenas para usuários autenticados e outros apenas para usuários com permissão específica.

### Documentação

A API utiliza o Swagger como documentação principal pela url: `/swagger/index.html`



## Como Executar
1. Certifique-se de ter o .NET Core SDK instalado na sua máquina. Para verificar se está instalado, abra o terminal e execute o seguinte comando:
```powershell
   dotnet --version
   ```
2. Necessario ter um banco MySQL.
3. Clone o repositório: `git clone https://github.com/gaabezk/Achados-e-Perdidos-DotNet6.git`
4. Entre na pasta do projeto: `cd Achados-e-Perdidos-DotNet6`
5. Informe a `string de conexão` e a `secret` no `appsettings.json`
6. Abra o terminal na pasta raiz do projeto
7. Execute o comando `dotnet restore` para restaurar as dependências do projeto
8. Navegue até a pasta `ACHADOS_E_PERDIDOS.Data` no terminal
9. Execute o comando `dotnet ef database update` para criar o banco de dados e aplicar as migrações
10. Volte para a pasta raiz do projeto e navegue até a pasta `ACHADOS_E_PERDIDOS.WebApi`
11. Execute o comando `dotnet run` para iniciar a aplicação
12. Acesse a aplicação no seu navegador através do endereço `http://localhost:5240`


## Licença

Este projeto está licenciado sob a licença MIT. Consulte o arquivo LICENSE para obter mais informações.
