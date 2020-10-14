# Send Message to the another user

Send the text message to the another user with the receiver id.

**URL** : `/api/v1/Messaging/`

**Method** : `POST`

**Auth required** : YES, JWT must be added to the Authorization header

## Request Body Example

```json
{
  "receiverId": 2,
  "text": "Hello world"
}
```   

## Success Response   

**Code** : `200 OK`

**Response Example**

Message sent and added to DB

## Failure Response 

**Code** : `400`

Receiver user id not exists in the DB

```json
{
  "errorMessages": [
    "User that you are trying to send message is not exists"
  ]
}
```