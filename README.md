# [PJWSTK_APBD_T2] Wypożyczalnia Sprzętu - Projekt w C#

Projekt obiektowy w C# - który realizuje oraz demonstruje system uczelnianej wypożyczalni sprzętu.

## Instrukcja uruchomienia (IDE)

1. Otwórz rozwiązanie (`PJWSTK_APBD_T2.sln`) w środowisku IDE (preferowane: JetBrains Rider - w nim robiłem).
2. Ustaw typ projektu `PJWSTK_APBD_T2` jako konsolowy projekt startowy.
3. Uruchom program (Run). Aplikacja automatycznie wykona `Program.cs` - oraz znajdujący się w nim scenariusz
   demonstracyjny.

## Instrukcja uruchomienia (CLI)

1. Otwórz terminal i przejdź do katalogu projektu (tzn. tam gdzie znajduje się `PJWSTK_APBD_T2.csproj`).
2. Uruchom polecenie:
   ```
   dotnet build
   ```
   aby zbudować projekt.
3. Następnie uruchom polecenie:
   ```
   dotnet run
   ```
   aby uruchomić aplikację. Program wykona `Program.cs` - oraz znajdujący się w nim scenariusz demonstracyjny.

## Architektura i Decyzje Projektowe

Projekt został podzielony na: **Models** (modele), **Services** (logikę) oraz **Program.cs** (skrypt testowy). Podział
ten gwarantuje czytelność oraz zgodnie z założeniami pozbywamy się działania na jednym pliku.

# Credits

- Autor: Adam Kalinowski
- Numer Indeksu: s31250