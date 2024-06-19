# KingICT Middleware API

## Uvod
Zadatak je bio napraviti middleware REST API u .NET tehnologiji kako bi omogućili prikaz i manipuliranje podacima s drugih izvora poput drugog API-ja demonstriranog u ovom zadatku te kasnije različitih baza podataka, RSS feedova i sl.

## Tehnologije 
### .NET
- **.NET Version**: 8
- **ASP.NET Web API**
- **Entity Framework**

## Sadržaj
- [Kontroleri](#kontroleri)
  - [JwtTokensController](#jwttokenscontroller)
  - [UsersController](#userscontroller)
  - [ProductsController](#productscontroller)
- [Servisi](#servisi)
  - [ProductService](#productservice)
- [DTO Modeli](#dto-modeli)
- [Konfiguracija](#konfiguracija)
- [Kako Pokrenuti](#kako-pokrenuti)

## Kontroleri

### JwtTokensController
Ovaj kontroler upravlja stvaranjem JWT tokena.

#### Metode

- `GET api/jwttokens/gettoken`
  - **Opis:** Generira JWT token.
  - **Odgovor:** 
    - `200 OK` sa serijaliziranim JWT tokenom.
    - `500 Internal Server Error` ako se dogodi greška.

### UsersController
Ovaj kontroler upravlja operacijama vezanim za korisnike i zahtijeva autorizaciju.

#### Metode

- `GET api/users`
  - **Opis:** Dohvaća sve korisnike.
  - **Odgovor:** 
    - `200 OK` s popisom korisnika mapiranih u `UserDTO`.
    - `404 Not Found` ako korisnici nisu mogli biti dohvaćeni.

- `POST api/users/login`
  - **Opis:** Autentificira korisnika i pruža JWT token ako je uspješno.
  - **Parametri:** `UserLoginDto` koji sadrži korisničko ime i lozinku.
  - **Odgovor:** 
    - `200 OK` s JWT tokenom.
    - `400 Bad Request` ako je korisničko ime ili lozinka netočna.
    - `500 Internal Server Error` ako se dogodi greška.

### ProductsController
Ovaj kontroler upravlja operacijama vezanim za proizvode i zahtijeva autorizaciju.

#### Metode

- `GET api/products`
  - **Opis:** Dohvaća sve proizvode.
  - **Odgovor:** 
    - `200 OK` s popisom proizvoda mapiranih u `ProductDTO`.
    - `404 Not Found` ako proizvodi nisu mogli biti dohvaćeni.

- `GET api/products/{id}`
  - **Opis:** Dohvaća pojedinačni proizvod prema njegovom ID-u.
  - **Odgovor:** 
    - `200 OK` s proizvodom mapiranim u `ProductDTO`.
    - `404 Not Found` ako proizvod s navedenim ID-om nije pronađen.

- `GET api/products/filter`
  - **Opis:** Filtrira proizvode prema kategoriji i rasponu cijena.
  - **Query Parametri:** `category`, `priceRange` (format: `min-max`).
  - **Odgovor:** 
    - `200 OK` s filtriranim proizvodima.
    - `404 Not Found` ako nijedan proizvod ne odgovara kriterijima filtriranja.
    - `500 Internal Server Error` ako se dogodi greška.

- `GET api/products/search`
  - **Opis:** Pretražuje proizvode prema naslovu.
  - **Query Parametar:** `title`
  - **Odgovor:** 
    - `200 OK` s proizvodima koji odgovaraju naslovu.
    - `404 Not Found` ako nijedan proizvod ne odgovara naslovu.
    - `500 Internal Server Error` ako se dogodi greška.

## Servisi

### ProductService
Ovaj servis djeluje kao middleware između KingICTWebAPI i vanjskog DUMMY API-ja.

#### Metode

- `GetAllProducts`
  - **Opis:** Dohvaća sve proizvode iz dummy API-ja.
  - **Odgovor:** 
    - Popis `Product` objekata.

- `GetProductById`
  - **Opis:** Dohvaća pojedinačni proizvod prema njegovom ID-u iz dummy API-ja.
  - **Odgovor:** 
    - `Product` objekt.

- `GetAllUsers`
  - **Opis:** Dohvaća sve korisnike iz dummy API-ja.
  - **Odgovor:** 
    - Popis `User` objekata.

## DTO Modeli

### UserDTO
  - `firstName` (string)
  - `lastName` (string)
  - `username` (string)
  - `password` (string)

### ProductDTO
  - `id` (int)
  - `title` (string)
  - `description` (string)
  - `category` (string)
  - `price` (decimal)
  - `images` (lista stringova)

### UserLoginDto
  - `username` (string)
  - `password` (string)

## Konfiguracija
Osigurajte da `appsettings.json` datoteka sadrži potrebnu konfiguraciju za JWT:

```json
{
  "JWT": {
    "SecureKey": "your-secure-key"
  }
}

Konfigurirajte appsettings.json datoteku s vašim sigurnosnim ključem.
Pokrenite aplikaciju.
Koristite API klijent (npr. Swagger) za interakciju s krajnjim točkama.
