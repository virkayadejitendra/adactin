# adactin - test
following repo consits of two projects
1-InsurenceAPI 
   -.Net Core Web Api app
2-InsurenceSPA
  -Angular Client app
    
## 1 for Running the Web Api Application

Before running the application, make sure you have the following prerequisites installed:

- [.NET Core 7 runtime](https://dotnet.microsoft.com/download)

Follow these steps to run the application:

1. Clone the repository: `https://github.com/virkayadejitendra/adactin.git`
2. Navigate to the project directory: `cd repository/InsurenceAPI`
3. Build the application: `dotnet build`
4. Start the application: `dotnet run`

The application will be accessible at `http://localhost:5214`.

## 2 Running the Angular Client Application

Before running the application, make sure you have the following prerequisites installed:

- [Node.js](https://nodejs.org)
- [Angular CLI](https://angular.io/cli)

Follow these steps to run the application:

1. Clone the repository: `https://github.com/virkayadejitendra/adactin.git`
2. Navigate to the project directory: `cd repository/InsurenceSPA`
3. Please verify the WEB API app's base apiUrl mentioned in following environment.ts file is same as it current running if not please replace with current running apiUrl
      "..\InsurenceSPA\src\environments\environment.ts"
5. Install the dependencies: `npm install`
6. Start the development server: `ng serve --open`

The application will be accessible at `http://localhost:4200`.
