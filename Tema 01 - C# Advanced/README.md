### Workflow  


Basically we have a <b>  Fundraiser.cs  </b> class which encapsulates the  specified attributes  and a <b> Donations.cs </b> class.  

The donation class included a dictionary of <b> Currency : Ammount </b>.

I have included the <b> Fundraisers </b> as a IRegistry in <b> PetShelter.cs </b> to keep them in the in-memory database. Also I have updated the pet shelter with donation logic.
#### First step is just to create one. 

By running the console, just register as specified.
``` 
2. Register a fundraiser  
``` 

#### After selecting the fundraiser, it displays basic info and further asking if you want to donate. 

After registration, you can easily access them by:

``` 
6. See fundraisers 
```  
That would prompt: 
``` 
Fundraisers to choose from:
1. Title 1 
``` 

We would select 1

``` 
About Title 1: Cat help, Donation Target: 100,EUR, At the moment: RON=0,EUR=0,USD=0
Total ammount in target currency: 0

Current donators:
Would you like to donate ?
1. yes
2. No, thank you.
``` 

And we would donate: 

```
What's your name? (So we can credit you.)
Nicu

What's your personal Id? (No, I don't know what GDPR is. Why do you ask?)
2

How much would you like to donate? Please specify the ammound and  currency: RON EUR or USD. Example: 100 RON
100 RON

``` 

#### To repeat the 6th option to see the updated fundraiser.  

If you keep donating, the prompt would look like this:

``` 

Fundraisers to choose from:
1. Title1
1

About Title1: CatHelpDescription, Donation Target: 100,EUR, At the moment: RON=0,EUR=0,USD=200
Total ammount in target currency: 186

Current donators:
Nicu
The fundraiser reached its Donation Target! Please take a look at other fundraisers!
1. Ok. Thank you!
1 

```  


# I do have to mention that I did not implement the logic for checking of  repeating donors ( It would just add to the list the same name). 
### 