# IDEA Test - Blazor Web App

🚀 **Web aplikacija za upravljanje korisnicima i ponudama, razvijena u .NET 9 i Blazor-u.**  
🔐 **Podržava autentifikaciju putem ASP.NET Core Identity.**  

## 📂 Struktura Projekta

📂 IDEA_Holding_Test
 ├── 📂 bin							 # Build fajlovi
 ├── 📂 Components					 # Blazor komponente (Frontend)
 ├── 📂 obj							 # Privremeni fajlovi prilikom build-a
 ├── 📂 Properties					 # Postavke aplikacije (launchSettings.json)
 ├── 📂 wwwroot						 # Statički fajlovi (CSS, JS, slike)
 ├── appsettings.Development.json    # Konfiguracija (baza podataka, auth)
 ├── appsettings.json				 # Konfiguracija (baza podataka, auth)
 ├── IDEA_Holding_Test.csproj		 # Projekt fajl
 ├── Program.cs						 # Glavna aplikacija (Backend API + Blazor)
 ├── README.md						 # Dokumentacija
 ├── TaskForDevelopers - IDEA.pdf    # Zadatak koji treba izvršiti
 
## 🛠 **Tehnologije**
✅ **.NET 9** - Backend API + ASP.NET Core Identity  
✅ **Blazor Web App** - Frontend framework  
✅ **Radzen Blazor** - UI komponente  
✅ **Entity Framework Core** - ORM za bazu podataka  
✅ **PostgreSQL** - Baza podataka  

## ⚡ **Kako pokrenuti projekat?**

1️) **Kloniraj repozitorijum**  
   ```sh
   git clone https://github.com/fejzo9/IDEA-Test.git
   cd IDEA-Test
   ```
2️) **Instaliraj potrebne pakete**
```
dotnet restore
```

3) **Konfiguriši bazu podataka**

Uredi appsettings.json i dodaj konekcioni string za PostgreSQL:
```
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=idea_test_db;Username=postgres;Password=yourpassword"
}
```
Kreiraj bazu i pokreni migracije:
```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

4) **Pokreni aplikaciju**
```
dotnet run
```

5) **Otvori u Browseru**
Aplikacija će raditi na: http://localhost:5059

🎨 Funkcionalnosti
- Korisnička autentifikacija (login, registracija)
- Uloge korisnika (Admin, User, Anonymous)
- CRUD operacije za korisnike i ponude
- Pretraga i filtriranje korisnika i ponuda
- Radzen UI komponente (DataGrid, Notifikacije, Dialogs)

**Autor: Fejzullah Ždralović**