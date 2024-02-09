# DNFS - Salt Stars Day 5

## A. Scenario

There's screaming from the coffee room and as you enter you see three coworkers involved in a very lively argument.

> WITHOUT LUKE IT WOULDN'T BE A SERIES!

> Calm down. I'm just saying that THE ROBOTS ARE THE ONLY ONES IN ALL THE MOVIES

> Leia!?! Typically guys, and not think about Leia. She is the one that has the force the longest.

Apparently the hero lister has become a thing and now people are only talking about their childhood heroes and which one is better. If only we could rate them some ... wait a minute.

You rush back to the mob and say:

> Phineas, i know what we're gonna do today!

## B. What you will be working on today

Today we will add a simple rating function, by letting the user click on a star symbol for the amount of awesomeness that a certain hero has, according to you. The database for this will be comma separated text-file, with each hero's id and star count. Like this:

```text
1,4
3,3
14,2
2,5
```

You will build this as a new endpoint in the `HeroesController` and store the file in a `data`-directory within the `Salt.Stars.API`.

The stars will be presented on the Hero's page and should be clickable to update the information.

## C. Tools and requirements

## D. Lab instructions

Today's task is a bit tricky and requires code in quite a few different pages. Here is what you should do:

- Implement the `IStarFileClient` in the `StarFileClient.cs` file.

  - We have supplied you with a few helper methods that will assist you in the file reading/writing
  - Use the `StarFileReaderIntegrationTests` tests to drive out the code

- Implement a new endpoint `HeroesController` for `HttpPatch`

  - Patch is the correct way to add additional data to a resource according to REST
  - Use the tests for the endpoint to guide you in how to write the code
  - Use the Model classes for the `HeroStarUpdateRequest` and `HeroStarUpdateResponse` without changing them

- Update the `HeroesController.GetHeroAsync` so that it also includes the `StarRating` property.

  - The endpoint should return the entire resource (Hero) so this is the right spot to aggregate the data
  - Use the `IStarFileClient.GetStarsForHero` method to get the star rating from the file
  - You implemented that in the first step

- Call the API from the Salt.Start.Web application

  - Implement the new method `UpdateStars` in the `HeroApiClient`
    - Note that you need to do a `HttpClient.PatchAsync` this time and that the request will be a `StarUpdateRequest` and the response a `StarUpdateResponse`
    - These objects need to be serialized to and from JSON. We have done from before, using `JsonSerializer.DeserializeAsync` but to JSON is done with the `JsonSerializer.Serialize`.
    - Pass the serialized `StarUpdateRequest` as the body of the `HttpClient.PatchAsync` by creating a body `new StringContent(json, Encoding.UTF8, "application/json");`

- Show the stars on the Blazor page

  - Update `HeroResponse.Hero` to hold a `StarRating` property
  - Show the `HeroResponse.Hero.StarRating` property in a suitable/good looking way on the page

- Finally - allow the user to update the star counts

  - Add input `<input @bind="Rating">` around to include the `Model.HeroResponse.Hero.StarRating`
  - Implement a `SetStarRating` function to call the `IHeroAPIClient.UpdateStars` method
  - When it succeeds, show an indication on the page to ensure to the user that the data has been updated.

- If you have time over, consider making a nicer presentation of the stars, with some proper ⭐️⭐️⭐️ kind of display

### Tips

- Be sure to go slow and take breaks.
- Ensure that everyone follow along
- Use the tests and the Run/Debug-options to understand what is happening.

---

Good luck and have fun!
