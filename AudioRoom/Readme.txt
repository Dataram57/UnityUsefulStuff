****************************
* Audio Room by Dataram_57 *
*			v1			   *
****************************

*** Description ***

There are tons of momments where it is better to describe the sound by the space area, than 1 single point in the space.
And even if we use 1 single point in the space, there will be some ares that will require clones of it.
AudioRooms comes with a simple solution...
We describe the space area,on which our AudioSource will be moving.
This will give an effect of being inside of the party, where music seems to be everywhere.
By that AudioRoom gives lots usages, especially with the things like: Rains, Forest noises, Ambients, Drones etc...

*** Instruction ***

0. Add an AudioRoom class to your object
1. Create an area where you want to hear your sound at 0 distance.
2. Give it a group name
3. Create/Use GameObject with AudioSource component
4. To this object add AudioRoomFollow component, and set his group name to the group name of area that you want to use.
5. Set type value of the component to (Single or Optimal or Pool).

*If you want, you can change the code of AudioRoomFollow script to optimize unused features, or add something new. 

*** Types ***

Single			Behaves individually and include only first registered possible AudioRoom
Optimal			Behaves individually and includes all possible AudioRooms
Pool			Snaps his position to the closest and unused AudioRoom by any other pool GameObject

*** Classes ***

AudioRoom			Main class
AudioRoomEditor		Class for the unity editor
AudioRoomPack		Class that AudioRoom uses to locate AudioRooms with the same group name;

Optional:

AudioRoomFollow		Simple script that uses AudioRooms to change the position of the GameObject(with AudioSource) 
-enum AudioRoomFollowType		used for type selection

Example:

ExampleShowcaseMovement		Simple player movement

*** Assets from the Example ***

https://freesound.org/people/JarredGibb/sounds/243728/
https://freesound.org/people/FlatHill/sounds/237729/
https://freesound.org/people/XHALE303/sounds/535481/

*** Contact ***

Twitter: @Dataram_57

*** Donate ***
BTC: bc1qpps369cdkn87x008fre66ghhz8eunvl33wydy6
ETH: 0xAB96794853a126AF84106cF967C8011749e10471
XRP: rKj2BrQgz4b3YFQ368Qy1xeyn8DbHzC2dW