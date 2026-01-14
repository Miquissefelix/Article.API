**ArticlePlatform.Api**

Plataforma de artigos com moderação, comentários públicos, mensageria assíncrona e autenticação JWT

**Descrição**

ArticlePlatform.Api é uma API backend inspirada em plataformas como Dev.to e Medium.
Ela permite que autores escrevam, administradores moderem e o público leia e comente artigos.

O projeto demonstra práticas modernas de backend incluindo:
✔ CQRS
✔ Mensageria assíncrona (MassTransit + RabbitMQ)
✔ Autenticação JWT + Roles
✔ Comentários públicos
✔ Auditoria + E-mails automáticos
✔ Boas práticas arquiteturais

**🚀 Funcionalidades**

***Público***

-Listar artigos publicados;
-Visualizar detalhes do artigo;
-Listar comentários;
-Adicionar comentários (não requer login do autor);
-Paginação e ordenação;

***Autor***

-Login + JWT;
-Criar artigos;
-Editar artigos;
-Submeter para aprovação;
-Ver status (Aprovado/Rejeitado);
-Receber notificação por e-mail;

***Administrador***

-Login + JWT + Role Admin;
-Aprovar ou rejeitar artigos;
-Registrar motivo da rejeição;
-Acompanhar auditoria;

**🛠 Stack Técnica**
<img width="354" height="384" alt="git" src="https://github.com/user-attachments/assets/792f0ee8-930b-4b15-9b35-43ebca096bc6" />

