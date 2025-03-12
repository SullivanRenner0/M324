# Anleitung API

- [Anleitung API](#anleitung-api)
  - [Basis-URL](#basis-url)
  - [API-Version](#api-version)
  - [Ressourcen](#ressourcen)
    - [`TodoStatus`](#todostatus)
    - [`TodoPriority`](#todopriority)
    - [`ToDo`](#todo)
  - [Endpunkte](#endpunkte)
    - [1. Alle ToDos abrufen](#1-alle-todos-abrufen)
      - [Anfrage](#anfrage)
      - [Antwort](#antwort)
      - [Beispiel](#beispiel)
    - [2. Einzelnes ToDo abrufen](#2-einzelnes-todo-abrufen)
      - [Anfrage](#anfrage-1)
      - [Antwort](#antwort-1)
      - [Beispiel](#beispiel-1)
    - [3. Neues ToDo erstellen](#3-neues-todo-erstellen)
      - [Anfrage](#anfrage-2)
      - [Antwort](#antwort-2)
      - [Beispielanfrage](#beispielanfrage)
    - [4. ToDo aktualisieren](#4-todo-aktualisieren)
      - [Anfrage](#anfrage-3)
      - [Antwort](#antwort-3)
      - [Beispielanfrage](#beispielanfrage-1)
    - [5. ToDo löschen](#5-todo-löschen)
      - [Anfrage](#anfrage-4)
      - [Antwort](#antwort-4)
  - [Fehlercodes](#fehlercodes)

---

## Basis-URL

```
https://todo-app-dvdbhkfff6avgxbu.switzerlandnorth-01.azurewebsites.net
```

## API-Version

Diese API verwendet Versionierung: `v1`

## Ressourcen

### `TodoStatus`

| Wert          | Bedeutung                           |
| ------------- | ----------------------------------- |
| `Open`        | Die Aufgabe ist noch offen.         |
| `In_Progress` | Die Aufgabe wird gerade bearbeitet. |
| `Done`        | Die Aufgabe ist abgeschlossen.      |

### `TodoPriority`

| Wert     | Bedeutung          |
| -------- | ------------------ |
| `Low`    | Niedrige Priorität |
| `Medium` | Mittlere Priorität |
| `High`   | Hohe Priorität     |

### `ToDo`

Ein ToDo-Element hat folgende Eigenschaften:

| Feld          | Typ            | Beschreibung                                       |
| ------------- | -------------- | -------------------------------------------------- |
| `Id`          | `long`         | Eindeutige ID der ToDo-Aufgabe                     |
| `Header`      | `string`       | Titel der ToDo-Aufgabe                             |
| `Description` | `string`       | Beschreibung der Aufgabe                           |
| `Status`      | `TodoStatus`   | Status der Aufgabe (`Open`, `In_Progress`, `Done`) |
| `Priority`    | `TodoPriority` | Priorität (`Low`, `Medium`, `High`)                |
| `CreatedAt`   | `DateTime`     | Erstellungsdatum                                   |
| `Deadline`    | `DateTime?`    | Optionales Fälligkeitsdatum                        |

---

## Endpunkte

### 1. Alle ToDos abrufen

**GET** `/api/v1/todos`

#### Anfrage

- Keine Parameter erforderlich.

#### Antwort

- **200 OK**: Gibt eine Liste aller ToDo-Elemente zurück.

#### Beispiel

```json
[
  {
    "id": 1,
    "header": "Einkaufen gehen",
    "description": "Milch und Brot kaufen",
    "status": "Open",
    "priority": "Medium",
    "createdAt": "2025-03-12T10:00:00Z",
    "deadline": "2025-03-15T18:00:00Z"
  }
]
```

---

### 2. Einzelnes ToDo abrufen

**GET** `/api/v1/todos/{id}`

#### Anfrage

| Parameter | Typ    | Beschreibung         |
| --------- | ------ | -------------------- |
| `id`      | `long` | ID des ToDo-Elements |

#### Antwort

- **200 OK**: Gibt das ToDo-Element zurück.
- **404 Not Found**: Wenn kein ToDo mit der ID existiert.

#### Beispiel

```json
{
  "id": 1,
  "header": "Einkaufen gehen",
  "description": "Milch und Brot kaufen",
  "status": "Open",
  "priority": "Medium",
  "createdAt": "2025-03-12T10:00:00Z",
  "deadline": "2025-03-15T18:00:00Z"
}
```

---

### 3. Neues ToDo erstellen

**POST** `/api/v1/todos/create`

#### Anfrage

Das vollständige `ToDo`-Objekt muss gesendet werden. Dabei werden die Felder `id` und `createdAt` von der API ignoriert, selbst wenn sie in der Anfrage enthalten sind.

#### Antwort

- **201 Created**: ToDo wurde erfolgreich erstellt.
- **400 Bad Request**: Fehlerhafte Anfrage oder fehlende Felder.

#### Beispielanfrage

```json
{
  "header": "Projekt abschließen",
  "description": "Finale Tests durchführen und Deployment vorbereiten",
  "status": "In_Progress",
  "priority": "High",
  "deadline": "2025-03-20T23:59:59Z"
}
```

---

### 4. ToDo aktualisieren

**PUT** `/api/v1/todos/update`

#### Anfrage

Das vollständige `ToDo`-Objekt muss gesendet werden. Dabei werden die Felder `id` und `createdAt` von der API ignoriert, selbst wenn sie in der Anfrage enthalten sind.

#### Antwort

- **200 OK**: ToDo wurde aktualisiert.
- **400 Bad Request**: Falls die Aktualisierung fehlschlägt.

#### Beispielanfrage

```json
{
  "id": 1,
  "header": "Einkaufen gehen",
  "description": "Milch, Brot und Eier kaufen",
  "status": "Open",
  "priority": "Medium",
  "deadline": "2025-03-16T18:00:00Z"
}
```

---

### 5. ToDo löschen

**DELETE** `/api/v1/todos/{id}`

#### Anfrage

| Parameter | Typ    | Beschreibung         |
| --------- | ------ | -------------------- |
| `id`      | `long` | ID des ToDo-Elements |

#### Antwort

- **204 No Content**: Erfolgreich gelöscht.
- **404 Not Found**: Wenn kein ToDo mit dieser ID existiert.

---

## Fehlercodes

| Statuscode      | Bedeutung                      |
| --------------- | ------------------------------ |
| 200 OK          | Anfrage erfolgreich            |
| 201 Created     | Ressource erfolgreich erstellt |
| 204 No Content  | Erfolgreich gelöscht           |
| 400 Bad Request | Fehlerhafte Anfrage            |
| 404 Not Found   | Ressource nicht gefunden       |

---
