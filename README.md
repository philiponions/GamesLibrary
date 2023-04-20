# GamesLibrary
GamesLibrary is a RESTful API developed with ASP.NET Core and EF Core that allows users to access information about over 1000 video games. The data was collected by scraping gamestop.ca with a Python script and the BeautifulSoup4 library. The API is connected to a SQLite database.

I deployed the app with a docker container at render.com
<br> Check it out! I provided the Swagger UI documentation on the site.

https://gameslib.onrender.com/swagger/index.html

<img width="800" alt="Screenshot 2023-04-19 at 7 52 06 PM" src="https://user-images.githubusercontent.com/78581216/233237430-ecd5ecf6-524b-4941-84c5-a2e80296baba.png">

## Why I made this
After creating a bunch of API's with Node.js and Express I wanted to find another way to write them. ASP.NET Core is one of the most popular frameworks out there, so I decided on learning that. I also wanted to learn how to containerize apps using Docker.

## Things I learned:
- C# (more so a refresher. Haven’t coded in c# for years)
- SQL and SQLite (a refresher as well)
- ASP.NET Core 
- Using an ORM (Entity Framework Core)
- Containerizing applications via Docker
- Web scraping with Python and BeautifulSoup 4



## Getting started
If you want to use the API locally, you will need to have Visual Studio and Docker installed on your machine.

Clone this repo and open the solution in Visual Studio. Run the Dockerfile which can be found under the /GamesLibrary directory

## API Endpoints
### GET: api/Games
Retrieves a list of games by page number.

```C#
[HttpGet]
public async Task<ActionResult<IEnumerable<Game>>> GetGames(int pageNumber = 1, int pageSize = 10)
```

### GET: api/Games/price
Gets a list of games based on a specified price.

```C#
[HttpGet("price")]
public async Task<ActionResult<IEnumerable<Game>>> GetGames([FromQuery] string condition, [FromQuery] float price)
```

### GET: api/Games/brand/{brand}
Grabs a list of games from a specific console.
```C#
[HttpGet("brand/{brand}")]
public async Task<ActionResult<IEnumerable<Game>>> GetGames(string brand)
```

Allowed strings:
```
xb1 - Xbox One
xsx - Xbox Series X
ps5 - PS5
ps4 - PS4
pc - PC
switch - Switch
```


### GET: api/Games/name
Gets one or more games based on a specified substring.
```C#
[HttpGet("name")]
public async Task<ActionResult<IEnumerable<Game>>> GetGameByName(string searchString)
```

### GET: api/Games/{id}
Grabs a game by id.
```C#
[HttpGet("{id}")]
public async Task<ActionResult<Game>> GetGameById(string id)
```
