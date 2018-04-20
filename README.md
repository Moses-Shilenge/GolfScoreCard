# GolfScoreCard

The application can now calculate the golf score using stroke play method and Stable Ford points methods.
### The application has the following views:
* Score board (Create page)
  * I didn't complicate the create function as of yet, on this form, you can add a **players name** and **score** for each **hole**. 
* Previous Scores (View page)
  * The default view displays the stroke play scores. The list is sorted by **date** and each row can be **updated** or **deleted**
  * Below the table I've added two buttons, one navigates you the **create page** and the second navigates you to the **Stable Ford Score view**, there's no consistency in the style as I am still experimenting with the views.
* Edit and Delete views
  * pretty much self explanatory.
  
## Golf terms
* **Par:** number of expected shots for a hole.
* **Index:** represents the level of difficulty of the hole ranging from 1 being the hardest to 18 the easiest.
* **Handicap:** The number shots credited for playing skill or ability in the game. The lower the handicap the more experienced the player.

Moving on I will be implementing stableford points (par scoring).
### Points are awarded as follows:
* on par = 2 points
* 1 over the par = 1 point
* 2 over the par = 0 points

* 1 under par = 3 points
* 2 under par = 4 points

I am also new to Golf, so to make life easier I've added the two links which helped me understand how this should work:

* https://www.youtube.com/watch?v=UO4Zri-H3AE
* https://www.youtube.com/watch?v=cZelYhBaE2M&t=25s

## Setup
I've added a **Resources** folder containing a database backup, please use this when running the project. I've added the Tees in the DB and the Migrations doesn't insert these values as of yet, I'll include this in my next commit. 

### You can restore the database from the file in the resource folder.
