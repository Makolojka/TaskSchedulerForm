# HarmonogramMK

HarmonogramMK to prosta aplikacja do planowania zadań. Umożliwia ona tworzenie zadań uruchamijących inne aplikacje o wybranej porze. 
Zadania wykonywane są automatycznie o określonym przez użytkownika czasie. Aplikacja zapewnia łatwy sposób dodawania zadań, 
które będą wykonane w określonych godzinach, dniach lub co określony czas np. codziennie lub co tydzień.
![Reference Image](/screenshots/1.png)

## Funkcje

###  Kreator zadań
- **Nazwa zadania**: W tym polu użytkownik wprowadza nazwę zadania (max. 14 znaków).
- **Ścieżka do aplikacji**: W tym polu użytkownik może ręcznie wprowadzić ścieżkę do aplikacji, która po określonym czasie 
powinna się uruchomić lub może wybrać ścieżkę używając eksploratora plików klikając w przycisk *Wybierz*.
- **Data uruchomienia**: W tym polu użytkownik używając kontrolki z ikoną kalendarza możew wybrać datę wykonania zadania.
- **Godzina/minuty**: Użytkownik może wprowadzić tu porę(godziny i minuty) wykonania zadania.
- **Kiedy ma być uruchamiane zadanie?**: Użytkownik może wybrać tu typ wykonywania zadania.
- **Rodzaje Zadań**: Dostępne zadania to **Jednorazowe** - zadanie wykona się jeden raz po czym zostanie usunięte, 
 jeżeli aplikacja nie jest włączona o porze o której zadanie powinno się wykonać zadanie pozostanie w liście aktywnych zadań (ze zmienionym kolorem i komunikatem), 
 aby użytkownik wiedział, że takie zadanie się nie wykonało. Użytkownik może je usunąć.
**Codzienne** zadanie będzie wykonywać się codziennie o tej samej porze dopóki użytkownik go nie anuluje.
**Cotygodniowe** zadanie będzie wykonywać się co tydzień o tej samej porze dopóki użytkownik go nie anuluje.
- **Utwórz zadanie**: przycisk umożliwia dodanie nowego zadania.
![Reference Image](/screenshots/2.png)

###  Podgląd zadań
- **Aktywne zadania**: W tym oknie widoczne są wszystkie aktywne zadania oraz takie, które 
nie mogły wykonać się ze względu na niedostępność aplikacji w czasie w którym powinny się uruchomić. 
Użytkownik ma tu również możliwość anulowania zadania lub usunięcia(nie wykonane zadania).
![Reference Image](/screenshots/3.png)

###  Inne funkcjonalności
- **Automatyczny zapis**: Aplikacja automatycznie zapisuje zadania, nawet po zamknięciu programu.
- **Ustawienia**: Przycisk umożliwia przejście do okna wyboru/zmiany ustawień aplikacji.
Użytkownik może tu zmienić folder zapisu zadań lub włączyć/wyłączyć możliwość uruchamiania aplikacji wraz ze startem systemu.
- **Minimalizacja/praca w tle**: po kliknięciu minimalizuj aplikacja minimalizuje się, a jej ikona pojawia się w zasobniku systemowym(system tray).
Podwójne kliknięcie na ikonę pozwala otworzyć okno aplikacji z powrotem. 
![Reference Image](/screenshots/5.png)

## Jak pobrać?

1. **Pobranie**: Pobierz najnowszą wersję HarmonogramMK z [Releases](https://github.com/your-username/HarmonogramMK/releases).
2. **Instalacja**: Wystarczy uruchomić pobrany plik wykonywalny, aby otworzyć aplikację.

## Użycie

- **Dodawanie Zadania**: Wprowadź nazwę zdarzenia, wybierz docelową aplikację, wybierz rodzaj zdarzenia (Jednorazowe, Dziennie, Tygodniowo) i ustaw datę oraz godzinę. Kliknij przycisk "Utwórz zadanie".
- **Usuwanie zadania**: Kliknij przycisk "Usuń" obok zadania, aby je usunąć z listy.
- **Anulowanie zadania**: Kliknij przycisk "Anuluj" obok zadania, aby zatrzymać jego wykonanie i usunąć z listy.
- **Powiadomienia**: Aplikacja wyświetli powiadomienie, jeżeli otwarcie aplikacji nie powiedzie się, jeżeli się powiedzie aplikacja zostanie uruchomiona bez powiadamiania o tym użytkownika.

## Uwaga!
Aplikację najlepiej uruchamiać jako administrator, gdyż brak praw do niektórych aplikacji lub folderów może spowodować, że aplikacje się nie uruchomią, aplikacja została zabezpieczona i użytkownik powinien otrzymać stosowne komunikaty o braku praw do folderów lub aplikacji.

