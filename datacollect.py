# Python script for scraping data from gamestop.ca
import requests
from bs4 import BeautifulSoup
import json
import sqlite3

db = sqlite3.connect("library.db")
cur = db.cursor()

cur.execute('DROP TABLE IF EXISTS Game')


cur.execute('''CREATE TABLE IF NOT EXISTS Game(
                id TEXT PRIMARY KEY, name TEXT, 
                brand TEXT,  price FLOAT)''')

library = []

for i in range(24, 72, 24):
    URL = 'https://www.gamestop.ca/SearchResult/QuickSearchAjax?productType=2&typesorting=0&sdirection=ascending&skippos={}&takenum=24'.format(str(i))
    page = requests.get(URL)
    soup = BeautifulSoup(page.content, "html.parser")
    games = soup.find_all("div", class_ = "searchProductTile")    
    
    for game in games:        
        try:
            item = json.loads(game['data-product'])[0]
            item['price'] = float(item['price'])
            library.append(item)                                      

        except Exception as e: pass

    print("At index:", i)

print(games)
# Prepare insert statement
insert_statement = 'INSERT INTO Game (id, name, price, brand) VALUES (?, ?, ?, ?)'

# Convert list of dictionaries into a list of tuples, in order.
values = [(item['id'], item['name'], item['price'], item['brand']) for item in library]

# Execute bulk insert
cur.executemany(insert_statement, values)

# Commit transaction
db.commit()

# Close connection
db.close()