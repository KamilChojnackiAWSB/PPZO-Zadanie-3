**Opis działania aplikacji**

```Aplikacja przedstawia system parkingu wielopoziomowego. Parking składa się z dwóch pięter oraz dwóch stref na każdym piętrze - wschodnia i zachodnia. Użytkownik może dodawać pojazdy takie jak samochody i motocykle, usuwać je na podstawie numeru rejestracyjnego oraz przeglądać stan parkingu. Każda strefa ma limit 20 miejsc parkingowych, a program pokazuje liczbę wolnych miejsc.```

<br/>

**Klasy**

* **Pojazd**
  
  Typ klasy: Abstrakcyjna

  Odpowiedzialność: Reprezentuje ogólny pojazd.
  
  Właściwości:

  - NumerRejestracyjny – unikalny identyfikator pojazdu (zapisywany dużymi literami)
  - Marka – marka pojazdu
  - CzyZaparkowany – informacja czy pojazd znajduje się na parkingu

  Metody:

  - Zaparkuj() – zmienia stan pojazdu na zaparkowany
  - Odjedz() – usuwa pojazd z parkingu zmieniając jego stan
  - TypPojazdu() – metoda abstrakcyjna określająca typ pojazdu


* **Samochod**
 
  Dziedziczy po klasie Pojazd
  
  Odpowiedzialność: Reprezentuje samochód.
  Metody:

  - TypPojazdu() - zwraca typ pojazdu jako ,,Samochód"


* **Motocykl**
  
  Dziedziczy po klasie Pojazd
  
  Odpowiedzialność: Reprezentuje motocykl.
  
  Metody:

  - TypPojazdu() - zwraca typ pojazdu jako ,,Motocykl"


* **Parking**
  
  Odpowiedzialność: Zarządza pojazdami oraz organizacją miejsc parkingowych.

  Właściwości:
  - miejsca – słownik przechowujący listy pojazdów w strefach:
  <br/>
  
  >
  >P1-W (piętro 1 wschód)
  >
  >P1-Z (piętro 1 zachód)
  >
  >P2-W (piętro 2 wschód)
  >
  >P2-Z (piętro 2 zachód)


  - limit – maksymalna liczba pojazdów w jednej strefie to 20

  Metody:

  - DodajPojazd() – dodaje pojazd do wybranej strefy jeśli są wolne miejsca
  - UsunPojazd() – usuwa pojazd na podstawie numeru rejestracyjnego
  - PokazPojazdy() – wyświetla pojazdy z podziałem na strefy
  - WolneMiejsca() – zwraca liczbę wolnych miejsc w danej strefie


* **Program**
  
  Odpowiedzialność: Obsługuje menu użytkownika i steruje działaniem programu.

<br/><br/>

**Relacje między klasami**
  
  * Parking -> Pojazd
    
    Relacja kolekcji (agregacji) – parking przechowuje listy pojazdów


  * Samochod, Motocykl -> Pojazd
    
      Relacja dziedziczenia


  * Program -> Parking
    
    Relacja użycia która tworzy i wykorzystuje obiekt parkingu


  * Pojazd używany jako parametr
    
      Np. w metodzie DodajPojazd

<br/><br/>

**Zasady OOP**
  
  - Enkapsulacja

    Właściwości mają private set. Dane pojazdu nie mogą być zmieniane bezpośrednio


 - Dziedziczenie

    Samochod i Motocykl dziedziczą po klasie Pojazd


 - Polimorfizm

    Metoda TypPojazdu() działa inaczej w zależności od klasy:

    Samochód - "Samochód"
  
    Motocykl - "Motocykl"


- Abstrakcja

    Użycie klasy abstrakcyjnej Pojazd. Wymuszenie implementacji metody TypPojazdu()
