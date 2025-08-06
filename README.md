# BooksStorage (EN-US)

## Overview
BooksStorage is a microservices-based web and API application designed for managing and storing books. It leverages Go, Angular, and utilizes Docker, Kubernetes, and MongoDB for deployment and storage. The project provides a web interface for searching and consulting books, as well as backend APIs for feeding and retrieving book data.

## Features
- Microservices architecture
- Web interface for book management (Angular)
- Backend APIs (Go)
- MongoDB for persistent storage
- Dockerized services for easy deployment
- Kubernetes manifests for orchestration and scaling

## Architecture
- **Client**: Angular web application for user interaction
- **Server**: Multiple backend services (Go) for API and background processing
- **Database**: MongoDB for storing book data
- **DevOps**: Docker for containerization, Kubernetes for orchestration

## Technologies Used
- Angular
- Go
- MongoDB
- Docker
- Kubernetes

## Setup & Deployment
1. **Clone the repository**
   
   HTTPS:
   ```sh
   git clone https://github.com/Vininhos/Books-Storage.git
   ```
   or with SSH:
   ```sh
   git clone https://github.com/Vininhos/Books-Storage.git
   ```
2. **Docker Compose**
   ```sh
   cd Books-Storage
   docker-compose up --build
   ```
3. **Kubernetes**
   - Use the manifests in the `K8s/` directory for deployment.
   - Example:
     ```sh
     kubectl apply -k K8s/BooksStorage/overlays/dev
     ```

## Usage
- Access the web interface to search, add, and manage books.
- Use the API endpoints for programmatic access (see API documentation if available).

## License
MIT License

---

# BooksStorage (PT-BR)

## Visão Geral
BooksStorage é uma aplicação web e API baseada em microsserviços para gerenciar e armazenar livros. Utiliza C#, Go e Angular, além de Docker, Kubernetes e MongoDB para implantação e armazenamento. O projeto oferece uma interface web para consulta e busca de livros, além de APIs para alimentar e recuperar dados dos livros.

## Funcionalidades
- Arquitetura de microsserviços
- Interface web para gerenciamento de livros (Angular)
- APIs backend (Go)
- MongoDB para armazenamento persistente
- Serviços dockerizados para fácil implantação
- Manifests Kubernetes para orquestração e escalabilidade

## Arquitetura
- **Cliente**: Aplicação web Angular para interação do usuário
- **Servidor**: Múltiplos serviços backend (Go) para API e processamento em background
- **Banco de Dados**: MongoDB para armazenar dados dos livros
- **DevOps**: Docker para conteinerização, Kubernetes para orquestração

## Tecnologias Utilizadas
- Angular
- Go
- MongoDB
- Docker
- Kubernetes

## Instalação & Deploy
1. **Clone o repositório**
   ```sh
   git clone https://github.com/Vininhos/Books-Storage.git
   ```
2. **Docker Compose**
   ```sh
   cd Books-Storage
   docker-compose up --build
   ```
3. **Kubernetes**
   - Utilize os manifests no diretório `K8s/` para implantação.
   - Exemplo:
     ```sh
     kubectl apply -k K8s/BooksStorage/overlays/dev
     ```

## Uso
- Acesse a interface web para buscar, adicionar e gerenciar livros.
- Utilize os endpoints da API para acesso programático (veja a documentação da API se disponível).

## Licença
MIT License