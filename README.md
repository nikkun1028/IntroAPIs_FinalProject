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
5. basic response model (statusCode, statusDescription, list of items) ✅

<br />
<br />

## Documentation:
what's this API about?
> Monster Hunter API allows a client to see information about player, player's weapon equipment, and its weapon type.

endpoints and HTTP methods that a client can use:
> 3 endpoints are following: <br />
`api/Player` <br />
`api/Weapon` <br />
`api/WeaponType` <br/>

> HTTP methods for each endpoint: <br />
`GET, api/Player` will return a list of all players <br />
`GET, api/Player/ID` will return a player of ID specified <br />
`POST, api/Player` will add a new player to the players list <br />
`DELETE, api/Player/ID` will delete the player with ID from the list <br />
`GET, api/Weapon` will return a list of all weapons <br />
`GET, api/Weapon/ID` will return a weapon of ID specified <br />
`POST, api/Weapon` will add a new weapon to the weapons list <br />
`DELETE, api/Weapon/ID` will delete the weapon with ID from the list <br />
`GET, api/WeaponType` will return a list of all weapon types <br />
`GET, api/WeaponType/ID` will return a weapon type of ID specified <br />




sample request body:
> this is a request body for `POST api/Player` <br />
any misspelling of the property name will cause it to be null <br />
which will possibly throw an error because of the database constraints. <br />
Below is when you want assign a weapon to a new player `example_name` <br />
```
{
  "playerName": "example_name", 
  "weapon": null
}
```
and you will need to do `PUT api/Player/ID` after the POST <br />
in order to edit info about the weapon (to something not null) <br />
```
{

}
```


sample response body:
> this is a response body for `GET api/Weapon/4` <br />
```
{
  "statusCode": 200,
  "statusDescription": "GET successful",
  "weapons":[{ 
    "weaponID":4,"weaponType":null,"weaponName":"Kamura Glintblades I","atk":50,"critical":0
   }]
}
```

<br />
<br />

## Problems/Concerns:
Wanted to add more to the database about Monsters too, but it's going to be useless for this project (since it's not connected to any of other tables) so I decided not to include here. Also I gave up implementing Armors tables since I thought connecting 6 different tables to just Player table might be too much. I wanted this project to be less data. 😃 <br />
Another major problem is that whenever you try to do `POST api/Player` it tries to create a new weaponType item with duplicated WeaponTypeID (which results an error) if you implement it into the request body. So, if you want to reference the weapon instead of assigning new weapon to the player, you need to do `PUT api/Player/ID`😭

<br />
<br />

## MonsterHunter Rise/SunBreak:
it's a very good game you should play.
