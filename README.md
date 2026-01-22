# System Biletów Lotniczych

## Opis projektu
Nasz projekt ma za zadanie obsłużyć system kupowania biletów. Można w nim uzupełnić dane kupującego, szczegóły podróży oraz dodać dodatkowe usługi.
System przelicza cenę, sprawdza dostępność miejsc w samolocie i rezerwuje miejsce.

---

## Technologie, Architektura Systemu

* Platforma: .NET 8.0
* Język: C# 12.0
* Baza Danych: SQL Server LocalDB
* Serializacja: XML (System.Xml.Serialization)
* ORM: Entity Framework 6 

## Instrukcja Instalacji i Uruchomienia

1.**Wymagania:** 
* VisualStudio 2022 (lub nowsze) z zainstalowanym ".NET desktop development"  
* .NET 8.0 
* SQL Server Express LocalDB

2.**Pobranie projektu**
* Sklonuj repozytorium na swój dysk lokalny
* Albo pobierz archiwum ZIP i wypakuj je

3.**Konfiguracja Bazy Danych** 
* Otwórz Package Manager Studio i wykonaj aktualizację bazy poprzez wpisanie komendy: Update-Databse

4.**Przywracanie pakietów**
* Przy pierwszym budowaniu VisualStudio powinno automatycznie pobrać brakujące pakiety NuGet
* Jeżeli tak się nie stanie wybierz opcję Restore NuGet Packages po kliknięciu prawym przyciskiem myszy na rozwiązanie

5.**Możesz uruchomić projekt :D**

## Główne funkcjonalności
* Zarządzanie biiletami, obsługa kilka zróżnicowanych typów biletów, wykorzystując mechanizm dziedziczenia z klasy bazowej Bilet
* Inteligetny system obliczania cen, uwzględnia sezonowość, klasy, usługi dodatkowe, system zniżek wykorzystujący delegat
* Przechowywanie danych albo w bazie danych przy użyciu Entity Framework, albo do pliku .xml używając serializacje Xml
* Opcje sortowania listy biletów
