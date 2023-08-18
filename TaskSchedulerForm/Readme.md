# HarmonogramMK

HarmonogramMK to prosta aplikacja do planowania zadañ. Umo¿liwia ona tworzenie zadañ uruchamij¹cych inne aplikacje o wybranej porze. 
Zadania wykonywane s¹ automatycznie o okreœlonym przez u¿ytkownika czasie. Aplikacja zapewnia ³atwy sposób dodawania zadañ, 
które bêd¹ wykonane w okreœlonych godzinach, dniach lub co okreœlony czas np. codziennie lub co tydzieñ.
![Reference Image](/screenshots/1.png)

## Funkcje

###  Kreator zadañ
- **Nazwa zadania**: W tym polu u¿ytkownik wprowadza nazwê zadania (max. 14 znaków).
- **Œcie¿ka do aplikacji**: W tym polu u¿ytkownik mo¿e rêcznie wprowadziæ œcie¿kê do aplikacji, która po okreœlonym czasie 
powinna siê uruchomiæ lub mo¿e wybraæ œcie¿kê u¿ywaj¹c eksploratora plików klikaj¹c w przycisk *Wybierz*.
- **Data uruchomienia**: W tym polu u¿ytkownik u¿ywaj¹c kontrolki z ikon¹ kalendarza mo¿ew wybraæ datê wykonania zadania.
- **Godzina/minuty**: U¿ytkownik mo¿e wprowadziæ tu porê(godziny i minuty) wykonania zadania.
- **Kiedy ma byæ uruchamiane zadanie?**: U¿ytkownik mo¿e wybraæ tu typ wykonywania zadania.
- **Rodzaje Zadañ**: Dostêpne zadania to **Jednorazowe** - zadanie wykona siê jeden raz po czym zostanie usuniête, 
 je¿eli aplikacja nie jest w³¹czona o porze o której zadanie powinno siê wykonaæ zadanie pozostanie w liœcie aktywnych zadañ (ze zmienionym kolorem i komunikatem), 
 aby u¿ytkownik wiedzia³, ¿e takie zadanie siê nie wykona³o. U¿ytkownik mo¿e je usun¹æ.
**Codzienne** zadanie bêdzie wykonywaæ siê codziennie o tej samej porze dopóki u¿ytkownik go nie anuluje.
**Cotygodniowe** zadanie bêdzie wykonywaæ siê co tydzieñ o tej samej porze dopóki u¿ytkownik go nie anuluje.
- **Utwórz zadanie**: przycisk umo¿liwia dodanie nowego zadania.
![Reference Image](/screenshots/2.png)

###  Podgl¹d zadañ
- **Aktywne zadania**: W tym oknie widoczne s¹ wszystkie aktywne zadania oraz takie, które 
nie mog³y wykonaæ siê ze wzglêdu na niedostêpnoœæ aplikacji w czasie w którym powinny siê uruchomiæ. 
U¿ytkownik ma tu równie¿ mo¿liwoœæ anulowania zadania lub usuniêcia(nie wykonane zadania).
![Reference Image](/screenshots/3.png)

###  Inne funkcjonalnoœci
- **Automatyczny zapis**: Aplikacja automatycznie zapisuje zadania, nawet po zamkniêciu programu.
- **Ustawienia**: Przycisk umo¿liwia przejœcie do okna wyboru/zmiany ustawieñ aplikacji.
U¿ytkownik mo¿e tu zmieniæ folder zapisu zadañ lub w³¹czyæ/wy³¹czyæ mo¿liwoœæ uruchamiania aplikacji wraz ze startem systemu.
- **Minimalizacja/praca w tle**: po klikniêciu minimalizuj aplikacja minimalizuje siê, a jej ikona pojawia siê w zasobniku systemowym(system tray).
Podwójne klikniêcie na ikonê pozwala otworzyæ okno aplikacji z powrotem. 
![Reference Image](/screenshots/5.png)

## Jak pobraæ?

1. **Pobranie**: Pobierz najnowsz¹ wersjê HarmonogramMK z [Releases](https://github.com/your-username/HarmonogramMK/releases).
2. **Instalacja**: Wystarczy uruchomiæ pobrany plik wykonywalny, aby otworzyæ aplikacjê.

## U¿ycie

- **Dodawanie Zadania**: WprowadŸ nazwê zdarzenia, wybierz docelow¹ aplikacjê, wybierz rodzaj zdarzenia (Jednorazowe, Dziennie, Tygodniowo) i ustaw datê oraz godzinê. Kliknij przycisk "Utwórz zadanie".
- **Usuwanie zadania**: Kliknij przycisk "Usuñ" obok zadania, aby je usun¹æ z listy.
- **Anulowanie zadania**: Kliknij przycisk "Anuluj" obok zadania, aby zatrzymaæ jego wykonanie i usun¹æ z listy.
- **Powiadomienia**: Aplikacja wyœwietli powiadomienie, je¿eli otwarcie aplikacji nie powiedzie siê, je¿eli siê powiedzie aplikacja zostanie uruchomiona bez powiadamiania o tym u¿ytkownika.

## Uwaga!
Aplikacjê najlepiej uruchamiaæ jako administrator, gdy¿ brak praw do niektórych aplikacji lub folderów mo¿e spowodowaæ, ¿e aplikacje siê nie uruchomi¹, aplikacja zosta³a zabezpieczona i u¿ytkownik powinien otrzymaæ stosowne komunikaty o braku praw do folderów lub aplikacji.

