# TestTask

With the `TestTask.Frontend` project, you can create surveys and add questions to them.

### The solution consists of 4 projects:
* TestTask.Api
* TestTask.Frontend
* TestTask.Data
* TestTask.Tests

**To start the `TestTask.Api` project**, you need to specify in the `appsettings.json` file the **connection string** to your databases.
**To start the `TestTask.Frontend` project**, you need to specify in the `appsettings.json` file the **url address** at which you will have the TestTask.Api project deployed.

The `TestTask.Data` project contains the `Survey` and `Question` object models.

The `TestTask.Tests` project contains unit tests of the `TestTask.Api` project controllers.
Also, if you run and go to the root address of the `TestTask.Api` project, you will be taken to the `Swagger UI`, where you can test all the Api methods.
