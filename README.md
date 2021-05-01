# Guestbook

Guestbook .NET Core Web API.

The application is currently hosted at `http://13.88.112.93/guestbook` running on AKS.

## Endpoints

`GET /entries`

Gets all entries in the guestbook

Response Body:
```
{
  "entries": [
    {
      "message": string,
      "timestamp": string
    }
  ]
}
```
---

`POST /entires/add`

Add an entry to the guestbook

Request Body:
```
{
  "message": string
}
```

---

`DELETE /entries/clear`

Remove all entries from the guestbook
