# Blocking the another user

Blocking the another user with the UserId. Blocked user will not able to send message to the blocker user.

**URL** : `/api/v1/Blocking/`

**Method** : `POST`

**Auth required** : YES, JWT must be added to the Authorization header

## Request Body Example

```json
{
  "blockedId": 2,
  "isActive": true
}
```   

## Success Response   

**Code** : `200 OK`

**Response Example**

User blocked

## Failure Response 

**Code** : `400`

Blocked user is not exists

```json
{
  "errorMessages": [
    "User that you are trying to block is not exists"
  ]
}
```