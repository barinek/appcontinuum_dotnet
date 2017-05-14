# Application Continuum (.NET Core)

.NET Core Microservices Reference Architecture

### Database Setup

```
mysql -uroot --execute="create database uservices_test"

mysql -uroot --execute="grant all on uservices_test.* to 'uservices'@'localhost' identified by 'uservices';"
```

### Schema Migrations

```
flyway -url="jdbc:mysql://localhost:3306/uservices_test?user=root&password=" -locations=filesystem:databases/continuum clean migrate
```

### Test and Production Environment

````
export PORT=8081

export VCAP_SERVICES='{ "p-mysql": [ { "credentials": { "hostname": "localhost", "port": 3306, "name": "uservices_test", "username": "root", "password": "" } } ] }'
 
export REGISTRATION_SERVER_ENDPOINT=http://localhost:8883
````