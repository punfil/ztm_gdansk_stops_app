# ztm_gdansk_stops_app
Shows data about tram/bus stops in Trojmiasto PL.

Proszę zaimplementować prostą aplikację pozwalającą na
zalogowanie się użytkownika. Po zalogowaniu wyświetla użytkownikowi listę wybranych przez niego
tablic przystankowych (osobna dla każdego użytkownika). Tablica przystankowa zawiera listę
niebawem odjeżdżających z tego przystanku, pojazdów ZTM. Użytkownik powinien mieć możliwość
dodania i usunięcia przystanków z listy.

## TODOs:
### Backend:
- Wykorzystanie ORM
- Uwierzytelnianie/autoryzacja używając JWT

### Frontend:
- Funkcjonalny podział GUI na komponenty jedno-plikowe.
- Implementacja przynajmniej jednego multikomponentu.
- Jeden bądź więcej komponentów zawieranych w multikomponentach powinny posiadać powiązane dwukierunkowo zmienne – dyrektywa v-on.
- Wykorzystanie dyrektywy v-bind:class lub v-bind:style.
- Wykorzystać przynajmniej 2 wtyczki (np. omówiona w instrukcji:
	- vue-good-table,
	- vue-resource pozwalająca na łatwe konstruowanie żądań internetowych i obsługi odpowiedzi za pomocą XMLHttpRequest lub JSONP i współpracująca np. z json-serwer – prostym serwerem HTTP przechowywującego dane w plikach JSON .)
- Implementacja magazynu Vuex.
- Implementacje i użycie własnej wtyczki.
- Własne testy jednostkowe.
- Własne testy E2E
