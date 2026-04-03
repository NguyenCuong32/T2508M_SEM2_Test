# Hero Game Player Management

Ung dung JavaFX quan ly nguoi choi cho bai final test.

## Yeu cau

- JDK 21 hoac moi hon
- MySQL

## Tao database

Chay file [final_test/src/main/resources/db/script.sql](final_test/src/main/resources/db/script.sql).

## Chay ung dung

```powershell
cd final_test
$env:JAVA_HOME='C:\Program Files\Java\jdk-25.0.2'
.\mvnw.cmd javafx:run
```

## Cau hinh database tuy chon

Mac dinh:

- `jdbc:mysql://localhost:3306/HeroGame`
- user `root`
- password `123456`

Co the ghi de bang system property hoac environment variable:

- `db.url` hoac `DB_URL`
- `db.user` hoac `DB_USER`
- `db.pass` hoac `DB_PASS`
