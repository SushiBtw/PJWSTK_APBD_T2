# [PJWSTK_APBD_T2] Wypożyczalnia Sprzętu - Projekt w C#

Projekt obiektowy w C# - który realizuje oraz demonstruje system uczelnianej wypożyczalni sprzętu.

## Instrukcja pobierania

1. Pobierz projekt z repozytorium git:
   ```
   git clone https://github.com/SushiBtw/PJWSTK_APBD_T2.git
   ```
2. Przejdź do katalogu projektu:
   ```
   cd PJWSTK_APBD_T2
   ```

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

### 1. Kohezja

Każda klasa ma jasno określoną odpowiedzialność.

- **Models**: Zawierają tylko dane i ewentualnie proste metody pomocnicze.
- **Services**: Zawierają logikę, operacje na danych oraz zarządzanie stanem. Np: `RentalService` zarządza
  wypożyczeniami, a `FeeCalculator` oblicza opłaty.

### 2. Coupling

Klasy są luźno powiązane. Na przykład, `RentalService` korzysta z `FeeCalculator`, ale nie jest od niego zależny. Dzięki
temu można łatwo wymienić implementację `FeeCalculator` bez konieczności modyfikowania
`RentalService`.

### 3. Dziedziczenie i Polimorfizm

Zastosowałem dziedziczenie (domenowe). Zamiast sprawdzać za pomocą instrukcji `if` typ użytkownika podczas wypożyczania,
klasy `Student` i `Employee` same definiują swoją właściwość `MaxActiveRentals` (2 i 5). Pozwoliło to na
eleganckie wykorzystanie polimorfizmu do walidacji limitów.

# Credits

- Autor: Adam Kalinowski
- Numer Indeksu: s31250