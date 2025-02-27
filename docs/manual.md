# Anleitung

1. [Start der App](#start-der-app)
2. [Neues Todo erstellen](#neues-todo-erstellen)
3. [Todo bearbeiten](#todo-bearbeiten)
4. [Todo löschen](#todo-löschen)
5. [JSON Export](#json-export)

## Start der App

Beim Start der App werden automatisch alle gespeicherten Todos geladen und in der Tabelle dargestellt

![app_on_start](./images/app_on_start.png)

## Neues Todo erstellen

Über die Funktion 'Neues Todo erstellen' öffnet sich folgende Maske, in der die Eigenschaften des neues Todos bearbeitet werden können

![new_todo_open](./images/new_todo_open.png)

Beim speichern erscheint dann folder Dialog, mit folgenden Optionen

![new_todo_on_close](./images/new_todo_on_close.png)

- **Ja:** Das Todo wird erstellt und die Maske wird geschlossen
- **Nein:** Das Todo wird **nicht** erstellt und die Maske wird geschlossen
- **Abbrechen:** Der Dialog schliesst sich und man kan das Todo weiterbearbeiten

## Todo bearbeiten

Um ein Todo zu bearbeiten muss man die Zeile in der Hauptansicht doppelklicken.
Dann öffnet sich folgende Maske

![todo_open](./images/todo_open.png)

Beim speichern erscheint dann folder Dialog, mit folgenden Optionen

![todo_on_close](./images/todo_on_close.png)

- **Ja:** Das Todo wird gespeichert und die Maske wird geschlossen
- **Nein:** Das Todo wird **nicht** gespeichert und die Maske wird geschlossen
- **Abbrechen:** Der Dialog schliesst sich und man kan das Todo weiterbearbeiten

## Todo löschen

Über die Funktion 'Todo löschen' kann ein existierendes Todo gelöscht werden. \
Nach dem Ausführen der Funktion wird der Benutzer gefragt, ob er sich sicher ist das Todo zu löschen.
Wenn dieser dann mit 'Ja' bestätigt wird das Todo gelöscht.
![app_on_delete](./images/app_on_delete.png)

## JSON Export

Um alle Todos als JSON zu exportieren gibt es die Funtion 'JSON Export'
Nach dem ausführen der Funktion öffnet sich ein Speicherdialog für die JSON-Datei

![app_on_export](./images/app_on_export.png)

Wenn man diesen Dialog nun bestätigt wird die Datei unter dem angegebenen Pfad gespeichert.
