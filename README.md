```
EVENT TICKETING SYSTEM
```

```
Right Click Database > Open in Terminal

dotnet ef dbcontext scaffold "Server=localhost;port=5432;Database=eventticketingsystem;User Id=postgres;Password=sasa@123;TrustServerCertificate=True;" Npgsql.EntityFrameworkCore.PostgreSQL -o AppDbContext -c AppDbContext -f
```

user name - admin
password - admin
