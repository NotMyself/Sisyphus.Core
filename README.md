# Sisyphus
Keeps rolling that rock uphill on schedule.

![Sisyphus](/docs/images/sisyphus_wide.jpg?raw=true "Sisyphus")

## TOC

 - [Getting Started](#getting-started)
 - [External Dependencies](#external-dependencies)
 - [Creating a New Job Project](#creating-a-new-job-project)
 - [Implementing a Job](#implementing-a-job)
 - [Registering a Job in Sisyphus.Service](#registering-a-job-in-sisyphusservice)

### Getting Started

1. Clone the repository: `git clone https://github.com/NotMyself/Sisyphus.git`.
2. Change directory into the cloned repository `cd Sisyphus`.
3. Run script to register uri: `script/register_uri.ps1`.
4. Open the `Sisyphus.sln` in Visual Studio.
5. Open the Sql Server Object Explorer View, View > Sql Server Object Explorer.
6. Under the `(localdb)\MSSQLLocalDB` node, right click Databases and select `Add New Database`.
7. Set Database Name to `Sisyphus` and click `OK`.
8. Hit `F5` to start the service as a console application.
9. You should now be able to browse to [http://localhost:8080](http://localhost:8080) to see the Hangfire Dashboard.

### External Dependencies

1. [Autofac](https://autofac.org/) - Dependency Intection framework used for service and plugin registration
2. [TopShelf](http://topshelf-project.com/) - Service host framework used to host in console or windows service
3. [Hangfire](http://hangfire.io/) - Background Job Processing framework used to schedule and execute long running jobs
4. [Hangfire.Pro](http://hangfire.io/pro/) - Extensions to Hangfire that add several advanced job type scenarios

### Creating a New Job Project

1. Open the `Sisyphus.sln` solution.
2. Right click on the `Jobs` solution folder and select Add > New Project
3. Select `Class Library` from the `Visual C#` section.
4. Name your library `Sisyphus.Jobs.*JobName*` where JobName is the name of a single job or collection of jobs.
5. Set the Location to the `src` folder located at the root of the repository.

    ![Add New Project](/docs/images/add_new_project.PNG?raw=true "Add New Project")
6. Right click your new project and select Add > Existing Item.
7. Navigate to the `src` folder and select the `SolutionInfo.cs` file.
8. From the `Add` button drop down menu, select `Add As Link`.

    ![Add As Link](/docs/images/add_solution_info.PNG?raw=true "Add As Link")
9. Drag the linked `SolutionInfo.cs` into the `Properties` folder.
10. Open the `AssemblyInfo.cs` file in the Properties folder and remove all attrubutes except these:
    - AssemblyTitle
    - AssemblyDescription
    - Guid
11. Right click the Solution and select Manage Nuget Packages for Solution.
12. You may be prompted for credentails for the Hanfire nuget feed, they can be found in [NuGet.config](/NuGet.config?raw=true).
13. Add NuGet references to the following pacakges to your project using the installed tab of the Nuget Manager:
    - Autofac
    - Hangfire.Pro
    - Hangfire.Autofac
14. Right click the new project and select Properties.
15. Select the Build tab and modify the Output Directory for both Debug and Release as follows:
    - Debug = ..\Sisyphus.Service\bin\Debug\
    - Release = ..\Sisyphus.Service\bin\Release\ 
16. Right click the Solution and select `Project Dependecies`.
17. For the project `Sisyphus.Service` add a check next to your new project.

    ![Add Project Dependeices](/docs/images/add_project_dependecies.PNG?raw=true "Add Project Dependecies")
18. Build the Solution.
19. Ensure your new project's built assembly is located in the `bin` directory for the Sisyphus.Service project.

### Implementing a Job

1. Your new job project should have a Class1.cs, open it.
2. Implement the following interfaces found in `Sisyphus.Core`:
   - **IBackgroundJob** - implements the asyncronous `Task` to be run
   - **IBackgroundJobSchedule** - implements the schedule to run the Task
   - **IBackgroundJobScheduler** - implements the scheduling action
3. Rename the `Class1` class to something appropreate for your job as well as the `Class1.cs` file.

### Registering a Job in Sisyphus.Service

1. Add a new class named `*JobName*Module` where JobName is the name of your job or the collection of jobs.
   - It should match the name of your project.
2. Inherit from the `Module` base class in `Autofac`.
3. Override the `Load` method from the `Module` base class.
4. Using the `ContainerBuilder` register each of your jobs as `self`, `IBackgroundJob` and `IBackgroundJobScheduler`

    ```csharp
    builder.RegisterType<ExampleJob>()
                .AsSelf()
                .As<IBackgroundJob>()
                .As<IBackgroundJobScheduler>();
   ```
   
5. In the Sisyphus.Service project, open the App.config.
6. In the `autofac\modules` section add a weak assembly reference to your module.

   ```xml
   <module type="Sisyphus.Jobs.Example.ExampleModule, Sisyphus.Jobs.Example" />
   ```
   
7. Build the Solution and Run.
8. You should now see your job listed on the Hangfire Dashboard.
