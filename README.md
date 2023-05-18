### Uwagi i komentarze

1. Aplikacja składa się z 3 warstw:
   - aplikacji (domena, model danych, implementacje serwisów, wyjatki i kontrakty);
   - infrastruktury (trwałość danych - pobieranie danych z JSON, implementacje serwisów, repozytoria, helpery);
   - WebAPI - minimalistyczne API REST'owe z 3 końcówkami, posegregowanymi w 2 grupy oraz wrapper odpowiedzi API.
2. Domyślnie rozwiązanie byłoby podzielone na min. 3 projekty, ale warstwy posegregowane są obecnie w folderach.
3. W celu wyodrębnienia logiki i mechanizmów odpytywania Proxmox, wydzielono osobny projekt */src/ProxmoxWrapper*, który zawiera prosty wrapper PveClient z obsługą logiki zapytań. Provider jest wstrzykiwany do kontenera zależności jako obiekt *Scoped*, a przy każdym jego konstruowaniu przekazywana jest lista zasobów z bazy danych. Provier zawiera:
   - logikę logowania na różne hosty w obrębie klastra, w celu znalezienia informacji o danym 'nodzie';
   - mechanizm przeszukiwania róznych node'ów oraz klastrów w celu znalezienia maszyny, jeśli ta nie występuje na określonym hoście.
4. Końcówki API korzystają z serwisu IProxmoxService, który jest warstwą pośrednią pomiędzy wrapperem PveClient i warstwą trwałości danych a API. Jego głównym zadaniem jest wstępne znalezienie zasobu i przekazanie do provider'a Proxmox, obsługa odpowiedzi (w tym ewentualnych wyjątków) i konwersja na klasy POCO odpowiedzi.