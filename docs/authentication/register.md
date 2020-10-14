# Register

Register to the system with the email, username and password.

**URL** : `/api/v1/Authentication/Register`

**Method** : `POST`

**Auth required** : NO

## Request Body Example

```json
{
  "username": "yusuf.celebi",
  "password": "123456789",
  "email": "test@gmail.com"
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
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.   eyJpZCI6IjEiLCJuYmYiOjE2MDI2OTE3OTEsImV4cCI6MTYwMjc3ODE5MSwiaWF0IjoxNjAyNjkxNzkxfQ.IJL0slI5jXm8ZXtWrbYwV5iyEn2ySaRmJVQIc2TVN3c"
}
```

## Failure Response 

**Code** : `400`

If username and email already exists

```json
{
  "errorMessages": [
    "Email is already exists. Please try a different email",
    "Username is already exists. Please try a different username"
  ]
}
```