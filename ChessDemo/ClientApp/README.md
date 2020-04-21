
Ten projekt został utworzony w oparciu prekonfigurowany szablon dla aplikacji React:  https://github.com/facebookincubator/create-react-app


## Frontend
Do budowy aplikacji klienckiej posłużyłem się biblioteką React, gdyż znakomicie ułatwia zapanowanie nad stanem po stronie klienta
jednocześnie zaś jest stosunkowo wygodna i prosta w użyciu. Widok szachownicy jest generowany wyłącznie w oparciu o stan komponentu - 
stanem elementów DOM i jego modyfikacja odbywa się w ramach cyklu życia komponentu w reakcji na zdarzenia, które wymuszają zmianę stanu komponentu. 
Komponent przechowuje obrazy figur w formacie .png, co pozwala skorzystać z transparentnego tła. 
Zrezygnowałem ze stoowania obrazów do zaznaczania pól. Pola dostępne dla figury są zaznaczanie poprzez style css. Silnik przeglądarki renderuje kolory tła znacznie szybciej niż obrazy, 
więc jest mniej obciążająca technika.
Frontend jest jedyną warstwą przechowującą stan aplikacji, którego najważniejszymi filarami są: wybrana figura, pozycja figury, pola dostępne dla ruchu.
Do komunikacji z backendem zastosowałem klasyczne rozwiązanie oparte na obiektach typu XMLHttpRequest. 
Wynika to z faktu, że ten typ obiektu (w przeciwieństwie do wygodniejszego interfejsu jQuery) był dostępny "od ręki" na poziomie komponentu, 
bez konieczności importowania czegokolwiek z zewnątrz. Przy użyciu tych obiektów frontend pobiera listę dostępnych figur dla węzła DOM typu 'SELECT',
tablicę współrzędnych pól w zasięgu wybranej figury, a także sprawdza ruch który chce wykonać użytkownik. 

## Backend
Backend nie zawiera kontrolerów zwracających widoki. Kontrolery definiują API złożone wyłącznie z metod typu GET,
gdyż frontend wykonuje operacje ajax wyłacznie w celu pobrania niezbędnych danych. 
Backend nie przechowuje stanu aplikacji w słowniku sesji - rezultaty obsługi żądań zależą wyłącznie od parametrów wejściowych.
Aby ułatwić rozbudowę modelu zastosowałem fabrykę abstrakcyjną, gdzie wartością sterującą jest nazwa typu. 
Fabryka dostarcza implementacji w postaci klas pochodnych, dziedziczących po klasie abstrakcyjnej Figure.
Ponieważ obiekty to nie przechowują istotnego stanu aplikacji, a jedynie dostarczają implementacji metod, 
tę hierarchię klas można równie skutecznie zastąpić interfejsem. 
Implementując metody dostarczające dane zdecydowałem się na przekazywanie parametrów wyłącznie poprzez querystring.
W przypadku metod GET podejście to ma pewną zaletę: z łatwością można sprawdzać działanie API przy użyciu URL wklejonego w oknie przegądarki, 
bez konieczności posiadania ukończonej aplikacji klienckiej. 






