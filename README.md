## FinalProject
CSCI 39537 Intro to APIs - MonsterHunter APIs
<br />
Due 4/26/2023 at 11:40PM
<br />
Rei Imai (24134776)
<br />
<br />

## Submission Checklist:
1. `README.md` on root repo ✅
2. `.sql` code for database ✅
3. three API endpoints and three HTTP methods 🛑
4. at least one controller ✅
5. basic response model (statusCode, statusDescription, list of items) 🛑

<br />
<br />

## Documentation:
what's this API about?
> Monster Hunter API allows a client to see information about player, player's weapon equipment, and its weapon type.

endpoints that a client can use:
> 3 endpoints and available HTTP methods for each endpoint are following: <br />
```GET, POST, DELETE     api/Player``` <br />
```GET, POST, DELETE     api/Weapon``` <br />
```GET                  api/WeaponType```

sample request body:
> this is a request body for POST api/Player <br />
any misspelling of the property name will cause it to be null <br />
which will possibly throw an error because of the database constraints.
```
{
  "playerName": "example_name", 
  "weapon": {
    "weaponType": {
      "weaponTypeID": 1,
      "weaponTypeName": "Great Sword"
    },
    "weaponName": "example_weapon",
    "atk": 100,
    "critical": 0
  }
}
```



sample response body:
> this is a response body for GET api/Weapon/4 <br />
```
{
  "weaponID": 4,
  "weaponType": null,
  "weaponName": "Kamura Glintblades I",
  "atk": 50,
  "critical": 0
}
```

<br />
<br />

## Problems/Concerns:
Wanted to add more to the database about Monsters too, but it's going to be useless for this project (since it's not connected to any of other tables) so I decided not to include here. Also I gave up implementing Armors tables since I thought connecting 6 different tables to just Player table might be too much. I wanted this project to be less data. 😃

<br />
<br />

## MonsterHunter Rise/SunBreak:
it's a very good game you should play.
