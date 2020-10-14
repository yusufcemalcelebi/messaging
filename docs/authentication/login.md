# Log in to the system

Login to system with username and password. Take the authentication token.

**URL** : `/api/v1/Authentication/Login`

**Method** : `POST`

**Auth required** : NO

## Request Body Example

```json
{
  "username": "yusuf.celebi",
  "password": "123456789"
}
```   

## Success Response   

**Code** : `200 OK`

**Response Example**

User added to DB and token is given

```json
{
  "id": 1,
  "username": "yusuf.celebi",
  "email": "test@gmail.com",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJuYmYiOjE2MDI2OTIxMjYsImV4cCI6MTYwMjc3ODUyNiwiaWF0IjoxNjAyNjkyMTI2fQ.GBDdGi_kxgWcUzS6vdNv-W6vnKt-GOYA9D0eOCLgJF0"
}
```

## Failure Response 

**Code** : `400`

Invalid login case

```json
{
  "errorMessages": [
    "Invalid login"
  ]
}
```