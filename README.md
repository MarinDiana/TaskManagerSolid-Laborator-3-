# TaskManagerSolid

## Descriere

Aplicatie de gestiune a sarcinilor dezvoltata in C# (.NET), care demonstreaza utilizarea principiilor SOLID, arhitectura pe straturi si design pattern-uri moderne.

Aplicatia permite:
- adaugarea sarcinilor
- completarea sarcinilor
- stergerea sarcinilor
- notificarea utilizatorului la anumite actiuni

---

## Arhitectura

Proiectul este structurat pe mai multe straturi:

- Models → entitati si mostenire (TaskItem, RecurringTask, DeadlineTask)
- Services → logica de business (TaskService, TaskValidator)
- Repositories → acces la date (InMemoryTaskRepository, SQLiteTaskRepository)
- Notifications → sistem de notificari extensibil
- Tests → teste unitare cu NUnit

---

## Principii SOLID

### SRP (Single Responsibility Principle)
Fiecare clasa are o singura responsabilitate:
- TaskService gestioneaza logica
- TaskValidator valideaza
- Repository gestioneaza datele

---

### OCP (Open Closed Principle)
Sistemul este deschis pentru extensie si inchis pentru modificare:
- adaugarea unui nou notifier (ex: SlackNotifier) nu modifica codul existent

---

### LSP (Liskov Substitution Principle)
Clasele derivate (RecurringTask, DeadlineTask) pot inlocui clasa de baza TaskItem fara a modifica comportamentul sistemului.

---

### DIP (Dependency Inversion Principle)
TaskService depinde de interfete (ITaskRepository, ITaskNotifier), nu de implementari concrete.

---

## Functionalitati

- Task standard, recurring si deadline
- Validare task-uri
- Notificari multiple (Console, Email, File, Slack)
- Persistenta in SQLite
- Vizualizare date in baza de date

---

## Baza de date

Se foloseste SQLite.

Fisierul `tasks.db` este creat automat la rulare.

Tabela `Tasks` contine:
- Id
- Title
- Description
- Status
- Priority
- TaskType
- NotificationType
- DueDate
- RecurrenceInterval
- CreatedAt

---

## Testare

Proiectul contine teste unitare folosind NUnit.

Testele verifica:
- adaugare task
- validare task
- completare task
- stergere task
- comportament pentru RecurringTask si DeadlineTask

Toate testele trec cu succes.

---

## Rulare proiect

1. Deschide solutia in Visual Studio
2. Ruleaza proiectul
3. Se vor afisa task-urile in consola
4. Se creeaza automat baza de date SQLite

---

## Tehnologii utilizate

- C# (.NET)
- SQLite
- NUnit
- Visual Studio

## SOLID Principles (Lab 4)

### SRP (Single Responsibility Principle)
Fiecare clasa are o singura responsabilitate:
- TaskService gestioneaza logica de business
- TaskValidator valideaza datele
- Repository gestioneaza accesul la date

---

### OCP (Open Closed Principle)
Sistemul este deschis pentru extensie si inchis pentru modificare:
- se pot adauga noi tipuri de notificari (ex: SlackNotifier) fara modificarea codului existent

---

### LSP (Liskov Substitution Principle)
Clasele derivate (RecurringTask, DeadlineTask) pot inlocui clasa de baza TaskItem fara a afecta functionalitatea.

---

### ISP (Interface Segregation Principle)
Interfata ITaskRepository a fost separata in:
- ITaskReader (GetAll, GetById)
- ITaskWriter (Add, Update, Delete)

Astfel, clasele folosesc doar metodele de care au nevoie.

Exemplu:
```csharp
public class ReportService
{
    private readonly ITaskReader reader;
}

## Concluzie

Aplicatia respecta toate principiile SOLID si demonstreaza o arhitectura modulara, extensibila si usor de intretinut.