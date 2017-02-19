# Campspot Developer Programming Chanllenge #

## Prerequisites ##
1. Download & Install [Visual Studio Community](https://www.visualstudio.com/vs/community/)
2. Download & Install [Git](https://git-scm.com)

## How to Build ##
1. Clone this repo `git clone https://github.com/caseymichael/campspot.git campspot`
2. Open the campspot.sln with Visual Studio Community.
3. To build use the shortcut `CTRL + SHIT + B` or go to the "Build > Build" file menu.

## How to Run Program ##
After building the application you can debug the application using Visual Studio Community by pressing `f5`. 
You can also open a command prompt and navigate to the executable folder "Campspot\bin\Debug" and run `Campspot.exe "filepath"` passing in the file location of your test cases.

## How to Execute Tests ##
You can execute the unit tests by going to the file menu "Test > Windows > Text Explorer" then clicking "Run All"


### High Level Description of approach ###
1. Get the test-case.json data into C# objects
	a. Direct mapping to a RootObject
	b. Break a part the RootObject into separate classes for easier manipulation.
2. Break a part the logic into separate rules that can be applied in order and expanded upon.
	a. A campsite is not available if a previous reservation overlaps with the search query.
	b. A campsite is not available if it would create a gap with the reservations before and after the search query.
	c. If all other rules pass then the campsite is available.

### Assumptions or Special Considerations ###
1. I didn't take into account checkin / checkout times. For example, you would checkout of the campsite by 11am and the next person could checkin to that same campsite around 2pm allowing for reservations to technically occur on the same day.