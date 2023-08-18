# HarmonogramMK

HarmonogramMK to prosta aplikacja do planowania zada�. Umo�liwia ona tworzenie zada� uruchamij�cych inne aplikacje o wybranej porze. 
Zadania wykonywane s� automatycznie o okre�lonym przez u�ytkownika czasie. Aplikacja zapewnia �atwy spos�b dodawania zada�, 
kt�re b�d� wykonane w okre�lonych godzinach, dniach lub co okre�lony czas np. codziennie lub co tydzie�.
![Reference Image](/screenshots/1.png)

## Funkcje

###  Kreator zada�
- **Nazwa zadania**: W tym polu u�ytkownik wprowadza nazw� zadania (max. 14 znak�w).
- **�cie�ka do aplikacji**: W tym polu u�ytkownik mo�e r�cznie wprowadzi� �cie�k� do aplikacji, kt�ra po okre�lonym czasie 
powinna si� uruchomi� lub mo�e wybra� �cie�k� u�ywaj�c eksploratora plik�w klikaj�c w przycisk *Wybierz*.
- **Data uruchomienia**: W tym polu u�ytkownik u�ywaj�c kontrolki z ikon� kalendarza mo�ew wybra� dat� wykonania zadania.
- **Godzina/minuty**: U�ytkownik mo�e wprowadzi� tu por�(godziny i minuty) wykonania zadania.
- **Kiedy ma by� uruchamiane zadanie?**: U�ytkownik mo�e wybra� tu typ wykonywania zadania.
- **Rodzaje Zada�**: Dost�pne zadania to **Jednorazowe** - zadanie wykona si� jeden raz po czym zostanie usuni�te, 
 je�eli aplikacja nie jest w��czona o porze o kt�rej zadanie powinno si� wykona� zadanie pozostanie w li�cie aktywnych zada� (ze zmienionym kolorem i komunikatem), 
 aby u�ytkownik wiedzia�, �e takie zadanie si� nie wykona�o. U�ytkownik mo�e je usun��.
**Codzienne** zadanie b�dzie wykonywa� si� codziennie o tej samej porze dop�ki u�ytkownik go nie anuluje.
**Cotygodniowe** zadanie b�dzie wykonywa� si� co tydzie� o tej samej porze dop�ki u�ytkownik go nie anuluje.
- **Utw�rz zadanie**: przycisk umo�liwia dodanie nowego zadania.
![Reference Image](/screenshots/2.png)

###  Podgl�d zada�
- **Aktywne zadania**: W tym oknie widoczne s� wszystkie aktywne zadania oraz takie, kt�re 
nie mog�y wykona� si� ze wzgl�du na niedost�pno�� aplikacji w czasie w kt�rym powinny si� uruchomi�. 
U�ytkownik ma tu r�wnie� mo�liwo�� anulowania zadania lub usuni�cia(nie wykonane zadania).
![Reference Image](/screenshots/3.png)

###  Inne funkcjonalno�ci
- **Automatyczny zapis**: Aplikacja automatycznie zapisuje zadania, nawet po zamkni�ciu programu.
- **Ustawienia**: Przycisk umo�liwia przej�cie do okna wyboru/zmiany ustawie� aplikacji.
U�ytkownik mo�e tu zmieni� folder zapisu zada� lub w��czy�/wy��czy� mo�liwo�� uruchamiania aplikacji wraz ze startem systemu.
- **Minimalizacja/praca w tle**: po klikni�ciu minimalizuj aplikacja minimalizuje si�, a jej ikona pojawia si� w zasobniku systemowym(system tray).
Podw�jne klikni�cie na ikon� pozwala otworzy� okno aplikacji z powrotem. 
![Reference Image](/screenshots/5.png)

## Jak pobra�?

1. **Pobranie**: Pobierz najnowsz� wersj� HarmonogramMK z [Releases](https://github.com/your-username/HarmonogramMK/releases).
2. **Instalacja**: Wystarczy uruchomi� pobrany plik wykonywalny, aby otworzy� aplikacj�.

## U�ycie

- **Dodawanie Zadania**: Wprowad� nazw� zdarzenia, wybierz docelow� aplikacj�, wybierz rodzaj zdarzenia (Jednorazowe, Dziennie, Tygodniowo) i ustaw dat� oraz godzin�. Kliknij przycisk "Utw�rz zadanie".
- **Usuwanie zadania**: Kliknij przycisk "Usu�" obok zadania, aby je usun�� z listy.
- **Anulowanie zadania**: Kliknij przycisk "Anuluj" obok zadania, aby zatrzyma� jego wykonanie i usun�� z listy.
- **Powiadomienia**: Aplikacja wy�wietli powiadomienie, je�eli otwarcie aplikacji nie powiedzie si�, je�eli si� powiedzie aplikacja zostanie uruchomiona bez powiadamiania o tym u�ytkownika.

## Uwaga!
Aplikacj� najlepiej uruchamia� jako administrator, gdy� brak praw do niekt�rych aplikacji lub folder�w mo�e spowodowa�, �e aplikacje si� nie uruchomi�, aplikacja zosta�a zabezpieczona i u�ytkownik powinien otrzyma� stosowne komunikaty o braku praw do folder�w lub aplikacji.

