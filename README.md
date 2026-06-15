# SalaShow

SalaShow é um sistema criado para gerenciar a reserva de salas em faculdades/empresas, criado para compor a nota da 3ª avaliação da matéria de Programação Orientada a Objetos, lecionada pelo professor Paulo Manseira, do Centro Universitário Católica de Santa Catarina.

Este programa foi criado utilizando arquitetura MVC, aplicando os conceitos estudados em sala de aula.

## Requisitos

- **.NET Framework 4.7.2** ou **Mono** (para Linux)
- **Visual Studio 2019+** ou **Rider**

---

## Instruções de Build

Siga as instruções abaixo para compilar, a depender do seu sistema e ferramenta escolhida.

### Windows - Utilizando Visual Studio

1. Abra a solução `SalaShow.sln` no Visual Studio.
2. Compile com **Ctrl+Shift+B** (Build > Build Solution).

Se houver erros de pacotes NuGet ausentes, restaure os mesmos:

```
dotnet restore
```

Ou clique com o botão direito na solução e selecione **Restore NuGet Packages**.

---

### Ubuntu (ou Windows) - Utilizando Rider

1. Instale o **Mono** (necessário para compilar projetos .NET Framework no Linux):

```
sudo apt update
sudo apt install mono-complete
```

2. Abra a solução `SalaShow.sln` no Rider.
3. Vá em **Build > Build Solution** (Ctrl+F9).

Caso os pacotes não sejam restaurados automaticamente, execute no terminal:

```
dotnet restore
```

> **Nota:** Se o `dotnet` CLI não estiver instalado, use `msbuild SalaShow.sln /t:Restore`.

---

## Estrutura do Projeto

```
SalaShow/
        Controller/  ---->  Lógica
        Model/       ---->  Classes de modelo
        View/        ---->  Interface (CLI)
        Program.cs   ---->  Entrypoint do programa
```

---

## Resolução de Problemas - `dotnet restore`

O comando `dotnet restore` baixa dependências NuGet ausentes antes da compilação. Sua execução não deve ser obrigatória, mas pode ser necessária nos seguintes casos:

- Na **primeira compilação** em uma máquina nova;
- Quando o arquivo `packages.config` ou referências de pacote forem alterados;
- Se houver erros do tipo _"This project references NuGet package(s) that are missing on this computer"_.

Para executar:

```
dotnet restore SalaShow.sln
```

---

Criado em ambiente Linux.
