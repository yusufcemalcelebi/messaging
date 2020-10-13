# Messaging Service

- These app consists of basic Messaging methods for a real world scenario with the Authentication mechanisms.   
- JWT is used to implement Authentication and Authorization functionalities.
- .Net Core 3.1 Web Api is used with the layered architecture. EF Core is used to implement data access logic. 
- Code First Migration is used to create require tables in the SQL Server 2017. 
- Application is dockerized, docker-compose can be used to start the application and require database on docker.

## Open Endpoints

Open endpoints require no Authentication.

* [Login](login.md) : `POST /api/v1/Authentication/Login/`
* [Register](register.md) : `POST /api/v1/Authentication/Register/`

## Endpoints that require Authentication

Closed endpoints require a valid Token in the Authorization header of the
request. A Token can be acquired from the Login or Register endpoints

### Message Endpoints

Message Controller includes the API's to get messages and to send message. 

* [Get Messages](messaging/get.md) : `GET /api/v1/Messaging/`
* [Send Message](user/post.md) : `POST /api/v1/Messaging`

### Blocking

Blocking API's can be used to block and to unblock the target users. 

* [Create or Update Block](blocking/post.md) : `POST /api/v1/Blocking/`