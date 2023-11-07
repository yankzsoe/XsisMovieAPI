# XsisMovieAPI

## Gambaran Umum

Project ini dibuat untuk teknikal test di PT. XSIS MITRA UTAMA.

## Fitur Utama
REST API dibangun dengan NET Core 6 dengan menggunakan Postgres serta menerapkan Clean Architecture 
- Fitur 1: Menggunakan Migration dan Code First strategy
- Fitur 2: CQRS dengan MediatR
- Fitur 3: Input Validation dengan FluentValidation
- Fitur 4: Error handling dengan Middleware
- Fitur 5: Monitor performance sederhana pada pipeline
- Fitur 6: Menerapkan Dependency Injection
- Fitur 7: N-tier domain

## Sebelum Menjalankan Program
- Ubah connctionstring yang ada pada appsettings.json (XsisMovieAPI.WebAPI)
- Jalankan Migration dengan membuka halaman Package Manager Console
- Arahkan "Default Project" pada project "XsisMovieAPI.Infrastructure"
- Ketik perintah berikut "Add-Migration init"
- Kemudian ketik "Update-Database"
- Setelah semua step berhasil dilakukan, maka sudah bisa di-running 
