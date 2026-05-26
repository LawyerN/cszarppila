# Aplikacja webowa do wyświetlania oraz zarządzania danymi dotyczącymi rozgrywek LaLiga

### Autorzy
- Kacper Filip
- Bartosz Gdowski

### Do czego służy aplikacja
Aplikacja służy od wyświetlania (normalni użytkownicy) oraz zarządzania (admin) danymi rozgrywek LaLiga. Dane te są dostępne wyłącznie dla zalogowanych użytkowników. Pozwala ona na dostęp do danych:
- statystyk zawodników (gole, asysty),
- statystyk stadionów (gole na stadionach, mecze odbyte na stadionach),
- statystyk sędziów (sędziowane spotkania),
- terminarza spotkań (spotkania, ich wyniki, drużyny biorące udział, data spotkania, stadion).

### Funkcjonalności aplikacji
Aplikacja oferuje następujące główne funkcjonalności:

Zarządzanie bazą danych (Dashboard): 
 * Użytkownik po zalogowaniu ma dostęp do dashboardu, na którym wyświetlane są zestawienia najlepszych strzelców, asystentów oraz statystyki stadionów i sędziów.
 * System umożliwia przeglądanie listy wszystkich zarejestrowanych meczów, zawodników, drużyn, stadionów i sędziów.

CRUD (Create, Read, Update, Delete):
 * Aplikacja posiada pełny interfejs do dodawania, edytowania i usuwania danych dotyczących drużyn, zawodników oraz stadionów i sędziów.

API RESTful:
 * Udostępnia interfejs API (w kontrolerze TeamsApiController), który pozwala na operacje na danych drużyn (GET, POST, PUT, DELETE) przy użyciu obiektów typu DTO (Data Transfer Object) dla zwiększenia bezpieczeństwa.
 * Dostęp do API jest chroniony przez własny filtr autoryzacji [ApiAuth].

Autoryzacja i sesja:
 * System wykorzystuje ciasteczka (Cookies) do zarządzania sesją użytkownika.
 * Większość kontrolerów (HomeController, TeamsController, PlayersController, MatchesController) wymaga autoryzacji.

Inicjalizacja danych:
 * Podczas uruchamiania aplikacji, w pliku Program.cs uruchamiany jest mechanizm DbSeeder, który dba o utworzenie bazy danych oraz automatyczne utworzenie konta administratora (z możliwością konfiguracji nazwy użytkownika i hasła w appsettings.json).
