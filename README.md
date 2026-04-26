# SIMI - Sistema Integrado de Monitoramento Industrial 🏭

Este projeto foi desenvolvido como parte da Situação de Aprendizagem (SA) focada em **Documentação, Persistência e Publicação de APIs**. A solução monitora sinais industriais em tempo real, integrando simuladores de hardware, uma API de processamento e uma interface de controle para o operador.

## 📋 Requisitos da SA Atendidos
- **Persistência:** Implementação de banco de dados SQLite com criação automática de tabelas.
- **Evolução de Sinais:** Inclusão do sinal de **Humidade** (Sinal Industrial escolhido) além da temperatura.
- **Regras de Negócio:** Validação de limites operacionais diretamente no Backend.
- **Documentação:** API documentada via Swagger com anotações XML completas.
- **Versionamento:** Publicação em repositório público no GitHub.

## 🛠️ Tecnologias Utilizadas
- **Backend:** ASP.NET Core Web API 8.0
- **Frontend:** WPF (Windows Presentation Foundation) com padrão MVVM
- **Simulador:** .NET Console Application
- **Banco de Dados:** SQLite com Entity Framework Core
- **Documentação:** Swagger (OpenAPI)

## 🏗️ Arquitetura da Solução
A solução é dividida em 4 projetos principais:
1. **ApiProcessamento:** Responsável por receber os dados, aplicar regras de negócio e persistir no SQLite.
2. **SensorInterface:** Dashboard visual que consome a API e exibe alertas ao operador.
3. **SensorSimulator:** Emula o comportamento de sensores enviando dados via protocolo HTTP (POST).
4. **Shared:** Biblioteca de classes compartilhada para garantir a integridade do contrato de dados.
