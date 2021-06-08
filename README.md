# Guestbook

Guestbook .NET Core Web API.

The application is currently hosted at `http://138.91.72.114/guestbook` running on AKS.

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

`POST /entries/add`

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
