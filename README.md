# BYUBroadcastingCodingChallenge
Coding Challenge given by BYUBroadcasting for evaluation.


Hey awesome BYU-B programmers!

This was quite the test you guys set up here. Tricky tricky ;) you should read this article. https://medium.com/@rodbegbie/find-open-restaurants-an-engineering-take-home-test-dissected-1ada20282ceb

I decided to do a C# MVC Razor application for it, just to play around with it and have fun. Fun it was not. 

It’s harder than it looks. Ah, dates and times. Anyone who’s had to work with them is scarred. 
What could seem simple at first glance (Naïve approach: If the restaurant is open on the day, see if the 
requested time is greater than the opening timeand less than the closing time) breaks when you have restaurants open past midnight. Nothing in the question hints at this, so it was only discoverable by running your code and realizing that some restaurants
you expected to see didn’t get returned. This is a question that rewards attention to detail.

I got it finished, and here are the steps that you will need to take to get it running:

1. Download the Project from Github.
2. Open the Project in Visual Studio.
3. Open Tools > Nuget Package Manager > Package Manager Console.
4. In the Package Manager Console, run "Update-Database". (This creates the Localdb needed to store the restaurant objects).
5. Run the project.
6. On the Navbar, click Restaurants.
7. I made it so you don't have to select any dates and it will give you all of the restaurants, or you can either put in a date or time or both and it will filter it accordingly. 


Since you probably want to look and see how I did it, most of the work is done on two different files.
1. Models > SeedRestaurantInfo > SeedRestaurantData.cs. This is where I parse the JSON and create the DB objects. (This is run when you first start the project.)
2. Pages > Restaurants > Index.cshtml.cs. This is where I do the querying and filtering. (This happens when you load the Restaurants page/click the filter button).
