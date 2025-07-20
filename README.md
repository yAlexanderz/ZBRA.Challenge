# Solução do Desafio ZBRA

![Status](https://img.shields.io/badge/build-passing-brightgreen)
![Cobertura](https://img.shields.io/badge/cobertura-100%25-brightgreen)
![.NET](https://img.shields.io/badge/.NET-8.0-blue)

Esta é minha implementação para o desafio ZBRA. Tentei seguir os princípios SOLID e usar boas práticas de arquitetura limpa.

## Visão Geral

O desafio tinha duas questões bem diferentes:

1. **Validação e Contagem de Senhas**: Analisar quantas senhas válidas existem dentro de um intervalo baseado em regras específicas.
2. **Processamento de Comandos**: Executar uma sequência de comandos de um arquivo e calcular um endereço final.

## Arquitetura

Optei por uma arquitetura em camadas pra facilitar os testes e manutenção:

- **Core**: Entidades de negócio, regras e interfaces
- **Application**: Implementação dos casos de uso
- **Infrastructure**: Implementações concretas (IO, acesso a dados)
- **Console**: Interface com usuário

## Estrutura da Solução

```
ZBRA.Challenge/
├── src/
│   ├── ZBRA.Challenge.Core/               
│   ├── ZBRA.Challenge.Application/        
│   ├── ZBRA.Challenge.Infrastructure/    
│   └── ZBRA.Challenge.Console/           
└── tests/
    ├── ZBRA.Challenge.Core.Tests/         
    ├── ZBRA.Challenge.Application.Tests/   
    └── ZBRA.Challenge.Infrastructure.Tests/
```

## Padrões de Design Utilizados

Utilizei alguns padrões que achei adequados para o problema:

- **Strategy**: Para as regras de validação de senha
- **Command**: Para os diferentes tipos de comandos
- **Factory**: Para criar as instâncias de comando
- **DI**: Injeção de dependências nos construtores

## Implementação das Questões

### Questão 1: Validação de Senhas

Implementei as seguintes regras:
- Validação de intervalo (184759-856920)
- Verificação de dígitos adjacentes iguais
- Verificação de dígitos nunca decrescentes
- Grupo de exatamente dois dígitos adjacentes iguais

### Questão 2: Processamento de Comandos

Interpretei os comandos conforme especificação:
- Comandos de endereço (começando com 20)
- Comandos de salto (começando com 5)
- Comandos sem operação

## Como rodar

### Pré-requisitos
- .NET 8.0

### Instruções
1. Clone o repo
```bash
git clone https://github.com/yAlexanderz/ZBRA.Challenge
cd ZBRA.Challenge
```

2. Compile
```bash
dotnet build
```

3. Execute
```bash
cd src/ZBRA.Challenge.Console
dotnet run
```

## Testes

Os testes são bem completos, dá pra rodar com:
```bash
dotnet test
```

## Decisões técnicas

Algumas decisões que tomei durante o desenvolvimento:

1. **Por que Clean Architecture?**
   - Facilita a manutenção e os testes
   - Separa bem as responsabilidades
   - Deixa o código mais organizado

2. **Abordagem de validação de senhas**
   - Uso de Strategy pra facilitar adicionar novas regras
   - Cada regra tem uma única responsabilidade

3. **Processamento de comandos**
   - Command pattern pra encapsular os diferentes tipos
   - Separação entre criação e execução de comandos

## Conclusão

O desafio foi interessante de implementar. Acho que consegui uma solução razoavelmente limpa e testável.

---

## Autor
Yago Alexander  