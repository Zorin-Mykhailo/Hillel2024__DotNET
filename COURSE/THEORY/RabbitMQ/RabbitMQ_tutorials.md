[`🏠 HOME`](../../README.md)  

[`📘 RabbitMQ THEORY`](./README.md) 

# Навчальний посібник з RabbitMQ - "Hello World!"

## Вступ

> [!NOTE]
> **Попередні вимоги**
> Цей навчальний посібник передбачає, що RabbitMQ встановлено і працює на локальному комп'ютері за стандартним портом (5672). У разі використання іншого хосту, порту або облікових даних, потрібно буде налаштувати параметри підключення.

```mermaid
flowchart LR;
    P((P))
    Q[[Queue_name]]
    C((C))

    style P fill:#0094FF, stroke:#FFFFFF,stroke-width:2px
    style C fill:#00FF21, stroke:#FFFFFF,stroke-width:2px
    style Q fill:#FFD800, stroke:#FFFFFF,stroke-width:2px

    P-->Q-->C    
```